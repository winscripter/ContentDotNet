namespace ContentDotNet.Audio.Formats.Ogg
{
    using ContentDotNet.Api.Primitives;

    public class OggReader : IDisposable
    {
        private readonly Stream _stream;
        private readonly BinaryReader _reader;

        public OggReader(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _reader = new BinaryReader(_stream);
        }

        /// <summary>
        /// Reads the next OGG page from the stream. Returns null on EOF.
        /// </summary>
        public OggPage? ReadPage()
        {
            if (_stream.Position >= _stream.Length)
                return null;

            // Capture pattern
            var magic = new FourCC(_reader.ReadUInt32());
            if (!magic.Equals(OggConstants.Magic))
                throw new InvalidDataException("Invalid Ogg page magic; expected 'OggS'.");

            var header = new OggPageHeader
            {
                Magic = magic,
                Version = _reader.ReadByte(),
                HeaderType = _reader.ReadByte(),
                GranulePosition = _reader.ReadInt64(),
                SerialNumber = _reader.ReadUInt32(),
                SequenceNumber = _reader.ReadUInt32(),
                Checksum = _reader.ReadUInt32(),
                PageSegments = _reader.ReadByte()
            };

            // Segment table
            List<byte> segmentTable = [.. _reader.ReadBytes(header.PageSegments)];
            var totalSize = 0;
            foreach (var s in segmentTable)
                totalSize += s;

            // Segment data
            List<byte> data = [.. _reader.ReadBytes(totalSize)];

            return new OggPage
            {
                Header = header,
                SegmentTable = segmentTable,
                SegmentData = data
            };
        }

        public void Dispose()
        {
            _reader.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
