namespace ContentDotNet.Protocols.Rtp.Models
{
    /// <summary>
    ///   The RTP header.
    /// </summary>
    public record RtpHeader
    {
        /// <summary>
        ///   The version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        ///   The padding.
        /// </summary>
        public bool Padding { get; set; }

        /// <summary>
        ///   The extension.
        /// </summary>
        public bool Extension { get; set; }

        /// <summary>
        ///   The CSRC count.
        /// </summary>
        public uint CsrcCount { get; set; }

        /// <summary>
        ///   The marker.
        /// </summary>
        public bool Marker { get; set; }

        /// <summary>
        ///   Payload type.
        /// </summary>
        public uint PayloadType { get; set; }

        /// <summary>
        ///   The sequence number.
        /// </summary>
        public uint SequenceNumber { get; set; }

        /// <summary>
        ///   The timestamp.
        /// </summary>
        public uint Timestamp { get; set; }

        /// <summary>
        ///   The SSRC.
        /// </summary>
        public uint Ssrc { get; set; }

        /// <summary>
        ///   The CSRC list. There are <see cref="CsrcCount"/> items.
        /// </summary>
        public IList<uint> CsrcList { get; set; } = [];
    }
}
