namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Helpers;
    using static ContentDotNet.Protocols.Rtsp.Helpers.StaticStringUtils;

    internal class WwwAuthenticateImpl : WwwAuthenticateBase, IRtspWwwAuthenticateHeader
    {
        public override string Text => "WWW-Authenticate";

        public string? DigestRealm { get; set; }
        public string? Nonce { get; set; }
        public string? Algorithm { get; set; }

        public override string ToString()
        {
            var kvb = new KeyValueBuilder();
            kvb.SetIfValueNotNull("Digest realm", QuotedOrNull(DigestRealm));
            kvb.SetIfValueNotNull("nonce", QuotedOrNull(Nonce));
            kvb.SetIfValueNotNull("algorithm", QuotedOrNull(Algorithm));
            return kvb.BuildCommaSeparatedString();
        }
    }
}
