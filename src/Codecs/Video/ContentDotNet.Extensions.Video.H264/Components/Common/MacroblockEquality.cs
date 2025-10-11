namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   Macroblock comparison tools
    /// </summary>
    public static class MacroblockEquality
    {
        /// <summary>
        ///   Is the specified macroblock of equal type as <paramref name="type"/>?
        /// </summary>
        /// <param name="info">The source H.264 macroblock</param>
        /// <param name="type">The macroblock type</param>
        /// <returns>A boolean indicating that <paramref name="info"/> has the same macroblock type as <paramref name="type"/>.</returns>
        public static bool AreEqual(H264MacroblockInfo info, H264MacroblockType type) => info == type;

        /// <summary>
        ///   Are the sub-macroblock traits equal to the macroblock type <paramref name="type"/>?
        /// </summary>
        /// <param name="subMbType">The sub-macroblock type</param>
        /// <param name="sliceType">The macroblock traits.</param>
        /// <param name="type">The type to compare with.</param>
        /// <returns>A boolean indicating that <paramref name="subMbType"/> and <paramref name="sliceType"/> are the matching macroblock and slice types for <paramref name="type"/>.</returns>
        public static bool SubMacroblocksEqual(int subMbType, H264SliceType sliceType, H264MacroblockType type) =>
            subMbType == type.MacroblockTypeNumber && sliceType == type.SliceType;
    }
}
