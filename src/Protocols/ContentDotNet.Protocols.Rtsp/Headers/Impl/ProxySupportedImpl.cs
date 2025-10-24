namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class ProxySupportedImpl : ProxySupportedBase, IRtspProxySupportedHeader
    {
        public override string Text => "Proxy-Supported";

        public List<string> Values { get; set; } = [];

        public override string ToString() => string.Join(", ", Values);
    }
}
