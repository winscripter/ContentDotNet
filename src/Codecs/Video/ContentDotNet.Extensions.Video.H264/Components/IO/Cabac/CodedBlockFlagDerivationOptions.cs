namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    // Do not make this a record type. It will only bloat the assembly size.
    // This is an internal struct, and we don't perform any comparisons or stuff
    // like that on it, so a struct is perfectly fine.

    public struct CodedBlockFlagDerivationOptions
    {
        public int luma4x4BlkIdx;
        public int iCbCr;
        public int chroma4x4BlkIdx;
        public int luma8x8BlkIdx;
        public int cb4x4BlkIdx;
        public int cb8x8BlkIdx;
        public int cr4x4BlkIdx;
        public int cr8x8BlkIdx;

        public CodedBlockFlagDerivationOptions(int luma4x4BlkIdx, int iCbCr, int chroma4x4BlkIdx, int luma8x8BlkIdx, int cb4x4BlkIdx, int cb8x8BlkIdx, int cr4x4BlkIdx, int cr8x8BlkIdx)
        {
            this.luma4x4BlkIdx = luma4x4BlkIdx;
            this.iCbCr = iCbCr;
            this.chroma4x4BlkIdx = chroma4x4BlkIdx;
            this.luma8x8BlkIdx = luma8x8BlkIdx;
            this.cb4x4BlkIdx = cb4x4BlkIdx;
            this.cb8x8BlkIdx = cb8x8BlkIdx;
            this.cr4x4BlkIdx = cr4x4BlkIdx;
            this.cr8x8BlkIdx = cr8x8BlkIdx;
        }
    }
}
