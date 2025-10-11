namespace ContentDotNet.Extensions.Video.H264.Components.Transforms.Transformers
{
    /// <summary>
    ///   Transforms H.264 residuals coefficients
    /// </summary>
    public interface ICoefficientTransformer
    {
        /// <summary>
        ///   Transform Chroma DC Transform coefficients.
        /// </summary>
        /// <param name="f">f</param>
        /// <param name="c">c</param>
        /// <param name="ChromaArrayType">ChromaArrayType</param>
        public void TransformChromaDcTransformCoefficients(
            int[,] f,
            int[,] c,
            int ChromaArrayType);

        /// <summary>
        ///   Transform Residual 4x4 blocks
        /// </summary>
        /// <param name="d">d</param>
        /// <param name="r">r (output)</param>
        public void TransformResidual4x4Blocks(
            int[,] d,
            int[,] r);

        /// <summary>
        ///   Transform Residual 8x8 blocks.
        /// </summary>
        /// <param name="d">d</param>
        /// <param name="r">r (output)</param>
        public void TransformResidual8x8Blocks(
            int[,] d,
            int[,] r);
    }
}
