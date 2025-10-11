namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record SvcRbspNalUnitHeaderSvcExtension(
        [property: UDescriptor("1")] bool IdrFlag,
        [property: UDescriptor("6")] uint PriorityId,
        [property: UDescriptor("1")] bool NoInterLayerPredFlag,
        [property: UDescriptor("3")] uint DependencyId,
        [property: UDescriptor("4")] uint QualityId,
        [property: UDescriptor("3")] uint TemporalId,
        [property: UDescriptor("1")] bool UseRefBasePicFlag,
        [property: UDescriptor("1")] bool DiscardableFlag,
        [property: UDescriptor("1")] bool OutputFlag,
        [property: UDescriptor("2")] uint ReservedThree2Bits
    );
}
