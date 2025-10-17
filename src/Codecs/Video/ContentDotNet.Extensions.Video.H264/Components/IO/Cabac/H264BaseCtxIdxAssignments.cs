namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using static ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel.Expressions.ContextIndexFactoryExpressions;

    /// <summary>
    ///   Assign base ctxIdxOffset and maxBinIdxCtx values prior to binarization.
    /// </summary>
    public static class H264BaseCtxIdxAssignments
    {
        private static readonly ContextIndexAndParser mb_type_SI = new(R(A(0, 6), A(0, 3)), (x, y, z) => H264Binarization.MbType(x, H264SliceType.SI, false));
        private static readonly ContextIndexAndParser mb_type_I = new(R(I(6), I(3)), (x, y, z) => H264Binarization.MbType(x, H264SliceType.I, false));
        private static readonly ContextIndexAndParser mb_skip_flag_PSP = new(R(I(0), I(11)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser mb_type_PSP = new(R(A(2, 5), A(14, 17)), (x, y, z) => H264Binarization.MbType(x, z, false));
        private static readonly ContextIndexAndParser sub_mb_type_PSP = new(R(I(2), I(21)), (x, y, z) => H264Binarization.MbType(x, z, true));
        private static readonly ContextIndexAndParser mb_skip_flag_B = new(R(I(0), I(24)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser mb_type_B = new(R(A(3, 5), A(27, 32)), (x, y, z) => H264Binarization.MbType(x, H264SliceType.B, false));
        private static readonly ContextIndexAndParser sub_mb_type_B = new(R(I(3), I(36)), (x, y, z) => H264Binarization.MbType(x, H264SliceType.B, true));
        private static readonly ContextIndexAndParser mvd_lx_0 = new(R(N(A(4, 0)), B(N(A(40, 0)))), (x, y, z) => H264Binarization.Uegk(x, true, 9, 3));
        private static readonly ContextIndexAndParser mvd_lx_1 = new(R(N(A(4, 0)), B(N(A(47, 0)))), (x, y, z) => H264Binarization.Uegk(x, true, 9, 3));
        private static readonly ContextIndexAndParser ref_idx_lx = new(R(I(2), I(54)), (x, y, z) => H264Binarization.U(x));
        private static readonly ContextIndexAndParser mb_qp_delta = new(R(I(2), I(60)), (x, y, z) => H264Binarization.MbQpDelta(x));
        private static readonly ContextIndexAndParser intra_chroma_pred_mode = new(R(I(1), I(64)), (x, y, z) => H264Binarization.TU(x, 3).Value);
        private static readonly ContextIndexAndParser prev_intranxn_pred_mode_flag = new(R(I(0), I(68)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser rem_intranxn_pred_mode_flag = new(R(I(0), I(69)), (x, y, z) => H264Binarization.FL(x, 7));
        private static readonly ContextIndexAndParser mb_field_decoding_flag = new(R(I(0), I(70)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser coded_block_pattern = new(R(A(3, 1), A(73, 77)), (x, y, z) => H264Binarization.CodedBlockPattern(x, x.DecodingVariables.ChromaArrayType));
        private static readonly ContextIndexAndParser coeff_sign_flag = new(R(I(0), N(B(I(0)))), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser end_of_sice_flag = new(R(I(0), I(276)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser transform_size_8x8_flag = new(R(I(0), I(399)), (x, y, z) => H264Binarization.FL(x, 1));

        private static readonly ContextIndexAndParser coded_block_flag_cbc_lt_5 = new(R(I(0), I(85)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser coded_block_flag_5_lt_cbc_lt_9 = new(R(I(0), I(460)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser coded_block_flag_9_lt_cbc_lt_13 = new(R(I(0), I(472)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser coded_block_flag_cbc_is_5_9_13 = new(R(I(0), I(1012)), (x, y, z) => H264Binarization.FL(x, 1));

        private static readonly ContextIndexAndParser significant_coeff_flag_frame_cbc_lt_5 = new(R(I(0), I(105)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_field_cbc_lt_5 = new(R(I(0), I(277)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_frame_cbc_eq_5 = new(R(I(0), I(402)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_field_cbc_eq_5 = new(R(I(0), I(436)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_frame_5_lt_cbc_lt_9 = new(R(I(0), I(484)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_frame_9_lt_cbc_lt_13 = new(R(I(0), I(528)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_field_5_lt_cbc_lt_9 = new(R(I(0), I(776)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_field_9_lt_cbc_lt_13 = new(R(I(0), I(820)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_frame_cbc_eq_9 = new(R(I(0), I(660)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_frame_cbc_eq_13 = new(R(I(0), I(718)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_field_cbc_eq_9 = new(R(I(0), I(675)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser significant_coeff_flag_field_cbc_eq_13 = new(R(I(0), I(733)), (x, y, z) => H264Binarization.FL(x, 1));

        private static readonly ContextIndexAndParser last_significant_coeff_flag_frame_cbc_lt_5 = new(R(I(0), I(166)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_field_cbc_lt_5 = new(R(I(0), I(338)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_frame_cbc_eq_5 = new(R(I(0), I(417)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_field_cbc_eq_5 = new(R(I(0), I(451)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_frame_5_lt_cbc_lt_9 = new(R(I(0), I(572)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_frame_9_lt_cbc_lt_13 = new(R(I(0), I(616)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_field_5_lt_cbc_lt_9 = new(R(I(0), I(864)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_field_9_lt_cbc_lt_13 = new(R(I(0), I(908)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_frame_cbc_eq_9 = new(R(I(0), I(690)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_frame_cbc_eq_13 = new(R(I(0), I(748)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_field_cbc_eq_9 = new(R(I(0), I(699)), (x, y, z) => H264Binarization.FL(x, 1));
        private static readonly ContextIndexAndParser last_significant_coeff_flag_field_cbc_eq_13 = new(R(I(0), I(757)), (x, y, z) => H264Binarization.FL(x, 1));

        private static readonly ContextIndexAndParser coeff_abs_level_minus1_cbc_lt_5 = new(R(N(A(1, 0)), B(N(A(227, 0)))), (x, y, z) => H264Binarization.Uegk(x, false, 14, 0));
        private static readonly ContextIndexAndParser coeff_abs_level_minus1_cbc_eq_5 = new(R(N(A(1, 0)), B(N(A(426, 0)))), (x, y, z) => H264Binarization.Uegk(x, false, 14, 0));
        private static readonly ContextIndexAndParser coeff_abs_level_minus1_cbc_5_lt_cbc_lt_9 = new(R(N(A(1, 0)), B(N(A(952, 0)))), (x, y, z) => H264Binarization.Uegk(x, false, 14, 0));
        private static readonly ContextIndexAndParser coeff_abs_level_minus1_cbc_9_lt_cbc_lt_13 = new(R(N(A(1, 0)), B(N(A(982, 0)))), (x, y, z) => H264Binarization.Uegk(x, false, 14, 0));
        private static readonly ContextIndexAndParser coeff_abs_level_minus1_cbc_eq_9 = new(R(N(A(1, 0)), B(N(A(708, 0)))), (x, y, z) => H264Binarization.Uegk(x, false, 14, 0));
        private static readonly ContextIndexAndParser coeff_abs_level_minus1_cbc_eq_13 = new(R(N(A(1, 0)), B(N(A(766, 0)))), (x, y, z) => H264Binarization.Uegk(x, false, 14, 0));
    
        /// <summary>
        ///   Assigns a parser with ctxIdx.
        /// </summary>
        /// <param name="se"></param>
        /// <param name="ctxBlockCat"></param>
        /// <param name="isFrameMacroblock"></param>
        /// <param name="sliceType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ContextIndexAndParser? GetParserWithCtxIdx(H264SyntaxElement se, int ctxBlockCat, bool isFrameMacroblock, H264SliceType sliceType)
        {
            return se switch
            {
                H264SyntaxElement.MacroblockType => sliceType switch
                {
                    H264SliceType.SI => mb_type_SI,
                    H264SliceType.I => mb_type_I,
                    H264SliceType.P or H264SliceType.SP => mb_type_PSP,
                    H264SliceType.B => mb_type_B,
                    _ => null
                },
                H264SyntaxElement.MacroblockSkipFlag => sliceType switch
                {
                    H264SliceType.P or H264SliceType.SP => mb_skip_flag_PSP,
                    H264SliceType.B => mb_skip_flag_B,
                    _ => throw new InvalidOperationException($"{sliceType} is not a valid slice type for MacroblockSkipFlag; expected P, SP or B")
                },
                H264SyntaxElement.SubMacroblockType => sliceType switch
                {
                    H264SliceType.P or H264SliceType.SP => sub_mb_type_PSP,
                    H264SliceType.B => sub_mb_type_B,
                    _ => null
                },
                H264SyntaxElement.MotionVectorDifferenceX => mvd_lx_0,
                H264SyntaxElement.MotionVectorDifferenceY => mvd_lx_1,
                H264SyntaxElement.ReferenceIndex => ref_idx_lx,
                H264SyntaxElement.MacroblockQuantizationParameterDelta => mb_qp_delta,
                H264SyntaxElement.IntraChromaPredictionMode => intra_chroma_pred_mode,
                H264SyntaxElement.PreviousIntraNxNPredictionModeFlag => prev_intranxn_pred_mode_flag,
                H264SyntaxElement.RemainingIntraNxNPredictionMode => rem_intranxn_pred_mode_flag,
                H264SyntaxElement.MacroblockFieldDecodingFlag => mb_field_decoding_flag,
                H264SyntaxElement.CodedBlockPattern => coded_block_pattern,
                H264SyntaxElement.CoeffSignFlag => coeff_sign_flag,
                H264SyntaxElement.EndOfSliceFlag => end_of_sice_flag,
                H264SyntaxElement.TransformSize8x8Flag => transform_size_8x8_flag,
                H264SyntaxElement.CodedBlockFlag => ctxBlockCat switch
                {
                    < 5 => coded_block_flag_cbc_lt_5,
                    > 5 and < 9 => coded_block_flag_5_lt_cbc_lt_9,
                    > 9 and < 13 => coded_block_flag_cbc_is_5_9_13,
                    < 13 => coded_block_flag_9_lt_cbc_lt_13,
                    _ => null
                },
                H264SyntaxElement.SignificantCoeffFlag => (isFrameMacroblock, ctxBlockCat) switch
                {
                    (false, > 5 and < 9) => significant_coeff_flag_field_5_lt_cbc_lt_9,
                    (false, > 9 and < 13) => significant_coeff_flag_field_9_lt_cbc_lt_13,
                    (false, 13) => significant_coeff_flag_field_cbc_eq_13,
                    (false, 5) => significant_coeff_flag_field_cbc_eq_5,
                    (false, 9) => significant_coeff_flag_field_cbc_eq_9,
                    (false, < 5) => significant_coeff_flag_field_cbc_lt_5,
                    (true, > 5 and < 9) => significant_coeff_flag_frame_5_lt_cbc_lt_9,
                    (true, > 9 and < 13) => significant_coeff_flag_frame_9_lt_cbc_lt_13,
                    (true, 13) => significant_coeff_flag_frame_cbc_eq_13,
                    (true, 5) => significant_coeff_flag_frame_cbc_eq_5,
                    (true, 9) => significant_coeff_flag_frame_cbc_eq_9,
                    (true, < 5) => significant_coeff_flag_frame_cbc_lt_5,
                    _ => null
                },
                H264SyntaxElement.LastSignificantCoeffFlag => (isFrameMacroblock, ctxBlockCat) switch
                {
                    (false, > 5 and < 9) => last_significant_coeff_flag_field_5_lt_cbc_lt_9,
                    (false, > 9 and < 13) => last_significant_coeff_flag_field_9_lt_cbc_lt_13,
                    (false, 13) => last_significant_coeff_flag_field_cbc_eq_13,
                    (false, 5) => last_significant_coeff_flag_field_cbc_eq_5,
                    (false, 9) => last_significant_coeff_flag_field_cbc_eq_9,
                    (false, < 5) => last_significant_coeff_flag_field_cbc_lt_5,
                    (true, > 5 and < 9) => last_significant_coeff_flag_frame_5_lt_cbc_lt_9,
                    (true, > 9 and < 13) => last_significant_coeff_flag_frame_9_lt_cbc_lt_13,
                    (true, 13) => last_significant_coeff_flag_frame_cbc_eq_13,
                    (true, 5) => last_significant_coeff_flag_frame_cbc_eq_5,
                    (true, 9) => last_significant_coeff_flag_frame_cbc_eq_9,
                    (true, < 5) => last_significant_coeff_flag_frame_cbc_lt_5,
                    _ => null
                },
                H264SyntaxElement.CoeffAbsLevelMinus1 => ctxBlockCat switch
                {
                    > 5 and < 9 => coeff_abs_level_minus1_cbc_5_lt_cbc_lt_9,
                    > 9 and < 13 => coeff_abs_level_minus1_cbc_9_lt_cbc_lt_13,
                    13 => coeff_abs_level_minus1_cbc_eq_13,
                    5 => coeff_abs_level_minus1_cbc_eq_5,
                    9 => coeff_abs_level_minus1_cbc_eq_9,
                    < 5 => coeff_abs_level_minus1_cbc_lt_5,
                    _ => null
                },
                _ => throw new NotImplementedException($"Syntax element {se} not implemented")
            };
        }
    }
}
