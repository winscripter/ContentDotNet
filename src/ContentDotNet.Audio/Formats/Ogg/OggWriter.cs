namespace ContentDotNet.Audio.Formats.Ogg
{
    public class OggWriter : IDisposable
    {
        private readonly Stream _stream;
        private readonly BinaryWriter _writer;

        public OggWriter(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _writer = new BinaryWriter(_stream);
        }

        public void WritePage(OggPage page)
        {
            var h = page.Header;

            // Compute checksum later — write header with 0 temporarily.
            using var ms = new MemoryStream();
            using var tempWriter = new BinaryWriter(ms);

            tempWriter.Write(h.Magic.Value);
            tempWriter.Write(h.Version);
            tempWriter.Write(h.HeaderType);
            tempWriter.Write(h.GranulePosition);
            tempWriter.Write(h.SerialNumber);
            tempWriter.Write(h.SequenceNumber);
            tempWriter.Write(0u); // Placeholder checksum
            tempWriter.Write((byte)page.SegmentTable.Count);
            tempWriter.Write(page.SegmentTable.ToArray());
            tempWriter.Write(page.SegmentData.ToArray());

            // Compute CRC32
            var bytes = ms.ToArray();
            var checksum = ComputeOggCrc32(bytes);

            // Patch in checksum (bytes 22–25)
            BitConverter.GetBytes(checksum).CopyTo(bytes, 22);

            // Write to final stream
            _writer.Write(bytes);
        }

        private static uint ComputeOggCrc32(byte[] data)
        {
            // OGG uses a standard 32-bit CRC polynomial (0x04C11DB7)
            const uint poly = 0x04C11DB7;
            uint crc = 0;
            foreach (var b in data)
            {
                crc ^= (uint)(b << 24);
                for (int i = 0; i < 8; i++)
                    crc = (crc & 0x80000000) != 0
                        ? (crc << 1) ^ poly
                        : (crc << 1);
            }
            return crc;
        }

        public void Dispose()
        {
            _writer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
