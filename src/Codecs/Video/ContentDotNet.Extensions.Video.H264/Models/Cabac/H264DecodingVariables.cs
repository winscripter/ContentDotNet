namespace ContentDotNet.Extensions.Video.H264.Models.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   Variables necessary for various decoding functions in the H.264 CABAC decoder.
    /// </summary>
    public class H264DecodingVariables
    {
        /// <summary>
        ///   Reference index list 0
        /// </summary>
        public List<uint> RefIdxL0 { get; set; } = [];

        /// <summary>
        ///   Reference index list 1
        /// </summary>
        public List<uint> RefIdxL1 { get; set; } = [];

        /// <summary>
        ///   Motion vector difference list 0
        /// </summary>
        public H264MotionVectorDifference MvdL0 { get; set; } = new();

        /// <summary>
        ///   Motion vector difference list 1
        /// </summary>
        public H264MotionVectorDifference MvdL1 { get; set; } = new();

        /// <summary>
        ///   Chroma array type
        /// </summary>
        public int ChromaArrayType { get; set; }

        /// <summary>
        ///   Macroblock partition index
        /// </summary>
        public int MbPartIdx { get; set; }

        /// <summary>
        ///   List type
        /// </summary>
        public H264ListType ListType { get; set; }

        /// <summary>
        ///   Sub-macroblock type
        /// </summary>
        public List<uint> SubMbType { get; set; } = [0u, 0u, 0u, 0u];

        /// <summary>
        ///   Coded block flag options
        /// </summary>
        public CodedBlockFlagDerivationOptions CodedBlockFlagOptions { get; set; } = new(0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        ///   Ctx block cat
        /// </summary>
        public int CtxBlockCat { get; set; }

        /// <summary>
        ///   Previous macroblock address
        /// </summary>
        public AddressAndAvailability PreviousMacroblockAddress { get; set; }

        /// <summary>
        ///   Numc8x8
        /// </summary>
        public int NumC8x8 { get; set; }

        /// <summary>
        ///   Level list index
        /// </summary>
        public int LevelListIndex { get; set; }

        /// <summary>
        ///   The residual block type
        /// </summary>
        public ResidualBlockType ResidualBlockType { get; set; }

        /// <summary>
        ///   All coefficients in the current list that are equal to 1
        /// </summary>
        public int ReportedCoefficientsForCurrentListEqualTo1 { get; set; }

        /// <summary>
        ///   All coefficients in the current list that are greater than 1
        /// </summary>
        public int ReportedCoefficientsForCurrentListGreaterThan1 { get; set; }
    }
}
