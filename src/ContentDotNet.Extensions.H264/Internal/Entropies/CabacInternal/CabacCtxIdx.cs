using ContentDotNet.Extensions.H264.Utilities;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Internal.Entropies.CabacInternal;

internal static class CabacCtxIdx
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MbType(bool isLeftMacroblockIntra, bool isTopMacroblockIntra)
        => 27 + Int32Boolean.I32(isLeftMacroblockIntra) + Int32Boolean.I32(isTopMacroblockIntra);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MbSkipFlag(bool isLeftMacroblockSkipped, bool isTopMacroblockSkipped)
        => 11 + Int32Boolean.I32(isLeftMacroblockSkipped) + Int32Boolean.I32(isTopMacroblockSkipped);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SubMbType(bool isLeftSubMacroblockIntra, bool isTopSubMacroblockIntra)
        => 21 + Int32Boolean.I32(isLeftSubMacroblockIntra) + Int32Boolean.I32(isTopSubMacroblockIntra);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Mvd(int mvdLeft, int mvdTop)
        => 40 + (Math.Abs(mvdLeft) > 0 || Math.Abs(mvdTop) > 0 ? 1 : 0)
                 + (Math.Abs(mvdLeft) > 0 && Math.Abs(mvdTop) > 0 ? 1 : 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RefIdx(bool isLeftMacroblockIntra, bool isTopMacroblockIntra)
        => 54 + Int32Boolean.I32(isLeftMacroblockIntra) + Int32Boolean.I32(isTopMacroblockIntra);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MbQpDelta(bool isLeftMacroblockIntra, bool isTopMacroblockIntra)
        => 60 + Int32Boolean.I32(isLeftMacroblockIntra) + Int32Boolean.I32(isTopMacroblockIntra);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IntraChromaPredMode(bool isLeftMacroblockIntra, bool isTopMacroblockIntra)
        => 64 + Int32Boolean.I32(isLeftMacroblockIntra) + Int32Boolean.I32(isTopMacroblockIntra);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int PrevIntra4x4PredModeFlag(bool isLeftMacroblockIntra, bool isTopMacroblockIntra)
        => 68 + Int32Boolean.I32(isLeftMacroblockIntra) + Int32Boolean.I32(isTopMacroblockIntra);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CodedBlockPattern(int blockCategory)
        => 73 + blockCategory;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CodedBlockFlag(int blockCategory)
        => 85 + blockCategory;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SignificantCoeffFlag(int blockCategory)
        => 105 + blockCategory;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastSignificantCoeffFlag(int blockCategory)
        => 166 + blockCategory;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CoeffAbsLevelMinus1(int blockCategory)
        => 227 + blockCategory;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EndOfSliceFlag()
        => 276;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int TransformSize8x8Flag(bool leftUses8x8Transform, bool topUses8x8Transform)
        => 399 + Int32Boolean.I32(leftUses8x8Transform) + Int32Boolean.I32(topUses8x8Transform);
}
