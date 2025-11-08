namespace ContentDotNet.Video
{
    using ContentDotNet.Api;
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.Api.Colors;
    using ContentDotNet.Api.Pictures;

    /// <summary>
    ///   Decoder for video codecs.
    /// </summary>
    public interface IVideoCodecDecoder<TPixelFormat> where TPixelFormat : unmanaged, IColor
    {
        /// <summary>
        ///   The configuration.
        /// </summary>
        Configuration Configuration { get; set; }

        /// <summary>
        ///   Is async supported?
        /// </summary>
        bool SupportsAsync { get; }

        /// <summary>
        ///   Codec technical name (i.e. H264)
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Actual codec name (i.e. H.264 or ITU-T H.264)
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///   The backing bit-stream.
        /// </summary>
        BitStreamReader BitStream { get; }

        /// <summary>
        ///   Decodes the picture.
        /// </summary>
        /// <returns>The picture.</returns>
        Picture<TPixelFormat> DecodePicture();

        /// <summary>
        ///   Asynchronously decodes the picture.
        /// </summary>
        /// <returns>The decoded picture.</returns>
        Task<Picture<TPixelFormat>> DecodePictureAsync();
    }
}
