namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Helpers;
    using static ContentDotNet.Protocols.Rtsp.Helpers.StaticStringUtils;

    internal class AuthenticationInfoImpl : AuthenticationInfoBase, IRtspAuthenticationInfoHeader
    {
        public override string Text => "Authentication-Info";

        public string? NextNonce { get; set; }
        public string? QualityOfProtection { get; set; }
        public string? ResponseAuthenticationDigest { get; set; }
        public string? ClientNonce { get; set; }
        public int? NonceCount { get; set; }

        public override string ToString()
        {
            var kvb = new KeyValueBuilder();
            kvb.SetIfValueNotNull("nextnonce", QuotedOrNull(NextNonce));
            kvb.SetIfValueNotNull("qop", QuotedOrNull(QualityOfProtection));
            kvb.SetIfValueNotNull("rspauth", QuotedOrNull(ResponseAuthenticationDigest));
            kvb.SetIfValueNotNull("cnonce", QuotedOrNull(ClientNonce));
            kvb.SetIfValueNotNull("nc", QuotedOrNull(NonceCount?.ToString()));
            return kvb.BuildCommaSeparatedString();
        }
    }
}
