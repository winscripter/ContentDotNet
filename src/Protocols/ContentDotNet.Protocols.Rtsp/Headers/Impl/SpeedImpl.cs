namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class SpeedImpl : SpeedBase, IRtspSpeedHeader
    {
        public override string Text => "Speed";

        public string? SpeedRange { get; set; }

        public override string ToString() => SpeedRange ?? string.Empty;
    }
}
