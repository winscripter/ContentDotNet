namespace ContentDotNet.Extensions.Audio.G722
{
    using ContentDotNet.Abstractions;
    using ContentDotNet.BitStream;

    /// <summary>
    ///   Built-in G.722 service
    /// </summary>
    public class G722Service : IG722Service
    {
        /// <inheritdoc cref="IG722Service.CreateDecoder(BitStreamReader)" />
        public IPcmAudioCodec CreateDecoder(BitStreamReader reader)
        {
            return new G722Decoder(reader);
        }

        /// <inheritdoc cref="IG722Service.CreateEncoder(BitStreamWriter)" />
        public IPcmAudioCodecWriter CreateEncoder(BitStreamWriter writer)
        {
            throw new NotImplementedException("G.722 encoder is not yet implemented");
        }
    }
}
