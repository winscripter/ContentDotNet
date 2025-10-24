namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class NotifyReasonImpl : NotifyReasonBase, IRtspNotifyReasonHeader
    {
        public override string Text => "Notify-Reason";

        public string? ReasonToken { get; set; }

        public override string ToString() => ReasonToken ?? string.Empty;
    }
}
