namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record SvcRbspPrefixNalUnitSvc(
        [property: UDescriptor("1")] bool StoreRefBasePicFlag,
        SvcRbspDecRefBasePicMarking? DecRefBasePicMarking,
        [property: UDescriptor("1")] bool AdditionalPrefixNalUnitExtensionFlag,
        List<bool>? AdditionalPrefixNalUnitExtensionDataFlags
    );
}
