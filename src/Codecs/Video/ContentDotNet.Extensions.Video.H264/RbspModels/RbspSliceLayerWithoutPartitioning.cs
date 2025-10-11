namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspSliceLayerWithoutPartitioning(
        RbspSliceHeader SliceHeader
        // rbsp_slice_trailing_bits() not modeled; parser handles alignment
    );
}
