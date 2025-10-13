namespace ContentDotNet.Protocols.Rtsp.Headers
{
    /// <summary>
    ///   Abstracts an RTSP header.
    /// </summary>
    public interface IRtspHeader
    {
        /// <summary>
        ///   The raw RTSP text.
        /// </summary>
        string? RawText { get; set; }
    }
}
