namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Models
{
    using System.Xml.Linq;

    public class Ttm1Copyright(XElement element) : ITtm1Copyright
    {
        public string Copyright
        {
            get => Element.Value;
            set => Element.Value = value;
        }

        public XElement Element => element;
    }
}
