namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   The field picture
    /// </summary>
    public abstract class FieldPicture : DpbPicture
    {
        /// <summary>
        ///   The picture side.
        /// </summary>
        public abstract H264PictureSide Side { get; set; }

        /// <summary>
        ///   The actual pixture.
        /// </summary>
        public abstract FramePicture Picture { get; set; }
    }
}
