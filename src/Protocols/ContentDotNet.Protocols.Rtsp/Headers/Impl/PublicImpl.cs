namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class PublicImpl : PublicBase, IRtspPublicHeader
    {
        public override string Text => "Public";

        public List<string> Methods { get; set; } = [];

        public override string ToString() => string.Join(", ", Methods);
    }
}
