namespace ContentDotNet.Protocols.Rtsp.Headers.Formatters
{
    /// <summary>
    ///   The RTSP header formatter.
    /// </summary>
    public class RtspHeaderFormatter
    {
        /// <summary>
        ///   Formats the value.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value">Input value.</param>
        /// <returns>The formatted value.</returns>
        public virtual string Format<T>(T value)
        {
            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        ///   Parses the value.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value">Input value to parse.</param>
        /// <returns>The parsed value.</returns>
        public virtual T Parse<T>(string value)
        {
            throw new InvalidOperationException();
        }
    }
}
