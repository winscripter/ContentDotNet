namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

internal static class MacroblockKinds
{
    // https://github.com/zoltanmaric/h264-fer/blob/master/fer_h264/fer_h264/h264_globals.h#L22C1-L80C19
    public const byte NA = 0xFF;

    public const byte P_L0_16x16 = 0;
    public const byte P_L0_L0_16x8 = 1;
    public const byte P_L0_L0_8x16 = 2;
    public const byte P_8x8 = 3;
    public const byte P_8x8ref0 = 4;

    public const byte I_4x4 = 0;
    public const byte I_16x16_0_0_0 = 1;
    public const byte I_16x16_1_0_0 = 2;
    public const byte I_16x16_2_0_0 = 3;
    public const byte I_16x16_3_0_0 = 4;
    public const byte I_16x16_0_1_0 = 5;
    public const byte I_16x16_1_1_0 = 6;
    public const byte I_16x16_2_1_0 = 7;
    public const byte I_16x16_3_1_0 = 8;
    public const byte I_16x16_0_2_0 = 9;
    public const byte I_16x16_1_2_0 = 10;
    public const byte I_16x16_2_2_0 = 11;
    public const byte I_16x16_3_2_0 = 12;
    public const byte I_16x16_0_0_1 = 13;
    public const byte I_16x16_1_0_1 = 14;
    public const byte I_16x16_2_0_1 = 15;
    public const byte I_16x16_3_0_1 = 16;
    public const byte I_16x16_0_1_1 = 17;
    public const byte I_16x16_1_1_1 = 18;
    public const byte I_16x16_2_1_1 = 19;
    public const byte I_16x16_3_1_1 = 20;
    public const byte I_16x16_0_2_1 = 21;
    public const byte I_16x16_1_2_1 = 22;
    public const byte I_16x16_2_2_1 = 23;
    public const byte I_16x16_3_2_1 = 24;
    public const byte I_PCM = 25;

    public const byte P_Skip = 31;

    public const byte P_L0_8x8 = 0;
    public const byte P_L0_8x4 = 1;
    public const byte P_L0_4x8 = 2;
    public const byte P_L0_4x4 = 3;
    public const byte B_Direct_8x8 = 4;

    public const byte Intra_4x4 = 0;
    public const byte Intra_16x16 = 1;
    public const byte Pred_L0 = 2;
    public const byte Pred_L1 = 3;
    public const byte BiPred = 4;
    public const byte Direct = 5;

    public const byte P = 0;
    public const byte B = 1;
    public const byte I = 2;
    public const byte SP = 3;
    public const byte SI = 4;

    // STUB: Incorporated code doesn't have this
    public const byte Intra_8x8 = 140;
}
