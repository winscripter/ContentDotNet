namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation.Predictors
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;

    internal class DefaultIntra16x16Predictor : IIntra16x16Predictor
    {
        const int ANY = 0;

        public H264State? H264State { get; set; }

        public void Dc(IntraPredictionSamples p, int[,] pred16x16L)
        {
            // Algorithm for p[x, -1] and p[-1, y] availability:
            //                     15            15
            // pred16x16L[x, y] = (Σ p[x2, -1] + Σ p[-1, y2] + 16) >> 5
            //                   x2 = 0        y2 = 0
            //
            // Algorithm for p[-1, y] availability:
            //                     15
            // pred16x16L[x, y] = (Σ p[-1, y2] + 8) >> 4
            //                   y2 = 0
            //
            // Algorithm for p[x, -1] availability:
            //                     15
            // pred16x16L[x, y] = (Σ p[x2, -1] + 8) >> 4
            //                   x2 = 0
            //
            // Algorithm for no availability (fallback):
            //
            // pred16x16L[x, y] = 1 << (BitDepthY - 1)
            //

            if (p.IsAvailable(ANY, -1) && p.IsAvailable(-1, ANY))
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        int sumX = 0;
                        int sumY = 0;

                        for (int x2 = 0; x2 < 16; x2++)
                        {
                            sumX += p[x2, -1].Y;
                        }

                        for (int y2 = 0; y2 < 16; y2++)
                        {
                            sumY += p[-1, y2].Y;
                        }

                        pred16x16L[x, y] = (sumX + sumY + 16) >> 5;
                    }
                }
            }
            else if (p.IsAvailable(-1, ANY))
            {
                int sumY = 0;
                for (int y2 = 0; y2 < 16; y2++)
                {
                    sumY += p[-1, y2].Y;
                }

                int prediction = (sumY + 8) >> 4;

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        pred16x16L[x, y] = prediction;
                    }
                }
            }
            else if (p.IsAvailable(ANY, -1))
            {
                int sumX = 0;
                for (int x2 = 0; x2 < 16; x2++)
                {
                    sumX += p[x2, -1].Y;
                }

                int prediction = (sumX + 8) >> 4;

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        pred16x16L[x, y] = prediction;
                    }
                }
            }
            else
            {
                int prediction = 1 << (H264State!.DeriveBitDepthY() - 1);

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        pred16x16L[x, y] = prediction;
                    }
                }
            }
        }

        public void Horizontal(IntraPredictionSamples p, int[,] pred16x16L)
        {
            if (!p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    pred16x16L[x, y] = p[-1, y].Y;
                }
            }
        }

        public void Plane(IntraPredictionSamples p, int[,] pred16x16L)
        {
            if (H264State == null)
                throw IntraThrowHelper.NoH264State();

            int H = 0;
            int V = 0;
            for (int x = 0; x <= 7; x++)
            {
                H += x + 1 * (p[8 + x, -1].Y - p[6 - x, -1].Y);
            }

            for (int y = 0; y <= 7; y++)
            {
                V += (y + 1) * (p[-1, 8 + y].Y - p[-1, 6 - y].Y);
            }

            int a = 16 * (p[-1, 15].Y + p[15, -1].Y);
            int b = (5 * H + 32) >> 6;
            int c = (5 * V + 32) >> 6;

            int BitDepthY = H264State.DeriveBitDepthY();

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    pred16x16L[x, y] = Clipping.Clip1Y((a + b * (x - 7) + c * (y - 7) + 16) >> 5, BitDepthY);
                }
            }
        }

        public void Vertical(IntraPredictionSamples p, int[,] pred16x16L)
        {
            if (!p.IsAvailable(ANY, -1))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    pred16x16L[x, y] = p[x, -1].Y;
                }
            }
        }
    }
}
