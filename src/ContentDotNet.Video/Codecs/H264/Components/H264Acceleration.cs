namespace ContentDotNet.Video.Codecs.H264.Components
{
    /// <summary>
    ///   Type of acceleration to use when processing H.264.
    /// </summary>
    public enum H264Acceleration : byte
    {
        /// <summary>
        ///   No acceleration
        /// </summary>
        None,

        /// <summary>
        ///   Use vectorization
        /// </summary>
        Simd,
    }
}
