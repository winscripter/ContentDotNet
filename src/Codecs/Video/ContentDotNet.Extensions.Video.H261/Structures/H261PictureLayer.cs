namespace ContentDotNet.Extensions.Video.H261.Structures
{
    /// <summary>
    ///   The H.261 picture layer.
    /// </summary>
    public struct H261PictureLayer
    {
        /// <summary>
        ///   The picture start code.
        /// </summary>
        public uint PictureStartCode;

        /// <summary>
        ///   The temporal reference.
        /// </summary>
        public uint TemporalReference;

        /// <summary>
        ///   The type information.
        /// </summary>
        public uint TypeInformation;

        /// <summary>
        ///   Extra insertion information bit.
        /// </summary>
        public bool ExtraInsertionInformation;

        /// <summary>
        ///   The spare information.
        /// </summary>
        public uint SpareInformation;
    }
}
