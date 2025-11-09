namespace ContentDotNet.Api
{
    /// <summary>
    ///   Text decoration.
    /// </summary>
    [Flags]
    public enum TextDecoration : byte
    {
        /// <summary>
        ///   No decoration
        /// </summary>
        None = 0,

        /// <summary>
        ///   Italic decoration
        /// </summary>
        Italic = 1,
        
        /// <summary>
        ///   Strikethrough decoration
        /// </summary>
        Strikethrough = 2,

        /// <summary>
        ///   Bold decoration
        /// </summary>
        Bold = 4,

        /// <summary>
        ///   Underline decoration
        /// </summary>
        Underline = 8,

        /// <summary>
        ///   ALL CAPS decoration
        /// </summary>
        AllCaps = 16,

        /// <summary>
        ///  all lowercase Decoration
        /// </summary>
        AllLowercase = 32,
    }
}
