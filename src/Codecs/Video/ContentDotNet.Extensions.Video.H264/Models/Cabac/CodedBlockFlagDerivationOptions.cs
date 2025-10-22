namespace ContentDotNet.Extensions.Video.H264.Models.Cabac
{
    public class CodedBlockFlagDerivationOptions
    {
        public int Luma4x4BlkIdx { get; set; }
        public int ICbCr { get; set; }
        public int Chroma4x4BlkIdx { get; set; }
        public int Luma8x8BlkIdx { get; set; }
        public int Cb4x4BlkIdx { get; set; }
        public int Cb8x8BlkIdx { get; set; }
        public int Cr4x4BlkIdx { get; set; }
        public int Cr8x8BlkIdx { get; set; }

        public CodedBlockFlagDerivationOptions(int luma4x4BlkIdx, int iCbCr, int chroma4x4BlkIdx, int luma8x8BlkIdx, int cb4x4BlkIdx, int cb8x8BlkIdx, int cr4x4BlkIdx, int cr8x8BlkIdx)
        {
            this.Luma4x4BlkIdx = luma4x4BlkIdx;
            this.ICbCr = iCbCr;
            this.Chroma4x4BlkIdx = chroma4x4BlkIdx;
            this.Luma8x8BlkIdx = luma8x8BlkIdx;
            this.Cb4x4BlkIdx = cb4x4BlkIdx;
            this.Cb8x8BlkIdx = cb8x8BlkIdx;
            this.Cr4x4BlkIdx = cr4x4BlkIdx;
            this.Cr8x8BlkIdx = cr8x8BlkIdx;
        }
    }
}
