namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
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

        public static int AssignCtxIdxIncForCoeffFlagsAndAbsLevel(IH264CabacDecoder cd, int binIdx, int NumC8x8, int levelListIdx, ResidualBlockType blkType, StandaloneCtxIdxIncDerivativeMode derivativeMode, bool isFrame, int numDecodAbsLevelEq1, int numDecodAbsLevelGt1)
        {
            int mode = (int)derivativeMode + 1;

            var ctxBlockCat = blkType switch
            {
                ResidualBlockType.Intra16x16DCLevel => 0,
                ResidualBlockType.Intra16x16ACLevel => 1,
                ResidualBlockType.LumaLevel4x4 => 2,
                ResidualBlockType.ChromaDCLevel => 3,
                ResidualBlockType.ChromaACLevel => 4,
                ResidualBlockType.LumaLevel8x8 => 5,
                ResidualBlockType.Cb16x16DCLevel => 6,
                ResidualBlockType.Cb16x16ACLevel => 7,
                ResidualBlockType.CbLevel4x4 => 8,
                ResidualBlockType.CbLevel8x8 => 9,
                ResidualBlockType.Cr16x16DCLevel => 10,
                ResidualBlockType.Cr16x16ACLevel => 11,
                ResidualBlockType.CrLevel4x4 => 12,
                ResidualBlockType.CrLevel8x8 => 13,
                _ => throw new NotImplementedException($"ResidualBlockType {blkType} is not implemented."),
            };
            cd.DecodingVariables.CtxBlockCat = ctxBlockCat;

            if ((mode is 1 or 2) && (ctxBlockCat is not 3 and not 5 and not 9 and not 13))
                return levelListIdx;

            if ((mode is 1 or 2) && ctxBlockCat == 3)
                return Math.Min(levelListIdx / NumC8x8, 2);

            int ctxIdxInc = 0;

            switch (levelListIdx)
            {
                case 0:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 0 : 0) : 0;
                    break;

                case 1:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 1 : 1) : 1;
                    break;

                case 2:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 2 : 1) : 1;
                    break;

                case 3:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 3 : 2) : 1;
                    break;

                case 4:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 2) : 1;
                    break;

                case 5:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 5 : 3) : 1;
                    break;

                case 6:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 5 : 3) : 1;
                    break;

                case 7:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 4) : 1;
                    break;

                case 8:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 5) : 1;
                    break;

                case 9:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 3 : 6) : 1;
                    break;

                case 10:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 3 : 7) : 1;
                    break;

                case 11:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 7) : 1;
                    break;

                case 12:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 7) : 1;
                    break;

                case 13:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 8) : 1;
                    break;

                case 14:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 5 : 4) : 1;
                    break;

                case 15:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 5 : 5) : 1;
                    break;

                case 16:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 6) : 2;
                    break;

                case 17:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 9) : 2;
                    break;

                case 18:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 10) : 2;
                    break;

                case 19:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 4 : 10) : 2;
                    break;

                case 20:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 3 : 8) : 2;
                    break;

                case 21:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 3 : 11) : 2;
                    break;

                case 22:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 6 : 12) : 2;
                    break;

                case 23:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 7 : 11) : 2;
                    break;

                case 24:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 7 : 9) : 2;
                    break;

                case 25:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 7 : 9) : 2;
                    break;

                case 26:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 8 : 10) : 2;
                    break;

                case 27:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 9 : 10) : 2;
                    break;

                case 28:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 10 : 8) : 2;
                    break;

                case 29:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 9 : 11) : 2;
                    break;

                case 30:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 8 : 12) : 2;
                    break;

                case 31:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 7 : 11) : 2;
                    break;

                case 32:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 7 : 9) : 3;
                    break;

                case 33:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 6 : 9) : 3;
                    break;

                case 34:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 11 : 10) : 3;
                    break;

                case 35:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 12 : 10) : 3;
                    break;

                case 36:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 13 : 8) : 3;
                    break;

                case 37:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 11 : 11) : 3;
                    break;

                case 38:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 6 : 12) : 3;
                    break;

                case 39:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 7 : 11) : 3;
                    break;

                case 40:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 8 : 9) : 4;
                    break;

                case 41:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 9 : 9) : 4;
                    break;

                case 42:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 14 : 10) : 4;
                    break;

                case 43:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 10 : 10) : 4;
                    break;

                case 44:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 9 : 8) : 4;
                    break;

                case 45:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 8 : 13) : 4;
                    break;

                case 46:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 6 : 13) : 4;
                    break;

                case 47:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 11 : 9) : 4;
                    break;

                case 48:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 12 : 9) : 5;
                    break;

                case 49:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 13 : 10) : 5;
                    break;

                case 50:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 11 : 10) : 5;
                    break;

                case 51:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 6 : 8) : 5;
                    break;

                case 52:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 9 : 13) : 6;
                    break;

                case 53:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 14 : 13) : 6;
                    break;

                case 54:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 10 : 9) : 6;
                    break;

                case 55:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 9 : 9) : 6;
                    break;

                case 56:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 11 : 10) : 7;
                    break;

                case 57:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 12 : 10) : 7;
                    break;

                case 58:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 13 : 14) : 7;
                    break;

                case 59:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 11 : 14) : 7;
                    break;

                case 60:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 14 : 14) : 8;
                    break;

                case 61:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 10 : 14) : 8;
                    break;

                case 62:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 12 : 14) : 8;
                    break;

                case 63:
                    ctxIdxInc = (mode == 1) ? (isFrame ? 0 : 0) : 0;
                    break;
            }

            if (mode != 3)
                return ctxIdxInc;

            // Mode is 3 here

            if (binIdx == 0)
                return (numDecodAbsLevelGt1 != 0) ? 0 : Math.Min(4, 1 + numDecodAbsLevelEq1);
            else
                return 5 + Math.Min(4 - ((ctxBlockCat == 3) ? 1 : 0), numDecodAbsLevelGt1);
        }
    }
}
