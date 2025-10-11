namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks;

    /// <summary>
    ///   DPB picture factory
    /// </summary>
    public interface IDpbPictureFactory
    {
        /// <summary>
        ///   Creates a complementary field picture.
        /// </summary>
        /// <param name="top">Top picture.</param>
        /// <param name="bottom">Bottom picture.</param>
        /// <param name="state">Associated H.264 state.</param>
        /// <returns>The complementary field picture.</returns>
        ComplementaryFieldPair CreateComplementaryFieldPair(PictureDescriptor top, PictureDescriptor bottom, H264State? state);

        /// <summary>
        ///   Creates a field picture.
        /// </summary>
        /// <param name="side">The picture side.</param>
        /// <param name="picture">The actual picture.</param>
        /// <param name="state">Associated H.264 state.</param>
        /// <returns>The field picture.</returns>
        FieldPicture CreateField(H264PictureSide side, FramePicture picture, H264State state);

        /// <summary>
        ///   Creates a frame picture.
        /// </summary>
        /// <param name="picture">The picture.</param>
        /// <param name="state">Associated H.264 state.</param>
        /// <returns>The frame picture.</returns>
        FramePicture CreateFrame(H264ReferencePictureImage picture, H264State state);
    }
}
