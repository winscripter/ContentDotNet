namespace ContentDotNet.Utilities
{
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   Math operations that are faster than the .NET built-in <see cref="Math"/> class.
    /// </summary>
    public static class FastMath
    {
        /// <summary>
        ///   Implementation of the Abs function that is slightly faster than the traditional <see cref="Math.Abs"/>
        ///   method, resulting in less JIT instructions. This version never throws <see cref="OverflowException"/>.
        /// </summary>
        /// <param name="src">The source value.</param>
        /// <returns>Result of the Abs function.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DangerousAbs(int src)
        {
            return src < 0 ? -src : src;
        }

        /// <summary>
        ///   Implementation of the Abs function that is slightly faster than the traditional <see cref="Math.Abs"/>
        ///   method, resulting in less JIT instructions. This version never throws <see cref="OverflowException"/>.
        /// </summary>
        /// <param name="src">The source value.</param>
        /// <returns>Result of the Abs function.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short DangerousAbs(short src)
        {
            return src < 0 ? (short)-src : src;
        }

        /// <summary>
        ///   Implementation of the Abs function that is slightly faster than the traditional <see cref="Math.Abs"/>
        ///   method, resulting in less JIT instructions. This version never throws <see cref="OverflowException"/>.
        /// </summary>
        /// <param name="src">The source value.</param>
        /// <returns>Result of the Abs function.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long DangerousAbs(long src)
        {
            return src < 0 ? -src : src;
        }

        /// <summary>
        ///   Implementation of the Abs function that is slightly faster than the traditional <see cref="Math.Abs"/>
        ///   method, resulting in less JIT instructions. This version never throws <see cref="OverflowException"/>.
        /// </summary>
        /// <param name="src">The source value.</param>
        /// <returns>Result of the Abs function.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte DangerousAbs(sbyte src)
        {
            return src < 0 ? (sbyte)-src : src;
        }
    }
}
