namespace ContentDotNet.Extensions.Video.H264.Enumerations
{
    /// <summary>
    ///   The residual block type.
    /// </summary>
    public enum ResidualBlockType
    {
        /// <summary>
        ///   DC coefficients of the Intra 16x16 luma block.
        /// </summary>
        Intra16x16DCLevel,

        /// <summary>
        ///   AC coefficients of the Intra 16x16 luma block.
        /// </summary>
        Intra16x16ACLevel,

        /// <summary>
        ///   Luma coefficients in 4x4 transform blocks.
        /// </summary>
        LumaLevel4x4,

        /// <summary>
        ///   Luma coefficients in 8x8 transform blocks.
        /// </summary>
        LumaLevel8x8,

        /// <summary>
        ///   DC coefficients of the chroma blue (Cb) 16x16 block.
        /// </summary>
        Cb16x16DCLevel,

        /// <summary>
        ///   AC coefficients of the chroma blue (Cb) 16x16 block.
        /// </summary>
        Cb16x16ACLevel,

        /// <summary>
        ///   Chroma blue (Cb) coefficients in 4x4 transform blocks.
        /// </summary>
        CbLevel4x4,

        /// <summary>
        ///   Chroma blue (Cb) coefficients in 8x8 transform blocks.
        /// </summary>
        CbLevel8x8,

        /// <summary>
        ///   DC coefficients of the chroma red (Cr) 16x16 block.
        /// </summary>
        Cr16x16DCLevel,

        /// <summary>
        ///   AC coefficients of the chroma red (Cr) 16x16 block.
        /// </summary>
        Cr16x16ACLevel,

        /// <summary>
        ///   Chroma red (Cr) coefficients in 4x4 transform blocks.
        /// </summary>
        CrLevel4x4,

        /// <summary>
        ///   Chroma red (Cr) coefficients in 8x8 transform blocks.
        /// </summary>
        CrLevel8x8,

        /// <summary>
        ///   DC coefficients of the chroma block.
        /// </summary>
        ChromaDCLevel,

        /// <summary>
        ///   AC coefficients of the chroma block.
        /// </summary>
        ChromaACLevel
    }
}
