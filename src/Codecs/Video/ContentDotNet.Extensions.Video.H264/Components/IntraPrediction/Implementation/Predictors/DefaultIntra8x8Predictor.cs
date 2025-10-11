namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation.Predictors
{
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;

    internal class DefaultIntra8x8Predictor : IIntra8x8Predictor
    {
        const int ANY = 0;

        public H264State? H264State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dc(IntraPredictionSamples p, int[,] pred8x8L)
        {
            // Algorithm for p[x, -1] and p[-1, y] availability:
            //                   7             7
            // pred8x8L[x, y] = (Σ p[x2, -1] + Σ p[-1, y2] + 8) >> 4
            //                 x2 = 0        y2 = 0
            //
            // Algorithm for p[-1, y] availability:
            //                   7
            // pred8x8L[x, y] = (Σ p[-1, y2] + 4) >> 3
            //                 y2 = 0
            //
            // Algorithm for p[x, -1] availability:
            //                   7
            // pred8x8L[x, y] = (Σ p[x2, -1] + 4) >> 3
            //                 x2 = 0
            //
            // Algorithm for no availability (fallback):
            //
            // pred8x8L[x, y] = 1 << (BitDepthY - 1)
            //

            if (p.IsAvailable(ANY, -1) && p.IsAvailable(-1, ANY))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        int sumX = 0;
                        int sumY = 0;

                        for (int x2 = 0; x2 < 8; x2++)
                        {
                            sumX += p[x2, -1].Y;
                        }

                        for (int y2 = 0; y2 < 8; y2++)
                        {
                            sumY += p[-1, y2].Y;
                        }

                        pred8x8L[x, y] = (sumX + sumY + 8) >> 4;
                    }
                }
            }
            else if (p.IsAvailable(-1, ANY))
            {
                int sumY = 0;
                for (int y2 = 0; y2 < 8; y2++)
                {
                    sumY += p[-1, y2].Y;
                }

                int prediction = (sumY + 4) >> 3;

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        pred8x8L[x, y] = prediction;
                    }
                }
            }
            else if (p.IsAvailable(ANY, -1))
            {
                int sumX = 0;
                for (int x2 = 0; x2 < 8; x2++)
                {
                    sumX += p[x2, -1].Y;
                }

                int prediction = (sumX + 4) >> 3;

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        pred8x8L[x, y] = prediction;
                    }
                }
            }
            else
            {
                int prediction = 1 << (H264State!.DeriveBitDepthY() - 1);

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        pred8x8L[x, y] = prediction;
                    }
                }
            }
        }

        public void DiagonalDownLeft(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(0, -1) || !p.IsAvailable(8, -1))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (x == 7 && y == 7)
                        pred8x8L[x, y] = (p[14, -1].Y + 3 * p[15, -1].Y + 2) >> 2;
                    else
                        pred8x8L[x, y] = (p[x + y, -1].Y + 2 * p[x + y + 1, -1].Y + p[x + y + 2, -1].Y + 2) >> 2;
                }
            }
        }

        public void DiagonalDownRight(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(ANY, -1) || !p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (x > y)
                        pred8x8L[x, y] = (p[x - y - 2, -1].Y + 2 * p[x - y - 1, -1].Y + p[x - y, -1].Y + 2) >> 2;
                    else if (x < y)
                        pred8x8L[x, y] = (p[-1, y - x - 2].Y + 2 * p[-1, y - x - 1].Y + p[-1, y - x].Y + 2) >> 2;
                    else
                        pred8x8L[x, y] = (p[0, -1].Y + 2 * p[-1, -1].Y + p[-1, 0].Y + 2) >> 2;
                }
            }
        }

        public void Horizontal(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NoH264State();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    pred8x8L[x, y] = p[-1, y].Y;
                }
            }
        }

        public void HorizontalDown(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(ANY, -1) || !p.IsAvailable(-1, ANY) || !p.IsAvailable(-1, -1))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int zHD = 2 * y - x;
                    if (zHD is 0 or 2 or 4 or 6 or 8 or 10 or 12 or 14)
                    {
                        pred8x8L[x, y] = (p[-1, y - (x >> 1) - 1].Y + p[-1, y - (x >> 1)].Y + 1) >> 1;
                    }
                    else if (zHD is 1 or 3 or 5 or 7 or 9 or 11 or 13)
                    {
                        pred8x8L[x, y] = (p[-1, y - (x >> 1) - 2].Y + 2 * p[-1, y - (x >> 1) - 1].Y +
                                 p[-1, y - (x >> 1)].Y + 2) >> 2;
                    }
                    else if (zHD == -1)
                    {
                        pred8x8L[x, y] = (p[-1, 0].Y + 2 * p[-1, -1].Y + p[0, -1].Y + 2) >> 2;
                    }
                    else
                    {
                        pred8x8L[x, y] = (p[x - 2 * y - 1, -1].Y + 2 * p[x - 2 * y - 2, -1].Y + p[x - 2 * y - 3, -1].Y + 2) >> 2;
                    }
                }
            }
        }

        public void HorizontalUp(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NoH264State();

            int template = p[-1, 7].Y;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int zHU = x + 2 * y;
                    if (zHU is 0 or 2 or 4 or 6 or 8 or 10 or 12)
                        pred8x8L[x, y] = (p[-1, y + (x >> 1)].Y + p[-1, y + (x >> 1) + 1].Y + 1) >> 1;
                    else if (zHU is 1 or 3 or 5 or 7 or 9 or 11)
                        pred8x8L[x, y] = (p[-1, y + (x >> 1)].Y + 2 * p[-1, y + (x >> 1) + 1].Y + p[-1, y + (x >> 1) + 2].Y + 2) >> 2;
                    else if (zHU == 13)
                        pred8x8L[x, y] = (p[-1, 6].Y + 3 * p[-1, 7].Y + 2) >> 2;
                    else
                        pred8x8L[x, y] = template;
                }
            }
        }

        public void Vertical(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(ANY, -1))
                throw IntraThrowHelper.NoH264State();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    pred8x8L[x, y] = p[x, -1].Y;
                }
            }
        }

        public void VerticalLeft(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(0, -1) || !p.IsAvailable(8, -1))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (y is 0 or 2 or 4 or 6)
                        pred8x8L[x, y] = (p[x + (y >> 1), -1].Y + p[x + (y >> 1) + 1, -1].Y + 1) >> 1;
                    else
                        pred8x8L[x, y] = (p[x + (y >> 1), -1].Y + 2 * p[x + (y >> 1) + 1, -1].Y + p[x + (y >> 1) + 2, -1].Y + 2) >> 2;
                }
            }
        }

        public void VerticalRight(IntraPredictionSamples p, int[,] pred8x8L)
        {
            if (!p.IsAvailable(ANY, -1) || !p.IsAvailable(-1, ANY) || !p.IsAvailable(-1, -1))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int zVR = 2 * x - y;
                    if (zVR is 0 or 2 or 4 or 6 or 8 or 10 or 12 or 14)
                    {
                        pred8x8L[x, y] = (p[x - (y >> 1) - 1, -1].Y + p[x - (y >> 1), -1].Y + 1) >> 1;
                    }
                    else if (zVR is 1 or 3 or 5 or 7 or 9 or 11 or 13)
                    {
                        pred8x8L[x, y] = (p[x - (y >> 1) - 2, -1].Y + 2 * p[x - (y >> 1) - 1, -1].Y +
                                 p[x - (y >> 1), -1].Y + 2) >> 2;
                    }
                    else if (zVR == -1)
                    {
                        pred8x8L[x, y] = (p[-1, 0].Y + 2 * p[-1, -1].Y + p[0, -1].Y + 2) >> 2;
                    }
                    else
                    {
                        pred8x8L[x, y] = (p[-1, y - 2 * x - 1].Y + 2 * p[-1, y - 2 * x - 2].Y + p[-1, y - 2 * x - 3].Y + 2) >> 2;
                    }
                }
            }
        }
    }
}
