namespace ContentDotNet.Protocols.Rtsp.Headers.Records
{
    /// <summary>
    ///   A single Accept-Language record.
    /// </summary>
    public record AcceptLanguageRecord
    {
        /// <summary>
        ///   The language type, i.e. en, da, or en-gb.
        /// </summary>
        public string Language { get; set; }

        /// <inheritdoc cref="AcceptRecord.Quality" />
        public double? Quality { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="AcceptLanguageRecord"/> class.
        /// </summary>
        /// <param name="language">See <see cref="Language"/></param>
        /// <param name="quality">See <see cref="AcceptRecord.Quality"/></param>
        public AcceptLanguageRecord(string language, double? quality)
        {
            Language = language;
            Quality = quality;
        }
    }
}
