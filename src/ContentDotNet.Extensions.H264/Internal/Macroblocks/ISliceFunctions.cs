using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

internal static class ISliceFunctions
{
    public static int MbPartPredMode(int mbType, bool transformSize8x8Flag /*🐔🎶*/)
    {
        if (mbType == I_NxN)
        {
            return transformSize8x8Flag ? Intra_4x4 : Intra_8x8;
        }

        if (mbType == I_PCM)
        {
            return na;
        }

        return Intra_16x16;
    }

    public static int GetIntra16x16PredMode(int mbType)
    {
        if (mbType == I_NxN) return na;

        if (mbType is I_16x16_0_0_0 or I_16x16_0_1_0 or I_16x16_0_2_0 or I_16x16_0_0_1 or I_16x16_0_1_1 or I_16x16_0_2_1) return 0;
        if (mbType is I_16x16_1_0_0 or I_16x16_1_1_0 or I_16x16_1_2_0 or I_16x16_1_0_1 or I_16x16_1_1_1 or I_16x16_1_2_1) return 1;
        if (mbType is I_16x16_2_0_0 or I_16x16_2_1_0 or I_16x16_2_2_0 or I_16x16_2_0_1 or I_16x16_2_1_1 or I_16x16_2_2_1) return 2;
        if (mbType is I_16x16_3_0_0 or I_16x16_3_1_0 or I_16x16_3_2_0 or I_16x16_3_0_1 or I_16x16_3_1_1 or I_16x16_3_2_1) return 3;

        return na;
    }

    public static int GetCodedBlockPatternChroma(int mbType)
    {
        if (mbType is I_16x16_0_0_0 or I_16x16_1_0_0 or I_16x16_2_0_0 or I_16x16_3_0_0 or I_16x16_0_0_1 or I_16x16_1_0_1 or I_16x16_2_0_1 or I_16x16_3_0_1) return 0;
        if (mbType is I_16x16_0_1_0 or I_16x16_1_1_0 or I_16x16_2_1_0 or I_16x16_3_1_0 or I_16x16_0_1_1 or I_16x16_1_1_1 or I_16x16_2_1_1 or I_16x16_3_1_1) return 1;
        if (mbType is I_16x16_0_2_0 or I_16x16_1_2_0 or I_16x16_2_2_0 or I_16x16_3_2_0 or I_16x16_0_2_1 or I_16x16_1_2_1 or I_16x16_2_2_1 or I_16x16_3_2_1) return 2;

        return na;
    }

    public static int GetCodedBlockPatternLuma(int mbType)
    {
        if (mbType is >= I_16x16_0_0_0 and <= I_16x16_3_2_0) return 0;
        if (mbType is >= I_16x16_0_0_1 and <= I_16x16_3_2_1) return 15;

        return na;
    }
}
