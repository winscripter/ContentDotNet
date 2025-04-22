using System.Xml.Serialization;

namespace ContentDotNet.Protocol.ProtocolModel;

[XmlRoot("Base64Command")]
public sealed class Base64
{
    [XmlAttribute("Type")]
    public Base64CommandType Type { get; }

    [XmlText]
    public string Value { get; }

    public Base64(Base64CommandType type, string value)
    {
        Type = type;
        Value = value;
    }
}
