namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;

    internal class FromImpl : FromBase, IRtspFromHeader
    {
        public override string Text => "From";

        public string? EmailAddress { get; set; }

        public override string ToString() => EmailAddress ?? string.Empty;
    }
}
