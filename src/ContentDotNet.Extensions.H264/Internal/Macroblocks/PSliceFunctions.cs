using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

internal static class PSliceFunctions
{
    public static int NumMbPart(int mbType)
    {
        if (mbType is P_L0_16x16 or P_Skip) return 1;
        if (mbType is P_L0_L0_16x8 or P_L0_L0_8x16) return 2;
        if (mbType is P_8x8 or P_8x8ref0) return 4;

        throw new InvalidOperationException("Invalid macroblock type for NumMbPart");
    }

    public static int MbPartPredMode(int mbType, int thing /*🐣*/)
    {
        // I named the second parameter 'thing' because I have no idea what
        // is its actual name. ITU-T just provides 'MbPartPredMode(mb_type, 0)'
        // and 1.

        if (thing == 0)
        {
            if (mbType is P_Skip or P_L0_16x16 or P_L0_L0_16x8 or P_L0_L0_8x16) return Pred_L0;
            else if (mbType is P_8x8 or P_8x8ref0) return na;

            throw new InvalidOperationException("Invalid macroblock type for MbPartPredMode");
        }
        else if (thing == 1)
        {
            if (mbType is P_L0_16x16 or P_8x8 or P_8x8ref0 or P_Skip) return na;
            else if (mbType is P_L0_L0_16x8 or P_L0_L0_8x16) return Pred_L0;

            throw new InvalidOperationException("Invalid macroblock type for MbPartPredMode");
        }

        throw new InvalidOperationException("Invalid second parameter");
    }

    public static int MbPartWidth(int mbType)
    {
        if (mbType is P_L0_16x16 or P_L0_L0_16x8 or P_Skip) return 16;
        else if (mbType is P_L0_L0_8x16 or P_8x8 or P_8x8ref0) return 8;

        throw new InvalidOperationException("Invalid macroblock type for MbPartWidth");
    }

    public static int MbPartHeight(int mbType)
    {
        if (mbType is P_L0_16x16 or P_L0_L0_8x16 or P_Skip) return 16;
        else if (mbType is P_L0_L0_16x8 or P_8x8 or P_8x8ref0) return 8;

        throw new InvalidOperationException("Invalid macroblock type for MbPartHeight");
    }
}
