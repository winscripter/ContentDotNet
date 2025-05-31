using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.PredictionMode;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed class H264Decoder
{
    private static ReadOnlySpan<int> Intra16x16_0 =>
    [
        SliceTypes.I_16x16_0_0_0,
        SliceTypes.I_16x16_0_0_1,
        SliceTypes.I_16x16_0_1_0,
        SliceTypes.I_16x16_0_1_1,
        SliceTypes.I_16x16_0_2_0,
        SliceTypes.I_16x16_0_2_1,
    ];

    private static ReadOnlySpan<int> Intra16x16_1 =>
    [
        SliceTypes.I_16x16_1_0_0,
        SliceTypes.I_16x16_1_0_1,
        SliceTypes.I_16x16_1_1_0,
        SliceTypes.I_16x16_1_1_1,
        SliceTypes.I_16x16_1_2_0,
        SliceTypes.I_16x16_1_2_1,
    ];

    private static ReadOnlySpan<int> Intra16x16_2 =>
    [
        SliceTypes.I_16x16_2_0_0,
        SliceTypes.I_16x16_2_0_1,
        SliceTypes.I_16x16_2_1_0,
        SliceTypes.I_16x16_2_1_1,
        SliceTypes.I_16x16_2_2_0,
        SliceTypes.I_16x16_2_2_1,
    ];

    private static ReadOnlySpan<int> Intra16x16_3 =>
    [
        SliceTypes.I_16x16_3_0_0,
        SliceTypes.I_16x16_3_0_1,
        SliceTypes.I_16x16_3_1_0,
        SliceTypes.I_16x16_3_1_1,
        SliceTypes.I_16x16_3_2_0,
        SliceTypes.I_16x16_3_2_1,
    ];

    public static (ContainerMatrix16x16 luma, ContainerMatrix16x16 cb, ContainerMatrix16x16 cr) Decode16x16Macroblock(
        MacroblockLayer mbLayer,
        IntraInterDecoder intraInter,
        GeneralSliceType sliceType,
        IMacroblockUtility util,
        int currMbAddr,
        int picWidthInMbs,
        int mbsInFrame,
        bool constrainedIntraPredFlag,
        DerivationContext dc,
        MacroblockSizeChroma sizes,
        int totalMbsInFrame,
        int chromaArrayType,
        /*output*/ Matrix cSL,
        /*output*/ Matrix cSCb,
        /*output*/ Matrix cSCr)
    {
        var matrixL = new ContainerMatrix16x16();
        var matrixCb = new ContainerMatrix16x16();
        var matrixCr = new ContainerMatrix16x16();

        if (mbLayer.MbType == SliceTypes.I_PCM)
        {
            // PCM macroblocks. This is dead easy - your pixels
            // are provided right in front of you.

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j += 2)
                {
                    matrixCb[i, j] = mbLayer.PcmChroma[i * 8 + j];
                    matrixCr[i, j] = mbLayer.PcmChroma[i * 8 + j + 1];
                }
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    matrixL[i, j] = mbLayer.PcmLuma[i * 8 + j];
                }
            }
        }
        else
        {
            int mbPartPredMode = Util264.MbPartPredMode((int)mbLayer.MbType, 0, mbLayer.TransformSize8x8Flag, sliceType);
            switch (mbPartPredMode)
            {
                case SliceTypes.Intra_16x16:
                    {
                        // Intra_16x16 macroblocks are guaranteed to have macroblock predictions,
                        // and, in the case of CBP's, residuals. Though residuals aren't yet supported.

                        var mbPred = mbLayer.Prediction!.Value;

                        Span<int> predBufferBackingL = stackalloc int[16 * 16];
                        var predBufferL = new Matrix16x16(predBufferBackingL);
                        Span<int> predBufferBackingCb = stackalloc int[16 * 16];
                        var predBufferCb = new Matrix16x16(predBufferBackingCb);
                        Span<int> predBufferBackingCr = stackalloc int[16 * 16];
                        var predBufferCr = new Matrix16x16(predBufferBackingCr);

                        Span<int> leftL = stackalloc int[16];
                        Span<int> topL = stackalloc int[16];
                        Span<int> leftCb = stackalloc int[16];
                        Span<int> topCb = stackalloc int[16];
                        Span<int> leftCr = stackalloc int[16];
                        Span<int> topCr = stackalloc int[16];

                        FetchSamples(leftL, topL, leftCb, topCb, leftCr, topCr, out int leftTop);

                        Span<int> pL = stackalloc int[16 * 16];
                        var pLSamples = new IntraPredictionSamples(pL, leftL, topL, YuvBinary.GetY(leftTop));
                        Span<int> pCb = stackalloc int[16 * 16];
                        var pCbSamples = new IntraPredictionSamples(pCb, leftCb, topCb, YuvBinary.GetCb(leftTop));
                        Span<int> pCr = stackalloc int[16 * 16];
                        var pCrSamples = new IntraPredictionSamples(pCr, leftCr, topCr, YuvBinary.GetCr(leftTop));

                        int intra16x16PredMode = DeriveIntra16x16PredictionMode((int)mbLayer.MbType);
                        intraInter.IntraPredictor.Intra16x16SamplePredict(cSL, ref predBufferL, constrainedIntraPredFlag, intra16x16PredMode, pLSamples, dc);
                        intraInter.IntraPredictor.IntraChromaSamplePredict(cSCb, ref predBufferCb, sizes, pCbSamples, dc, constrainedIntraPredFlag, (int)mbPred.IntraChromaPredMode, chromaArrayType);
                        intraInter.IntraPredictor.IntraChromaSamplePredict(cSCr, ref predBufferCr, sizes, pCrSamples, dc, constrainedIntraPredFlag, (int)mbPred.IntraChromaPredMode, chromaArrayType);

                        for (int x = 0; x < 16; x++)
                        {
                            for (int y = 0; y < 16; y++)
                            {
                                matrixL[x, y] = (uint)predBufferL[x, y];
                                matrixCb[x, y] = (uint)predBufferCb[x, y];
                                matrixCr[x, y] = (uint)predBufferCr[x, y];
                            }
                        }

                        break;
                    }

                case SliceTypes.Intra_8x8:
                    {
                        // This is a 16x16 macroblock that consists of 4 8x8 blocks.

                        if (mbLayer.SubMacroblockPrediction is not SubMacroblockPrediction subMbPred)
                            throw new InvalidOperationException("Sub-macroblock prediction isn't present");

                        if (mbLayer.Prediction is not MacroblockPrediction mbPred)
                            throw new InvalidOperationException("Macroblock prediction isn't present");

                        Span<int> predBufferBackingL = stackalloc int[16 * 16];
                        var predBufferL = new Matrix16x16(predBufferBackingL);
                        Span<int> predBufferBackingCb = stackalloc int[16 * 16];
                        var predBufferCb = new Matrix16x16(predBufferBackingCb);
                        Span<int> predBufferBackingCr = stackalloc int[16 * 16];
                        var predBufferCr = new Matrix16x16(predBufferBackingCr);

                        Span<int> leftL = stackalloc int[16];
                        Span<int> topL = stackalloc int[16];
                        Span<int> leftCb = stackalloc int[16];
                        Span<int> topCb = stackalloc int[16];
                        Span<int> leftCr = stackalloc int[16];
                        Span<int> topCr = stackalloc int[16];

                        FetchSamples(leftL, topL, leftCb, topCb, leftCr, topCr, out int leftTop);

                        Span<int> pL = stackalloc int[16 * 16];
                        var pLSamples = new IntraPredictionSamples(pL, leftL, topL, YuvBinary.GetY(leftTop));
                        Span<int> pCb = stackalloc int[16 * 16];
                        var pCbSamples = new IntraPredictionSamples(pCb, leftCb, topCb, YuvBinary.GetCb(leftTop));
                        Span<int> pCr = stackalloc int[16 * 16];
                        var pCrSamples = new IntraPredictionSamples(pCr, leftCr, topCr, YuvBinary.GetCr(leftTop));

                        int intra16x16PredMode = DeriveIntra16x16PredictionMode((int)mbLayer.MbType);

                        Span<int> intra8x8PredMode = stackalloc int[4];

                        for (int i = 0; i < 4; i++)
                        {
                            intra8x8PredMode[i] = H264IntraDecisionFlow.Get8x8MacroblockType(
                                mbLayer,
                                sliceType,
                                picWidthInMbs,
                                totalMbsInFrame,
                                currMbAddr,
                                i,
                                util);
                        }

                        for (int x = 0; x < 2; x++)
                        {
                            for (int y = 0; y < 2; y++)
                            {
                                int luma8x8BlkIdx = x * 2 + y;

                                _Core(intra8x8PredMode, pLSamples, pCbSamples, pCrSamples);

                                void _Core(Span<int> intra8x8PredMode, IntraPredictionSamples pL, IntraPredictionSamples pCb, IntraPredictionSamples pCr)
                                {
                                    Span<int> predLBacking = stackalloc int[8 * 8];
                                    var predL = new Matrix8x8(predLBacking);

                                    Span<int> predCbBacking = stackalloc int[16 * 16];
                                    var predCb = new Matrix16x16(predCbBacking);
                                    Span<int> predCrBacking = stackalloc int[16 * 16];
                                    var predCr = new Matrix16x16(predCrBacking);

                                    intraInter.IntraPredictor.Intra8x8SamplePredict(
                                        luma8x8BlkIdx,
                                        cSL,
                                        ref predL,
                                        constrainedIntraPredFlag,
                                        intra8x8PredMode,
                                        pL,
                                        dc
                                    );

                                    intraInter.IntraPredictor.IntraChromaSamplePredict(
                                        cSCb,
                                        ref predCb,
                                        sizes,
                                        pCb,
                                        dc,
                                        constrainedIntraPredFlag,
                                        (int)mbPred.IntraChromaPredMode,
                                        chromaArrayType
                                    );

                                    intraInter.IntraPredictor.IntraChromaSamplePredict(
                                        cSCr,
                                        ref predCr,
                                        sizes,
                                        pCr,
                                        dc,
                                        constrainedIntraPredFlag,
                                        (int)mbPred.IntraChromaPredMode,
                                        chromaArrayType
                                    );
                                }
                            }
                        }

                        break;
                    }

                case SliceTypes.Intra_4x4:
                    {
                        // This is a 16x16 macroblock that consists of 16 4x4 blocks.

                        if (mbLayer.SubMacroblockPrediction is not SubMacroblockPrediction subMbPred)
                            throw new InvalidOperationException("Sub-macroblock prediction isn't present");

                        if (mbLayer.Prediction is not MacroblockPrediction mbPred)
                            throw new InvalidOperationException("Macroblock prediction isn't present");

                        Span<int> predBufferBackingL = stackalloc int[16 * 16];
                        var predBufferL = new Matrix16x16(predBufferBackingL);
                        Span<int> predBufferBackingCb = stackalloc int[16 * 16];
                        var predBufferCb = new Matrix16x16(predBufferBackingCb);
                        Span<int> predBufferBackingCr = stackalloc int[16 * 16];
                        var predBufferCr = new Matrix16x16(predBufferBackingCr);

                        Span<int> leftL = stackalloc int[16];
                        Span<int> topL = stackalloc int[16];
                        Span<int> leftCb = stackalloc int[16];
                        Span<int> topCb = stackalloc int[16];
                        Span<int> leftCr = stackalloc int[16];
                        Span<int> topCr = stackalloc int[16];

                        FetchSamples(leftL, topL, leftCb, topCb, leftCr, topCr, out int leftTop);

                        Span<int> pL = stackalloc int[16 * 16];
                        var pLSamples = new IntraPredictionSamples(pL, leftL, topL, YuvBinary.GetY(leftTop));
                        Span<int> pCb = stackalloc int[16 * 16];
                        var pCbSamples = new IntraPredictionSamples(pCb, leftCb, topCb, YuvBinary.GetCb(leftTop));
                        Span<int> pCr = stackalloc int[16 * 16];
                        var pCrSamples = new IntraPredictionSamples(pCr, leftCr, topCr, YuvBinary.GetCr(leftTop));

                        int intra16x16PredMode = DeriveIntra16x16PredictionMode((int)mbLayer.MbType);

                        Span<int> intra8x8PredMode = stackalloc int[4];

                        for (int i = 0; i < 4; i++)
                        {
                            intra8x8PredMode[i] = H264IntraDecisionFlow.Get8x8MacroblockType(
                                mbLayer,
                                sliceType,
                                picWidthInMbs,
                                totalMbsInFrame,
                                currMbAddr,
                                i,
                                util);
                        }

                        Span<int> remIntra4x4PredMode = stackalloc int[16];
                        Span<bool> prevIntra4x4PredModeFlag = stackalloc bool[16];

                        for (int i = 0; i < 16; i++)
                        {
                            remIntra4x4PredMode[i] = (int)mbPred.RemIntra4x4PredMode[i];
                            prevIntra4x4PredModeFlag[i] = mbPred.PrevIntra4x4PredModeFlag[i];
                        }

                        Span<int> intra4x4PredMode = stackalloc int[16];

                        for (int i = 0; i < 16; i++)
                            IntraInterDecoder.Intra.DeriveIntra4x4PredMode(i, dc, constrainedIntraPredFlag, intra4x4PredMode, intra8x8PredMode, remIntra4x4PredMode, prevIntra4x4PredModeFlag);

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                int luma4x4BlkIdx = x * 4 + y;

                                _Core(intra4x4PredMode, pLSamples, pCbSamples, pCrSamples);

                                void _Core(Span<int> intra4x4PredMode, IntraPredictionSamples pL, IntraPredictionSamples pCb, IntraPredictionSamples pCr)
                                {
                                    Span<int> predLBacking = stackalloc int[4 * 4];
                                    var predL = new Matrix4x4(predLBacking);

                                    Span<int> predCbBacking = stackalloc int[16 * 16];
                                    var predCb = new Matrix16x16(predCbBacking);
                                    Span<int> predCrBacking = stackalloc int[16 * 16];
                                    var predCr = new Matrix16x16(predCrBacking);

                                    Span<int> availableForIntraPredLBackingLeft = stackalloc int[16];
                                    Span<int> availableForIntraPredLBackingTop = stackalloc int[16];
                                    Span<int> availableForIntraPredLBackingP = stackalloc int[16 * 16];
                                    availableForIntraPredLBackingLeft.Fill(1);
                                    availableForIntraPredLBackingTop.Fill(1);
                                    availableForIntraPredLBackingP.Fill(1);
                                    var pLAvailable = new IntraPredictionSamples(availableForIntraPredLBackingP, availableForIntraPredLBackingLeft, availableForIntraPredLBackingTop, 1);

                                    IntraInterDecoder.Intra.Intra4x4SamplePredict(
                                        dc,
                                        luma4x4BlkIdx,
                                        constrainedIntraPredFlag,
                                        pL,
                                        cSL,
                                        predL,
                                        pLAvailable,
                                        intra4x4PredMode
                                    );

                                    intraInter.IntraPredictor.IntraChromaSamplePredict(
                                        cSCb,
                                        ref predCb,
                                        sizes,
                                        pCb,
                                        dc,
                                        constrainedIntraPredFlag,
                                        (int)mbPred.IntraChromaPredMode,
                                        chromaArrayType
                                    );

                                    intraInter.IntraPredictor.IntraChromaSamplePredict(
                                        cSCr,
                                        ref predCr,
                                        sizes,
                                        pCr,
                                        dc,
                                        constrainedIntraPredFlag,
                                        (int)mbPred.IntraChromaPredMode,
                                        chromaArrayType
                                    );
                                }
                            }
                        }

                        break;
                    }
            }
        }

        return (matrixL, matrixCb, matrixCr);

        void FetchSamples(Span<int> leftL, Span<int> topL, Span<int> leftCb, Span<int> topCb, Span<int> leftCr, Span<int> topCr, out int leftTop)
        {
            ContainerMatrix16x16? pixelsToLeft = util.GetPixelsToLeft(currMbAddr);
            ContainerMatrix16x16? pixelsToTop = util.GetPixelsToTop(currMbAddr);

            if (pixelsToLeft is null)
                throw new VideoCodecDecoderException("Cannot decode Intra_16x16 macroblock without left pixels available.");
            if (pixelsToTop is null)
                throw new VideoCodecDecoderException("Cannot decode Intra_16x16 macroblock without top pixels available.");

            Span<int> left = stackalloc int[16];
            Span<int> top = stackalloc int[16];

            for (int i = 0; i < 16; i++)
            {
                left[i] = (int)pixelsToLeft.Value[15, i];
                top[i] = (int)pixelsToTop.Value[i, 15];
            }

            Span<int> p = stackalloc int[16 * 16];

            leftTop = (int)util.GetPixels(new AddressFlow(currMbAddr, picWidthInMbs, mbsInFrame).Up().Left().Value)[15, 15];

            var predSamples = new IntraPredictionSamples(p, left, top, leftTop);

            for (int i = 0; i < 16; i++)
            {
                var (y, cb, cr) = YuvBinary.Extract(left[i]);
                var extractedTop = YuvBinary.Extract(top[i]);

                leftL[i] = y;
                topL[i] = extractedTop.y;
                leftCb[i] = cb;
                topCb[i] = extractedTop.cb;
                leftCr[i] = cr;
                topCr[i] = extractedTop.cr;
            }
        }
    }

    private static int DeriveIntra16x16PredictionMode(int mbType)
    {
        if (Intra16x16_0.Contains(mbType))
            return 0;
        if (Intra16x16_1.Contains(mbType))
            return 1;
        if (Intra16x16_2.Contains(mbType))
            return 2;
        if (Intra16x16_3.Contains(mbType))
            return 3;

        throw new VideoCodecDecoderException($"Invalid macroblock type for Intra_16x16 prediction: {mbType}.");
    }
}
