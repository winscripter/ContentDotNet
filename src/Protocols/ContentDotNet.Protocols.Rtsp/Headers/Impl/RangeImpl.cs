namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Helpers;
    using static ContentDotNet.Protocols.Rtsp.Helpers.StaticStringUtils;

    internal class RangeImpl : RangeBase, IRtspRangeHeader
    {
        public override string Text => "Range";

        public string? Clock { get; set; }
        public string? NormalPlayTime { get; set; }

        public override string ToString()
        {
            var kvb = new KeyValueBuilder();
            kvb.SetIfValueNotNull("clock", QuotedOrNull(Clock));
            kvb.SetIfValueNotNull("npt", QuotedOrNull(NormalPlayTime));
            return kvb.BuildCommaSeparatedString();
        }
    }
}
