namespace ContentDotNet.Subtitles
{
    /// <summary>
    ///   Generic subtitle reader.
    /// </summary>
    public interface ISubtitleReader
    {
        /// <summary>
        ///   The backing stream reader.
        /// </summary>
        StreamReader Reader { get; }

        /// <summary>
        ///   Reads the next subtitle line.
        /// </summary>
        /// <returns>The subtitle line.</returns>
        ISubtitleLine ReadNextLine();

        /// <summary>
        ///   Reads the next subtitle line, asynchronously.
        /// </summary>
        /// <returns>The subtitle line.</returns>
        Task<ISubtitleLine> ReadNextLineAsync();

        /// <summary>
        ///   Used to convert to and from string from and to TimeSpan.
        /// </summary>
        ISubtitleTimeFormatter Formatter { get; set; }
    }
}
