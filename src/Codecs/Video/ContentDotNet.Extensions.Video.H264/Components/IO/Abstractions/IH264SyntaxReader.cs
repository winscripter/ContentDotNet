namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   Reads syntax elements for <c>slice_data</c>, <c>macroblock_layer</c>, <c>mb_pred</c>, <c>sub_mb_pred</c>,
    ///   as well as all residual types.
    /// </summary>
    public partial interface IH264SyntaxReader
    {
        /// <summary>
        ///   Miscellaneous things in this syntax reader.
        /// </summary>
        Dictionary<string, object> Miscellaneous { get; }

        /// <summary>
        ///   Uses CABAC?
        /// </summary>
        bool UsesCabac { get; }

        /// <summary>
        ///   The bitstream reader.
        /// </summary>
        BitStreamReader Reader { get; set; }

        /// <summary>
        ///   RBSP state
        /// </summary>
        H264RbspState RbspState { get; set; }

        /// <summary>
        ///   Current macroblock
        /// </summary>
        H264MacroblockInfo? MacroblockInfo { get; set; }
    }
}
