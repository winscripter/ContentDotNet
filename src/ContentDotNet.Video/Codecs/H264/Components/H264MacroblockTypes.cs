namespace ContentDotNet.Video.Codecs.H264.Components
{
    public static partial class H264MacroblockTypes
    {
        public static readonly H264MacroblockType P_Skip = new(H264SliceType.P, 0, true);
        public static readonly H264MacroblockType B_Skip = new(H264SliceType.B, 0, true);
    }
}
