namespace ContentDotNet.Rtsp.Sdp.Packets;

internal sealed partial class SdpVersionPacket : SdpPacket
{
    public override char Character => 'v';

    public override string Value { get; }

    public SdpVersionPacket(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public static SdpVersionPacket Parse(string value)
    {
        string[] splitted = value.Split('=');
        if (splitted.Length != 2)
            throw new SdpParserException("The version packet must have exactly one '=' character.");

        if (splitted[0] != "v")
            throw new SdpParserException("The version packet must start with 'v'.");

        string version = splitted[1].Trim();
        if (string.IsNullOrEmpty(version))
            throw new SdpParserException("The version value cannot be empty.");

        return new SdpVersionPacket(version);
    }

    public override SdpPacket Clone()
    {
        return new SdpVersionPacket(Value);
    }
}
