namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   The macroblock traits.
    /// </summary>
    public static partial class MacroblockTraits
    {
        /// <summary>
        ///   Returns the macroblock partition width.
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int MbPartWidth(H264MacroblockInfo mb)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return P_MbPartWidth(mb.Rbsp.TransformSize8x8Flag, (int)mb.Rbsp.MbType) ?? throw new InvalidOperationException();
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return B_MbPartWidth(mb.Rbsp.TransformSize8x8Flag, (int)mb.Rbsp.MbType) ?? throw new InvalidOperationException();
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns the macroblock partition width.
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int MbPartHeight(H264MacroblockInfo mb)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return P_MbPartHeight(mb.Rbsp.TransformSize8x8Flag, (int)mb.Rbsp.MbType) ?? throw new InvalidOperationException();
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return B_MbPartHeight(mb.Rbsp.TransformSize8x8Flag, (int)mb.Rbsp.MbType) ?? throw new InvalidOperationException();
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns the sub-macroblock prediction mode.
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="mbType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int SubMbPredMode(H264MacroblockInfo mb, int mbType)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return SubP_SubMbPredMode(mb.Rbsp.TransformSize8x8Flag, mbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return SubB_SubMbPredMode(mb.Rbsp.TransformSize8x8Flag, mbType);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns the sub-macroblock partition width.
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="mbType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int SubMbPartWidth(H264MacroblockInfo mb, int mbType)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return SubP_SubMbPartWidth(mb.Rbsp.TransformSize8x8Flag, mbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return SubB_SubMbPartWidth(mb.Rbsp.TransformSize8x8Flag, mbType);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns the sub-macroblock partition height.
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="mbType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int SubMbPartHeight(H264MacroblockInfo mb, int mbType)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return SubP_SubMbPartHeight(mb.Rbsp.TransformSize8x8Flag, mbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return SubB_SubMbPartHeight(mb.Rbsp.TransformSize8x8Flag, mbType);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns macroblock partition prediction mode.
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="mbPartIdx"></param>
        /// <returns></returns>
        public static int MbPartPredMode(H264MacroblockInfo mb, int mbPartIdx) => MbPartPredMode(mb.SliceType, (int)mb.Rbsp.MbType, mbPartIdx, mb.Rbsp.TransformSize8x8Flag, mb.Inferred);

        private static int MbPartPredMode(H264SliceType sliceType, int mbType, int mbPartIdx, bool transformSize8x8Flag, bool inferred)
        {
            if (sliceType == H264SliceType.I)
            {
                if (mbPartIdx == 0)
                {
                    return I_MbPartPredMode_0(transformSize8x8Flag, mbType);
                }
                return NotAn.na;
            }
            else if (sliceType == H264SliceType.P || sliceType == H264SliceType.SP)
            {
                if (inferred)
                {
                    if (mbPartIdx == 0)
                    {
                        return PredictionModes.Pred_L0;
                    }
                    else if (mbPartIdx == 1)
                    {
                        return PredictionModes.na;
                    }
                    throw new InvalidOperationException();
                }
                if (mbPartIdx == 0)
                {
                    return P_MbPartPredMode_0(transformSize8x8Flag, mbType) ?? throw new InvalidOperationException();
                }
                else if (mbPartIdx == 1)
                {
                    return P_MbPartPredMode_1(transformSize8x8Flag, mbPartIdx) ?? throw new InvalidOperationException();
                }
                return NotAn.na;
            }
            else if (sliceType == H264SliceType.B)
            {
                if (inferred)
                {
                    if (mbPartIdx == 0)
                    {
                        return PredictionModes.Direct;
                    }
                    else if (mbPartIdx == 1)
                    {
                        return PredictionModes.na;
                    }
                    throw new InvalidOperationException();
                }
                if (mbPartIdx == 0)
                {
                    return B_MbPartPredMode_0(transformSize8x8Flag, mbType) ?? throw new InvalidOperationException();
                }
                else if (mbPartIdx == 1)
                {
                    return B_MbPartPredMode_1(transformSize8x8Flag, mbPartIdx) ?? throw new InvalidOperationException();
                }
                return NotAn.na;
            }
            else /* SI */
            {
                return NotAn.na;
            }
        }

        public static int GetIntra16x16PredictionMode(int mbType, bool transformSize8x8Flag) => I_GetPredMode(mbType, transformSize8x8Flag)!.Value;

        public static int GetIntra16x16CodedBlockPatternLuma(int mbType, bool transformSize8x8Flag) => I_GetCbpL(mbType, transformSize8x8Flag)!.Value;

        public static int GetIntra16x16CodedBlockPatternChroma(int mbType, bool transformSize8x8Flag) => I_CbpC(mbType, transformSize8x8Flag)!.Value;

        public static int NumSubMbPart(int subMbType, bool transformSize8x8Flag, H264SliceType sliceType)
        {
            return sliceType switch
            {
                H264SliceType.P => SubP_NumSubMbPart(transformSize8x8Flag, subMbType),
                H264SliceType.B => SubB_NumSubMbPart(transformSize8x8Flag, subMbType),
                _ => throw new InvalidOperationException("Invalid slice type")
            };
        }

        public static int NumMbPart(int mbType, bool transformSize8x8Flag, H264SliceType sliceType)
        {
            return sliceType switch
            {
                H264SliceType.P => P_GetNumMbPart(transformSize8x8Flag, mbType) ?? -1,
                H264SliceType.B => B_GetNumMbPart(transformSize8x8Flag, mbType) ?? -1,
                _ => throw new InvalidOperationException("Invalid slice type")
            };
        }
    }
}
