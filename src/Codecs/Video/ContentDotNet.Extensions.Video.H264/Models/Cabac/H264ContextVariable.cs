namespace ContentDotNet.Extensions.Video.H264.Models.Cabac
{
    /// <summary>
    ///   H.264 context variable.
    /// </summary>
    public record H264ContextVariable
    {
        /// <summary>
        ///   Most probable symbol.
        /// </summary>
        public int ValMps { get; set; }

        /// <summary>
        ///   State index.
        /// </summary>
        public int PStateIdx { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264ContextVariable"/> class.
        /// </summary>
        public H264ContextVariable()
        {
        }
    }
}
