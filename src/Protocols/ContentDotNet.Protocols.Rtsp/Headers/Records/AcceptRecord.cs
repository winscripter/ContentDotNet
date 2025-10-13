namespace ContentDotNet.Protocols.Rtsp.Headers.Records
{
    /// <summary>
    ///   A single accept record.
    /// </summary>
    public record AcceptRecord
    {
        /// <summary>
        ///   The MIME type, i.e. application/sdp.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        ///   The quality value, or null if unspecified. I.e. q=0.5 results in this value being 0.5,
        ///   but if q= isn't specified, this is null.
        /// </summary>
        /// <remarks>
        ///   This does not specify preferred quality. It solely represents the quality specified
        ///   in the syntax of the Accept value.
        /// </remarks>
        public double? Quality { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="AcceptRecord"/> class.
        /// </summary>
        /// <param name="mimeType">See <see cref="MimeType"/></param>
        /// <param name="quality">See <see cref="Quality"/></param>
        public AcceptRecord(string mimeType, double? quality)
        {
            MimeType = mimeType;
            Quality = quality;
        }
    }
}
