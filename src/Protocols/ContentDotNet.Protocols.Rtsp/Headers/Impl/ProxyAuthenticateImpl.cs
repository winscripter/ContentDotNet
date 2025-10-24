namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Text;

    internal class ProxyAuthenticateImpl : ProxyAuthenticateBase, IRtspProxyAuthenticateHeader
    {
        public override string Text => "Proxy-Authenticate";

        public string? AuthScheme { get; set; }
        public string? Realm { get; set; }
        public string? Nonce { get; set; }
        public string? Algorithm { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            bool applied = false;
            if (AuthScheme != null)
            {
                sb.Append($"auth-scheme={AuthScheme}");
                applied = true;
            }
            if (Realm != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"Digest realm={Realm}");
                applied = true;
            }
            if (Nonce != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"nonce={Nonce}");
                applied = true;
            }
            if (Algorithm != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"algorithm={Algorithm}");
            }
            return sb.ToString();
        }
    }
}
