namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class IfNoneMatchImpl : IfNoneMatchBase, IRtspIfNoneMatchHeader
    {
        public override string Text => "If-None-Match";

        public List<string> ETags { get; set; } = [];

        public override string ToString() => string.Join(", ", ETags);
    }
}
