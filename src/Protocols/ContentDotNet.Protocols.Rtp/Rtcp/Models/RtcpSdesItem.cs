namespace ContentDotNet.Protocols.Rtp.Rtcp.Models
{
    /// <summary>
    ///   SDES item.
    /// </summary>
    public record RtcpSdesItem : IRtcpPacket
    {
        /// <summary>
        ///   The version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        ///   The length.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        ///   The data.
        /// </summary>
        public byte[] Data { get; set; }

        public RtcpSdesItem(uint version, uint length, byte[] data)
        {
            Version = version;
            Length = length;
            Data = data;
        }
    }
}
