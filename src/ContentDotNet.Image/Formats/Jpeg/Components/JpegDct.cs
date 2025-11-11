namespace ContentDotNet.Image.Formats.Jpeg.Components
{
    internal static class JpegDct
    {
        private static readonly float OneDividedBySqrtOf2 = 1f / MathF.Sqrt(2f);
        private const float OneDividedBy4 = 0.25f;

        public static void Fdct8x8(ReadOnlySpan<float> src, Span<float> dst)
        {
            for (int v = 0; v < 8; v++)
            {
                for (int u = 0; u < 8; u++)
                {
                    float sum = 0f;
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            sum += src[y * 8 + x] *
                                   MathF.Cos(((2 * x + 1) * u * MathF.PI) / 16f) *
                                   MathF.Cos(((2 * y + 1) * v * MathF.PI) / 16f);
                        }
                    }

                    dst[v * 8 + u] = OneDividedBy4 * Cx(u) * Cx(v) * sum;
                }
            }
        }

        public static void Idct8x8(ReadOnlySpan<float> src, Span<float> dst)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    float sum = 0f;
                    for (int v = 0; v < 8; v++)
                    {
                        for (int u = 0; u < 8; u++)
                        {
                            sum += Cx(u) * Cx(v) * src[v * 8 + u] *
                                   MathF.Cos(((2 * x + 1) * u * MathF.PI) / 16f) *
                                   MathF.Cos(((2 * y + 1) * v * MathF.PI) / 16f);
                        }
                    }

                    dst[y * 8 + x] = OneDividedBy4 * sum;
                }
            }
        }

        private static float Cx(int x) => x == 0 ? OneDividedBySqrtOf2 : 1f;
    }
}
