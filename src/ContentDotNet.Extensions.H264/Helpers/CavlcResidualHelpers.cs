using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Helpers;

internal static class CavlcResidualHelpers
{
    // Each element is separated with the following columns:
    //
    // *1 - TO is an abbreviation for TrailingOnes(coeff_token)
    // *2 - TC is an abbreviation for TotalCoeff(coeff_token)
    //
    //   TO*1   TC*2    0 <= nC < 2     2 <= nC < 4     4 <= nC < 8     8 <= nC     nC = = −1       nC = = −2  
    //

    public static ReadOnlySpan<byte> LUT =>
    [
        0,      0,      0b1,    0b11,   0b1111, 0b000011,       0b01,   0b1,
        0,      1,      0b000101,       0b001011,       0b001111,       0b000000,       0b000111,       0b0001111,
        1,      1,      0b01,   0b10,   0b1110, 0b000001,       0b1,    0b01,
        0,      2,      0b000001111,    0b000111,       0b001011,       0b000100,       0b000100,       0b0001110,
        1,      2,      0b000100,       0b00111,        0b01111,        0b000101,       0b000110,       0b0001101,
        2,      2,      0b001,  0b011,  0b1101, 0b000110,       0b001,  0b001,
        0,      3,      0b000000111,    0b0000111,      0b001000,       0b001000,       0b000011,       0b000000111,
        1,      3,      0b00000110,     0b001010,       0b01100,        0b001001,       0b0000011,      0b0001100,
        2,      3,      0b0000101,      0b001001,       0b01110,        0b001010,       0b0000010,      0b0001011,
        3,      3,      0b00011,        0b0101, 0b1100, 0b001011,       0b000101,       0b00001,
        0,      4,      0b0000000111,   0b00000111,     0b0001111,      0b001100,       0b000010,       0b000000110,
        1,      4,      0b000000110,    0b0000110,      0b01010,        0b001101,       0b00000011,     0b000000101,
        2,      4,      0b00000101,     0b000101,       0b01011,        0b001110,       0b00000010,     0b0001010,
        3,      4,      0b000011,       0b0100, 0b1011, 0b001111,       0b0000000,      0b000001,
        0,      5,      0b00000000111,  0b00000100,     0b0001011,      0b010000,       0b0,    0b0000000111,
        1,      5,      0b0000000110,   0b0000110,      0b01000,        0b010001,       0b0,    0b0000000110,
        2,      5,      0b000000101,    0b0000101,      0b01001,        0b010010,       0b0,    0b000000100,
        3,      5,      0b0000100,      0b00110,        0b1010, 0b010011,       0b0,    0b0001001,
        0,      6,      0b0000000001111,        0b000000111,    0b0001001,      0b010100,       0b0,    0b00000000111,
        1,      6,      0b00000000110,  0b00000110,     0b001110,       0b010101,       0b0,    0b00000000110,
        2,      6,      0b0000000101,   0b00000101,     0b001101,       0b010110,       0b0,    0b0000000101,
        3,      6,      0b00000100,     0b001000,       0b1001, 0b010111,       0b0,    0b0001000,
        0,      7,      0b0000000001011,        0b00000001111,  0b0001000,      0b011000,       0b0,    0b000000000111,
        1,      7,      0b0000000001110,        0b000000110,    0b001010,       0b011001,       0b0,    0b000000000110,
        2,      7,      0b00000000101,  0b000000101,    0b001001,       0b011010,       0b0,    0b00000000101,
        3,      7,      0b000000100,    0b000100,       0b1000, 0b011011,       0b0,    0b0000000100,
        0,      8,      0b0000000001000,        0b00000001011,  0b00001111,     0b011100,       0b0,    0b0000000000111,
        1,      8,      0b0000000001010,        0b00000001110,  0b0001110,      0b011101,       0b0,    0b000000000101,
        2,      8,      0b0000000001101,        0b00000001101,  0b0001101,      0b011110,       0b0,    0b000000000100,
        3,      8,      0b0000000100,   0b0000100,      0b01101,        0b011111,       0b0,    0b00000000100,
        0,      9,      0b00000000001111,       0b000000001111, 0b00001011,     0b10000,        0b0,    0b0,
        1,      9,      0b00000000001110,       0b00000001010,  0b00001110,     0b10001,        0b0,    0b0,
        2,      9,      0b0000000001001,        0b00000001001,  0b0001010,      0b100010,       0b0,    0b0,
        3,      9,      0b00000000100,  0b000000100,    0b001100,       0b100011,       0b0,    0b0,
        0,      10,     0b00000000001011,       0b000000001011, 0b000001111,    0b100100,       0b0,    0b0,
        1,      10,     0b00000000001010,       0b000000001110, 0b00001010,     0b100101,       0b0,    0b0,
        2,      10,     0b00000000001101,       0b000000001101, 0b00001101,     0b100110,       0b0,    0b0,
        3,      10,     0b000000000110, 0b00000001100,  0b0001100,      0b100111,       0b0,    0b0,
        0,      11,     0b000000000001111,      0b000000001000, 0b000001011,    0b101000,       0b0,    0b0,
        1,      11,     0b000000000001110,      0b000000001010, 0b000001110,    0b101001,       0b0,    0b0,
        2,      11,     0b00000000001001,       0b000000001001, 0b00001001,     0b101010,       0b0,    0b0,
        3,      11,     0b00000000001100,       0b00000001000,  0b00001100,     0b101011,       0b0,    0b0,
        0,      12,     0b000000000001011,      0b0000000001111,        0b000001000,    0b101100,       0b0,    0b0,
        1,      12,     0b000000000001010,      0b0000000001110,        0b000001010,    0b101101,       0b0,    0b0,
        2,      12,     0b000000000001101,      0b0000000001101,        0b000001101,    0b101110,       0b0,    0b0,
        3,      12,     0b00000000001000,       0b000000001100, 0b00001000,     0b101111,       0b0,    0b0,
        0,      13,     0b0000000000001111,     0b0000000001011,        0b0000001101,   0b110000,       0b0,    0b0,
        1,      13,     0b000000000000001,      0b0000000001010,        0b000000111,    0b110001,       0b0,    0b0,
        2,      13,     0b000000000001001,      0b0000000001001,        0b000001001,    0b110010,       0b0,    0b0,
        3,      13,     0b000000000001100,      0b0000000001100,        0b000001100,    0b110011,       0b0,    0b0,
        0,      14,     0b0000000000001011,     0b0000000000111,        0b0000001001,   0b110100,       0b0,    0b0,
        1,      14,     0b0000000000001110,     0b00000000001011,       0b0000001100,   0b110101,       0b0,    0b0,
        2,      14,     0b0000000000001101,     0b0000000000110,        0b0000001011,   0b110110,       0b0,    0b0,
        3,      14,     0b000000000001000,      0b0000000001000,        0b00000010010,  0b110111,       0b0,    0b0,
        0,      15,     0b0000000000000111,     0b00000000001001,       0b0000000101,   0b1111000,      0b0,    0b0,
        1,      15,     0b0000000000001010,     0b00000000001000,       0b0000001000,   0b1111001,      0b0,    0b0,
        2,      15,     0b0000000000001001,     0b00000000001010,       0b00000000111,  0b111010,       0b0,    0b0,
        3,      15,     0b0000000000001100,     0b0000000000001,        0b00000000110,  0b1111011,      0b0,    0b0,
        0,      16,     0b0000000000000100,     0b00000000000111,       0b0000000001,   0b111100,       0b0,    0b0,
        1,      16,     0b0000000000000110,     0b00000000000110,       0b0000000100,   0b111101,       0b0,    0b0,
        2,      16,     0b0000000000000101,     0b00000000000101,       0b0000000011,   0b111110,       0b0,    0b0,
        3,      16,     0b0000000000001000,     0b00000000000100,       0b0000000010,   0b111111,       0b0,    0b0,
    ];

