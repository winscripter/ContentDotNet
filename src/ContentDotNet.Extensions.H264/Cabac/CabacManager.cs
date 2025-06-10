using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Cabac;

internal sealed partial class CabacManager
{
    private const int TotalCabacContexts = 49;

    public const int MacroblockType = 0;
    public const int MacroblockSkipFlag = 1;
    public const int SubMacroblockType = 2;
    public const int MotionVectorDifferenceX = 3;
    public const int MotionVectorDifferenceY = 4;
    public const int ReferenceIndex = 5;
    public const int MacroblockQuantizationParameterDelta = 6;
    public const int IntraChromaPredictionMode = 7;
    public const int PreviousIntraNxNPredictionModeFlag = 8;
    public const int RemainingIntraNxNPredictionMode = 9;
    public const int MacroblockFieldDecodingFlag = 10;
    public const int CodedBlockPattern = 11;
    public const int CodedBlockFlag1 = 12;
    public const int CodedBlockFlag2 = 13;
    public const int CodedBlockFlag3 = 14;
    public const int CodedBlockFlag4 = 15;
    public const int SignificantCoeffFlag1 = 16;
    public const int SignificantCoeffFlag2 = 17;
    public const int SignificantCoeffFlag3 = 18;
    public const int SignificantCoeffFlag4 = 19;
    public const int SignificantCoeffFlag5 = 20;
    public const int SignificantCoeffFlag6 = 21;
    public const int SignificantCoeffFlag7 = 22;
    public const int SignificantCoeffFlag8 = 23;
    public const int SignificantCoeffFlag9 = 24;
    public const int SignificantCoeffFlag10 = 25;
    public const int SignificantCoeffFlag11 = 26;
    public const int SignificantCoeffFlag12 = 27;
    public const int LastSignificantCoeffFlag1 = 28;
    public const int LastSignificantCoeffFlag2 = 29;
    public const int LastSignificantCoeffFlag3 = 30;
    public const int LastSignificantCoeffFlag4 = 31;
    public const int LastSignificantCoeffFlag5 = 32;
    public const int LastSignificantCoeffFlag6 = 33;
    public const int LastSignificantCoeffFlag7 = 34;
    public const int LastSignificantCoeffFlag8 = 35;
    public const int LastSignificantCoeffFlag9 = 36;
    public const int LastSignificantCoeffFlag10 = 37;
    public const int LastSignificantCoeffFlag11 = 38;
    public const int LastSignificantCoeffFlag12 = 39;
    public const int CoeffAbsLevelMinus1_1 = 40;
    public const int CoeffAbsLevelMinus1_2 = 41;
    public const int CoeffAbsLevelMinus1_3 = 42;
    public const int CoeffAbsLevelMinus1_4 = 43;
    public const int CoeffAbsLevelMinus1_5 = 44;
    public const int CoeffAbsLevelMinus1_6 = 45;
    public const int CoeffSignFlag = 46;
    public const int EndOfSliceFlag = 47;
    public const int ConstTransformSize8x8Flag = 48;

    private readonly bool[] _init = new bool[TotalCabacContexts]; // false by default
    private readonly CabacContext[] _cabacs = new CabacContext[TotalCabacContexts];

    private readonly BitStreamReader _boundReader;

    public CabacManager(BitStreamReader boundReader, IMacroblockUtility util)
    {
        _boundReader = boundReader;
        Utility = util;
    }

    public GeneralSliceType SliceType { get; set; } = GeneralSliceType.I;
    public bool IsFrameMacroblock { get; set; } = false;
    public IMacroblockUtility Utility { get; set; }
    public DerivationContext DerivationContext { get; set; } = default;
    public int CabacInitIdc { get; set; }
    public int SliceQPY { get; set; }
    public int ChromaArrayType { get; set; }
    public int PicWidthInMbs { get; set; }
    public int MbPartIdx { get; set; }
    public int SubMbPartIdx { get; set; }
    public bool TransformSize8x8Flag { get; set; }
    public MacroblockTypeHistory MbTypeArray { get; set; }
    public MacroblockTypeHistory SubMbTypeArray { get; set; }
    public ContainerMatrix4x4x2 MvdLX { get; set; }
    public bool L0Mode { get; set; }
    public ResidualBlockType BlockType { get; set; }
    public int NumC8x8 { get; set; }

