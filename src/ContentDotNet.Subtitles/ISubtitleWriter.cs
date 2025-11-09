namespace ContentDotNet.Subtitles
{
    /// <summary>
    ///   Generic subtitle writer.
    /// </summary>
    public interface ISubtitleWriter
    {
        /// <summary>
        ///   Backing stream writer.
        /// </summary>
        StreamWriter Writer { get; }

        /// <summary>
        ///   Writes the subtitle line.
        /// </summary>
        /// <param name="line">The subtitle line.</param>
        void WriteLine(ISubtitleLine line);

        /// <summary>
        ///   Writes the subtitle line.
        /// </summary>
        /// <param name="line">The subtitle line.</param>
        Task WriteLineAsync(ISubtitleLine line);

        /// <summary>
        ///   Used to convert to and from string from and to TimeSpan.
        /// </summary>
        ISubtitleTimeFormatter Formatter { get; set; }
    }
}
