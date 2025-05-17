using ContentDotNet.Abstractions;
using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.G711;

/// <summary>
///   Represents the transient G.711 service.
/// </summary>
public interface IG711Service
{
    /// <summary>
    ///   Creates the G.711 codec reader.
    /// </summary>
    /// <param name="reader">Reader stream</param>
    /// <param name="law">G.711 law</param>
    /// <returns>The G.711 codec reader</returns>
    IPcmAudioCodec CreateCodec(BitStreamReader reader, G711Law law);

    /// <summary>
    ///   Creates the G.711 codec writer.
    /// </summary>
    /// <param name="writer">Writer stream</param>
    /// <param name="law">G.711 law</param>
    /// <returns>The G.711 codec writer</returns>
    IPcmAudioCodecWriter CreateCodecWriter(BitStreamWriter writer, G711Law law);
}
