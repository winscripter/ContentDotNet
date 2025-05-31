using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Constants representing slice/macroblock types according to the H.264
///   specification provided by ITU-T.
/// </summary>
public static class SliceTypes
{
    #region Public Constants

    /// <summary>
    /// Prediction mode: List 0 prediction.
    /// </summary>
    public const int Pred_L0 = 2;

    /// <summary>
    /// Prediction mode: List 1 prediction.
    /// </summary>
    public const int Pred_L1 = 3;

    /// <summary>
    /// Prediction mode: Bi-prediction.
    /// </summary>
    public const int BiPred = 4;

    /// <summary>
    /// Prediction mode: Direct prediction.
    /// </summary>
    public const int Direct = 5;

    /// <summary>
    /// Intra prediction mode: 4x4 blocks.
    /// </summary>
    public const int Intra_4x4 = 0;

    /// <summary>
    /// Intra prediction mode: 16x16 blocks.
    /// </summary>
    public const int Intra_16x16 = 1;

    /// <summary>
    /// Intra prediction mode: 8x8 blocks.
    /// </summary>
    public const int Intra_8x8 = 2;

    /// <summary>
    /// Inter prediction mode.
    /// </summary>
    public const int Inter = 3;

    #region P

    /// <summary>
    /// P-slice: List 0 prediction, 16x16 macroblock.
    /// </summary>
    public const int P_L0_16x16 = 0;

    /// <summary>
    /// P-slice: List 0 prediction, two 16x8 macroblocks.
    /// </summary>
    public const int P_L0_L0_16x8 = 1;

    /// <summary>
    /// P-slice: List 0 prediction, two 8x16 macroblocks.
    /// </summary>
    public const int P_L0_L0_8x16 = 2;

    /// <summary>
    /// P-slice: 8x8 macroblock.
    /// </summary>
    public const int P_8x8 = 3;

    /// <summary>
    /// P-slice: 8x8 macroblock with reference index 0.
    /// </summary>
    public const int P_8x8ref0 = 4;

    /// <summary>
    /// P-slice: Skip mode (inferred).
    /// </summary>
    public const int P_Skip = 0;

    /// <summary>
    /// P-slice: List 0 prediction, 8x8 sub-macroblock.
    /// </summary>
    public const int P_L0_8x8 = 0;

    /// <summary>
    /// P-slice: List 0 prediction, 8x4 sub-macroblock.
    /// </summary>
    public const int P_L0_8x4 = 1;

    /// <summary>
    /// P-slice: List 0 prediction, 4x8 sub-macroblock.
    /// </summary>
    public const int P_L0_4x8 = 2;

    /// <summary>
    /// P-slice: List 0 prediction, 4x4 sub-macroblock.
    /// </summary>
    public const int P_L0_4x4 = 3;

    #endregion

    #region B

    /// <summary>
    /// B-slice: Direct prediction, 16x16 macroblock.
    /// </summary>
    public const int B_Direct_16x16 = 0;

    /// <summary>
    /// B-slice: List 0 prediction, 16x16 macroblock.
    /// </summary>
    public const int B_L0_16x16 = 1;

    /// <summary>
    /// B-slice: List 1 prediction, 16x16 macroblock.
    /// </summary>
    public const int B_L1_16x16 = 2;

    /// <summary>
    /// B-slice: Bi-prediction, 16x16 macroblock.
    /// </summary>
    public const int B_Bi_16x16 = 3;

    /// <summary>
    /// B-slice: List 0 prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_L0_L0_16x8 = 4;

    /// <summary>
    /// B-slice: List 0 prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_L0_L0_8x16 = 5;

    /// <summary>
    /// B-slice: List 1 prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_L1_L1_16x8 = 6;

    /// <summary>
    /// B-slice: List 1 prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_L1_L1_8x16 = 7;

    /// <summary>
    /// B-slice: List 0 and List 1 prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_L0_L1_16x8 = 8;

    /// <summary>
    /// B-slice: List 0 and List 1 prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_L0_L1_8x16 = 9;

    /// <summary>
    /// B-slice: List 1 and List 0 prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_L1_L0_16x8 = 10;

    /// <summary>
    /// B-slice: List 1 and List 0 prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_L1_L0_8x16 = 11;

    /// <summary>
    /// B-slice: List 0 and Bi-prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_L0_Bi_16x8 = 12;

    /// <summary>
    /// B-slice: List 0 and Bi-prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_L0_Bi_8x16 = 13;

    /// <summary>
    /// B-slice: List 1 and Bi-prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_L1_Bi_16x8 = 14;

    /// <summary>
    /// B-slice: List 1 and Bi-prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_L1_Bi_8x16 = 15;

