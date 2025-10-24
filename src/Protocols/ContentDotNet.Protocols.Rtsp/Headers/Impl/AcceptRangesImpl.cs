namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class AcceptRangesImpl : AcceptRangesBase, IRtspAcceptRangesHeader
    {
        public override string Text => "Accept-Ranges";

        public List<string> Ranges { get; set; } = [];

        public override string ToString() => string.Join(", ", Ranges);
    }
}
