using System.Text.RegularExpressions;

namespace ContentDotNet.Rtsp.Sdp.Packets;

/// <summary>
///   Represents an SDP Origin packet.
/// </summary>
public sealed class SdpOriginPacket : SdpPacket
{
    // At this point, knowing regex should be a flex
    private static readonly Regex OriginRegex = new(@"(?<username>[a-zA-Z0-9]*)\s+(?<sessionId>[0-9]*)\s+(?<version>[0-9]*)\s+(?<netType>[a-zA-Z]*)\s+(?<addressType>[a-zA-Z]*)\s+(?<address>[0-9]*\.[0-9]*\.[0-9]*\.[0-9]*)");

    /// <inheritdoc/>
    public override char Character => 'o';

    /// <inheritdoc/>
    public override string Value { get; }

    /// <summary>
    /// Gets the username of the session originator.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Gets the unique session ID.
    /// </summary>
    public ulong SessionId { get; }

    /// <summary>
    /// Gets the session version.
    /// </summary>
    public ulong Version { get; }

    /// <summary>
    /// Gets the network type (e.g., "IN").
    /// </summary>
    public string NetworkType { get; }

    /// <summary>
    /// Gets the address type (e.g., "IP4").
    /// </summary>
    public string AddressType { get; }

    /// <summary>
    /// Gets the unicast address of the originator.
    /// </summary>
    public string Address { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SdpOriginPacket"/> class by parsing the specified value.
    /// </summary>
    /// <param name="value">The SDP origin line value to parse.</param>
    /// <exception cref="SdpParserException">Thrown if the value does not match the expected origin format.</exception>
    public SdpOriginPacket(string value)
    {
        if (!OriginRegex.IsMatch(value))
            throw new SdpParserException("Invalid Origin packet");

        Value = value;

        Match match = OriginRegex.Match(value);
        Username = match.Groups["username"].Value;
        SessionId = ulong.Parse(match.Groups["sessionId"].Value);
        Version = ulong.Parse(match.Groups["version"].Value);
        NetworkType = match.Groups["netType"].Value;
        AddressType = match.Groups["addressType"].Value;
        Address = match.Groups["address"].Value;
    }

    /// <summary>
    /// Returns a deep copy of this <see cref="SdpOriginPacket"/> instance.
    /// </summary>
    /// <returns>A new <see cref="SdpOriginPacket"/> with the same value.</returns>
    public override SdpPacket Clone()
    {
        return new SdpOriginPacket(Value);
    }
}
