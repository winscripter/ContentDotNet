namespace ContentDotNet
{
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   Highly optimized functions.
    /// </summary>
    public static class IntrinsicFunctions
    {
        /// <summary>
        ///   Checks if all bits are set equal to 1 starting with bit 1 ending with bit <paramref name="topBit"/>.
        /// </summary>
        /// <param name="value">The source integer</param>
        /// <param name="topBit">The highest bit</param>
        /// <returns>A boolean</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllBitsAreOne(int value, int topBit)
        {
            int mask = (1 << (topBit + 1)) -1;
            return (value & mask) == mask;
        }

        /// <summary>
        ///   Clips <paramref name="c"/>. If it's greater than <paramref name="b"/>, it becomes
        ///   <paramref name="b"/>. If it's less than <paramref name="a"/>, it becomes <paramref name="a"/>.
        ///   Otherwise, it's kept as is.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns><paramref name="c"/>, making sure that it is within range of <paramref name="a"/> - <paramref name="b"/>, inclusive.</returns>
        public static int Clip3(int a, int b, int c)
        {
            if (c < a)
            {
                return a;
            }
            else if (c > b)
            {
                return b;
            }
            else
            {
                return c;
            }
        }
    }
}
