namespace ContentDotNet.Protocols.Sdp.EventArguments
{
    using ContentDotNet.Protocols.Sdp.Abstractions;

    /// <summary>
    ///   Event arguments for the 'line written' event.
    /// </summary>
    public class LineWrittenEventArgs : EventArgs
    {
        /// <summary>
        ///   The line that was written.
        /// </summary>
        public ISdpLineModel? Line { get; set; }

        /// <summary>
        ///   Was the line written asynchronously?
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LineWrittenEventArgs"/> class.
        /// </summary>
        /// <param name="line">The line that was written.</param>
        /// <param name="isAsync">Was the line written asynchronously?</param>
        public LineWrittenEventArgs(ISdpLineModel? line, bool isAsync)
        {
            Line = line;
            IsAsync = isAsync;
        }
    }
}
