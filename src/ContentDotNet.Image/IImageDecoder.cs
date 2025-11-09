namespace ContentDotNet.Image
{
    using ContentDotNet.Api.BitStream;

    using ContentDotNet.Api.Pictures;

    /// <summary>
    ///   Decoder for image formats.
    /// </summary>
    public interface IImageDecoder
    {
        /// <summary>
        ///   Is async supported?
        /// </summary>
        bool SupportsAsync { get; }

        /// <summary>
        ///   Image technical name (i.e. PNG)
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Actual format name (i.e. Bitmap or Portable Network Graphics (PNG))
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///   Decodes the image.
        /// </summary>
        /// <returns>The picture.</returns>
        IPicture Decode();

        /// <summary>
        ///   Asynchronously decodes the image.
        /// </summary>
        /// <returns>The decoded picture.</returns>
        Task<IPicture> DecodeAsync();
    }
}
