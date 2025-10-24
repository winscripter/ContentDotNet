namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class RequireImpl : RequireBase, IRtspRequireHeader
    {
        public override string Text => "Require";

        public string? Feature { get; set; }

        public override string ToString() => Feature ?? string.Empty;
    }
}
