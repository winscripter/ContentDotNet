namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.IOImplementation
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.CtxIdxDerivative;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Presets;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using ContentDotNet.Primitives;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    internal class H264CabacReader : StrictlyCabacReader
    {
        private readonly IH264CabacDecoder decoder;
        private readonly int sliceQPY;
        private readonly H264SliceType sliceType;
        private readonly int cabacInitIdc;
        private readonly H264State state;

        public H264CabacReader(IH264CabacDecoder decoder)
        {
            this.decoder = decoder;
            this.sliceQPY = decoder.State.H264RbspState?.SliceQpy() ?? 0;
            this.sliceType = H264SliceTypes.FetchSliceType(decoder.State.H264RbspState?.SliceHeader?.SliceType ?? 0);
            this.cabacInitIdc = (int?)decoder.State.H264RbspState?.SliceHeader?.CabacInitIdc ?? 0;
            state = decoder.State;

            this.Miscellaneous["CabacDecodingVariables"] = decoder.DecodingVariables;
        }

        public IH264CabacDecoder CabacDecoder => decoder; // For unit tests

        public bool IsFrameMacroblock { get; set; }
        public override H264MacroblockInfo? MacroblockInfo { get; set; } = null;

        [DoesNotReturn]
        private static void ThrowNullParserFailure(H264SyntaxElement se)
        {
            throw new InvalidOperationException($"Could not parse CABAC. Syntax element: {se}");
        }

        private void InitializeContextIndex(int ctxIdx)
        {
            if (!decoder.IsContextInitialized(ctxIdx))
                decoder.InitializeContext(ctxIdx, sliceQPY, cabacInitIdc, sliceType, false);
        }

        private int GetContextIndex(H264SyntaxElement se, ContextIndexAndParser cip, StandaloneCtxIdxIncDerivativeMode mode = 0)
        {
            int ctxIdx = AssignCtxIdx.Assign(decoder, state, MacroblockInfo, se, cip.GetOffset(decoder), decoder.BinIndex, mode);
            decoder.PrefixContextIndex = AssignCtxIdx.Assign(decoder, state, MacroblockInfo, se, cip.GetPrefixOffset(), decoder.BinIndex, mode);

            if (cip.Record.CtxIdxOffset.HasSuffix)
                decoder.SuffixContextIndex = AssignCtxIdx.Assign(decoder, state, MacroblockInfo, se, cip.GetSuffixOffset(), decoder.BinIndex, mode);

            return ctxIdx;
        }

        private int ParseSimple(H264SyntaxElement se)
        {
            this.decoder.Affix = H264Affix.Prefix;

            decoder.BinIndex = 0;
            decoder.ArithmeticReader.BinTracker.Reset();

            var parser = H264BaseCtxIdxAssignments.GetParserWithCtxIdx(se, decoder.DecodingVariables.CtxBlockCat, IsFrameMacroblock, sliceType);
            this.decoder.ContextIndexRecord = parser?.Record;

            if (parser == null)
            {
                ThrowNullParserFailure(se);
            }

            int ctxIdx = GetContextIndex(se, parser);
            InitializeContextIndex(ctxIdx);

            decoder.Recompute = () =>
            {
                ctxIdx = GetContextIndex(se, parser);
                InitializeContextIndex(ctxIdx);
            };

            int binarized = parser.Parse(decoder, sliceType);

            return binarized;
        }

        private int ParseForCoeffFlagAndAbsLevel(H264SyntaxElement se, StandaloneCtxIdxIncDerivativeMode mode)
        {
            this.decoder.Affix = H264Affix.Prefix;

            decoder.BinIndex = 0;
            decoder.ArithmeticReader.BinTracker.Reset();

            var parser = H264BaseCtxIdxAssignments.GetParserWithCtxIdx(se, decoder.DecodingVariables.CtxBlockCat, IsFrameMacroblock, sliceType);
            this.decoder.ContextIndexRecord = parser?.Record;

            if (parser == null)
            {
                ThrowNullParserFailure(se);
            }

            int ctxIdx = GetContextIndex(se, parser, mode);
            InitializeContextIndex(ctxIdx);

            decoder.Recompute = () =>
            {
                ctxIdx = GetContextIndex(se, parser, mode);
                InitializeContextIndex(ctxIdx);
            };

            int binarized = parser.Parse(decoder, sliceType);

            return binarized;
        }

        private bool ParseBooleanForCoeffFlagAndAbsLevel(H264SyntaxElement se, StandaloneCtxIdxIncDerivativeMode mode) =>
            ParseForCoeffFlagAndAbsLevel(se, mode).AsBoolean();

        private bool ParseBooleanSimple(H264SyntaxElement se) => Int32Boolean.B(ParseSimple(se));

        public override bool ReadCodedBlockFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.CodedBlockFlag);
        }

        public override Task<bool> ReadCodedBlockFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadCodedBlockPattern()
        {
            return (uint)ParseSimple(H264SyntaxElement.CodedBlockPattern);
        }

        public override Task<uint> ReadCodedBlockPatternAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadCoeffAbsLevelMinus1()
        {
            return (uint)ParseForCoeffFlagAndAbsLevel(H264SyntaxElement.CoeffAbsLevelMinus1, StandaloneCtxIdxIncDerivativeMode.CoefficientAbsoluteLevelMinus1);
        }

        public override Task<uint> ReadCoeffAbsLevelMinus1Async()
        {
            throw new NotImplementedException();
        }

        public override bool ReadCoeffSignFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.CoeffSignFlag);
        }

        public override Task<bool> ReadCoeffSignFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadMbSkipFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.MacroblockSkipFlag);
        }

        public override Task<bool> ReadMbSkipFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadEndOfSliceFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.EndOfSliceFlag);
        }

        public override Task<bool> ReadEndOfSliceFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadIntraChromaPredMode()
        {
            return (uint)ParseSimple(H264SyntaxElement.IntraChromaPredictionMode);
        }

        public override Task<uint> ReadIntraChromaPredModeAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadLastSignificantCoeffFlag()
        {
            return ParseBooleanForCoeffFlagAndAbsLevel(H264SyntaxElement.LastSignificantCoeffFlag, StandaloneCtxIdxIncDerivativeMode.LastSignificantCoefficientFlag);
        }

        public override Task<bool> ReadLastSignificantCoeffFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadMbFieldDecodingFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.MacroblockFieldDecodingFlag);
        }

        public override Task<bool> ReadMbFieldDecodingFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override int ReadMbQpDelta()
        {
            return ParseSimple(H264SyntaxElement.CodedBlockPattern);
        }

        public override Task<int> ReadMbQpDeltaAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadMbType()
        {
            return (uint)ParseSimple(H264SyntaxElement.MacroblockType);
        }

        public override Task<uint> ReadMbTypeAsync()
        {
            throw new NotImplementedException();
        }

        public override int ReadMvdL0()
        {
            return ParseSimple(H264SyntaxElement.MotionVectorDifferenceX);
        }

        public override Task<int> ReadMvdL0Async()
        {
            throw new NotImplementedException();
        }

        public override int ReadMvdL1()
        {
            return ParseSimple(H264SyntaxElement.MotionVectorDifferenceY);
        }

        public override Task<int> ReadMvdL1Async()
        {
            throw new NotImplementedException();
        }

        public override bool ReadPrevIntra4x4PredModeFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.PreviousIntraNxNPredictionModeFlag);
        }

        public override Task<bool> ReadPrevIntra4x4PredModeFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadPrevIntra8x8PredModeFlag()
        {
            return ParseBooleanSimple(H264SyntaxElement.PreviousIntraNxNPredictionModeFlag);
        }

        public override Task<bool> ReadPrevIntra8x8PredModeFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadRefIdxL0()
        {
            return (uint)ParseSimple(H264SyntaxElement.ReferenceIndex);
        }

        public override Task<uint> ReadRefIdxL0Async()
        {
            throw new NotImplementedException();
        }

        public override uint ReadRefIdxL1()
        {
            return (uint)ParseSimple(H264SyntaxElement.ReferenceIndex);
        }

        public override Task<uint> ReadRefIdxL1Async()
        {
            throw new NotImplementedException();
        }

        public override uint ReadRemIntra4x4PredMode()
        {
            return (uint)ParseSimple(H264SyntaxElement.RemainingIntraNxNPredictionMode);
        }

        public override Task<uint> ReadRemIntra4x4PredModeAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadRemIntra8x8PredMode()
        {
            return (uint)ParseSimple(H264SyntaxElement.RemainingIntraNxNPredictionMode);
        }

        public override Task<uint> ReadRemIntra8x8PredModeAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadSignificantCoeffFlag()
        {
            return ParseBooleanForCoeffFlagAndAbsLevel(H264SyntaxElement.SignificantCoeffFlag, StandaloneCtxIdxIncDerivativeMode.SignificantCoefficientFlag);
        }

        public override Task<bool> ReadSignificantCoeffFlagAsync()
        {
            throw new NotImplementedException();
        }

        public override uint ReadSubMbType()
        {
            return (uint)ParseSimple(H264SyntaxElement.SubMacroblockType);
        }

        public override Task<uint> ReadSubMbTypeAsync()
        {
            throw new NotImplementedException();
        }

        public override bool ReadTransformSize8X8Flag()
        {
            return ParseBooleanSimple(H264SyntaxElement.TransformSize8x8Flag);
        }

        public override Task<bool> ReadTransformSize8X8FlagAsync()
        {
            throw new NotImplementedException();
        }
    }
}
