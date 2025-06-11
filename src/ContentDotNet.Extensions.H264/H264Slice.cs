using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Cabac;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents the H.264 slice, with macroblocks.
/// </summary>
public sealed class H264Slice
{
    // Fields like _mbs and _mapUnitToSliceGroupMap are likely to be allocated on the LOH.
    // Thus, setting them to null will hint the GC to collect it.
    // We care about memory usage, significantly in fact.

    private readonly List<int>? _mapUnitToSliceGroupMap;
    internal readonly List<MacroblockLayer>? _mbs;
    private readonly CabacManager? _cabac;
    private readonly int _initialCurrMbAddr;

    internal readonly SequenceParameterSet SPS;
    internal readonly PictureParameterSet PPS;
    internal readonly SliceHeader SliceHeader;

    internal DerivationContext Context = default;
    internal NalUnit NAL;

    private readonly IMacroblockUtility _util;

    private readonly long _nalLen;

    private readonly BitStreamReader _connectedBsReader;

    internal H264Slice(SequenceParameterSet sps, PictureParameterSet pps, SliceHeader shd, BitStreamReader reader, NalUnit nal, long nalLength)
    {
        _nalLen = nalLength;
        NAL = nal;

        _mapUnitToSliceGroupMap = [];
        _mbs = [];
        _initialCurrMbAddr = (int)shd.FirstMbInSlice * (2 - Int32Boolean.I32(shd.GetMbaffFrameFlag(sps)));

        SPS = sps;
        PPS = pps;
        SliceHeader = shd;

        _util = new MbUtilImpl(this);

        _connectedBsReader = reader;
        if (pps.EntropyCodingModeFlag)
        {
            _cabac = new CabacManager(reader, _util)
            {
                CabacInitIdc = (int)shd.CabacInitIdc,
                ChromaArrayType = (int)sps.GetChromaArrayType(),
                DerivationContext = new DerivationContext(0, sps.MbAdaptiveFrameFieldFlag, false, default, new NeighboringMacroblocks(), _initialCurrMbAddr, 0, 0, sps.GetPicWidthInSamplesL(), false, (int)(sps.BitDepthLumaMinus8 + 8), (int)(sps.BitDepthChromaMinus8 + 8)),
                L0Mode = true,
                PicWidthInMbs = (int)(sps.PicWidthInMbsMinus1 + 1),
                SliceType = shd.GetSliceType()
            };
        }

        Parse();
    }

    private void Parse()
    {
        bool moreDataFlag = true;
        bool prevMbSkipped = false;
        int CurrMbAddr = _initialCurrMbAddr;
        bool mbFieldDecodingFlag;
        do
        {
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
                        CurrMbAddr++; // NextMbAddress ----------------------------------------------
                        if (_cabac is not null)
                        {
                            var dc = _cabac.DerivationContext;
                            dc.CurrMbAddr = CurrMbAddr;
                            _cabac!.DerivationContext = dc;

                            this.Context.CurrMbAddr = CurrMbAddr;
                        }
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
                _mbs!.Add(ParseMacroblockLayer(mbFieldDecodingFlag));
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
            CurrMbAddr++; // NextMbAddress ----------------------------------------------------------
            if (_cabac is not null)
            {
                var dc = _cabac.DerivationContext;
                dc.CurrMbAddr = CurrMbAddr;
                _cabac!.DerivationContext = dc;

                this.Context.CurrMbAddr = CurrMbAddr;
            }
        }
        while (moreDataFlag);
    }

    private MacroblockLayer ParseMacroblockLayer(bool mbFieldDecodingFlag)
    {
        var subSize = ChromaFormat.GetSubsamplingAndSize(SPS);
        return MacroblockLayer.Read(
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
            subSize.ChromaHeight);
    }

    internal sealed class MbUtilImpl : IMacroblockUtility
    {
        private readonly H264Slice _self;

        public MbUtilImpl(H264Slice self) => _self = self;

        public bool AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(int address)
        {
            MacroblockLayer mb = GetMacroblock(address);
            if (mb.CodedBlockPattern == 0 &&
                mb.Intra16x16Residual is not null &&
                IsAllZero(mb.Intra16x16Residual!.Value.Intra16x16ACLevel) &&
                IsAllZero(mb.Intra16x16Residual!.Value.Cb16x16ACLevel) &&
                IsAllZero(mb.Intra16x16Residual!.Value.Cr16x16ACLevel))
                return true;

            return false;

            static bool IsAllZero(ContainerMatrix16x16 container)
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        if (container[x, y] != 0)
                            return false;
                    }
                }

