namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    using ContentDotNet.Colors;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   Abstracts a picture of the Decoded Picture Buffer (DPB).
    /// </summary>
    public abstract class DpbPicture
    {
        /// <summary>
        ///   The state last captured when decoding this picture.
        /// </summary>
        public H264State? State { get; set; }

        /// <summary>
        ///   Returns and caches the picture.
        /// </summary>
        /// <param name="cache">The cache</param>
        /// <param name="pictureFactory">Can be necessary when creating new pictures.</param>
        /// <returns>The picture.</returns>
        public abstract Picture<YCbCr> GetAndCacheRaw(ISinglePictureCache<YCbCr> cache, IPictureFactory? pictureFactory = null);
    }
}
