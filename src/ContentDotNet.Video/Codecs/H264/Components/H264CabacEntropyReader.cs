namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Utilities;
    using ContentDotNet.Video.Codecs.H264.Extensions;
    using System.Runtime.CompilerServices;
    using static H264PredictionModes;

    public class H264CabacEntropyReader
    {
        public H264CodecContext CodecContext { get; set; }
        public H264CabacBiariReader ArithmeticReader { get; set; }
        public H264CabacContexts Contexts { get; set; } = new();

        public H264CabacEntropyReader(H264CodecContext codecContext, H264CabacBiariReader arithmeticReader)
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

            int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, ref this.Contexts.GetMotionVectorContextRef(0, ctx));
            if (actSym != 0)
            {
                ctx = 5 * mvdComponent;
                actSym = DecodeUnaryExpGolombMotionVector(this, ref this.Contexts.GetMotionVectorContextRef(1, ctx), 3) + 1;

                if (this.ArithmeticReader.DecodeSymbolWithEqualProbability(this))
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

            int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMotionVectorContextRef(0, actCtx));

            if (actSym != 0)
            {
                actCtx = 5 * mvdComponent;
                actSym = DecodeUnaryExpGolombMotionVector(this, this.Contexts.GetMotionVectorContextRef(1, actCtx), 3) + 1;

                if (this.Contexts.DecodeSymbolWithEqualProbability(this))
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

            return this.ArithmeticReader.DecodeSymbol(this, ref contextVariable);
        }

        /// <summary>
        ///   Decodes and returns the value of the syntax element <c>rem_intra_nxn_pred_mode</c> where <c>nxn</c> is <c>4x4</c> or <c>8x8</c>.
        /// </summary>
        /// <returns>Result of the <c>rem_intra_nxn_pred_mode</c> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReadRemIntraNxNPredMode()
        {
            int actSym = this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetIntraPredModeContext(0));
            if (actSym == 1) return -1;
            else
            {
                int returnValue = this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetIntraPredModeContext(1));
                returnValue |= this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetIntraPredModeContext(1)) << 1;
                returnValue |= this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetIntraPredModeContext(1)) << 2;
                return returnValue;
            }
        }
    }
}
