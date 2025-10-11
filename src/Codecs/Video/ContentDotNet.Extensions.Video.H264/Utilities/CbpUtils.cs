namespace ContentDotNet.Extensions.Video.H264.Utilities
{
    internal static class CbpUtils
    {
        public static int GetCodedBlockPattern(int cbpLuma, int cbpChroma)
        {
            return cbpChroma * 16 + cbpLuma;
        }
    }
}
