namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Collections.Bits;
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record RbspHrdParameters(
        [property: UeDescriptor] uint CpbCntMinus1,
        [property: UDescriptor("4")] uint BitRateScale,
        [property: UDescriptor("4")] uint CpbSizeScale,
        uint[] BitRateValueMinus1,
        uint[] CpbSizeValueMinus1,
        IBitCollection CbrFlag,
        [property: UDescriptor("5")] uint InitialCpbRemovalDelayLengthMinus1,
        [property: UDescriptor("5")] uint CpbRemovalDelayLengthMinus1,
        [property: UDescriptor("5")] uint DpbOutputDelayLengthMinus1,
        [property: UDescriptor("5")] uint TimeOffsetLength
    );
}
