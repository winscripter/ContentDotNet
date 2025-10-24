namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class SessionImpl : SessionBase, IRtspSessionHeader
    {
        public override string Text => "Session";

        public int? SessionId { get; set; }
        public int? Timeout { get; set; }

        public override string ToString() => (SessionId ?? 0).ToString();
    }
}