                return true;
            }
        }

        public void Get16x16LumaBlock(int luma16x16BlkIdx, ContainerMatrix16x16 output)
        {
            throw new NotImplementedException();
        }

        public void Get4x4LumaBlock(int luma4x4BlkIdx, ContainerMatrix4x4 output)
        {
            throw new NotImplementedException();
        }

        public void Get8x8LumaBlock(int luma8x8BlkIdx, ContainerMatrix8x8 output)
        {
            throw new NotImplementedException();
        }

        public void GetIntra16x16PredMode(int mbAddr, Span<int> output)
        {
            var mb = GetMacroblock(mbAddr);
            output[0] = H264Decoder.DeriveIntra16x16PredictionMode((int)mb.MbType);
        }

        public void GetIntra4x4PredMode(int mbAddr, Span<int> output)
        {
            var mb = GetMacroblock(mbAddr);
            var pred = mb.Prediction ?? throw new InvalidOperationException("No macroblock prediction");

            Span<int> remIntra4x4PredMode = stackalloc int[16];
            Span<int> remIntra8x8PredMode = stackalloc int[16];
            Span<bool> prevIntra4x4PredModeFlag = stackalloc bool[16];
            Span<bool> prevIntra8x8PredModeFlag = stackalloc bool[16];

            for (int i = 0; i < 16; i++)
            {
                remIntra4x4PredMode[i] = (int)pred.RemIntra4x4PredMode[i];
                remIntra8x8PredMode[i] = (int)pred.RemIntra8x8PredMode[i];
                prevIntra4x4PredModeFlag[i] = pred.PrevIntra4x4PredModeFlag[i];
                prevIntra8x8PredModeFlag[i] = pred.PrevIntra8x8PredModeFlag[i];
            }

            Span<int> intra4x4PredMode = stackalloc int[16];
            Span<int> intra8x8PredMode = stackalloc int[16];

            for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
            {
                IntraInterDecoder.Intra.DeriveIntra4x4PredMode(
                    luma4x4BlkIdx,
                    new DerivationContext(0, false, false, default, default, mbAddr, (int)mb.MbType, 0, 0, false, 0, 0),
                    false,
                    intra4x4PredMode,
                    intra8x8PredMode,
                    remIntra4x4PredMode,
                    prevIntra4x4PredModeFlag
                );
            }

            for (int i = 0; i < 16; i++)
                output[i] = intra4x4PredMode[i];
        }

        public void GetIntra8x8PredMode(int mbAddr, Span<int> output)
        {
            throw new NotImplementedException();
        }

        public MacroblockLayer GetMacroblock(int address)
        {
            if (_self._mbs is null)
                throw new InvalidOperationException("Cannot access mbs");
            return _self._mbs[address];
        }

        public MacroblockLayer? GetMacroblockToTheBottom(int address)
        {
            return GetMacroblock(
                new AddressFlow(
                    address,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Down()
                .Value
            );
        }

        public MacroblockLayer? GetMacroblockToTheLeft(int address)
        {
            return GetMacroblock(
                new AddressFlow(
                    address,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Left()
                .Value
            );
        }

        public MacroblockLayer? GetMacroblockToTheRight(int address)
        {
            return GetMacroblock(
                new AddressFlow(
                    address,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Right()
                .Value
            );
        }

        public MacroblockLayer? GetMacroblockToTheTop(int address)
        {
            return GetMacroblock(
                new AddressFlow(
                    address,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Up()
                .Value
            );
        }

        public uint GetMbType(int mbAddr)
        {
            return GetMacroblock(mbAddr).MbType;
        }

        public ContainerMatrix16x16 GetPixels(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public ContainerMatrix16x16? GetPixelsToBottom(int mbAddr)
        {
            return GetPixels(
                new AddressFlow(
                    mbAddr,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Down()
                .Value
            );
        }

        public ContainerMatrix16x16? GetPixelsToLeft(int mbAddr)
        {
            return GetPixels(
                new AddressFlow(
                    mbAddr,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Left()
                .Value
            );
        }

        public ContainerMatrix16x16? GetPixelsToRight(int mbAddr)
        {
            return GetPixels(
                new AddressFlow(
                    mbAddr,
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                    (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                )
                .Right()
                .Value
            );
        }

        public ContainerMatrix16x16? GetPixelsToTop(int mbAddr)
        {
            return GetPixels(
                 new AddressFlow(
                     mbAddr,
                     (int)(_self.SPS.PicWidthInMbsMinus1 + 1),
                     (int)(_self.SPS.PicWidthInMbsMinus1 + 1) * (int)(_self.SPS.PicHeightInMapUnitsMinus1 + 1)
                 )
                 .Up()
                 .Value
             );
        }

        public uint GetSubMbType(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCoefficient(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool HasPixels(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool HasPixelsToBottom(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool HasPixelsToLeft(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool HasPixelsToRight(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool HasPixelsToTop(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool IsCodedWithInter(int mbAddr)
        {
            return _self.SliceHeader.GetSliceType() is GeneralSliceType.P or GeneralSliceType.B;
        }

        public bool IsCodedWithIntra(int mbAddr)
        {
            return _self.SliceHeader.GetSliceType() is GeneralSliceType.I;
        }

        public bool IsCodedWithIntra16x16(int mbAddr)
        {
            if (!IsCodedWithIntra(mbAddr))
                return false;

            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_16x16;
        }

        public bool IsCodedWithIntra4x4(int mbAddr)
        {
            if (!IsCodedWithIntra(mbAddr))
                return false;

            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_4x4;
        }

        public bool IsCodedWithIntra8x8(int mbAddr)
        {
            if (!IsCodedWithIntra(mbAddr))
                return false;

            var mb = GetMacroblock(mbAddr);
            return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _self.SliceHeader.GetSliceType()) is SliceTypes.Intra_8x8;
        }

        public bool IsFieldMacroblock(int mbAddr)
        {
            return _self.SliceHeader.FieldPicFlag;
        }

        public bool IsFrameMacroblock(int mbAddr)
        {
            return !IsFieldMacroblock(mbAddr);
        }

        public bool IsMacroblockOfTypeSi(int mbAddr)
        {
            return _self.SliceHeader.GetSliceType() == GeneralSliceType.SI;
        }

        public bool IsMbSkipFlagForMacroblock(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public void SetPixels(int mbAddr, ContainerMatrix16x16 pixels)
        {
            throw new NotImplementedException();
        }
    }
}
