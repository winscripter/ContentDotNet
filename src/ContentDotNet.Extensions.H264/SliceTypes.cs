using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Constants representing slice/macroblock types according to the H.264
///   specification provided by ITU-T.
/// </summary>
public static class SliceTypes
{
    #region Public Constants

    public const int Pred_L0 = 2;
    public const int Pred_L1 = 3;
    public const int BiPred = 4;
    public const int Direct = 5;

    #region P

    // Page 129
    public const int P_L0_16x16 = 0;
    public const int P_L0_L0_16x8 = 1;
    public const int P_L0_L0_8x16 = 2;
    public const int P_8x8 = 3;
    public const int P_8x8ref0 = 4;
    public const int P_Skip = 0; // They say it's "inferred", but, what does that mean...

    // Page 134
    public const int P_L0_8x8 = 0;
    public const int P_L0_8x4 = 1;
    public const int P_L0_4x8 = 2;
    public const int P_L0_4x4 = 3;

    #endregion

    #region B

    // Page 130
    public const int B_Direct_16x16 = 0;
    public const int B_L0_16x16 = 1;
    public const int B_L1_16x16 = 2;
    public const int B_Bi_16x16 = 3;
    public const int B_L0_L0_16x8 = 4;
    public const int B_L0_L0_8x16 = 5;
    public const int B_L1_L1_16x8 = 6;
    public const int B_L1_L1_8x16 = 7;
    public const int B_L0_L1_16x8 = 8;
    public const int B_L0_L1_8x16 = 9;
    public const int B_L1_L0_16x8 = 10;
    public const int B_L1_L0_8x16 = 11;
    public const int B_L0_Bi_16x8 = 12;
    public const int B_L0_Bi_8x16 = 13;
    public const int B_L1_Bi_16x8 = 14;
    public const int B_L1_Bi_8x16 = 15;
    public const int B_Bi_L0_16x8 = 16;
    public const int B_Bi_L0_8x16 = 17;
    public const int B_Bi_L1_16x8 = 18;
    public const int B_Bi_L1_8x16 = 19;
    public const int B_Bi_Bi_16x8 = 20;
    public const int B_Bi_Bi_8x16 = 21;
    public const int B_8x8 = 22;
    public const int B_Skip = 0; // They say it's "inferred", but, what does that mean...

    // Page 135
    public const int B_Direct_8x8 = 0;
    public const int B_L0_8x8 = 1;
    public const int B_L1_8x8 = 2;
    public const int B_Bi_8x8 = 3;
    public const int B_L0_8x4 = 4;
    public const int B_L0_4x8 = 5;
    public const int B_L1_8x4 = 6;
    public const int B_L1_4x8 = 7;
    public const int B_Bi_8x4 = 8;
    public const int B_Bi_4x8 = 9;
    public const int B_L0_4x4 = 10;
    public const int B_L1_4x4 = 11;
    public const int B_Bi_4x4 = 12;

    #endregion

    #region I

    // Page 127
    public const int I_NxN = 0;
    public const int I_16x16_0_0_0 = 1;
    public const int I_16x16_1_0_0 = 2;
    public const int I_16x16_2_0_0 = 3;
    public const int I_16x16_3_0_0 = 4;
    public const int I_16x16_0_1_0 = 5;
    public const int I_16x16_1_1_0 = 6;
    public const int I_16x16_2_1_0 = 7;
    public const int I_16x16_3_1_0 = 8;
    public const int I_16x16_0_2_0 = 9;
    public const int I_16x16_1_2_0 = 10;
    public const int I_16x16_2_2_0 = 11;
    public const int I_16x16_3_2_0 = 12;
    public const int I_16x16_0_0_1 = 13;
    public const int I_16x16_1_0_1 = 14;
    public const int I_16x16_2_0_1 = 15;
    public const int I_16x16_3_0_1 = 16;
    public const int I_16x16_0_1_1 = 17;
    public const int I_16x16_1_1_1 = 18;
    public const int I_16x16_2_1_1 = 19;
    public const int I_16x16_3_1_1 = 20;
    public const int I_16x16_0_2_1 = 21;
    public const int I_16x16_1_2_1 = 22;
    public const int I_16x16_2_2_1 = 23;
    public const int I_16x16_3_2_1 = 24;
    public const int I_PCM = 25;

    #endregion

    #endregion

    #region Internal Constants

    internal const int NA = 255;
    internal const int na = NA;

    #endregion

    #region Public Functions
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsP(int sliceType) => sliceType is 0 or 5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsB(int sliceType) => sliceType is 1 or 6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsI(int sliceType) => sliceType is 2 or 7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsP(uint sliceType) => sliceType is 0 or 5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsB(uint sliceType) => sliceType is 1 or 6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsI(uint sliceType) => sliceType is 2 or 7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSP(int sliceType) => sliceType is 3 or 8;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSP(uint sliceType) => sliceType is 3 or 8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSI(int sliceType) => sliceType is 4 or 9;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSI(uint sliceType) => sliceType is 4 or 9;

    #endregion
}
