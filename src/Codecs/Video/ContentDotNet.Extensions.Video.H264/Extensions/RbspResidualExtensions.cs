namespace ContentDotNet.Extensions.Video.H264.Extensions
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    ///   Extensions for RBSP residuals.
    /// </summary>
    public static class RbspResidualExtensions
    {
        /// <summary>
        ///   Returns the chroma level 4x4 based on iCbCr (0 for Cb, 1 for Cr).
        /// </summary>
        /// <param name="residual">The source residual.</param>
        /// <param name="iCbCr">The index of the chroma channel.</param>
        /// <returns>The 4x4 residual chroma level - either Cb or Cr depending on <paramref name="iCbCr"/>.</returns>
        public static List<List<int>> GetChromaLevel4x4(
            this RbspResidual residual,
            int iCbCr)
            => iCbCr == 0 ? residual.CbLevel4x4 : residual.CrLevel4x4;

        /// <summary>
        ///   Returns the chroma level 8x8 based on iCbCr (0 for Cb, 1 for Cr).
        /// </summary>
        /// <param name="residual">The source residual.</param>
        /// <param name="iCbCr">The index of the chroma channel.</param>
        /// <returns>The 8x8 residual chroma level - either Cb or Cr depending on <paramref name="iCbCr"/>.</returns>
        public static List<List<int>> GetChromaLevel8x8(
            this RbspResidual residual,
            int iCbCr)
            => iCbCr == 0 ? residual.CbLevel8x8 : residual.CrLevel8x8;

        /// <summary>
        ///   Returns the chroma level 16x16 (AC) based on iCbCr (0 for Cb, 1 for Cr).
        /// </summary>
        /// <param name="residual">The source residual.</param>
        /// <param name="iCbCr">The index of the chroma channel.</param>
        /// <returns>The 16x16 (AC) residual chroma level - either Cb or Cr depending on <paramref name="iCbCr"/>.</returns>
        public static List<List<int>> GetChroma16x16Ac(
            this RbspResidual residual,
            int iCbCr)
            => iCbCr == 0 ? residual.CbIntra16x16ACLevel : residual.CrIntra16x16ACLevel;

        /// <summary>
        ///   Returns the chroma level 16x16 (DC) based on iCbCr (0 for Cb, 1 for Cr).
        /// </summary>
        /// <param name="residual">The source residual.</param>
        /// <param name="iCbCr">The index of the chroma channel.</param>
        /// <returns>The 16x16 (DC) residual chroma level - either Cb or Cr depending on <paramref name="iCbCr"/>.</returns>
        public static List<int> GetChroma16x16Dc(
            this RbspResidual residual,
            int iCbCr)
            => iCbCr == 0 ? residual.CbIntra16x16DCLevel : residual.CrIntra16x16DCLevel;
    }
}
