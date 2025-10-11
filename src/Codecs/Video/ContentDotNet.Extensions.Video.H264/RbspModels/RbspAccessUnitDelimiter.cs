namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record RbspAccessUnitDelimiter(
        [property: UDescriptor("3")] uint PrimaryPicType
    );
}
