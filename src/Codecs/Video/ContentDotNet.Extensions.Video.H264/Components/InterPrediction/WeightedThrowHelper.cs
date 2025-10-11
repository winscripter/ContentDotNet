namespace ContentDotNet.Extensions.Video.H264.Components.InterPrediction
{
    using ContentDotNet.Extensions.Video.H264.Exceptions;

    internal static class WeightedThrowHelper
    {
        private static readonly WeightedPredictionException wpx_PwtMissing =
            new("The prediction weight table is missing. Therefore, explicit prediction weight derivation cannot proceed");

        public static WeightedPredictionException PredWeightTableMissing() => wpx_PwtMissing;
    }
}
