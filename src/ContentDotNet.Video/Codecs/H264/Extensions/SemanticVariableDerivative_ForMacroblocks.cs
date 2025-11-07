namespace ContentDotNet.Video.Codecs.H264.Extensions
{
    using ContentDotNet.Video.Codecs.H264.Rbsp;

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
