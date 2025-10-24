namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class MTagImpl : MTagBase, IRtspMTagHeader
    {
        public override string Text => "MTag";

        public string? Value { get; set; }

        public override string ToString() => Value ?? string.Empty;
    }
}
