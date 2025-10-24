namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class DateImpl : DateBase, IRtspDateHeader
    {
        public override string Text => "Date";

        public string? Time { get; set; }

        public override string ToString() => Time ?? string.Empty;
    }
}
