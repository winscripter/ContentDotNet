namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    public static partial class MacroblockTypes
    {
        public static readonly H264MacroblockType P_Skip = new(H264SliceType.P, 0, true);
        public static readonly H264MacroblockType B_Skip = new(H264SliceType.B, 0, true);
    }
}
