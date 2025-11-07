namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record RbspPictureParameterSet(
        [property: UeDescriptor] uint PicParameterSetId,
        [property: UeDescriptor] uint SeqParameterSetId,
        [property: UDescriptor("1")] bool EntropyCodingModeFlag,
        [property: UDescriptor("1")] bool BottomFieldPicOrderInFramePresentFlag,
        [property: UeDescriptor] uint NumSliceGroupsMinus1,

        // Slice Group Info (conditionally present)
        [property: UeDescriptor] uint? SliceGroupMapType,
        [property: UeDescriptor] uint[]? RunLengthMinus1,
        [property: UeDescriptor] uint[]? TopLeft,
        [property: UeDescriptor] uint[]? BottomRight,
        [property: UDescriptor("1")] bool? SliceGroupChangeDirectionFlag,
        [property: UeDescriptor] uint? SliceGroupChangeRateMinus1,
        [property: UeDescriptor] uint? PicSizeInMapUnitsMinus1,
        [property: UDescriptor("v")] uint[]? SliceGroupId,

        // Reference index defaults
        [property: UeDescriptor] uint NumRefIdxL0DefaultActiveMinus1,
        [property: UeDescriptor] uint NumRefIdxL1DefaultActiveMinus1,

        // Prediction flags
        [property: UDescriptor("1")] bool WeightedPredFlag,
        [property: UDescriptor("2")] uint WeightedBiPredIdc,

        // QP/offsets
        [property: SeDescriptor] int PicInitQpMinus26,
        [property: SeDescriptor] int PicInitQsMinus26,
        [property: SeDescriptor] int ChromaQpIndexOffset,

        // Misc flags
        [property: UDescriptor("1")] bool DeblockingFilterControlPresentFlag,
        [property: UDescriptor("1")] bool ConstrainedIntraPredFlag,
        [property: UDescriptor("1")] bool RedundantPicCntPresentFlag,

        // Extra RBSP data (conditionally present if more_rbsp_data())
        [property: UDescriptor("1")] bool? Transform8x8ModeFlag,
        [property: UDescriptor("1")] bool? PicScalingMatrixPresentFlag,
        [property: UDescriptor("1")] bool[]? PicScalingListPresentFlag,
        List<List<int>> ScalingList4x4,
        List<List<int>> ScalingList8x8,
        bool[] UseDefaultScalingMatrixFlag4x4,
        bool[] UseDefaultScalingMatrixFlag8x8,

        [property: SeDescriptor] int? SecondChromaQpIndexOffset
    );
}
