namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspSliceHeader(
        uint FirstMbInSlice,         // ue(v)
        uint SliceType,         // ue(v)
        uint PicParameterSetId,      // ue(v)

        // conditional: separate_colour_plane_flag == 1
        uint? ColourPlaneId,         // u(2)?

        uint FrameNum,               // u(v)

        // conditional: !frame_mbs_only_flag
        bool? FieldPicFlag,          // u(1)?
                                     // conditional: field_pic_flag == true
        bool? BottomFieldFlag,       // u(1)?

        // conditional: IdrPicFlag
        uint? IdrPicId,              // ue(v)?

        // conditional: pic_order_cnt_type == 0
        uint? PicOrderCntLsb,        // u(v)?
                                     // conditional: bottom_field_pic_order_in_frame_present_flag && !field_pic_flag
        int? DeltaPicOrderCntBottom, // se(v)?

        // conditional: pic_order_cnt_type == 1 && !delta_pic_order_always_zero_flag
        int[]? DeltaPicOrderCnt,     // se(v)[1 or 2], see below

        // conditional: redundant_pic_cnt_present_flag
        uint? RedundantPicCnt,       // ue(v)?

        // conditional: slice_type == B
        bool? DirectSpatialMvPredFlag, // u(1)?

        // conditional: slice_type in {P, SP, B}
        bool? NumRefIdxActiveOverrideFlag,  // u(1)?
                                            // conditional: num_ref_idx_active_override_flag == true
        uint? NumRefIdxL0ActiveMinus1,       // ue(v)?
                                             // conditional: slice_type == B && num_ref_idx_active_override_flag == true
        uint? NumRefIdxL1ActiveMinus1,       // ue(v)?

        // conditional: nal_unit_type == 20 || nal_unit_type == 21
        MvcRbspRefPicListMvcModification? RefPicListMvcModification,
        // else
        RbspRefPicListModification? RefPicListModification,

        // conditional: weighted_pred_flag && (slice_type in {P, SP}) || (weighted_bipred_idc == 1 && slice_type == B)
        RbspPredWeightTable? PredWeightTable,

        // conditional: nal_ref_idc != 0
        RbspDecRefPicMarking? DecRefPicMarking,

        // conditional: entropy_coding_mode_flag && slice_type not in {I, SI}
        uint? CabacInitIdc,                  // ue(v)?

        int SliceQpDelta,                   // se(v)

        // conditional: slice_type in {SP, SI}
        bool? SpForSwitchFlag,               // u(1)? only if slice_type == SP
        int? SliceQsDelta,                   // se(v)?

        // conditional: deblocking_filter_control_present_flag
        uint? DisableDeblockingFilterIdc,   // ue(v)?
                                            // conditional: disable_deblocking_filter_idc != 1
        int? SliceAlphaC0OffsetDiv2,         // se(v)?
        int? SliceBetaOffsetDiv2,            // se(v)?

        // conditional: num_slice_groups_minus1 > 0 && slice_group_map_type in [3..5]
        uint? SliceGroupChangeCycle          // u(v)?
    );
}
