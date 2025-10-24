namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;

    internal class ViaImpl : ViaBase, IRtspViaHeader
    {
        public override string Text => "Via";

        public List<ViaRecord> Value { get; set; } = [];

        public override string ToString() => string.Join(", ", Value.Select(x => $"{x.ProtocolAndVersion} {x.Host}:{x.Port}"));
    }
}
