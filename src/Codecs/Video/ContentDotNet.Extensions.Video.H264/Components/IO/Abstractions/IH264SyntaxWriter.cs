namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   H.264 syntax writer.
    /// </summary>
    public partial interface IH264SyntaxWriter
    {
        /// <summary>
        ///   Uses CABAC?
        /// </summary>
        bool UsesCabac { get; }

        /// <summary>
        ///   The bitstream writer.
        /// </summary>
        BitStreamWriter Writer { get; set; }

        /// <summary>
        ///   RBSP state
        /// </summary>
        H264RbspState RbspState { get; set; }
    }
}
