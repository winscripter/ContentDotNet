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

    public static int MbPartPredMode(int mbType, int thing /*🐣🐣🐣🐣🐣🐣🐣🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤*/)
    {
        if (thing == 0)
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
        else if (thing == 1)
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
}
