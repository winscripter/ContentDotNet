namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    public static partial class H264Extensions
    {
        public static H264SliceType GetSliceType(this H264State state)
        {
            return (H264SliceType)((state.H264RbspState?.SliceHeader?.SliceType ?? 0) % 5);
        }
    }
}
