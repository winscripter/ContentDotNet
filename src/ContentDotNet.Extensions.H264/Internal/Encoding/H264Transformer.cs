using ContentDotNet.Containers;

namespace ContentDotNet.Extensions.H264.Internal.Encoding;

internal static class H264Transformer
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

    static readonly int[,,] LevelScale2x2 = new int[6, 2, 2]
    {
        { {10, 13}, {13, 16} },
        { {11, 14}, {14, 18} },
        { {13, 16}, {16, 20} },
        { {14, 18}, {18, 23} },
        { {16, 20}, {20, 25} },
        { {18, 23}, {23, 29} }
    };

    public static void Scan4x4TransformCoefficients(ContainerMatrix4x4 matrix, Span<int> list)
    {
        for (int i = 0; i < InverseZigZagScan.Length; i++)
        {
            int index = InverseZigZagScan[i];
            int row = index / 4;
            int col = index % 4;
            list[i] = (int)matrix[row, col];
        }
    }

    private static void InverseScaleResidual4x4Blocks(int qP, ContainerMatrix4x4Int32 d, bool cIsLumaIntra16x16OrChroma, out ContainerMatrix4x4Int32 c)
    {
        c = new();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == 0 && j == 0 && cIsLumaIntra16x16OrChroma)
                {
                    c[0, 0] = d[0, 0];
                }
                else
                {
                    if (qP >= 24)
                        c[i, j] = d[i, j] >> (qP / 6 - 4) / LevelScale4x4[qP % 6, i, j];
                    else
                        c[i, j] = ((d[i, j] << (4 - qP / 6)) - (1 << (3 - qP / 6))) / LevelScale4x4[qP % 6, i, j];
                }
            }
        }
    }

    private static void TransformResidual4x4Blocks_Encode(ContainerMatrix4x4Int32 input, out ContainerMatrix4x4Int32 output)
    {
        var temp = new ContainerMatrix4x4Int32();

        for (int i = 0; i < 4; i++)
        {
            int s0 = input[i, 0] + input[i, 3];
            int s1 = input[i, 1] + input[i, 2];
            int s2 = input[i, 1] - input[i, 2];
            int s3 = input[i, 0] - input[i, 3];

            temp[i, 0] = s0 + s1;
            temp[i, 1] = s3 + s2;
            temp[i, 2] = s0 - s1;
            temp[i, 3] = s3 - s2;
        }

        var transformed = new ContainerMatrix4x4Int32();

        for (int j = 0; j < 4; j++)
        {
            int s0 = temp[0, j] + temp[3, j];
            int s1 = temp[1, j] + temp[2, j];
            int s2 = temp[1, j] - temp[2, j];
            int s3 = temp[0, j] - temp[3, j];

            transformed[0, j] = (s0 + s1 + 1) >> 1;
            transformed[1, j] = (s3 + s2 + 1) >> 1;
            transformed[2, j] = (s0 - s1 + 1) >> 1;
            transformed[3, j] = (s3 - s2 + 1) >> 1;
        }

        output = transformed;
    }

    private static void ScaleResidual4x4Blocks_Encode(int qP, ContainerMatrix4x4Int32 transformed, bool isLumaIntra16x16OrChroma, out ContainerMatrix4x4Int32 quantized)
    {
        quantized = new();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == 0 && j == 0 && isLumaIntra16x16OrChroma)
                {
                    quantized[i, j] = transformed[0, 0];
                }
                else
                {
                    if (qP >= 24)
                    {
                        quantized[i, j] = (transformed[i, j] << (6 - qP / 6)) / LevelScale4x4[qP % 6, i, j];
                    }
                    else
                    {
                        int add = 1 << (3 - qP / 6);
                        quantized[i, j] = ((transformed[i, j] << (4 - qP / 6)) + add) / LevelScale4x4[qP % 6, i, j];
                    }
                }
            }
        }
    }

    public static void TransformAndQuantizeResidual4x4(int qP, bool isLumaIntra16x16OrChroma, ContainerMatrix4x4Int32 input, out ContainerMatrix4x4Int32 quantized)
    {
        TransformResidual4x4Blocks_Encode(input, out var transformed);
        ScaleResidual4x4Blocks_Encode(qP, transformed, isLumaIntra16x16OrChroma, out quantized);
    }

    public static void QuantizeAndTransformDCForIntra16x16(int qP, bool transformBypassFlag, ContainerMatrix4x4Int32 dcY, out ContainerMatrix4x4Int32 quantized)
    {
        if (transformBypassFlag)
        {
            quantized = new();
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    quantized[x, y] = dcY[x, y];
                }
            }
            return;
        }

        ContainerMatrix4x4Int32 f = new();

        f[0, 0] = dcY[0, 0];
        f[0, 1] = dcY[0, 1];
        f[0, 2] = dcY[0, 2];
        f[0, 3] = dcY[0, 3];

        f[1, 0] = dcY[1, 0];
        f[1, 1] = dcY[1, 1];
        f[1, 2] = dcY[1, 2];
        f[1, 3] = dcY[1, 3];

        f[2, 0] = dcY[2, 0];
        f[2, 1] = dcY[2, 1];
        f[2, 2] = dcY[2, 2];
        f[2, 3] = dcY[2, 3];

        f[3, 0] = dcY[3, 0];
        f[3, 1] = dcY[3, 1];
        f[3, 2] = dcY[3, 2];
        f[3, 3] = dcY[3, 3];

        quantized = new();

        if (qP >= 36)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    quantized[i, j] = f[i, j] >> (qP / 6 - 6) / LevelScale4x4[qP % 6, 0, 0];
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int numerator = (f[i, j] << (6 - qP / 6));
                    int rounding = 1 << (5 - qP / 6);
                    quantized[i, j] = (numerator - rounding) / LevelScale4x4[qP % 6, 0, 0];
                }
            }
        }
    }


    public static void QuantizeAndTransformChromaDCForIntra8x8(int qP, bool transformBypassFlag, ContainerMatrix4x4Int32 dcCb, ContainerMatrix4x4Int32 dcCr, out ContainerMatrix4x4Int32 quantizedCb, out ContainerMatrix4x4Int32 quantizedCr)
    {
        if (transformBypassFlag)
        {
            quantizedCb = new();
            quantizedCr = new();
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    quantizedCb[x, y] = dcCb[x, y];
                    quantizedCr[x, y] = dcCr[x, y];
                }
            }
            return;
        }

        ContainerMatrix4x4Int32 transformedCb = new();
        ContainerMatrix4x4Int32 transformedCr = new();

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                transformedCb[i, j] = dcCb[i, j];
                transformedCr[i, j] = dcCr[i, j];
            }
        }

        quantizedCb = new();
        quantizedCr = new();

        if (qP >= 36)
        {
            int shift = qP / 6 - 6;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    quantizedCb[i, j] = transformedCb[i, j] >> shift / LevelScale2x2[qP % 6, 0, 0];
                    quantizedCr[i, j] = transformedCr[i, j] >> shift / LevelScale2x2[qP % 6, 0, 0];
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int numeratorCb = (transformedCb[i, j] << (6 - qP / 6));
                    int numeratorCr = (transformedCr[i, j] << (6 - qP / 6));
                    int rounding = 1 << (5 - qP / 6);
                    quantizedCb[i, j] = (numeratorCb - rounding) / LevelScale2x2[qP % 6, 0, 0];
                    quantizedCr[i, j] = (numeratorCr - rounding) / LevelScale2x2[qP % 6, 0, 0];
                }
            }
        }
    }
}
