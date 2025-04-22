using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class Decoder264
{
    private static readonly (int x, int y) LumaLocationsA = (-1, 0);
    private static readonly (int x, int y) LumaLocationsB = (0, -1);
    private static readonly (int x, int y) LumaLocationsD = (-1, -1);

    public static void Derive4x4LumaBlocks(
        int luma4x4BlkIdx,
        DerivationContext dc,
        out int mbAddrA, out bool mbAddrAAvailable, out int luma4x4BlkIdxA, out bool luma4x4BlkIdxAAvailable,
        out int mbAddrB, out bool mbAddrBAvailable, out int luma4x4BlkIdxB, out bool luma4x4BlkIdxBAvailable)
    {
        DeriveBlock(LumaLocationsA, luma4x4BlkIdx, dc, out mbAddrA, out mbAddrAAvailable, out luma4x4BlkIdxA, out luma4x4BlkIdxAAvailable);
        DeriveBlock(LumaLocationsB, luma4x4BlkIdx, dc, out mbAddrB, out mbAddrBAvailable, out luma4x4BlkIdxB, out luma4x4BlkIdxBAvailable);

        static void DeriveBlock((int x, int y) distances, int luma4x4BlkIdx, DerivationContext dc, out int mbAddrN, out bool mbAddrNAvailable, out int luma4x4BlkIdxN, out bool luma4x4BlkIdxNAvailable)
        {
            int x = 0;
            int y = 0;
            Util264.Inverse4x4LumaScan(luma4x4BlkIdx, ref x, ref y);

            int xN = x + distances.x;
            int yN = y + distances.y;

            mbAddrN = 0;
            DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out mbAddrNAvailable);

            if (!mbAddrNAvailable)
            {
                luma4x4BlkIdxN = 0;
                luma4x4BlkIdxNAvailable = false;
            }
            else
            {
                Derive4x4LumaBlockIndices(xW, yW, out luma4x4BlkIdxN);
                luma4x4BlkIdxNAvailable = true;
            }
        }
    }

    public static void Derive8x8LumaBlocks(
        DerivationContext dc,
        int luma8x8BlkIdx,
        out int mbAddrA, out bool mbAddrAAvailable, out int luma8x8BlkIdxA, out bool luma8x8BlkIdxAAvailable,
        out int mbAddrB, out bool mbAddrBAvailable, out int luma8x8BlkIdxB, out bool luma8x8BlkIdxBAvailable)
    {
        mbAddrA = 0;
        mbAddrB = 0;
        DeriveCore(LumaLocationsA, ref mbAddrA, out mbAddrAAvailable, out luma8x8BlkIdxA, out luma8x8BlkIdxAAvailable);
        DeriveCore(LumaLocationsB, ref mbAddrB, out mbAddrBAvailable, out luma8x8BlkIdxB, out luma8x8BlkIdxBAvailable);

        void DeriveCore((int x, int y) locations, ref int mbAddrN, out bool mbAddrNAvailable, out int luma8x8BlkIdxN, out bool luma8x8BlkIdxNAvailable)
        {
            int xD = locations.x;
            int yD = locations.y;

            int xN = luma8x8BlkIdx % 2 * 8 + xD;
            int yN = luma8x8BlkIdx / 2 * 8 + yD;

            DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out mbAddrNAvailable);

            if (!mbAddrNAvailable)
            {
                luma8x8BlkIdxNAvailable = false;
                luma8x8BlkIdxN = 0;
                return;
            }
            else
            {
                Derive8x8LumaBlockIndices(xW, yW, out luma8x8BlkIdxN);
                luma8x8BlkIdxNAvailable = true;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Derive4x4LumaBlockIndices(int xP, int yP, out int luma4x4BlkIdx)
    {
        luma4x4BlkIdx = 8 * (yP / 8) + 4 * (xP / 8) + 2 * (yP % 8 / 4) + xP % 8 / 4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Derive4x4ChromaBlockIndices(int xP, int yP, out int chroma4x4BlkIdx)
    {
        chroma4x4BlkIdx = 2 * (yP / 4) + xP / 4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Derive8x8LumaBlockIndices(int xP, int yP, out int luma8x8BlkIdx)
    {
        luma8x8BlkIdx = 2 * (yP / 8) + xP / 8;
    }

    public static void DeriveMacroblockAndSubMacroblockPartitionIndices(int xP, int yP, int mbType, Span<int> subMbType, ref int mbPartIdx, ref int subMbPartIdx)
    {
        if (mbType == MacroblockKinds.I)
        {
            mbPartIdx = 0;
        }
        else
        {
            mbPartIdx = 16 / Util264.MbPartWidth(mbType) * (yP / Util264.MbPartHeight(mbType)) + xP / Util264.MbPartWidth(mbType);
        }

        if (mbType is MacroblockKinds.P_8x8 or MacroblockKinds.P_8x8ref0 or MacroblockKinds.B_8x8 or MacroblockKinds.B_Skip or MacroblockKinds.B_Direct_16x16)
        {
            subMbPartIdx = 0;
        }
        else
        {
            subMbPartIdx = 8 / SubMbPartWidth(subMbType[mbPartIdx])
                         * (yP % 8 / SubMbPartHeight(subMbType[mbPartIdx]))
                         + xP % 8 / SubMbPartWidth(subMbType[mbPartIdx]);
        }
    }

    public static void DeriveNeighboringPartitions(
        DerivationContext dc,
        int mbPartIdx,
        int currSubMbType,
        int subMbPartIdx,
        int mbType,
        Span<int> mbTypeArray,
        Span<int> subMbType,
        ref int mbAddrA, ref int mbPartIdxA, ref int subMbPartIdxA, ref bool validA,
        ref int mbAddrB, ref int mbPartIdxB, ref int subMbPartIdxB, ref bool validB,
        ref int mbAddrC, ref int mbPartIdxC, ref int subMbPartIdxC, ref bool validC,
        ref int mbAddrD, ref int mbPartIdxD, ref int subMbPartIdxD, ref bool validD)
    {
        int x = 0;
        int y = 0;
        Util264.InverseMacroblockPartitionScan(mbPartIdx, mbType, ref x, ref y);

        int xS = 0;
        int yS = 0;
        if (mbType is MacroblockKinds.P_8x8 or MacroblockKinds.P_8x8ref0 or MacroblockKinds.B_8x8)
            Util264.InverseSubMacroblockPartitionScan(subMbPartIdx, subMbType, mbPartIdx, mbType, ref xS, ref yS);

        int predPartWidth = 0;
        if (mbType is MacroblockKinds.P_Skip or MacroblockKinds.B_Skip or MacroblockKinds.B_Direct_16x16)
        {
            predPartWidth = 16;
        }
        else if (mbType is MacroblockKinds.B_8x8)
        {
            if (currSubMbType == MacroblockKinds.B_Direct_8x8)
            {
                predPartWidth = 16;
            }
            else
            {
                predPartWidth = Util264.SubMbPartWidth(subMbType[mbPartIdx]);
            }
        }
        else if (mbType is MacroblockKinds.P_8x8 or MacroblockKinds.P_8x8ref0)
        {
            predPartWidth = Util264.SubMbPartWidth(subMbType[mbPartIdx]);
        }
        else
        {
            predPartWidth = Util264.MbPartWidth(mbType);
        }

        int xW = 0;
        int yW = 0;

        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, LumaLocationsA, mbTypeArray, subMbType, ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA, dc, mbType);
        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, LumaLocationsB, mbTypeArray, subMbType, ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB, dc, mbType);
        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, (predPartWidth, -1), mbTypeArray, subMbType, ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC, dc, mbType);
        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, LumaLocationsD, mbTypeArray, subMbType, ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD, dc, mbType);
    }

    private static void DeriveNeighboringPartition(int x, int y, int xS, int yS, out int xW, out int yW, (int x, int y) locations, Span<int> mbTypeArray, Span<int> subMbType, ref int mbAddrN, ref int mbPartIdxN, ref int subMbPartIdxN, ref bool validN, DerivationContext dc, int mbType)
    {
        int xNa = x + xS + locations.x;
        int yNa = y + yS + locations.y;

        DeriveNeighboringLocations(dc, true, xNa, yNa, out xW, out yW, ref dc.MbAddrX, ref mbAddrN, out validN);

        if (validN)
        {
            int mbTypeA = mbTypeArray[mbAddrN];
            int subMbTypeA = 0;
            if (mbTypeA is MacroblockKinds.P_8x8 or MacroblockKinds.P_8x8ref0 or MacroblockKinds.B_8x8)
                subMbTypeA = subMbType[mbAddrN];

            if (mbTypeA is MacroblockKinds.P_8x8 or MacroblockKinds.P_8x8ref0 or MacroblockKinds.B_8x8)
            {
                DeriveMacroblockAndSubMacroblockPartitionIndices(xW, yW, mbType, subMbType, ref mbPartIdxN, ref subMbPartIdxN);
            }
            else
            {
                Span<int> subMbTypeDISCARD = stackalloc int[16];
                int mbPartIdxDISCARD = 0;
                int subMbPartIdxDISCARD = 0;
                DeriveMacroblockAndSubMacroblockPartitionIndices(xW, yW, mbType, subMbTypeDISCARD, ref mbPartIdxDISCARD, ref subMbPartIdxDISCARD);
            }
        }
    }

    public static void DeriveNeighboringLocations(DerivationContext dc, bool neighboringLumaLocations, int xN, int yN, out int xW, out int yW, ref int mbAddrX, ref int mbAddrN, out bool valid)
    {
        if (dc.IsMbaff)
        {
            DeriveNeighboringLocationsMbaff(neighboringLumaLocations, !dc.IsMbaffFieldMacroblock, dc.CurrMbAddr, dc.MbAddrXFrameFlag, xN, yN, dc.NeighboringMacroblocks, dc.Sizes, out xW, out yW, ref mbAddrX, ref mbAddrN);
            valid = true;
            return;
        }

        DeriveNeighboringLocations(dc.Sizes, dc.CurrMbAddr, neighboringLumaLocations, dc.NeighboringMacroblocks, xN, yN, out xW, out yW, out mbAddrN, out valid);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 62
    public static void DeriveNeighboringLocations(
        MacroblockSizeChroma mbSize,
        int currMbAddr,
        bool neighboringLumaLocations,
        NeighboringMacroblocks macroblocks,
        int xN,
        int yN,
        out int xW,
        out int yW,
        out int mbAddrN,
        out bool mbAddrNValid)
    {
        int maxW = neighboringLumaLocations ? 16 : mbSize.Width;
        int maxH = neighboringLumaLocations ? 16 : mbSize.Height;

        if (xN == 0 && yN == 0)
        {
            mbAddrN = macroblocks.MbAddrD;
            mbAddrNValid = macroblocks.IsMbAddrDAvailable;
        }
        else if (xN == 0 && yN > 0 && yN <= maxH - 1)
        {
            mbAddrN = macroblocks.MbAddrA;
            mbAddrNValid = macroblocks.IsMbAddrAAvailable;
        }
        else if (xN >= 0 && xN <= maxW - 1 && yN < 0)
        {
            mbAddrN = macroblocks.MbAddrB;
            mbAddrNValid = macroblocks.IsMbAddrBAvailable;
        }
        else if (xN >= 0 && xN <= maxW - 1 && yN >= 0 && yN <= maxH - 1)
        {
            mbAddrN = currMbAddr;
            mbAddrNValid = true;
        }
        else if (xN > maxW - 1 && yN < 0)
        {
            mbAddrN = macroblocks.MbAddrC;
            mbAddrNValid = macroblocks.IsMbAddrCAvailable;
        }
        else if (xN > maxW - 1 && yN >= 0 && yN <= maxH - 1 || yN > maxH - 1)
        {
            // TODO: Maybe this is incorrect here?
            mbAddrN = 0;
            mbAddrNValid = false;
        }
        else
        {
            mbAddrN = 0;
            mbAddrNValid = false;
        }

        xW = (xN + maxW) % maxW;
        yW = (yN + maxH) % maxH;
    }

    public static void DeriveNeighboringLocationsMbaff(
        bool neighboringLumaLocations,
        bool isFrameMacroblock,
        int currMbAddr,
        bool mbAddrXFrameFlag,
        int xN,
        int yN,
        NeighboringMacroblocks macroblocks,
        MacroblockSizeChroma mbSize,
        out int xW,
        out int yW,
        ref int mbAddrX,
        ref int mbAddrN)
    {
        bool currMbFrameFlag = isFrameMacroblock;
        bool mbIsTopMbFlag = currMbAddr % 2 == 0;

        int maxW = neighboringLumaLocations ? 16 : mbSize.Width;
        int maxH = neighboringLumaLocations ? 16 : mbSize.Height;
        int yM = 0;

        if (xN < 0)
        {
            if (yN < 0)
            {
                if (currMbFrameFlag)
                {
                    if (mbIsTopMbFlag)
                    {
                        mbAddrX = macroblocks.MbAddrD;
                        mbAddrN = macroblocks.MbAddrD + 1;
                        yM = yN;
                    }
                    else
                    {
                        mbAddrX = macroblocks.MbAddrA;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrA;
                            yM = yN;
                        }
                        else
                        {
                            mbAddrN = macroblocks.MbAddrA + 1;
                            yM = yN + maxH >> 1;
                        }
                    }
                }
                else
                {
                    if (mbIsTopMbFlag)
                    {
                        mbAddrX = macroblocks.MbAddrD;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrD + 1;
                            yM = 2 * yN;
                        }
                        else
                        {
                            mbAddrN = macroblocks.MbAddrD;
                            yM = yN;
                        }
                    }
                    else
                    {
                        mbAddrX = macroblocks.MbAddrD;
                        mbAddrN = macroblocks.MbAddrD;
                        yM = yN;
                    }
                }
            }
            else if (yN >= 0 && yN <= maxH - 1)
            {
                if (currMbFrameFlag)
                {
                    if (mbIsTopMbFlag)
                    {
                        mbAddrX = macroblocks.MbAddrA;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrA;
                            yM = yN;
                        }
                        else
                        {
                            if (yN % 2 == 0)
                            {
                                mbAddrN = macroblocks.MbAddrA;
                                yM = yN >> 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrA + 1;
                                yM = yN >> 1;
                            }
                        }
                    }
                    else
                    {
                        mbAddrX = macroblocks.MbAddrA;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrA + 1;
                            yM = yN;
                        }
                        else
                        {
                            if (yN % 2 == 0)
                            {
                                mbAddrN = macroblocks.MbAddrA;
                                yM = yN >> 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrA + 1;
                                yM = yN >> 1;
                            }
                        }
                    }
                }
                else
                {
                    if (mbIsTopMbFlag)
                    {
                        mbAddrX = macroblocks.MbAddrA;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrA;
                            yM = yN;
                        }
                        else
                        {
                            if (yN < maxH / 2)
                            {
                                mbAddrN = macroblocks.MbAddrA;
                                yM = yN << 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrA + 1;
                                yM = (yN << 1) - maxH;
                            }
                        }
                    }
                    else
                    {
                        mbAddrX = macroblocks.MbAddrA;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrA + 1;
                            yM = yN;
                        }
                        else
                        {
                            if (yN < maxH / 2)
                            {
                                mbAddrN = macroblocks.MbAddrA;
                                yM = (yN << 1) + 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrA + 1;
                                yM = (yN << 1) + 1 - maxH;
                            }
                        }
                    }
                }
            }
            else
            {
                if (currMbFrameFlag)
                {
                    if (mbIsTopMbFlag)
                    {
                        mbAddrX = macroblocks.MbAddrB;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrB;
                            yM = yN - maxH;
                        }
                        else
                        {
                            if ((yN - maxH) % 2 == 0)
                            {
                                mbAddrN = macroblocks.MbAddrB;
                                yM = yN - maxH >> 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrB + 1;
                                yM = yN - maxH >> 1;
                            }
                        }
                    }
                    else
                    {
                        mbAddrX = macroblocks.MbAddrB;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrB + 1;
                            yM = yN - maxH;
                        }
                        else
                        {
                            if ((yN - maxH) % 2 == 0)
                            {
                                mbAddrN = macroblocks.MbAddrB;
                                yM = yN - maxH >> 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrB + 1;
                                yM = yN - maxH >> 1;
                            }
                        }
                    }
                }
                else
                {
                    if (mbIsTopMbFlag)
                    {
                        mbAddrX = macroblocks.MbAddrB;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrB + 1;
                            yM = 2 * (yN - maxH);
                        }
                        else
                        {
                            mbAddrN = macroblocks.MbAddrB;
                            yM = yN - maxH;
                        }
                    }
                    else
                    {
                        mbAddrX = macroblocks.MbAddrB;
                        if (mbAddrXFrameFlag)
                        {
                            mbAddrN = macroblocks.MbAddrB + 1;
                            yM = yN - maxH;
                        }
                        else
                        {
                            if (yN - maxH < maxH / 2)
                            {
                                mbAddrN = macroblocks.MbAddrB;
                                yM = (yN - maxH << 1) + 1;
                            }
                            else
                            {
                                mbAddrN = macroblocks.MbAddrB + 1;
                                yM = (yN - maxH << 1) + 1 - maxH;
                            }
                        }
                    }
                }
            }
        }

        xW = (xN + maxW) % maxW;
        yW = (yM + maxH) % maxH;
    }
}
