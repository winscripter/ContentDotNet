namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization
{
    /// <summary>
    ///   Provides the result of the TU (Truncated Unary) H.264 CABAC binarization.
    /// </summary>
    public record struct TuResult
    {
        /// <summary>
        ///   The actual value.
        /// </summary>
        public int Value;

        /// <summary>
        ///   The total number of bins that were read.
        /// </summary>
        public int BinsRead;

        internal TuResult(int value, int binsRead)
        {
            Value = value;
            BinsRead = binsRead;
        }
    }
}
