namespace ContentDotNet.Protocols.Rtsp.Headers.Records
{
    /// <summary>
    ///   A single Accept-Encoding record.
    /// </summary>
    public record AcceptEncodingRecord
    {
        /// <summary>
        ///   The encoding type, i.e. gzip, deflate, or identity.
        /// </summary>
        public string Encoding { get; set; }

        /// <inheritdoc cref="AcceptRecord.Quality" />
        public double? Quality { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="AcceptEncodingRecord"/> class.
        /// </summary>
        /// <param name="encoding">See <see cref="Encoding"/></param>
        /// <param name="quality">See <see cref="AcceptRecord.Quality"/></param>
        public AcceptEncodingRecord(string encoding, double? quality)
        {
            Encoding = encoding;
            Quality = quality;
        }
    }
}
