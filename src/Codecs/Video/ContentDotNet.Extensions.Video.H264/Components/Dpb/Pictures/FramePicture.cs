namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    using ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks;

    /// <summary>
    ///   DPB frame picture
    /// </summary>
    public abstract class FramePicture : DpbPicture
    {
        /// <summary>
        ///   The picture.
        /// </summary>
        public abstract H264ReferencePictureImage Picture { get; }
    }
}
