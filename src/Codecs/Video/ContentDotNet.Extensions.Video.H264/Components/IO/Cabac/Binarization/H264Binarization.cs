namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using ContentDotNet.Primitives;

    /// <summary>
    ///   Provides access to H.264 binarization.
    /// </summary>
    public static class H264Binarization
    {
        /// <summary>
        ///   Performs H.264 unary binarization.
        /// </summary>
        /// <param name="decoder">The source H.264 CABAC decoder.</param>
        /// <returns>The result of the unary-binarized integer.</returns>
        public static int U(IH264CabacDecoder decoder)
        {
            // Allows preventing runaway infinite loop
            RecursionCounter rc = new(Limits.LargestUBinarization);

            // Actual value to return
            int uValue = 0;

            while (decoder.ReadBin())
            {
                uValue++;
                rc.Increment();
            }
            return uValue;
        }

        /// <summary>
        ///   Performs H.264 truncated unary binarization.
        /// </summary>
        /// <param name="decoder">The source H.264 CABAC decoder.</param>
        /// <param name="cMax">Maximum number of bins.</param>
        /// <returns>The result of the truncated unary-binarized integer and the number of bins read.</returns>
        public static TuResult TU(IH264CabacDecoder decoder, int cMax)
        {
            // Principles are same as U, except, if uValue == cMax, abort and return.

            // Allows preventing runaway infinite loop
            RecursionCounter rc = new(Limits.LargestUBinarization);

            // Actual value to return.
            int tuValue = 0;
            int bitsRead = 0;

            bool last = false;

            while (tuValue < cMax && (last = decoder.ReadBin()))
            {
                tuValue++;
                bitsRead++;
                rc.Increment();
            }

            if (!last)
                bitsRead++;

            return new(tuValue, bitsRead);
        }

        /// <summary>
        ///   Performs UEGk binarization.
        /// </summary>
        /// <param name="decoder">The input H.264 decoder.</param>
        /// <param name="signedValFlag"></param>
        /// <param name="uCoff"></param>
        /// <param name="k"></param>
        /// <returns>The result of the UEGk binarized integer.</returns>
        public static int Uegk(IH264CabacDecoder decoder, bool signedValFlag, int uCoff, int k)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(uCoff, 0, nameof(uCoff));

            decoder.Affix = H264Affix.Prefix;

            TuResult prefix = TU(decoder, uCoff);

            if ((!signedValFlag && !(prefix.BinsRead == uCoff && IntrinsicFunctions.AllBitsAreOne(prefix.Value, prefix.BinsRead))) ||
                (signedValFlag && prefix.BinsRead == 1 && prefix.Value == 0) ||
                decoder.ForcePrefix)
            {
                return prefix.Value;
            }

            int synElVal = prefix.Value;

            void put(int i)
            {
                synElVal = synElVal << 1 | i;
            }

            decoder.Affix = H264Affix.Suffix;

            if (Math.Abs(synElVal) >= uCoff)
            {
                int sufS = Math.Abs(synElVal) - uCoff;
                bool stopLoop = false;
                do
                {
                    if (sufS >= (1 << k))
                    {
                        put(1);
                        sufS -= (1 << k);
                        k++;
                    }
                    else
                    {
                        put(0);
                        while (Int32Boolean.B(k--))
                            put((sufS >> k) & 1);
                        stopLoop = true;
                    }
                } while (!stopLoop);
            }

            if (signedValFlag && synElVal != 0)
            {
                if (synElVal > 0)
                {
                    put(0);
                }
                else
                {
                    put(1);
                }
            }

            decoder.Affix = H264Affix.Prefix;

            return synElVal;
        }

        /// <summary>
        ///   Performs H.264 Fixed-Length binarization.
        /// </summary>
        /// <param name="decoder">The source H.264 CABAC decoder</param>
        /// <param name="cMax">Maximum fixed length integer value.</param>
        /// <returns>The result of Fixed-length binarization</returns>
        public static int FL(IH264CabacDecoder decoder, int cMax)
        {
            int fixedLength = (int)Math.Ceiling(Math.Log2(cMax + 1));
            int value = 0;
            for (int i = 0; i < fixedLength; i++)
                value = value << 1 | decoder.ReadBin().AsInt32();
            return value;
        }

        // This is the internal mb_type function. The caller, MbType, invokes this and just sets
        // the decoder.Affix value to Prefix prior to returning.
        //
        // It might look like a God method, but it's really just decoding the integer-to-bins table
        // for different slice types. It's described in the H.264 spec, clause 9.3.2.5.
        //
        // We have tested this method for all possible integers and slice types in unit tests,
        // and I'm sure it probably works as it should.
        private static int MbTypeInternal(IH264CabacDecoder decoder, H264SliceType slice, bool subMbType)
        {
            decoder.Affix = H264Affix.Prefix;
            int prefix = slice == H264SliceType.SI ? 0 : 1;
            if (prefix == 0)
            {
                return prefix;
            }
            else
            {
                decoder.Affix = H264Affix.Prefix;

                if (!subMbType)
                {
                    if (slice == H264SliceType.I)
                    {
                        //0(I_NxN)          0
                        //1(I_16x16_0_0_0)  1 0 0 0 0 0
                        //2(I_16x16_1_0_0)  1 0 0 0 0 1
                        //3(I_16x16_2_0_0)  1 0 0 0 1 0
                        //4(I_16x16_3_0_0)  1 0 0 0 1 1
                        //5(I_16x16_0_1_0)  1 0 0 1 0 0 0
                        //6(I_16x16_1_1_0)  1 0 0 1 0 0 1
                        //7(I_16x16_2_1_0)  1 0 0 1 0 1 0
                        //8(I_16x16_3_1_0)  1 0 0 1 0 1 1
                        //9(I_16x16_0_2_0)  1 0 0 1 1 0 0
                        //10(I_16x16_1_2_0) 1 0 0 1 1 0 1
                        //11(I_16x16_2_2_0) 1 0 0 1 1 1 0
                        //12(I_16x16_3_2_0) 1 0 0 1 1 1 1
                        //13(I_16x16_0_0_1) 1 0 1 0 0 0
                        //14(I_16x16_1_0_1) 1 0 1 0 0 1
                        //15(I_16x16_2_0_1) 1 0 1 0 1 0
                        //16(I_16x16_3_0_1) 1 0 1 0 1 1
                        //17(I_16x16_0_1_1) 1 0 1 1 0 0 0
                        //18(I_16x16_1_1_1) 1 0 1 1 0 0 1
                        //19(I_16x16_2_1_1) 1 0 1 1 0 1 0
                        //20(I_16x16_3_1_1) 1 0 1 1 0 1 1
                        //21(I_16x16_0_2_1) 1 0 1 1 1 0 0
                        //22(I_16x16_1_2_1) 1 0 1 1 1 0 1
                        //23(I_16x16_2_2_1) 1 0 1 1 1 1 0
                        //24(I_16x16_3_2_1) 1 0 1 1 1 1 1
                        //25(I_PCM)         1 1

                        bool b0 = decoder.ReadBin();
                        if (!b0)
                        {
                            return 0;
                        }
                        else
                        {
                            bool b1 = decoder.ReadBin();
                            if (b1)
                            {
                                return 25;
                            }
                            else
                            {
                                bool b2 = decoder.ReadBin();
                                if (!b2)
                                {
                                    bool b3 = decoder.ReadBin();
                                    if (!b3)
                                    {
                                        // Starting with 1
                                        bool b4 = decoder.ReadBin();
                                        bool b5 = decoder.ReadBin();

                                        return 1 + ((b4.AsInt32() << 1) | b5.AsInt32());
                                    }
                                    else
                                    {
                                        // Starting with 5
                                        bool b4 = decoder.ReadBin();
                                        bool b5 = decoder.ReadBin();
                                        bool b6 = decoder.ReadBin();

                                        return 5 + ((b4.AsInt32() << 2) | (b5.AsInt32() << 1) | b6.AsInt32());
                                    }
                                }
                                else
                                {
                                    bool b3 = decoder.ReadBin();
                                    if (!b3)
                                    {
                                        // Starting with 13
                                        bool b4 = decoder.ReadBin();
                                        bool b5 = decoder.ReadBin();

                                        return 13 + ((b4.AsInt32() << 1) | b5.AsInt32());
                                    }
                                    else
                                    {
                                        // Starting with 17
                                        bool b4 = decoder.ReadBin();
                                        bool b5 = decoder.ReadBin();
                                        bool b6 = decoder.ReadBin();

                                        return 17 + ((b4.AsInt32() << 2) | (b5.AsInt32() << 1) | b6.AsInt32());
                                    }
                                }
                            }
                        }
                    }
                    else if (slice is H264SliceType.P or H264SliceType.SP)
                    {
                        bool b0 = decoder.ReadBin();
                        if (!b0)
                        {
                            bool b1 = decoder.ReadBin();
                            bool b2 = decoder.ReadBin();
                            if (!b1)
                            {
                                if (!b2) return 0;
                                else return 3;
                            }
                            else
                            {
                                if (!b2) return 2;
                                else return 1;
                            }
                        }
                        else
                        {
                            return 5; // Intra, prefix only
                        }
                    }
                    else if (slice == H264SliceType.B)
                    {
                        int b0 = decoder.ReadBin().AsInt32();
                        if (b0 == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            int b1 = decoder.ReadBin().AsInt32();
                            if (b1 == 0) return decoder.ReadBin().AsInt32() + 1;
                            else
                            {
                                int b2 = decoder.ReadBin().AsInt32();
                                int b3 = decoder.ReadBin().AsInt32();
                                int b4 = decoder.ReadBin().AsInt32();
                                int b5 = decoder.ReadBin().AsInt32();

                                if (b0 == 1 && b1 == 1 && b2 == 1 && b3 == 1 && b4 == 1 && b5 == 0) return 11;

                                if (b2 == 1)
                                {
                                    if (b3 == 1 && b5 == 1)
                                    {
                                        if (b4 == 1) return 22;
                                        else
                                        {
                                            // Special case: 1 1 1 1 0  1 
                                            // In this context, 111101 is the prefix.
                                            // Following it is the same mb_type binarization, except, it's B slice. It's
                                            // the suffix value.
                                            //
                                            // Finally, the result is the suffix

                                            decoder.Affix = H264Affix.Suffix;

                                            return MbTypeInternal(decoder, H264SliceType.I, false);
                                        }
                                    }

                                    int b6 = decoder.ReadBin().AsInt32();

                                    return 12 + ((b3 << 3) | (b4 << 2) | (b5 << 1) | b6);
                                }
                                else
                                {
                                    return 3 + ((b3 << 2) | (b4 << 1) | b5);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (slice is H264SliceType.P or H264SliceType.SP)
                    {
                        int b0 = decoder.ReadBin().AsInt32();
                        if (b0 == 1)
                        {
                            return 0; // P_L0_8x8
                        }
                        else
                        {
                            int b1 = decoder.ReadBin().AsInt32();
                            if (b1 == 0)
                            {
                                return 1; // P_L0_8x4
                            }
                            else
                            {
                                int b2 = decoder.ReadBin().AsInt32();
                                if (b2 == 1)
                                {
                                    return 2; // P_L0_4x8
                                }
                                else
                                {
                                    return 3; // P_L0_4x4
                                }
                            }
                        }
                    }
                    else if (slice == H264SliceType.B)
                    {
                        int b0 = decoder.ReadBin().AsInt32();
                        if (b0 == 0)
                        {
                            return 0; // B_Direct_8x8
                        }
                        else
                        {
                            int b1 = decoder.ReadBin().AsInt32();
                            if (b1 == 0) return decoder.ReadBin().AsInt32() + 1;
                            else
                            {
                                int b2 = decoder.ReadBin().AsInt32();
                                if (b2 == 0)
                                {
                                    int b3 = decoder.ReadBin().AsInt32();
                                    int b4 = decoder.ReadBin().AsInt32();

                                    return 3 + ((b3 << 1) | b4);
                                }
                                else
                                {
                                    int b3 = decoder.ReadBin().AsInt32();

                                    if (b3 == 0)
                                    {
                                        int b4 = decoder.ReadBin().AsInt32();
                                        int b5 = decoder.ReadBin().AsInt32();

                                        return 7 + ((b4 << 1) | b5);
                                    }
                                    else
                                    {
                                        return 11 + decoder.ReadBin().AsInt32();
                                    }
                                }
                            }
                        }
                    }
                }

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        ///   Performs H.264 mb_type binarization.
        /// </summary>
        /// <param name="decoder">The source H.264 decoder</param>
        /// <param name="slice">The type of the slice</param>
        /// <param name="subMbType">Decoding sub_mb_type?</param>
        /// <returns>The result of the mb_type binarization.</returns>
        public static int MbType(IH264CabacDecoder decoder, H264SliceType slice, bool subMbType)
        {
            int retval = MbTypeInternal(decoder, slice, subMbType);
            decoder.Affix = H264Affix.Prefix;
            return retval;
        }

        /// <summary>
        ///   Performs H.264 coded_block_pattern binarization.
        /// </summary>
        /// <param name="decoder">The source H.264 decoder.</param>
        /// <param name="chromaArrayType">The ChromaArrayType variable.</param>
        /// <returns>The result of the coded_block_pattern binarization.</returns>
        public static int CodedBlockPattern(IH264CabacDecoder decoder, int chromaArrayType)
        {
            decoder.Affix = H264Affix.Prefix;
            int prefix = FL(decoder, 15);
            if (chromaArrayType is not (0 or 3))
            {
                decoder.Affix = H264Affix.Suffix;
                int suffix = TU(decoder, 2).Value;
                decoder.Affix = H264Affix.Prefix;

                return CbpUtils.GetCodedBlockPattern(prefix, suffix);
            }
            else
            {
                return prefix;
            }
        }

        /// <summary>
        ///   Performs H.264 mb_qp_delta binarization.
        /// </summary>
        /// <param name="decoder">The source H.264 CABAC decoder</param>
        /// <returns>The result of the mb_qp_delta binarization.</returns>
        public static int MbQpDelta(IH264CabacDecoder decoder)
        {
            int codeNum = U(decoder);
            int val = (codeNum + 1) >> 1;
            return (codeNum & 1) == 0 ? -val : val;
        }
    }
}
