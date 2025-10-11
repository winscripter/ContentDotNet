namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   The arithmetic writer.
    /// </summary>
    public interface IH264ArithmeticWriter
    {
        /// <summary>
        ///   The bitstream writer.
        /// </summary>
        BitStreamWriter Writer { get; }

        /// <summary>
        ///   The bin tracker.
        /// </summary>
        IBinTracker BinTracker { get; }

        /// <summary>
        ///   The range.
        /// </summary>
        int Range { get; set; }

        /// <summary>
        ///   The low.
        /// </summary>
        int Low { get; set; }

        /// <summary>
        ///   Writes a bin.
        /// </summary>
        /// <param name="binVal">Bin value</param>
        /// <param name="binType">The bin type.</param>
        void WriteBin(bool binVal, ArithmeticBinType binType);

        /// <summary>
        ///   Writes a bin.
        /// </summary>
        /// <param name="binVal">Bin value</param>
        /// <param name="ctxIdx"></param>
        /// <param name="bypassFlag"></param>
        void WriteBin(bool binVal, int ctxIdx, bool bypassFlag);

        /// <summary>
        ///   Writes a bin.
        /// </summary>
        /// <param name="binVal">Bin value</param>
        /// <param name="binType">The bin type.</param>
        Task WriteBinAsync(bool binVal, ArithmeticBinType binType);

        /// <summary>
        ///   Writes a bin.
        /// </summary>
        /// <param name="binVal">Bin value</param>
        /// <param name="ctxIdx"></param>
        /// <param name="bypassFlag"></param>
        Task WriteBinAsync(bool binVal, int ctxIdx, bool bypassFlag);
    }
}
