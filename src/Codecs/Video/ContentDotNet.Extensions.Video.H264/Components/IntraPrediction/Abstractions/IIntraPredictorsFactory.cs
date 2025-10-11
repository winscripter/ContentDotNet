namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions
{
    /// <summary>
    ///   Factory for <see cref="IIntraPredictors"/>.
    /// </summary>
    public interface IIntraPredictorsFactory
    {
        /// <summary>
        ///   Creates intra predictors.
        /// </summary>
        /// <returns>The intra predictors.</returns>
        IIntraPredictors CreatePredictors();
    }
}
