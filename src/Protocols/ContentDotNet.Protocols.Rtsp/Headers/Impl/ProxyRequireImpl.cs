namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ProxyRequireImpl : ProxyRequireBase, IRtspProxyRequireHeader
    {
        public override string Text => "Proxy-Require";

        public string? Require { get; set; }

        public override string ToString() => Require ?? string.Empty;
    }
}
