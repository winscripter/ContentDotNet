namespace ContentDotNet.Video.Shared.ItuT.RbspBuilder
{
    /// <summary>
    ///   ITU-T RBSP Buffer builder.
    /// </summary>
    public interface IItuRbspBufferBuilder : IDisposable, IAsyncDisposable
    {
        /// <summary>
        ///   Feeds a byte.
        /// </summary>
        /// <param name="b">A byte</param>
        void FeedByte(byte b);

        /// <summary>
        ///   Asynchronously feeds a byte.
        /// </summary>
        /// <param name="b">Byte to feed</param>
        /// <returns>An awaitable task</returns>
        Task FeedByteAsync(byte b);

        /// <summary>
        ///   Creates the stream.
        /// </summary>
        /// <returns>A stream</returns>
        Stream CreateStream();
    }
}
