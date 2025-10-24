namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Helpers;
    using System.Text;

    internal class TransportImpl : TransportBase, IRtspTransportHeader
    {
        public override string Text => "Transport";

        public string? MediaDeliveryProtocol { get; set; }
        public string? TransportMethod { get; set; }
        public string? Ssrc { get; set; }
        public string? SourceAddress { get; set; }
        public string? DestinationAddress { get; set; }
        public string? TimeToLive { get; set; }
        public string? Mode { get; set; }
        public bool Interleaved { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            bool applied = false;
            if (MediaDeliveryProtocol != null)
            {
                sb.Append($"{MediaDeliveryProtocol}");
                applied = true;
            }
            if (DestinationAddress != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"dest_addr={DestinationAddress}");
                applied = true;
            }
            if (SourceAddress != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"src_addr={SourceAddress}");
                applied = true;
            }
            if (TimeToLive != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"ttl={TimeToLive}");
                applied = true;
            }
            if (Mode != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"mode={Mode}");
                applied = true;
            }
            if (Interleaved)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"interleaved=1");
                applied = true;
            }
            if (Ssrc != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"ssrc={Ssrc}");
                applied = true;
            }
            return sb.ToString();
        }
    }
}
