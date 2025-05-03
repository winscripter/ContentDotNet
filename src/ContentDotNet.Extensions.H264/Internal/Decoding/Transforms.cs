using ContentDotNet.Extensions.H264.Containers;

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

    public static void ScaleTransformDCTransformCoefficientsForIntra16x16(int bitDepth, int qP, bool transformBypassFlag, ContainerMatrix4x4 c, out ContainerMatrix4x4Int32 dcY)
    {
        if (transformBypassFlag)
        {
            dcY = new();
        }
        else
        {
            ContainerMatrix4x4Int32 f = new();
            f[0, 0] = 1 * (int)c[0, 0] * 1;
            f[0, 1] = 1 * (int)c[0, 1] * 1;
            f[0, 2] = 1 * (int)c[0, 2] * 1;
            f[0, 3] = 1 * (int)c[0, 3] * 1;

            f[1, 0] = 1 * (int)c[1, 0] * 1;
            f[1, 1] = 1 * (int)c[1, 1] * 1;
            f[1, 2] = -1 * (int)c[1, 2] * -1;
            f[1, 3] = -1 * (int)c[1, 3] * -1;

            f[2, 0] = 1 * (int)c[2, 0] * 1;
            f[2, 1] = -1 * (int)c[2, 1] * -1;
            f[2, 2] = -1 * (int)c[2, 2] * -1;
            f[2, 3] = 1 * (int)c[2, 3] * 1;

            f[3, 0] = 1 * (int)c[3, 0] * 1;
            f[3, 1] = -1 * (int)c[3, 1] * -1;
            f[3, 2] = 1 * (int)c[3, 2] * 1;
            f[3, 3] = -1 * (int)c[3, 3] * -1;

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
}
