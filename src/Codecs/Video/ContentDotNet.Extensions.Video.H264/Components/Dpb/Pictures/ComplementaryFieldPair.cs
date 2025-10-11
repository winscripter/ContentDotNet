namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    /// <summary>
    ///   Abstracts a complementary field pair.
    /// </summary>
    public abstract class ComplementaryFieldPair : DpbPicture
    {
        /// <summary>
        ///   The top picture.
        /// </summary>
        public abstract PictureDescriptor Top { get; }

        /// <summary>
        ///   The bottom picture.
        /// </summary>
        public abstract PictureDescriptor Bottom { get; }
    }
}
