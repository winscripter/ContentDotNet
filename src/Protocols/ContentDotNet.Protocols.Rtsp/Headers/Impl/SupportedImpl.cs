namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class SupportedImpl : SupportedBase, IRtspSupportedHeader
    {
        public override string Text => "Supported";

        public List<string> SupportedExtensions { get; set; } = [];

        public override string ToString() => string.Join(", ", SupportedExtensions);
    }
}
