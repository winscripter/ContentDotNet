namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Utilities;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   Contexts.
    /// </summary>
    public class H264CabacContexts
    {
        private H264CabacContextVariable[][] MbType { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(3, 11);
        private H264CabacContextVariable[][] MvContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(2, 10);

        /// <summary>
        ///   Returns the reference to the motion vector ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetMotionVectorContextRef(int x, int y)
        {
            return ref MvContexts[x][y];
        }

        /// <summary>
        ///   Returns the reference to the macroblock type ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetMacroblockTypeContextRef(int x, int y)
        {
            return ref MbType[x][y];
        }
    }
}
