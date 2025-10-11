namespace ContentDotNet.Extensions.Video.H264.Extensions
{
    using System.Runtime.InteropServices;

    internal static class InternalExtensions
    {
        public static List<int> CastAsInt32(this List<uint> source) => [.. source.Select(x => (int)x)];

        public static int[] AsInt32Array(this List<uint> source) => CollectionsMarshal.AsSpan(source).ToArray().CastAsInt32();

        public static int[] CastAsInt32(this uint[] source) => [.. source.Select(x => (int)x)];
    }
}
