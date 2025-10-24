namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class RequestStatusImpl : RequestStatusBase, IRtspRequestStatusHeader
    {
        public override string Text => "Request-Status";

        public int? CSeq { get; set; }
        public int? Status { get; set; }
        public string? Reason { get; set; }

        public override string ToString() => throw new NotImplementedException();
    }
}
