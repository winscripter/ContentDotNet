namespace ContentDotNet.Extensions.Video.H264.Components.Transforms.Transformers
{
    using ContentDotNet.Primitives;

    internal class FastTransformer : ICoefficientTransformer
    {
        public static readonly FastTransformer Instance = new();

        private static readonly int TwoPowFive = (int)Math.Pow(2, 5);

        public void TransformChromaDcTransformCoefficients(
            int[,] f,
            int[,] c,
            int ChromaArrayType)
        {
            if (ChromaArrayType == 1)
            {
                // Compute intermediate result: d = A * c
                int d00 = c[0, 0] + c[1, 0];
                int d01 = c[0, 1] + c[1, 1];
                int d10 = c[0, 0] - c[1, 0];
                int d11 = c[0, 1] - c[1, 1];

                // Final f = d * B
                f[0, 0] = d00 + d01;
                f[0, 1] = d00 - d01;
                f[1, 0] = d10 + d11;
                f[1, 1] = d10 - d11;
            }
            else
            {
                // f = M_left * c * M_right
                // Where c is 4x2, f will be 4x4

                int[,] d = new int[4, 2];

                // M_left * c
                for (int i = 0; i < 4; i++)
                {
                    int s0 = (i & 1) == 0 ? 1 : -1;
                    int s1 = (i & 2) == 0 ? 1 : -1;

                    d[i, 0] = c[0, 0] + s0 * c[1, 0] + s1 * c[2, 0] + s0 * s1 * c[3, 0];
                    d[i, 1] = c[0, 1] + s0 * c[1, 1] + s1 * c[2, 1] + s0 * s1 * c[3, 1];
                }

                // d * M_right
                for (int i = 0; i < 4; i++)
                {
                    f[i, 0] = d[i, 0] + d[i, 1];
                    f[i, 1] = d[i, 0] - d[i, 1];
                }
            }
        }

        public void TransformResidual4x4Blocks(
            int[,] dij,
            int[,] rij)
        {
            Span<int> eBuffer = stackalloc int[Value2DArray.GetNumberOfElements(4, 4)];
            var e = new Value2DArray(eBuffer, 4, 4);

            for (int i = 0; i < 4; i++) e[i, 0] = dij[i, 0] + dij[i, 2];
            for (int i = 0; i < 4; i++) e[i, 1] = dij[i, 0] - dij[i, 2];
            for (int i = 0; i < 4; i++) e[i, 2] = (dij[i, 1] >> 1) - dij[i, 3];
            for (int i = 0; i < 4; i++) e[i, 3] = dij[i, 1] + (dij[i, 3] >> 1);

            Span<int> fBuffer = stackalloc int[Value2DArray.GetNumberOfElements(4, 4)];
            var f = new Value2DArray(fBuffer, 4, 4);

            for (int i = 0; i < 4; i++) f[i, 0] = e[i, 0] + e[i, 3];
            for (int i = 0; i < 4; i++) f[i, 1] = e[i, 1] + e[i, 2];
            for (int i = 0; i < 4; i++) f[i, 2] = e[i, 1] - e[i, 2];
            for (int i = 0; i < 4; i++) f[i, 3] = e[i, 0] - e[i, 3];

            Span<int> gBuffer = stackalloc int[Value2DArray.GetNumberOfElements(4, 4)];
            var g = new Value2DArray(gBuffer, 4, 4);

            for (int i = 0; i < 4; i++) g[i, 0] = f[0, i] + f[2, i];
            for (int i = 0; i < 4; i++) g[i, 1] = f[0, i] - f[2, i];
            for (int i = 0; i < 4; i++) g[i, 2] = (f[1, i] >> 1) - f[3, i];
            for (int i = 0; i < 4; i++) g[i, 3] = f[1, i] + (f[3, i] >> 1);

            Span<int> hBuffer = stackalloc int[Value2DArray.GetNumberOfElements(4, 4)];
            var h = new Value2DArray(hBuffer, 4, 4);

            for (int i = 0; i < 4; i++) h[i, 0] = g[0, i] + g[3, i];
            for (int i = 0; i < 4; i++) h[i, 1] = f[1, i] + f[2, i];
            for (int i = 0; i < 4; i++) h[i, 2] = g[1, i] - g[2, i];
            for (int i = 0; i < 4; i++) h[i, 3] = g[0, i] - g[3, i];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rij[i, j] = (h[i, j] + TwoPowFive) >> 6;
                }
            }
        }

        public void TransformResidual8x8Blocks(
            int[,] d,
            int[,] r)
        {
            Span<int> eBuffer = stackalloc int[Value2DArray.GetNumberOfElements(8, 8)];
            var e = new Value2DArray(eBuffer, 8, 8);

            for (int i = 0; i < 8; i++)
            {
                e[i, 0] = d[i, 0] + d[i, 4];
                e[i, 1] = -d[i, 3] + d[i, 5] - d[i, 7] - (d[i, 7] >> 1);
                e[i, 2] = d[i, 0] - d[i, 4];
                e[i, 3] = d[i, 1] + d[i, 7] - d[i, 3] - (d[i, 3] >> 1);
                e[i, 4] = (d[i, 2] >> 1) - d[i, 6];
                e[i, 5] = -d[i, 1] + d[i, 7] + d[i, 5] + (d[i, 5] >> 1);
                e[i, 6] = d[i, 2] + (d[i, 6] >> 1);
                e[i, 7] = d[i, 3] + d[i, 5] + d[i, 1] + (d[i, 1] >> 1);
            }

            Span<int> fBuffer = stackalloc int[Value2DArray.GetNumberOfElements(8, 8)];
            var f = new Value2DArray(fBuffer, 8, 8);

            for (int i = 0; i < 8; i++)
            {
                f[i, 0] = e[i, 0] + e[i, 6];
                f[i, 1] = e[i, 1] + (e[i, 7] >> 2);
                f[i, 2] = e[i, 2] + e[i, 4];
                f[i, 3] = e[i, 3] + (e[i, 5] >> 2);
                f[i, 4] = e[i, 2] - e[i, 4];
                f[i, 5] = (e[i, 3] >> 2) - e[i, 5];
                f[i, 6] = e[i, 0] - e[i, 6];
                f[i, 7] = e[i, 7] - (e[i, 1] >> 2);
            }

            Span<int> gBuffer = stackalloc int[Value2DArray.GetNumberOfElements(8, 8)];
            var g = new Value2DArray(gBuffer, 8, 8);

            for (int i = 0; i < 8; i++)
            {
                g[i, 0] = f[i, 0] + f[i, 7];
                g[i, 1] = f[i, 2] + f[i, 5];
                g[i, 2] = f[i, 4] + f[i, 3];
                g[i, 3] = f[i, 6] + f[i, 1];
                g[i, 4] = f[i, 6] - f[i, 1];
                g[i, 5] = f[i, 4] - f[i, 3];
                g[i, 6] = f[i, 2] - f[i, 5];
                g[i, 7] = f[i, 0] - f[i, 7];
            }

            Span<int> hBuffer = stackalloc int[Value2DArray.GetNumberOfElements(8, 8)];
            var h = new Value2DArray(hBuffer, 8, 8);

            for (int j = 0; j < 8; j++)
            {
                h[0, j] = g[0, j] + g[4, j];
                h[1, j] = -g[3, j] + g[5, j] - g[7, j] - (g[7, j] >> 1);
                h[2, j] = g[0, j] - g[4, j];
                h[3, j] = g[1, j] + g[7, j] - g[3, j] - (g[3, j] >> 1);
                h[4, j] = (g[2, j] >> 1) - g[6, j];
                h[5, j] = -g[1, j] + g[7, j] + g[5, j] + (g[5, j] >> 1);
                h[6, j] = g[2, j] + (g[6, j] >> 1);
                h[7, j] = g[3, j] + g[5, j] + g[1, j] + (g[1, j] >> 1);
            }

            Span<int> kBuffer = stackalloc int[Value2DArray.GetNumberOfElements(8, 8)];
            var k = new Value2DArray(kBuffer, 8, 8);

            for (int j = 0; j < 8; j++)
            {
                k[0, j] = h[0, j] + h[6, j];
                k[1, j] = h[1, j] + (h[7, j] >> 2);
                k[2, j] = h[2, j] + h[4, j];
                k[3, j] = h[3, j] + (h[5, j] >> 2);
                k[4, j] = h[2, j] - h[4, j];
                k[5, j] = (h[3, j] >> 2) - h[5, j];
                k[6, j] = h[0, j] - h[6, j];
                k[7, j] = h[7, j] - (h[1, j] >> 2);
            }

            Span<int> mBuffer = stackalloc int[Value2DArray.GetNumberOfElements(8, 8)];
            var m = new Value2DArray(mBuffer, 8, 8);

            for (int j = 0; j < 8; j++)
            {
                m[0, j] = k[0, j] + k[7, j];
                m[1, j] = k[2, j] + k[5, j];
                m[2, j] = k[4, j] + k[3, j];
                m[3, j] = k[6, j] + k[1, j];
                m[4, j] = k[6, j] - k[1, j];
                m[5, j] = k[4, j] - k[3, j];
                m[6, j] = k[2, j] - k[5, j];
                m[7, j] = k[0, j] - k[7, j];
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    r[i, j] = (m[i, j] + 32) >> 6;
                }
            }
        }
    }
}
