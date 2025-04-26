using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

internal static class BSliceFunctions
{
    public static int NumMbPart(int mbType)
    {
        if (mbType is B_Direct_16x16 or B_Skip) return na;
        if (mbType == B_8x8) return 4;
        if (mbType is B_L0_16x16 or B_L1_16x16 or B_Bi_16x16) return 1;
        if (mbType is >= B_L0_L0_16x8 and <= B_Bi_Bi_8x16) return 2;

        throw new InvalidOperationException("Invalid macroblock type for NumMbPart");
    }

    public static int MbPartPredMode(int mbType, int mbPartIdx)
    {
        if (mbPartIdx == 0)
        {
            if (mbType is B_Direct_16x16 or B_Skip) return Direct;
            if (mbType is >= B_Bi_L0_16x8 and <= B_Bi_Bi_8x16 or B_Bi_16x16) return BiPred;
            if (mbType is B_L0_16x16 or B_L0_L0_16x8 or B_L0_L0_8x16 or
                                        B_L0_L1_16x8 or B_L0_L1_8x16 or
                                        B_L0_Bi_16x8 or B_L0_Bi_8x16) return Pred_L0;
            if (mbType is B_L1_16x16 or B_L1_L1_16x8 or B_L1_L1_8x16 or
                                        B_L1_L0_16x8 or B_L1_L0_8x16 or
                                        B_L1_Bi_16x8 or B_L1_Bi_8x16) return Pred_L1;

            throw new InvalidOperationException("Invalid macroblock type for MbPartPredMode");
        }
        else if (mbPartIdx == 1)
        {
            if (mbType is B_Direct_16x16 or B_L0_16x16 or B_L1_16x16 or B_Bi_16x16 or B_Skip) return na;
            if (mbType is B_L0_L0_16x8 or B_L0_L0_8x16 or B_L1_L0_16x8 or B_L1_L0_8x16 or
                          B_Bi_L0_16x8 or B_Bi_L0_8x16) return Pred_L0;
            if (mbType is B_L1_L1_16x8 or B_L1_L1_8x16 or B_L0_L1_16x8 or B_L0_L1_8x16 or
                          B_Bi_L1_16x8 or B_Bi_L1_8x16) return Pred_L1;
            if (mbType is B_L0_Bi_16x8 or B_L0_Bi_8x16 or B_L1_Bi_16x8 or B_L1_Bi_8x16 or
                          B_Bi_Bi_16x8 or B_Bi_Bi_8x16) return BiPred;

            throw new InvalidOperationException("Invalid macroblock type for MbPartPredMode");
        }

        throw new InvalidOperationException("The parameter is invalid");
    }

    public static int MbPartWidth(int mbType)
    {
        if (mbType is B_Direct_16x16 or B_L0_L0_8x16 or B_L1_L1_8x16 or B_L0_L1_8x16 or B_L1_L0_8x16 or B_L0_Bi_8x16 or
                      B_L1_Bi_8x16 or B_Bi_L0_8x16 or B_Bi_L1_8x16 or B_Bi_Bi_8x16 or B_8x8 or B_Skip)
            return 8;

        if (mbType is B_L0_16x16 or B_L1_16x16 or B_Bi_16x16 or B_L0_L0_16x8 or B_L1_L1_16x8 or B_L0_L1_16x8 or B_L1_L0_16x8 or
                      B_L0_Bi_16x8 or B_L1_Bi_16x8 or B_Bi_L0_16x8 or B_Bi_L1_16x8 or B_Bi_Bi_16x8)
            return 16;

        throw new InvalidOperationException("Invalid macroblock type for MbPartWidth");
    }

    public static int MbPartHeight(int mbType)
    {
        if (mbType is B_Direct_16x16 or B_L0_L0_16x8 or B_L1_L1_16x8 or B_L0_L1_16x8 or B_L1_L0_16x8 or B_L0_Bi_16x8 or
                      B_L1_Bi_16x8 or B_Bi_L0_16x8 or B_Bi_L1_16x8 or B_Bi_Bi_16x8 or B_8x8 or B_Skip)
            return 8;

        if (mbType is B_L0_16x16 or B_Bi_16x16 or B_L0_L0_8x16 or B_L1_L1_8x16 or B_L0_L1_8x16 or B_L1_L0_8x16 or B_L0_Bi_8x16 or
                      B_L1_Bi_8x16 or B_Bi_L0_8x16 or B_Bi_L1_8x16 or B_Bi_Bi_8x16)
            return 16;

        throw new InvalidOperationException("Invalid macroblock type for MbPartHeight");
    }

    public static int NumSubMbPart(int mbType)
    {
        if (mbType is B_Direct_8x8 || mbType is >= B_L0_4x4 and <= B_Bi_4x4) return 4;
        if (mbType is >= B_L0_8x8 and <= B_Bi_8x8) return 1;
        if (mbType is >= B_L0_8x4 and <= B_Bi_4x8) return 2;

        throw new InvalidOperationException("Invalid macroblock type for NumSubMbPart");
    }

    public static int SubMbPredMode(int mbType)
    {
        if (mbType == B_Direct_8x8) return Direct;
        if (mbType is B_L0_8x8 or B_L0_8x4 or B_L0_4x8 or B_L0_4x4) return Pred_L0;
        if (mbType is B_L1_8x8 or B_L1_8x4 or B_L1_4x8 or B_L1_4x4) return Pred_L1;
        if (mbType is B_Bi_8x8 or B_Bi_8x4 or B_Bi_4x8 or B_Bi_4x4) return BiPred;

        throw new InvalidOperationException("Invalid macroblock type for SubMbPredMode");
    }

    public static int SubMbPartWidth(int mbType)
    {
        if (mbType is B_Direct_8x8 or B_L0_4x8 or B_L1_4x8 or B_Bi_4x8 or B_L0_4x4 or B_L1_4x4 or B_Bi_4x4) return 4;
        if (mbType is B_L0_8x8 or B_L1_8x8 or B_Bi_8x8 or B_L0_8x4 or B_L1_8x4 or B_Bi_8x4) return 8;

        throw new InvalidOperationException("Invalid macroblock type for SubMbPartWidth");
    }

    public static int SubMbPartHeight(int mbType)
    {
        if (mbType is B_Direct_8x8 or B_L0_8x4 or B_L1_8x4 or B_Bi_8x4 or B_L0_4x4 or B_L1_4x4 or B_Bi_4x4) return 4;
        if (mbType is B_L0_8x8 or B_L1_8x8 or B_Bi_8x8 or B_L0_4x8 or B_L1_4x8 or B_Bi_4x8) return 8;

        throw new InvalidOperationException("Invalid macroblock type for SubMbPartHeight");
    }
}
