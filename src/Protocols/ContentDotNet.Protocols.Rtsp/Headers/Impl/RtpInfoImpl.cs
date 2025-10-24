namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;
    using ContentDotNet.Protocols.Rtsp.Helpers;
    using System.Collections.Generic;
    using static ContentDotNet.Protocols.Rtsp.Helpers.StaticStringUtils;

    internal class RtpInfoImpl : RtpInfoBase, IRtspRtpInfoHeader
    {
        public override string Text => "RTP-Info";

        public List<RtpInfoRecord> Value { get; set; } = [];

        public override string ToString()
        {
            return string.Join(", ", Value.Select(x =>
            {
                var kvb = new KeyValueBuilder();
                kvb.SetIfValueNotNull("url", QuotedOrNull(x.Url));
                kvb.SetIfValueNotNull("seq", QuotedOrNull(x.Seq));
                kvb.SetIfValueNotNull("ssrc", QuotedOrNull(x.Ssrc));
                kvb.SetIfValueNotNull("rtptime", QuotedOrNull(x.RtpTime));
                return kvb.ToString();
            }));
        }
    }
}
