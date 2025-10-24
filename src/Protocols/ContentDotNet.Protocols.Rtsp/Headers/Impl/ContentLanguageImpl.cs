namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Collections.Generic;

    internal class ContentLanguageImpl : ContentLanguageBase, IRtspContentLanguageHeader
    {
        public override string Text => "Content-Language";

        public List<string> Languages { get; set; } = [];

        public override string ToString() => string.Join(", ", Languages);
    }
}
