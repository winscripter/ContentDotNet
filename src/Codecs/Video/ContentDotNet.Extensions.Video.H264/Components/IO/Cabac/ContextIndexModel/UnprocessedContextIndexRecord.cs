namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    /// <summary>
    ///   Groups maxBinIdxCtx and ctxIdxOffset.
    /// </summary>
    public record UnprocessedContextIndexRecord
    {
        /// <summary>
        ///   maxBinIdxCtx
        /// </summary>
        public IContextIndexValue MaxBinIdxCtx { get; set; }

        /// <summary>
        ///   CtxIdxOffset
        /// </summary>
        public IContextIndexValue CtxIdxOffset { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="UnprocessedContextIndexRecord"/> class.
        /// </summary>
        /// <param name="maxBinIdxCtx">maxBinIdxCtx</param>
        /// <param name="ctxIdxOffset">CtxIdxOffset</param>
        public UnprocessedContextIndexRecord(IContextIndexValue maxBinIdxCtx, IContextIndexValue ctxIdxOffset)
        {
            MaxBinIdxCtx = maxBinIdxCtx;
            CtxIdxOffset = ctxIdxOffset;
        }
    }
}
