using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal static class Transforms
{
    public static ReadOnlySpan<int> InverseZigZagScan =>
    [
        0, 1, 5, 6,
        2, 4, 7, 10,
        3, 8, 9, 11,
        12, 13, 14, 15
    ];

    private static readonly int[,,] LevelScale4x4 = new int[6, 4, 4]
    {
        { {10,13,10,13}, {13,16,13,16}, {10,13,10,13}, {13,16,13,16} },
        { {11,14,11,14}, {14,18,14,18}, {11,14,11,14}, {14,18,14,18} },
        { {13,16,13,16}, {16,20,16,20}, {13,16,13,16}, {16,20,16,20} },
        { {14,18,14,18}, {18,23,18,23}, {14,18,14,18}, {18,23,18,23} },
        { {16,20,16,20}, {20,25,20,25}, {16,20,16,20}, {20,25,20,25} },
        { {18,23,18,23}, {23,29,23,29}, {18,23,18,23}, {23,29,23,29} },
    };

    private static readonly int[,,] LevelScale8x8 = new int[6, 8, 8]
    {
        // QP % 6 = 0
        {
            {6,13,20,28,13,20,28,32},
            {13,20,28,32,20,28,32,37},
            {20,28,32,37,28,32,37,42},
            {28,32,37,42,32,37,42,47},
            {13,20,28,32,20,28,32,37},
            {20,28,32,37,28,32,37,42},
            {28,32,37,42,32,37,42,47},
            {32,37,42,47,37,42,47,51}
        },
        // QP % 6 = 1
        {
            {6,13,19,26,13,19,26,30},
            {13,19,26,30,19,26,30,34},
            {19,26,30,34,26,30,34,38},
            {26,30,34,38,30,34,38,42},
            {13,19,26,30,19,26,30,34},
            {19,26,30,34,26,30,34,38},
            {26,30,34,38,30,34,38,42},
            {30,34,38,42,34,38,42,46}
        },
        // QP % 6 = 2
        {
            {6,13,18,24,13,18,24,28},
            {13,18,24,28,18,24,28,32},
            {18,24,28,32,24,28,32,36},
            {24,28,32,36,28,32,36,40},
            {13,18,24,28,18,24,28,32},
            {18,24,28,32,24,28,32,36},
            {24,28,32,36,28,32,36,40},
            {28,32,36,40,32,36,40,44}
        },
        // QP % 6 = 3
        {
            {6,12,17,22,12,17,22,26},
            {12,17,22,26,17,22,26,30},
            {17,22,26,30,22,26,30,34},
            {22,26,30,34,26,30,34,38},
            {12,17,22,26,17,22,26,30},
            {17,22,26,30,22,26,30,34},
            {22,26,30,34,26,30,34,38},
            {26,30,34,38,30,34,38,42}
        },
        // QP % 6 = 4
        {
            {6,12,16,20,12,16,20,24},
            {12,16,20,24,16,20,24,28},
            {16,20,24,28,20,24,28,32},
            {20,24,28,32,24,28,32,36},
            {12,16,20,24,16,20,24,28},
            {16,20,24,28,20,24,28,32},
            {20,24,28,32,24,28,32,36},
            {24,28,32,36,28,32,36,40}
        },
        // QP % 6 = 5
        {
            {6,12,15,19,12,15,19,22},
            {12,15,19,22,15,19,22,26},
            {15,19,22,26,19,22,26,30},
            {19,22,26,30,22,26,30,34},
            {12,15,19,22,15,19,22,26},
            {15,19,22,26,19,22,26,30},
            {19,22,26,30,22,26,30,34},
            {22,26,30,34,26,30,34,38}
        }
    };

    public static void InverseScan4x4TransformCoefficients(Span<int> list, Span<ContainerMatrix4x4> c)
    {
        ContainerMatrix4x4 matrix = c[0];

        for (int i = 0; i < InverseZigZagScan.Length; i++)
        {
            int index = InverseZigZagScan[i];
            int row = index / 4;
            int col = index % 4;
            matrix[row, col] = (uint)list[i];
        };

        c[0] = matrix;
    }

    public static void TransformDecodeLumaSamples16x16(Container64UInt32 i16x16DCLevel, ContainerMatrix16x16 i16x16ACLevel)
    {
        Span<uint> i16x16DCLTemp = stackalloc uint[16];
        for (int i = 0; i < 16; i++)
            i16x16DCLTemp[i] = i16x16DCLevel[i];

    }

    public static void ScaleTransformDCTransformCoefficientsForIntra16x16(int qP, bool transformBypassFlag, ContainerMatrix4x4Int32 c, out ContainerMatrix4x4Int32 dcY)
    {
        if (transformBypassFlag)
        {
            dcY = new();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    dcY[x, y] = c[x, y];
                }
            }
        }
        else
        {
            ContainerMatrix4x4Int32 f = new();
            f[0, 0] = 1 * c[0, 0] * 1;
            f[0, 1] = 1 * c[0, 1] * 1;
            f[0, 2] = 1 * c[0, 2] * 1;
            f[0, 3] = 1 * c[0, 3] * 1;

            f[1, 0] = 1 * c[1, 0] * 1;
            f[1, 1] = 1 * c[1, 1] * 1;
            f[1, 2] = -1 * c[1, 2] * -1;
            f[1, 3] = -1 * c[1, 3] * -1;

            f[2, 0] = 1 * c[2, 0] * 1;
            f[2, 1] = -1 * c[2, 1] * -1;
            f[2, 2] = -1 * c[2, 2] * -1;
            f[2, 3] = 1 * c[2, 3] * 1;

            f[3, 0] = 1 * c[3, 0] * 1;
            f[3, 1] = -1 * c[3, 1] * -1;
            f[3, 2] = 1 * c[3, 2] * 1;
            f[3, 3] = -1 * c[3, 3] * -1;

            dcY = new();

            if (qP >= 36)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dcY[i, j] = (f[i, j] * LevelScale4x4[qP % 6, 0, 0]) << (qP / 6 - 6);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dcY[i, j] = (f[i, j] * LevelScale4x4[qP % 6, 0, 0] + (1 << (5 - qP / 6))) >> (6 - qP / 6);
                    }
                }
            }
        }
    }

    // This process is only invoked when ChromaArrayType is equal to 1 or 2.
    public static void ScaleTransformChromaDCTransformCoefficients(int bitDepth, int chromaArrayType, int qP, bool transformBypassModeFlag, MacroblockSizeChroma mb, ContainerMatrix4x4Int32 c, out ContainerMatrix4x4Int32 dcC)
    {
        if (transformBypassModeFlag)
        {
            dcC = new();

            for (int x = 0; x < (mb.Width / 4) - 1; x++)
            {
                for (int y = 0; y < (mb.Height / 4) - 1; y++)
                {
                    dcC[x, y] = c[x, y];
                }
            }
        }
        else
        {
            TransformChromaDCTransformCoefficients(chromaArrayType, c, out var f);
            ScaleChromaDCTransformCoefficients(bitDepth, qP, f, out dcC);
        }
    }

    private static ReadOnlySpan<int> TransformChromaDCTransformCoeffs_T =>
    [
        1,  1,  1,  1,
        1,  1, -1, -1,
        1, -1, -1,  1,
        1, -1,  1, -1
    ];

    private static void TransformChromaDCTransformCoefficients(int chromaArrayType, ContainerMatrix4x4Int32 c, out ContainerMatrix4x4Int32 f)
    {
        f = new();

        if (chromaArrayType == 1)
        {
            f[0, 0] = 1 * c[0, 0] * 1;
            f[0, 1] = 1 * c[0, 1] * 1;
            f[1, 0] = 1 * c[1, 0] * 1;
            f[1, 1] = -1 * c[1, 1] * -1;
        }
        else
        {
            //     /------------\   /----------\   /------\
            //     | 1  1  1  1 |   | c00  c01 |   |      |
            //     | 1  1 -1 -1 |   | c10  c11 |   | 1  1 |
            // f = |            | * |          | * |      |
            //     | 1 -1 -1  1 |   | c20  c21 |   |      |
            //     | 1 -1  1 -1 |   | c30  c31 |   | 1 -1 |
            //     \------------/   \----------/   \------/

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int sum = 0;
                    sum += TransformChromaDCTransformCoeffs_T[i * 4 + 0] * c[0, j];
                    sum += TransformChromaDCTransformCoeffs_T[i * 4 + 1] * c[1, j];
                    sum += TransformChromaDCTransformCoeffs_T[i * 4 + 2] * c[2, j];
                    sum += TransformChromaDCTransformCoeffs_T[i * 4 + 3] * c[3, j];

                    f[i, j] = sum;
                }
            }
        }
    }

    public static void ScaleChromaDCTransformCoefficients(int chromaArrayType, int qP, ContainerMatrix4x4Int32 f, out ContainerMatrix4x4Int32 dcC)
    {
        if (chromaArrayType == 1)
        {
            dcC = new();
            dcC[0, 1] = ((f[0, 1] * LevelScale4x4[qP % 6, 0, 0]) << (qP / 6)) >> 5;
        }
        else
        {
            dcC = new();
            int qPDC = qP + 3;
            if (qPDC >= 36)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dcC[i, j] = (f[i, j] * LevelScale4x4[qP % 6, 0, 0]) << (qP / 6) - 6;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dcC[i, j] = (f[i, j] * LevelScale4x4[qPDC % 6, 0, 0] + (int)Math.Pow(2, 5 - qPDC / 6)) >> (6 - qPDC / 6);
                    }
                }
            }
        }
    }

    public static void ScaleTransformResidual4x4Blocks(int mbType, bool spSlice, bool transformSize8x8Flag, bool isLumaIntra16x16OrChroma, bool transformBypassModeFlag, bool isLuma, int qpy, int qsy, int qpc, int qsc, GeneralSliceType sliceType, ContainerMatrix4x4Int32 c, out ContainerMatrix4x4Int32 r)
    {
        bool sMbFlag = IsSI(mbType) || (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Inter && spSlice);

        int qP = sMbFlag switch
        {
            false when isLuma => qpy,
            false when !isLuma => qpc,
            true when isLuma => qsy,
            true when !isLuma => qsc,
            _ => throw new NotImplementedException()
        };

        if (transformBypassModeFlag)
        {
            r = new();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    r[i, j] = c[i, j];
                }
            }
        }
        else
        {
            ScaleResidual4x4Blocks(qP, c, isLumaIntra16x16OrChroma, out var d);
            TransformResidual4x4Blocks(d, out r);
        }
    }

    private static void ScaleResidual4x4Blocks(int qP, ContainerMatrix4x4Int32 c, bool cIsLumaIntra16x16OrChroma, out ContainerMatrix4x4Int32 d)
    {
        d = new();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == 0 && j == 0 && cIsLumaIntra16x16OrChroma)
                {
                    d[0, 0] = c[0, 0];
                }
                else
                {
                    if (qP >= 24)
                        d[i, j] = (c[i, j] * LevelScale4x4[qP % 6, i, j]) << (qP / 6 - 4);
                    else
                        d[i, j] = (c[i, j] * LevelScale4x4[qP % 6, i, j] + (int)Math.Pow(2, 3 - qP / 6)) >> (4 - qP / 6);
                }
            }
        }
    }

    private static void TransformResidual4x4Blocks(ContainerMatrix4x4Int32 d, out ContainerMatrix4x4Int32 r)
    {
        ContainerMatrix4x4Int32 e = new();
        for (int i = 0; i < 4; i++)
        {
            e[i, 0] = d[i, 0] + d[i, 2];
            e[i, 1] = d[i, 0] - d[i, 2];
            e[i, 2] = (d[i, 1] >> 1) - d[i, 3];
            e[i, 3] = d[i, 1] + (d[i, 3] >> 1);
        }

        ContainerMatrix4x4Int32 f = new();
        for (int i = 0; i < 4; i++)
        {
            f[i, 0] = e[i, 0] + e[i, 3];
            f[i, 1] = e[i, 1] + e[i, 2];
            f[i, 2] = e[i, 1] - e[i, 2];
            f[i, 3] = e[i, 0] - e[i, 3];
        }

        ContainerMatrix4x4Int32 g = new();
        for (int j = 0; j < 4; j++)
        {
            g[0, j] = f[0, j] + f[2, j];
            g[1, j] = f[0, j] - f[2, j];
            g[2, j] = (f[1, j] >> 1) - f[3, j];
            g[3, j] = f[1, j] + (f[3, j] >> 1);
        }

        ContainerMatrix4x4Int32 h = new();
        for (int j = 0; j < 4; j++)
        {
            h[0, j] = g[0, j] + g[3, j];
            h[1, j] = g[1, j] + g[2, j];
            h[2, j] = g[1, j] - g[2, j];
            h[3, j] = g[0, j] - g[3, j];
        }

        r = new();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                r[i, j] = (h[i, j] + 32) >> 6;
            }
        }
    }

    public static void ScaleTransformResidual8x8Blocks(bool isLuma, int qpc, int qpy, bool transformBypassModeFlag, ContainerMatrix8x8Int32 c, out ContainerMatrix8x8Int32 r)
    {
        int qP = isLuma ? qpy : qpc;

        if (transformBypassModeFlag)
        {
            r = new();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    r[i, j] = c[i, j];
                }
            }
        }
        else
        {
            ScaleResidual8x8Blocks(qP, c, out var d);
            TransformResidual8x8Blocks(d, out r);
        }
    }

    private static void ScaleResidual8x8Blocks(int qP, ContainerMatrix8x8Int32 c, out ContainerMatrix8x8Int32 d)
    {
        if (qP >= 36)
        {
            d = new();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    d[i, j] = (c[i, j] * LevelScale8x8[qP % 6, i, j]) << (qP / 6 - 6);
                }
            }
        }
        else
        {
            d = new();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    d[i, j] = (c[i, j] * LevelScale8x8[qP % 6, i, j]) + (int)Math.Pow(2, 5 - qP / 6) >> (6 - qP / 6);
                }
            }
        }
    }

    private static void TransformResidual8x8Blocks(ContainerMatrix8x8Int32 d, out ContainerMatrix8x8Int32 r)
    {
        var e = new ContainerMatrix8x8Int32();
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

        var f = new ContainerMatrix8x8Int32();
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

        var g = new ContainerMatrix8x8Int32();
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

        var h = new ContainerMatrix8x8Int32();
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

        var k = new ContainerMatrix8x8Int32();
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

        var m = new ContainerMatrix8x8Int32();
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

        r = new();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                r[i, j] = (m[i, j] + 32) >> 6;
            }
        }
    }
}
