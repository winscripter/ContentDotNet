using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Cabac;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Minimal;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Extensions.H26x;
using ContentDotNet.Primitives;
using System.Reflection.PortableExecutable;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents the H.264 slice, with macroblocks.
/// </summary>
public sealed class H264Slice
{
    private static readonly Dictionary<int, int> s_intra16x16MbTypeToIntraPredMode = new()
    {
        // 0
        [SliceTypes.I_16x16_0_0_0] = 0,
        [SliceTypes.I_16x16_0_0_1] = 0,
        [SliceTypes.I_16x16_0_1_0] = 0,
        [SliceTypes.I_16x16_0_1_1] = 0,
        [SliceTypes.I_16x16_0_2_0] = 0,
        [SliceTypes.I_16x16_0_2_1] = 0,
        // 1
        [SliceTypes.I_16x16_1_0_0] = 1,
        [SliceTypes.I_16x16_1_0_1] = 1,
        [SliceTypes.I_16x16_1_1_0] = 1,
        [SliceTypes.I_16x16_1_1_1] = 1,
        [SliceTypes.I_16x16_1_2_0] = 1,
        [SliceTypes.I_16x16_1_2_1] = 1,
        // 2
        [SliceTypes.I_16x16_2_0_0] = 2,
        [SliceTypes.I_16x16_2_0_1] = 2,
        [SliceTypes.I_16x16_2_1_0] = 2,
        [SliceTypes.I_16x16_2_1_1] = 2,
        [SliceTypes.I_16x16_2_2_0] = 2,
        [SliceTypes.I_16x16_2_2_1] = 2,
        // 3
        [SliceTypes.I_16x16_3_0_0] = 3,
        [SliceTypes.I_16x16_3_0_1] = 3,
        [SliceTypes.I_16x16_3_1_0] = 3,
        [SliceTypes.I_16x16_3_1_1] = 3,
        [SliceTypes.I_16x16_3_2_0] = 3,
        [SliceTypes.I_16x16_3_2_1] = 3,
    };

    // Fields like _mbs and _mapUnitToSliceGroupMap are likely to be allocated on the LOH.
    // Thus, setting them to null will hint the GC to collect it.
    // We care about memory usage, significantly in fact.

    private readonly int[] _mapUnitToSliceGroupMap;
    internal readonly LimitedList<MinimalMacroblockLayer> _mbs;
    internal readonly LimitedList<bool> _mbFieldDecodingFlags;
    internal readonly LimitedList<bool> _mbSkipFlags;
    private CabacManager? _cabac;
    private readonly int _initialCurrMbAddr;

    internal readonly SequenceParameterSet SPS;
    internal readonly PictureParameterSet PPS;
    internal readonly SliceHeader SliceHeader;

    internal DerivationContext Context;
    internal NalUnit NAL;

    private readonly IMacroblockUtility _util;

    private readonly long _nalLen;

    private readonly BitStreamReader _connectedBsReader;

    private readonly IntraInterDecoder _intraInterDecoder;

    internal H264Slice(SequenceParameterSet sps, PictureParameterSet pps, SliceHeader shd, BitStreamReader reader, NalUnit nal, long nalLength)
    {
        _nalLen = nalLength;
        NAL = nal;

        SPS = sps;
        PPS = pps;
        SliceHeader = shd;

        _mbs = new LimitedList<MinimalMacroblockLayer>(SPS.GetPicSizeInMapUnits() + 1);
        _mbFieldDecodingFlags = new LimitedList<bool>(SPS.GetPicSizeInMapUnits() + 1);
        _mbSkipFlags = new LimitedList<bool>(SPS.GetPicSizeInMapUnits() + 1);
        _initialCurrMbAddr = (int)shd.FirstMbInSlice * (2 - Int32Boolean.I32(shd.GetMbaffFrameFlag(sps)));

        _mapUnitToSliceGroupMap = new int[SPS.GetPicSizeInMapUnits()];

        _util = new MbUtilImpl(this);

        _connectedBsReader = reader;

        Context = new DerivationContext(
            0,
            SliceHeader.GetMbaffFrameFlag(SPS),
            false,
            Util264.GetMbWidthHeightC(SPS),
            default,
            _initialCurrMbAddr,
            0,
            0,
            SPS.GetPicWidthInSamplesL(),
            false,
            (int)(SPS.BitDepthLumaMinus8 + 8),
            (int)(SPS.BitDepthChromaMinus8 + 8)
        );

        _intraInterDecoder = new IntraInterDecoder(Context, _util, SPS.GetFrameSize());

        CreateMapUnitToSliceGroupMaps();
        Parse();
    }

