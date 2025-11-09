namespace ContentDotNet.Audio.Codecs.G722
{
    using ContentDotNet.Api.Abstractions;
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.Audio;

    /// <summary>
    ///   G.722 service
    /// </summary>
    public interface IG722Service
    {
        /// <summary>
        ///   Creates a G.722 decoder.
        /// </summary>
        /// <param name="reader">The bit-stream reader</param>
        /// <returns>The G.722 decoder</returns>
        IPcmAudioCodec CreateDecoder(BitStreamReader reader);

        /// <summary>
        ///   Creates a G.722 encoder.
        /// </summary>
        /// <param name="writer">The bit-stream writer</param>
        /// <returns>The G.722 encoder</returns>
        IPcmAudioCodecWriter CreateEncoder(BitStreamWriter writer);
    }
}
