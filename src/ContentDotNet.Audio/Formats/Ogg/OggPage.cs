namespace ContentDotNet.Audio.Formats.Ogg
{
    using ContentDotNet.Api.Primitives;

    /// <summary>
    ///   OGG page.
    /// </summary>
    public class OggPage
    {
        /// <summary>
        ///   The header of the OGG page.
        /// </summary>
        public OggPageHeader Header { get; set; } = default;

        /// <summary>
        ///   The segment table.
        /// </summary>
        public List<byte> SegmentTable { get; set; } = [];

        /// <summary>
        ///   Raw segment data bytes.
        /// </summary>
        public List<byte> SegmentData { get; set; } = [];
    }

    /// <summary>
    ///   OGG page header.
    /// </summary>
    public struct OggPageHeader
    {
        /// <summary>
        ///   Equal to "OggS".
        /// </summary>
        public FourCC Magic;

        /// <summary>
        ///   The version.
        /// </summary>
        public byte Version;

        /// <summary>
        ///   Header type flags (bitfield). 0x01 = continuation, 0x02 = beginning-of-stream, 0x04 = end-of-stream.
        /// </summary>
        public byte HeaderType;

        /// <summary>
        ///   Granule position.
        /// </summary>
        public long GranulePosition;

        /// <summary>
        ///   Serial number.
        /// </summary>
        public uint SerialNumber;

        /// <summary>
        ///   The sequence number.
        /// </summary>
        public uint SequenceNumber;

        /// <summary>
        ///   The checksum.
        /// </summary>
        public uint Checksum;

        /// <summary>
        ///   The page segments.
        /// </summary>
        public byte PageSegments;
    }
}
