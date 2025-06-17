using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Manages CABAC parsing.
/// </summary>
public sealed partial class CabacManager
{
    private const int TotalCabacContexts = 49;

    private const int MacroblockType = 0;
    private const int MacroblockSkipFlag = 1;
    private const int SubMacroblockType = 2;
    private const int MotionVectorDifferenceX = 3;
    private const int MotionVectorDifferenceY = 4;
    private const int ReferenceIndex = 5;
    private const int MacroblockQuantizationParameterDelta = 6;
    private const int IntraChromaPredictionMode = 7;
    private const int PreviousIntraNxNPredictionModeFlag = 8;
    private const int RemainingIntraNxNPredictionMode = 9;
    private const int MacroblockFieldDecodingFlag = 10;
    private const int CodedBlockPattern = 11;
    private const int CodedBlockFlag1 = 12;
    private const int CodedBlockFlag2 = 13;
    private const int CodedBlockFlag3 = 14;
    private const int CodedBlockFlag4 = 15;
    private const int SignificantCoeffFlag1 = 16;
    private const int SignificantCoeffFlag2 = 17;
    private const int SignificantCoeffFlag3 = 18;
    private const int SignificantCoeffFlag4 = 19;
    private const int SignificantCoeffFlag5 = 20;
    private const int SignificantCoeffFlag6 = 21;
    private const int SignificantCoeffFlag7 = 22;
    private const int SignificantCoeffFlag8 = 23;
    private const int SignificantCoeffFlag9 = 24;
    private const int SignificantCoeffFlag10 = 25;
    private const int SignificantCoeffFlag11 = 26;
    private const int SignificantCoeffFlag12 = 27;
    private const int LastSignificantCoeffFlag1 = 28;
    private const int LastSignificantCoeffFlag2 = 29;
    private const int LastSignificantCoeffFlag3 = 30;
    private const int LastSignificantCoeffFlag4 = 31;
    private const int LastSignificantCoeffFlag5 = 32;
    private const int LastSignificantCoeffFlag6 = 33;
    private const int LastSignificantCoeffFlag7 = 34;
    private const int LastSignificantCoeffFlag8 = 35;
    private const int LastSignificantCoeffFlag9 = 36;
    private const int LastSignificantCoeffFlag10 = 37;
    private const int LastSignificantCoeffFlag11 = 38;
    private const int LastSignificantCoeffFlag12 = 39;
    private const int CoeffAbsLevelMinus1_1 = 40;
    private const int CoeffAbsLevelMinus1_2 = 41;
    private const int CoeffAbsLevelMinus1_3 = 42;
    private const int CoeffAbsLevelMinus1_4 = 43;
    private const int CoeffAbsLevelMinus1_5 = 44;
    private const int CoeffAbsLevelMinus1_6 = 45;
    private const int CoeffSignFlag = 46;
    private const int EndOfSliceFlag = 47;
    private const int ConstTransformSize8x8Flag = 48;

    private readonly bool[] _init = new bool[TotalCabacContexts]; // false by default
    private readonly CabacContext[] _cabacs = new CabacContext[TotalCabacContexts];

    private readonly BitStreamReader _boundReader;

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacManager"/> class.
    /// </summary>
    /// <param name="boundReader">The <see cref="BitStreamReader"/> to use for reading CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    public CabacManager(BitStreamReader boundReader, IMacroblockUtility util)
    {
        _boundReader = boundReader;
        Utility = util;
    }

    /// <summary>
    ///   Gets or sets the type of the current slice.
    /// </summary>
    public GeneralSliceType SliceType { get; set; } = GeneralSliceType.I;

    /// <summary>
    ///   Gets or sets a value indicating whether the current macroblock is a frame macroblock.
    /// </summary>
    public bool IsFrameMacroblock { get; set; } = false;

    /// <summary>
    ///   Gets or sets the macroblock utility instance.
    /// </summary>
    public IMacroblockUtility Utility { get; set; }

    /// <summary>
    ///   Gets or sets the derivation context for CABAC parsing.
    /// </summary>
    public DerivationContext DerivationContext { get; set; } = default;

    /// <summary>
    ///   Gets or sets the CABAC initialization IDC.
    /// </summary>
    public int CabacInitIdc { get; set; }

    /// <summary>
    ///   Gets or sets the slice quantization parameter for the luma channel.
    /// </summary>
    public int SliceQPY { get; set; }

    /// <summary>
    ///   Gets or sets the chroma array type.
    /// </summary>
    public int ChromaArrayType { get; set; }

    /// <summary>
    ///   Gets or sets the picture width in macroblocks.
    /// </summary>
    public int PicWidthInMbs { get; set; }

    /// <summary>
    ///   Gets or sets the macroblock partition index.
    /// </summary>
    public int MbPartIdx { get; set; }

