using System.Xml.Serialization;

namespace ContentDotNet.Protocol.ProtocolModel;

public enum Base64CommandType
{
    [XmlElement("Encode")]
    Encode = 1,

    [XmlElement("Decode")]
    Decode = 2,
}
