namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Models
{
    using System.Xml.Linq;

    public class Ttm1Desc(XElement element) : ITtm1Desc
    {
        public string Description
        {
            get => Element.Value;
            set => Element.Value = value;
        }

        public XElement Element => element;
    }
}