    private int NextMbAddress(int n)
    {
        int PicSizeInMbs = (int)((SPS.PicWidthInMbsMinus1 + 1) * (SPS.PicHeightInMapUnitsMinus1 + 1));
        int i = n + 1;
        while (i < PicSizeInMbs && GetMbToSliceGroupMap(i) != GetMbToSliceGroupMap(n))
            i++;
        return i;
    }

    /// <summary>
    ///   Parses an H.264 slice.
    /// </summary>
    /// <param name="sps">The SPS</param>
    /// <param name="pps">The PPS</param>
    /// <param name="shd">The Slice Header</param>
    /// <param name="reader">The Bitstream reader</param>
    /// <param name="nal">The NAL</param>
    /// <param name="nalLength">The length of the NAL</param>
    /// <returns>An H.264 slice</returns>
    public static H264Slice ParseSlice(SequenceParameterSet sps, PictureParameterSet pps, SliceHeader shd, BitStreamReader reader, NalUnit nal, long nalLength)
        => new(sps, pps, shd, reader, nal, nalLength);

    private void Parse()
    {
        if (PPS.EntropyCodingModeFlag)
        {
            _cabac = new CabacManager(_connectedBsReader, _util)
            {
                CabacInitIdc = (int)SliceHeader.CabacInitIdc,
                ChromaArrayType = (int)SPS.GetChromaArrayType(),
                DerivationContext = Context,
                L0Mode = true,
                PicWidthInMbs = (int)(SPS.PicWidthInMbsMinus1 + 1),
                SliceType = SliceHeader.GetSliceType()
            };
            Console.WriteLine("Offset: " + _cabac.Decoder.CodIOffset + ", range: " + _cabac.Decoder.CodIRange);
            while (!Util264.ByteAligned(_connectedBsReader))
                _ = _connectedBsReader.ReadBit(); // cabac_alignment_one_bit
        }
        int iteration = 0;
        bool moreDataFlag = true;
        bool prevMbSkipped = false;
        int CurrMbAddr = _initialCurrMbAddr;
        bool mbFieldDecodingFlag;
        do
        {
            Console.WriteLine("iter: " + iteration);
            iteration++;
            bool mbSkipFlag = false;
            mbFieldDecodingFlag = false;
            GeneralSliceType sliceType = this.SliceHeader.GetSliceType();
            if (sliceType != GeneralSliceType.I && sliceType != GeneralSliceType.SI)
            {
                if (!this.PPS.EntropyCodingModeFlag)
                {
                    uint mbSkipRun = _connectedBsReader.ReadUE();
                    prevMbSkipped = mbSkipRun > 0;
                    for (int i = 0; i < mbSkipRun; i++)
                    {
                        CurrMbAddr = NextMbAddress(CurrMbAddr);
                        Context.CurrMbAddr = CurrMbAddr;
                        Context.NeighboringMacroblocks.Refresh(CurrMbAddr, SPS);

                        this.Context.CurrMbAddr = CurrMbAddr;
                    }

                    if (mbSkipRun > 0)
                        moreDataFlag = Util264.MoreRbspData(_connectedBsReader, _nalLen);
                }
                else
                {
                    mbSkipFlag = Int32Boolean.B(_cabac!.ParseMbSkipFlag());
                    moreDataFlag = !mbSkipFlag;
                }
            }

            if (moreDataFlag)
            {
                if (SliceHeader.GetMbaffFrameFlag(SPS) &&
                    (CurrMbAddr % 2 == 0 || (CurrMbAddr % 2 == 1 && prevMbSkipped)))
                {
                    mbFieldDecodingFlag = Int32Boolean.B(_cabac!.ParseMacroblockFieldDecodingFlag());
                }
                Context.IsMbaffFieldMacroblock = mbFieldDecodingFlag;
                var mb = ParseMacroblockLayer(mbFieldDecodingFlag);
                Context.MbType = (int)mb.MbType;
                _mbs!.Add(mb);
                _mbFieldDecodingFlags.Add(mbFieldDecodingFlag);
                _mbSkipFlags.Add(mbSkipFlag);
            }

            if (!PPS.EntropyCodingModeFlag)
            {
                moreDataFlag = Util264.MoreRbspData(_connectedBsReader, _nalLen);
            }
            else
            {
                if (sliceType != GeneralSliceType.I && sliceType != GeneralSliceType.SI)
                    prevMbSkipped = mbSkipFlag;

                if (SliceHeader.GetMbaffFrameFlag(SPS) && CurrMbAddr % 2 == 0)
                {
                    moreDataFlag = true;
                }
                else
                {
                    bool endOfSliceFlag = Int32Boolean.B(_cabac!.ParseEndOfSliceFlag());
                    moreDataFlag = !endOfSliceFlag;
                }
            }
            CurrMbAddr = NextMbAddress(CurrMbAddr);
            if (_cabac is not null)
            {
                Context.CurrMbAddr = CurrMbAddr;
                Context.NeighboringMacroblocks.Refresh(CurrMbAddr, SPS);

                this.Context.CurrMbAddr = CurrMbAddr;
            }
        }
        while (moreDataFlag);
    }

