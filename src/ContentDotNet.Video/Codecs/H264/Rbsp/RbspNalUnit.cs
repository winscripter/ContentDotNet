namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record RbspNalUnit(
        [property: FDescriptor(1)] bool ForbiddenZeroBit,
        [property: UDescriptor("2")] uint NalRefIdc,
        [property: UDescriptor("5")] uint NalUnitType,
        [property: UDescriptor("1")] bool SvcExtensionFlag,
        [property: UDescriptor("1")] bool Avc3DExtensionFlag,
        SvcRbspNalUnitHeaderSvcExtension? SvcExtension,
        Avc3DNalUnitHeader3DAvcExtension? Avc3DExtension,
        MvcNalUnitHeaderMvcExtension? MvcExtension,
        Stream RbspByte);
}
