namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class UnsupportedImpl : UnsupportedBase, IRtspUnsupportedHeader
    {
        public override string Text => "Unsupported";

        public List<string> UnsupportedExtensions { get; set; } = [];

        public override string ToString() => string.Join(", ", UnsupportedExtensions);
    }
}
