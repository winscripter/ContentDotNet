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
public sealed partial class CabacReader
{
    private const int TotalCabacContexts = 1024;

    private readonly bool[] _init = new bool[TotalCabacContexts]; // false by default
    private readonly CabacContext[] _cabacs = new CabacContext[TotalCabacContexts];
    private readonly ArithmeticDecoder _arithmeticDecodingEngine;

    private readonly BitStreamReader _boundReader;

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacReader"/> class.
    /// </summary>
    /// <param name="boundReader">The <see cref="BitStreamReader"/> to use for reading CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    public CabacReader(BitStreamReader boundReader, IMacroblockUtility util)
    {
        _boundReader = boundReader;
        Utility = util;
        _arithmeticDecodingEngine = new ArithmeticDecoder(boundReader);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacReader"/> class.
    /// </summary>
    /// <param name="boundReader">The <see cref="BitStreamReader"/> to use for reading CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    /// <param name="decoder">The decoder.</param>
    public CabacReader(BitStreamReader boundReader, IMacroblockUtility util, ArithmeticDecoder decoder)
    {
        _boundReader = boundReader;
        Utility = util;
        _arithmeticDecodingEngine = decoder;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacReader"/> class.
    /// </summary>
    /// <param name="boundReader">The <see cref="BitStreamReader"/> to use for reading CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    /// <param name="codIOffset">CodIOffset</param>
    /// <param name="codIRange">CodIRange</param>
    public CabacReader(BitStreamReader boundReader, IMacroblockUtility util, uint codIOffset, uint codIRange)
        : this(boundReader, util, new ArithmeticDecoder(boundReader, codIOffset, codIRange))
    {
    }

    /// <summary>
    ///   Gets the arithmetic decoding engine.
    /// </summary>
    public ArithmeticDecoder Decoder => _arithmeticDecodingEngine;

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
    public DerivationContext DerivationContext { get; set; } = null!;

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

    private void InitializeOrUpdate(SyntaxElement se)
    {
        var (ctxIdx, bypassFlag) = GetCtxIdxAndBypassFlag(se);

        if (!_init[ctxIdx])
        {
            _cabacs[ctxIdx] = new CabacContext(ctxIdx, CabacInitIdc, SliceType is GeneralSliceType.I or GeneralSliceType.SI, bypassFlag, SliceQPY);
            _init[ctxIdx] = true;
        }
    }

    private (int ctxIdx, bool bypassFlag) GetCtxIdxAndBypassFlag(SyntaxElement se)
    {
        var (maxBinIdxCtx, ctxIdxOffset, bypassFlag) = Binarization.GetFields(se, SliceType, BlockType, NumC8x8, IsFrameMacroblock);

        Span<int> s = stackalloc int[1];
        int ctxIdx = CabacCtxIdxDerivation.AssignCtxIdxInc(
            ctxIdxOffset,
            0,
            s,
            _arithmeticDecodingEngine.PreviouslyDecodedBins,
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

        return (ctxIdx, bypassFlag);
    }

    private int Parse(SyntaxElement se)
    {
        InitializeOrUpdate(se);
        var (ctxIdx, _) = GetCtxIdxAndBypassFlag(se);
        return Binarization.Binarize(_arithmeticDecodingEngine, ref _cabacs[ctxIdx], SliceType, se, ChromaArrayType);
    }

    /// <summary>
    ///   Parses the syntax element <c>mb_type</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMbType() => Parse(SyntaxElement.MacroblockType);

    /// <summary>
    ///   Parses the syntax element <c>mb_skip_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMbSkipFlag() => Parse(SyntaxElement.MacroblockSkipFlag);

    /// <summary>
    ///   Parses the syntax element <c>sub_mb_type</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseSubMbType() => Parse(SyntaxElement.SubMacroblockType);

    /// <summary>
    ///   Parses the syntax element <c>mvd_l0</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMvdL0() => Parse(SyntaxElement.MotionVectorDifferenceX);

    /// <summary>
    ///   Parses the syntax element <c>mvd_l1</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMvdL1() => Parse(SyntaxElement.MotionVectorDifferenceY);

    /// <summary>
    ///   Parses the syntax element <c>ref_idx_LX</c> where LX can be L0 or L1.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseRefIdxLX() => Parse(SyntaxElement.ReferenceIndex);

    /// <summary>
    ///   Parses the syntax element <c>mb_qp_delta</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMbQpDelta() => Parse(SyntaxElement.MacroblockQuantizationParameterDelta);

    /// <summary>
    ///   Parses the syntax element <c>intra_chroma_pred_mode</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseIntraChromaPredMode() => Parse(SyntaxElement.IntraChromaPredictionMode);

    /// <summary>
    ///   Parses the syntax element <c>prev_intra_NxN_pred_mode_flag</c> where N can be 4 or 8.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParsePrevIntraNxNPredModeFlag() => Parse(SyntaxElement.PreviousIntraNxNPredictionModeFlag);

    /// <summary>
    ///   Parses the syntax element <c>rem_intra_NxN_pred_mode</c> where N can be 4 or 8.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseRemIntraNxNPredMode() => Parse(SyntaxElement.RemainingIntraNxNPredictionMode);

    /// <summary>
    ///   Parses the syntax element <c>mb_field_decoding_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseMacroblockFieldDecodingFlag() => Parse(SyntaxElement.MacroblockFieldDecodingFlag);

    /// <summary>
    ///   Parses the syntax element <c>coded_block_pattern</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseCodedBlockPattern() => Parse(SyntaxElement.CodedBlockPattern);

    /// <summary>
    ///   Parses the syntax element <c>coeff_sign_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseCoeffSignFlag() => Parse(SyntaxElement.CoeffSignFlag);

    /// <summary>
    ///   Parses the syntax element <c>end_of_slice_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseEndOfSliceFlag() => Parse(SyntaxElement.EndOfSliceFlag);

    /// <summary>
    ///   Parses the syntax element <c>transform_size_8x8_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseTransformSize8x8Flag() => Parse(SyntaxElement.TransformSize8x8Flag);

    /// <summary>
    ///   Parses the syntax element <c>coded_block_flag</c>.
    /// </summary>
    /// <returns>The parsed syntax element.</returns>
    public int ParseCodedBlockFlag()
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (ctxBlockCat < 5)
        {
            return Parse(SyntaxElement.CodedBlockFlag);
        }
        else if (5 < ctxBlockCat && ctxBlockCat < 9)
        {
            return Parse(SyntaxElement.CodedBlockFlag);
        }
        else if (9 < ctxBlockCat && ctxBlockCat < 13)
        {
            return Parse(SyntaxElement.CodedBlockFlag);
        }
        else if (ctxBlockCat is 5 or 9 or 13)
        {
            return Parse(SyntaxElement.CodedBlockFlag);
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
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
        }
        else // field macroblock
        {
            if (ctxBlockCat < 5)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(SyntaxElement.SignificantCoeffFlag);
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
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
        }
        else // field macroblock
        {
            if (ctxBlockCat < 5)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 5)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 9)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
            }
            else if (ctxBlockCat == 13)
            {
                return Parse(SyntaxElement.LastSignificantCoeffFlag);
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
            return Parse(SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (ctxBlockCat == 5)
        {
            return Parse(SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (5 < ctxBlockCat && ctxBlockCat < 9)
        {
            return Parse(SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (9 < ctxBlockCat && ctxBlockCat < 13)
        {
            return Parse(SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (ctxBlockCat == 9)
        {
            return Parse(SyntaxElement.CoeffAbsLevelMinus1);
        }
        else if (ctxBlockCat == 13)
        {
            return Parse(SyntaxElement.CoeffAbsLevelMinus1);
        }

        ThrowInvalidCtxBlockCat();
        return default;
    }

    private static void ThrowInvalidCtxBlockCat()
    {
        throw new InvalidOperationException("Invalid ctxBlockCat");
    }
}
