namespace ContentDotNet.Audio.Formats.Ogg
{
    using System;
    using System.IO;

    /// <summary>
    ///   Provides a high level abstraction for reading raw packet data off of OGG files. This makes
    ///   multiplexing and demultiplexing very easy.
    /// </summary>
    /// <remarks>
    ///   Note: This stream is not seekable. To make it seekable, you might have to copy chunks of data
    ///   into a stream that supports seeking, like FileStream or MemoryStream. Don't copy the entire file
    ///   into the seekable streams, as that's inefficient.
    /// </remarks>
    public class OggStream : Stream
    {
        private readonly Stream _baseStream;
        private readonly OggReader? _reader;
        private readonly OggWriter? _writer;

        private OggPage? _currentPage;
        private int _pageOffset;

        private readonly bool _canRead;
        private readonly bool _canWrite;

        public OggStream(Stream baseStream, bool forReading)
        {
            _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));

            if (forReading)
            {
                _reader = new OggReader(baseStream);
                _canRead = true;
            }
            else
            {
                _writer = new OggWriter(baseStream);
                _canWrite = true;
            }
        }

        // --- Stream capabilities ---
        public override bool CanRead => _canRead;
        public override bool CanWrite => _canWrite;
        public override bool CanSeek => false; // Ogg pages aren't random-access friendly
        public override long Length => throw new NotSupportedException();
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        // --- Reading bytes across pages ---
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!_canRead) throw new NotSupportedException("Stream not readable.");
            ArgumentNullException.ThrowIfNull(buffer);

            int totalRead = 0;
            while (count > 0)
            {
                if (_currentPage == null || _pageOffset >= _currentPage.SegmentData.Count)
                {
                    _currentPage = _reader!.ReadPage();
                    if (_currentPage == null)
                        break; // EOF

                    _pageOffset = 0;
                }

                int remainingInPage = _currentPage.SegmentData.Count - _pageOffset;
                int toRead = Math.Min(count, remainingInPage);
                byte[] segmentDataAsByteArray = [.. _currentPage.SegmentData];
                Array.Copy(segmentDataAsByteArray, _pageOffset, buffer, offset, toRead);

                _pageOffset += toRead;
                offset += toRead;
                count -= toRead;
                totalRead += toRead;
            }

            return totalRead;
        }

        public override int ReadByte()
        {
            Span<byte> one = stackalloc byte[1];
            int read = Read(one.ToArray(), 0, 1);
            return read == 0 ? -1 : one[0];
        }

        // --- Writing bytes into pages ---
        private readonly MemoryStream _writeBuffer = new();

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!_canWrite) throw new NotSupportedException("Stream not writable.");
            ArgumentNullException.ThrowIfNull(buffer);

            // Write into buffer first
            _writeBuffer.Write(buffer, offset, count);

            // Flush if buffer exceeds typical page size (~64kB)
            if (_writeBuffer.Length >= 65536)
                FlushPage();
        }

        public override void WriteByte(byte value)
        {
            Span<byte> one = [value];
            Write(one.ToArray(), 0, 1);
        }

        private void FlushPage()
        {
            if (_writeBuffer.Length == 0) return;

            byte[] data = _writeBuffer.ToArray();
            _writeBuffer.SetLength(0);

            // Split into segment table (255-byte chunks)
            var segments = new List<byte>();
            int offset = 0;
            while (offset < data.Length)
            {
                int chunk = Math.Min(255, data.Length - offset);
                segments.Add((byte)chunk);
                offset += chunk;
            }

            var page = new OggPage
            {
                Header = new OggPageHeader
                {
                    Magic = OggConstants.Magic,
                    Version = 0,
                    HeaderType = 0,
                    GranulePosition = 0,
                    SerialNumber = 1,
                    SequenceNumber = 0, // Could increment for real streams
                    Checksum = 0,
                    PageSegments = (byte)segments.Count
                },
                SegmentTable = [.. segments.ToArray()],
                SegmentData = [.. data]
            };

            _writer!.WritePage(page);
        }

        public override void Flush()
        {
            if (_canWrite)
                FlushPage();

            _baseStream.Flush();
        }

        // --- Seek not supported for OGG ---
        public override long Seek(long offset, SeekOrigin origin) =>
            throw new NotSupportedException();

        public override void SetLength(long value) =>
            throw new NotSupportedException();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_canWrite)
                    FlushPage();

                _writer?.Dispose();
                _reader?.Dispose();
                _baseStream.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
