namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class CSeqImpl : CSeqBase, IRtspCSeqHeader
    {
        public override string Text => "CSeq";

        public int? SequenceNumber { get; set; }

        public override string ToString() => (SequenceNumber ?? 0).ToString();
    }
}
