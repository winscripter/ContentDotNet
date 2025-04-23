using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Internal.Abstractions;
using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class Decoder264
{
    private static void DeriveIntra4x4PredMode(
        int luma4x4BlkIdx,
        DerivationContext dc,
        bool constrainedIntraPredFlag,
        Span<int> intra4x4PredMode,
        Span<int> intra8x8PredMode,
        Span<int> remIntra4x4PredMode,
        Span<bool> prevIntra4x4PredModeFlag)
    {
        Derive4x4LumaBlocks(luma4x4BlkIdx, dc, out int mbAddrA, out bool mbAddrAAvailable, out int luma4x4BlkIdxA, out _, out int mbAddrB, out bool mbAddrBAvailable, out int luma4x4BlkIdxB, out _);

        bool dcPredModePredictedFlag = !mbAddrAAvailable || !mbAddrBAvailable
                                       || mbAddrAAvailable && constrainedIntraPredFlag
                                       || mbAddrBAvailable && constrainedIntraPredFlag;
        Intra4x4PredictionMode intraMxMPredModeA;
        if (dcPredModePredictedFlag || mbAddrA != MacroblockKinds.Intra_4x4 && mbAddrA != MacroblockKinds.Intra_8x8)
        {
            intraMxMPredModeA = Intra4x4PredictionMode.Dc;
        }
        else
        {
            if (mbAddrA == MacroblockKinds.Intra_4x4)
            {
                intraMxMPredModeA = (Intra4x4PredictionMode)intra4x4PredMode[luma4x4BlkIdxA];
            }
            else
            {
                intraMxMPredModeA = (Intra4x4PredictionMode)intra8x8PredMode[luma4x4BlkIdxA >> 2];
            }
        }

        Intra4x4PredictionMode intraMxMPredModeB;
        if (dcPredModePredictedFlag || mbAddrB != MacroblockKinds.Intra_4x4 && mbAddrB != MacroblockKinds.Intra_8x8)
        {
            intraMxMPredModeB = Intra4x4PredictionMode.Dc;
        }
        else
        {
            if (mbAddrB == MacroblockKinds.Intra_4x4)
            {
                intraMxMPredModeB = (Intra4x4PredictionMode)intra4x4PredMode[luma4x4BlkIdxB];
            }
            else
            {
                intraMxMPredModeB = (Intra4x4PredictionMode)intra8x8PredMode[luma4x4BlkIdxB >> 2];
            }
        }

        int predIntra4x4PredMode = Math.Min((byte)intraMxMPredModeA, (byte)intraMxMPredModeB);
        if (prevIntra4x4PredModeFlag[luma4x4BlkIdx])
            intra4x4PredMode[luma4x4BlkIdx] = predIntra4x4PredMode;
        else if (remIntra4x4PredMode[luma4x4BlkIdx] < predIntra4x4PredMode)
            intra4x4PredMode[luma4x4BlkIdx] = remIntra4x4PredMode[luma4x4BlkIdx];
        else
            intra4x4PredMode[luma4x4BlkIdx] = remIntra4x4PredMode[luma4x4BlkIdx] + 1;
    }

    // NOTE: This and FetchPredictionSamplesIntra4x4 was taken from https://github.com/zoltanmaric/h264-fer/blob/master/fer_h264/fer_h264/intra.cpp#L294
    private static readonly int[][] Intra4x4ScanOrder =
    [
        [0, 0],  [4,  0],  [0, 4],  [4,  4],
        [8, 0],  [12, 0],  [8, 4],  [12, 4],
        [0, 8],  [4,  8],  [0, 12], [4,  12],
        [8, 8],  [12, 8],  [8, 12], [12, 12]
    ];

    private static void FetchPredictionSamplesIntra4x4(int luma4x4BlkIdx, int CurrMbAddr, int PicWidthInMbs, Span<int> p, IStrideImageFactory factory)
    {
        int lW = factory.LWidth;

        int xP = CurrMbAddr % PicWidthInMbs << 4;
        int yP = CurrMbAddr / PicWidthInMbs << 4;

        int x0 = Intra4x4ScanOrder[luma4x4BlkIdx][0];
        int y0 = Intra4x4ScanOrder[luma4x4BlkIdx][1];

        int x = xP + x0;
        int y = yP + y0;

        int xF = x - 1;
        int yF = y - 1;
        int frameIdx = yF * lW + xF;
        if (xF < 0 || yF < 0)
        {
            p[0] = -1;
        }
        else
        {
            p[0] = factory.GetLuma(frameIdx);
        }

        xF = x - 1;
        yF = y;
        if (xF < 0)
        {
            for (int i = 1; i < 5; i++)
            {
                p[i] = -1;
            }
        }
        else
        {
            for (int i = 1; i < 5; i++)
            {
                frameIdx = yF * lW + xF;
                p[i] = factory.GetLuma(frameIdx);
                yF++;
            }
        }

        xF = x;
        yF = y - 1;
        if (yF < 0)
        {
            for (int i = 5; i < 13; i++)
            {
                p[i] = -1;
            }
        }
        else
        {
            for (int i = 5; i < 9; i++)
            {
                frameIdx = yF * lW + xF;
                p[i] = factory.GetLuma(frameIdx);
                xF++;
            }

            xF = x + 4;
            bool edgeSubMB = xF >= lW || x0 == 12 && y0 > 0;
            if (edgeSubMB == true || luma4x4BlkIdx == 3 || luma4x4BlkIdx == 11)
            {
                xF = x + 3;
                frameIdx = yF * lW + xF;
                for (int i = 9; i < 13; i++)
                {
                    p[i] = factory.GetLuma(frameIdx);
                }
            }
            else
            {
                for (int i = 9; i < 13; i++)
                {
                    frameIdx = yF * lW + xF;
                    p[i] = factory.GetLuma(frameIdx);
                    xF++;
                }
            }
        }
    }

    private static void PSet(Span<int> p, int x, int y, int value)
    {
        p[x == -1 ? y + 1 : x + 5] = value;
    }

    private static int PGet(Span<int> p, int x, int y)
    {
        return p[x == -1 ? y + 1 : x + 5];
    }

    public static void Intra4x4SamplePredict(
        DerivationContext dc,
        int luma4x4BlkIdx,
        bool mbAddrNAvailable,
        bool constrainedInterPredFlag,
        bool mbaffFrameAndMbIsField,
        int mbAddrN,
        bool mbaffFrameFlag,
        int pictureWidthInSamplesL,
        bool isFrame,
        bool isField,
        int bitDepthY,
        Span<int> p,
        Matrix16x16 cSL,
        Matrix4x4 predL,
        Span<int> availableForIntraPred,
        Span<int> intra4x4PredMode)
    {
        int xO = 0;
        int yO = 0;
        Util264.Inverse4x4LumaScan(luma4x4BlkIdx, ref xO, ref yO);

        for (int y = -1; y < 4; y++)
        {
            int x = -1;
            bool isAvailable = !(mbAddrNAvailable ||
                                 constrainedInterPredFlag ||
                                 x > 3 && luma4x4BlkIdx is 3 or 11);

            int xN = xO + x;
            int yN = yO + y;

            DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out _);
            Util264.Inverse4x4LumaScan(mbAddrN, ref xW, ref yW);

            if (isAvailable)
            {
                int xM = 0;
                int yM = 0;
                int xTemp = 0;
                int yTemp = 0;
                Util264.InverseMacroblockScan(mbAddrN, isFrame, isField, mbaffFrameFlag, pictureWidthInSamplesL, ref xTemp, ref yTemp, ref xM, ref yM);

                if (mbaffFrameAndMbIsField) PSet(p, x, y, cSL[xM + xW, yM + 2 * yW]);
                else PSet(p, x, y, cSL[xM + xW, yM + yW]);
            }
        }

        int type = intra4x4PredMode[luma4x4BlkIdx];
        if (type == 0)
        {
            // Vertical
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    predL[x, y] = PGet(p, x, -1);
        }
        else if (type == 1)
        {
            // Horizontal
            if (PGet(availableForIntraPred, -1, 0) == 1 ||
                PGet(availableForIntraPred, -1, 1) == 1 ||
                PGet(availableForIntraPred, -1, 2) == 1 ||
                PGet(availableForIntraPred, -1, 3) == 1)
            {
                for (int x = 0; x < 4; x++)
                    for (int y = 0; y < 4; y++)
                        predL[x, y] = PGet(p, -1, y);
            }
        }
        else if (type == 2)
        {
            // DC
            bool available = true;
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    if (PGet(availableForIntraPred, x, -1) != 1 && PGet(availableForIntraPred, -1, y) != 1)
                        available = false;

            if (available)
            {
                for (int x = 0; x < 4; x++)
                    for (int y = 0; y < 4; y++)
                        predL[x, y] = PGet(p, 0, -1) + PGet(p, 1, -1) + PGet(p, 2, -1) + PGet(p, 3, -1) +
                              PGet(p, -1, 0) + PGet(p, -1, 1) + PGet(p, -1, 2) + PGet(p, -1, 3) + 4 >> 3;
            }
            else
            {
                bool available2 = true;
                for (int x = 0; x < 4; x++)
                    if (PGet(availableForIntraPred, x, -1) != 1)
                        available2 = false;
                bool available3 = true;
                for (int y = 0; y < 4; y++)
                    if (PGet(availableForIntraPred, -1, y) != 1)
                        available3 = false;

                if (!available2 && available3)
                {
                    for (int x = 0; x < 4; x++)
                        for (int y = 0; y < 4; y++)
                            predL[x, y] = PGet(p, -1, 0) + PGet(p, -1, 1) + PGet(p, -1, 2) + PGet(p, -1, 3) + 2 >> 2;
                }
                else
                {
                    bool available4 = true;
                    bool available5 = true;
                    for (int y = 0; y < 4; y++)
                        if (PGet(availableForIntraPred, -1, y) != 1)
                            available4 = false;
                    for (int x = 0; x < 4; x++)
                        if (PGet(availableForIntraPred, x, -1) != 1)
                            available5 = false;

                    if (!available4 && available5)
                    {
                        for (int x = 0; x < 4; x++)
                            for (int y = 0; y < 4; y++)
                                predL[x, y] = PGet(p, 0, -1) + PGet(p, 1, -1) + PGet(p, 2, -1) + PGet(p, 3, -1) + 2 >> 2;
                    }
                    else
                    {
                        int template = 1 << bitDepthY - 1;
                        for (int x = 0; x < 4; x++)
                            for (int y = 0; y < 4; y++)
                                predL[x, y] = template;
                    }
                }
            }
        }
        else if (type == 3)
        {
            // Diagonal Down Left

            bool isApplicable = true;
            for (int x = 0; x < 8; x++)
                if (PGet(availableForIntraPred, x, -1) != 1)
                    isApplicable = false;

            if (isApplicable)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (x == 3 && y == 3)
                        {
                            predL[x, y] = PGet(p, 6, -1) + 3 * PGet(p, 7, -1) + 2 >> 2;
                        }
                        else
                        {
                            predL[x, y] = PGet(p, x + y, -1) + 2 * PGet(p, x + y + 1, -1) + PGet(p, x + y + 2, -1) + 2 >> 2;
                        }
                    }
                }
            }
        }
        else if (type == 4)
        {
            // Diagonal Down Right
            bool available = true;
            for (int x = 0; x < 4; x++)
                if (PGet(availableForIntraPred, x, -1) != 1)
                    available = false;
            for (int y = -1; y < 4; y++)
                if (PGet(availableForIntraPred, -1, y) != 1)
                    available = false;

            if (!available)
                return;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (x > y)
                    {
                        predL[x, y] = PGet(p, x - y - 2, -1) + 2 * PGet(p, x - y - 1, -1) + PGet(p, x - y, -1) + 2 >> 2;
                    }
                    else if (x < y)
                    {
                        predL[x, y] = PGet(p, -1, y - x - 2) + 2 * PGet(p, -1, y - x - 1) + PGet(p, -1, y - x) + 2 >> 2;
                    }
                    else // x == y
                    {
                        predL[x, y] = PGet(p, 0, -1) + 2 * PGet(p, -1, -1) + PGet(p, -1, 0) + 2 >> 2;
                    }
                }
            }
        }
        else if (type == 5)
        {
            // Vertical right
            bool available = true;
            for (int x = 0; x < 4; x++)
                if (PGet(availableForIntraPred, x, -1) != 1)
                    available = false;
            for (int y = -1; y < 4; y++)
                if (PGet(availableForIntraPred, -1, y) != 1)
                    available = false;

            if (!available)
                return;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int zVR = 2 * x - y;
                    if (zVR is 0 or 2 or 4 or 6)
                    {
                        predL[x, y] = PGet(p, x - (y >> 1) - 1, -1) + PGet(p, x - (y >> 1), -1) + 1 >> 1;
                    }
                    else if (zVR is 1 or 3 or 5)
                    {
                        predL[x, y] = PGet(p, x - (y >> 1) - 2, -1) + 2 * PGet(p, x - (y >> 1) - 1, -1) + PGet(p, x - (y >> 1), -1) + 2 >> 2;
                    }
                    else if (zVR == -1)
                    {
                        predL[x, y] = PGet(p, -1, 0) + 2 * PGet(p, -1, -1) + PGet(p, 0, -1) + 2 >> 2;
                    }
                    else
                    {
                        predL[x, y] = PGet(p, -1, y - 1) + 2 * PGet(p, -1, y - 2) + PGet(p, -1, y - 3) + 2 >> 2;
                    }
                }
            }
        }
        else if (type == 6)
        {
            // Horizontal down
            bool available = true;
            for (int x = 0; x < 4; x++)
                if (PGet(availableForIntraPred, x, -1) != 1)
                    available = false;
            for (int y = -1; y < 4; y++)
                if (PGet(availableForIntraPred, -1, y) != 1)
                    available = false;

            if (!available)
                return;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int zHD = 2 * y - x;
                    if (zHD is 0 or 2 or 4 or 6)
                    {
                        predL[x, y] = PGet(p, -1, y - (x >> 1) - 1) + PGet(p, -1, y - (x >> 1)) + 1 >> 1;
                    }
                    else if (zHD is 1 or 3 or 5)
                    {
                        predL[x, y] = PGet(p, -1, y - (x >> 1) - 2) + 2 * PGet(p, -1, y - (x >> 1) - 1) + PGet(p, -1, y - (x >> 1)) + 2 >> 2;
                    }
                    else if (zHD == -1)
                    {
                        predL[x, y] = PGet(p, -1, 0) + 2 * PGet(p, -1, -1) + PGet(p, 0, -1) + 2 >> 2;
                    }
                    else
                    {
                        predL[x, y] = PGet(p, x - 1, -1) + 2 * PGet(p, x - 2, -1) + PGet(p, x - 3, -1) + 2 >> 2;
                    }
                }
            }
        }
        else if (type == 7)
        {
            // Vertical left
            bool isApplicable = true;
            for (int x = 0; x < 8; x++)
                if (PGet(availableForIntraPred, x, -1) != 1)
                    isApplicable = false;

            if (!isApplicable)
                return;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (y is 0 or 2)
                    {
                        predL[x, y] = PGet(p, x + (y >> 1), -1) + PGet(p, x + (y >> 1) + 1, -1) + 1 >> 1;
                    }
                    else
                    {
                        predL[x, y] = PGet(p, x + (y >> 1), -1) + 2 * PGet(p, x + (y >> 1) + 1, -1) + PGet(p, x + (y >> 1) + 2, -1) + 2 >> 2;
                    }
                }
            }
        }
        else if (type == 8)
        {
            // Horizontal up
            bool isApplicable = true;
            for (int y = 0; y < 8; y++)
                if (PGet(availableForIntraPred, -1, y) != 1)
                    isApplicable = false;

            if (!isApplicable)
                return;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int zHU = x + 2 * y;
                    if (zHU is 0 or 2 or 4)
                    {
                        predL[x, y] = PGet(p, -1, y + (x >> 1)) + PGet(p, -1, y + (x >> 1) + 1) + 1 >> 1;
                    }
                    else if (zHU is 1 or 3)
                    {
                        predL[x, y] = PGet(p, -1, y + (x >> 1)) + 2 * PGet(p, -1, y + (x >> 1) + 1) + PGet(p, -1, y + (x >> 1) + 2) + 2 >> 2;
                    }
                    else if (zHU == 5)
                    {
                        predL[x, y] = PGet(p, -1, 2) + 3 * PGet(p, -1, 3) + 2 >> 2;
                    }
                    else
                    {
                        predL[x, y] = PGet(p, -1, 3);
                    }
                }
            }
        }
    }

    public void DeriveIntra8x8PredMode(
        DerivationContext dc,
        bool constrainedIntraPredFlag,
        Span<int> intra4x4PredMode,
        Span<int> intra8x8PredMode,
        Span<int> remIntra8x8PredMode,
        Span<bool> prevIntra8x8PredModeFlag,
        int luma8x8BlkIdx)
    {
        Derive8x8LumaBlocks(
            dc,
            luma8x8BlkIdx,
            out int mbAddrA, out bool mbAddrAAvailable, out int luma8x8BlkIdxA, out bool luma8x8BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int luma8x8BlkIdxB, out bool luma8x8BlkIdxBAvailable);

        bool dcPredModePredictedFlag = !mbAddrAAvailable ||
                                       !mbAddrBAvailable ||
                                       (mbAddrAAvailable && _macroblockUtility.IsCodedWithInter(mbAddrA) && constrainedIntraPredFlag) ||
                                       (mbAddrBAvailable && _macroblockUtility.IsCodedWithInter(mbAddrB) && constrainedIntraPredFlag);

        DeriveInternal(true, luma8x8BlkIdxA, mbAddrA, mbAddrAAvailable, out int intraMxMPredModeA, luma8x8BlkIdxA, intra4x4PredMode);
        DeriveInternal(false, luma8x8BlkIdxB, mbAddrB, mbAddrBAvailable, out int intraMxMPredModeB, luma8x8BlkIdxB, intra4x4PredMode);

        int predIntra8x8PredMode = Math.Min(intraMxMPredModeA, intraMxMPredModeB);
        if (prevIntra8x8PredModeFlag[luma8x8BlkIdx])
            intra8x8PredMode[luma8x8BlkIdx] = predIntra8x8PredMode;
        else if (remIntra8x8PredMode[luma8x8BlkIdx] < predIntra8x8PredMode)
            intra8x8PredMode[luma8x8BlkIdx] = remIntra8x8PredMode[luma8x8BlkIdx];
        else
            intra8x8PredMode[luma8x8BlkIdx] = remIntra8x8PredMode[luma8x8BlkIdx] + 1;

        void DeriveInternal(bool isA, int luma8x8BlkIdx, int mbAddrN, bool mbAddrNAvailable, out int intraMxMPredModeN, int luma8x8BlkIdxN, Span<int> intra4x4PredMode)
        {
            if (dcPredModePredictedFlag || !(_macroblockUtility.IsCodedWithIntra4x4(mbAddrN) || _macroblockUtility.IsCodedWithIntra8x8(mbAddrN)))
            {
                intraMxMPredModeN = 2; // DC
                return;
            }

            if (_macroblockUtility.IsCodedWithIntra8x8(mbAddrN))
            {
                Span<int> predMode = stackalloc int[16];
                _macroblockUtility.GetIntra8x8PredMode(mbAddrN, predMode);

                intraMxMPredModeN = predMode[luma8x8BlkIdxN];
            }
            else
            {
                int n;
                if (isA)
                {
                    if (dc.IsMbaff && !dc.IsMbaffFieldMacroblock && !_macroblockUtility.IsFrameMacroblock(mbAddrN) && luma8x8BlkIdx == 2)
                        n = 3;
                    else
                        n = 1;
                }
                else
                {
                    n = 2;
                }

                intraMxMPredModeN = intra4x4PredMode[luma8x8BlkIdxN * 4 + n];
            }
        }
    }

    public void Intra8x8SamplePredict(
        int luma8x8BlkIdx,
        Matrix16x16 cSL,
        Matrix8x8 predL,
        bool constrainedIntraPredFlag,
        Span<int> intra8x8PredMode,
        Span<int> p,
        DerivationContext dc)
    {
        int xO = 0;
        int yO = 0; // yo indeed!
        Util264.Inverse8x8LumaScan(luma8x8BlkIdx, ref xO, ref yO);

        Span<int> availability = stackalloc int[8 * 8];

        for (int y = -1; y < 8; y++)
            Core(-1, y, cSL, p, availability);

        for (int x = 0; x < 16; x++)
            Core(x, -1, cSL, p, availability);

        Span<int> pB = stackalloc int[8 * 8];

        for (int y = -1; y < 8; y++)
            Intra8x8SamplePredictionReferenceSampleFilter(-1, y, p, availability, pB);
        for (int x = 0; x < 16; x++)
            Intra8x8SamplePredictionReferenceSampleFilter(x, -1, p, availability, pB);

        int mode = intra8x8PredMode[luma8x8BlkIdx];
        if (mode == 0)
        {
            // Vertical
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 8))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        predL[x, y] = PGet(pB, x, -1);
                    }
                }
            }
        }
        else if (mode == 1)
        {
            // Horizontal
            if (SamplingUtils.YAllMarkedAvailable(availability, 0, 8))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        predL[x, y] = PGet(pB, -1, y);
                    }
                }
            }
        }
        else if (mode == 2)
        {
            // DC
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 8) &&
                SamplingUtils.YAllMarkedAvailable(availability, 0, 8))
            {
                int xSum = 0;
                int ySum = 0;
                for (int i = 0; i < 8; i++)
                    xSum += PGet(pB, i, -1);
                for (int i = 0; i < 8; i++)
                    ySum += PGet(pB, -1, i);

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        predL[x, y] = (xSum + ySum + 8) >> 4;
                    }
                }
            }
            else if (SamplingUtils.XAnyMarkedNotAvailable(availability, 0, 8) &&
                     SamplingUtils.YAllMarkedAvailable(availability, 0, 8))
            {
                int sum = 0;
                for (int i = 0; i < 8; i++)
                    sum += PGet(pB, -1, i);

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        predL[x, y] = (sum + 4) >> 3;
                    }
                }
            }
            else if (SamplingUtils.YAnyMarkedNotAvailable(availability, 0, 8) &&
                     SamplingUtils.XAllMarkedAvailable(availability, 0, 8))
            {
                int sum = 0;
                for (int i = 0; i < 8; i++)
                    sum += PGet(pB, i, -1);

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        predL[x, y] = (sum + 4) >> 3;
                    }
                }
            }
            else
            {
                int sum = 1 << (dc.BitDepthY - 1);
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        predL[x, y] = sum;
                    }
                }
            }
        }
        else if (mode == 3)
        {
            // Diagonal Down Left
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 16))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (x == 7 && y == 7)
                        {
                            predL[x, y] = (PGet(pB, 14, -1) + 3 * PGet(pB, 15, -1) + 2) >> 2;
                        }
                        else
                        {
                            predL[x, y] = (PGet(pB, x + y, -1) + 2 * PGet(pB, x + y + 1, -1) + PGet(pB, x + y + 2, -1) + 2) >> 2;
                        }
                    }
                }
            }
        }
        else if (mode == 4)
        {
            // Diagonal Down Right
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 8) &&
                SamplingUtils.YAllMarkedAvailable(availability, -1, 8))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (x > y)
                        {
                            predL[x, y] = (PGet(pB, x - y - 2, -1) + 2 * PGet(pB, x - y - 1, -1) + PGet(pB, x - y, -1) + 2 ) >> 2;
                        }
                        else if (x < y)
                        {
                            predL[x, y] = (PGet(pB,  -1, y - x - 2) + 2 * PGet(pB,  -1, y - x - 1) + PGet(pB,  -1, y - x) + 2 ) >> 2;
                        }
                        else // x == y
                        {
                            predL[x, y] = (PGet(pB, 0, -1) + 2 * PGet(pB,  -1, -1) + PGet(pB,  -1, 0) + 2 ) >> 2;
                        }
                    }
                }
            }
        }
        else if (mode == 5)
        {
            // Vertical Right
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 8) &&
                SamplingUtils.YAllMarkedAvailable(availability, -1, 8))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        int zVR = 2 * x - y;
                        if (zVR is 0 or 2 or 4 or 6 or 8 or 10 or 12 or 14)
                            predL[x, y] = (PGet(pB, x - (y >> 1) - 1, -1) + PGet(pB, x - (y >> 1), -1) + 1) >> 1;
                        else if (zVR is 1 or 3 or 5 or 7 or 9 or 11 or 13)
                            predL[x, y] = (PGet(pB, x - (y >> 1) - 2, -1) + 2 * PGet(pB, x - (y >> 1) - 1, -1) + PGet(pB, x - (y >> 1), -1) + 2) >> 2;
                        else if (zVR == -1)
                            predL[x, y] = (PGet(pB, -1, 0) + 2 * PGet(pB, -1, -1) + PGet(pB, 0, -1) + 2) >> 2;
                        else
                            predL[x, y] = (PGet(pB, -1, y - 2 * x - 1) + 2 * PGet(pB, -1, y - 2 * x - 2) + PGet(pB, -1, y - 2 * x - 3) + 2) >> 2;
                    }
                }
            }
        }
        else if (mode == 6)
        {
            // Horizontal Down
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 8) &&
                SamplingUtils.YAllMarkedAvailable(availability, -1, 8))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        int zHD = 2 * x - y;
                        if (zHD is 0 or 2 or 4 or 6 or 8 or 10 or 12 or 14)
                            predL[x, y] = (PGet(pB, -1, y - (x >> 1) - 1) + PGet(pB, -1, y - (x >> 1)) + 1) >> 1;
                        else if (zHD is 1 or 3 or 5 or 9 or 11 or 13)
                            predL[x, y] = (PGet(pB, -1, y - (x >> 1) - 2) + 2 * PGet(pB, -1, y - (x >> 1) - 1) + PGet(pB, -1, y - (x >> 1)) + 2) >> 2;
                        else if (zHD == -1)
                            predL[x, y] = (PGet(pB, -1, 0) + 2 * PGet(pB, -1, -1) + PGet(pB, 0, -1) + 2) >> 2;
                        else
                            predL[x, y] = (PGet(pB, x - 2 * y - 1, -1) + 2 * PGet(pB, x - 2 * y - 2, -1) + PGet(pB, x - 2 * y - 3, -1) + 2) >> 2;
                    }
                }
            }
        }
        else if (mode == 7)
        {
            // Vertical Left
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 16))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (y is 0 or 2 or 4 or 6)
                            predL[x, y] = (PGet(pB, x + (y >> 1), -1) + PGet(pB, x + (y >> 1) + 1, -1) + 1) >> 1;
                        else
                            predL[x, y] = (PGet(pB, x + (y >> 1), -1) + 2 * PGet(pB, x + (y >> 1) + 1, -1) + PGet(pB, x + (y >> 1) + 2, -1) + 2) >> 2;
                    }
                }
            }
        }
        else if (mode == 8)
        {
            // Horizontal Up
            if (SamplingUtils.XAllMarkedAvailable(availability, 0, 8))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        int zHU = x + 2 * y;
                        if (zHU is 0 or 2 or 4 or 6 or 8 or 10 or 12)
                            predL[x, y] = (PGet(pB, -1, y + (x >> 1)) + PGet(pB, -1, y + (x >> 1) + 1) + 1) >> 1;
                        else if (zHU is 1 or 3 or 5 or 7 or 9 or 11)
                            predL[x, y] = (PGet(pB, -1, y + (x >> 1)) + 2 * PGet(pB, -1, y + (x >> 1) + 1) + PGet(pB, -1, y + (x >> 1) + 2) + 2) >> 2;
                        else if (zHU == 13)
                            predL[x, y] = (PGet(pB, -1, 6) + 3 * PGet(pB, -1, 7) + 2 ) >> 2;
                        else
                            predL[x, y] = PGet(pB, -1, 7);
                    }
                }
            }
        }

        void Core(int x, int y, Matrix16x16 cSL, Span<int> p, Span<int> availability)
        {
            int xN = xO + x;
            int yN = yO + y;

            int mbAddrN = 0;
            DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out bool mbAddrNAvailable);

            for (int yInner = -1; yInner < 8; yInner++)
                Internal(-1, yInner, xW, yW, dc, _macroblockUtility, constrainedIntraPredFlag, availability, cSL, p, mbAddrN, mbAddrNAvailable);

            for (int xInner = 0; xInner < 16; xInner++)
                Internal(xInner, -1, xW, yW, dc, _macroblockUtility, constrainedIntraPredFlag, availability, cSL, p, mbAddrN, mbAddrNAvailable);
        }

        static void Internal(int x, int y, int xW, int yW, DerivationContext dc, IMacroblockUtility macroblockUtility, bool constrainedIntraPredFlag, Span<int> availability, Matrix16x16 cSL, Span<int> p, int mbAddrN, bool available)
        {
            bool isUnavailable = !available || (macroblockUtility.IsCodedWithInter(mbAddrN) && constrainedIntraPredFlag);
            PSet(availability, x, y, isUnavailable ? 0 : 1);

            int xM = 0;
            int yM = 0;

            if (!isUnavailable)
            {
                Util264.InverseMacroblockScan(mbAddrN, !dc.IsMbaffFieldMacroblock, dc.IsMbaffFieldMacroblock, dc.IsMbaff, dc.PictureWidthInSamplesL, ref x, ref y, ref xM, ref yM);
            
                if (dc.IsMbaff && !macroblockUtility.IsFrameMacroblock(mbAddrN))
                {
                    PSet(p, x, y, cSL[xM + xW, yM + 2 * yW]);
                }
                else
                {
                    PSet(p, x, y, cSL[yM + xW, yM + yW]);
                }
            }
        }
    }

    private static void Intra8x8SamplePredictionReferenceSampleFilter(int x, int y, Span<int> p, Span<int> availability, Span<int> pB /*p`, e.g. pB<acktick>*/)
    {
        if (SamplingUtils.XAllMarkedAvailable(availability, 0, 16))
        {
            if (PGet(availability, -1, -1) == 1)
                PSet(pB, 0, -1, (PGet(p, -1, -1) + 2 * PGet(p, 0, -1) + PGet(p, 1, -1) + 2) >> 2);
            else
                PSet(pB, 0, -1, (3 * PGet(p, 0, -1) + PGet(p, 1, -1) + 2) >> 2);

            PSet(pB, x, -1, (PGet(p, x - 1, -1) + 2 * PGet(p, x, -1) + PGet(p, x + 1, -1) + 2) >> 2);
            PSet(pB, 15, -1, (PGet(p, 14, -1) + 3 * PGet(p, 15, -1) + 2) >> 2);
        }
        
        if (PGet(availability, -1, -1) == 1)
        {
            if (PGet(availability, 0, -1) == 0 || PGet(availability, -1, 0) == 0)
            {
                if (PGet(availability, 0, -1) == 1)
                {
                    PSet(pB, -1, -1, (3 * PGet(p, -1, -1) + PGet(p, 0, -1) + 2) >> 2);
                }
                else if (PGet(availability, 0, -1) == 0 && PGet(availability, 0, -1) == 0)
                {
                    PSet(pB, -1, -1, (3 * PGet(p, -1, -1) + PGet(p, -1, 0) + 2) >> 2);
                }
                else
                {
                    PSet(pB, -1, -1, PGet(p, -1, -1));
                }
            }
            else
            {
                PSet(pB, -1, -1, (PGet(p, 0, -1) + 2 * PGet(p, -1, -1) + PGet(p, -1, 0) + 2) >> 2);
            }
        }

        if (SamplingUtils.YAllMarkedAvailable(availability, 0, 8))
        {
            if (PGet(availability, -1, -1) == 1)
            {
                PSet(pB, -1, 0, (PGet(p, -1, -1) + 2 * PGet(p, -1, 0) + PGet(p, -1, 1) + 2) >> 2);
            }
            else
            {
                PSet(pB, -1, 0, (3 * PGet(p, -1, 0) + PGet(p, -1, 1) + 2) >> 2);
            }

            for (int y2 = 1; y2 < 7; y2++)
            {
                PSet(pB, -1, y2, (PGet(p, -1, y2 - 1) + 2 * PGet(p, -1, y2) + PGet(p, -1, y2 + 1) + 2) >> 2);
            }

            PSet(pB, -1, 7, (PGet(p, -1, 6) + 3 * PGet(p, -1, 7) + 2) >> 2);
        }
    }
}
