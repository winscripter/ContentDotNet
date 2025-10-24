namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class RetryAfterImpl : RetryAfterBase, IRtspRetryAfterHeader
    {
        public override string Text => "Retry-After";

        public string? DateOrTime { get; set; }

        public override string ToString() => DateOrTime ?? string.Empty;
    }
}