    /// <summary>
    /// B-slice: Bi-prediction and List 0, two 16x8 macroblocks.
    /// </summary>
    public const int B_Bi_L0_16x8 = 16;

    /// <summary>
    /// B-slice: Bi-prediction and List 0, two 8x16 macroblocks.
    /// </summary>
    public const int B_Bi_L0_8x16 = 17;

    /// <summary>
    /// B-slice: Bi-prediction and List 1, two 16x8 macroblocks.
    /// </summary>
    public const int B_Bi_L1_16x8 = 18;

    /// <summary>
    /// B-slice: Bi-prediction and List 1, two 8x16 macroblocks.
    /// </summary>
    public const int B_Bi_L1_8x16 = 19;

    /// <summary>
    /// B-slice: Bi-prediction, two 16x8 macroblocks.
    /// </summary>
    public const int B_Bi_Bi_16x8 = 20;

    /// <summary>
    /// B-slice: Bi-prediction, two 8x16 macroblocks.
    /// </summary>
    public const int B_Bi_Bi_8x16 = 21;

    /// <summary>
    /// B-slice: 8x8 macroblock.
    /// </summary>
    public const int B_8x8 = 22;

    /// <summary>
    /// B-slice: Skip mode (inferred).
    /// </summary>
    public const int B_Skip = 0;

    /// <summary>
    /// B-slice: Direct prediction, 8x8 sub-macroblock.
    /// </summary>
    public const int B_Direct_8x8 = 0;

    /// <summary>
    /// B-slice: List 0 prediction, 8x8 sub-macroblock.
    /// </summary>
    public const int B_L0_8x8 = 1;

    /// <summary>
    /// B-slice: List 1 prediction, 8x8 sub-macroblock.
    /// </summary>
    public const int B_L1_8x8 = 2;

    /// <summary>
    /// B-slice: Bi-prediction, 8x8 sub-macroblock.
    /// </summary>
    public const int B_Bi_8x8 = 3;

    /// <summary>
    /// B-slice: List 0 prediction, 8x4 sub-macroblock.
    /// </summary>
    public const int B_L0_8x4 = 4;

    /// <summary>
    /// B-slice: List 0 prediction, 4x8 sub-macroblock.
    /// </summary>
    public const int B_L0_4x8 = 5;

    /// <summary>
    /// B-slice: List 1 prediction, 8x4 sub-macroblock.
    /// </summary>
    public const int B_L1_8x4 = 6;

    /// <summary>
    /// B-slice: List 1 prediction, 4x8 sub-macroblock.
    /// </summary>
    public const int B_L1_4x8 = 7;

    /// <summary>
    /// B-slice: Bi-prediction, 8x4 sub-macroblock.
    /// </summary>
    public const int B_Bi_8x4 = 8;

    /// <summary>
    /// B-slice: Bi-prediction, 4x8 sub-macroblock.
    /// </summary>
    public const int B_Bi_4x8 = 9;

    /// <summary>
    /// B-slice: List 0 prediction, 4x4 sub-macroblock.
    /// </summary>
    public const int B_L0_4x4 = 10;

    /// <summary>
    /// B-slice: List 1 prediction, 4x4 sub-macroblock.
    /// </summary>
    public const int B_L1_4x4 = 11;

    /// <summary>
    /// B-slice: Bi-prediction, 4x4 sub-macroblock.
    /// </summary>
    public const int B_Bi_4x4 = 12;

    #endregion

    #region I

    /// <summary>
    /// I-slice: NxN intra prediction.
    /// </summary>
    public const int I_NxN = 0;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (0,0,0).
    /// </summary>
    public const int I_16x16_0_0_0 = 1;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (1,0,0).
    /// </summary>
    public const int I_16x16_1_0_0 = 2;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (2,0,0).
    /// </summary>
    public const int I_16x16_2_0_0 = 3;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (3,0,0).
    /// </summary>
    public const int I_16x16_3_0_0 = 4;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (0,1,0).
    /// </summary>
    public const int I_16x16_0_1_0 = 5;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (1,1,0).
    /// </summary>
    public const int I_16x16_1_1_0 = 6;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (2,1,0).
    /// </summary>
    public const int I_16x16_2_1_0 = 7;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (3,1,0).
    /// </summary>
    public const int I_16x16_3_1_0 = 8;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (0,2,0).
    /// </summary>
    public const int I_16x16_0_2_0 = 9;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (1,2,0).
    /// </summary>
    public const int I_16x16_1_2_0 = 10;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (2,2,0).
    /// </summary>
    public const int I_16x16_2_2_0 = 11;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (3,2,0).
    /// </summary>
    public const int I_16x16_3_2_0 = 12;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (0,0,1).
    /// </summary>
    public const int I_16x16_0_0_1 = 13;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (1,0,1).
    /// </summary>
    public const int I_16x16_1_0_1 = 14;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (2,0,1).
    /// </summary>
    public const int I_16x16_2_0_1 = 15;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (3,0,1).
    /// </summary>
    public const int I_16x16_3_0_1 = 16;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (0,1,1).
    /// </summary>
    public const int I_16x16_0_1_1 = 17;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (1,1,1).
    /// </summary>
    public const int I_16x16_1_1_1 = 18;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (2,1,1).
    /// </summary>
    public const int I_16x16_2_1_1 = 19;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (3,1,1).
    /// </summary>
    public const int I_16x16_3_1_1 = 20;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (0,2,1).
    /// </summary>
    public const int I_16x16_0_2_1 = 21;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (1,2,1).
    /// </summary>
    public const int I_16x16_1_2_1 = 22;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (2,2,1).
    /// </summary>
    public const int I_16x16_2_2_1 = 23;