    /// <summary>
    ///   Specifies number of bits to read for every column/row in <see cref="LUT"/>. First two columns
    ///   shall be ignored as they imply TotalCoeff and TrailingOnes.
    /// </summary>
    public static ReadOnlySpan<int> Sizes =>
    [
        1,      1,      1,      2,      4,      6,      2,      1,
        1,      1,      6,      6,      6,      6,      6,      7,
        1,      1,      2,      2,      4,      6,      1,      2,
        1,      1,      9,      6,      6,      6,      6,      7,
        1,      1,      6,      5,      5,      6,      6,      7,
        1,      1,      3,      3,      4,      6,      3,      3,
        1,      1,      9,      7,      6,      6,      6,      9,
        1,      1,      8,      6,      5,      6,      7,      7,
        1,      1,      7,      6,      5,      6,      7,      7,
        1,      1,      5,      4,      4,      6,      6,      5,
        1,      1,      10,     8,      7,      6,      6,      9,
        1,      1,      9,      7,      5,      6,      8,      9,
        1,      1,      8,      6,      5,      6,      8,      7,
        1,      1,      6,      4,      4,      6,      7,      6,
        1,      1,      11,     8,      7,      6,      0,      10,
        1,      1,      10,     7,      5,      6,      0,      10,
        1,      1,      9,      7,      5,      6,      0,      9,
        1,      1,      7,      5,      4,      6,      0,      7,
        1,      1,      13,     9,      7,      6,      0,      11,
        1,      1,      11,     8,      6,      6,      0,      11,
        1,      1,      10,     8,      6,      6,      0,      10,
        1,      1,      8,      6,      4,      6,      0,      7,
        1,      1,      13,     11,     7,      6,      0,      12,
        1,      1,      13,     9,      6,      6,      0,      12,
        1,      1,      11,     9,      6,      6,      0,      11,
        1,      1,      9,      6,      4,      6,      0,      10,
        1,      1,      13,     11,     8,      6,      0,      13,
        1,      1,      13,     11,     7,      6,      0,      12,
        1,      1,      13,     11,     7,      6,      0,      12,
        1,      1,      10,     7,      5,      6,      0,      11,
        1,      1,      14,     12,     8,      5,      0,      0,
        1,      1,      14,     11,     8,      5,      0,      0,
        1,      1,      13,     11,     7,      6,      0,      0,
        1,      1,      11,     9,      6,      6,      0,      0,
        1,      2,      14,     12,     9,      6,      0,      0,
        1,      2,      14,     12,     8,      6,      0,      0,
        1,      2,      14,     12,     8,      6,      0,      0,
        1,      2,      12,     11,     7,      6,      0,      0,
        1,      2,      15,     12,     9,      6,      0,      0,
        1,      2,      15,     12,     9,      6,      0,      0,
        1,      2,      14,     12,     8,      6,      0,      0,
        1,      2,      14,     11,     8,      6,      0,      0,
        1,      2,      15,     13,     9,      6,      0,      0,
        1,      2,      15,     13,     9,      6,      0,      0,
        1,      2,      15,     13,     9,      6,      0,      0,
        1,      2,      14,     12,     8,      6,      0,      0,
        1,      2,      16,     13,     10,     6,      0,      0,
        1,      2,      15,     13,     9,      6,      0,      0,
        1,      2,      15,     13,     9,      6,      0,      0,
        1,      2,      15,     13,     9,      6,      0,      0,
        1,      2,      16,     13,     10,     6,      0,      0,
        1,      2,      16,     14,     10,     6,      0,      0,
        1,      2,      16,     13,     10,     6,      0,      0,
        1,      2,      15,     13,     11,     6,      0,      0,
        1,      2,      16,     14,     10,     7,      0,      0,
        1,      2,      16,     14,     10,     7,      0,      0,
        1,      2,      16,     14,     11,     6,      0,      0,
        1,      2,      16,     13,     11,     7,      0,      0,
        1,      2,      16,     14,     10,     6,      0,      0,
        1,      2,      16,     14,     10,     6,      0,      0,
        1,      2,      16,     14,     10,     6,      0,      0,
        1,      2,      16,     14,     10,     6,      0,      0,
    ];

