namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record RbspSequenceParameterSetExtension(
        [property: UeDescriptor] uint SeqParameterSetId,
        [property: UeDescriptor] uint AuxFormatIdc,
        [property: UeDescriptor] uint? BitDepthAuxMinus8,
        [property: UDescriptor("1")] bool? AlphaIncrFlag,
        [property: UDescriptor("v")] uint? AlphaOpaqueValue,
        [property: UDescriptor("v")] uint? AlphaTransparentValue,
        [property: UDescriptor("1")] bool AdditionalExtensionFlag
    );
}
