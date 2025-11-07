namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record SvcRbspPrefixNalUnitSvc(
        [property: UDescriptor("1")] bool StoreRefBasePicFlag,
        SvcRbspDecRefBasePicMarking? DecRefBasePicMarking,
        [property: UDescriptor("1")] bool AdditionalPrefixNalUnitExtensionFlag,
        List<bool>? AdditionalPrefixNalUnitExtensionDataFlags
    );
}
