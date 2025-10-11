namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Implementation;

    /// <summary>
    ///   Implementation for an H.264 service.
    /// </summary>
    public class H264Service : IH264Service
    {
        /// <inheritdoc cref="IH264Service.CreateDecoder(BitStreamReader)" />
        public AbstractH264Decoder CreateDecoder(BitStreamReader reader)
        {
            return new H264DecoderImplementation(reader);
        }
    }
}
