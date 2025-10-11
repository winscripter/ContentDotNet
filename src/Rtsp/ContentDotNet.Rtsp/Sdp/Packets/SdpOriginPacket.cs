namespace ContentDotNet.Rtsp.Sdp.Packets;

internal sealed class SdpOriginPacket : SdpPacket
{
    public override char Character => 'o';
    public override string Value { get; }

    private readonly string[] _splitted;

    public string Username
    {
        get => _splitted[0];
        set => _splitted[0] = value;
    }

    public string SessionId
    {
        get => _splitted[1];
        set => _splitted[1] = value;
    }

    public string SessionVersion
    {
        get => _splitted[2];
        set => _splitted[2] = value;
    }

    public string NetworkType
    {
        get => _splitted[3];
        set => _splitted[3] = value;
    }

    public string AddressType
    {
        get => _splitted[4];
        set => _splitted[4] = value;
    }

    public string Address
    {
        get => _splitted[5];
        set => _splitted[5] = value;
    }

    public SdpOriginPacket(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));
        }
        _splitted = value.Split(' ');
        if (_splitted.Length < 6)
        {
            throw new ArgumentException("Origin packet must contain at least 6 fields.");
        }
        Value = value;
    }

    public static SdpOriginPacket Parse(string value)
    {
        string[] splitted = value.Split('=');

        if (splitted.Length != 2)
            throw new SdpParserException("The origin packet must have exactly one '=' character.");

        if (splitted[0] != "o")
            throw new SdpParserException("The origin packet must start with 'o'.");

        return new SdpOriginPacket(splitted[1].Trim());
    }

    public override SdpPacket Clone()
    {
        return new SdpOriginPacket(Value);
    }
}
