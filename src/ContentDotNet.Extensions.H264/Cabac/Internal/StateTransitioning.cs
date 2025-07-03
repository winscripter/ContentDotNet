using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Cabac.Internal;

internal static class StateTransitioning
{
    private static ReadOnlySpan<int> TransIdxLPS =>
    [
        0, 0, 1, 2, 2, 4, 4, 5, 6, 7, 8, 9, 9, 11, 11, 12,
        13, 13, 15, 15, 16, 16, 18, 18, 19, 19, 21, 21, 22, 22, 23, 24,
        24, 25, 26, 26, 27, 27, 28, 29, 29, 30, 30, 30, 31, 32, 32, 33,
        33, 33, 34, 34, 35, 35, 35, 36, 36, 36, 37, 37, 37, 38, 38, 63,
    ];

    private static ReadOnlySpan<int> TransIdxMPS =>
    [
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
        17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32,
        33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
        49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 62, 63,
    ];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetLps(int pStateIdx) => TransIdxLPS[pStateIdx];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMps(int pStateIdx) => TransIdxMPS[pStateIdx];

    public static void Apply(ref CabacContext cabac, bool binVal)
    {
        if (binVal == cabac.ValMps)
        {
            cabac.PStateIdx = GetMps(cabac.PStateIdx);
        }
        else
        {
            if (cabac.PStateIdx == 0)
                cabac.ValMps = !cabac.ValMps;
            cabac.PStateIdx = GetLps(cabac.PStateIdx);
        }
    }
}
