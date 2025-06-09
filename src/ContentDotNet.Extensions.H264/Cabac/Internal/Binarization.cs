using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Cabac.Internal;

internal static class Binarization
{
    public static int UnaryBinarize(BitStreamReader reader)
    {
        int synElVal = 0;
        while (reader.ReadBit())
            synElVal++;
        return synElVal;
    }

    public static int TruncatedUnaryBinarize(BitStreamReader reader, int cMax)
    {
        int count = 0;
        for (int i = 0; i < cMax; i++)
        {
            bool bit = reader.ReadBit();
            if (!bit)
                break;
            count++;
        }

        return count;
    }

    public static (int count, int value) TruncatedUnaryBinarizeWithValue(BitStreamReader reader, int cMax)
    {
        int count = 0;
        int value = 0;
        for (int i = 0; i < cMax; i++)
        {
            bool bit = reader.ReadBit();
            if (bit)
                value = (value << 1) | 0x1;
            else
                value <<= 1;

            if (!bit)
                break;

            count++;
        }

        return (count, value);
    }

    public static int UegkBinarize(BitStreamReader reader, bool signedValFlag, int k, int uCoff)
    {
        var (count, value) = TruncatedUnaryBinarizeWithValue(reader, uCoff);

        if ((!signedValFlag && count != uCoff && Intrinsic.AllBitsSetToOne(value, 0, count)) ||
            (signedValFlag && count == 0 && value == 0))
        {
            return count;
        }
        else
        {
            int building = 0;
            int synElVal = 0;
            if (Math.Abs(synElVal) >= uCoff)
            {
                int sufS = Math.Abs(synElVal) - uCoff;
                bool stopLoop = false;
                do
                {
                    if (sufS >= (1 << k))
                    {
                        building = (building << 1) | 0x1;
                        sufS -= 1 << k;
                        k++;
                    }
                    else
                    {
                        building <<= 1;
                        while (Int32Boolean.B(k--))
                        {
                            if (Int32Boolean.B((sufS >> k) & 0x1))
                                building = (building << 1) | 0x1;
                            else
                                building <<= 1;
                        }
                        stopLoop = true;
                    }
                }
                while (!stopLoop);
            }

            if (signedValFlag && synElVal != 0)
            {
                if (synElVal > 0)
                    building <<= 1;
                else
                    building = (building << 1) | 0x1;
            }

            return building;
        }
    }

    public static int FixedLengthBinarize(BitStreamReader reader, int cMax)
    {
        uint fixedLength = Intrinsic.CeilLog2((uint)cMax + 1u);
        return (int)reader.ReadBits(fixedLength);
    }

