namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Security.Cryptography;
    using System.Text;

    internal class ProxyAuthenticationInfoImpl : ProxyAuthenticateBase, IRtspProxyAuthenticationInfoHeader
    {
        public override string Text => "Proxy-Authentication-Info";

        public string? NextNonce { get; set; }
        public string? QualityOfProtection { get; set; }
        public string? RspAuth { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            bool applied = false;
            if (NextNonce != null)
            {
                sb.Append($"next-nonce={NextNonce}");
                applied = true;
            }
            if (QualityOfProtection != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"qop={QualityOfProtection}");
                applied = true;
            }
            if (RspAuth != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"rsp-auth={RspAuth}");
            }
            return sb.ToString();
        }
    }
}
