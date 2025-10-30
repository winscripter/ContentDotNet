namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Implementation
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Delegates.IO.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using ContentDotNet.Extensions.Video.H264.Extensions;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using System.Threading.Tasks;

    internal class H264CabacDecoder : IH264CabacDecoder
    {
        private readonly H264ContextVariable[] cv = new H264ContextVariable[1024];
        private readonly bool[] cvInit = new bool[1024]; // Defaults to false

        public H264DecodingVariables DecodingVariables { get; init; } = new();
        public IH264ArithmeticReader ArithmeticReader { get; init; }
        public UnprocessedContextIndexRecord? ContextIndexRecord { get; set; }

        public H264CabacDecoder(IH264ArithmeticReader arithmeticReader, H264State state)
        {
            ArithmeticReader = arithmeticReader;
            State = state;
        }

        public int BinIndex { get; set; } = 0;
        public H264State State { get; set; }

        private H264Affix affix;
        public H264Affix Affix
        {
            get => affix;
            set
            {
                affix = value;
                BinIndex = 0;
                Recompute();
            }
        }
        public RecomputeCallback Recompute { get; set; } = () => { };
        public int SuffixContextIndex { get; set; }
        public int PrefixContextIndex { get; set; }

        public bool ReadBin()
        {
            bool useBypass = Affix == H264Affix.Suffix && ContextIndexRecord?.CtxIdxOffset.UsesDecodeBypass == true;
            if (useBypass)
                return ArithmeticReader.ReadBin(ArithmeticBinType.Bypass, null);

            int ctxIdx = this.GetCtxIdx();
            if (!cvInit[ctxIdx])
            {
                InitializeContext(ctxIdx, State.DeriveSliceQpy(), (int?)SyntaxElementGrabber.FetchCabacInitIdc(State.H264RbspState) ?? 0, State.GetSliceType());
                cvInit[ctxIdx] = true;
            }

            bool ret = ArithmeticReader.ReadBin(ctxIdx,
                false, // We performed the check earlier
                cv[ctxIdx]);

            Recompute();
            BinIndex++;

            return ret;
        }

        public async Task<bool> ReadBinAsync()
        {
            bool useBypass = Affix == H264Affix.Suffix && ContextIndexRecord?.CtxIdxOffset.UsesDecodeBypass == true;
            if (useBypass)
                return await ArithmeticReader.ReadBinAsync(ArithmeticBinType.Bypass, null);

            int ctxIdx = this.GetCtxIdx();
            if (!cvInit[ctxIdx])
            {
                InitializeContext(ctxIdx, State.DeriveSliceQpy(), (int?)SyntaxElementGrabber.FetchCabacInitIdc(State.H264RbspState) ?? 0, State.GetSliceType());
                cvInit[ctxIdx] = true;
            }

            bool ret = await ArithmeticReader.ReadBinAsync(ctxIdx,
                false, // We performed the check earlier
                cv[ctxIdx]);

            Recompute();
            BinIndex++;

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
