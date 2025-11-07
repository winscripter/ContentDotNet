namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record RbspSliceDataPartitionALayer(
        RbspSliceHeader SliceHeader,
        [property: UeDescriptor] uint SliceId
        // rbsp_slice_trailing_bits() handled externally
    );
}
