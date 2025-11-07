namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record RbspSliceDataPartitionBLayer(
        [property: UeDescriptor] uint SliceId,
        // if (separate_colour_plane_flag == 1)
        [property: UDescriptor("2")] uint? ColourPlaneId,
        // if (redundant_pic_cnt_present_flag)
        [property: UeDescriptor] uint? RedundantPicCnt
        // rbsp_slice_trailing_bits() handled externally
    );
}
