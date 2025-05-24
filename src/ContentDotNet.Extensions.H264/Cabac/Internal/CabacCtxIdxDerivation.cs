using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Cabac.Internal;

internal static class CabacCtxIdxDerivation
{
    private static ReadOnlySpan<int> CodedBlockFlagToCtxIdxBlockCatOffsetAssignments => [
        0, 4, 8, 12, 16, 0, 0, 4, 8, 4, 0, 4, 8, 8
    ];

    private static ReadOnlySpan<int> SignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments => [
        0, 15, 29, 44, 47, 0, 0, 15, 29, 0, 0, 15, 29, 0
    ];

    private static ReadOnlySpan<int> LastSignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments => [
        0, 15, 29, 44, 47, 0, 0, 15, 29, 0, 0, 15, 29, 0 // Same as the one above
    ];

    private static ReadOnlySpan<int> CoeffAbsLevelMinus1ToCtxIdxBlockCatOffsetAssignments => [
        0, 10, 20, 30, 39, 0, 0, 10, 20, 0, 0, 10, 20, 0
    ];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int AssignCtxIdxBlockCatOffsetForCodedBlockFlag(int ctxBlockCat) => CodedBlockFlagToCtxIdxBlockCatOffsetAssignments[ctxBlockCat];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int AssignCtxIdxBlockCatOffsetForSignificantCoeffFlag(int ctxBlockCat) => SignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments[ctxBlockCat];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int AssignCtxIdxBlockCatOffsetForLastSignificantCoeffFlag(int ctxBlockCat) => LastSignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments[ctxBlockCat];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int AssignCtxIdxBlockCatOffsetForCoeffAbsLevelMinus1(int ctxBlockCat) => CoeffAbsLevelMinus1ToCtxIdxBlockCatOffsetAssignments[ctxBlockCat];

