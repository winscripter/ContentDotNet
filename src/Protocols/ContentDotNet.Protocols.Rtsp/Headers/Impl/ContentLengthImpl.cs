namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class ContentLengthImpl : ContentLengthBase, IRtspContentLengthHeader
    {
        public override string Text => "Content-Length";

        public int Length { get; set; }

        public override string ToString() => Length.ToString();
    }
}
