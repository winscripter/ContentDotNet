using ContentDotNet.Rtsp.Sdp.Packets;

namespace ContentDotNet.Rtsp.Sdp;

/// <summary>
///   Abstracts an SDP packet parser.
/// </summary>
public interface ISdpParser
{
    /// <summary>
    ///   The character, like v, o, or s.
    /// </summary>
    char Character { get; }

    /// <summary>
    ///   Checks if this SDP parser can parse <paramref name="src"/>.
    /// </summary>
    /// <param name="src">Packet source to be parsed.</param>
    /// <returns>A boolean, indicating whether <paramref name="src"/> can be parsed with this SDP parser.</returns>
    bool CanParse(SdpPacketSource src);

    /// <summary>
    ///   Attempts parsing the SDP packet source.
    /// </summary>
    /// <param name="src">Packet source</param>
    /// <returns>The parsed packet, or <see langword="null"/> if it couldn't be parsed.</returns>
    SdpPacket? Parse(SdpPacketSource src);

    /// <summary>
    ///   Specific type of the <see cref="SdpPacket"/> being returned by
    ///   the <see cref="Parse(SdpPacketSource)"/> method.
    /// </summary>
    Type PacketType { get; }
}
