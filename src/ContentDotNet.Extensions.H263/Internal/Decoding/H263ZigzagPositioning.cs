using ContentDotNet.Containers;

namespace ContentDotNet.Extensions.H263.Internal.Decoding;

internal static partial class H263ZigzagPositioning
{
    private static ReadOnlySpan<int> ZigzagMap =>
    [
         0,  1,  5,  6, 14, 15, 27, 28,
         2,  4,  7, 13, 16, 26, 29, 42,
         3,  8, 12, 17, 25, 30, 41, 43,
         9, 11, 18, 24, 31, 40, 44, 53,
        10, 19, 23, 32, 39, 45, 52, 54,
        20, 22, 33, 38, 46, 51, 55, 60,
        21, 34, 37, 47, 50, 56, 59, 61,
        35, 36, 48, 49, 57, 58, 62, 63
    ];


    public static ContainerMatrix8x8Int32 PerformZigzagPosition(ContainerMatrix8x8Int32 m8x8)
    {
        var retval = new ContainerMatrix8x8Int32();

        Span<int> flatInput = stackalloc int[64];
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
                flatInput[y * 8 + x] = m8x8[y, x];

        for (int i = 0; i < 64; i++)
        {
            int index = ZigzagMap[i];
            int row = i / 8;
            int col = i % 8;
            retval[row, col] = flatInput[index];
        }

        return retval;
    }
}