    private void InitializeOrUpdate(int cabacIndex, SyntaxElement se)
    {
        if (!_init[cabacIndex])
        {
            var (maxBinIdxCtx, ctxIdxOffset) = Binarization.GetFields(se, SliceType, BlockType, NumC8x8, IsFrameMacroblock);

            Span<int> s = stackalloc int[1];
            int ctxIdx = CabacCtxIdxDerivation.AssignCtxIdxInc(
                ctxIdxOffset,
                0,
                s,
                default,
                Utility,
                DerivationContext,
                PicWidthInMbs,
                MbPartIdx,
                SubMbPartIdx,
                TransformSize8x8Flag,
                SliceType,
                MbTypeArray,
                SubMbTypeArray,
                MvdLX,
                L0Mode,
                out _,
                out _
            );

            _cabacs[MacroblockType] = new CabacContext(_boundReader, ctxIdx, CabacInitIdc, SliceType is GeneralSliceType.I or GeneralSliceType.SI, SliceQPY);
            _init[MacroblockType] = true;
        }
        else
        {
            var (maxBinIdxCtx, ctxIdxOffset) = Binarization.GetFields(SyntaxElement.MacroblockType, SliceType, BlockType, NumC8x8, IsFrameMacroblock);

            int binIdx = _cabacs[cabacIndex].BinIdx;
            BitString priorDecodedBins = _cabacs[cabacIndex].PriorDecodedBinValues;

            Span<int> s = stackalloc int[1];
            int ctxIdx = CabacCtxIdxDerivation.AssignCtxIdxInc(
                ctxIdxOffset,
                binIdx,
                s,
                priorDecodedBins,
                Utility,
                DerivationContext,
                PicWidthInMbs,
                MbPartIdx,
                SubMbPartIdx,
                TransformSize8x8Flag,
                SliceType,
                MbTypeArray,
                SubMbTypeArray,
                MvdLX,
                L0Mode,
                out _,
                out bool bypass
            );

            _cabacs[cabacIndex] = new CabacContext(_boundReader, ctxIdx, CabacInitIdc, SliceType is GeneralSliceType.I or GeneralSliceType.SI, SliceQPY)
            {
                BinIdx = binIdx,
                PriorDecodedBinValues = priorDecodedBins
            };
        }
    }

    private int Parse(int index, SyntaxElement se)
    {
        InitializeOrUpdate(index, se);
        return Binarization.Binarize(_cabacs[index], SliceType, se, ChromaArrayType);
    }

    public int ParseMbType() => Parse(MacroblockType, SyntaxElement.MacroblockType);
    public int ParseMbSkipFlag() => Parse(MacroblockSkipFlag, SyntaxElement.MacroblockSkipFlag);
    public int ParseSubMbType() => Parse(SubMacroblockType, SyntaxElement.SubMacroblockType);
    public int ParseMvdL0() => Parse(MotionVectorDifferenceX, SyntaxElement.MotionVectorDifferenceX);
    public int ParseMvdL1() => Parse(MotionVectorDifferenceY, SyntaxElement.MotionVectorDifferenceY);
    public int ParseRefIdxLX() => Parse(ReferenceIndex, SyntaxElement.ReferenceIndex);
    public int ParseMbQpDelta() => Parse(MacroblockQuantizationParameterDelta, SyntaxElement.MacroblockQuantizationParameterDelta);
    public int ParseIntraChromaPredMode() => Parse(IntraChromaPredictionMode, SyntaxElement.IntraChromaPredictionMode);
    public int ParsePrevIntraNxNPredModeFlag() => Parse(PreviousIntraNxNPredictionModeFlag, SyntaxElement.PreviousIntraNxNPredictionModeFlag);
    public int ParseRemIntraNxNPredMode() => Parse(RemainingIntraNxNPredictionMode, SyntaxElement.RemainingIntraNxNPredictionMode);
    public int ParseMacroblockFieldDecodingFlag() => Parse(MacroblockFieldDecodingFlag, SyntaxElement.MacroblockFieldDecodingFlag);
    public int ParseCodedBlockPattern() => Parse(CodedBlockPattern, SyntaxElement.CodedBlockPattern);
    public int ParseCoeffSignFlag() => Parse(CoeffSignFlag, SyntaxElement.CoeffSignFlag);
    public int ParseEndOfSliceFlag() => Parse(EndOfSliceFlag, SyntaxElement.EndOfSliceFlag);
    public int ParseTransformSize8x8Flag() => Parse(ConstTransformSize8x8Flag, SyntaxElement.TransformSize8x8Flag);

