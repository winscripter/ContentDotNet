namespace ContentDotNet.Audio.Shared.Flac
{
    using System.Numerics;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   FLAC bit math
    /// </summary>
    public static class FlacBitMath
    {
        private static ReadOnlySpan<byte> DebruijnTable => [
            0, 1, 2, 7, 3,13, 8,19, 4,25,14,28, 9,34,20,40,
            5,17,26,38,15,46,29,48,10,31,35,54,21,50,41,57,
            63, 6,12,18,24,27,33,39,16,37,45,47,30,53,49,56,
            62,11,23,32,36,44,52,55,61,22,43,51,60,42,59,58
        ];

        /// <summary>
        ///   Counts the leading zeroes in an integer.
        /// </summary>
        /// <param name="source">Input integer.</param>
        /// <returns>The number of leading zeroes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CountLeadingZeros(uint source)
        {
            // NOTE: Do not use Lzcnt or ArmBase classes directly.
            // The BitOperations.LeadingZeroCount method does all of this for us.
            // Even if there's no intrinsic option available, it'll use a VERY fast
            // software-based approach.
            //
            // For now, this method just simplifies it so we don't manually cast to uint
            // when calling BitOperations.LeadingZeroCount.

            return (uint)BitOperations.LeadingZeroCount(source);
        }

        /// <summary>
        ///   Counts the leading zeroes in an integer.
        /// </summary>
        /// <param name="source">Input integer.</param>
        /// <returns>The number of leading zeroes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CountLeadingZeros(ulong source)
        {
            // NOTE: Do not use Lzcnt or ArmBase classes directly.
            // The BitOperations.LeadingZeroCount method does all of this for us.
            // Even if there's no intrinsic option available, it'll use a VERY fast
            // software-based approach.
            //
            // For now, this method just simplifies it so we don't manually cast to uint
            // when calling BitOperations.LeadingZeroCount.

            return (uint)BitOperations.LeadingZeroCount(source);
        }

        /// <summary>
        ///   Does ilog2 in an integer.
        /// </summary>
        /// <param name="source">Input integer.</param>
        /// <returns>The result of ilog2.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Ilog2(uint source)
        {
            return 31u ^ CountLeadingZeros(source);
        }

        /// <summary>
        ///   Does ilog2 in an integer.
        /// </summary>
        /// <param name="source">Input integer.</param>
        /// <returns>The result of ilog2.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Ilog2(ulong source)
        {
            source |= source >> 1;
            source |= source >> 2;
            source |= source >> 4;
            source |= source >> 8;
            source |= source >> 16;
            source |= source >> 32;
            source = (source >> 1) + 1;
            return DebruijnTable[(int)((source * 0x218A392CD3D5DBFuL) >> 58 & 0x3F)];
        }

        /// <summary>
        ///   Does silog2 in an integer.
        /// </summary>
        /// <param name="source">Input integer.</param>
        /// <returns>The result of silog2.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint SIlog2(long source)
        {
            if (source == 0) return 0;
            if (source == -1) return 2;
            source = (source < 0) ? (-(source + 1)) : source;
            return Ilog2((ulong)source) + 2;
        }

        /// <summary>
        ///   Does extra_mulbits_unsigned in an integer.
        /// </summary>
        /// <param name="source">Input integer.</param>
        /// <returns>The result of extra_mulbits_unsigned.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ExtraMulBitsUnsigned(uint source)
        {
            if (source == 0) return 0;
            uint ilog2 = Ilog2(source);
            if (((source >> (int)ilog2) << (int)ilog2) == source) return ilog2;
            else return ilog2 + 1;
        }
    }
}
