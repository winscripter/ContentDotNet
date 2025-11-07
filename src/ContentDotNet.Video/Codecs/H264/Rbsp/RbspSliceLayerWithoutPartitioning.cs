namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    public record RbspSliceLayerWithoutPartitioning(
        RbspSliceHeader SliceHeader
        // rbsp_slice_trailing_bits() not modeled; parser handles alignment
    );
}
