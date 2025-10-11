namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   H.264 state
    /// </summary>
    public class H264State
    {
        /// <summary>
        ///   Current macroblock address.
        /// </summary>
        public int CurrMbAddr { get; set; }

        /// <summary>
        ///   RBSP state.
        /// </summary>
        public H264RbspState? H264RbspState { get; set; }

        /// <summary>
        ///   The macroblock utility.
        /// </summary>
        public IMacroblockUtility MacroblockUtility { get; set; }

        /// <summary>
        ///   The chroma macroblock sizes.
        /// </summary>
        public H264MacroblockChromaSizes ChromaSizes { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264State"/> class.
        /// </summary>
        /// <param name="util">The macroblock utility.</param>
        public H264State(IMacroblockUtility util)
        {
            MacroblockUtility = util;
        }
    }
}
