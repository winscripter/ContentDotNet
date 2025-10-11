namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    public static partial class H264Extensions
    {
        public static int GetCodedBlockPatternLuma(
            this RbspMacroblockLayer mb)
        {
            return mb.CodedBlockPattern % 16;
        }

        public static int GetCodedBlockPatternChroma(
            this RbspMacroblockLayer mb)
        {
            return mb.CodedBlockPattern / 16;
        }
    }
}
