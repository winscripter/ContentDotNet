namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    using System.Xml.Linq;

    /// <summary>
    ///   TTML 1 Metadata element.
    /// </summary>
    public interface ITtm1Element
    {
        /// <summary>
        ///   Backing XML element.
        /// </summary>
        XElement Element { get; }
    }
}
