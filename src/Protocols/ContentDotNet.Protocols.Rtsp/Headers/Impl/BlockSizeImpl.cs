namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class BlockSizeImpl : BlockSizeBase, IRtspBlockSizeHeader
    {
        public override string Text => "Block-Size";

        public int? Value { get; set; }

        public override string ToString() => Value?.ToString() ?? string.Empty;
    }
}
