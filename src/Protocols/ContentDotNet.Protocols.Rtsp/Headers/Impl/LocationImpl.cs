namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class LocationImpl : LocationBase, IRtspLocationHeader
    {
        public override string Text => "Location";

        public string? Uri { get; set; }

        public override string ToString() => Uri ?? string.Empty;
    }
}
