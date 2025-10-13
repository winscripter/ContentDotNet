namespace ContentDotNet.Protocols.Rtsp.Headers.Records
{
    using ContentDotNet.Protocols.Rtsp.Headers.Enumerations;

    /// <summary>
    ///   An Accept-Credentials record.
    /// </summary>
    public record AcceptCredentialsRecord
    {
        /// <summary>
        ///   The origin.
        /// </summary>
        public Uri Origin { get; set; }

        /// <summary>
        ///   The hash algorithm.
        /// </summary>
        public AcceptCredentialsHashAlgorithm HashAlgorithm { get; set; }

        /// <summary>
        ///   The digest, in Base-64.
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="AcceptCredentialsRecord"/> class.
        /// </summary>
        /// <param name="origin">See <see cref="Origin"/></param>
        /// <param name="hashAlgorithm">See <see cref="HashAlgorithm"/></param>
        /// <param name="digest">See <see cref="Digest"/></param>
        public AcceptCredentialsRecord(Uri origin, AcceptCredentialsHashAlgorithm hashAlgorithm, string digest)
        {
            Origin = origin;
            HashAlgorithm = hashAlgorithm;
            Digest = digest;
        }
    }
}
