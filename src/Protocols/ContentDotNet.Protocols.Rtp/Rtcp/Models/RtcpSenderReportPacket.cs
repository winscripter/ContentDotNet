namespace ContentDotNet.Protocols.Rtp.Rtcp.Models
{
    /// <summary>
    ///   A sender report RTCP packet.
    /// </summary>
    public record RtcpSenderReportPacket : IRtcpPacket
    {
        /// <summary>
        ///   The version
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        ///   The padding.
        /// </summary>
        public bool Padding { get; set; }

        /// <summary>
        ///   The reception report count.
        /// </summary>
        public uint ReceptionReportCount { get; set; }

        /// <summary>
        ///   The packet type.
        /// </summary>
        public uint PacketType { get; set; }

        /// <summary>
        ///   The length.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        ///   The SSRC.
        /// </summary>
        public uint Ssrc { get; set; }

        /// <summary>
        ///   The NTP timestamp.
        /// </summary>
        public ulong NtpTimestamp { get; set; }

        /// <summary>
        ///   The RTP timestamp.
        /// </summary>
        public uint RtpTimestamp { get; set; }

        /// <summary>
        ///   The sender packet count.
        /// </summary>
        public uint SenderPacketCount { get; set; }

        /// <summary>
        ///   The sender octet count.
        /// </summary>
        public uint SenderOctetCount { get; set; }

        /// <summary>
        ///   The SSRC N.
        /// </summary>
        public uint SsrcN { get; set; }

        /// <summary>
        ///   The fraction lost.
        /// </summary>
        public uint FractionLost { get; set; }

        /// <summary>
        ///   The cumulative number of packets lost.
        /// </summary>
        public uint CumulativeNumberOfPacketsLost { get; set; }

        /// <summary>
        ///   The extended highest sequence number received.
        /// </summary>
        public uint ExtendedHighestSequenceNumberReceived { get; set; }

        /// <summary>
        ///   Interarrival jitter
        /// </summary>
        public uint InterarrivalJitter { get; set; }

        /// <summary>
        ///   The last SR timestamp.
        /// </summary>
        public uint LastSRTimestamp { get; set; }

        /// <summary>
        ///   The delay since last SR.
        /// </summary>
        public uint DelaySinceLastSR { get; set; }

        public RtcpSenderReportPacket(uint version, bool padding, uint receptionReportCount, uint packetType, uint length, uint ssrc, ulong ntpTimestamp, uint rtpTimestamp, uint senderPacketCount, uint senderOctetCount, uint ssrcN, uint fractionLost, uint cumulativeNumberOfPacketsLost, uint extendedHighestSequenceNumberReceived, uint interarrivalJitter, uint lastSRTimestamp, uint delaySinceLastSR)
        {
            Version = version;
            Padding = padding;
            ReceptionReportCount = receptionReportCount;
            PacketType = packetType;
            Length = length;
            Ssrc = ssrc;
            NtpTimestamp = ntpTimestamp;
            RtpTimestamp = rtpTimestamp;
            SenderPacketCount = senderPacketCount;
            SenderOctetCount = senderOctetCount;
            SsrcN = ssrcN;
            FractionLost = fractionLost;
            CumulativeNumberOfPacketsLost = cumulativeNumberOfPacketsLost;
            ExtendedHighestSequenceNumberReceived = extendedHighestSequenceNumberReceived;
            InterarrivalJitter = interarrivalJitter;
            LastSRTimestamp = lastSRTimestamp;
            DelaySinceLastSR = delaySinceLastSR;
        }
    }
}
