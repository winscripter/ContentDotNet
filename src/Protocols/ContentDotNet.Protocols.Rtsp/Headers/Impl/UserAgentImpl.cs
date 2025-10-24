namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class UserAgentImpl : UserAgentBase, IRtspUserAgentHeader
    {
        public override string Text => "User-Agent";

        public string? Value { get; set; }

        public override string ToString() => Value ?? string.Empty;
    }
}
