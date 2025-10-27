namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Delegates.IO.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;

    /// <summary>
    ///   Abstracts an H.264 CABAC Decoder
    /// </summary>
    public interface IH264CabacDecoder
    {
        /// <summary>
        ///   Context index for suffix
        /// </summary>
        int SuffixContextIndex { get; set; }

        /// <summary>
        ///   Context index for prefix
        /// </summary>
        int PrefixContextIndex { get; set; }

        /// <summary>
        ///   The H.264 state.
        /// </summary>
        H264State State { get; }

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
        UnprocessedContextIndexRecord? ContextIndexRecord { get; set; }

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

        /// <summary>
        ///   Is the context variable <paramref name="ctxIdx"/> already initialized?
        /// </summary>
        /// <param name="ctxIdx">The context variable index.</param>
        /// <returns>A boolean, indicating if the context variable <paramref name="ctxIdx"/> is already initialized.</returns>
        bool IsContextInitialized(int ctxIdx);
    }
}
