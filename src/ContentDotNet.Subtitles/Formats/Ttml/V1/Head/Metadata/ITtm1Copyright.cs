namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    /// <summary>
    ///   ttm:copyright element
    /// </summary>
    public interface ITtm1Copyright : ITtm1Element
    {
        /// <summary>
        ///   The copyright.
        /// </summary>
        string Copyright { get; set; }
    }
}
