namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   Macroblock utility extensions.
    /// </summary>
    public static class MacroblockUtilityExtensions
    {
        public static bool IsIntra(this IMacroblockUtility util, H264MacroblockInfo mb) =>
            util.GetSliceType(mb) == H264SliceType.I;

        public static bool IsIntra(this IMacroblockUtility util, int address) =>
            util.IsIntra(util.GetMacroblock(address));

        public static bool IsInter(this IMacroblockUtility util, H264MacroblockInfo mb) =>
            util.GetSliceType(mb) is H264SliceType.P or H264SliceType.B or H264SliceType.SP;

        public static bool IsInter(this IMacroblockUtility util, int address) =>
            util.IsInter(util.GetMacroblock(address));
    }
}
