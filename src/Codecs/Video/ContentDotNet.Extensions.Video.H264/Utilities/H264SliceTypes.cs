namespace ContentDotNet.Extensions.Video.H264.Utilities
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    ///   The slice types
    /// </summary>
    public static class H264SliceTypes
    {
        public static H264SliceType FetchSliceType(this RbspSliceHeader sh) => FetchSliceType(sh.SliceType);
        public static H264SliceType FetchSliceType(uint sliceType) => (H264SliceType)((int)(sliceType % 5));

        public static bool IsB(uint sliceType) => FetchSliceType(sliceType) == H264SliceType.B;
        public static bool IsI(uint sliceType) => FetchSliceType(sliceType) == H264SliceType.I;
        public static bool IsP(uint sliceType) => FetchSliceType(sliceType) == H264SliceType.P;
        public static bool IsSP(uint sliceType) => FetchSliceType(sliceType) == H264SliceType.SP;
        public static bool IsSI(uint sliceType) => FetchSliceType(sliceType) == H264SliceType.SI;
    }
}
