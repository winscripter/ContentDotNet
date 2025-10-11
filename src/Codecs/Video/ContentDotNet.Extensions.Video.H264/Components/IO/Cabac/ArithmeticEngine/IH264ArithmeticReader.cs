namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   The arithmetic reader.
    /// </summary>
    public interface IH264ArithmeticReader
    {
        /// <summary>
        ///   The bitstream reader.
        /// </summary>
        BitStreamReader Reader { get; }

        /// <summary>
        ///   The bin tracker.
        /// </summary>
        IBinTracker BinTracker { get; }

        /// <summary>
        ///   The range.
        /// </summary>
        int Range { get; set; }

        /// <summary>
        ///   The offset.
        /// </summary>
        int Offset { get; set; }

        /// <summary>
        ///   Reads a bin.
        /// </summary>
        /// <param name="binType">The bin type.</param>
        /// <param name="contextVariable">
        ///   This is the context variable. While you can pass null to it and not use it,
        ///   it is mandatory when reading a decision. In addition, this method will make
        ///   changes to the context variable when reading a decision. When not reading
        ///   a decision, i.e., when reading a bypass or termination bin, this parameter
        ///   is not used, even if it's null.
        /// </param>
        /// <returns>A boolean.</returns>
        bool ReadBin(ArithmeticBinType binType, H264ContextVariable? contextVariable);

        /// <summary>
        ///   Reads a bin.
        /// </summary>
        /// <param name="ctxIdx"></param>
        /// <param name="contextVariable">
        ///   This is the context variable. While you can pass null to it and not use it,
        ///   it is mandatory when reading a decision. In addition, this method will make
        ///   changes to the context variable when reading a decision. When not reading
        ///   a decision, i.e., when reading a bypass or termination bin, this parameter
        ///   is not used, even if it's null.
        /// </param>
        /// <param name="bypassFlag"></param>
        /// <returns>A boolean.</returns>
        bool ReadBin(int ctxIdx, bool bypassFlag, H264ContextVariable? contextVariable);

        /// <summary>
        ///   Reads a bin.
        /// </summary>
        /// <param name="binType">The bin type.</param>
        /// <param name="contextVariable">
        ///   This is the context variable. While you can pass null to it and not use it,
        ///   it is mandatory when reading a decision. In addition, this method will make
        ///   changes to the context variable when reading a decision. When not reading
        ///   a decision, i.e., when reading a bypass or termination bin, this parameter
        ///   is not used, even if it's null.
        /// </param>
        /// <returns>A boolean.</returns>
        Task<bool> ReadBinAsync(ArithmeticBinType binType, H264ContextVariable? contextVariable);

        /// <summary>
        ///   Reads a bin.
        /// </summary>
        /// <param name="ctxIdx"></param>
        /// <param name="bypassFlag"></param>
        /// <param name="contextVariable">
        ///   This is the context variable. While you can pass null to it and not use it,
        ///   it is mandatory when reading a decision. In addition, this method will make
        ///   changes to the context variable when reading a decision. When not reading
        ///   a decision, i.e., when reading a bypass or termination bin, this parameter
        ///   is not used, even if it's null.
        /// </param>
        /// <returns>A boolean.</returns>
        Task<bool> ReadBinAsync(int ctxIdx, bool bypassFlag, H264ContextVariable? contextVariable);
    }
}
