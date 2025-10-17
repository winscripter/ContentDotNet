namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Implementation
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using System.Threading.Tasks;

    internal class H264CabacDecoder : IH264CabacDecoder
    {
        private readonly H264ContextVariable[] cv = new H264ContextVariable[1024];
        private readonly bool[] cvInit = new bool[1024]; // Defaults to false

        public H264DecodingVariables DecodingVariables { get; init; } = new();
        public IH264ArithmeticReader ArithmeticReader { get; init; }
        public UnprocessedContextIndexRecord? ContextIndexRecord { get; init; }

        public H264CabacDecoder(IH264ArithmeticReader arithmeticReader, H264State state)
        {
            ArithmeticReader = arithmeticReader;
            State = state;
        }

        public int CtxIdxSuffix { get; set; } = 0;
        public int CtxIdxPrefix { get; set; } = 0;
        public bool ForcePrefix { get; set; } = false;
        public int BinIndex { get; set; } = 0;
        public H264State State { get; set; }

        public H264Affix Affix { get; set; }
        public RecomputeCallback Recompute { get; set; } = () => { };

        public bool ReadBin()
        {
            int ctxIdx = Affix == H264Affix.Prefix ? CtxIdxPrefix : CtxIdxSuffix;
            if (!cvInit[ctxIdx])
            {
                InitializeContext(ctxIdx, State.DeriveSliceQpy(), (int)SyntaxElementGrabber.GetCabacInitIdc(State.H264RbspState), State.GetSliceType());
                cvInit[ctxIdx] = true;
            }

            bool ret = ArithmeticReader.ReadBin(ctxIdx,
                ContextIndexRecord?.MaxBinIdxCtx.UsesDecodeBypass == true || ContextIndexRecord?.CtxIdxOffset.UsesDecodeBypass == true,
                cv[ctxIdx]);
            Recompute();
            return ret;
        }

        public async Task<bool> ReadBinAsync()
        {
            int ctxIdx = Affix == H264Affix.Prefix ? CtxIdxPrefix : CtxIdxSuffix;
            if (!cvInit[ctxIdx])
            {
                InitializeContext(ctxIdx, State.DeriveSliceQpy(), (int)SyntaxElementGrabber.GetCabacInitIdc(State.H264RbspState), State.GetSliceType());
                cvInit[ctxIdx] = true;
            }

            bool ret = await ArithmeticReader.ReadBinAsync(ctxIdx,
                ContextIndexRecord?.MaxBinIdxCtx.UsesDecodeBypass == true || ContextIndexRecord?.CtxIdxOffset.UsesDecodeBypass == true,
                cv[ctxIdx]);
            Recompute();
            return ret;
        }

        public void InitializeContext(int ctxIdx, int sliceQPY, int cabacInitIdc, H264SliceType sliceType, bool reinitialize = false)
        {
            if (!reinitialize && cvInit[ctxIdx])
                throw ContextVariableReinitializationException.Instance;

            cv[ctxIdx] = H264CabacInitializer.CreateContextVariable(ctxIdx, cabacInitIdc, sliceType, sliceQPY);
            cvInit[ctxIdx] = true;
        }

        public bool IsContextInitialized(int ctxIdx) => cvInit[ctxIdx];
    }
}
