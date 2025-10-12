namespace ContentDotNet.Shared.ItuT.RbspBuilder
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   ITU RBSP Builder Buffer (for H.26x)
    /// </summary>
    public class MemoryRbspBufferBuilder : IItuRbspBufferBuilder
    {
        private readonly MemoryStream memoryStream;

        /// <summary>
        ///   Initializes a new instance of the <see cref="MemoryRbspBufferBuilder"/> class.
        /// </summary>
        public MemoryRbspBufferBuilder()
        {
            this.memoryStream = new MemoryStream();
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
            this.memoryStream.WriteByte(b);
        }

        /// <inheritdoc cref="IItuRbspBufferBuilder.FeedByteAsync(byte)" />
        public async Task FeedByteAsync(byte b)
        {
            Memory<byte> mem = new(new byte[1]);
            mem.Span[0] = b;
            await this.memoryStream.WriteAsync(mem);
        }
    }
}
