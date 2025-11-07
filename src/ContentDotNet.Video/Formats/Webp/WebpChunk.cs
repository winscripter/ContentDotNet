namespace ContentDotNet.Video.Formats.Webp
{
    using ContentDotNet.Api.Primitives;

    /// <summary>
    ///   The webp chunk.
    /// </summary>
    public class WebpChunk : IDisposable
    {
        private bool _yetDisposed = false;

        /// <summary>
        ///   Size of the chunk.
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        ///   Type of the chunk.
        /// </summary>
        public FourCC Type { get; set; }

        /// <summary>
        ///   Chunk payload (data).
        /// </summary>
        public Stream Payload { get; set; }

        public WebpChunk(uint size, FourCC type, Stream payload)
        {
            Size = size;
            Type = type;
            Payload = payload;
        }

        /// <summary>
        ///   Releases unmanaged memory.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            ObjectDisposedException.ThrowIf(_yetDisposed, this);
            _yetDisposed = true;
            this.Payload.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
