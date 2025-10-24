namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Helpers;
    using static ContentDotNet.Protocols.Rtsp.Helpers.StaticStringUtils;

    internal class AuthorizationImpl : AuthorizationBase, IRtspAuthorizationHeader
    {
        public override string Text => "Authorization";

        public string? DigestUsername { get; set; }
        public string? Realm { get; set; }
        public string? Nonce { get; set; }
        public string? Uri { get; set; }
        public string? Response { get; set; }
        public string? QualityOfProtection { get; set; }
        public int? NonceCount { get; set; }
        public string? ClientNonce { get; set; }

        public override string ToString()
        {
            var kvb = new KeyValueBuilder();
            kvb.SetIfValueNotNull("Digest username", QuotedOrNull(DigestUsername));
            kvb.SetIfValueNotNull("realm", QuotedOrNull(Realm));
            kvb.SetIfValueNotNull("nonce", QuotedOrNull(Nonce));
            kvb.SetIfValueNotNull("uri", QuotedOrNull(Uri));
            kvb.SetIfValueNotNull("response", QuotedOrNull(Response));
            kvb.SetIfValueNotNull("qop", QuotedOrNull(QualityOfProtection));
            kvb.SetIfValueNotNull("nc", QuotedOrNull(NonceCount?.ToString()));
            kvb.SetIfValueNotNull("cnonce", QuotedOrNull(ClientNonce));
            return kvb.BuildCommaSeparatedString();
        }
    }
}
