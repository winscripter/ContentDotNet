namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class TerminateReasonImpl : TerminateReasonBase, IRtspTerminateReasonHeader
    {
        public override string Text => "Terminate-Reason";

        public string? Reason { get; set; }

        public override string ToString() => Reason ?? string.Empty;
    }
}
