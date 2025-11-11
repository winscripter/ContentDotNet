namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Models
{
    using ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Enumerations;
    using System.Xml.Linq;

    public class Ttm1Name(XElement element) : ITtm1Name
    {
        public Ttm1NameType NameType
        {
            get => Ttm1NameTypeFormatter.FromString(Element.Attribute("type")!.Value) ?? throw new InvalidOperationException("Invalid name type"),
            set => Element.SetAttributeValue("type", Ttm1NameTypeFormatter.ToString(value));
        }

        public string Content
        {
            get => Element.Value;
            set => Element.Value = value;
        }

        public XElement Element => element;
    }
}
