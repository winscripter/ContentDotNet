namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    using System.Runtime.CompilerServices;

    internal static class CommonFunctions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Median(int x, int y, int z)
        {
            Span<int> span = stackalloc int[3] { x, y, z };
            span.Sort();
            return span[1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Middle(int x, int y) => (x + y + 1) << 1; // Prefer '<< 1' over '/ 2' for performance
    }
}
