namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;

    /// <summary>
    ///   Abstracts an H.264 CABAC Decoder
    /// </summary>
    public interface IH264CabacDecoder
    {
        /// <summary>
        ///   Decoding variables.
        /// </summary>
        H264DecodingVariables DecodingVariables { get; }

        /// <summary>
        ///   The arithmetic decoder.
        /// </summary>
        IH264ArithmeticReader ArithmeticReader { get; }

        /// <summary>
        ///   Context indices
        /// </summary>
        UnprocessedContextIndexRecord? ContextIndexRecord { get; }

        /// <summary>
        ///   Suffix ctxIdx
        /// </summary>
        int CtxIdxSuffix { get; set; }

        /// <summary>
        ///   Prefix ctxIdx
        /// </summary>
        int CtxIdxPrefix { get; set; }

        /// <summary>
        ///   Forces all affixes to be prefix only. Do not use this without knowing what
        ///   you're doing, as it is almost certainly guaranteed to corrupt the decoder
        ///   when used incorrectly.
        /// </summary>
        bool ForcePrefix { get; set; }

        /// <summary>
        ///   The bin index.
        /// </summary>
        int BinIndex { get; set; }

        /// <summary>
        ///   The affix.
        /// </summary>
        H264Affix Affix { get; set; }

        /// <summary>
        ///   Invoked after each <see cref="ReadBin"/> and <see cref="ReadBinAsync"/>
        ///   method invocation.
        /// </summary>
        RecomputeCallback Recompute { get; set; }

        /// <summary>
        ///   Reads a single bin.
        /// </summary>
        /// <returns>The bin.</returns>
        bool ReadBin();

        /// <summary>
        ///   Reads a single bin.
        /// </summary>
        /// <returns>The bin.</returns>
        Task<bool> ReadBinAsync();

        /// <summary>
        ///   Initializes a context variable <paramref name="ctxIdx"/>.
        /// </summary>
        /// <param name="ctxIdx">The context index.</param>
        /// <param name="sliceQPY">The slice QPY</param>
        /// <param name="cabacInitIdc">Initialization identifier code</param>
        /// <param name="sliceType">Slice type</param>
        /// <param name="reinitialize">
        /// When false and the context variable at <paramref name="ctxIdx" /> was already initialized, a
        /// <see cref="ContextVariableReinitializationException"/> is thrown. But if true, that context
        /// variable gets reset with the given values accordingly.
        /// </param>
        void InitializeContext(int ctxIdx, int sliceQPY, int cabacInitIdc, H264SliceType sliceType, bool reinitialize = false);
    }
}
