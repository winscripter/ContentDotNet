namespace ContentDotNet.Abstractions
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Colors;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   Abstracts a video codec decoder.
    /// </summary>
    public interface IVideoCodecDecoder<TPixel> where TPixel : unmanaged, IColor
    {
        /// <summary>
        ///   Codec name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///   Codec name displayed to the UI.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        ///   Current index of a frame.
        /// </summary>
        long CurrentFrameIndex { get; set; }

        /// <summary>
        ///   Backing bitstream. Changing positions could corrupt data read by the codec.
        /// </summary>
        BitStreamReader BitStreamReader { get; set; }

        /// <summary>
        ///   The configuration.
        /// </summary>
        Configuration Configuration { get; set; }

        /// <summary>
        ///   Reads the next picture.
        /// </summary>
        /// <typeparam name="TPixel">Pixel</typeparam>
        /// <returns>The picture</returns>
        Picture<TPixel> ReadPicture();

        /// <summary>
        ///   Reads the next picture.
        /// </summary>
        /// <typeparam name="TPixel">Pixel</typeparam>
        /// <returns>The picture</returns>
        Task<Picture<TPixel>> ReadPictureAsync();
    }
}
