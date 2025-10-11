namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   The H.264 service.
    /// </summary>
    public interface IH264Service : IService
    {
        /// <summary>
        ///   Creates the H.264 decoder.
        /// </summary>
        /// <param name="reader">The source bit-stream reader.</param>
        /// <returns>The H.264 decoder.</returns>
        AbstractH264Decoder CreateDecoder(BitStreamReader reader);
    }
}
