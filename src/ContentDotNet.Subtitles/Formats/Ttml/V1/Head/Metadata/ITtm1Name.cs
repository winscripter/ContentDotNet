namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    using ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Enumerations;

    /// <summary>
    ///   ttm:name element
    /// </summary>
    public interface ITtm1Name : ITtm1AgentElement
    {
        /// <summary>
        ///   The name type.
        /// </summary>
        Ttm1NameType NameType { get; set; }

        /// <summary>
        ///   The content.
        /// </summary>
        string Content { get; set; }
    }
}
