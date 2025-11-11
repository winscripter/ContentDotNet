namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    /// <summary>
    ///   ttm:title element
    /// </summary>
    public interface ITtm1Title : ITtm1Element
    {
        /// <summary>
        ///   The title text.
        /// </summary>
        string Title { get; set; }
    }
}