    /// <summary>
    ///   Gets or sets the sub-macroblock partition index.
    /// </summary>
    public int SubMbPartIdx { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether the transform size is 8x8.
    /// </summary>
    public bool TransformSize8x8Flag { get; set; }

    /// <summary>
    ///   Gets or sets the macroblock type history.
    /// </summary>
    public MacroblockTypeHistory MbTypeArray { get; set; }

    /// <summary>
    ///   Gets or sets the sub-macroblock type history.
    /// </summary>
    public MacroblockTypeHistory SubMbTypeArray { get; set; }

    /// <summary>
    ///   Gets or sets the motion vector difference matrix.
    /// </summary>
    public ContainerMatrix4x4x2 MvdLX { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether L0 mode is used.
    /// </summary>
    public bool L0Mode { get; set; }

    /// <summary>
    ///   Gets or sets the type of the residual block.
    /// </summary>
    public ResidualBlockType BlockType { get; set; }

    /// <summary>
    ///   Gets or sets the number of 8x8 chroma blocks.
    /// </summary>
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
        //else
        //{
        //    var (maxBinIdxCtx, ctxIdxOffset) = Binarization.GetFields(SyntaxElement.MacroblockType, SliceType, BlockType, NumC8x8, IsFrameMacroblock);

        //    int binIdx = _cabacs[cabacIndex].BinIdx;
        //    BitString priorDecodedBins = _cabacs[cabacIndex].PriorDecodedBinValues;

        //    Span<int> s = stackalloc int[1];
        //    int ctxIdx = CabacCtxIdxDerivation.AssignCtxIdxInc(
        //        ctxIdxOffset,
        //        binIdx,
        //        s,
        //        priorDecodedBins,
        //        Utility,
        //        DerivationContext,
        //        PicWidthInMbs,
        //        MbPartIdx,
        //        SubMbPartIdx,
        //        TransformSize8x8Flag,
        //        SliceType,
        //        MbTypeArray,
        //        SubMbTypeArray,
        //        MvdLX,
        //        L0Mode,
        //        out _,
        //        out bool bypass
        //    );

        //    _cabacs[cabacIndex] = new CabacContext(_boundReader, ctxIdx, CabacInitIdc, SliceType is GeneralSliceType.I or GeneralSliceType.SI, SliceQPY)
        //    {
        //        BinIdx = binIdx,
        //        PriorDecodedBinValues = priorDecodedBins
        //    };
        //}
    }

    private int Parse(int index, SyntaxElement se)
    {
        InitializeOrUpdate(index, se);
        return Binarization.Binarize(_cabacs[index], SliceType, se, ChromaArrayType);
    }

    /// <summary>
    ///   Parses the syntax element <c>mb_type</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMbType() => Parse(MacroblockType, SyntaxElement.MacroblockType);

    /// <summary>
    ///   Parses the syntax element <c>mb_skip_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMbSkipFlag() => Parse(MacroblockSkipFlag, SyntaxElement.MacroblockSkipFlag);

    /// <summary>
    ///   Parses the syntax element <c>sub_mb_type</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseSubMbType() => Parse(SubMacroblockType, SyntaxElement.SubMacroblockType);

    /// <summary>
    ///   Parses the syntax element <c>mvd_l0</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMvdL0() => Parse(MotionVectorDifferenceX, SyntaxElement.MotionVectorDifferenceX);

    /// <summary>
    ///   Parses the syntax element <c>mvd_l1</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMvdL1() => Parse(MotionVectorDifferenceY, SyntaxElement.MotionVectorDifferenceY);

    /// <summary>
    ///   Parses the syntax element <c>ref_idx_LX</c> where LX can be L0 or L1.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseRefIdxLX() => Parse(ReferenceIndex, SyntaxElement.ReferenceIndex);

    /// <summary>
    ///   Parses the syntax element <c>mb_qp_delta</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMbQpDelta() => Parse(MacroblockQuantizationParameterDelta, SyntaxElement.MacroblockQuantizationParameterDelta);

    /// <summary>
    ///   Parses the syntax element <c>intra_chroma_pred_mode</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseIntraChromaPredMode() => Parse(IntraChromaPredictionMode, SyntaxElement.IntraChromaPredictionMode);

    /// <summary>
    ///   Parses the syntax element <c>prev_intra_NxN_pred_mode_flag</c> where N can be 4 or 8.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParsePrevIntraNxNPredModeFlag() => Parse(PreviousIntraNxNPredictionModeFlag, SyntaxElement.PreviousIntraNxNPredictionModeFlag);

    /// <summary>
    ///   Parses the syntax element <c>rem_intra_NxN_pred_mode</c> where N can be 4 or 8.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseRemIntraNxNPredMode() => Parse(RemainingIntraNxNPredictionMode, SyntaxElement.RemainingIntraNxNPredictionMode);

    /// <summary>
    ///   Parses the syntax element <c>mb_field_decoding_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMacroblockFieldDecodingFlag() => Parse(MacroblockFieldDecodingFlag, SyntaxElement.MacroblockFieldDecodingFlag);

    /// <summary>
    ///   Parses the syntax element <c>coded_block_pattern</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseCodedBlockPattern() => Parse(CodedBlockPattern, SyntaxElement.CodedBlockPattern);

    /// <summary>
    ///   Parses the syntax element <c>coeff_sign_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseCoeffSignFlag() => Parse(CoeffSignFlag, SyntaxElement.CoeffSignFlag);

    /// <summary>
    ///   Parses the syntax element <c>end_of_slice_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseEndOfSliceFlag() => Parse(EndOfSliceFlag, SyntaxElement.EndOfSliceFlag);

    /// <summary>
    ///   Parses the syntax element <c>transform_size_8x8_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseTransformSize8x8Flag() => Parse(ConstTransformSize8x8Flag, SyntaxElement.TransformSize8x8Flag);

    /// <summary>
    ///   Parses the syntax element <c>coded_block_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
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

    /// <summary>
    ///   Parses the syntax element <c>significant_coeff_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
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

    /// <summary>
    ///   Parses the syntax element <c>last_significant_coeff_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
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

    /// <summary>
    ///   Parses the syntax element <c>coeff_abs_level_minus1</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
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
