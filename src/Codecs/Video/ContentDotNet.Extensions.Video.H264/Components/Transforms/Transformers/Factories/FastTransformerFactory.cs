namespace ContentDotNet.Extensions.Video.H264.Components.Transforms.Transformers.Factories
{
    /// <summary>
    ///   Factory for a transformer with decent performance and memory usage.
    /// </summary>
    public class FastTransformerFactory : ITransformerFactory
    {
        public static readonly FastTransformerFactory Instance = new();

        /// <inheritdoc cref="ITransformerFactory.CreateTransformer" />
        public ICoefficientTransformer CreateTransformer()
        {
            return FastTransformer.Instance;
        }
    }
}
