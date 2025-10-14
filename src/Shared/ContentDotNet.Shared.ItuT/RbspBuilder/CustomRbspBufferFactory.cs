namespace ContentDotNet.Shared.ItuT.RbspBuilder
{
    /// <summary>
    ///   An RBSP buffer factory that uses a provided delegate to create <see cref="IItuRbspBufferBuilder"/>.
    /// </summary>
    public class CustomRbspBufferFactory : IItuRbspBufferFactory
    {
        private readonly Func<IItuRbspBufferBuilder> _createBuilder;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CustomRbspBufferFactory"/> class.
        /// </summary>
        /// <param name="createBuilder">The create builder delegate</param>
        public CustomRbspBufferFactory(Func<IItuRbspBufferBuilder> createBuilder)
        {
            _createBuilder = createBuilder;
        }

        /// <inheritdoc cref="IItuRbspBufferFactory.CreateBuilder"/>
        public IItuRbspBufferBuilder CreateBuilder()
        {
            return this._createBuilder();
        }
    }
}
