namespace ContentDotNet.Rtsp.Sdp.Packets;

/// <summary>
/// Represents the SDP version packet ("v=" line) in an SDP message.
/// </summary>
public sealed class SdpVersionPacket : SdpPacket
{
    /// <inheritdoc/>
    public override char Character => 'v';

    /// <inheritdoc/>
    public override string Value { get; }

    /// <summary>
    /// Gets the SDP version as an integer.
    /// </summary>
    public int Version { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SdpVersionPacket"/> class with the specified value and version.
    /// </summary>
    /// <param name="value">The string value of the version (should be an integer as a string).</param>
    /// <param name="version">The version as an integer.</param>
    public SdpVersionPacket(string value, int version)
    {
        Value = value;
        Version = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SdpVersionPacket"/> class by parsing the specified value.
    /// </summary>
    /// <param name="value">The string value to parse. May optionally start with "v=".</param>
    /// <exception cref="SdpParserException">Thrown if the value cannot be parsed as an integer.</exception>
    public SdpVersionPacket(string value)
    {
        if (value.StartsWith("v="))
            value = value[2..];

        if (!int.TryParse(value, out int r))
            throw new SdpParserException("The provided value must be an integer");

        Value = value;
        Version = r;
    }

    /// <summary>
    /// Returns a deep copy of this <see cref="SdpVersionPacket"/> instance.
    /// </summary>
    /// <returns>A new <see cref="SdpVersionPacket"/> with the same value and version.</returns>
    public override SdpPacket Clone()
    {
        return new SdpVersionPacket(Value, Version);
    }
}
