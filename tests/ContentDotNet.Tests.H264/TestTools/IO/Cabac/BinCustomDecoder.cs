namespace ContentDotNet.Tests.H264.TestTools.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Delegates.IO.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;
    using System.Threading.Tasks;

    internal class BinCustomDecoder(bool[] binSequence) : IH264CabacDecoder
    {
        public H264State State => throw new NotImplementedException();

        public H264DecodingVariables DecodingVariables => throw new NotImplementedException();

        public IH264ArithmeticReader ArithmeticReader => throw new NotImplementedException();

        public UnprocessedContextIndexRecord? ContextIndexRecord { get; set; }

        public int CtxIdxSuffix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CtxIdxPrefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ForcePrefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BinIndex { get; set; }
        public H264Affix Affix { get; set; }
        public RecomputeCallback Recompute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void InitializeContext(int ctxIdx, int sliceQPY, int cabacInitIdc, H264SliceType sliceType, bool reinitialize = false)
        {
            throw new NotImplementedException();
        }

        public bool IsContextInitialized(int ctxIdx)
        {
            throw new NotImplementedException();
        }

        public bool ReadBin()
        {
            return binSequence.ElementAt(BinIndex++);
        }

        public Task<bool> ReadBinAsync()
        {
            throw new NotImplementedException();
        }
    }
}
