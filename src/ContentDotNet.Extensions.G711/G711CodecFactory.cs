using ContentDotNet.Abstractions;
using ContentDotNet.BitStream;
using ContentDotNet.Extensions.G711.Internal;

namespace ContentDotNet.Extensions.G711;

/// <summary>
///   The G.711 codec factory.
/// </summary>
public static class G711CodecFactory
{
    private static readonly IG711Service s_service = new G711ServiceImpl();

    /// <summary>
    ///   Gets the singleton instance of the <see cref="IG711Service"/> implementation.
    /// </summary>
    /// <returns>
    ///   The singleton <see cref="IG711Service"/> instance.
    /// </returns>
    public static IG711Service GetService()
    {
        return s_service;
    }

    /// <summary>
    ///   Creates a G.711 A-law PCM audio codec reader for the specified bit stream.
    /// </summary>
    /// <param name="stream">The <see cref="BitStreamReader"/> to read from.</param>
    /// <returns>
    ///   An <see cref="IPcmAudioCodec"/> instance for reading A-law encoded audio data.
    /// </returns>
    public static IPcmAudioCodec CreateALawReader(BitStreamReader stream)
    {
        return GetService().CreateCodec(stream, G711Law.A);
    }

    /// <summary>
    ///   Creates a G.711 Mu-law PCM audio codec reader for the specified bit stream.
    /// </summary>
    /// <param name="stream">The <see cref="BitStreamReader"/> to read from.</param>
    /// <returns>
    ///   An <see cref="IPcmAudioCodec"/> instance for reading Mu-law encoded audio data.
    /// </returns>
    public static IPcmAudioCodec CreateMuLawReader(BitStreamReader stream)
    {
        return GetService().CreateCodec(stream, G711Law.Mu);
    }

    /// <summary>
    ///   Creates a G.711 A-law PCM audio codec writer for the specified bit stream.
    /// </summary>
    /// <param name="stream">The <see cref="BitStreamWriter"/> to write to.</param>
    /// <returns>
    ///   An <see cref="IPcmAudioCodecWriter"/> instance for writing A-law encoded audio data.
    /// </returns>
    public static IPcmAudioCodecWriter CreateALawWriter(BitStreamWriter stream)
    {
        return GetService().CreateCodecWriter(stream, G711Law.A);
    }

    /// <summary>
    ///   Creates a G.711 Mu-law PCM audio codec writer for the specified bit stream.
    /// </summary>
    /// <param name="stream">The <see cref="BitStreamWriter"/> to write to.</param>
    /// <returns>
    ///   An <see cref="IPcmAudioCodecWriter"/> instance for writing Mu-law encoded audio data.
    /// </returns>
    public static IPcmAudioCodecWriter CreateMuLawWriter(BitStreamWriter stream)
    {
        return GetService().CreateCodecWriter(stream, G711Law.Mu);
    }
}
