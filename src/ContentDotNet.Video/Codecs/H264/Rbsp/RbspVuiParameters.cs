namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record RbspVuiParameters(
        [property: UDescriptor("1")] bool AspectRatioInfoPresentFlag,
        [property: UDescriptor("8")] uint? AspectRatioIdc,
        [property: UDescriptor("16")] uint? SarWidth,
        [property: UDescriptor("16")] uint? SarHeight,

        [property: UDescriptor("1")] bool OverscanInfoPresentFlag,
        [property: UDescriptor("1")] bool? OverscanAppropriateFlag,

        [property: UDescriptor("1")] bool VideoSignalTypePresentFlag,
        [property: UDescriptor("3")] uint? VideoFormat,
        [property: UDescriptor("1")] bool? VideoFullRangeFlag,
        [property: UDescriptor("1")] bool? ColourDescriptionPresentFlag,
        [property: UDescriptor("8")] uint? ColourPrimaries,
        [property: UDescriptor("8")] uint? TransferCharacteristics,
        [property: UDescriptor("8")] uint? MatrixCoefficients,

        [property: UDescriptor("1")] bool ChromaLocInfoPresentFlag,
        [property: UeDescriptor] uint? ChromaSampleLocTypeTopField,
        [property: UeDescriptor] uint? ChromaSampleLocTypeBottomField,

        [property: UDescriptor("1")] bool TimingInfoPresentFlag,
        [property: UDescriptor("32")] uint? NumUnitsInTick,
        [property: UDescriptor("32")] uint? TimeScale,
        [property: UDescriptor("1")] bool? FixedFrameRateFlag,

        [property: UDescriptor("1")] bool NalHrdParametersPresentFlag,
        RbspHrdParameters? NalHrdParameters,

        [property: UDescriptor("1")] bool VclHrdParametersPresentFlag,
        RbspHrdParameters? VclHrdParameters,

        [property: UDescriptor("1")] bool? LowDelayHrdFlag,
        [property: UDescriptor("1")] bool PicStructPresentFlag,

        [property: UDescriptor("1")] bool BitstreamRestrictionFlag,
        [property: UDescriptor("1")] bool? MotionVectorsOverPicBoundariesFlag,
        [property: UeDescriptor] uint? MaxBytesPerPicDenom,
        [property: UeDescriptor] uint? MaxBitsPerMbDenom,
        [property: UeDescriptor] uint? Log2MaxMvLengthHorizontal,
        [property: UeDescriptor] uint? Log2MaxMvLengthVertical,
        [property: UeDescriptor] uint? MaxNumReorderFrames,
        [property: UeDescriptor] uint? MaxDecFrameBuffering
    );
}
