using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Internal.Prediction;

internal static class IntraPrediction
{
    public static void Intra4x4LumaPredict(Matrix16x4x4 lumaSamples, Span<int> intra4x4PredMode, Span<int> intra8x8PredMode, Span<int> sl, Matrix16x16 predL)
    {
        for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
        {
            DeriveIntra4x4PredMode(luma4x4BlkIdx, intra4x4PredMode);
        }

        for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
        {
            Intra4x4LumaPredictCore(luma4x4BlkIdx, lumaSamples, intra4x4PredMode, intra8x8PredMode, sl, predL);
        }
    }

    private static void Intra4x4LumaPredictCore(int luma4x4BlkIdx, Matrix16x4x4 lumaSamples, Span<int> intra4x4PredMode, Span<int> intra8x8PredMode, Span<int> sl, Matrix16x16 predL)
    {
        Span<int> backing = stackalloc int[4 * 4];
        Matrix4x4 pred4x4L = new(backing);

        Intra4x4SamplePredict(luma4x4BlkIdx, sl, pred4x4L);

        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                lumaSamples[luma4x4BlkIdx, x, y] = pred4x4L[x, y];

        int xO = 0;
        int yO = 0;
        Util264.Inverse4x4LumaScan(luma4x4BlkIdx, ref xO, ref yO);

        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                predL[xO + x, yO + y] = pred4x4L[x, y];

        ConstructPicture(predL, luma4x4BlkIdx, sl);
    }

    private static void DeriveIntra4x4PredMode(
        int luma4x4BlkIdx,
        DerivationContext dc,
        bool constrainedIntraPredFlag,
        Span<int> intra4x4PredMode,
        Span<int> intra8x8PredMode,
        Span<int> remIntra4x4PredMode,
        Span<bool> prevIntra4x4PredModeFlag)
    {
        InterPrediction.Derive4x4LumaBlocks(luma4x4BlkIdx, dc, out int mbAddrA, out bool mbAddrAAvailable, out int luma4x4BlkIdxA, out bool luma4x4BlkIdxAAvailable, out int mbAddrB, out bool mbAddrBAvailable, out int luma4x4BlkIdxB, out bool luma4x4BlkIdxBAvailable);

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
        p[x == -1 ? y + 1 : x + 17] = value;
    }

    private static int PGet(Span<int> p, int x, int y)
    {
        return p[x == -1 ? y + 1 : x + 17];
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

            InterPrediction.DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out _);
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

    public static void DeriveIntra8x8PredMode(
        DerivationContext dc,
        bool constrainedIntraPredFlag,
        int luma8x8BlkIdx)
    {
        InterPrediction.Derive8x8LumaBlocks(
            dc,
            luma8x8BlkIdx,
            out int mbAddrA, out bool mbAddrAAvailable, out int luma8x8BlkIdxA, out bool luma8x8BlkIdxAAvailable,
            out int mbAddrB, out bool mbAddrBAvailable, out int luma8x8BlkIdxB, out bool luma8x8BlkIdxBAvailable);

        bool dcPredModePredictedFlag = !mbAddrAAvailable ||
                                       !mbAddrBAvailable ||
                                       mbAddrAAvailable && constrainedIntraPredFlag ||
                                       mbAddrBAvailable && constrainedIntraPredFlag;

        void DeriveInternal(int mbAddrN, bool mbAddrNAvailable, out int intraMxMPredModeN, int luma8x8BlkIdxN, Span<int> intra4x4PredMode)
        {

        }
    }
}