    private int GetMbToSliceGroupMap(int i)
    {
        int PicWidthInMbs = (int)(SPS.PicWidthInMbsMinus1 + 1);
        if (SPS.FrameMbsOnlyFlag || SliceHeader.FieldPicFlag)
            return _mapUnitToSliceGroupMap[i];
        else if (SliceHeader.GetMbaffFrameFlag(SPS))
            return _mapUnitToSliceGroupMap[i / 2];
        else
            return _mapUnitToSliceGroupMap[(i / (2 * PicWidthInMbs)) * PicWidthInMbs + (i % PicWidthInMbs)];
    }

    /// <summary>
    ///   Builds the resulting frame from the slice.
    /// </summary>
    /// <param name="frame">Where resulting pixels are placed.</param>
    public void BuildFrame(IFrame frame)
    {
        int picWidthInMbs = (int)(SPS.PicWidthInMbsMinus1 + 1);
        int picHeightInMapUnits = (int)(SPS.PicHeightInMapUnitsMinus1 + 1);
        int chromaArrayType = (int)SPS.GetChromaArrayType();
        GeneralSliceType sliceType = SliceHeader.GetSliceType();

        for (int mbAddr = 0; mbAddr < _mbs.Count; mbAddr++)
        {
            var mb = _mbs[mbAddr];
            int mbX = mbAddr % picWidthInMbs;
            int mbY = mbAddr / picWidthInMbs;

            switch (sliceType)
            {
                case GeneralSliceType.I:
                    {
                        switch (Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, sliceType))
                        {
                            case SliceTypes.Intra_16x16:
                                {
                                    _Core();
                                    break;

                                    void _Core()
                                    {
                                        Span<int> leftL = stackalloc int[16];
                                        Span<int> topL = stackalloc int[16];
                                        int topLeft;

                                        for (int i = 0; i < 16; i++)
                                        {
                                            if (mbX > 0)
                                            {
                                                leftL[i] = frame.Y[(mbX * 16) - 1, (mbY * 16) + i];
                                            }
                                            
                                            if (mbY > 0)
                                            {
                                                topL[i] = frame.Y[(mbX * 16) + i, (mbY * 16) - 1];
                                            }
                                        }

                                        topLeft = mbX > 0 && mbY > 0 ? frame.Y[(mbX * 16) - 1, (mbY * 16) - 1] : 0;

                                        Span<int> buf = stackalloc int[16 * 16];

                                        var p = new IntraPredictionSamples(buf, leftL, topL, topLeft);

                                        Span<int> resultBuf = stackalloc int[16 * 16];
                                        Matrix16x16 result = new(resultBuf);

                                        _intraInterDecoder.IntraPredictor.Intra16x16SamplePredict(
                                            frame.Y,
                                            result,
                                            PPS.ConstrainedIntraPredFlag,
                                            s_intra16x16MbTypeToIntraPredMode[(int)mb.MbType],
                                            p,
                                            Context);

                                        for (int x = 0; x < 16; x++)
                                        {
                                            for (int y = 0; y < 16; y++)
                                            {
                                                frame.Y[(mbX * 16) + x, (mbY * 16) + y] = result[x, y];
                                            }
                                        }
                                    }
                                }

                            case SliceTypes.Intra_8x8:
                                {
                                    _Core();
                                    break;

                                    void _Core()
                                    {
                                        Span<int> leftL = stackalloc int[16];
                                        Span<int> topL = stackalloc int[16];
                                        int topLeft;

                                        for (int i = 0; i < 16; i++)
                                        {
                                            if (mbX > 0)
                                            {
                                                leftL[i] = frame.Y[(mbX * 16) - 1, (mbY * 16) + i];
                                            }

                                            if (mbY > 0)
                                            {
                                                topL[i] = frame.Y[(mbX * 16) + i, (mbY * 16) - 1];
                                            }
                                        }

                                        topLeft = mbX > 0 && mbY > 0 ? frame.Y[(mbX * 16) - 1, (mbY * 16) - 1] : 0;

                                        Span<int> buf = stackalloc int[16 * 16];

                                        var p = new IntraPredictionSamples(buf, leftL, topL, topLeft);

                                        Span<int> intra4x4PredMode = stackalloc int[16];
                                        Span<int> intra8x8PredMode = stackalloc int[4];

                                        Span<int> remIntra8x8PredMode = stackalloc int[4];
                                        Span<bool> prevIntra8x8PredModeFlag = stackalloc bool[4];

                                        for (int i = 0; i < 4; i++)
                                        {
                                            remIntra8x8PredMode[i] = (int)mb.Prediction!.Value.RemIntra8x8PredMode[i];
                                            prevIntra8x8PredModeFlag[i] = mb.Prediction!.Value.PrevIntra8x8PredModeFlag[i];
                                        }

                                        for (int i = 0; i < 4; i++)
                                        {
                                            _intraInterDecoder.IntraPredictor.DeriveIntra8x8PredMode(
                                                Context,
                                                PPS.ConstrainedIntraPredFlag,
                                                intra4x4PredMode,
                                                intra8x8PredMode,
                                                remIntra8x8PredMode,
                                                prevIntra8x8PredModeFlag,
                                                i);
                                        }

                                        for (int i = 0; i < 4; i++)
                                        {
                                            _Core2(intra8x8PredMode, p);

                                            void _Core2(Span<int> intra8x8PredMode, IntraPredictionSamples p)
                                            {
                                                Span<int> predBacking = stackalloc int[8 * 8];
                                                Matrix8x8 predL = new(predBacking);

                                                _intraInterDecoder.IntraPredictor.Intra8x8SamplePredict(
                                                    i,
                                                    frame.Y,
                                                    ref predL,
                                                    PPS.ConstrainedIntraPredFlag,
                                                    intra8x8PredMode,
                                                    p,
                                                    Context);

                                                for (int x = 0; x < 8; x++)
                                                {
                                                    for (int y = 0; y < 8; y++)
                                                    {
                                                        int pixX = (mbX * 16) + ((i % 2) * 8) + x;
                                                        int pixY = (mbY * 16) + ((i / 2) * 8) + y;

                                                        frame.Y[pixX, pixY] = predL[x, y];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            case SliceTypes.Intra_4x4:
                                {
                                    _Core();
                                    break;

                                    void _Core()
                                    {
                                        Span<int> leftL = stackalloc int[16];
                                        Span<int> topL = stackalloc int[16];
                                        int topLeft;

                                        for (int i = 0; i < 16; i++)
                                        {
                                            if (mbX > 0)
                                            {
                                                leftL[i] = frame.Y[(mbX * 16) - 1, (mbY * 16) + i];
                                            }

                                            if (mbY > 0)
                                            {
                                                topL[i] = frame.Y[(mbX * 16) + i, (mbY * 16) - 1];
                                            }
                                        }

                                        topLeft = mbX > 0 && mbY > 0 ? frame.Y[(mbX * 16) - 1, (mbY * 16) - 1] : 0;

                                        Span<int> buf = stackalloc int[4 * 4];

                                        var p = new IntraPredictionSamples(buf, leftL, topL, topLeft);

                                        Span<int> remIntra4x4PredMode = stackalloc int[4];
                                        Span<bool> prevIntra4x4PredModeFlag = stackalloc bool[4];

                                        for (int i = 0; i < 16; i++)
                                        {
                                            remIntra4x4PredMode[i] = (int)mb.Prediction!.Value.RemIntra4x4PredMode[i];
                                            prevIntra4x4PredModeFlag[i] = mb.Prediction!.Value.PrevIntra4x4PredModeFlag[i];
                                        }

                                        Span<int> intra4x4PredMode = stackalloc int[16];
                                        Span<int> intra8x8PredMode = stackalloc int[4];

                                        for (int i = 0; i < 16; i++)
                                        {
                                            _Core2(intra4x4PredMode, intra8x8PredMode, remIntra4x4PredMode, prevIntra4x4PredModeFlag, p);

                                            void _Core2(Span<int> intra4x4PredMode, Span<int> intra8x8PredMode, Span<int> remIntra4x4PredMode, Span<bool> prevIntra4x4PredModeFlag, IntraPredictionSamples p)
                                            {
                                                Span<int> predBacking = stackalloc int[4 * 4];
                                                Matrix4x4 predL = new(predBacking);

                                                IntraInterDecoder.Intra.DeriveIntra4x4PredMode(
                                                    i,
                                                    Context,
                                                    PPS.ConstrainedIntraPredFlag,
                                                    intra4x4PredMode,
                                                    intra8x8PredMode,
                                                    remIntra4x4PredMode,
                                                    prevIntra4x4PredModeFlag);

                                                for (int x = 0; x < 4; x++)
                                                {
                                                    for (int y = 0; y < 4; y++)
                                                    {
                                                        int pixX = mbX * 16 + ((i % 4) * 4) + x;
                                                        int pixY = mbY * 16 + ((i / 4) * 4) + y;

                                                        frame.Y[pixX, pixY] = predL[x, y];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                        }

                        if (SliceTypes.IsIntra(Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, sliceType)) &&
                            SPS.GetChromaArrayType() != 0)
                        {
                            Span<int> leftCb = stackalloc int[16];
                            Span<int> topCb = stackalloc int[16];
                            int topLeftCb;
                            Span<int> leftCr = stackalloc int[16];
                            Span<int> topCr = stackalloc int[16];
                            int topLeftCr;

                            for (int i = 0; i < 16; i++)
                            {
                                if (mbX > 0)
                                {
                                    leftCb[i] = frame.U[(mbX * 16) - 1, (mbY * 16) + i];
                                    leftCr[i] = frame.V[(mbX * 16) - 1, (mbY * 16) + i];
                                }

                                if (mbY > 0)
                                {
                                    topCb[i] = frame.U[(mbX * 16) + i, (mbY * 16) - 1];
                                    topCr[i] = frame.V[(mbX * 16) + i, (mbY * 16) - 1];
                                }
                            }

                            topLeftCb = mbX > 0 && mbY > 0 ? frame.U[(mbX * 16) - 1, (mbY * 16) - 1] : 0;
                            topLeftCr = mbX > 0 && mbY > 0 ? frame.V[(mbX * 16) - 1, (mbY * 16) - 1] : 0;

                            Span<int> bufCb = stackalloc int[16 * 16];
                            Span<int> bufCr = stackalloc int[16 * 16];

                            var pCb = new IntraPredictionSamples(bufCb, leftCb, topCb, topLeftCb);
                            var pCr = new IntraPredictionSamples(bufCr, leftCr, topCr, topLeftCr);

                            Span<int> predCbBuf = stackalloc int[16 * 16];
                            Span<int> predCrBuf = stackalloc int[16 * 16];
                            Matrix16x16 predCb = new(predCbBuf);
                            Matrix16x16 predCr = new(predCrBuf);

                            _intraInterDecoder.IntraPredictor.IntraChromaSamplePredict(
                                frame.U,
                                ref predCb,
                                MacroblockSizeChroma.From(SPS.ChromaFormatIdc),
                                pCb,
                                Context,
                                PPS.ConstrainedIntraPredFlag,
                                (int)mb.Prediction!.Value.IntraChromaPredMode,
                                (int)SPS.GetChromaArrayType()
                            );

                            _intraInterDecoder.IntraPredictor.IntraChromaSamplePredict(
                                frame.V,
                                ref predCr,
                                MacroblockSizeChroma.From(SPS.ChromaFormatIdc),
                                pCr,
                                Context,
                                PPS.ConstrainedIntraPredFlag,
                                (int)mb.Prediction!.Value.IntraChromaPredMode,
                                (int)SPS.GetChromaArrayType()
                            );
                        }

                        break;
                    }

                case GeneralSliceType.P:
                case GeneralSliceType.B:
                    {
                        _intraInterDecoder.InterPredictor.CurrMbAddr = mbAddr;
                        _intraInterDecoder.InterPredictor.DerivationContext = Context;
                        _intraInterDecoder.InterPredictor.MbFieldDecodingFlag = _mbFieldDecodingFlags[mbAddr];
                        _intraInterDecoder.InterPredictor.NalUnit = NAL;
                        _intraInterDecoder.InterPredictor.PictureParameterSet = PPS;
                        _intraInterDecoder.InterPredictor.SequenceParameterSet = SPS;
                        _intraInterDecoder.InterPredictor.SliceHeader = SliceHeader;
                        _intraInterDecoder.InterPredictor.Decode(
                            false,
                            mbX,
                            mbY,
                            MacroblockSizeChroma.From(SPS.ChromaFormatIdc),
                            mb,
                            SPS.GetChromaFormat(),
                            frame.Y,
                            frame.U,
                            frame.V);
                        break;
                    }
            }
        }
    }

    private void CreateMapUnitToSliceGroupMaps()
    {
        int PicSizeInMapUnits = SPS.GetPicSizeInMapUnits();
        int PicWidthInMbs = (int)(SPS.PicWidthInMbsMinus1 + 1);

        if (PPS.NumSliceGroupsMinus1 == 0)
        {
            for (int i = 0; i < PicSizeInMapUnits - 1; i++)
                _mapUnitToSliceGroupMap[i] = 0;
        }
        else
        {
            uint sliceGroupMapType = PPS.SliceGroupMapType;
            switch (sliceGroupMapType)
            {
                case 0:
                    {
                        int i = 0;
                        do
                        {
                            for (int iGroup = 0; iGroup <= PPS.NumSliceGroupsMinus1 && i < PicSizeInMapUnits; i += (int)PPS.RunLengthMinus1[iGroup++] + 1)
                            {
                                for (int j = 0; j <= PPS.RunLengthMinus1[iGroup] && i + j < PicSizeInMapUnits; j++)
                                {
                                    _mapUnitToSliceGroupMap[i + j] = iGroup;
                                }
                            }
                        }
                        while (i < PicSizeInMapUnits);

                        break;
                    }

                case 1:
                    {
                        for (int i = 0; i < PicSizeInMapUnits; i++)
                        {
                            _mapUnitToSliceGroupMap[i] = ((i % PicWidthInMbs) +
                                                              (((i / PicWidthInMbs) * ((int)PPS.NumSliceGroupsMinus1 + 1)) / 2))
                                                            % ((int)PPS.NumSliceGroupsMinus1 + 1);
                        }

                        break;
                    }

                case 2:
                    {
                        for (int i = 0; i < PicSizeInMapUnits; i++)
                            _mapUnitToSliceGroupMap[i] = (int)PPS.NumSliceGroupsMinus1;

                        for (int iGroup = (int)PPS.NumSliceGroupsMinus1 - 1; iGroup >= 0; iGroup--)
                        {
                            int yTopLeft = (int)PPS.GetTopLeftFast(iGroup) / PicWidthInMbs;
                            int xTopLeft = (int)PPS.GetTopLeftFast(iGroup) % PicWidthInMbs;
                            int yBottomRight = (int)PPS.GetBottomRightFast(iGroup) / PicWidthInMbs;
                            int xBottomRight = (int)PPS.GetBottomRightFast(iGroup) % PicWidthInMbs;
                            for (int y = yTopLeft; y <= yBottomRight; y++)
                            {
                                for (int x = xTopLeft; x <= xBottomRight; x++)
                                {
                                    _mapUnitToSliceGroupMap[y * PicWidthInMbs + x] = iGroup;
                                }
                            }
                        }

                        break;
                    }

                case 3:
                    {
                        throw new NotImplementedException("Box-out slice group map types");
                        //for (int i = 0; i < PicSizeInMapUnits; i++)
                        //    _mapUnitToSliceGroupMap.Add(1);
                        //int x = (PicWidthInMbs - Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag)) / 2;
                        //int y = (PicHeightInMapUnits - Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag)) / 2;
                        //(int leftBound, int topBound) = (x, y);
                        //(int rightBound, int bottomBound) = (x, y);
                        //(int xDir, int yDir) = (Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag) - 1, Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag));
                        //bool mapUnitVacant;
                        //for (int k = 0; k < MapUnitsInSliceGroup0; k += Int32Boolean.I32(mapUnitVacant))
                        //{
                        //    mapUnitVacant = _mapUnitToSliceGroupMap[y * PicWidthInMbs + x] == 1;
                        //    if (mapUnitVacant)
                        //        _mapUnitToSliceGroupMap[y * PicWidthInMbs + x] = 0;
                        //    if (xDir == -1 && x == leftBound)
                        //    {
                        //        leftBound = Math.Max(leftBound - 1, 0);
                        //        x = leftBound;
                        //        (xDir, yDir) = (0, 2 * Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag) - 1);
                        //    }
                        //    else if (xDir == 1 && x == rightBound)
                        //    {
                        //        rightBound = Math.Min(rightBound + 1, PicWidthInMbs - 1);
                        //        x = rightBound;
                        //        (xDir, yDir) = (0, 1 - 2 * Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag));
                        //    }
                        //    else if (yDir == -1 && y == topBound)
                        //    {
                        //        topBound = Math.Max(topBound - 1, 0);
                        //        y = topBound;
                        //        (xDir, yDir) = (1 - 2 * Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag), 0);
                        //    }
                        //    else if (yDir == 1 && y == bottomBound)
                        //    {
                        //        bottomBound = Math.Min(bottomBound + 1, PicHeightInMapUnits - 1);
                        //        y = bottomBound;
                        //        (xDir, yDir) = (2 * Int32Boolean.I32(PPS.SliceGroupChangeDirectionFlag) - 1, 0);
                        //    }
                        //    else
                        //        (x, y) = (x + xDir, y + yDir);
                        //}
                    }

                case 4:
                    {
                        throw new NotImplementedException("Raster scan slice group map types");
                    }

                case 5:
                    {
                        throw new NotImplementedException("Wipe slice group map types");
                    }

                case 6:
                    {
                        throw new NotImplementedException("Explicit slice group map types");
                    }
            }
        }
    }

    private MinimalMacroblockLayer ParseMacroblockLayer(bool mbFieldDecodingFlag)
    {
        var subSize = ChromaFormat.GetSubsamplingAndSize(SPS);
        return MinimalMacroblockLayer.From(
            MacroblockLayer.Read(
                _connectedBsReader,
                _cabac,
                PPS.Transform8x8ModeFlag,
                PPS.EntropyCodingModeFlag ? EntropyCodingMode.Cabac : EntropyCodingMode.Cavlc,
                SPS.BitDepthLumaMinus8,
                SPS.BitDepthChromaMinus8,
                default,
                (int)SPS.GetChromaArrayType(),
                SliceHeader.GetSliceType(),
                (int)PPS.NumRefIdxL0DefaultActiveMinus1,
                (int)PPS.NumRefIdxL1DefaultActiveMinus1,
                mbFieldDecodingFlag,
                SliceHeader.GetMbaffFrameFlag(SPS),
                SliceHeader.FieldPicFlag,
                SPS.Direct8X8InferenceFlag,
                NAL,
                Context,
                _util,
                default,
                PPS.ConstrainedIntraPredFlag,
                subSize.ChromaWidth,
                subSize.ChromaHeight));
    }

    internal sealed class MbUtilImpl : IMacroblockUtility
    {
        private readonly H264Slice _self;

        public MbUtilImpl(H264Slice self) => _self = self;

        public bool AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(int address)
        {
            MinimalMacroblockLayer mb = GetMacroblock(address);
            MinimalResidual res = mb.Intra16x16Residual ?? throw new InvalidOperationException("No residual");

            bool lumaZero = AllZero(res.Intra16x16ACLevel);

            if (_self.SPS.GetChromaArrayType() == 0)
                return lumaZero;

            return lumaZero &&
                AllZero(res.Cb16x16ACLevel) &&
                AllZero(res.Cr16x16ACLevel) &&
                mb.CodedBlockPattern == 0;
            
            static bool AllZero(ContainerMatrix16x16Byte b)
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        if (b[x, y] != 0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public void GetIntra8x8PredMode(int mbAddr, Span<int> output)
        {
            throw new NotImplementedException();
        }

        public MinimalMacroblockLayer GetMacroblock(int address)
        {
            if (address < 0 || address > ((_self.SPS.PicWidthInMbsMinus1 + 1) * (_self.SPS.PicHeightInMapUnitsMinus1 + 1)))
                throw new InvalidOperationException("Attempting to access macroblock out of range. Total MBs: " + ((_self.SPS.PicWidthInMbsMinus1 + 1) * (_self.SPS.PicHeightInMapUnitsMinus1 + 1)) + ", MB: " + address + ", discovered: " + _self._mbs.Count);

            return _self._mbs![address]; // Does not discover macroblocks ahead
        }

        public MinimalMacroblockLayer? GetMacroblockToTheBottom(int address)
        {
            if ((address + _self.SPS.PicWidthInMbsMinus1 + 1) > ((_self.SPS.PicWidthInMbsMinus1 + 1) * (_self.SPS.PicHeightInMapUnitsMinus1 + 1)))
                return null;

            return _self._mbs![address + (int)_self.SPS.PicWidthInMbsMinus1 + 1];
        }

        public MinimalMacroblockLayer? GetMacroblockToTheLeft(int address)
        {
            if ((address - 1) < 0)
                return null;

            return _self._mbs![address + (int)_self.SPS.PicWidthInMbsMinus1 + 1];
        }

        public MinimalMacroblockLayer? GetMacroblockToTheRight(int address)
        {
            if ((address + 1) > ((_self.SPS.PicWidthInMbsMinus1 + 1) * (_self.SPS.PicHeightInMapUnitsMinus1 + 1)))
                return null;

            return _self._mbs![address + (int)_self.SPS.PicWidthInMbsMinus1 + 1];
        }

        public MinimalMacroblockLayer? GetMacroblockToTheTop(int address)
        {
            if ((address - (_self.SPS.PicWidthInMbsMinus1 + 1)) < 0)
                return null;

            return _self._mbs![address + (int)_self.SPS.PicWidthInMbsMinus1 + 1];
        }

        public uint GetMbType(int mbAddr)
        {
            return GetMacroblock(mbAddr).MbType;
        }

        public uint GetSubMbType(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCoefficient(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool IsCodedWithInter(int mbAddr)
        {
            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Inter;
        }

        public bool IsCodedWithIntra(int mbAddr)
        {
            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_16x16 or SliceTypes.Intra_8x8 or SliceTypes.Intra_4x4;
        }

        public bool IsCodedWithIntra16x16(int mbAddr)
        {
            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_16x16;
        }

        public bool IsCodedWithIntra4x4(int mbAddr)
        {
            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_4x4;
        }

        public bool IsCodedWithIntra8x8(int mbAddr)
        {
            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_8x8;
        }

        public bool IsFieldMacroblock(int mbAddr)
        {
            return _self._mbFieldDecodingFlags[mbAddr] && _self.SPS.MbAdaptiveFrameFieldFlag;
        }

        public bool IsFrameMacroblock(int mbAddr)
        {
            return !_self._mbFieldDecodingFlags[mbAddr] && _self.SPS.MbAdaptiveFrameFieldFlag;
        }

        public bool IsMacroblockOfTypeSi(int mbAddr)
        {
            return _self.SliceHeader.GetSliceType() is GeneralSliceType.SI;
        }

        public bool IsMbSkipFlagForMacroblock(int mbAddr)
        {
            return _self._mbSkipFlags[mbAddr];
        }
    }
}
