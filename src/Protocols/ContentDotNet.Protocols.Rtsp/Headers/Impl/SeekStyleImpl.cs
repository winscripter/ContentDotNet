namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class SeekStyleImpl : SeekStyleBase, IRtspSeekStyleHeader
    {
        public override string Text => "Seek-Style";

        public string? Value { get; set; }

        public override string ToString() => Value ?? string.Empty;
    }
}
