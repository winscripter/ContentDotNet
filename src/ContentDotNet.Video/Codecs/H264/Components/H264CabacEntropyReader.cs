namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Utilities;
    using ContentDotNet.Video.Codecs.H264.Extensions;
    using System.Runtime.CompilerServices;
    using static H264PredictionModes;
    using static H264MacroblockTypes;

    public partial class H264CabacEntropyReader
    {
        public H264CodecContext CodecContext { get; set; }
        public H264CabacArithmeticReader ArithmeticReader { get; set; }
        public H264CabacContexts Contexts { get; set; } = new();

        public H264CabacEntropyReader(H264CodecContext codecContext, H264CabacArithmeticReader arithmeticReader)
        {
            CodecContext = codecContext;
            ArithmeticReader = arithmeticReader;
        }

        /// <summary>
        ///   Decodes and returns the value of the syntax element <c>mvd_l0</c> or <c>mvd_l1</c>.
        /// </summary>
        /// <param name="mvd">The value of <c>mvd_lX</c> where X=<paramref name="mvdComponent"/>.</param>
        /// <param name="mvdComponent">If 1, you're decoding <c>mvd_l1</c>. If 0, you're decoding <c>mvd_l0</c>.</param>
        /// <returns></returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] Too complex to inline?
        public int Mvd(H264Mvd mvd, int mvdComponent)
        {
            if (this.CodecContext.CurrentMacroblock!.MbaffEnabled)
                return MvdMbaff(mvd, mvdComponent);

            H264Derivative.DeriveNeighboringMacroblocks2(CodecContext, out var mbA, out var mbB);
            int ctx = 0;
            if (mbA.Address.Availability) ctx = FastMath.DangerousAbs(mvd.RawMvdData[mbA.Positions.Y][mbA.Positions.X][mvdComponent]);
            if (mbB.Address.Availability) ctx += FastMath.DangerousAbs(mvd.RawMvdData[mbB.Positions.Y][mbB.Positions.X][mvdComponent]);

            if (ctx < 3) ctx = 5 * mvdComponent;
            else ctx = 5 * mvdComponent + 2 + (ctx > 32).AsInt32();

            int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMotionVectorContextRef(0, ctx));
            if (actSym != 0)
            {
                ctx = 5 * mvdComponent;
                actSym = DecodeUnaryExpGolombMotionVector(this, ref this.Contexts.GetMotionVectorContextRef(1, ctx), 3) + 1;

                if (this.ArithmeticReader.DecodeSymbolWithEqualProbability())
                    actSym = -actSym;
            }

            return actSym;
        }

        /// <summary>
        ///   Decodes and returns the value of the syntax element <c>mvd_l0</c> or <c>mvd_l1</c>.
        /// </summary>
        /// <param name="mvd">The value of <c>mvd_lX</c> where X=<paramref name="mvdComponent"/>.</param>
        /// <param name="mvdComponent">If 1, you're decoding <c>mvd_l1</c>. If 0, you're decoding <c>mvd_l0</c>.</param>
        /// <remarks>
        ///   The difference between this method and <see cref="Mvd(H264Mvd, int)"/> is that this method is specific to MBAFF frames only.
        /// </remarks>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] Too complex to inline?
        public int MvdMbaff(H264Mvd mvd, int mvdComponent)
        {
            H264Macroblock currMbRef = this.CodecContext!.CurrentMacroblock!;

            H264Derivative.DeriveNeighboringMacroblocks2(CodecContext, out var mbA, out var mbB);
            int a = 0, b = 0;
            if (mbA.Address.Availability)
            {
                a = FastMath.DangerousAbs(mvd.RawMvdData[mbA.Positions.Y][mbA.Positions.X][mvdComponent]);
                if (this.CodecContext.DeriveMbaffFrameFlag() && mvdComponent == 1)
                {
                    H264Macroblock mb = this.CodecContext.MacroblockUtility!.GetMacroblock(mbA.Address.Address);
                    if (!currMbRef.IsField && mb.IsField) a *= 2;
                    else if (currMbRef.IsField && !mb.IsField) a /= 2;
                }
            }
            if (mbB.Address.Availability)
            {
                b = FastMath.DangerousAbs(mvd.RawMvdData[mbB.Positions.Y][mbB.Positions.X][mvdComponent]);
                if (this.CodecContext.DeriveMbaffFrameFlag() && mvdComponent == 1)
                {
                    H264Macroblock mb = this.CodecContext.MacroblockUtility!.GetMacroblock(mbB.Address.Address);
                    if (!currMbRef.IsField && mb.IsField) b *= 2;
                    else if (currMbRef.IsField && !mb.IsField) b /= 2;
                }
            }
            a += b;

            int actCtx = a < 3 ? (5 * mvdComponent) : (5 * mvdComponent + (2 + (a > 32).AsInt32()));

            int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMotionVectorContextRef(0, actCtx));

            if (actSym != 0)
            {
                actCtx = 5 * mvdComponent;
                actSym = DecodeUnaryExpGolombMotionVector(this, this.Contexts.GetMotionVectorContextRef(1, actCtx), 3) + 1;

                if (this.ArithmeticReader.DecodeSymbolWithEqualProbability())
                    actSym = -actSym;
            }

            return actSym;
        }

        /// <summary>
        ///   Decodes and returns the value of the syntax element <c>mb_skip_flag</c>.
        /// </summary>
        /// <returns>Result of the <c>mb_skip_flag</c> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SkipFlag()
        {
            H264SliceType st = this.CodecContext.SliceType;
            int ctxBase = H264CabacInternalTables.SkipFlagCtxIdxBase[(int)st];
            int ctxAcc = H264CabacInternalTables.SkipFlagCtxIdxAccessor[(int)st];

            H264Derivative.DeriveNeighboringMacroblocks(CodecContext, out var mbA, out var mbB);
            ref H264CabacContextVariable contextVariable = ref this.Contexts.GetMacroblockTypeContextRef(ctxAcc,
                ctxBase + (mbA.Availability ? (this.CodecContext.MacroblockUtility!.GetMacroblock(mbA.Address).Skipped ? 0 : 1) : 0)
                + (mbB.Availability ? (this.CodecContext.MacroblockUtility!.GetMacroblock(mbB.Address).Skipped ? 0 : 1) : 0));

            return this.ArithmeticReader.DecodeSymbol(ref contextVariable);
        }

        /// <summary>
        ///   Decodes and returns the value of the syntax element <c>rem_intra_nxn_pred_mode</c> or <c>prev_intra_nxn_pred_mode_flag</c> where <c>nxn</c> is <c>4x4</c> or <c>8x8</c>.
        /// </summary>
        /// <returns>Result of the <c>rem_intra_nxn_pred_mode</c> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IntraNxNPredMode()
        {
            int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetIntraPredModeContextRef(0));
            if (actSym == 1) return -1;
            else
            {
                int returnValue = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetIntraPredModeContextRef(1));
                returnValue |= this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetIntraPredModeContextRef(1)) << 1;
                returnValue |= this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetIntraPredModeContextRef(1)) << 2;
                return returnValue;
            }
        }

        /// <summary>
        ///   Decodes and returns the value of the syntax element <c>ref_idx_lX</c> where X is 0 or 1.
        /// </summary>
        /// <param name="list">When 0, you're decoding <c>ref_idx_l0</c>. When 1, you're decoding <c>ref_idx_l1</c>.</param>
        /// <returns>Result of the <c>ref_idx_lX</c> value.</returns>
        public int RefIdx(int list)
        {
            H264Derivative.DeriveNeighboringMacroblocks2(CodecContext, out var mbA, out var mbB);
            int a = 0, b = 0;
            if (mbB.Address.Availability)
            {
                int b8b = ((mbB.Positions.X >> 1) & 0x01) + (mbB.Positions.Y & 0x02);
                var neighborMB = CodecContext.MacroblockUtility!.GetMacroblock(mbB.Address.Address);
                H264PB8Mode pb8Mode = default;
                if (CodecContext.CurrentMacroblock != null && CodecContext.CurrentMacroblock.MacroblockLayer.SubMbPred != null)
                    pb8Mode = H264PB8x8Modes.GetPB8Mode((int)CodecContext.CurrentMacroblock.MacroblockLayer.SubMbPred.SubMbType[b8b], CodecContext.SliceType);
                if (!((neighborMB == I_PCM) || H264MacroblockTraits.MbPartPredMode(neighborMB, 0) == Direct || (pb8Mode.B8mode == 0 && pb8Mode.B8pdir == 2)))
                {
                    if (CodecContext.CurrentMacroblock!.MbaffEnabled && !CodecContext.CurrentMacroblock.IsField && neighborMB.IsField) b = GetRefIdx(mbB.Positions.PosY, mbB.Positions.PosX) > 1 ? 2 : 0;
                    else b = GetRefIdx(mbB.Positions.PosY, mbB.Positions.PosX) > 0 ? 2 : 0;
                }
            }
            if (mbA.Address.Availability)
            {
                int b8b = ((mbA.Positions.X >> 1) & 0x01) + (mbA.Positions.Y & 0x02);
                var neighborMB = CodecContext.MacroblockUtility!.GetMacroblock(mbA.Address.Address);
                H264PB8Mode pb8Mode = default;
                if (CodecContext.CurrentMacroblock != null && CodecContext.CurrentMacroblock.MacroblockLayer.SubMbPred != null)
                    pb8Mode = H264PB8x8Modes.GetPB8Mode((int)CodecContext.CurrentMacroblock.MacroblockLayer.SubMbPred.SubMbType[b8b], CodecContext.SliceType);
                if (!((neighborMB == I_PCM) || H264MacroblockTraits.MbPartPredMode(neighborMB, 0) == Direct || (pb8Mode.B8mode == 0 && pb8Mode.B8pdir == 2)))
                {
                    if (CodecContext.CurrentMacroblock!.MbaffEnabled && !CodecContext.CurrentMacroblock.IsField && neighborMB.IsField) a = GetRefIdx(mbB.Positions.PosY, mbB.Positions.PosX) > 1 ? 1 : 0;
                    else a = GetRefIdx(mbB.Positions.PosY, mbB.Positions.PosX) > 0 ? 1 : 0;
                }
            }
            int actCtx = a + b;
            int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetReferenceIndexContextRef(0, actCtx));
            if (actSym != 0) actSym = DecodeUnary(this, this.Contexts.GetReferenceIndexContextRef(0, 4), 1) + 1;
            return actSym;

            int GetRefIdx(int x, int y) => CodecContext.CurrentMacroblock!.SubMacroblocks![x][y].ReferenceIndices[list];
        }
    }
}
