namespace ContentDotNet.Video.Codecs.H264.Components
{
    using System.Runtime.CompilerServices;

    internal struct H264PB8Mode
    {
        public int B8mode, B8pdir;

        public H264PB8Mode(int b8mode, int b8pdir)
        {
            B8mode = b8mode;
            B8pdir = b8pdir;
        }
    }

    internal static class H264PB8x8Modes
    {
        private const int IBLOCK = 11;

        private static ReadOnlySpan<int> Pv2b8 => [4, 5, 6, 7, IBLOCK];
        private static ReadOnlySpan<int> Pv2pd => [0, 0, 0, 0, -1];
        private static ReadOnlySpan<int> Bv2b8 => [0, 4, 4, 4, 5, 6, 5, 6, 5, 6, 7, 7, 7, IBLOCK];
        private static ReadOnlySpan<int> Bv2pd => [2, 0, 1, 2, 0, 0, 1, 1, 2, 2, 0, 1, 2, -1];

        /// <summary>
        ///   Called when parsing sub_mb_type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sliceType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static H264PB8Mode GetPB8Mode(int value, H264SliceType sliceType)
        {
            if (sliceType == H264SliceType.B) return new(Bv2b8[value], Bv2pd[value]);
            else return new(Pv2b8[value], Pv2pd[value]);
        }
    }
}