    internal static int DeriveCtxIdx(int ctxIdxOffset, int binIdx, Span<int> refIdxLX, BitString priorDecodedBinValues, IMacroblockUtility util, DerivationContext dc, int picWidthInMbs, int mbPartIdx, int subMbPartIdx, bool transformSize8x8Flag, GeneralSliceType sliceType, MacroblockTypeHistory mbTypeArray, MacroblockTypeHistory subMbTypeArray, ContainerMatrix4x4x2 mvdLX, bool invokedForL0, out bool applyInference)
    {
        int ctxIdxInc = AssignCtxIdxInc(ctxIdxOffset, binIdx, refIdxLX, priorDecodedBinValues, util, dc, picWidthInMbs, mbPartIdx, subMbPartIdx, transformSize8x8Flag, sliceType, mbTypeArray, subMbTypeArray, mvdLX, invokedForL0, out applyInference);
        return ctxIdxInc + ctxIdxOffset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int DeriveCtxIdxForCodedBlockFlag(int ctxBlockCat, int ctxIdxOffset)
    {
        return ctxIdxOffset + AssignCtxIdxBlockCatOffsetForCodedBlockFlag(ctxBlockCat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int DeriveCtxIdxForSignificantCoeffFlag(int ctxBlockCat, int ctxIdxOffset)
    {
        return ctxIdxOffset + AssignCtxIdxBlockCatOffsetForSignificantCoeffFlag(ctxBlockCat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int DeriveCtxIdxForLastSignificantCoeffFlag(int ctxBlockCat, int ctxIdxOffset)
    {
        return ctxIdxOffset + AssignCtxIdxBlockCatOffsetForLastSignificantCoeffFlag(ctxBlockCat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int DeriveCtxIdxForCoeffAbsLevelMinus1(int ctxBlockCat, int ctxIdxOffset)
    {
        return ctxIdxOffset + AssignCtxIdxBlockCatOffsetForCoeffAbsLevelMinus1(ctxBlockCat);
    }

    internal static int AssignCtxIdxInc(int ctxIdxOffset, int binIdx, Span<int> refIdxLX, BitString priorDecodedBinValues, IMacroblockUtility util, DerivationContext dc, int picWidthInMbs, int mbPartIdx, int subMbPartIdx, bool transformSize8x8Flag, GeneralSliceType sliceType, MacroblockTypeHistory mbTypeArray, MacroblockTypeHistory subMbTypeArray, ContainerMatrix4x4x2 mvdLX, bool invokedForL0, out bool applyInference)
    {
        applyInference = false;

        if (ctxIdxOffset == 0)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMbType(ctxIdxOffset, picWidthInMbs, util, dc);
            else
                return SliceTypes.NA;
        }
        else if (ctxIdxOffset == 3)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMbType(ctxIdxOffset, picWidthInMbs, util, dc);
            else if (binIdx == 2)
                return 3;
            else if (binIdx == 3)
                return 4;
            else if (binIdx is 4 or 5)
                return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
            else
                return 7;
        }
        else if (ctxIdxOffset == 11)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMbSkipFlag(util, dc, picWidthInMbs, dc.IsMbaff, false, out applyInference);
            else
                throw new InvalidOperationException();
        }
        else if (ctxIdxOffset == 14)
        {
            if (binIdx == 0)
                return 0;
            else if (binIdx == 1)
                return 1;
            else if (binIdx == 2)
                return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
            throw new InvalidOperationException();
        }
        else if (ctxIdxOffset == 17)
        {
            if (binIdx == 0)
                return 0;
            else if (binIdx == 2)
                return 1;
            else if (binIdx == 3)
                return 2;
            else if (binIdx == 4)
                return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
            else
                return 3;
        }
        else if (ctxIdxOffset == 21)
            return binIdx;
        else if (ctxIdxOffset == 24)
            return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
        else if (ctxIdxOffset == 27)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMbType(ctxIdxOffset, picWidthInMbs, util, dc);
            else if (binIdx == 1)
                return 3;
            else if (binIdx == 2)
                return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
            else
                return 5;
        }
        else if (ctxIdxOffset == 32)
        {
            if (binIdx == 0)
                return 0;
            else if (binIdx == 2)
                return 1;
            else if (binIdx == 3)
                return 2;
            else if (binIdx == 4)
                return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
            else
                return 3;
        }
        else if (ctxIdxOffset == 36)
        {
            if (binIdx == 0)
                return 0;
            else if (binIdx == 1)
                return 1;
            else if (binIdx == 2)
                return CabacCtxIdxIncDerivation.AssignCtxIdxIncUsingPriorDecodedBinValues(Int32Boolean.I32(priorDecodedBinValues[0]), Int32Boolean.I32(priorDecodedBinValues[1]), Int32Boolean.I32(priorDecodedBinValues[2]), ctxIdxOffset, binIdx);
            else
                return 3;
        }
        else if (ctxIdxOffset is 40 or 47)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMvdLX(mbPartIdx, subMbPartIdx, ctxIdxOffset, transformSize8x8Flag, util, sliceType, dc, mbTypeArray, subMbTypeArray, dc.MbType, mvdLX, invokedForL0);
            else if (binIdx == 1)
                return 3;
            else if (binIdx == 2)
                return 4;
            else if (binIdx == 3)
                return 5;
            else
                return 6;
        }
        else if (ctxIdxOffset == 54)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForRefIdxLX(mbPartIdx, util, transformSize8x8Flag, invokedForL0, dc.MbType, mbTypeArray, refIdxLX, subMbTypeArray, sliceType, dc);
            else if (binIdx == 1)
                return 4;
            else
                return 5;
        }
        else if (ctxIdxOffset == 60)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMbQpDelta(util, dc, transformSize8x8Flag, sliceType);
            else if (binIdx == 1)
                return 2;
            else
                return 3;
        }
        else if (ctxIdxOffset == 64)
        {
            if (binIdx == 0)
                return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForIntraChromaPredMode(dc, util, picWidthInMbs);
            else
                return 3;
        }
        else if (ctxIdxOffset is 68 or 69)
            return 0;
        else if (ctxIdxOffset == 70)
            return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForMbFieldDecodingFlag(util, picWidthInMbs, dc, out applyInference);
        else if (ctxIdxOffset is 73 or 77)
            return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForCodedBlockPattern(ctxIdxOffset, binIdx, picWidthInMbs, dc, util);
        else if (ctxIdxOffset == 276)
            return 0; // Everything else is "not an"
        else if (ctxIdxOffset == 399)
            return CabacCtxIdxIncDerivation.DeriveCtxIdxIncForTransformSize8x8Flag(dc, util, picWidthInMbs);
        else
            throw new ArgumentException("Invalid ctxIdxOffset values", nameof(ctxIdxOffset));

        throw new InvalidOperationException("Cannot derive ctxIdxInc");
    }
}
