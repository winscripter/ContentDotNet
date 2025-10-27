namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.CtxIdxDerivative
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Components;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;

    /// <summary>
    ///   Derive ctxIdx without relying on other macroblocks.
    /// </summary>
    internal static class H264CabacCtxIdxIncDerivativeStandalone
    {
        public static int AssignUsingPriorDecodedBins(int ctxIdxOffset, int binIdx, BinHistory history)
        {
            int b1 = history[1].AsInt32();
            int b3 = history[3].AsInt32();

            if (ctxIdxOffset == 3)
            {
                if (binIdx == 4)
                {
                    return (b3 != 0) ? 5 : 6;
                }
                else if (binIdx == 5)
                {
                    return (b3 != 0) ? 6 : 7;
                }
            }
            else if (ctxIdxOffset == 14 && binIdx == 2)
            {
                return (b1 != 1) ? 2 : 3;
            }
            else if (ctxIdxOffset == 17 && binIdx == 4)
            {
                return (b3 != 0) ? 2 : 3;
            }
            else if (ctxIdxOffset == 27 && binIdx == 2)
            {
                return (b1 != 0) ? 4 : 5;
            }
            else if (ctxIdxOffset == 32 && binIdx == 4)
            {
                return (b3 != 0) ? 2 : 3;
            }
            else if (ctxIdxOffset == 36 && binIdx == 2)
            {
                return (b1 != 0) ? 2 : 3;
            }

            throw UnreachableException.Instance;
        }

        public static int AssignCtxIdxIncForCoeffFlagsAndAbsLevel(IH264CabacDecoder cd, ResidualBlockType blkType, StandaloneCtxIdxIncDerivativeMode derivativeMode, bool isFrame)
        {
            int numDecodAbsLevelEq1 = cd.DecodingVariables.ReportedCoefficientsForCurrentListEqualTo1;
            int numDecodAbsLevelGt1 = cd.DecodingVariables.ReportedCoefficientsForCurrentListGreaterThan1;
            int levelListIdx = cd.DecodingVariables.LevelListIndex;
            int binIdx = cd.BinIndex;
            int NumC8x8 = cd.DecodingVariables.NumC8x8;

            int ctxBlockCat = CabacResidualToCtxBlockCatMapping.GetCtxBlockCat(blkType);
            cd.DecodingVariables.CtxBlockCat = ctxBlockCat;

            if ((derivativeMode is StandaloneCtxIdxIncDerivativeMode.SignificantCoefficientFlag or StandaloneCtxIdxIncDerivativeMode.LastSignificantCoefficientFlag)
                && (ctxBlockCat is not 3 and not 5 and not 9 and not 13))
                return levelListIdx;

            if ((derivativeMode is StandaloneCtxIdxIncDerivativeMode.SignificantCoefficientFlag or StandaloneCtxIdxIncDerivativeMode.LastSignificantCoefficientFlag)
                && ctxBlockCat == 3)
                return Math.Min(levelListIdx / NumC8x8, 2);

            int ctxIdxInc = CabacLevelListIndexToCtxIdxInc.Get(levelListIdx, derivativeMode, isFrame);

            if (derivativeMode is StandaloneCtxIdxIncDerivativeMode.SignificantCoefficientFlag or StandaloneCtxIdxIncDerivativeMode.LastSignificantCoefficientFlag)
                return ctxIdxInc;

            if (binIdx == 0)
                return (numDecodAbsLevelGt1 != 0) ? 0 : Math.Min(4, 1 + numDecodAbsLevelEq1);
            else
                return 5 + Math.Min(4 - ((ctxBlockCat == 3) ? 1 : 0), numDecodAbsLevelGt1);
        }
    }
}
