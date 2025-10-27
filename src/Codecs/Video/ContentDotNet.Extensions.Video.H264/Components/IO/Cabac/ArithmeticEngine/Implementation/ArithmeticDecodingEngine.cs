namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Implementation
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;
    using System.Threading.Tasks;

    internal class ArithmeticDecodingEngine : IH264ArithmeticReader
    {
        public BitStreamReader Reader { get; set; }

        public IBinTracker BinTracker { get; set; }

        public ArithmeticDecodingEngine(BitStreamReader reader, IBinTracker binTracker, int range, int offset)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, 511, nameof(offset));
            ArgumentOutOfRangeException.ThrowIfNegative(offset, nameof(offset));
            ArgumentOutOfRangeException.ThrowIfLessThan(range, 1, nameof(range));

            if (offset is 510 or 511)
                throw ArithmeticThrowHelper.OffsetIs510Or511();

            Reader = reader;
            BinTracker = binTracker;
            Range = range;
            Offset = offset;
        }

        public int Range { get; set; }
        public int Offset { get; set; }

        private bool LogBin(bool bin)
        {
            if (BinTracker.Track)
                BinTracker.Feed(bin);
            return bin;
        }

        public bool ReadBin(ArithmeticBinType binType, H264ContextVariable? contextVariable)
        {
            if (binType == ArithmeticBinType.Bypass)
            {
                return LogBin(InternalDecodeBypass());
            }
            else if (binType == ArithmeticBinType.Termination)
            {
                return LogBin(InternalDecodeTermination());
            }
            else
            {
                if (contextVariable == null)
                    throw ArithmeticThrowHelper.NoContextVariable();

                return LogBin(InternalDecodeDecision(contextVariable));
            }
        }

        public bool ReadBin(int ctxIdx, bool bypassFlag, H264ContextVariable? contextVariable)
        {
            ArithmeticBinType abt = (ctxIdx, bypassFlag) switch
            {
                (276, _) => ArithmeticBinType.Termination,
                (_, true) => ArithmeticBinType.Bypass,
                _ => ArithmeticBinType.Decision
            };
            return ReadBin(abt, contextVariable);
        }

        public async Task<bool> ReadBinAsync(ArithmeticBinType binType, H264ContextVariable? contextVariable)
        {
            if (binType == ArithmeticBinType.Bypass)
            {
                return LogBin(await InternalDecodeBypassAsync());
            }
            else if (binType == ArithmeticBinType.Termination)
            {
                return LogBin(await InternalDecodeTerminationAsync());
            }
            else
            {
                if (contextVariable == null)
                    throw ArithmeticThrowHelper.NoContextVariable();

                return LogBin(await InternalDecodeDecisionAsync(contextVariable));
            }
        }

        public async Task<bool> ReadBinAsync(int ctxIdx, bool bypassFlag, H264ContextVariable? contextVariable)
        {
            ArithmeticBinType abt = (ctxIdx, bypassFlag) switch
            {
                (276, _) => ArithmeticBinType.Termination,
                (_, true) => ArithmeticBinType.Bypass,
                _ => ArithmeticBinType.Decision
            };
            return await ReadBinAsync(abt, contextVariable);
        }

        #region Implementation for Decoding Decisions

        private bool InternalDecodeDecision(H264ContextVariable cv)
        {
            DecodeDecisionPreprocessingStage(cv, out int codIRangeLPS);

            bool binVal;
            if (Offset >= Range)
            {
                binVal = !cv.ValMps.AsBoolean();
                Offset -= Range;
                Range = codIRangeLPS;

                if (cv.PStateIdx == 0) cv.ValMps = 1 - cv.ValMps;

                cv.PStateIdx = StateTransitioningTable.LpsTransitionTable[cv.PStateIdx];
            }
            else
            {
                binVal = cv.ValMps.AsBoolean();
                cv.PStateIdx = StateTransitioningTable.MpsTransitionTable[cv.PStateIdx];
            }

            Renormalize();
            return binVal;
        }

        private async Task<bool> InternalDecodeDecisionAsync(H264ContextVariable cv)
        {
            DecodeDecisionPreprocessingStage(cv, out int codIRangeLPS);

            bool binVal;
            if (Offset >= Range)
            {
                binVal = !cv.ValMps.AsBoolean();
                Offset -= Range;
                Range = codIRangeLPS;

                if (cv.PStateIdx == 0) cv.ValMps = 1 - cv.ValMps;

                cv.PStateIdx = StateTransitioningTable.LpsTransitionTable[cv.PStateIdx];
            }
            else
            {
                binVal = cv.ValMps.AsBoolean();
                cv.PStateIdx = StateTransitioningTable.MpsTransitionTable[cv.PStateIdx];
            }

            await RenormalizeAsync();
            return binVal;
        }

        private void DecodeDecisionPreprocessingStage(H264ContextVariable cv, out int codIRangeLPS)
        {
            int qCodIRangeIdx = (Range >> 6) & 3;
            codIRangeLPS = H264CabacInitializer.GetRangeTabLps(cv.PStateIdx, qCodIRangeIdx);
            Range -= codIRangeLPS;
        }

        #endregion

        #region Renormalization

        private void Renormalize()
        {
            while (Range < 256)
            {
                Range <<= 1;
                Offset <<= 1;
                Offset |= Reader.ReadBit().AsInt32();
            }
        }

        private async Task RenormalizeAsync()
        {
            while (Range < 256)
            {
                Range <<= 1;
                Offset <<= 1;
                Offset |= (await Reader.ReadBitAsync()).AsInt32();
            }
        }

        #endregion

        #region Implementation for Bypass Decoding

        private bool InternalDecodeBypass()
        {
            Offset <<= 1;
            Offset |= Reader.ReadBit().AsInt32();

            if (Offset >= Range)
            {
                Offset -= Range;
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> InternalDecodeBypassAsync()
        {
            Offset <<= 1;
            Offset |= (await Reader.ReadBitAsync()).AsInt32();

            if (Offset >= Range)
            {
                Offset -= Range;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Implementation for Termination Decoding

        private bool InternalDecodeTermination()
        {
            Range -= 2;
            if (Offset >= Range)
            {
                return true;
            }
            else
            {
                Renormalize();
                return false;
            }
        }

        private async Task<bool> InternalDecodeTerminationAsync()
        {
            Range -= 2;
            if (Offset >= Range)
            {
                return true;
            }
            else
            {
                await RenormalizeAsync();
                return false;
            }
        }

        #endregion
    }
}
