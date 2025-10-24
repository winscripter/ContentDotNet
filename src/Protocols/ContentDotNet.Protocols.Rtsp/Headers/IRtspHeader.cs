namespace ContentDotNet.Protocols.Rtsp.Headers
{
    /// <summary>
    ///   Abstracts an RTSP header.
    /// </summary>
    public interface IRtspHeader
    {
        /// <summary>
        ///   The RTSP text that identifies the line, before the
        ///   : character.
        /// </summary>
        string Text { get; }
    }
}
