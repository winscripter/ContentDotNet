using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Utilities;
using System.Runtime.CompilerServices;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class BaselineDecoder
{
    public static class Scanning
    {
        private static readonly (int x, int y) LumaLocationsA = (-1, 0);
        private static readonly (int x, int y) LumaLocationsB = (0, -1);
        private static readonly (int x, int y) LumaLocationsD = (-1, -1);

        public static void InverseMacroblockScan(int mbAddr, bool isFrame, bool isField, bool mbaffFrameFlag, int pictureWidthInSamplesL, ref int x, ref int y, ref int xO, ref int yO)
        {
            if (isFrame)
            {
                x = xO;
                y = yO + mbAddr % 2 * 16;
            }
            else if (isField)
            {
                x = xO;
                y = yO + mbAddr % 2;
            }
            else if (mbaffFrameFlag)
            {
                xO = Util264.InverseRasterScan(mbAddr / 2, 16, 32, pictureWidthInSamplesL, 0);
                yO = Util264.InverseRasterScan(mbAddr / 2, 16, 32, pictureWidthInSamplesL, 1);
            }
            else
            {
                x = Util264.InverseRasterScan(mbAddr, 16, 16, pictureWidthInSamplesL, 0);
                y = Util264.InverseRasterScan(mbAddr, 16, 16, pictureWidthInSamplesL, 1);
            }
        }

        public static void InverseSubMacroblockPartitionScan(int subMbPartIdx, MacroblockTypeHistory subMbType, int mbPartIdx, int mbType, GeneralSliceType sliceType, ref int x, ref int y)
        {
            if (mbType is SliceTypes.P_8x8 or SliceTypes.P_8x8ref0 or SliceTypes.B_8x8)
            {
                x = Util264.InverseRasterScan(subMbPartIdx, Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType), Util264.SubMbPartHeight(subMbType[mbPartIdx], sliceType), 8, 0);
                y = Util264.InverseRasterScan(subMbPartIdx, Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType), Util264.SubMbPartHeight(subMbType[mbPartIdx], sliceType), 8, 1);
            }
            else
            {
                x = Util264.InverseRasterScan(subMbPartIdx, 4, 4, 8, 0);
                y = Util264.InverseRasterScan(subMbPartIdx, 4, 4, 8, 1);
            }
        }

        public static void InverseMacroblockPartitionScan(int mbPartIdx, int mbType, GeneralSliceType sliceType, ref int x, ref int y)
        {
            x = Util264.InverseRasterScan(mbPartIdx, Util264.MbPartWidth(mbType, sliceType), Util264.MbPartHeight(mbType, sliceType), 16, 0);
            y = Util264.InverseRasterScan(mbPartIdx, Util264.MbPartWidth(mbType, sliceType), Util264.MbPartHeight(mbType, sliceType), 16, 1);
        }

        public static void Inverse4x4LumaScan(int luma4x4BlkIdx, ref int x, ref int y)
        {
            x = Util264.InverseRasterScan(luma4x4BlkIdx / 4, 8, 8, 16, 0) + Util264.InverseRasterScan(luma4x4BlkIdx % 4, 4, 4, 8, 0);
            y = Util264.InverseRasterScan(luma4x4BlkIdx / 4, 8, 8, 16, 1) + Util264.InverseRasterScan(luma4x4BlkIdx % 4, 4, 4, 8, 1);
        }

        public static void Inverse4x4CbCrScan(int cbcr4x4BlkIdx, ref int x, ref int y)
        {
            x = Util264.InverseRasterScan(cbcr4x4BlkIdx / 4, 8, 8, 16, 0) + Util264.InverseRasterScan(cbcr4x4BlkIdx % 4, 4, 4, 8, 0);
            y = Util264.InverseRasterScan(cbcr4x4BlkIdx / 4, 8, 8, 16, 1) + Util264.InverseRasterScan(cbcr4x4BlkIdx % 4, 4, 4, 8, 1);
        }

        public static void Inverse8x8LumaScan(int luma8x8BlkIdx, ref int x, ref int y)
        {
            x = Util264.InverseRasterScan(luma8x8BlkIdx, 8, 8, 16, 0);
            y = Util264.InverseRasterScan(luma8x8BlkIdx, 8, 8, 16, 1);
        }

        public static void Inverse8x8CbCrScan(int cbcr8x8BlkIdx, ref int x, ref int y)
        {
            x = Util264.InverseRasterScan(cbcr8x8BlkIdx, 8, 8, 16, 0);
            y = Util264.InverseRasterScan(cbcr8x8BlkIdx, 8, 8, 16, 1);
        }

        public static void Inverse4x4ChromaScan(int chroma4x4BlkIdx, out int x, out int y)
        {
            x = Util264.InverseRasterScan(chroma4x4BlkIdx, 4, 4, 8, 0);
            y = Util264.InverseRasterScan(chroma4x4BlkIdx, 4, 4, 8, 1);
        }

        public static void DeriveNeighboringMacroblockAddresses(int currMbAddr, int picWidthInMbs, out NeighboringMacroblocks macroblocks)
        {
            // "picture width in megabytes" hehehe
            // I'm sorry...

            macroblocks = new NeighboringMacroblocks();

            bool availableA = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrA = currMbAddr - 1, currMbAddr);
            bool availableB = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrB = currMbAddr - picWidthInMbs, currMbAddr);
            bool availableC = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrC = currMbAddr - picWidthInMbs + 1, currMbAddr);
            bool availableD = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrD = currMbAddr - picWidthInMbs - 1, currMbAddr);

            macroblocks.IsMbAddrAAvailable = availableA || currMbAddr % picWidthInMbs == 0;
            macroblocks.IsMbAddrBAvailable = availableB;
            macroblocks.IsMbAddrCAvailable = availableC || (currMbAddr + 1) % picWidthInMbs == 0;
            macroblocks.IsMbAddrDAvailable = availableD || currMbAddr % picWidthInMbs == 0;
        }

        public static void DeriveNeighboringMacroblockAddressesMbaff(int currMbAddr, int picWidthInMbs, out NeighboringMacroblocks macroblocks)
        {
            macroblocks = new NeighboringMacroblocks();

            bool availableA = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrA = 2 * (currMbAddr / 2 - 1), currMbAddr);
            bool availableB = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrB = 2 * (currMbAddr / 2 - picWidthInMbs), currMbAddr);
            bool availableC = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrC = 2 * (currMbAddr / 2 - picWidthInMbs + 1), currMbAddr);
            bool availableD = Util264.IsMacroblockAddressAvailable(macroblocks.MbAddrD = 2 * (currMbAddr / 2 - picWidthInMbs - 1), currMbAddr);

            macroblocks.IsMbAddrAAvailable = availableA || currMbAddr / 2 % picWidthInMbs == 0;
            macroblocks.IsMbAddrBAvailable = availableB;
            macroblocks.IsMbAddrCAvailable = availableC || (currMbAddr / 2 + 1) % picWidthInMbs == 0;
            macroblocks.IsMbAddrDAvailable = availableD || currMbAddr / 2 % picWidthInMbs == 0;
        }

        public static void DeriveNeighboringMacroblockAddresses(int currMbAddr, int picWidthInMbs, bool isMbaff, out NeighboringMacroblocks macroblocks)
        {
            if (isMbaff) DeriveNeighboringMacroblockAddresses(currMbAddr, picWidthInMbs, out macroblocks);
            else DeriveNeighboringMacroblockAddressesMbaff(currMbAddr, picWidthInMbs, out macroblocks);
        }

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
                Inverse4x4LumaScan(luma4x4BlkIdx, ref x, ref y);

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

        public static void Derive4x4CbBlocks(
            int cb4x4BlkIdx,
            DerivationContext dc,
            out int mbAddrA, out bool mbAddrAAvailable, out int cb4x4BlkIdxA, out bool cb4x4BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int cb4x4BlkIdxB, out bool cb4x4BlkIdxBAvailable)
            => Derive4x4LumaBlocks(cb4x4BlkIdx, dc, out mbAddrA, out mbAddrAAvailable, out cb4x4BlkIdxA, out cb4x4BlkIdxAAvailable,
                                                    out mbAddrB, out mbAddrBAvailable, out cb4x4BlkIdxB, out cb4x4BlkIdxBAvailable);

        public static void Derive4x4CrBlocks(
            int cr4x4BlkIdx,
            DerivationContext dc,
            out int mbAddrA, out bool mbAddrAAvailable, out int cr4x4BlkIdxA, out bool cr4x4BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int cr4x4BlkIdxB, out bool cr4x4BlkIdxBAvailable)
            => Derive4x4LumaBlocks(cr4x4BlkIdx, dc, out mbAddrA, out mbAddrAAvailable, out cr4x4BlkIdxA, out cr4x4BlkIdxAAvailable,
                                                    out mbAddrB, out mbAddrBAvailable, out cr4x4BlkIdxB, out cr4x4BlkIdxBAvailable);

        public static void Derive4x4ChromaBlocks(
            DerivationContext dc,
            int chroma4x4BlkIdx,
            out int mbAddrA, out bool mbAddrAAvailable, out int chroma4x4BlkIdxA, out bool chroma4x4BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int chroma4x4BlkIdxB, out bool chroma4x4BlkIdxBAvailable)
        {
            InternalDerive(LumaLocationsA, chroma4x4BlkIdx, out mbAddrA, out chroma4x4BlkIdxA, out chroma4x4BlkIdxAAvailable, dc, out mbAddrAAvailable);
            InternalDerive(LumaLocationsB, chroma4x4BlkIdx, out mbAddrB, out chroma4x4BlkIdxB, out chroma4x4BlkIdxBAvailable, dc, out mbAddrBAvailable);

            static void InternalDerive((int x, int y) locations, int chroma4x4BlkIdx, out int mbAddrN, out int chroma4x4BlkIdxN, out bool chroma4x4BlkIdxNAvailable, DerivationContext dc, out bool valid)
            {
                mbAddrN = 0;

                Inverse4x4ChromaScan(chroma4x4BlkIdx, out int x, out int y);
                int xN = x + locations.x;
                int yN = y + locations.y;

                DeriveNeighboringLocations(dc, false, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out valid);

                if (!valid)
                {
                    chroma4x4BlkIdxN = 0;
                    chroma4x4BlkIdxNAvailable = false;
                }
                else
                {
                    Derive4x4ChromaBlockIndices(xW, yW, out chroma4x4BlkIdxN);
                    chroma4x4BlkIdxNAvailable = true;
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

        public static void DeriveMacroblockAndSubMacroblockPartitionIndices(GeneralSliceType sliceType, int xP, int yP, int mbType, MacroblockTypeHistory subMbType, ref int mbPartIdx, ref int subMbPartIdx)
        {
            if (IsI(mbType))
            {
                mbPartIdx = 0;
            }
            else
            {
                mbPartIdx = 16 / Util264.MbPartWidth(mbType, sliceType) * (yP / Util264.MbPartHeight(mbType, sliceType)) + xP / Util264.MbPartWidth(mbType, sliceType);
            }

            if (mbType is P_8x8 or P_8x8ref0 or B_8x8 or B_Skip or B_Direct_16x16)
            {
                subMbPartIdx = 0;
            }
            else
            {
                subMbPartIdx = 8 / Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType)
                             * (yP % 8 / Util264.SubMbPartHeight(subMbType[mbPartIdx], sliceType))
                             + xP % 8 / Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType);
            }
        }

        public static void Derive8x8CrBlocks(
            DerivationContext dc,
            int cr8x8BlkIdx,
            out int mbAddrA, out bool mbAddrAAvailable, out int cr8x8BlkIdxA, out bool cr8x8BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int cr8x8BlkIdxB, out bool cr8x8BlkIdxBAvailable)
            => Derive8x8LumaBlocks(dc, cr8x8BlkIdx, out mbAddrA, out mbAddrAAvailable, out cr8x8BlkIdxA, out cr8x8BlkIdxAAvailable,
                                                    out mbAddrB, out mbAddrBAvailable, out cr8x8BlkIdxB, out cr8x8BlkIdxBAvailable);

        public static void Derive8x8CbBlocks(
            DerivationContext dc,
            int cb8x8BlkIdx,
            out int mbAddrA, out bool mbAddrAAvailable, out int cb8x8BlkIdxA, out bool cb8x8BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int cb8x8BlkIdxB, out bool cb8x8BlkIdxBAvailable)
            => Derive8x8LumaBlocks(dc, cb8x8BlkIdx, out mbAddrA, out mbAddrAAvailable, out cb8x8BlkIdxA, out cb8x8BlkIdxAAvailable,
                                                    out mbAddrB, out mbAddrBAvailable, out cb8x8BlkIdxB, out cb8x8BlkIdxBAvailable);

        public static void DeriveNeighboringPartitions(
            GeneralSliceType sliceType,
            DerivationContext dc,
            int mbPartIdx,
            int currSubMbType,
            int subMbPartIdx,
            int mbType,
            MacroblockTypeHistory mbTypeArray,
            MacroblockTypeHistory subMbType,
            ref int mbAddrA, ref int mbPartIdxA, ref int subMbPartIdxA, ref bool validA,
            ref int mbAddrB, ref int mbPartIdxB, ref int subMbPartIdxB, ref bool validB,
            ref int mbAddrC, ref int mbPartIdxC, ref int subMbPartIdxC, ref bool validC,
            ref int mbAddrD, ref int mbPartIdxD, ref int subMbPartIdxD, ref bool validD)
        {
            int x = 0;
            int y = 0;
            InverseMacroblockPartitionScan(mbPartIdx, mbType, sliceType, ref x, ref y);

            int xS = 0;
            int yS = 0;
            if (mbType is P_8x8 or P_8x8ref0 or B_8x8)
                InverseSubMacroblockPartitionScan(subMbPartIdx, subMbType, mbPartIdx, mbType, sliceType, ref xS, ref yS);

            int predPartWidth;
            if (mbType is P_Skip or B_Skip or B_Direct_16x16)
            {
                predPartWidth = 16;
            }
            else if (mbType is B_8x8)
            {
                if (currSubMbType == B_Direct_8x8)
                {
                    predPartWidth = 16;
                }
                else
                {
                    predPartWidth = Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType);
                }
            }
            else if (mbType is P_8x8 or P_8x8ref0)
            {
                predPartWidth = Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType);
            }
            else
            {
                predPartWidth = Util264.MbPartWidth(mbType, sliceType);
            }


            DeriveNeighboringPartition(sliceType, x, y, xS, yS, out int xW, out int yW, LumaLocationsA, mbTypeArray, subMbType, ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA, dc, mbType);
            DeriveNeighboringPartition(sliceType, x, y, xS, yS, out xW, out yW, LumaLocationsB, mbTypeArray, subMbType, ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB, dc, mbType);
            DeriveNeighboringPartition(sliceType, x, y, xS, yS, out xW, out yW, (predPartWidth, -1), mbTypeArray, subMbType, ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC, dc, mbType);
            DeriveNeighboringPartition(sliceType, x, y, xS, yS, out xW, out yW, LumaLocationsD, mbTypeArray, subMbType, ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD, dc, mbType);
        }

        private static void DeriveNeighboringPartition(GeneralSliceType sliceType, int x, int y, int xS, int yS, out int xW, out int yW, (int x, int y) locations, MacroblockTypeHistory mbTypeArray, MacroblockTypeHistory subMbType, ref int mbAddrN, ref int mbPartIdxN, ref int subMbPartIdxN, ref bool validN, DerivationContext dc, int mbType)
        {
            int xNa = x + xS + locations.x;
            int yNa = y + yS + locations.y;

            DeriveNeighboringLocations(dc, true, xNa, yNa, out xW, out yW, ref dc.MbAddrX, ref mbAddrN, out validN);

            if (validN)
            {
                int mbTypeA = mbTypeArray[mbAddrN];
                int subMbTypeA;
                if (mbTypeA is P_8x8 or P_8x8ref0 or B_8x8)
                    subMbTypeA = subMbType[mbAddrN];

                if (mbTypeA is P_8x8 or P_8x8ref0 or B_8x8)
                {
                    DeriveMacroblockAndSubMacroblockPartitionIndices(sliceType, xW, yW, mbType, subMbType, ref mbPartIdxN, ref subMbPartIdxN);
                }
                else
                {
                    MacroblockTypeHistory subMbTypeDISCARD = new();
                    int mbPartIdxDISCARD = 0;
                    int subMbPartIdxDISCARD = 0;
                    DeriveMacroblockAndSubMacroblockPartitionIndices(sliceType, xW, yW, mbType, subMbTypeDISCARD, ref mbPartIdxDISCARD, ref subMbPartIdxDISCARD);
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
}
