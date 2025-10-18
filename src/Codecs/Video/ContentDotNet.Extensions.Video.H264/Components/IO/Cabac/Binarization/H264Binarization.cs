namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using ContentDotNet.Primitives;

    internal static class H264Binarization
    {
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

        public static int FL(IH264CabacDecoder decoder, int cMax)
        {
            int fixedLength = (int)Math.Ceiling(Math.Log2(cMax + 1));
            int value = 0;
            for (int i = 0; i < fixedLength; i++)
                value <<= 1 | decoder.ReadBin().AsInt32();
            return value;
        }

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
                decoder.Affix = H264Affix.Suffix;

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
                            return 0; // B_Direct_16x16
                        }
                        else
                        {
                            // b0 is 1 here

                            int b1 = decoder.ReadBin().AsInt32();
                            int b2 = decoder.ReadBin().AsInt32();

                            if (b1 == 0 && b2 == 0) return 1;
                            else if (b1 == 0 && b2 == 1) return 2;
                            else
                            {
                                // b1 is 1 here

                                int b3 = decoder.ReadBin().AsInt32();
                                int b4 = decoder.ReadBin().AsInt32();
                                int b5 = decoder.ReadBin().AsInt32();

                                if (b2 == 0)
                                {
                                    return ((b3 << 2) | (b4 << 1) | b5) + 3;
                                }
                                else
                                {
                                    if (b3 == 1 && b4 == 1) return 11;

                                    int b6 = decoder.ReadBin().AsInt32();

                                    if (b3 == 0)
                                    {
                                        return 12 + ((b6 << 2) | (b5 << 1) | b4);
                                    }
                                    else
                                    {
                                        if (b4 == 0)
                                            return 20 + b6;
                                        else
                                            return 22;
                                    }
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
                            if (b1 == 0)
                            {
                                int b2 = decoder.ReadBin().AsInt32();
                                if (b2 == 0) return 1; // B_L0_8x8
                                else return 2;         // B_L1_8x8
                            }
                            else
                            {
                                int b2 = decoder.ReadBin().AsInt32();
                                int b3 = decoder.ReadBin().AsInt32();
                                int b4 = decoder.ReadBin().AsInt32();

                                if (b2 == 0 && b3 == 0 && b4 == 0) return 3; // B_Bi_8x8
                                if (b2 == 0 && b3 == 0 && b4 == 1) return 4; // B_L0_8x4
                                if (b2 == 0 && b3 == 1 && b4 == 0) return 5; // B_L0_4x8
                                if (b2 == 0 && b3 == 1 && b4 == 1) return 6; // B_L1_8x4

                                int b5 = decoder.ReadBin().AsInt32();
                                int b6 = decoder.ReadBin().AsInt32();

                                if (b2 == 1 && b3 == 0 && b4 == 0 && b5 == 0 && b6 == 0) return 7;  // B_L1_4x8
                                if (b2 == 1 && b3 == 0 && b4 == 0 && b5 == 0 && b6 == 1) return 8;  // B_Bi_8x4
                                if (b2 == 1 && b3 == 0 && b4 == 0 && b5 == 1 && b6 == 0) return 9;  // B_Bi_4x8
                                if (b2 == 1 && b3 == 0 && b4 == 0 && b5 == 1 && b6 == 1) return 10; // B_L0_4x4
                                if (b2 == 1 && b3 == 1 && b4 == 0) return 11; // B_L1_4x4
                                if (b2 == 1 && b3 == 1 && b4 == 1) return 12; // B_Bi_4x4
                            }
                        }
                    }
                }

                throw new InvalidOperationException();
            }
        }

        public static int MbType(IH264CabacDecoder decoder, H264SliceType slice, bool subMbType)
        {
            int retval = MbTypeInternal(decoder, slice, subMbType);
            decoder.Affix = H264Affix.Prefix;
            return retval;
        }

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

        public static int MbQpDelta(IH264CabacDecoder decoder)
        {
            int codeNum = U(decoder);
            int val = (codeNum + 1) >> 1;
            return (codeNum & 1) == 0 ? -val : val;
        }
    }
}
