namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    internal static class DctCoefficientQuantizer
    {
        public static float Sq(
            ReadOnlySpan<float> S,
            ReadOnlySpan<float> Q,
            int vu)
            => MathF.Round(S[vu] / Q[vu]);

        public static float R(
            int vu,
            ReadOnlySpan<float> S,
            ReadOnlySpan<float> Q)
            => Sq(S, Q, vu) * Q[vu];

        public static float Diff(
            ReadOnlySpan<float> DC,
            float PRED,
            int i)
            => DC[i] - PRED;
    }
}
