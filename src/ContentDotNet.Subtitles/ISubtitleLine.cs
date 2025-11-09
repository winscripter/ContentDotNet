namespace ContentDotNet.Subtitles
{
    /// <summary>
    ///   A single subtitle line.
    /// </summary>
    public interface ISubtitleLine
    {
        /// <summary>
        ///   The text inside this line.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        ///   Styling options for this subtitle line.
        /// </summary>
        ISubtitleStyling? Styling { get; set; }

        /// <summary>
        ///   Start time appearance.
        /// </summary>
        TimeSpan Start { get; set; }

        /// <summary>
        ///   End time appearance.
        /// </summary>
        TimeSpan End { get; set; }
    }
}
