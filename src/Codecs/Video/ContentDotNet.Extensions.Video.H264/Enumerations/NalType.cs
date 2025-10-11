namespace ContentDotNet.Extensions.Video.H264.Enumerations
{
    /// <summary>
    ///   The NAL unit type.
    /// </summary>
    public enum NalType
    {
        /// <summary>
        ///   Sequence parameter set
        /// </summary>
        Sps,

        /// <summary>
        ///   Picture parameter set
        /// </summary>
        Pps,

        /// <summary>
        ///   IDR frame
        /// </summary>
        Idr,

        /// <summary>
        ///   Non-IDR frame
        /// </summary>
        NonIdr,

        /// <summary>
        ///   Access unit delimiter
        /// </summary>
        Aud,

        /// <summary>
        ///   Unknown NAL unit type
        /// </summary>
        Unknown = 0xFF,

        /// <summary>
        ///   NAL wasn't read
        /// </summary>
        DidNotRead
    }
}
