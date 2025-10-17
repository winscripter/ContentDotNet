namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   Assigns ctxIdx.
    /// </summary>
    public static partial class AssignCtxIdx
    {
        const int na = -1;

        private static ReadOnlySpan<int> IncludedCtxIdxOffsets =>
        [
            0, 3, 11, 14, 17, 21, 24, 27, 32, 36, 40, 47, 54, 60, 64, 68, 69, 70, 73, 77, 276, 399
        ];

        private static ReadOnlySpan<int> CodedBlockFlagToCtxIdxBlockCatOffsetAssignments =>
        [
            0, 4, 8, 12, 16, 0, 0, 4, 8, 4, 0, 4, 8, 8
        ];

        private static ReadOnlySpan<int> SignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments =>
        [
            0, 15, 29, 44, 47, 0, 0, 15, 29, 0, 0, 15, 29, 0
        ];

        private static ReadOnlySpan<int> LastSignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments =>
        [
            0, 15, 29, 44, 47, 0, 0, 15, 29, 0, 0, 15, 29, 0 // Same as the one above
        ];

        private static ReadOnlySpan<int> CoeffAbsLevelMinus1ToCtxIdxBlockCatOffsetAssignments =>
        [
            0, 10, 20, 30, 39, 0, 0, 10, 20, 0, 0, 10, 20, 0
        ];

        public static int Assign(IH264CabacDecoder decoder, H264State state, H264MacroblockInfo? currMB, H264SyntaxElement se, int ctxIdxOffset, int binIdx)
        {
            ArgumentNullException.ThrowIfNull(currMB, nameof(currMB));

            if (IncludedCtxIdxOffsets.Contains(ctxIdxOffset))
            {
                return Core(decoder, state, currMB, binIdx, ctxIdxOffset);
            }
            else
            {
                if (se == H264SyntaxElement.CodedBlockFlag) return ctxIdxOffset + CodedBlockFlagToCtxIdxBlockCatOffsetAssignments[decoder.DecodingVariables.CtxBlockCat];
                else if (se == H264SyntaxElement.SignificantCoeffFlag) return ctxIdxOffset + SignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments[decoder.DecodingVariables.CtxBlockCat];
                else if (se == H264SyntaxElement.LastSignificantCoeffFlag) return ctxIdxOffset + LastSignificantCoeffFlagToCtxIdxBlockCatOffsetAssignments[decoder.DecodingVariables.CtxBlockCat];
                else if (se == H264SyntaxElement.CoeffAbsLevelMinus1) return ctxIdxOffset + CoeffAbsLevelMinus1ToCtxIdxBlockCatOffsetAssignments[decoder.DecodingVariables.CtxBlockCat];
                else throw new InvalidOperationException();
            }
        }

        private static int InvokeCiiFunction(IH264CabacDecoder cd, int clauseId, H264State h264, H264MacroblockInfo? currMB, int binIdx, int ctxIdxOffset)
        {
            if (clauseId == 1)
            {
                return H264CabacCtxIdxIncDerivative.MbSkipFlag(h264);
            }
            else if (clauseId == 2)
            {
                return H264CabacCtxIdxIncDerivative.MbFieldDecodingFlag(h264);
            }
            else if (clauseId == 3)
            {
                return H264CabacCtxIdxIncDerivative.MbType(h264, ctxIdxOffset);
            }
            else if (clauseId == 4)
            {
                return H264CabacCtxIdxIncDerivative.CodedBlockPattern(h264, ctxIdxOffset, binIdx, cd.ArithmeticReader.BinTracker.RecentBins);
            }
            else if (clauseId == 5)
            {
                return H264CabacCtxIdxIncDerivative.MbQpDelta(h264, cd.DecodingVariables.PreviousMacroblockAddress);
            }
            else if (clauseId == 6)
            {
                return H264CabacCtxIdxIncDerivative.RefIdxLX(h264, currMB!, cd.DecodingVariables.ListType,
                    cd.DecodingVariables.ListType == H264ListType.List0 ? cd.DecodingVariables.RefIdxL0 : cd.DecodingVariables.RefIdxL1,
                    cd.DecodingVariables.SubMbType,
                    cd.DecodingVariables.MbPartIdx);
            }
            else if (clauseId == 7)
            {
                return H264CabacCtxIdxIncDerivative.Mvd(h264, currMB!, cd.DecodingVariables.ListType,
                    cd.DecodingVariables.MbPartIdx, ctxIdxOffset,
                    cd.DecodingVariables.ListType == H264ListType.List0 ? cd.DecodingVariables.MvdL0 : cd.DecodingVariables.MvdL1,
                    cd.DecodingVariables.SubMbType);
            }
            else if (clauseId == 8)
            {
                return H264CabacCtxIdxIncDerivative.IntraChromaPredMode(h264);
            }
            else if (clauseId == 9)
            {
                return H264CabacCtxIdxIncDerivative.CodedBlockFlag(h264, currMB!, cd.DecodingVariables.CtxBlockCat, cd.DecodingVariables.CodedBlockFlagOptions);
            }
            else if (clauseId == 10)
            {
                return H264CabacCtxIdxIncDerivative.TransformSize8x8Flag(h264);
            }
            else /* 22 */
            {
                return H264CabacCtxIdxIncDerivativeStandalone.AssignUsingPriorDecodedBins(ctxIdxOffset, binIdx, cd.ArithmeticReader.BinTracker.RecentBins);
            }
        }
    }
}
