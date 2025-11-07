namespace ContentDotNet.Video.Shared.ItuT.RbspBuilder
{
    /// <summary>
    ///   In-memory RBSP buffer factory
    /// </summary>
    public class MemoryRbspBufferFactory : IItuRbspBufferFactory
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="MemoryRbspBufferFactory"/> class.
        /// </summary>
        public static readonly MemoryRbspBufferFactory Instance = new();

        /// <inheritdoc cref="IItuRbspBufferFactory.CreateBuilder" />
        public IItuRbspBufferBuilder CreateBuilder()
        {
            return new MemoryRbspBufferBuilder();
        }
    }
}
