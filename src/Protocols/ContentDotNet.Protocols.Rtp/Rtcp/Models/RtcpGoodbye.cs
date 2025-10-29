namespace ContentDotNet.Protocols.Rtp.Rtcp.Models
{
    /// <summary>
    ///   The goodbye.
    /// </summary>
    public record RtcpGoodbye : IRtcpPacket
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
        ///   The packet type.
        /// </summary>
        public uint PacketType { get; set; }

        /// <summary>
        ///   The length.
        /// </summary>
        public uint Length { get; set; }

        public RtcpGoodbye(uint version, bool padding, uint packetType, uint length)
        {
            Version = version;
            Padding = padding;
            PacketType = packetType;
            Length = length;
        }
    }
}
