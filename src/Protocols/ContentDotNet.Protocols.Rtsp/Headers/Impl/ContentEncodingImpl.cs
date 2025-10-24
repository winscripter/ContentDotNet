namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ContentEncodingImpl : ContentEncodingBase, IRtspContentEncodingHeader
    {
        public override string Text => "Content-Encoding";

        public string? Encoding { get; set; }

        public override string ToString() => Encoding ?? string.Empty;
    }
}
