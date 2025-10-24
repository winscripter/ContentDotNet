namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class PipelinedRequestsImpl : PipelinedRequestsBase, IRtspPipelinedRequestsHeader
    {
        public override string Text => "Pipelined-Requests";

        public string? Token { get; set; }

        public override string ToString() => Token ?? string.Empty;
    }
}