    public int ParseCodedBlockFlag()
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (ctxBlockCat < 5)
        {
            return Parse(CodedBlockFlag1, SyntaxElement.CodedBlockFlag);
        }
        else if (5 < ctxBlockCat && ctxBlockCat < 9)
        {
            return Parse(CodedBlockFlag2, SyntaxElement.CodedBlockFlag);
        }
        else if (9 < ctxBlockCat && ctxBlockCat < 13)
        {
            return Parse(CodedBlockFlag3, SyntaxElement.CodedBlockFlag);
        }
        else if (ctxBlockCat is 5 or 9 or 13)
        {
            return Parse(CodedBlockFlag4, SyntaxElement.CodedBlockFlag);
        }

        ThrowInvalidCtxBlockCat();
        return default;
    }

    public int ParseSignificantCoeffFlag()
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (IsFrameMacroblock)
        {
            if (ctxBlockCat < 5)
            {
                return Parse(SignificantCoeffFlag1, SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(SignificantCoeffFlag2, SyntaxElement.SignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(SignificantCoeffFlag3, SyntaxElement.SignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(SignificantCoeffFlag4, SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(SignificantCoeffFlag5, SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(SignificantCoeffFlag6, SyntaxElement.SignificantCoeffFlag);
            }
        }
        else // field macroblock
        {
            if (ctxBlockCat < 5)
            {
                return Parse(SignificantCoeffFlag7, SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(SignificantCoeffFlag8, SyntaxElement.SignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(SignificantCoeffFlag9, SyntaxElement.SignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(SignificantCoeffFlag10, SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(SignificantCoeffFlag11, SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(SignificantCoeffFlag12, SyntaxElement.SignificantCoeffFlag);
            }
        }

        ThrowInvalidCtxBlockCat();
        return default;
    }

    public int ParseLastSignificantCoeffFlag()
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (IsFrameMacroblock)
        {
            if (ctxBlockCat < 5)
            {
                return Parse(LastSignificantCoeffFlag1, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(LastSignificantCoeffFlag2, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(LastSignificantCoeffFlag3, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(LastSignificantCoeffFlag4, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(LastSignificantCoeffFlag5, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(LastSignificantCoeffFlag6, SyntaxElement.LastSignificantCoeffFlag);
            }
        }
        else // field macroblock
        {
            if (ctxBlockCat < 5)
            {
                return Parse(LastSignificantCoeffFlag7, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(LastSignificantCoeffFlag8, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(LastSignificantCoeffFlag9, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(LastSignificantCoeffFlag10, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(LastSignificantCoeffFlag11, SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(LastSignificantCoeffFlag12, SyntaxElement.LastSignificantCoeffFlag);
            }
        }

        ThrowInvalidCtxBlockCat();
        return default;
    }

    public int ParseCoeffAbsLevelMinus1()
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (ctxBlockCat < 5)
        {
            return Parse(CoeffAbsLevelMinus1_1, SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (ctxBlockCat == 5)
        {
            return Parse(CoeffAbsLevelMinus1_2, SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (5 < ctxBlockCat && ctxBlockCat < 9)
        {
            return Parse(CoeffAbsLevelMinus1_3, SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (9 < ctxBlockCat && ctxBlockCat < 13)
        {
            return Parse(CoeffAbsLevelMinus1_4, SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (ctxBlockCat == 9)
        {
            return Parse(CoeffAbsLevelMinus1_5, SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (ctxBlockCat == 13)
        {
            return Parse(CoeffAbsLevelMinus1_6, SyntaxElement.CoeffAbsLevelMinus1);
        }

        ThrowInvalidCtxBlockCat();
        return default;
    }

    private static void ThrowInvalidCtxBlockCat()
    {
        throw new InvalidOperationException("Invalid ctxBlockCat");
    }
}
