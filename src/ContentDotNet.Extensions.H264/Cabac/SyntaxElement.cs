namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Represents the H.264 syntax element identifier.
/// </summary>
public enum SyntaxElement : byte
{
    /// <summary>
    ///   <c>mb_type</c>
    /// </summary>
    MacroblockType = 0,

    /// <summary>
    ///   <c>mb_skip_flag</c>
    /// </summary>
    MacroblockSkipFlag = 1,

    /// <summary>
    ///   <c>sub_mb_type</c>
    /// </summary>
    SubMacroblockType = 2,

    /// <summary>
    ///   <c>mvd_l0</c> and <c>mvd_l1</c>
    /// </summary>
    MotionVectorDifferenceX = 3,

    /// <summary>
    ///   <c>mvd_l0</c> and <c>mvd_l1</c>
    /// </summary>
    MotionVectorDifferenceY = 4,

    /// <summary>
    ///   <c>ref_idx_l0</c> and <c>ref_idx_l1</c>
    /// </summary>
    ReferenceIndex = 5,

    /// <summary>
    ///   <c>mb_qp_delta</c>
    /// </summary>
    MacroblockQuantizationParameterDelta,

    /// <summary>
    ///   <c>intra_chroma_pred_mode</c>
    /// </summary>
    IntraChromaPredictionMode,

    /// <summary>
    ///   <c>prev_intra4x4_pred_mode_flag</c> and <c>prev_intra8x8_pred_mode_flag</c>
    /// </summary>
    PreviousIntraNxNPredictionModeFlag,

    /// <summary>
    ///   <c>rem_intra4x4_pred_mode</c> and <c>rem_intra8x8_pred_mode</c>
    /// </summary>
    RemainingIntraNxNPredictionMode,

    /// <summary>
    ///   <c>mb_field_decoding_flag</c>
    /// </summary>
    MacroblockFieldDecodingFlag,

    /// <summary>
    ///   <c>coded_block_pattern</c>
    /// </summary>
    CodedBlockPattern,

    /// <summary>
    ///   <c>coded_block_flag</c>
    /// </summary>
    CodedBlockFlag,

    /// <summary>
    ///   <c>significant_coeff_flag</c>
    /// </summary>
    SignificantCoeffFlag,

    /// <summary>
    ///   <c>last_significant_coeff_flag</c>
    /// </summary>
    LastSignificantCoeffFlag,

    /// <summary>
    ///   <c>coeff_abs_level_minus1</c>
    /// </summary>
    CoeffAbsLevelMinus1,

    /// <summary>
    ///   <c>coeff_sign_flag</c>
    /// </summary>
    CoeffSignFlag,

    /// <summary>
    ///   <c>transform_size_8x8_flag</c>
    /// </summary>
    TransformSize8x8Flag,

    /// <summary>
    ///   <c>end_of_slice_flag</c>
    /// </summary>
    EndOfSliceFlag,
}
