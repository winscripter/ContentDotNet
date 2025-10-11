namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation.Predictors
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.Common.Derivative;
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Primitives;

    internal class DefaultIntraChromaPredictor : IIntraChromaPredictor
    {
        private static readonly XY ZeroZero = new(0, 0);

        public H264State? H264State { get; set; }

        public GetChromaChannelCallback GetChromaChannelCallback => (yCbCr) => yCbCr.Cb;

        public void Dc(IntraPredictionSamples p, int[,] predC)
        {
            if (H264State == null)
                throw IntraThrowHelper.NoH264State();

            for (int chroma4x4BlkIdx = 0; chroma4x4BlkIdx <= (1 << (H264State.DeriveChromaArrayType() + 1) - 1); chroma4x4BlkIdx++)
            {
                XY xy = H264Derivative.Inverse4x4ChromaBlockScan(chroma4x4BlkIdx);

                int xO = xy.X;
                int yO = xy.Y;

                if (xy == ZeroZero || xO > 0 && yO > 0)
                {
                    if (p.IsAvailable(xO, -1) && p.IsAvailable(-1, yO))
                    {
                        int xResult = 0;
                        int yResult = 0;

                        for (int x2 = 0; x2 < 4; x2++)
                        {
                            xResult += GetChromaChannelCallback(p[x2 + xO, -1]);
                        }

                        for (int y2 = 0; y2 < 4; y2++)
                        {
                            yResult += GetChromaChannelCallback(p[-1, y2 + yO]);
                        }

                        int template = (xResult + yResult + 4) >> 3;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else if (!p.IsAvailable(xO, -1) && p.IsAvailable(-1, yO))
                    {
                        int yResult = 0;

                        for (int y2 = 0; y2 < 4; y2++)
                        {
                            yResult += GetChromaChannelCallback(p[-1, y2 + yO]);
                        }

                        int template = (yResult + 2) >> 2;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else if (p.IsAvailable(xO, -1) && !p.IsAvailable(-1, yO))
                    {
                        int xResult = 0;

                        for (int x2 = 0; x2 < 4; x2++)
                        {
                            xResult += GetChromaChannelCallback(p[x2 + xO, -1]);
                        }

                        int template = (xResult + 2) >> 2;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else
                    {
                        int template = 1 << (H264State.DeriveBitDepthC() - 1);

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                }
                else if (xO > 0 && yO == 0)
                {
                    if (p.IsAvailable(xO, -1))
                    {
                        int xResult = 0;

                        for (int x2 = 0; x2 < 4; x2++)
                        {
                            xResult += GetChromaChannelCallback(p[x2 + xO, -1]);
                        }

                        int template = (xResult + 2) >> 2;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else if (p.IsAvailable(-1, yO))
                    {
                        int yResult = 0;

                        for (int y2 = 0; y2 < 4; y2++)
                        {
                            yResult += GetChromaChannelCallback(p[-1, y2 + yO]);
                        }

                        int template = (yResult + 2) >> 2;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else
                    {
                        int template = 1 << (H264State.DeriveBitDepthC() - 1);

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                }
                else
                {
                    if (p.IsAvailable(-1, yO))
                    {
                        int yResult = 0;

                        for (int y2 = 0; y2 < 4; y2++)
                        {
                            yResult += GetChromaChannelCallback(p[-1, y2 + yO]);
                        }

                        int template = (yResult + 2) >> 2;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else if (p.IsAvailable(xO, -1))
                    {
                        int xResult = 0;

                        for (int x2 = 0; x2 < 4; x2++)
                        {
                            xResult += GetChromaChannelCallback(p[x2 + xO, -1]);
                        }

                        int template = (xResult + 2) >> 2;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                    else
                    {
                        int template = 1 << (H264State.DeriveBitDepthC() - 1);

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                predC[x + xO, y + yO] = template;
                            }
                        }
                    }
                }
            }
        }

        public void Horizontal(IntraPredictionSamples p, int[,] predC)
        {
            H264MacroblockChromaSizes mbSizeC = H264State.DeriveChromaMacroblockSizes();

            for (int x = 0; x < mbSizeC.MbWidthC; x++)
            {
                for (int y = 0; y < mbSizeC.MbHeightC; y++)
                {
                    predC[x, y] = GetChromaChannelCallback(p[-1, y]);
                }
            }
        }

        public void Plane(IntraPredictionSamples p, int[,] predC)
        {
            int ChromaArrayType = H264State.DeriveChromaArrayType();
            H264MacroblockChromaSizes mbSizeC = H264State.DeriveChromaMacroblockSizes();

            int xCF = ChromaArrayType == 3 ? 4 : 0;
            int yCF = ChromaArrayType != 1 ? 4 : 0;

            int H = 0;
            int V = 0;

            for (int x = 0; x <= 3 + xCF; x++)
            {
                H += (x + 1) * (GetChromaChannelCallback(p[4 + xCF + x, -1]) - GetChromaChannelCallback(p[2 + xCF - x, -1]));
            }

            for (int y = 0; y <= 3 + yCF; y++)
            {
                V += (y + 1) * (GetChromaChannelCallback(p[-1, 4 + yCF + y]) - GetChromaChannelCallback(p[-1, 2 + yCF - y]));
            }

            int MbWidthC = mbSizeC.MbWidthC;
            int MbHeightC = mbSizeC.MbHeightC;

            int a = 16 * (GetChromaChannelCallback(p[-1, MbHeightC - 1]) + GetChromaChannelCallback(p[MbWidthC - 1, -1]));
            int b = ((34 - 29 * (ChromaArrayType == 3).AsInt32()) * H + 32) >> 6;
            int c = ((34 - 29 * (ChromaArrayType != 1).AsInt32()) * V + 32) >> 6;

            int BitDepthC = H264State.DeriveBitDepthC();

            for (int x = 0; x < MbWidthC; x++)
            {
                for (int y = 0; y < MbHeightC; y++)
                {
                    predC[x, y] = Clipping.Clip1C((a + b * (x - 3 - xCF) + c * (y - 3 - yCF) + 16) >> 5, BitDepthC);
                }
            }
        }

        public void Vertical(IntraPredictionSamples p, int[,] predC)
        {
            H264MacroblockChromaSizes mbSizeC = H264State.DeriveChromaMacroblockSizes();

            for (int x = 0; x < mbSizeC.MbWidthC; x++)
            {
                for (int y = 0; y < mbSizeC.MbHeightC; y++)
                {
                    predC[x, y] = GetChromaChannelCallback(p[x, -1]);
                }
            }
        }
    }
}
