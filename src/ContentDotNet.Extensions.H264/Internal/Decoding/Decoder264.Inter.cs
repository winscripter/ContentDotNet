using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using ContentDotNet.Extensions.H264.Utilities;
using System.Runtime.CompilerServices;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class Decoder264
{
    private static readonly (int x, int y) LumaLocationsA = (-1, 0);
    private static readonly (int x, int y) LumaLocationsB = (0, -1);
    private static readonly (int x, int y) LumaLocationsD = (-1, -1);

    private MacroblockTypeHistory mbTypeArray;
    private MacroblockTypeHistory subMbTypeArray;
    private GeneralSliceType sliceType;
    private int mbType;
    private int subMbType;
    private int mbPartIdx;
    private int subMbPartIdx;

    // NOTE: Initialization of the following fields is performed in
    //       the InitializeInterPrediction method, which is invoked
    //       by the constructor. The constructor doesn't directly
    //       initialize them. Use null! to suppress CS8618 warnings.

    private int[] refIdxL0 = null!;
    private int[] refIdxL1 = null!;
    private ArrayMatrix4x4x2 mvL0 = null!;
    private ArrayMatrix4x4x2 mvL1 = null!;
    private ArrayMatrix4x4x2 mvCL0 = null!;
    private ArrayMatrix4x4x2 mvCL1 = null!;

    private void InitializeInterPrediction()
    {
        sliceType = GeneralSliceType.I; // Default
        mbTypeArray = new();
        subMbTypeArray = new();
        mbType = 0;
        subMbType = 0;
        mbPartIdx = 0;
        subMbPartIdx = 0;
        refIdxL0 = new int[16];
        refIdxL1 = new int[16];
        mvL0 = new ArrayMatrix4x4x2();
        mvL1 = new ArrayMatrix4x4x2();
        mvCL0 = new ArrayMatrix4x4x2();
        mvCL1 = new ArrayMatrix4x4x2();
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

            Util264.Inverse4x4ChromaScan(chroma4x4BlkIdx, out int x, out int y);
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

    public void DeriveMacroblockAndSubMacroblockPartitionIndices(int xP, int yP, int mbType, MacroblockTypeHistory subMbType, ref int mbPartIdx, ref int subMbPartIdx)
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

    public void DeriveNeighboringPartitions(
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
        Util264.InverseMacroblockPartitionScan(mbPartIdx, mbType, sliceType, ref x, ref y);

        int xS = 0;
        int yS = 0;
        if (mbType is P_8x8 or P_8x8ref0 or B_8x8)
            Util264.InverseSubMacroblockPartitionScan(subMbPartIdx, subMbType, mbPartIdx, mbType, sliceType, ref xS, ref yS);

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


        DeriveNeighboringPartition(x, y, xS, yS, out int xW, out int yW, LumaLocationsA, mbTypeArray, subMbType, ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA, dc, mbType);
        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, LumaLocationsB, mbTypeArray, subMbType, ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB, dc, mbType);
        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, (predPartWidth, -1), mbTypeArray, subMbType, ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC, dc, mbType);
        DeriveNeighboringPartition(x, y, xS, yS, out xW, out yW, LumaLocationsD, mbTypeArray, subMbType, ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD, dc, mbType);
    }

    private void DeriveNeighboringPartition(int x, int y, int xS, int yS, out int xW, out int yW, (int x, int y) locations, MacroblockTypeHistory mbTypeArray, MacroblockTypeHistory subMbType, ref int mbAddrN, ref int mbPartIdxN, ref int subMbPartIdxN, ref bool validN, DerivationContext dc, int mbType)
    {
        int xNa = x + xS + locations.x;
        int yNa = y + yS + locations.y;

        DeriveNeighboringLocations(dc, true, xNa, yNa, out xW, out yW, ref dc.MbAddrX, ref mbAddrN, out validN);

        if (validN)
        {
            int mbTypeA = mbTypeArray[mbAddrN];
            int subMbTypeA = 0;
            if (mbTypeA is P_8x8 or P_8x8ref0 or B_8x8)
                subMbTypeA = subMbType[mbAddrN];

            if (mbTypeA is P_8x8 or P_8x8ref0 or B_8x8)
            {
                DeriveMacroblockAndSubMacroblockPartitionIndices(xW, yW, mbType, subMbType, ref mbPartIdxN, ref subMbPartIdxN);
            }
            else
            {
                MacroblockTypeHistory subMbTypeDISCARD = new();
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

    public void InterPredict(Matrix16x16 predL, Matrix16x16 predCb, Matrix16x16 predCr)
    {
        int mbPartIdxMin = 0;
        int mbPartIdxMax = 0;

        if (mbType is B_Skip or B_Direct_16x16)
        {
            mbPartIdxMax = 3;
        }
        else
        {
            mbPartIdxMax = Util264.NumMbPart(mbType, sliceType) - 1;
        }
    }

    public void DeriveMotionVectors(
        int chromaArrayType,
        Span<int> refIdxL0, Span<int> refIdxL1, out bool predFlagL0, out bool predFlagL1, out int subMvCnt)
    {
        predFlagL0 = false;
        predFlagL1 = false;
        subMvCnt = 0;

        if (mbType == P_Skip)
        {
            DeriveLumaMotionVectorsForSkippedPAndSPSlices();
            return;
        }


    }

    private void DeriveLumaMotionVectorsForSkippedPAndSPSlices(out int refIdxL0)
    {
        refIdxL0 = 0; // Literally!

    }

    private void DeriveMotionDataOfNeighboringPartitions(
        int currSubMbType,
        bool listSuffixFlag,
        out int mbAddrA, out int mbPartIdxA, out int subMbPartIdxA, out bool validA,
        out int mbAddrB, out int mbPartIdxB, out int subMbPartIdxB, out bool validB,
        out int mbAddrC, out int mbPartIdxC, out int subMbPartIdxC, out bool validC,
        ref MotionVector mvL0N, ref MotionVector mvL1N, ref int refIdxL0N, ref int refIdxL1N)
    {
        mbAddrA = 0;
        mbAddrB = 0;
        mbAddrC = 0;
        int mbAddrD = 0;

        mbPartIdxA = 0;
        mbPartIdxB = 0;
        mbPartIdxC = 0;
        int mbPartIdxD = 0;

        subMbPartIdxA = 0;
        subMbPartIdxB = 0;
        subMbPartIdxC = 0;
        int subMbPartIdxD = 0;

        validA = false;
        validB = false;
        validC = false;
        bool validD = false;
        DeriveNeighboringPartitions(
            _derivationContext, mbPartIdx, currSubMbType, subMbPartIdx,
            mbType, mbTypeArray, subMbTypeArray,
            ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA,
            ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB,
            ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC,
            ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD
        );

        if (validC)
        {
            mbAddrC = mbAddrD;
            mbPartIdxC = mbPartIdxD;
            subMbPartIdxC = subMbPartIdxD;
        }

        if (listSuffixFlag)
            DeriveInternal(mbAddrA, mbPartIdxA, subMbPartIdxA, validA, ref mvL0N, ref refIdxL0N, mvL0, refIdxL0);
        else
            DeriveInternal(mbAddrA, mbPartIdxA, subMbPartIdxA, validA, ref mvL1N, ref refIdxL1N, mvL1, refIdxL1);

        if (listSuffixFlag)
            DeriveInternal(mbAddrB, mbPartIdxB, subMbPartIdxB, validB, ref mvL0N, ref refIdxL0N, mvL0, refIdxL0);
        else
            DeriveInternal(mbAddrB, mbPartIdxB, subMbPartIdxB, validB, ref mvL1N, ref refIdxL1N, mvL1, refIdxL1);

        if (listSuffixFlag)
            DeriveInternal(mbAddrC, mbPartIdxC, subMbPartIdxC, validC, ref mvL0N, ref refIdxL0N, mvL0, refIdxL0);
        else
            DeriveInternal(mbAddrC, mbPartIdxC, subMbPartIdxC, validC, ref mvL1N, ref refIdxL1N, mvL1, refIdxL1);

        if (listSuffixFlag)
            DeriveInternal(mbAddrD, mbPartIdxD, subMbPartIdxD, validD, ref mvL0N, ref refIdxL0N, mvL0, refIdxL0);
        else
            DeriveInternal(mbAddrD, mbPartIdxD, subMbPartIdxD, validD, ref mvL1N, ref refIdxL1N, mvL1, refIdxL1);

        void DeriveInternal(int mbAddrN, int mbPartIdxN, int subMbPartIdxN, bool validN, ref MotionVector mvLXN, ref int refIdxLXN, ArrayMatrix4x4x2 mvLx, int[] refIdxLX)
        {
            if (!validN || _macroblockUtility.IsCodedWithIntra(_derivationContext.CurrMbAddr))
            {
                mvLXN = (0, 0);
                refIdxLXN = -1;
                return;
            }

            mvLXN.X = mvLx[mbPartIdxN, subMbPartIdxN, 0];
            mvLXN.Y = mvLx[mbPartIdxN, subMbPartIdxN, 1];

            refIdxLXN = refIdxLX[mbPartIdxN];

            if (_macroblockUtility.IsFieldMacroblock(_derivationContext.CurrMbAddr) && _macroblockUtility.IsFrameMacroblock(mbAddrN))
            {
                mvLXN.Y /= 2;
                refIdxLXN *= 2;
            }
            else
            {
                mvLXN.Y *= 2;
                refIdxLXN /= 2;
            }
        }
    }

    //private void DeriveMotionVectors(Matrix16x16 mvL0, Matrix16x16 mvL1, Matrix16x16 mvCL0cb, Matrix16x16 mvCL1cb, Matrix16x16 mvCL0cr, Matrix16x16 mvCL1cr, out int refIdxL0, out int refIdxL1, out bool predFlagL0, out bool predFlagL1, out int subMvCnt, Span<int> refIdxL0Array, Span<int> refIdxL1Array)
    //{
    //    if (mbType == P_Skip)
    //    {
    //        DeriveLumaMotionVectorsForSkippedMacroblocksInPAndSP(mvL0, out refIdxL0);
    //        predFlagL0 = true;
    //        predFlagL1 = false;
    //        refIdxL1 = 0;
    //        subMvCnt = 1;

    //        return;
    //    }

    //    if (mbType is B_Skip or B_Direct_16x16 || subMbTypeArray[mbPartIdx] == B_Direct_8x8)
    //    {
    //        DeriveLumaMotionVectorsForBSkipAndBDirect16x16AndBDirect8x8(mbPartIdx, subMbPartIdx, mvL0, mvL1, out refIdxL0, out refIdxL1, out subMvCnt, out predFlagL0, out predFlagL1);
    //        return;
    //    }

    //    DeriveInternal(out predFlagL0, mvL0, out refIdxL0, Pred_L0, refIdxL0Array, sliceType);
    //    DeriveInternal(out predFlagL1, mvL1, out refIdxL1, Pred_L1, refIdxL1Array, sliceType);

    //    static void DeriveInternal(out bool predFlagLX, bool transformSize8x8Flag, Span<int> subMbTypeArray, int mbType, int mbPartIdx, Matrix16x16 mvLX, out int refIdxLX, int predLX, Span<int> refIdxLXArray, GeneralSliceType sliceType)
    //    {
    //        int partPredMode = Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType);
    //        int subMbPredMode = Util264.SubMbPredMode(subMbTypeArray[mbPartIdx]);

    //        if (mbPartIdx == predLX || mbPartIdx == BiPred || subMbPredMode == predLX || subMbPredMode == BiPred)
    //        {
    //            refIdxLX = refIdxLXArray[mbPartIdx];
    //            predFlagLX = true;
    //        }
    //        else
    //        {
    //            refIdxLX = -1;
    //            predFlagLX = false;
    //        }

    //        int currSubMbType = mbType is B_8x8 ? subMbTypeArray[mbPartIdx] : na;

    //        Span<int> mvpLx = stackalloc int[2];

    //    }
    //}
}
