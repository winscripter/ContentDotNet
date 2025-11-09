namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Utilities;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   CABAC Contexts.
    /// </summary>
    /// <remarks>
    ///   <b>⚠️ Warning:</b> This class is subject to change in the future. Do not use. <b>⚠️</b>
    /// </remarks>
    public class H264CabacContexts
    {
        private const int NumberOfBlockTypes = 22; // Because we allow 4:4:4 chroma subsampling

        private H264CabacContextVariable[][] MbType { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(3, 11);
        private H264CabacContextVariable[][] MvContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(2, 10);
        private H264CabacContextVariable[][] RefNoContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(2, 6);

        // Texture
        private H264CabacContextVariable[] IntraPredModeContexts { get; set; } = new H264CabacContextVariable[2];
        private H264CabacContextVariable[][] OneContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(NumberOfBlockTypes, 5);
        private H264CabacContextVariable[][] AbsoluteContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(NumberOfBlockTypes, 5);

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

        /// <summary>
        ///   Returns the reference to the intra prediction mode ctx.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetIntraPredModeContextRef(int index)
        {
            return ref IntraPredModeContexts[index];
        }

        /// <summary>
        ///   Returns the reference to the reference index ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetReferenceIndexContextRef(int x, int y)
        {
            return ref RefNoContexts[x][y];
        }

        /// <summary>
        ///   Returns the reference to the one ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetOneContextRef(int x, int y)
        {
            return ref OneContexts[x][y];
        }

        /// <summary>
        ///   Returns the reference to the absolute ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetAbsoluteContextRef(int x, int y)
        {
            return ref AbsoluteContexts[x][y];
        }
    }
}
