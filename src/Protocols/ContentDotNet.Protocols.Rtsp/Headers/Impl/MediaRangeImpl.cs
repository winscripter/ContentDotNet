namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class MediaRangeImpl : MediaRangeBase, IRtspMediaRangeHeader
    {
        public override string Text => "Media-Range";

        public string? NormalPlayTime { get; set; }

        public override string ToString() => $"npt={NormalPlayTime}";
    }
}
