namespace ContentDotNet.Video.Codecs.H264.Components
{
    /// <summary>
    ///   The macroblock traits.
    /// </summary>
    public static partial class H264MacroblockTraits
    {
        /// <summary>
        ///   Returns the macroblock partition width.
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int MbPartWidth(H264Macroblock mb)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                if (mb.Skipped) return 16;
                return P_MbPartWidth((int)mb.MacroblockLayer.MbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                if (mb.Skipped) return 8;
                return B_MbPartWidth((int)mb.MacroblockLayer.MbType);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns the macroblock partition width.
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int MbPartHeight(H264Macroblock mb)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                if (mb.Skipped) return 16;
                return P_MbPartHeight((int)mb.MacroblockLayer.MbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                if (mb.Skipped) return 8;
                return B_MbPartHeight((int)mb.MacroblockLayer.MbType);
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
        public static int SubMbPredMode(H264Macroblock mb, int mbType)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return SubP_SubMbPredMode(mbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return SubB_SubMbPredMode(mbType);
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
        public static int SubMbPartWidth(H264Macroblock mb, int mbType)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return SubP_SubMbPartWidth(mbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return SubB_SubMbPartWidth(mbType);
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
        public static int SubMbPartHeight(H264Macroblock mb, int mbType)
        {
            if (mb.SliceType == H264SliceType.P || mb.SliceType == H264SliceType.SP)
            {
                return SubP_SubMbPartHeight(mbType);
            }
            else if (mb.SliceType == H264SliceType.B)
            {
                return SubB_SubMbPartHeight(mbType);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        ///   Returns macroblock partition prediction mode.
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="mbPartIdx"></param>
        /// <returns></returns>
        public static int MbPartPredMode(H264Macroblock mb, int mbPartIdx) => MbPartPredMode(mb.SliceType, (int)mb.MacroblockLayer.MbType, mbPartIdx, mb.MacroblockLayer.TransformSize8x8Flag, mb.Skipped);

        private static int MbPartPredMode(H264SliceType sliceType, int mbType, int mbPartIdx, bool transformSize8x8Flag, bool inferred)
        {
            if (sliceType == H264SliceType.I)
            {
                if (mbPartIdx == 0)
                {
                    return I_MbPartPredMode_0(transformSize8x8Flag, mbType);
                }
                return H264PredictionModes.na;
            }
            else if (sliceType == H264SliceType.P || sliceType == H264SliceType.SP)
            {
                if (inferred)
                {
                    if (mbPartIdx == 0)
                    {
                        return H264PredictionModes.Pred_L0;
                    }
                    else if (mbPartIdx == 1)
                    {
                        return H264PredictionModes.na;
                    }
                    throw new InvalidOperationException();
                }
                if (mbPartIdx == 0)
                {
                    return P_MbPartPredMode_0(mbType);
                }
                else if (mbPartIdx == 1)
                {
                    return P_MbPartPredMode_1(mbPartIdx);
                }
                return H264PredictionModes.na;
            }
            else if (sliceType == H264SliceType.B)
            {
                if (inferred)
                {
                    if (mbPartIdx == 0)
                    {
                        return H264PredictionModes.Direct;
                    }
                    else if (mbPartIdx == 1)
                    {
                        return H264PredictionModes.na;
                    }
                    throw new InvalidOperationException();
                }
                if (mbPartIdx == 0)
                {
                    return B_MbPartPredMode_0(transformSize8x8Flag, mbType);
                }
                else if (mbPartIdx == 1)
                {
                    return B_MbPartPredMode_1(transformSize8x8Flag, mbPartIdx);
                }
                return H264PredictionModes.na;
            }
            else /* SI */
            {
                return H264PredictionModes.na;
            }
        }

        public static int GetIntra16x16PredictionMode(int mbType, bool transformSize8x8Flag) => I_GetPredMode(mbType, transformSize8x8Flag)!.Value;

        public static int GetIntra16x16CodedBlockPatternLuma(int mbType, bool transformSize8x8Flag) => I_GetCbpL(mbType, transformSize8x8Flag)!.Value;

        public static int GetIntra16x16CodedBlockPatternChroma(int mbType, bool transformSize8x8Flag) => I_CbpC(mbType, transformSize8x8Flag)!.Value;

        public static int NumSubMbPart(int subMbType, H264SliceType sliceType)
        {
            return sliceType switch
            {
                H264SliceType.P => SubP_NumSubMbPart(subMbType),
                H264SliceType.B => SubB_NumSubMbPart(subMbType),
                _ => throw new InvalidOperationException("Invalid slice type")
            };
        }

        public static int NumMbPart(int mbType, bool inferred, H264SliceType sliceType)
        {
            if (inferred && sliceType == H264SliceType.B) return H264PredictionModes.na;
            else if (inferred && (sliceType == H264SliceType.P || sliceType == H264SliceType.SP)) return 1;

                return sliceType switch
                {
                    H264SliceType.P or H264SliceType.SP => P_GetNumMbPart(mbType),
                    H264SliceType.B => B_GetNumMbPart(mbType) ?? -1,
                    _ => 1
                };
        }
    }
}
