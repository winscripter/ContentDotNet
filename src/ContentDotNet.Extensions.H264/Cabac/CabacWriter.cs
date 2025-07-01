using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   A CABAC writer.
/// </summary>
public sealed class CabacWriter
{
    private const int TotalCabacContexts = 1024;

    private readonly bool[] _init = new bool[TotalCabacContexts]; // false by default
    private readonly CabacContext[] _cabacs = new CabacContext[TotalCabacContexts];
    private readonly ArithmeticEncoder _arithmeticEncodingEngine;

    private readonly BitStreamWriter _boundReader;

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacWriter"/> class.
    /// </summary>
    /// <param name="boundWriter">The <see cref="BitStreamWriter"/> to use for writing CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    public CabacWriter(BitStreamWriter boundWriter, IMacroblockUtility util)
    {
        _boundReader = boundWriter;
        Utility = util;
        _arithmeticEncodingEngine = new ArithmeticEncoder(boundWriter);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacWriter"/> class.
    /// </summary>
    /// <param name="boundWriter">The <see cref="BitStreamWriter"/> to use for writing CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    /// <param name="encoder">The arithmetic encoder.</param>
    public CabacWriter(BitStreamWriter boundWriter, IMacroblockUtility util, ArithmeticEncoder encoder)
    {
        _boundReader = boundWriter;
        Utility = util;
        _arithmeticEncodingEngine = encoder;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacWriter"/> class.
    /// </summary>
    /// <param name="boundWriter">The <see cref="BitStreamWriter"/> to use for writing CABAC bits.</param>
    /// <param name="util">The <see cref="IMacroblockUtility"/> implementation for macroblock operations.</param>
    /// <param name="codIRange">CodIRange</param>
    /// <param name="codILow">CodILow</param>
    public CabacWriter(BitStreamWriter boundWriter, IMacroblockUtility util, uint codILow, uint codIRange)
        : this(boundWriter, util, new ArithmeticEncoder(boundWriter, codILow, codIRange))
    {
    }

    /// <summary>
    ///   Gets the arithmetic encoding engine.
    /// </summary>
    public ArithmeticEncoder Encoder => _arithmeticEncodingEngine;

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
            _arithmeticEncodingEngine.PreviouslyWrittenBins,
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

    private void Write(SyntaxElement se, int value)
    {
        InitializeOrUpdate(se);
        var (ctxIdx, _) = GetCtxIdxAndBypassFlag(se);
        CabacBinarizationEncoder.Encode(_arithmeticEncodingEngine, ref _cabacs[ctxIdx], SliceType, se, value);
    }

    /// <summary>
    ///   Writes the syntax element <c>mb_type</c>.
    /// </summary>
    public void WriteMbType(int value) => Write(SyntaxElement.MacroblockType, value);

    /// <summary>
    ///   Writes the syntax element <c>mb_skip_flag</c>.
    /// </summary>
    public void WriteMbSkipFlag(int value) => Write(SyntaxElement.MacroblockSkipFlag, value);

    /// <summary>
    ///   Writes the syntax element <c>sub_mb_type</c>.
    /// </summary>
    public void WriteSubMbType(int value) => Write(SyntaxElement.SubMacroblockType, value);

    /// <summary>
    ///   Writes the syntax element <c>mvd_l0</c>.
    /// </summary>
    public void WriteMvdL0(int value) => Write(SyntaxElement.MotionVectorDifferenceX, value);

    /// <summary>
    ///   Writes the syntax element <c>mvd_l1</c>.
    /// </summary>
    public void WriteMvdL1(int value) => Write(SyntaxElement.MotionVectorDifferenceY, value);

    /// <summary>
    ///   Writes the syntax element <c>ref_idx_LX</c> where LX can be L0 or L1.
    /// </summary>
    public void WriteRefIdxLX(int value) => Write(SyntaxElement.ReferenceIndex, value);

    /// <summary>
    ///   Writes the syntax element <c>mb_qp_delta</c>.
    /// </summary>
    public void WriteMbQpDelta(int value) => Write(SyntaxElement.MacroblockQuantizationParameterDelta, value);

    /// <summary>
    ///   Writes the syntax element <c>intra_chroma_pred_mode</c>.
    /// </summary>
    public void WriteIntraChromaPredMode(int value) => Write(SyntaxElement.IntraChromaPredictionMode, value);

    /// <summary>
    ///   Writes the syntax element <c>prev_intra_NxN_pred_mode_flag</c> where N can be 4 or 8.
    /// </summary>
    public void WritePrevIntraNxNPredModeFlag(int value) => Write(SyntaxElement.PreviousIntraNxNPredictionModeFlag, value);

    /// <summary>
    ///   Writes the syntax element <c>rem_intra_NxN_pred_mode</c> where N can be 4 or 8.
    /// </summary>
    public void WriteRemIntraNxNPredMode(int value) => Write(SyntaxElement.RemainingIntraNxNPredictionMode, value);

    /// <summary>
    ///   Writes the syntax element <c>mb_field_decoding_flag</c>.
    /// </summary>
    public void WriteMacroblockFieldDecodingFlag(int value) => Write(SyntaxElement.MacroblockFieldDecodingFlag, value);

    /// <summary>
    ///   Writes the syntax element <c>coded_block_pattern</c>.
    /// </summary>
    public void WriteCodedBlockPattern(int value) => Write(SyntaxElement.CodedBlockPattern, value);

    /// <summary>
    ///   Writes the syntax element <c>coeff_sign_flag</c>.
    /// </summary>
    public void WriteCoeffSignFlag(int value) => Write(SyntaxElement.CoeffSignFlag, value);

    /// <summary>
    ///   Writes the syntax element <c>end_of_slice_flag</c>.
    /// </summary>
    public void WriteEndOfSliceFlag(int value) => Write(SyntaxElement.EndOfSliceFlag, value);

    /// <summary>
    ///   Writes the syntax element <c>transform_size_8x8_flag</c>.
    /// </summary>
    public void WriteTransformSize8x8Flag(int value) => Write(SyntaxElement.TransformSize8x8Flag, value);

    /// <summary>
    ///   Writes the syntax element <c>coded_block_flag</c>.
    /// </summary>
    public void WriteCodedBlockFlag(int value)
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (ctxBlockCat < 5)
        {
            Write(SyntaxElement.CodedBlockFlag, value);
        }
        else if (5 < ctxBlockCat && ctxBlockCat < 9)
        {
            Write(SyntaxElement.CodedBlockFlag, value);
        }
        else if (9 < ctxBlockCat && ctxBlockCat < 13)
        {
            Write(SyntaxElement.CodedBlockFlag, value);
        }
        else if (ctxBlockCat is 5 or 9 or 13)
        {
            Write(SyntaxElement.CodedBlockFlag, value);
        }

        ThrowInvalidCtxBlockCat();
    }

    /// <summary>
    ///   Writes the syntax element <c>significant_coeff_flag</c>.
    /// </summary>
    public void WriteSignificantCoeffFlag(int value)
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (IsFrameMacroblock)
        {
            if (ctxBlockCat < 5)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 5)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 9)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 13)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
        }
        else // field macroblock
        {
            if (ctxBlockCat < 5)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 5)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 9)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 13)
            {
                Write(SyntaxElement.SignificantCoeffFlag, value);
            }
        }

        ThrowInvalidCtxBlockCat();
    }

    /// <summary>
    ///   Writes the syntax element <c>last_significant_coeff_flag</c>.
    /// </summary>
    public void WriteLastSignificantCoeffFlag(int value)
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (IsFrameMacroblock)
        {
            if (ctxBlockCat < 5)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 5)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 9)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 13)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
        }
        else // field macroblock
        {
            if (ctxBlockCat < 5)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 5)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (5 < ctxBlockCat && ctxBlockCat < 9)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (9 < ctxBlockCat && ctxBlockCat < 13)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 9)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
            else if (ctxBlockCat == 13)
            {
                Write(SyntaxElement.LastSignificantCoeffFlag, value);
            }
        }

        ThrowInvalidCtxBlockCat();
    }

    /// <summary>
    ///   Writes the syntax element <c>coeff_abs_level_minus1</c>.
    /// </summary>
    public void WriteCoeffAbsLevelMinus1(int value)
    {
        (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(BlockType, NumC8x8);

        if (ctxBlockCat < 5)
        {
            Write(SyntaxElement.CoeffAbsLevelMinus1, value);
        }
        else if (ctxBlockCat == 5)
        {
            Write(SyntaxElement.CoeffAbsLevelMinus1, value);
        }
        else if (5 < ctxBlockCat && ctxBlockCat < 9)
        {
            Write(SyntaxElement.CoeffAbsLevelMinus1, value);
        }
        else if (9 < ctxBlockCat && ctxBlockCat < 13)
        {
            Write(SyntaxElement.CoeffAbsLevelMinus1, value);
        }
        else if (ctxBlockCat == 9)
        {
            Write(SyntaxElement.CoeffAbsLevelMinus1, value);
        }
        else if (ctxBlockCat == 13)
        {
            Write(SyntaxElement.CoeffAbsLevelMinus1, value);
        }

        ThrowInvalidCtxBlockCat();
    }

    private static void ThrowInvalidCtxBlockCat()
    {
        throw new InvalidOperationException("Invalid ctxBlockCat");
    }
}
