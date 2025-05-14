using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Utilities;

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
        BaselineDecoder.Scanning.InverseMacroblockScan(CurrMbAddr, isFrame, isField, mbaffFrameFlag, pictureWidthInSamplesL, ref x, ref y, ref xI, ref yI);

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
}
