namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ContentLocationImpl : ContentLocationBase, IRtspContentLocationHeader
    {
        public override string Text => "Content-Location";

        public string? Location { get; set; }

        public override string ToString() => Location?.ToString() ?? string.Empty;
    }
}
