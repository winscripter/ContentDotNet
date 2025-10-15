namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    using ContentDotNet.Linq;
    using ContentDotNet.Primitives;
    using System.Runtime.CompilerServices;

    internal static class JpegMathematicalFunctions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XY ComputeDimensionsAndScalingFactors(
            ReadOnlySpan<int> h,
            ReadOnlySpan<int> v,
            int i,
            XY xy)
            => new(
                (int)Math.Ceiling(xy.X * ((float)h[i] / h.Max())),
                (int)Math.Ceiling(xy.Y * ((float)v[i] / v.Max()))
            );
    }
}
