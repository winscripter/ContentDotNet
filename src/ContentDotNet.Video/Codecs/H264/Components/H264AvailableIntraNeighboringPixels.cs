namespace ContentDotNet.Video.Codecs.H264.Components
{
    /// <summary>
    ///   Neighboring Intra pixels that are marked as available.
    /// </summary>
    [Flags]
    public enum H264AvailableIntraNeighboringPixels : byte
    {
        /// <summary>
        ///   When this bit is on, the left pixels are available.
        /// </summary>
        Left = 1 << 0,

        /// <summary>
        ///   When this bit is on, the top pixels are available.
        /// </summary>
        Top = 1 << 1,

        /// <summary>
        ///   When this bit is on, the top-left pixels are available.
        /// </summary>
        TopLeft = 1 << 2,

        /// <summary>
        ///   When this bit is on, the top-right pixels are available.
        /// </summary>
        TopRight = 1 << 3
    }
}
