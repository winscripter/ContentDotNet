namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions
{
    /// <summary>
    ///   Intra predictors.
    /// </summary>
    public interface IIntraPredictors
    {
        /// <summary>
        ///   The 4x4 intra predictor.
        /// </summary>
        IIntra4x4Predictor Intra4x4Predictor { get; }

        /// <summary>
        ///   The 8x8 intra predictor.
        /// </summary>
        IIntra8x8Predictor Intra8x8Predictor { get; }

        /// <summary>
        ///   The 16x16 intra predictor.
        /// </summary>
        IIntra16x16Predictor Intra16x16Predictor { get; }

        /// <summary>
        ///   The Chroma intra predictor.
        /// </summary>
        IIntraChromaPredictor IntraChromaPredictor { get; }

        /// <summary>
        ///   The PCM intra predictor.
        /// </summary>
        IIntraPcmPredictor IntraPcmPredictor { get; }
    }
}
