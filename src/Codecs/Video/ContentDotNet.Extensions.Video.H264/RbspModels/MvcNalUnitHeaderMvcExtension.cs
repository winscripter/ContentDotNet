namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record MvcNalUnitHeaderMvcExtension(
        [property: UDescriptor("1")] bool NonIdrFlag,
        [property: UDescriptor("6")] uint PriorityId,
        [property: UDescriptor("10")] uint ViewId,
        [property: UDescriptor("3")] uint TemporalId,
        [property: UDescriptor("1")] bool AnchorPicFlag,
        [property: UDescriptor("1")] bool InterViewFlag,
        [property: UDescriptor("1")] bool ReservedOneBit
    );
}
