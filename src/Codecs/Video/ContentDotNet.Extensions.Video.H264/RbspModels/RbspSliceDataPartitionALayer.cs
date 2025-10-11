namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record RbspSliceDataPartitionALayer(
        RbspSliceHeader SliceHeader,
        [property: UeDescriptor] uint SliceId
        // rbsp_slice_trailing_bits() handled externally
    );
}
