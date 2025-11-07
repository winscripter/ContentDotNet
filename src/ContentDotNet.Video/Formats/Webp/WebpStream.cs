namespace ContentDotNet.Video.Formats.Webp
{
    using ContentDotNet.Api.Binary;
    using ContentDotNet.Api.Primitives;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///   WebP file stream.
    /// </summary>
    public class WebpStream : IDisposable, IAsyncDisposable
    {
        private readonly Stream _webp;
        private readonly BinaryReader _cachedBinaryReader;
        private readonly BinaryWriter _cachedBinaryWriter;

        private bool _disposed;

        /// <summary>
        ///   Initializes a new instance of the <see cref="WebpStream"/> class.
        /// </summary>
        /// <param name="webp">The input Stream.</param>
        public WebpStream(Stream webp)
        {
            this._webp = webp;
            this._cachedBinaryReader = new EndianAwareBinaryReader(webp, Endianness.LittleEndian, Encoding.UTF8, true);
            this._cachedBinaryWriter = new EndianAwareBinaryWriter(webp, Endianness.LittleEndian, Encoding.UTF8, true);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="WebpStream"/> class.
        /// </summary>
        /// <param name="webp">The input Stream.</param>
        public WebpStream(Stream webp, BinaryReader reader, BinaryWriter writer)
            : this(webp)
        {
            this._cachedBinaryReader = reader;
            this._cachedBinaryWriter = writer;
        }

        /// <summary>
        ///   The WebP stream.
        /// </summary>
        public Stream Stream => _webp;

        /// <summary>
        ///   The maximum bytes of data per chunk. Set to a negative value to allow infinitely large values.
        /// </summary>
        public int LimitPerChunkData { get; set; } = DataSize.Megabytes(128);

        /// <summary>
        ///   Releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _disposed = true;

            this._webp.Dispose();
            this._cachedBinaryReader.Dispose();
            this._cachedBinaryWriter.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged resources.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _disposed = true;

            await this._webp.DisposeAsync();
            this._cachedBinaryReader.Dispose();
            this._cachedBinaryWriter.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Reads a single WebP chunk.
        /// </summary>
        /// <param name="dataBuffer">Buffer for WebP data</param>
        /// <returns>The WebP chunk</returns>
        public WebpChunk ReadChunk(Stream dataBuffer)
        {
            FourCC type = this._cachedBinaryReader.ReadUInt32();
            uint size = this._cachedBinaryReader.ReadUInt32();

            for (int i = 0; i < size; i++)
            {
                if (LimitPerChunkData <= i && i > 0)
                {
                    throw new InvalidOperationException("Too many bytes read");
                }

                int currentByte = this._cachedBinaryReader.ReadByte();
                if (currentByte == -1)
                {
                    throw new EndOfStreamException();
                }

                dataBuffer.WriteByte((byte)currentByte);
            }

            return new WebpChunk(size, type, dataBuffer);
        }

        /// <summary>
        ///   Writes the single WebP chunk.
        /// </summary>
        /// <param name="chunk">The chunk to write.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void WriteChunk(WebpChunk chunk)
        {
            this._cachedBinaryWriter.Write(chunk.Type);
            this._cachedBinaryWriter.Write(chunk.Size);

            int idx = 0;
            while (chunk.Payload.Position < chunk.Payload.Length)
            {
                if (idx++ >= this.LimitPerChunkData)
                {
                    throw new InvalidOperationException("Too many bytes written");
                }

                this._cachedBinaryWriter.Write((byte)chunk.Payload.ReadByte());
            }
        }
    }
}
