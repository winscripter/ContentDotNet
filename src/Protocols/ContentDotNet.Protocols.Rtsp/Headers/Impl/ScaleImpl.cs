namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ScaleImpl : ScaleBase, IRtspScaleHeader
    {
        public override string Text => "Scale";

        public double? ScaleValue { get; set; }

        public override string ToString() => ScaleValue?.ToString(provider: null) ?? string.Empty;
    }
}
