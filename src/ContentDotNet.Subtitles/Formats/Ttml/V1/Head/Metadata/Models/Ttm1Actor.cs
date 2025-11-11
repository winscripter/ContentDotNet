namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Models
{
    using System.Xml.Linq;

    public class Ttm1Actor(XElement element) : ITtm1Actor
    {
        public string AgentReference
        {
            get => Element.Attribute("agent")!.Value;
            set => Element.SetAttributeValue("agent", value);
        }

        public XElement Element { get; } = element;
    }
}
