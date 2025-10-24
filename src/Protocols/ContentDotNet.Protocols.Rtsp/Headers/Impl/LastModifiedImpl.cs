namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class LastModifiedImpl : LastModifiedBase, IRtspLastModifiedHeader
    {
        public override string Text => "Last-Modified";

        public string? Time { get; set; }

        public override string ToString() => Time ?? string.Empty;
    }
}
