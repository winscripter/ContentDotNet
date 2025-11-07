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
        ///   Decodes and returns the value of the syntax element <c>mb_type</c>.
        /// </summary>
        /// <returns>Result of the <c>mb_type</c> value.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] Too complex to inline?
#if EXPERIMENTAL_FAST_FEATURES
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
#endif
        public int ReadMacroblockType()
        {
            H264SliceType st = this.CodecContext.SliceType;
            int a = 0, b = 0;
            int currMbType = 0;
            if (st == H264SliceType.I)
            {
                H264Derivative.DeriveNeighboringMacroblocks(CodecContext, out var mbA, out var mbB);
                H264Macroblock? mbTop = CodecContext.MacroblockUtility!.GetMacroblockOrNull(mbB.Address);
                H264Macroblock? mbLeft = CodecContext.MacroblockUtility!.GetMacroblockOrNull(mbA.Address);
                if (mbTop is not null)
                {
                    int mbppm = H264MacroblockTraits.MbPartPredMode(mbTop, 0);
                    b = mbppm != Intra_4x4 && mbppm != Intra_8x8 ? 1 : 0;
                }
                if (mbLeft is not null)
                {
                    int mbppm = H264MacroblockTraits.MbPartPredMode(mbLeft, 0);
                    a = mbppm != Intra_4x4 && mbppm != Intra_8x8 ? 1 : 0;
                }
                int actCtx = a + b;
                int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                if (actSym == 0) currMbType = 0;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32(this);
                    if (modeSym == 1) currMbType = 25; // I_PCM
                    else
                    {
                        actSym = 1;
                        actCtx = 4;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 12;
                        actCtx = 5;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        if (modeSym != 0)
                        {
                            actCtx = 6;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                            actSym += modeSym != 0 ? 8 : 4;
                        }
                        actCtx = 7;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 2;
                        actCtx = 8;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym;
                        currMbType = actSym;
                    }
                }
            }
            else if (st == H264SliceType.SI)
            {
                H264Derivative.DeriveNeighboringMacroblocks(CodecContext, out var mbA, out var mbB);
                H264Macroblock? mbTop = CodecContext.MacroblockUtility!.GetMacroblockOrNull(mbB.Address);
                H264Macroblock? mbLeft = CodecContext.MacroblockUtility!.GetMacroblockOrNull(mbA.Address);
                if (mbTop is not null)
                {
                    int mbppm = H264MacroblockTraits.MbPartPredMode(mbTop, 0);
                    b = mbppm != Intra_4x4 ? 1 : 0;
                }
                if (mbLeft is not null)
                {
                    int mbppm = H264MacroblockTraits.MbPartPredMode(mbLeft, 0);
                    a = mbppm != Intra_4x4 ? 1 : 0;
                }
                int actCtx = a + b;
                int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                if (actSym == 0) currMbType = 1;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32(this);
                    if (modeSym == 1) currMbType = 26; // I_PCM
                    else
                    {
                        actSym = 2;
                        actCtx = 4;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 12;
                        actCtx = 5;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        if (modeSym != 0)
                        {
                            actCtx = 6;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                            actSym += modeSym != 0 ? 8 : 4;
                        }
                        actCtx = 7;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 2;
                        actCtx = 8;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym;
                        currMbType = actSym;
                    }
                }
            }
            else if (st is H264SliceType.P or H264SliceType.SP)
            {
                int actSym = 0;
                if (this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetMacroblockTypeContextRef(1, 4)))
                {
                    actSym = 6 + this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 7));
                }
                else
                {
                    if (this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetMacroblockTypeContextRef(1, 5)))
                    {
                        actSym = 2 + (1 - this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 7)));
                    }
                    else
                    {
                        if (this.ArithmeticReader.DecodeSymbol(this, this.Contexts.GetMacroblockTypeContextRef(1, 5))) actSym = 4;
                        else actSym = 1;
                    }
                }

                if (actSym <= 6) currMbType = actSym;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32(this);
                    if (modeSym == 1) currMbType = 31;
                    else
                    {
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 8));
                        actSym += modeSym * 12;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 9));
                        if (modeSym != 0)
                        {
                            actSym += 4;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 9));
                            if (modeSym != 0) actSym += 4;
                        }
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 10));
                        actSym += modeSym * 2;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(this, this.Contexts.GetMacroblockTypeContextRef(1, 10));
                        actSym += modeSym;
                        currMbType = actSym;
                    }
                }
            }
            else // B slice
            {

            }
        }
    }
}
