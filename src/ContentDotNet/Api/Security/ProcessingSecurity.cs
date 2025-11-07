namespace ContentDotNet.Api.Security
{
    /// <summary>
    ///   Use these options to process untrusted input safely.
    /// </summary>
    public record struct ProcessingSecurity
    {
        /// <summary>
        ///   Security options for processing images.
        /// </summary>
        public ImageSecurityOptions ImageSecurityOptions;

        /// <summary>
        ///   Security options to mitigate Denial of Service attacks.
        /// </summary>
        public AntiDenialOfServiceOptions DoSOptions;
    }
}
