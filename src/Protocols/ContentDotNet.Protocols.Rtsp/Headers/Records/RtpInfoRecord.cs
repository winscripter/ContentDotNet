namespace ContentDotNet.Protocols.Rtsp.Headers.Records
{
    /// <summary>
    ///   The record for the RTP-Info header.
    /// </summary>
    public record RtpInfoRecord
    {
        /// <summary>
        ///   The URL
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        ///   The SSRC
        /// </summary>
        public string? Ssrc { get; set; }

        /// <summary>
        ///   The sequence
        /// </summary>
        public string? Seq { get; set; }

        /// <summary>
        ///   The RTP time
        /// </summary>
        public string? RtpTime { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="RtpInfoRecord"/> class.
        /// </summary>
        /// <param name="url">See <see cref="Url"/></param>
        /// <param name="ssrc">See <see cref="Ssrc"/></param>
        /// <param name="seq">See <see cref="Seq"/></param>
        /// <param name="rtpTime">See <see cref="RtpTime"/></param>
        public RtpInfoRecord(string? url, string? ssrc, string? seq, string? rtpTime)
        {
            Url = url;
            Ssrc = ssrc;
            Seq = seq;
            RtpTime = rtpTime;
        }
    }
}
