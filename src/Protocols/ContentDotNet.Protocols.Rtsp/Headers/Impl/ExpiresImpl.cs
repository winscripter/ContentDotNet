namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ExpiresImpl : ExpiresBase, IRtspExpiresHeader
    {
        public override string Text => "Expires";

        public string? Time { get; set; }

        public override string ToString() => Time ?? string.Empty;
    }
}
