namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction
{
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation;

    /// <summary>
    ///   Serves as the default factory for H.264 intra predictors.
    /// </summary>
    public class DefaultIntraPredictorsFactory : IIntraPredictorsFactory
    {
        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly DefaultIntraPredictorsFactory Instance = new();

        /// <inheritdoc cref="IIntraPredictorsFactory.CreatePredictors" />
        public IIntraPredictors CreatePredictors()
        {
            return new DefaultIntraPredictors();
        }
    }
}
