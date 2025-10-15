namespace ContentDotNet.Extensions.Audio.G722
{
    using ContentDotNet.Abstractions;
    using ContentDotNet.BitStream;

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
