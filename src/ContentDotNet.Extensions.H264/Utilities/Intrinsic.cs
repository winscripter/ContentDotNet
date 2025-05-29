using System.Numerics;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Utilities;

internal static class Intrinsic
{
    // The BitLengthFast methods are considered intrinsic because .NET will utilize
    // intrinsic CPU instructions (f.e. BSR on x86) to perform this task. In other
    // case, it defaults back to a highly optimized software fallback, though, the
    // intrinsic operation is implemented for ARM64, WebAssembly, and x86.

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BitLengthFast(int x) => x == 0 ? 0 : BitOperations.Log2((uint)x) + 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BitLengthFast(uint x) => x == 0 ? 0 : BitOperations.Log2(x) + 1;

    // ---------------------------------------------------------------------------------

#if PERF
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint CeilLog2(uint value) => (uint)(BitOperations.Log2(value) + (BitOperations.IsPow2(value) ? 0 : 1));
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint CeilLog2(uint value) => (uint)Math.Ceiling(Math.Log2(value));
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsContiguousOnes(int x)
    {
        if (x == 0) return false;

        int msbIndex = 31 - BitOperations.LeadingZeroCount((uint)x);
        int lsbIndex = BitOperations.TrailingZeroCount(x);

        int maskLength = msbIndex - lsbIndex + 1;
        int mask = (1 << maskLength) - 1;

        int expected = mask << lsbIndex;

        return x == expected;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllBitsSetToOne(int value, int bitStart, int bitEnd)
    {
        for (int x = bitStart; x < bitEnd; x++)
            if ((value & (1 << x)) == 0)
                return false;
        return true;
    }
}
