namespace ContentDotNet.Video.Codecs.H264.Components
{
    using static H264PredictionModes;

    public partial class H264CabacEntropyReader
    {
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
            int currMbType;
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
                int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                if (actSym == 0) currMbType = 0;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32();
                    if (modeSym == 1) currMbType = 25; // I_PCM
                    else
                    {
                        actSym = 1;
                        actCtx = 4;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 12;
                        actCtx = 5;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        if (modeSym != 0)
                        {
                            actCtx = 6;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                            actSym += modeSym != 0 ? 8 : 4;
                        }
                        actCtx = 7;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 2;
                        actCtx = 8;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
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
                int actSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                if (actSym == 0) currMbType = 1;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32();
                    if (modeSym == 1) currMbType = 26; // I_PCM
                    else
                    {
                        actSym = 2;
                        actCtx = 4;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 12;
                        actCtx = 5;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        if (modeSym != 0)
                        {
                            actCtx = 6;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                            actSym += modeSym != 0 ? 8 : 4;
                        }
                        actCtx = 7;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym * 2;
                        actCtx = 8;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(0, actCtx));
                        actSym += modeSym;
                        currMbType = actSym;
                    }
                }
            }
            else if (st is H264SliceType.P or H264SliceType.SP)
            {
                int actSym;
                if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(1, 4)))
                {
                    actSym = 6 + this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 7));
                }
                else
                {
                    if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(1, 5)))
                    {
                        actSym = 2 + (1 - this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 7)));
                    }
                    else
                    {
                        if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(1, 5))) actSym = 4;
                        else actSym = 1;
                    }
                }

                if (actSym <= 6) currMbType = actSym;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32();
                    if (modeSym == 1) currMbType = 31;
                    else
                    {
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 8));
                        actSym += modeSym * 12;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 9));
                        if (modeSym != 0)
                        {
                            actSym += 4;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 9));
                            if (modeSym != 0) actSym += 4;
                        }
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 10));
                        actSym += modeSym * 2;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, 10));
                        actSym += modeSym;
                        currMbType = actSym;
                    }
                }
            }
            else // B slice
            {
                H264Derivative.DeriveNeighboringMacroblocks(CodecContext, out var mbA, out var mbB);
                if (mbB.Availability) b = (CodecContext.MacroblockUtility?.GetMacroblock(mbB.Address)?.MacroblockLayer.MbType != 0) ? 1 : 0;
                if (mbA.Availability) a = (CodecContext.MacroblockUtility?.GetMacroblock(mbA.Address)?.MacroblockLayer.MbType != 0) ? 1 : 0;
                int actCtx = a + b;
                int actSym = 0;
                if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, actCtx)))
                {
                    if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 4)))
                    {
                        if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 5)))
                        {
                            actSym = 12;
                            if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym += 8;
                            if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym += 4;
                            if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym += 2;
                            if (actSym == 24) actSym = 11;
                            else if (actSym == 26) actSym = 22;
                            else
                            {
                                if (actSym == 22) actSym = 23;
                                if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym++;
                            }
                        }
                        else
                        {
                            actSym = 3;
                            if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym += 4;
                            if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym += 2;
                            if (this.ArithmeticReader.DecodeSymbol(ref this.Contexts.GetMacroblockTypeContextRef(2, 6))) actSym++;
                        }
                    }
                    else
                    {
                        actSym = 1 + this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(2, 6));
                    }
                }

                if (actSym <= 23) currMbType = actSym;
                else
                {
                    int modeSym = this.ArithmeticReader.DecodeFinalAsInt32();
                    if (modeSym == 1) currMbType = 48;
                    else
                    {
                        actCtx = 8;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, actCtx));
                        actSym = modeSym * 12;
                        actCtx = 9;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, actCtx));
                        if (modeSym != 0)
                        {
                            actSym += 4;
                            modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, actCtx));
                            if (modeSym != 0) actSym += 4;
                        }
                        actCtx = 10;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, actCtx));
                        actSym = modeSym * 2;
                        modeSym = this.ArithmeticReader.DecodeSymbolAsInt32(ref this.Contexts.GetMacroblockTypeContextRef(1, actCtx));
                        actSym += modeSym;
                        currMbType = actSym;
                    }
                }
            }
            return currMbType;
        }
    }
}
