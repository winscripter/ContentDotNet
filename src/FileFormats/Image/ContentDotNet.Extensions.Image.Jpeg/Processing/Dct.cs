namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    using ContentDotNet.Extensions.Image.Jpeg.Processing.Tables;

    internal static class Dct
    {
        private static readonly float OneDividedBySqrt2 = 1 / MathF.Sqrt(2);
        private const float One = 1F;
        private const float OneDividedByFour = 1F / 4F;

        public static float Apply(int a, int b, ValueBlock8x8F table)
        {
            float intermediate = 0F;

            for (int c = 0; c <= 7; c++)
            {
                for (int d = 0; d <= 7; d++)
                {
                    intermediate +=
                        Cu(c) * Cv(d) * table[d, c] * DctTables.Table[b * 8 + c] * DctTables.Table[a * 8 + d];
                }
            }

            return OneDividedByFour * intermediate;
        }

        public static float Fdct(int v, int u, ValueBlock8x8F syx) => Apply(v, u, syx);
        public static float Idct(int y, int x, ValueBlock8x8F svu) => Apply(y, x, svu);

        private static float Cx(float x)
        {
            if (x == 0F) return OneDividedBySqrt2;
            else return One;
        }

        private static float Cu(float u) => Cx(u);
        private static float Cv(float v) => Cx(v);
    }
}