    public static int BinarizeMacroblockOrSubMacroblockType(BitStreamReader reader, bool isSISlice, bool isBSlice, bool isPorSPSlice, bool isSubMbType)
    {
        if (isSISlice)
        {
            if ((isSISlice ? 0 : 1) == 0)
            {
                return 0;
            }
            else
            {
                int b1 = LookUpISlice();
                return b1;
            }
        }
        else
        {
            if (isPorSPSlice)
            {
                int mbType = LookUpISlice();
                if (mbType is >= 5 and <= 30)
                    mbType -= 5;
                return mbType;
            }
            else if (isBSlice)
            {
                int mbType = LookUpBSlice();
                if (mbType is >= 23 and <= 48)
                    mbType -= 23;
                return mbType;
            }
            else if (isBSlice && isSubMbType)
            {
                return LookUpSubBSlice();
            }
            else if (isPorSPSlice && isSubMbType)
            {
                return LookUpSubPSPSlice();
            }
            else if (isBSlice && !isSubMbType)
            {
                return LookUpBSlice();
            }
            else if (isPorSPSlice && !isSubMbType)
            {
                return LookUpPSPSlice();
            }
            else
            {
                throw new InvalidOperationException("Invalid CABAC mb_type/sub_mb_type[] binarization");
            }
        }

        int LookUpISlice()
        {
            if (!reader.ReadBit())  // first bit == 0
            {
                // code "0" → symbol 0 (I_NxN)
                return 0;
            }
            else
            {
                // first bit == 1

                if (reader.ReadBit())  // second bit == 1
                {
                    // code "11" → symbol 25 (I_PCM)
                    return 25;
                }
                else
                {
                    // second bit == 0

                    if (!reader.ReadBit())  // third bit == 0
                    {
                        // third bit == 0

                        if (!reader.ReadBit())  // fourth bit == 0
                        {
                            // fourth bit == 0

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                // fifth bit == 0

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 0 0 0 0 0" → symbol 1
                                    return 1;
                                }
                                else
                                {
                                    // sixth bit == 1
                                    // code "1 0 0 0 0 1" → symbol 2
                                    return 2;
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 0 0 0 1 0" → symbol 3
                                    return 3;
                                }
                                else
                                {
                                    // sixth bit == 1
                                    // code "1 0 0 0 1 1" → symbol 4
                                    return 4;
                                }
                            }
                        }
                        else
                        {
                            // fourth bit == 1

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 0 1 0 0 0" → symbol 5
                                        return 5;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 0 1 0 0 1" → symbol 6
                                        return 6;
                                    }
                                }
                                else
                                {
                                    // sixth bit == 1

                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 0 1 0 1 0" → symbol 7
                                        return 7;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 0 1 0 1 1" → symbol 8
                                        return 8;
                                    }
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 0 1 1 0 0" → symbol 9
                                        return 9;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 0 1 1 0 1" → symbol 10
                                        return 10;
                                    }
                                }
                                else
                                {
                                    // sixth bit == 1

                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 0 1 1 1 0" → symbol 11
                                        return 11;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 0 1 1 1 1" → symbol 12
                                        return 12;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // third bit == 1

                        if (!reader.ReadBit())  // fourth bit == 0
                        {
                            // fourth bit == 0

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                // code "1 0 1 0 0 0" → symbol 13
                                return 13;
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 0 1 0 0 1" → symbol 14
                                    return 14;
                                }
                                else
                                {
                                    // sixth bit == 1
                                    // code "1 0 1 0 1 0" → symbol 15
                                    return 15;
                                }
                            }
                        }
                        else
                        {
                            // fourth bit == 1

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 1 1 0 0 0" → symbol 17
                                        return 17;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 1 1 0 0 1" → symbol 18
                                        return 18;
                                    }
                                }
                                else
                                {
                                    // sixth bit == 1

                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 1 1 0 1 0" → symbol 19
                                        return 19;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 1 1 0 1 1" → symbol 20
                                        return 20;
                                    }
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 1 1 1 0 0" → symbol 21
                                        return 21;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 1 1 1 0 1" → symbol 22
                                        return 22;
                                    }
                                }
                                else
                                {
                                    // sixth bit == 1

                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 0 1 1 1 1 0" → symbol 23
                                        return 23;
                                    }
                                    else
                                    {
                                        // seventh bit == 1
                                        // code "1 0 1 1 1 1 1" → symbol 24
                                        return 24;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        int LookUpBSlice()
        {
            if (!reader.ReadBit())  // first bit == 0
            {
                // code "0" → symbol 0 (B_Direct_16x16)
                return 0;
            }
            else
            {
                // first bit == 1

                if (!reader.ReadBit())  // second bit == 0
                {
                    // second bit == 0

                    if (!reader.ReadBit())  // third bit == 0
                    {
                        // code "1 0 0" → symbol 1 (B_L0_16x16)
                        return 1;
                    }
                    else
                    {
                        // third bit == 1

                        // code "1 0 1" → symbol 2 (B_L1_16x16)
                        return 2;
                    }
                }
                else
                {
                    // second bit == 1

                    if (!reader.ReadBit())  // third bit == 0
                    {
                        // third bit == 0

                        if (!reader.ReadBit())  // fourth bit == 0
                        {
                            // fourth bit == 0

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                // fifth bit == 0

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 1 0 0 0 0" → symbol 3 (B_Bi_16x16)
                                    return 3;
                                }
                                else
                                {
                                    // sixth bit == 1

                                    // code "1 1 0 0 0 1" → symbol 4 (B_L0_L0_16x8)
                                    return 4;
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 1 0 0 1 0" → symbol 5 (B_L0_L0_8x16)
                                    return 5;
                                }
                                else
                                {
                                    // sixth bit == 1

                                    // code "1 1 0 0 1 1" → symbol 6 (B_L1_L1_16x8)
                                    return 6;
                                }
                            }
                        }
                        else
                        {
                            // fourth bit == 1

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 1 0 1 0 0" → symbol 7 (B_L1_L1_8x16)
                                    return 7;
                                }
                                else
                                {
                                    // sixth bit == 1

                                    // code "1 1 0 1 0 1" → symbol 8 (B_L0_L1_16x8)
                                    return 8;
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 1 0 1 1 0" → symbol 9 (B_L0_L1_8x16)
                                    return 9;
                                }
                                else
                                {
                                    // sixth bit == 1

                                    // code "1 1 0 1 1 1" → symbol 10 (B_L1_L0_16x8)
                                    return 10;
                                }
                            }
                        }
                    }
                    else
                    {
                        // third bit == 1

                        if (!reader.ReadBit())  // fourth bit == 1
                        {
                            // code starts with "1 1 1 0"

                            if (!reader.ReadBit())  // fifth bit == 0
                            {
                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 1 1 0 0 0 0" → symbol 12 (B_L0_Bi_16x8)
                                        return 12;
                                    }
                                    else
                                    {
                                        // seventh bit == 1

                                        // code "1 1 1 0 0 0 1" → symbol 13 (B_L0_Bi_8x16)
                                        return 13;
                                    }
                                }
                                else
                                {
                                    // sixth bit == 1

                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 1 1 0 0 1 0" → symbol 14 (B_L1_Bi_16x8)
                                        return 14;
                                    }
                                    else
                                    {
                                        // seventh bit == 1

                                        // code "1 1 1 0 0 1 1" → symbol 15 (B_L1_Bi_8x16)
                                        return 15;
                                    }
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 1 1 0 1 0 0" → symbol 16 (B_Bi_L0_16x8)
                                        return 16;
                                    }
                                    else
                                    {
                                        // seventh bit == 1

                                        // code "1 1 1 0 1 0 1" → symbol 17 (B_Bi_L0_8x16)
                                        return 17;
                                    }
                                }
                                else
                                {
                                    // sixth bit == 1

                                    if (!reader.ReadBit())  // seventh bit == 0
                                    {
                                        // code "1 1 1 0 1 1 0" → symbol 18 (B_Bi_L1_16x8)
                                        return 18;
                                    }
                                    else
                                    {
                                        // seventh bit == 1

                                        // code "1 1 1 0 1 1 1" → symbol 19 (B_Bi_L1_8x16)
                                        return 19;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // fourth bit == 1

                            if (!reader.ReadBit())  // fifth bit == 1
                            {
                                if (!reader.ReadBit())  // sixth bit == 0
                                {
                                    // code "1 1 1 1 0 0 0" → symbol 20 (B_Bi_Bi_16x8)
                                    return 20;
                                }
                                else
                                {
                                    // code "1 1 1 1 0 0 1" → symbol 21 (B_Bi_Bi_8x16)
                                    return 21;
                                }
                            }
                            else
                            {
                                // code "1 1 1 1 1 1" → symbol 22 (B_8x8)
                                // Only 6 bits here, so we read only 6 bits total for this one

                                if (!reader.ReadBit())  // sixth bit == 1
                                {
                                    // code mismatch or error
                                    return -1;
                                }

                                return 22;
                            }
                        }
                    }
                }
            }
        }

        int LookUpPSPSlice()
        {
            if (!reader.ReadBit())  // first bit == 0
            {
                if (!reader.ReadBit())  // second bit == 0
                {
                    if (!reader.ReadBit())  // third bit == 0
                    {
                        // 0 0 0 → symbol 0 (P_L0_16x16)
                        return 0;
                    }
                    else
                    {
                        // 0 0 1 → symbol 3 (P_8x8)
                        return 3;
                    }
                }
                else
                {
                    // second bit == 1
                    if (!reader.ReadBit())  // third bit == 0
                    {
                        // 0 1 0 → symbol 2 (P_L0_L0_8x16)
                        return 2;
                    }
                    else
                    {
                        // 0 1 1 → symbol 1 (P_L0_L0_16x8)
                        return 1;
                    }
                }
            }
            else
            {
                // First bit == 1 → invalid for this table or error
                return -1;
            }
        }

        int LookUpSubPSPSlice()
        {
            if (reader.ReadBit()) // first bit == 1
            {
                // code "1" → symbol 0 (P_L0_8x8)
                return 0;
            }
            else
            {
                // first bit == 0

                if (!reader.ReadBit()) // second bit == 0
                {
                    // code "0 0" → symbol 1 (P_L0_8x4)
                    return 1;
                }
                else
                {
                    // second bit == 1

                    if (reader.ReadBit()) // third bit == 1
                    {
                        // code "0 1 1" → symbol 2 (P_L0_4x8)
                        return 2;
                    }
                    else
                    {
                        // third bit == 0

                        // code "0 1 0" → symbol 3 (P_L0_4x4)
                        return 3;
                    }
                }
            }
        }

        int LookUpSubBSlice()
        {
            if (!reader.ReadBit()) // first bit == 0
            {
                // code "0" → symbol 0 (B_Direct_8x8)
                return 0;
            }
            else
            {
                // first bit == 1

                if (!reader.ReadBit()) // second bit == 0
                {
                    if (!reader.ReadBit()) // third bit == 0
                    {
                        // code "1 0 0" → symbol 1 (B_L0_8x8)
                        return 1;
                    }
                    else
                    {
                        // third bit == 1
                        // code "1 0 1" → symbol 2 (B_L1_8x8)
                        return 2;
                    }
                }
                else
                {
                    // second bit == 1

                    if (!reader.ReadBit()) // third bit == 0
                    {
                        if (!reader.ReadBit()) // fourth bit == 0
                        {
                            if (!reader.ReadBit()) // fifth bit == 0
                            {
                                // code "1 1 0 0 0" → symbol 3 (B_Bi_8x8)
                                return 3;
                            }
                            else
                            {
                                // fifth bit == 1
                                // code "1 1 0 0 1" → symbol 4 (B_L0_8x4)
                                return 4;
                            }
                        }
                        else
                        {
                            // fourth bit == 1

                            if (!reader.ReadBit()) // fifth bit == 0
                            {
                                // code "1 1 0 1 0" → symbol 5 (B_L0_4x8)
                                return 5;
                            }
                            else
                            {
                                // fifth bit == 1
                                // code "1 1 0 1 1" → symbol 6 (B_L1_8x4)
                                return 6;
                            }
                        }
                    }
                    else
                    {
                        // third bit == 1

                        if (!reader.ReadBit()) // fourth bit == 0
                        {
                            if (!reader.ReadBit()) // fifth bit == 0
                            {
                                if (!reader.ReadBit()) // sixth bit == 0
                                {
                                    // code "1 1 1 0 0 0" → symbol 7 (B_L1_4x8)
                                    return 7;
                                }
                                else
                                {
                                    // sixth bit == 1
                                    // code "1 1 1 0 0 1" → symbol 8 (B_Bi_8x4)
                                    return 8;
                                }
                            }
                            else
                            {
                                // fifth bit == 1

                                if (!reader.ReadBit()) // sixth bit == 0
                                {
                                    // code "1 1 1 0 1 0" → symbol 9 (B_Bi_4x8)
                                    return 9;
                                }
                                else
                                {
                                    // sixth bit == 1
                                    // code "1 1 1 0 1 1" → symbol 10 (B_L0_4x4)
                                    return 10;
                                }
                            }
                        }
                        else
                        {
                            // fourth bit == 1

                            if (!reader.ReadBit()) // fifth bit == 0
                            {
                                // code "1 1 1 1 0" → symbol 11 (B_L1_4x4)
                                return 11;
                            }
                            else
                            {
                                // code "1 1 1 1 1" → symbol 12 (B_Bi_4x4)
                                return 12;
                            }
                        }
                    }
                }
            }
        }
    }

    public static int BinarizeCodedBlockPattern(BitStreamReader reader, int chromaArrayType)
    {
        BitString bsPrefix = BitString.From(TruncatedUnaryBinarize(reader, 15));

        if (chromaArrayType is not 0 and not 3)
        {
            BitString bsSuffix = BitString.From(TruncatedUnaryBinarize(reader, 2));

            BitString bsResult = bsPrefix + bsSuffix;

            return bsResult.Value;
        }
        else
        {
            return bsPrefix.Value;
        }
    }

    public static int BinarizeMbQpDelta(BitStreamReader reader)
    {
        return UnaryBinarize(reader);
    }

    // I might be wrong, so I'll include this method so that if I'm wrong, I can change it later.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int AssociateWithPrefixSuffix(int prefix, int suffix) => prefix;

    public static (int binarized, int maxBinIdxCtx, int ctxIdxOffset) Binarize(BitStreamReader reader, GeneralSliceType sliceType, SyntaxElement element, int chromaArrayType, int NumC8x8, ResidualBlockType residualBlkType, bool isFrameMacroblock)
    {
        int maxBinIdxCtx = 0;
        int ctxIdxOffset = 0;
        int binarized;

        switch (element)
        {
            case SyntaxElement.MacroblockType:
                {
                    if (sliceType == GeneralSliceType.SI)
                    {
                        maxBinIdxCtx = AssociateWithPrefixSuffix(0, 6);
                        ctxIdxOffset = AssociateWithPrefixSuffix(0, 3);
                    }
                    else if (sliceType == GeneralSliceType.I)
                    {
                        maxBinIdxCtx = 6;
                        ctxIdxOffset = 3;
                    }
                    else if (sliceType is GeneralSliceType.P or GeneralSliceType.SP)
                    {
                        maxBinIdxCtx = AssociateWithPrefixSuffix(2, 5);
                        ctxIdxOffset = AssociateWithPrefixSuffix(14, 17);
                    }
                    else if (sliceType == GeneralSliceType.B)
                    {
                        maxBinIdxCtx = AssociateWithPrefixSuffix(3, 5);
                        ctxIdxOffset = AssociateWithPrefixSuffix(27, 32);
                    }

                    binarized = BinarizeMacroblockOrSubMacroblockType(
                        reader,
                        sliceType == GeneralSliceType.SI,
                        sliceType == GeneralSliceType.B,
                        sliceType == GeneralSliceType.P || sliceType == GeneralSliceType.SP,
                        false);
                }
                break;

            case SyntaxElement.MacroblockSkipFlag:
                {
                    if (sliceType is GeneralSliceType.P or GeneralSliceType.SP)
                    {
                        maxBinIdxCtx = 0;
                        ctxIdxOffset = 11;
                    }
                    else if (sliceType == GeneralSliceType.B)
                    {
                        maxBinIdxCtx = 0;
                        ctxIdxOffset = 24;
                    }

                    binarized = FixedLengthBinarize(reader, 1);

                    break;
                }

            case SyntaxElement.SubMacroblockType:
                {
                    if (sliceType is GeneralSliceType.P or GeneralSliceType.SP)
                    {
                        maxBinIdxCtx = 2;
                        ctxIdxOffset = 21;
                    }
                    else if (sliceType == GeneralSliceType.B)
                    {
                        maxBinIdxCtx = 3;
                        ctxIdxOffset = 36;
                    }

                    binarized = BinarizeMacroblockOrSubMacroblockType(
                        reader,
                        sliceType == GeneralSliceType.SI,
                        sliceType == GeneralSliceType.B,
                        sliceType == GeneralSliceType.P || sliceType == GeneralSliceType.SP,
                        true);

                    break;
                }

            case SyntaxElement.MotionVectorDifferenceX:
                {
                    maxBinIdxCtx = 4;
                    ctxIdxOffset = 40;

                    binarized = UegkBinarize(
                        reader,
                        true,
                        3,
                        9);
                    
                    break;
                }

            case SyntaxElement.MotionVectorDifferenceY:
                {
                    maxBinIdxCtx = 4;
                    ctxIdxOffset = 47;

                    binarized = UegkBinarize(
                        reader,
                        true,
                        3,
                        9);
                    
                    break;
                }

            case SyntaxElement.ReferenceIndex:
                {
                    maxBinIdxCtx = 2;
                    ctxIdxOffset = 54;

                    binarized = UnaryBinarize(reader);
                    
                    break;
                }

            case SyntaxElement.MacroblockQuantizationParameterDelta:
                {
                    maxBinIdxCtx = 2;
                    ctxIdxOffset = 60;

                    binarized = BinarizeMbQpDelta(reader);
                    
                    break;
                }

            case SyntaxElement.IntraChromaPredictionMode:
                {
                    maxBinIdxCtx = 1;
                    ctxIdxOffset = 64;

                    binarized = TruncatedUnaryBinarize(reader, 3);
                    
                    break;
                }

            case SyntaxElement.PreviousIntraNxNPredictionModeFlag:
                {
                    maxBinIdxCtx = 0;
                    ctxIdxOffset = 68;

                    binarized = FixedLengthBinarize(reader, 1);
                    
                    break;
                }

            case SyntaxElement.RemainingIntraNxNPredictionMode:
                {
                    maxBinIdxCtx = 0;
                    ctxIdxOffset = 69;

                    binarized = FixedLengthBinarize(reader, 8);
                    
                    break;
                }

            case SyntaxElement.MacroblockFieldDecodingFlag:
                {
                    maxBinIdxCtx = 0;
                    ctxIdxOffset = 70;

                    binarized = FixedLengthBinarize(reader, 1);
                    
                    break;
                }

            case SyntaxElement.CodedBlockPattern:
                {
                    maxBinIdxCtx = AssociateWithPrefixSuffix(3, 1);
                    ctxIdxOffset = AssociateWithPrefixSuffix(73, 77);

                    binarized = BinarizeCodedBlockPattern(reader, chromaArrayType);
                    
                    break;
                }

            case SyntaxElement.CodedBlockFlag:
                {
                    (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(residualBlkType, NumC8x8);

                    maxBinIdxCtx = 0;

                    if (ctxBlockCat < 5)
                    {
                        ctxIdxOffset = 85;
                    }
                    else if (5 < ctxBlockCat && ctxBlockCat < 9)
                    {
                        ctxIdxOffset = 460;
                    }
                    else if (9 < ctxBlockCat && ctxBlockCat < 13)
                    {
                        ctxIdxOffset = 472;
                    }
                    else if (ctxBlockCat is 5 or 9 or 13)
                    {
                        ctxIdxOffset = 1012;
                    }

                    binarized = FixedLengthBinarize(reader, 1);

                    break;
                }

            case SyntaxElement.SignificantCoeffFlag:
                {
                    (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(residualBlkType, NumC8x8);

                    maxBinIdxCtx = 0;

                    if (isFrameMacroblock)
                    {
                        if (ctxBlockCat < 5)
                        {
                            ctxIdxOffset = 105;
                        }
                        else if (ctxBlockCat == 5)
                        {
                            ctxIdxOffset = 402;
                        }
                        else if (5 < ctxBlockCat && ctxBlockCat < 9)
                        {
                            ctxIdxOffset = 484;
                        }
                        else if (9 < ctxBlockCat && ctxBlockCat < 13)
                        {
                            ctxIdxOffset = 528;
                        }
                        else if (ctxBlockCat == 9)
                        {
                            ctxIdxOffset = 660;
                        }
                        else if (ctxBlockCat == 13)
                        {
                            ctxIdxOffset = 718;
                        }
                    }
                    else // field macroblock
                    {
                        if (ctxBlockCat < 5)
                        {
                            ctxIdxOffset = 277;
                        }
                        else if (ctxBlockCat == 5)
                        {
                            ctxIdxOffset = 436;
                        }
                        else if (5 < ctxBlockCat && ctxBlockCat < 9)
                        {
                            ctxIdxOffset = 776;
                        }
                        else if (9 < ctxBlockCat && ctxBlockCat < 13)
                        {
                            ctxIdxOffset = 820;
                        }
                        else if (ctxBlockCat == 9)
                        {
                            ctxIdxOffset = 675;
                        }
                        else if (ctxBlockCat == 13)
                        {
                            ctxIdxOffset = 733;
                        }
                    }

                    binarized = FixedLengthBinarize(reader, 1);

                    break;
                }

            case SyntaxElement.LastSignificantCoeffFlag:
                {
                    (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(residualBlkType, NumC8x8);

                    maxBinIdxCtx = 0;

                    if (isFrameMacroblock)
                    {
                        if (ctxBlockCat < 5)
                        {
                            ctxIdxOffset = 166;
                        }
                        else if (ctxBlockCat == 5)
                        {
                            ctxIdxOffset = 417;
                        }
                        else if (5 < ctxBlockCat && ctxBlockCat < 9)
                        {
                            ctxIdxOffset = 572;
                        }
                        else if (9 < ctxBlockCat && ctxBlockCat < 13)
                        {
                            ctxIdxOffset = 616;
                        }
                        else if (ctxBlockCat == 9)
                        {
                            ctxIdxOffset = 690;
                        }
                        else if (ctxBlockCat == 13)
                        {
                            ctxIdxOffset = 748;
                        }
                    }
                    else // field macroblock
                    {
                        if (ctxBlockCat < 5)
                        {
                            ctxIdxOffset = 338;
                        }
                        else if (ctxBlockCat == 5)
                        {
                            ctxIdxOffset = 451;
                        }
                        else if (5 < ctxBlockCat && ctxBlockCat < 9)
                        {
                            ctxIdxOffset = 864;
                        }
                        else if (9 < ctxBlockCat && ctxBlockCat < 13)
                        {
                            ctxIdxOffset = 908;
                        }
                        else if (ctxBlockCat == 9)
                        {
                            ctxIdxOffset = 699;
                        }
                        else if (ctxBlockCat == 13)
                        {
                            ctxIdxOffset = 757;
                        }
                    }

                    binarized = FixedLengthBinarize(reader, 1);

                    break;
                }

            case SyntaxElement.CoeffAbsLevelMinus1:
                {
                    (_, int ctxBlockCat) = CabacFunctions.DeriveCtxBlockCatAndMaxNumCoeff(residualBlkType, NumC8x8);

                    maxBinIdxCtx = AssociateWithPrefixSuffix(1, 0);

                    if (ctxBlockCat < 5)
                    {
                        ctxIdxOffset = 227;
                    }
                    else if (ctxBlockCat == 5)
                    {
                        ctxIdxOffset = 426;
                    }
                    else if (5 < ctxBlockCat && ctxBlockCat < 9)
                    {
                        ctxIdxOffset = 952;
                    }
                    else if (9 < ctxBlockCat && ctxBlockCat < 13)
                    {
                        ctxIdxOffset = 982;
                    }
                    else if (ctxBlockCat == 9)
                    {
                        ctxIdxOffset = 708;
                    }
                    else if (ctxBlockCat == 13)
                    {
                        ctxIdxOffset = 766;
                    }

                    binarized = UegkBinarize(reader, false, 0, 14);

                    break;
                }

            case SyntaxElement.CoeffSignFlag:
                {
                    binarized = FixedLengthBinarize(reader, 1);

                    // Doesn't look like we assign anything.
                    break;
                }

            case SyntaxElement.EndOfSliceFlag:
                {
                    ctxIdxOffset = 276;

                    binarized = FixedLengthBinarize(reader, 1);

                    break;
                }

            case SyntaxElement.TransformSize8x8Flag:
                {
                    ctxIdxOffset = 399;

                    binarized = FixedLengthBinarize(reader, 1);

                    break;
                }

            default:
                throw new NotImplementedException("Syntax element named " + element + " is not yet implemented");
        }

        return (binarized, maxBinIdxCtx, ctxIdxOffset);
    }
}
