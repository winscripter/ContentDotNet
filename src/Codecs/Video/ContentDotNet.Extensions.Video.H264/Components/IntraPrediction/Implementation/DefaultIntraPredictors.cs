namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation
{
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation.Predictors;

    internal class DefaultIntraPredictors : IIntraPredictors
    {
        public DefaultIntraPredictors()
        {
            Intra4x4Predictor = new DefaultIntra4x4Predictor();
            Intra8x8Predictor = new DefaultIntra8x8Predictor();
            Intra16x16Predictor = new DefaultIntra16x16Predictor();
            IntraChromaPredictor = new DefaultIntraChromaPredictor();
            IntraPcmPredictor = new DefaultIntraPcmPredictor();
        }

        public IIntra4x4Predictor Intra4x4Predictor { get; }

        public IIntra8x8Predictor Intra8x8Predictor { get; }

        public IIntra16x16Predictor Intra16x16Predictor { get; }

        public IIntraChromaPredictor IntraChromaPredictor { get; }

        public IIntraPcmPredictor IntraPcmPredictor { get; }
    }
}
