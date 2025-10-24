namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class TimestampImpl : TimestampBase, IRtspTimestampHeader
    {
        public override string Text => "Timestamp";

        public string? DateTime { get; set; }

        public override string ToString() => DateTime ?? string.Empty;
    }
}
