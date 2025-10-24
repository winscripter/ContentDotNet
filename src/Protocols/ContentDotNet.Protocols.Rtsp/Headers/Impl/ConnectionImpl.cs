namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class ConnectionImpl : ConnectionBase, IRtspConnectionHeader
    {
        public override string Text => "Condition";

        public List<string> Directives { get; set; } = [];

        public override string ToString() => string.Join(", ", Directives);
    }
}
