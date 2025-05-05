using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class BaselineDecoder
{
    public sealed class Intra
    {
        private readonly IMacroblockUtility _macroblockUtility;

        public Intra(IMacroblockUtility macroblockUtility) => _macroblockUtility = macroblockUtility;

        private static void DeriveIntra4x4PredMode(
            int luma4x4BlkIdx,
            DerivationContext dc,
            bool constrainedIntraPredFlag,
            Span<int> intra4x4PredMode,
            Span<int> intra8x8PredMode,
            Span<int> remIntra4x4PredMode,
            Span<bool> prevIntra4x4PredModeFlag)
        {
            Inter.Derive4x4LumaBlocks(luma4x4BlkIdx, dc, out int mbAddrA, out bool mbAddrAAvailable, out int luma4x4BlkIdxA, out _, out int mbAddrB, out bool mbAddrBAvailable, out int luma4x4BlkIdxB, out _);

            bool dcPredModePredictedFlag = !mbAddrAAvailable || !mbAddrBAvailable
                                           || mbAddrAAvailable && constrainedIntraPredFlag
                                           || mbAddrBAvailable && constrainedIntraPredFlag;
            Intra4x4PredictionMode intraMxMPredModeA;
            if (dcPredModePredictedFlag || mbAddrA != SliceTypes.Intra_4x4 && mbAddrA != SliceTypes.Intra_8x8)
            {
                intraMxMPredModeA = Intra4x4PredictionMode.Dc;
            }
            else
            {
                if (mbAddrA == SliceTypes.Intra_4x4)
                {
                    intraMxMPredModeA = (Intra4x4PredictionMode)intra4x4PredMode[luma4x4BlkIdxA];
                }
                else
                {
                    intraMxMPredModeA = (Intra4x4PredictionMode)intra8x8PredMode[luma4x4BlkIdxA >> 2];
                }
            }

            Intra4x4PredictionMode intraMxMPredModeB;
            if (dcPredModePredictedFlag || mbAddrB != SliceTypes.Intra_4x4 && mbAddrB != SliceTypes.Intra_8x8)
            {
                intraMxMPredModeB = Intra4x4PredictionMode.Dc;
            }
            else
            {
                if (mbAddrB == SliceTypes.Intra_4x4)
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
            bool available,
            int mbAddrN,
            bool constrainedInterPredFlag,
            Span<int> p,
            Matrix16x16 cSL,
            Matrix4x4 predL,
            Span<int> availableForIntraPred,
            Span<int> intra4x4PredMode,
            IMacroblockUtility util) =>
            Intra4x4SamplePredict(
                dc,
                luma4x4BlkIdx,
                available,
                constrainedInterPredFlag,
                dc.IsMbaff && util.IsFieldMacroblock(mbAddrN),
                mbAddrN,
                dc.IsMbaff,
                dc.PictureWidthInSamplesL,
                dc.IsMbaffFieldMacroblock,
                dc.IsMbaffFieldMacroblock,
                dc.BitDepthY,
                p,
                cSL,
                predL,
                availableForIntraPred,
                intra4x4PredMode);

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

                Inter.DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out _);
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
            Inter.Derive8x8LumaBlocks(
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
                                predL[x, y] = (PGet(pB, x - y - 2, -1) + 2 * PGet(pB, x - y - 1, -1) + PGet(pB, x - y, -1) + 2) >> 2;
                            }
                            else if (x < y)
                            {
                                predL[x, y] = (PGet(pB, -1, y - x - 2) + 2 * PGet(pB, -1, y - x - 1) + PGet(pB, -1, y - x) + 2) >> 2;
                            }
                            else // x == y
                            {
                                predL[x, y] = (PGet(pB, 0, -1) + 2 * PGet(pB, -1, -1) + PGet(pB, -1, 0) + 2) >> 2;
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
                                predL[x, y] = (PGet(pB, -1, 6) + 3 * PGet(pB, -1, 7) + 2) >> 2;
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
                Inter.DeriveNeighboringLocations(dc, true, xN, yN, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out bool mbAddrNAvailable);

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

        public void Intra16x16SamplePredict(
            int luma8x8BlkIdx,
            Matrix16x16 cSL,
            Matrix16x16 predL,
            bool constrainedIntraPredFlag,
            int intra16x16PredMode,
            Span<int> p,
            DerivationContext dc)
        {
            Span<int> availability = stackalloc int[16];

            for (int y = -1; y < 16; y++) Core(-1, y, dc, p, availability, cSL, constrainedIntraPredFlag, _macroblockUtility);
            for (int x = 0; x < 16; x++) Core(x, -1, dc, p, availability, cSL, constrainedIntraPredFlag, _macroblockUtility);

            if (intra16x16PredMode == 0)
            {
                // Vertical
                if (SamplingUtils.XAllMarkedAvailable(availability, 0, 16))
                {
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            predL[x, y] = PGet(p, x, -1);
                        }
                    }
                }
            }
            else if (intra16x16PredMode == 1)
            {
                // Horizontal
                if (SamplingUtils.YAllMarkedAvailable(availability, 0, 16))
                {
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            predL[x, y] = PGet(p, -1, y);
                        }
                    }
                }
            }
            else if (intra16x16PredMode == 2)
            {
                // DC
                if (SamplingUtils.AllMarkedAvailable(availability, 0, 15, 0, 15))
                {
                    int xc = 0;
                    int yc = 0;

                    for (int i = 0; i < 16; i++) xc += PGet(p, i, -1);
                    for (int i = 0; i < 16; i++) yc += PGet(p, -1, i);

                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            predL[x, y] = (xc + yc + 16) >> 5;
                        }
                    }
                }
                else if (SamplingUtils.XAllMarkedNotAvailable(availability, 0, 15) &&
                         SamplingUtils.YAllMarkedAvailable(availability, 0, 15))
                {
                    int yc = 0;
                    for (int i = 0; i < 16; i++) yc += PGet(p, -1, i);

                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            predL[x, y] = (yc + 8) >> 4;
                        }
                    }
                }
                else if (SamplingUtils.YAllMarkedNotAvailable(availability, 0, 15) &&
                         SamplingUtils.XAllMarkedAvailable(availability, 0, 15))
                {
                    int xc = 0;
                    for (int i = 0; i < 16; i++) xc += PGet(p, i, -1);

                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            predL[x, y] = (xc + 8) >> 4;
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            predL[x, y] = 1 << (dc.BitDepthY - 1);
                        }
                    }
                }
            }
            else if (intra16x16PredMode == 3)
            {
                // Plane

                int h = 0;
                int v = 0;

                for (int i = 0; i < 8; i++) h += (i + 1) * (PGet(p, 8 + i, -1) - PGet(p, 6 - i, -1));
                for (int i = 0; i < 8; i++) v += (i + 1) * (PGet(p, -1, 8 + i) - PGet(p, -1, 6 - i));

                int a = 16 * (PGet(p, -1, 15) + PGet(p, 15, -1));
                int b = (5 * h + 32) >> 6;
                int c = (5 * v + 32) >> 6;

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        predL[x, y] = Util264.Clip1Y((a + b * (x - 7) + c * (y - 7) + 16) >> 5, dc.BitDepthY);
                    }
                }
            }

            static void Core(int x, int y, DerivationContext dc, Span<int> p, Span<int> availability, Matrix16x16 cSL, bool constrainedIntraPredFlag, IMacroblockUtility macroblockUtility)
            {
                int mbAddrN = 0;
                Inter.DeriveNeighboringLocations(dc, true, x, y, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out bool valid);

                for (int yInner = -1; yInner < 16; yInner++) Internal(-1, yInner, xW, yW, p, cSL, valid, macroblockUtility, mbAddrN, dc, constrainedIntraPredFlag, availability);
                for (int xInner = 0; xInner < 16; xInner++) Internal(xInner, -1, xW, yW, p, cSL, valid, macroblockUtility, mbAddrN, dc, constrainedIntraPredFlag, availability);

                static void Internal(int x, int y, int xW, int yW, Span<int> p, Matrix16x16 cSL, bool mbAddrValid, IMacroblockUtility macroblockUtility, int mbAddrN, DerivationContext dc, bool constrainedIntraPredFlag, Span<int> availability)
                {
                    bool isUnavailable = !mbAddrValid ||
                                         (macroblockUtility.IsCodedWithInter(mbAddrN) && constrainedIntraPredFlag) ||
                                         (macroblockUtility.IsMacroblockOfTypeSi(mbAddrN) && constrainedIntraPredFlag);

                    if (isUnavailable)
                    {
                        PSet(availability, x, y, 0);
                    }
                    else
                    {
                        PSet(availability, x, y, 1);

                        int xM = 0;
                        int yM = 0;
                        Util264.InverseMacroblockScan(mbAddrN, !dc.IsMbaffFieldMacroblock, dc.IsMbaffFieldMacroblock, dc.IsMbaff, dc.PictureWidthInSamplesL, ref x, ref y, ref xM, ref yM);

                        if (dc.IsMbaff && !macroblockUtility.IsFrameMacroblock(mbAddrN))
                            PSet(p, x, y, cSL[xM + xW, yM + 2 * yW]);
                        else
                            PSet(p, x, y, cSL[xM + xW, yM + yW]);
                    }
                }
            }
        }

        public void IntraChromaSamplePredict(
            Matrix16x16 cSC,
            Matrix16x16 predC,
            MacroblockSizeChroma sizes,
            Span<int> p,
            DerivationContext dc,
            bool constrainedIntraPredFlag,
            int intraChromaPredMode,
            int chromaArrayType)
        {
            if (chromaArrayType == 3u)
            {
                IntraChromaSamplePredictChromaArrayType3(cSC, predC, _macroblockUtility, dc, p, chromaArrayType);
                return;
            }

            Span<int> availability = stackalloc int[16 * 16];
            availability.Fill(1);

            bool isSI = _macroblockUtility.IsMacroblockOfTypeSi(dc.CurrMbAddr);

            for (int y = -1; y < sizes.Height; y++)
            {
                Core(-1, y, availability, p, cSC, dc, sizes, _macroblockUtility, constrainedIntraPredFlag, isSI);
            }

            for (int x = 0; x < sizes.Width; x++)
            {
                Core(x, -1, availability, p, cSC, dc, sizes, _macroblockUtility, constrainedIntraPredFlag, isSI);
            }

            if (intraChromaPredMode == 0)
            {
                // DC
                for (int chroma4x4BlkIdx = 0; chroma4x4BlkIdx < (1 << (chromaArrayType + 1)) - 1; chroma4x4BlkIdx++)
                {
                    Util264.Inverse4x4ChromaScan(chroma4x4BlkIdx, out int xO, out int yO);
                    if ((xO == 0 && yO == 0) || (xO > 0 && yO > 0))
                    {
                        if (SamplingUtils.AllMarkedAvailable(availability, xO, xO + 3, yO, yO + 3) &&
                            SamplingUtils.AllMarkedAvailable(availability, xO, xO + 3, yO, yO + 3))
                        {
                            int sumXCb = 0;
                            int sumYCb = 0;
                            for (int tempX = 0; tempX < 4; tempX++) sumXCb += PGet(p, tempX + xO, -1);
                            for (int tempY = 0; tempY < 4; tempY++) sumYCb += PGet(p, -1, tempY + yO);

                            int sumXCr = 0;
                            int sumYCr = 0;
                            for (int tempX = 0; tempX < 4; tempX++) sumXCr += PGet(p, tempX + xO, -1);
                            for (int tempY = 0; tempY < 4; tempY++) sumYCr += PGet(p, -1, tempY + yO);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sumXCb + sumYCb + 4) >> 3;
                                }
                            }
                        }
                        else if (SamplingUtils.XAnyMarkedNotAvailable(availability, xO, xO + 3) &&
                                 SamplingUtils.YAllMarkedAvailable(availability, yO, yO + 3))
                        {
                            int sum = 0;
                            for (int y = 0; y < 4; y++) sum += PGet(p, -1, y + yO);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sum + 2) >> 2;
                                }
                            }
                        }
                        else if (SamplingUtils.YAnyMarkedNotAvailable(availability, xO, xO + 3) &&
                                 SamplingUtils.XAllMarkedAvailable(availability, yO, yO + 3))
                        {
                            int sum = 0;
                            for (int x = 0; x < 4; x++) sum += PGet(p, x + xO, -1);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sum + 2) >> 2;
                                }
                            }
                        }
                        else
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = 1 << (dc.BitDepthC - 1);
                                }
                            }
                        }
                    }
                    else if (xO > 0 && yO == 0)
                    {
                        if (SamplingUtils.XAllMarkedAvailable(availability, xO, xO + 3))
                        {
                            int sum = 0;
                            for (int x = 0; x < 4; x++) sum += PGet(p, x + xO, -1);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sum + 2) >> 2;
                                }
                            }
                        }
                        else if (SamplingUtils.YAllMarkedAvailable(availability, yO, yO + 3))
                        {
                            int sum = 0;
                            for (int y = 0; y < 4; y++) sum += PGet(p, -1, y + yO);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sum + 2) >> 2;
                                }
                            }
                        }
                        else
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = 1 << (dc.BitDepthC - 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (SamplingUtils.YAllMarkedAvailable(availability, yO, yO + 3))
                        {
                            int sum = 0;

                            for (int y = 0; y < 4; y++) sum += PGet(p, -1, y + yO);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sum + 2) >> 2;
                                }
                            }
                        }
                        else if (SamplingUtils.XAllMarkedAvailable(availability, xO, yO + 3))
                        {
                            int sum = 0;

                            for (int x = 0; x < 4; x++) sum += PGet(p, x + xO, -1);

                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = (sum + 2) >> 2;
                                }
                            }
                        }
                        else
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    predC[x + xO, y + yO] = 1 << (dc.BitDepthC - 1);
                                }
                            }
                        }
                    }
                }
            }
            else if (intraChromaPredMode == 1)
            {
                // Horizontal

                if (SamplingUtils.YAllMarkedAvailable(p, 0, sizes.Height))
                {
                    for (int x = 0; x < sizes.Width; x++)
                    {
                        for (int y = 0; y < sizes.Height; y++)
                        {
                            predC[x, y] = PGet(p, -1, y);
                        }
                    }
                }
            }
            else if (intraChromaPredMode == 2)
            {
                // Vertical

                if (SamplingUtils.XAllMarkedAvailable(p, 0, sizes.Width))
                {
                    for (int x = 0; x < sizes.Width; x++)
                    {
                        for (int y = 0; y < sizes.Height; y++)
                        {
                            predC[x, y] = PGet(p, x, -1);
                        }
                    }
                }
            }
            else if (intraChromaPredMode == 3)
            {
                // Plane

                if (SamplingUtils.AllMarkedAvailable(availability, 0, -1, sizes.Width, sizes.Height))
                {
                    int xCF = chromaArrayType == 3 ? 4 : 0;
                    int yCF = chromaArrayType != 1 ? 4 : 0;

                    int h = 0;
                    int v = 0;

                    for (int x = 0; x < 3 + xCF; x++) h += (x + 1) * (PGet(p, 4 + xCF + x, -1) - PGet(p, 2 + xCF - x, -1));
                    for (int y = 0; y < 3 + yCF; y++) v += (y + 1) * (PGet(p, -1, 4 + yCF + y) - PGet(p, -1, 2 + yCF - y));

                    int a = 16 * (PGet(p, -1, sizes.Height - 1) + PGet(p, sizes.Width - 1, -1));
                    int b = ((34 - 29 * Int32Boolean.I32(chromaArrayType == 3)) * h + 32) >> 6;
                    int c = ((34 - 29 * Int32Boolean.I32(chromaArrayType != 1)) * v + 32) >> 6;

                    for (int x = 0; x < sizes.Width; x++)
                    {
                        for (int y = 0; y < sizes.Height; y++)
                        {
                            predC[x, y] = Util264.Clip1C((a + b * (x - 3 - xCF) + c * (y - 3 - yCF) + 16) >> 5, dc.BitDepthC);
                        }
                    }
                }
            }

            static void Core(int x, int y, Span<int> availability, Span<int> p, Matrix16x16 cSC, DerivationContext dc, MacroblockSizeChroma sizes, IMacroblockUtility util, bool constrainedIntraPredFlag, bool isSiMb)
            {
                int mbAddrN = 0;
                Inter.DeriveNeighboringLocations(dc, false, x, y, out int xW, out int yW, ref dc.MbAddrX, ref mbAddrN, out bool valid);

                bool isUnavailable = !valid ||
                                     (util.IsCodedWithInter(mbAddrN) && constrainedIntraPredFlag) ||
                                     (!isSiMb && constrainedIntraPredFlag && util.IsMacroblockOfTypeSi(mbAddrN));

                if (isUnavailable)
                {
                    PSet(availability, x, y, 0);
                }
                else
                {
                    PSet(availability, x, y, 1);

                    int xL = 0;
                    int yL = 0;
                    Util264.InverseMacroblockScan(mbAddrN, !dc.IsMbaffFieldMacroblock, dc.IsMbaffFieldMacroblock, dc.IsMbaff, dc.PictureWidthInSamplesL, ref x, ref y, ref xL, ref yL);

                    int xM = (xL >> 4) * sizes.Width;
                    int yM = ((yL >> 4) * sizes.Height) + (yL % 2);

                    if (dc.IsMbaff && dc.IsMbaffFieldMacroblock)
                        PSet(p, x, y, cSC[xM + xW, yM + 2 * yW]);
                    else
                        PSet(p, x, y, cSC[xM + xW, yM + yW]);
                }
            }
        }

        public static void IntraChromaSamplePredictChromaArrayType3(
            Matrix16x16 cSC,
            Matrix16x16 predC,
            IMacroblockUtility util,
            DerivationContext dc,
            Span<int> p,
            int chromaArrayType)
        {
            _ = cSC;
            _ = predC;
            _ = util;
            _ = dc;
            _ = p;
            _ = chromaArrayType;

            NYI.ImplementLater();
        }
    }
}
