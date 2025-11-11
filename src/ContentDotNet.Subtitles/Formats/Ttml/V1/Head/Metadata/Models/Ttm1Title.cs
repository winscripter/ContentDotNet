namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Models
{
    using System.Xml.Linq;

    public class Ttm1Title(XElement element) : ITtm1Title
    {
        public string Title
        {
            get => Element.Value;
            set => Element.Value = value;
        }

        public XElement Element => element;
    }
}
