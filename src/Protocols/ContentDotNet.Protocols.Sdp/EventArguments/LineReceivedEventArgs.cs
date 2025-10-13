namespace ContentDotNet.Protocols.Sdp.EventArguments
{
    using ContentDotNet.Protocols.Sdp.Abstractions;

    /// <summary>
    ///   Event arguments for the 'line received' event.
    /// </summary>
    public class LineReceivedEventArgs : EventArgs
    {
        /// <summary>
        ///   Did the line parse correctly?
        /// </summary>
        public bool ParsedSuccessfully { get; set; }

        /// <summary>
        ///   The actual line that was parsed, or <see langword="null"/> if parsing was not successful.
        /// </summary>
        public ISdpLineModel? Line { get; set; }

        /// <summary>
        ///   Was the line read asynchronously?
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LineReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="parsedSuccessfully">Did the line parse correctly?</param>
        /// <param name="line">The actual line that was parsed, or <see langword="null"/> if parsing was not successful.</param>
        /// <param name="isAsync">Was the line read asynchronously?</param>
        public LineReceivedEventArgs(bool parsedSuccessfully, ISdpLineModel? line, bool isAsync)
        {
            ParsedSuccessfully = parsedSuccessfully;
            Line = line;
            IsAsync = isAsync;
        }
    }
}
