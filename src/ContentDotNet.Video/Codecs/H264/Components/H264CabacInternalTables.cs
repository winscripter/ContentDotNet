namespace ContentDotNet.Video.Codecs.H264.Components
{
    internal static class H264CabacInternalTables
    {
        public static ReadOnlySpan<int> SkipFlagCtxIdxBase => [-50, 0, 7, -50, -50];
        public static ReadOnlySpan<int> SkipFlagCtxIdxAccessor => [-50, 1, 2, -50, -50];
    }
}
