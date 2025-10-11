namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation.Predictors
{
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;

    internal class DefaultIntra4x4Predictor : IIntra4x4Predictor
    {
        public H264State? H264State { get; set; }

        const int ANY = 0;

        public void Dc(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (p.IsAvailable(ANY, -1) && p.IsAvailable(-1, ANY))
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        pred4x4L[x, y] = (p[0, -1].Y + p[1, -1].Y + p[2, -1].Y + p[3, -1].Y +
                                          p[-1, 0].Y + p[-1, 1].Y + p[-1, 2].Y + p[-1, 3].Y + 4) >> 3;
                    }
                }
            }
            else if (!p.IsAvailable(ANY, -1) && p.IsAvailable(-1, ANY))
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        pred4x4L[x, y] = (p[-1, 0].Y + p[-1, 1].Y + p[-1, 2].Y + p[-1, 3].Y + 2) >> 2;
                    }
                }
            }
            else if (p.IsAvailable(ANY, -1) && !p.IsAvailable(-1, ANY))
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        pred4x4L[x, y] = (p[0, -1].Y + p[1, -1].Y + p[2, -1].Y + p[3, -1].Y + 2) >> 2;
                    }
                }
            }
            else
            {
                if (H264State == null)
                    throw IntraThrowHelper.NoH264State();

                int template = 1 << (H264State.DeriveBitDepthY() - 1);
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        pred4x4L[x, y] = template;
                    }
                }
            }
        }

        public void DiagonalDownLeft(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (!p.IsAvailable(0, -1) || !p.IsAvailable(4, -1)) // Top and Top Right
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (x == 3 && y == 3)
                        pred4x4L[x, y] = (p[6, -1].Y + 3 * p[7, -1].Y + 2) >> 2;
                    else
                        pred4x4L[x, y] = (p[x + y, -1].Y + 2 * p[x + y + 1, -1].Y + p[x + y + 2, -1].Y + 2) >> 2;
                }
            }
        }

        public void DiagonalDownRight(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (!p.IsAvailable(ANY, -1) || !p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (x > y)
                        pred4x4L[x, y] = (p[x - y - 2, -1].Y + 2 * p[x - y - 1, -1].Y + p[x - y, -1].Y + 2) >> 2;
                    else if (x < y)
                        pred4x4L[x, y] = (p[-1, y - x - 2].Y + 2 * p[-1, y - x - 1].Y + p[-1, y - x].Y + 2) >> 2;
                    else
                        pred4x4L[x, y] = (p[0, -1].Y + 2 * p[-1, -1].Y + p[-1, 0].Y + 2) >> 2;
                }
            }
        }

        public void Horizontal(IntraPredictionSamples samples, int[,] pred4x4L)
        {
            if (!samples.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    pred4x4L[x, y] = samples[x, -1].Y;
                }
            }
        }

        public void HorizontalDown(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (!p.IsAvailable(ANY, -1) || !p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int zHD = 2 * y - x;
                    if (zHD is 0 or 2 or 4 or 6)
                    {
                        pred4x4L[x, y] = (p[-1, y - (x >> 1) - 1].Y + p[-1, y - (x >> 1)].Y + 1) >> 1;
                    }
                    else if (zHD is 1 or 3 or 5)
                    {
                        pred4x4L[x, y] = (p[-1, y - (x >> 1) - 2].Y + 2 * p[-1, y - (x >> 1) - 1].Y + p[-1, y - (x >> 1)].Y + 2) >> 2;
                    }
                    else if (zHD == -1)
                    {
                        pred4x4L[x, y] = (p[-1, 0].Y + 2 * p[-1, -1].Y + p[0, -1].Y + 2) >> 2;
                    }
                    else
                    {
                        pred4x4L[x, y] = (p[x - 1, -1].Y + 2 * p[x - 2, -1].Y + p[x - 3, -1].Y + 2) >> 2;
                    }
                }
            }
        }

        public void HorizontalUp(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (!p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int zHU = x + 2 * y;
                    if (zHU is 0 or 2 or 4)
                    {
                        pred4x4L[x, y] = (p[ -1, y + (x >> 1)].Y + p[ -1, y + (x >> 1) + 1].Y + 1) >> 1;
                    }
                    else if (zHU is 1 or 3)
                    {
                        pred4x4L[x, y] = (p[-1, y + (x >> 1)].Y + 2 * p[-1, y + (x >> 1) + 1].Y + p[-1, y + (x >> 1) + 2].Y + 2) >> 2;
                    }
                    else if (zHU == 5)
                    {
                        pred4x4L[x, y] = (p[-1, 2].Y + 3 * p[-1, 3].Y + 2) >> 2;
                    }
                    else
                    {
                        pred4x4L[x, y] = p[-1, 3].Y;
                    }
                }
            }
        }

        public void Vertical(IntraPredictionSamples samples, int[,] pred4x4L)
        {
            if (!samples.IsAvailable(ANY, -1))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    pred4x4L[x, y] = samples[-1, y].Y;
                }
            }
        }

        public void VerticalLeft(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (!p.IsAvailable(0, -1) || !p.IsAvailable(4, -1)) // Top and Top Right
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (y is 0 or 2)
                        pred4x4L[x, y] = (p[x + (y >> 1), -1].Y + p[x + (y >> 1) + 1, -1].Y + 1) >> 1;
                    else
                        pred4x4L[x, y] = (p[x + (y >> 1), -1].Y + 2 * p[x + (y >> 1) + 1, -1].Y + p[x + (y >> 1) + 2, -1].Y + 2) >> 2;
                }
            }
        }

        public void VerticalRight(IntraPredictionSamples p, int[,] pred4x4L)
        {
            if (!p.IsAvailable(ANY, -1) || !p.IsAvailable(-1, ANY))
                throw IntraThrowHelper.NeighboringPixelsUnavailable();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int zVR = 2 * x - y;
                    if (zVR is 0 or 2 or 4 or 6)
                    {
                        pred4x4L[x, y] = (p[x - (y >> 1) - 1, -1].Y + p[x - (y >> 1), -1].Y + 1) >> 1;
                    }
                    else if (zVR is 1 or 3 or 5)
                    {
                        pred4x4L[x, y] = (p[x - (y >> 1) - 2, -1].Y + 2 * p[x - (y >> 1) - 1, -1].Y + p[x - (y >> 1), -1].Y + 2) >> 2;
                    }
                    else if (zVR == -1)
                    {
                        pred4x4L[x, y] = (p[-1, 0].Y + 2 * p[-1, -1].Y + p[0, -1].Y + 2) >> 2;
                    }
                    else
                    {
                        pred4x4L[x, y] = (p[-1, y - 1].Y + 2 * p[-1, y - 2].Y + p[-1, y - 3].Y + 2) >> 2;
                    }
                }
            }
        }
    }
}
