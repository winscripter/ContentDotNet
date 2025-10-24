namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ReferrerImpl : ReferrerBase, IRtspReferrerHeader
    {
        public override string Text => "Referrer";

        public string? Uri { get; set; }

        public override string ToString() => Uri ?? string.Empty;
    }
}
