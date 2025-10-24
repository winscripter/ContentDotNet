namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ServerImpl : ServerBase, IRtspServerHeader
    {
        public override string Text => "Server";

        public string? ServerValue { get; set; }

        public override string ToString() => ServerValue ?? string.Empty;
    }
}
