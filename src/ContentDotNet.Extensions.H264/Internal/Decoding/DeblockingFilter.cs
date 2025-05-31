using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal static class DeblockingFilter
{
    public static void FilterBlockEdges(
        bool chromaEdgeFlag,
        int iCbCr,
        bool verticalEdgeFlag,
        bool fieldModeInFrameFilteringFlag,
        Span<int> xE,
        Span<int> yE,
        int CurrMbAddr,
        Matrix SL,
        Matrix SCB,
        Matrix SCR,
        bool isFrame,
        bool isField,
        bool mbaffFrameFlag,
        int pictureWidthInSamplesL,
        ChromaFormat chromaFormat,
        MacroblockSizeChroma sizes,
        FilterOffsets filterOffsets)
    {
        int nE = !chromaEdgeFlag ? 16 : verticalEdgeFlag ? sizes.Height : sizes.Width;
        Matrix S = !chromaEdgeFlag ? SL : iCbCr == 0 ? SCB : SCR;

        int dy = 1 + Int32Boolean.I32(fieldModeInFrameFilteringFlag);

        int x = 0, y = 0, xI = 0, yI = 0;
        IntraInterDecoder.Scanning.InverseMacroblockScan(CurrMbAddr, isFrame, isField, mbaffFrameFlag, pictureWidthInSamplesL, ref x, ref y, ref xI, ref yI);

        int xP = !chromaEdgeFlag ? xI : xI / chromaFormat.ChromaWidth;
        int yP = !chromaEdgeFlag ? xI : (yI + chromaFormat.ChromaHeight - 1) / chromaFormat.ChromaHeight;

        for (int k = 0; k <= nE - 1; k++)
        {
            _Core(xE, yE);

            void _Core(Span<int> xE, Span<int> yE)
            {
                Span<int> q = stackalloc int[4];
                Span<int> p = stackalloc int[4];

                if (verticalEdgeFlag)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        q[i] = S[xP + xE[k] + i, yP + dy * yE[k]];
                        p[i] = S[xP + xE[k] - i - 1, yP + dy * yE[k]];
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        q[i] = S[xP + xE[k], yP + dy * (yE[k] + i) - (yE[k] % 2)];
                        p[i] = S[xP + xE[k], yP + dy * (yE[k] - i - 1) - (yE[k] % 2)];
                    }
                }

                FilterSetOfSamplesAcrossHorizontalOrVerticalBlockEdge(p, q, chromaEdgeFlag, 0, verticalEdgeFlag, filterOffsets);

                if (verticalEdgeFlag)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        S[xP + xE[k] + i, yP + dy * yE[k]] = q[i];
                        S[xP + xE[k] - i - 1, yP + dy * yE[k]] = p[i];
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        S[xP + xE[k], yP + dy * (yE[k] + i) - (yE[k] % 2)] = q[i];
                        S[xP + xE[k], yP + dy * (yE[k] - i - 1) - (yE[k] % 2)] = p[i];
                    }
                }
            }
        }
    }

    public static void FilterSetOfSamplesAcrossHorizontalOrVerticalBlockEdge(
        Span<int> p,
        Span<int> q,
        bool chromaEdgeFlag,
        int lumaBS,
        bool verticalEdgeFlag,
        FilterOffsets filterOffsets)
    {
        throw new Exception();
    }

    public static int DeriveLumaContentDependentBoundaryFilteringStrength(
        bool pIsFrame,
        bool qIsFrame,
        int pMbPartPredMode,
        int qMbPartPredMode,
        GeneralSliceType pSliceType,
        GeneralSliceType qSliceType,
        bool pqAreInDifferentMacroblockPairs,
        bool verticalEdgeFlag,
        bool mbaffFrameFlag,
        bool fieldPicFlag,
        bool blockEdgeIsMacroblockEdge,
        bool pTransformSize8x8Flag,
        bool qTransformSize8x8Flag,
        bool p8x8LumaHasNonzeroCoeffs,
        bool p4x4LumaHasNonzeroCoeffs,
        bool q8x8LumaHasNonzeroCoeffs,
        bool q4x4LumaHasNonzeroCoeffs)
    {
        bool mixedModeEdgeFlag = mbaffFrameFlag && pqAreInDifferentMacroblockPairs;

        int bS;
        if (blockEdgeIsMacroblockEdge &&
            (pIsFrame && qIsFrame && (SliceTypes.IsIntra(pMbPartPredMode) && SliceTypes.IsIntra(qMbPartPredMode))) ||
            (pSliceType is GeneralSliceType.SI or GeneralSliceType.SP && qSliceType is GeneralSliceType.SI or GeneralSliceType.SP) ||
            ((mbaffFrameFlag || fieldPicFlag) && verticalEdgeFlag && (SliceTypes.IsIntra(pMbPartPredMode) && SliceTypes.IsIntra(qMbPartPredMode))) ||
            ((mbaffFrameFlag || fieldPicFlag) && verticalEdgeFlag && pSliceType is GeneralSliceType.SI or GeneralSliceType.SP && qSliceType is GeneralSliceType.SI or GeneralSliceType.SP))
        {
            bS = 4;
        }
        else if ((!mixedModeEdgeFlag && (SliceTypes.IsIntra(pMbPartPredMode) || SliceTypes.IsI(qMbPartPredMode))) ||
                 (!mixedModeEdgeFlag && (pSliceType is GeneralSliceType.SI or GeneralSliceType.SP || qSliceType is GeneralSliceType.SI or GeneralSliceType.SP)) ||
                 (mixedModeEdgeFlag && !verticalEdgeFlag && (SliceTypes.IsIntra(pMbPartPredMode) || SliceTypes.IsIntra(qMbPartPredMode))) ||
                 (mixedModeEdgeFlag && !verticalEdgeFlag && (pSliceType is GeneralSliceType.SI or GeneralSliceType.SP || qSliceType is GeneralSliceType.SI or GeneralSliceType.SP)))
        {
            bS = 3;
        }
        else if ((pTransformSize8x8Flag && p8x8LumaHasNonzeroCoeffs) ||
                 (!pTransformSize8x8Flag && p4x4LumaHasNonzeroCoeffs) ||
                 (qTransformSize8x8Flag && q8x8LumaHasNonzeroCoeffs) ||
                 (!qTransformSize8x8Flag && q4x4LumaHasNonzeroCoeffs))
        {
            bS = 2;
        }
        else
        {
            if (mixedModeEdgeFlag)
            {
                bS = 1;
            }
            else
            {
                throw new NotImplementedException("DeriveLumaContentDependentBoundaryFilteringStrength: Unsupported case where bS needs to be set to 1 but mixedModeEdgeFlag is 0.");
            }
        }

        return bS;
    }
}
