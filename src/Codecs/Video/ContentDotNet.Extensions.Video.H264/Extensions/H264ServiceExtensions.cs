namespace ContentDotNet.Extensions.Video.H264.Extensions
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   Extensions for <see cref="IH264Service"/> implementations.
    /// </summary>
    public static class H264ServiceExtensions
    {
        /// <summary>
        ///   Creates an H.264 decoder.
        /// </summary>
        /// <param name="h264Service">The source H.264 service.</param>
        /// <param name="sourceStream">The stream where the H.264 data comes from.</param>
        /// <returns>The abstract H.264 decoder.</returns>
        public static AbstractH264Decoder CreateDecoder(
            this IH264Service h264Service,
            Stream sourceStream)
        {
            var bitStreamReader = new BitStreamReader(sourceStream);
            return h264Service.CreateDecoder(bitStreamReader);
        }

        /// <summary>
        ///   Creates an H.264 decoder.
        /// </summary>
        /// <param name="h264Service">The source H.264 service.</param>
        /// <param name="filePath">The file where the H.264 data comes from.</param>
        /// <returns>The abstract H.264 decoder.</returns>
        /// <remarks>
        ///   ⚠️ <b>Warning</b>: This method is OS dependent as it can perform
        ///   reading files from the system. This method won't work on platforms
        ///   where there's no storage access.
        /// </remarks>
        public static AbstractH264Decoder CreateDecoder(
            this IH264Service h264Service,
            string filePath)
        {
            using var stream = File.OpenRead(filePath);
            return CreateDecoder(h264Service, stream);
        }
    }
}
