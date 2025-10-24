namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ContentBaseImpl : ContentBaseBase, IRtspContentBaseHeader
    {
        public override string Text => "Content-Base";

        public string? Uri { get; set; }

        public override string ToString() => Uri ?? string.Empty;
    }
}
