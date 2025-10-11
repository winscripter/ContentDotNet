namespace ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   H.264 reference picture macroblock
    /// </summary>
    public record H264ReferencePictureMacroblock
    {
        /// <summary>
        ///   Rendered 16x16 image of the macroblock
        /// </summary>
        public Picture<YCbCr> Picture16x16 { get; set; }

        /// <summary>
        ///   The macroblock info.
        /// </summary>
        public H264MacroblockInfo MacroblockInfo { get; set; }

        public H264ReferencePictureMacroblock(Picture<YCbCr> picture16x16, H264MacroblockInfo macroblockInfo)
        {
            Picture16x16 = picture16x16;
            MacroblockInfo = macroblockInfo;
        }

        public static bool operator ==(H264ReferencePictureMacroblock info, H264MacroblockType type)
        {
            return info.MacroblockInfo == type;
        }

        public static bool operator !=(H264ReferencePictureMacroblock info, H264MacroblockType type) => !(info == type);
    }
}
