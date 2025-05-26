using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
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
                for (int j = 0; j < 16; j++)
                {
                    matrixL[i, j] = mbLayer.PcmLuma[i * 16 + j];
                    matrixCb[i, j] = mbLayer.PcmChroma[i * 8 + j];
                    matrixCr[i, j] = mbLayer.PcmChroma[i * 8 + j + 1];
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
                        // and, in the case of CBP's, residuals.

                        var mbPred = mbLayer.Prediction!.Value;
                        var residual = mbLayer.Intra16x16Residual!.Value;

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

                        int leftTop = (int)util.GetPixels(new AddressFlow(currMbAddr, picWidthInMbs, mbsInFrame).Up().Left().Value)[15, 15];

                        var predSamples = new IntraPredictionSamples(p, left, top, leftTop);

                        Span<int> predBufferBackingL = stackalloc int[16 * 16];
                        var predBufferL = new Matrix16x16(predBufferBackingL);
                        Span<int> predBufferBackingCb = stackalloc int[16 * 16];
                        var predBufferCb = new Matrix16x16(predBufferBackingCb);
                        Span<int> predBufferBackingCr = stackalloc int[16 * 16];
                        var predBufferCr = new Matrix16x16(predBufferBackingCr);

                        intraInter.IntraPredictor.Intra16x16SamplePredict(cSL, predBufferL, constrainedIntraPredFlag, DeriveIntra16x16PredictionMode((int)mbLayer.MbType), predSamples, dc);

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
            }
        }

        return (matrixL, matrixCb, matrixCr);
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
