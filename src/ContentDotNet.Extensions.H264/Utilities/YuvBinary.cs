using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Utilities;

internal static class YuvBinary
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int y, int cb, int cr) Extract(int n) => ((n >> 16) & 0xFF, (n >> 8) & 0xFF, n & 0xFF);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Pack((int y, int cb, int cr) value) => (value.y & 0xFF) << 16 | (value.cb & 0xFF) << 8 | (value.cr & 0xFF);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetY(int n) => (n >> 16) & 0xFF;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetCb(int n) => (n >> 8) & 0xFF;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetCr(int n) => n & 0xFF;
}
