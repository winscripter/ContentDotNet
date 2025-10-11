namespace ContentDotNet.Extensions.Video.H264.Components.Transforms.Transformers.Factories
{
    /// <summary>
    ///   The transformer factory.
    /// </summary>
    public interface ITransformerFactory
    {
        /// <summary>
        ///   Creates the transformer.
        /// </summary>
        /// <returns>The abstract transformer.</returns>
        ICoefficientTransformer CreateTransformer();
    }
}
