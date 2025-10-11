namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record Avc3DNalUnitHeader3DAvcExtension(
        [property: UDescriptor("8")] uint ViewIdx,
        [property: UDescriptor("1")] bool DepthFlag,
        [property: UDescriptor("1")] bool NonIdrFlag,
        [property: UDescriptor("3")] uint TemporalId,
        [property: UDescriptor("1")] bool AnchorPicFlag,
        [property: UDescriptor("1")] bool InterViewFlag
    );
}
