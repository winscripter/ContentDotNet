namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record RbspAccessUnitDelimiter(
        [property: UDescriptor("3")] uint PrimaryPicType
    );
}