    /// <summary>
    /// I-slice: 16x16 intra prediction with specific mode (3,2,1).
    /// </summary>
    public const int I_16x16_3_2_1 = 24;

    /// <summary>
    /// I-slice: PCM mode.
    /// </summary>
    public const int I_PCM = 25;

    #endregion

    #endregion

    #region Internal Constants

    /// <summary>
    /// Not applicable or undefined value.
    /// </summary>
    internal const int NA = 255;

    /// <summary>
    /// Alias for <see cref="NA"/>.
    /// </summary>
    internal const int na = NA;

    #endregion

    /// <summary>
    /// Determines if the given slice type corresponds to a P-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is a P-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsP(int sliceType) => sliceType is 0 or 5;

    /// <summary>
    /// Determines if the given slice type corresponds to a B-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is a B-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsB(int sliceType) => sliceType is 1 or 6;

    /// <summary>
    /// Determines if the given slice type corresponds to an I-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is an I-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsI(int sliceType) => sliceType is 2 or 7;

    /// <summary>
    /// Determines if the given slice type corresponds to a P-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is a P-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsP(uint sliceType) => sliceType is 0 or 5;

    /// <summary>
    /// Determines if the given slice type corresponds to a B-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is a B-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsB(uint sliceType) => sliceType is 1 or 6;

    /// <summary>
    /// Determines if the given slice type corresponds to an I-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is an I-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsI(uint sliceType) => sliceType is 2 or 7;

    /// <summary>
    /// Determines if the given slice type corresponds to an SP-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is an SP-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSP(int sliceType) => sliceType is 3 or 8;

    /// <summary>
    /// Determines if the given slice type corresponds to an SP-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is an SP-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSP(uint sliceType) => sliceType is 3 or 8;

    /// <summary>
    /// Determines if the given slice type corresponds to an SI-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is an SI-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSI(int sliceType) => sliceType is 4 or 9;

    /// <summary>
    /// Determines if the given slice type corresponds to an SI-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <returns><c>true</c> if the slice type is an SI-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSI(uint sliceType) => sliceType is 4 or 9;

    /// <summary>
    /// Determines if the given slice type corresponds to an EI-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EI-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEI(int sliceType, uint nalUnitType) => IsI(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EI-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EI-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEI(uint sliceType, uint nalUnitType) => IsI(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EI-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EI-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEI(uint sliceType, int nalUnitType) => IsI(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EI-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EI-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEI(int sliceType, int nalUnitType) => IsI(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EP-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EP-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEP(int sliceType, uint nalUnitType) => IsP(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EP-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EP-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEP(uint sliceType, uint nalUnitType) => IsP(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EP-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EP-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEP(uint sliceType, int nalUnitType) => IsP(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EP-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EP-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEP(int sliceType, int nalUnitType) => IsP(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EB-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EB-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEB(int sliceType, uint nalUnitType) => IsB(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EB-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EB-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEB(uint sliceType, uint nalUnitType) => IsB(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EB-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EB-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEB(uint sliceType, int nalUnitType) => IsB(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given slice type corresponds to an EB-slice.
    /// </summary>
    /// <param name="sliceType">The slice type to check.</param>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <returns><c>true</c> if the slice type is an EB-slice; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEB(int sliceType, int nalUnitType) => IsB(sliceType) && nalUnitType == 14;

    /// <summary>
    /// Determines if the given macroblock type corresponds to an intra macroblock type.
    /// </summary>
    /// <param name="mbType">The macroblock type to check.</param>
    /// <returns><c>true</c> if the macroblock type corresponds to <c>Intra_16x16</c>, <c>Intra_8x8</c> or <c>Intra_4x4</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIntra(int mbType) => mbType is Intra_16x16 or Intra_8x8 or Intra_4x4;
}
