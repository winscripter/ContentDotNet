namespace ContentDotNet.Subtitles
{
    /// <summary>
    ///   Converts <see cref="TimeSpan"/> into and from the time format string represented by the
    ///   subtitle format.
    /// </summary>
    public interface ISubtitleTimeFormatter
    {
        /// <summary>
        ///   Parses the time span.
        /// </summary>
        /// <param name="input">The input time representation as a string</param>
        /// <returns>The time span</returns>
        TimeSpan Parse(string input);

        /// <summary>
        ///   Converts that time span into the string representation compatible with this subtitle file format.
        /// </summary>
        /// <param name="input">The input time span</param>
        /// <returns>String representation compatible with this subtitle file format.</returns>
        string Format(TimeSpan input);
    }
}