    public static (int TotalCoeff, int TrailingOnes)? DecodeCoeffToken(BitStreamReader reader, int nC)
    {
        int column;
        if (nC >= 0 && nC < 2) column = 2;
        else if (nC < 4) column = 3;
        else if (nC < 8) column = 4;
        else if (nC >= 8) column = 5;
        else if (nC == -1) column = 6;
        else if (nC == -2) column = 7;
        else throw new ArgumentOutOfRangeException(nameof(nC));

        for (int row = 0; row < LUT.Length; row += 8)
        {
            int to = LUT[row];
            int tc = LUT[row + 1];
            byte vlc = LUT[row + column];
            int size = Sizes[row + column];

            uint bits = reader.PeekBits((uint)size);
            if (bits == vlc)
            {
                _ = reader.ReadBits((uint)size);
                return (tc, to);
            }
        }

        return null;
    }

    public static int GetNC(BitStreamReader reader, NalUnit nalu, DerivationContext dc, int chromaArrayType, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx, int chroma4x4BlkIdx, ResidualMode mode, IMacroblockUtility util, bool constrainedIntraPredFlag)
    {
        if (mode == ResidualMode.ChromaDcLevel)
            return chromaArrayType == 1 ? -1 : -2;

        if (mode == ResidualMode.Intra16x16DcLevel)
            luma4x4BlkIdx = 0;

        if (mode == ResidualMode.CbIntra16x16DcLevel)
            cb4x4BlkIdx = 0;

        if (mode == ResidualMode.CrIntra16x16DcLevel)
            cr4x4BlkIdx = 0;

        (int address, int block) blkA = (128, 128);
        (int address, int block) blkB = (128, 128);

        bool mbAddrAAvailable;
        int mbAddrA;
        int mbAddrB;
        bool mbAddrBAvailable;

        if (mode is ResidualMode.Intra16x16DcLevel or ResidualMode.Intra16x16AcLevel or ResidualMode.LumaLevel4x4)
        {
            Decoder264.Derive4x4LumaBlocks(luma4x4BlkIdx, dc,
                out mbAddrA, out mbAddrAAvailable, out int luma4x4BlkIdxA, out bool luma4x4BlkIdxAAvailable,
                out mbAddrB, out mbAddrBAvailable, out int luma4x4BlkIdxB, out bool luma4x4BlkIdxBAvailable);

            if (mbAddrAAvailable)
                blkA.address = mbAddrA;
            else
                blkA.address = 128;

            if (luma4x4BlkIdxAAvailable)
                blkA.block = luma4x4BlkIdxA;
            else
                blkA.block = 128;

            if (mbAddrBAvailable)
                blkB.address = mbAddrB;
            else
                blkB.address = 128;

            if (luma4x4BlkIdxBAvailable)
                blkB.block = luma4x4BlkIdxB;
            else
                blkB.block = 128;
        }
        else if (mode is ResidualMode.CbIntra16x16DcLevel or ResidualMode.CbIntra16x16AcLevel or ResidualMode.CbLevel4x4)
        {
            Decoder264.Derive4x4LumaBlocks(
                cb4x4BlkIdx, dc,
                out mbAddrA, out mbAddrAAvailable, out int cb4x4BlkIdxA, out bool cb4x4BlkIdxAAvailable,
                out mbAddrB, out mbAddrBAvailable, out int cb4x4BlkIdxB, out bool cb4x4BlkIdxBAvailable);

            if (mbAddrAAvailable)
                blkA.address = mbAddrA;
            else
                blkA.address = 128;

            if (cb4x4BlkIdxAAvailable)
                blkA.block = cb4x4BlkIdxA;
            else
                blkA.block = 128;

            if (mbAddrBAvailable)
                blkB.address = mbAddrB;
            else
                blkB.address = 128;

            if (cb4x4BlkIdxBAvailable)
                blkB.block = cb4x4BlkIdxB;
            else
                blkB.block = 128;
        }
        else if (mode is ResidualMode.CrIntra16x16DcLevel or ResidualMode.CrIntra16x16AcLevel or ResidualMode.CrLevel4x4)
        {
            Decoder264.Derive4x4LumaBlocks(
                cr4x4BlkIdx, dc,
                out mbAddrA, out mbAddrAAvailable, out int cr4x4BlkIdxA, out bool cr4x4BlkIdxAAvailable,
                out mbAddrB, out mbAddrBAvailable, out int cr4x4BlkIdxB, out bool cr4x4BlkIdxBAvailable);

            if (mbAddrAAvailable)
                blkA.address = mbAddrA;
            else
                blkA.address = 128;

            if (cr4x4BlkIdxAAvailable)
                blkA.block = cr4x4BlkIdxA;
            else
                blkA.block = 128;

            if (mbAddrBAvailable)
                blkB.address = mbAddrB;
            else
                blkB.address = 128;

            if (cr4x4BlkIdxBAvailable)
                blkB.block = cr4x4BlkIdxB;
            else
                blkB.block = 128;
        }
        else
        {
            Decoder264.Derive4x4ChromaBlocks(dc, chroma4x4BlkIdx,
                out mbAddrA, out mbAddrAAvailable, out int chroma4x4BlkIdxA, out bool chroma4x4BlkIdxAAvailable,
                out mbAddrB, out mbAddrBAvailable, out int chroma4x4BlkIdxB, out bool chroma4x4BlkIdxBAvailable
            );

            if (mbAddrAAvailable)
                blkA.address = mbAddrA;
            else
                blkA.address = 128;

            if (mbAddrBAvailable)
                blkB.address = mbAddrB;
            else
                blkB.address = 128;

            if (chroma4x4BlkIdxAAvailable)
                blkA.block = chroma4x4BlkIdxA;
            else
                blkA.block = 128;

            if (chroma4x4BlkIdxBAvailable)
                blkB.block = chroma4x4BlkIdxB;
            else
                blkB.block = 128;
        }

        bool availableFlagA = !(!mbAddrAAvailable || (util.IsCodedWithIntra(dc.CurrMbAddr) && constrainedIntraPredFlag && util.IsCodedWithInter(mbAddrA) && nalu.NalUnitType is 2 or 3 or 4));
        bool availableFlagB = !(!mbAddrBAvailable || (util.IsCodedWithIntra(dc.CurrMbAddr) && constrainedIntraPredFlag && util.IsCodedWithInter(mbAddrB) && nalu.NalUnitType is 2 or 3 or 4));

        int nA = 0;
        int nB = 0;

        if (availableFlagA)
        {
            uint targetMbType = util.GetMbType(mbAddrA);
            if (targetMbType is P_Skip or B_Skip)
            {
                nA = 0;
            }
            else if (targetMbType is not I_PCM && util.AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(blkA.address))
            {
                nA = 0;
            }
            else if (targetMbType is I_PCM)
            {
                nA = 16;
            }
            else
            {
                nA = util.GetTotalCoefficient(blkA.address);
            }
        }

        if (availableFlagB)
        {
            uint targetMbType = util.GetMbType(mbAddrB);
            if (targetMbType is P_Skip or B_Skip)
            {
                nB = 0;
            }
            else if (targetMbType is not I_PCM && util.AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(blkB.address))
            {
                nB = 0;
            }
            else if (targetMbType is I_PCM)
            {
                nB = 16;
            }
            else
            {
                nB = util.GetTotalCoefficient(blkB.address);
            }
        }

        return (availableFlagA && availableFlagB) ? nA + nB :
               (availableFlagA && !availableFlagB) ? nA :
               (!availableFlagA && availableFlagB) ? nB :
               0;
    }
}
