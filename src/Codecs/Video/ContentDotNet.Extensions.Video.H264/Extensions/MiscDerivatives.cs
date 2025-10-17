namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Utilities;

    public static partial class H264Extensions
    {
        public static H264SliceType GetSliceType(this H264State state)
        {
            if (state.H264RbspState == null || state.H264RbspState.SliceHeader == null)
                throw new InvalidOperationException("Missing RBSP states");

            return H264SliceTypes.FetchSliceType(state.H264RbspState.SliceHeader);
        }
    }
}
