namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    /// <summary>
    ///   ttm:desc element
    /// </summary>
    public interface ITtm1Desc : ITtm1Element
    {
        /// <summary>
        ///   The description.
        /// </summary>
        string Description { get; set; }
    }
}
