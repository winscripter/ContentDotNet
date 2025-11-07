namespace ContentDotNet.Video.Shared.ItuT.RbspBuilder
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   ITU RBSP Builder Buffer (for H.26x)
    /// </summary>
    public class MemoryRbspBufferBuilder : IItuRbspBufferBuilder
    {
        private readonly MemoryStream memoryStream;
        private int maxSize = -1; // Unlimited

        /// <summary>
        ///   Initializes a new instance of the <see cref="MemoryRbspBufferBuilder"/> class.
        /// </summary>
        public MemoryRbspBufferBuilder()
        {
            this.memoryStream = new MemoryStream();
        }

        /// <summary>
        ///   The maximum size of the RBSP in-memory buffer.
        /// </summary>
        public int MaxSize
        {
            get => maxSize;
            set => maxSize = value;
        }

        /// <inheritdoc cref="IItuRbspBufferBuilder.CreateStream" />
        public Stream CreateStream()
        {
            this.memoryStream.Position = 0;
            return this.memoryStream;
        }

        /// <inheritdoc cref="IDisposable.Dispose" />
        public void Dispose()
        {
            this.memoryStream.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IAsyncDisposable.DisposeAsync" />
        public async ValueTask DisposeAsync()
        {
            await this.memoryStream.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IItuRbspBufferBuilder.FeedByte(byte)" />
        public void FeedByte(byte b)
        {
            if (this.memoryStream.Length > this.maxSize &&
                this.maxSize >= 0)
            {
                throw new InvalidOperationException("Too many bytes were fed into the in-memory RBSP builder buffer");
            }
            
            this.memoryStream.WriteByte(b);
        }

        /// <inheritdoc cref="IItuRbspBufferBuilder.FeedByteAsync(byte)" />
        public async Task FeedByteAsync(byte b)
        {
            if (this.memoryStream.Length > this.maxSize &&
                this.maxSize >= 0)
            {
                throw new InvalidOperationException("Too many bytes were fed into the in-memory RBSP builder buffer");
            }

            Memory<byte> mem = new(new byte[1]);
            mem.Span[0] = b;
            await this.memoryStream.WriteAsync(mem);
        }
    }
}
