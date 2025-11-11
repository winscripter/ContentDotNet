namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    /// <summary>
    ///   ttm:actor element
    /// </summary>
    public interface ITtm1Actor : ITtm1AgentElement
    {
        /// <summary>
        ///   The agent attribute.
        /// </summary>
        string AgentReference { get; set; }
    }
}
