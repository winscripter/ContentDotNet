namespace ContentDotNet.Shared.ItuT.RbspBuilder
{
    /// <summary>
    ///   ITU-T RBSP buffer factory
    /// </summary>
    public interface IItuRbspBufferFactory
    {
        /// <summary>
        ///   Creates an ITU-T RBSP Buffer builder.
        /// </summary>
        /// <returns>The builder.</returns>
        IItuRbspBufferBuilder CreateBuilder();
    }
}
