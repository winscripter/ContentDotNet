namespace ContentDotNet.Extensions.Video.H264.Components.IO.Rbsp
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;
    using ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using ContentDotNet.Primitives;
    using Grabber = Utilities.SyntaxElementGrabber;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.PredictionModes;

    internal class ResidualIO
    {
        public static RbspResidual ReadResidual(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState)
        {
            // NOTE: The residual ( startIdx, endIdx ) element seems to be only invoked once - and that is in
            // the macroblock_layer element. However, there, the parameters passed to startIdx and endIdx
            // are always ( 0, 15 ) and do not seem to differ in any other condition. That's why we'll
            // hardcode these values.

            const int startIdx = 0;
            const int endIdx = 15;

            ResidualBlockType blockType = ResidualBlockType.Intra16x16DCLevel;
            ResidualBlockType blockBase = ResidualBlockType.Intra16x16DCLevel;

            ResidualBlockProcessor residual_block;

            H264DecodingVariables? dv = DefaultRbspReader.GetDecodingVariables(syntaxReader);

            Ref<bool> coded_block_flag = new();

            Action<int> setIdx4x4 = (x) =>
            {
                if (dv != null)
                    dv.CodedBlockFlagOptions.Luma4x4BlkIdx = x;
            };

            Action<int> setIdx8x8 = (x) =>
            {
                if (dv != null)
                    dv.CodedBlockFlagOptions.Luma8x8BlkIdx = x;
            };

            if (!Grabber.GetEntropyCodingModeFlag(rbspState))
                residual_block = ReadCavlcResidual;
            else
                residual_block = ReadCabacResidual;

            DcLevel16x16 Intra16x16DCLevel = new();
            AcLevel16x16 Intra16x16ACLevel = new();
            Level4x4 LumaLevel4x4 = new();
            Level8x8 LumaLevel8x8 = new();

            DcLevel16x16 CbIntra16x16DCLevel = new();
            AcLevel16x16 CbIntra16x16ACLevel = new();
            Level4x4 CbLevel4x4 = new();
            Level8x8 CbLevel8x8 = new();

            DcLevel16x16 CrIntra16x16DCLevel = new();
            AcLevel16x16 CrIntra16x16ACLevel = new();
            Level4x4 CrLevel4x4 = new();
            Level8x8 CrLevel8x8 = new();

            bool CbfIntra16x16DCLevel = new();
            List<bool> CbfIntra16x16ACLevel = [];
            List<bool> CbfLumaLevel4x4 = [];
            List<bool> CbfLumaLevel8x8 = [];

            bool CbfCbIntra16x16DCLevel = new();
            List<bool> CbfCbIntra16x16ACLevel = [];
            List<bool> CbfCbLevel4x4 = [];
            List<bool> CbfCbLevel8x8 = [];

            bool CbfCrIntra16x16DCLevel = new();
            List<bool> CbfCrIntra16x16ACLevel = [];
            List<bool> CbfCrLevel4x4 = [];
            List<bool> CbfCrLevel8x8 = [];

            Action<bool> receiveCbfForDC = (x) => CbfIntra16x16DCLevel = x;
            Action<bool> receiveCbfForAC = CbfIntra16x16ACLevel.Add;
            Action<bool> receiveCbfForLL4 = CbfLumaLevel4x4.Add;
            Action<bool> receiveCbfForLL8 = CbfLumaLevel8x8.Add;

            ResidualLuma(Intra16x16DCLevel, Intra16x16ACLevel, LumaLevel4x4, LumaLevel8x8, startIdx, endIdx);

            List<List<int>> ChromaDCLevel = [];
            for (int i = 0; i < 2; i++)
            {
                List<int> curr = [];
                for (int j = 0; j < 16; j++)
                {
                    curr.Add(0);
                }
                ChromaDCLevel.Add(curr);
            }
            List<List<List<int>>> ChromaACLevel = [];
            for (int i = 0; i < 2; i++)
            {
                List<List<int>> curr = [];
                for (int j = 0; j < 16; j++)
                {
                    List<int> curr2 = [];
                    for (int k = 0; k < 16; k++)
                    {
                        curr2.Add(0);
                    }
                    curr.Add(curr2);
                }
                ChromaACLevel.Add(curr);
            }

            List<bool> CbfChromaDCLevel = [];
            List<List<bool>> CbfChromaACLevel = [[], []];

            if (rbspState.ChromaArrayType() is 1 or 2)
            {
                blockType = ResidualBlockType.ChromaDCLevel;
                blockBase = blockType;

                H264ChromaFormat cf = rbspState.ChromaFormat();
                int NumC8x8 = 4 / (cf.ChromaWidth * cf.ChromaHeight);
                if (dv != null)
                    dv.NumC8x8 = NumC8x8;

                receiveCbfForDC = CbfChromaDCLevel.Add;

                for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.ICbCr = iCbCr;

                    if ((mb.Rbsp.GetCodedBlockPatternChroma() & 3).AsBoolean() && startIdx == 0)
                    {
                        // Set levelListIdx
                        if (dv != null)
                            dv.LevelListIndex = 0;

                        residual_block(ChromaDCLevel[iCbCr], 0, 4 * NumC8x8 - 1, 4 * NumC8x8);
                        CbfChromaDCLevel.Add(coded_block_flag.Value);
                    }
                }

                for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.ICbCr = iCbCr;

                    receiveCbfForDC = (x) => CbfChromaACLevel[iCbCr].Add(x);

                    for (int i8x8 = 0; i8x8 < NumC8x8; i8x8++)
                    {
                        for (int i4x4 = 0; i4x4 < 4; i4x4++)
                        {
                            if ((mb.Rbsp.GetCodedBlockPatternChroma() & 2).AsBoolean())
                            {
                                // Set levelListIdx
                                if (dv != null)
                                {
                                    dv.LevelListIndex = i8x8 * 4 + i4x4;
                                    dv.CodedBlockFlagOptions.Chroma4x4BlkIdx = i4x4;
                                }

                                residual_block(ChromaACLevel[iCbCr][i8x8 * 4 + i4x4], Math.Max(0, startIdx - 1), endIdx - 1, 15);
                                CbfChromaACLevel[iCbCr].Add(coded_block_flag.Value);
                            }
                            else
                                for (int i = 0; i < 15; i++)
                                    ChromaACLevel[iCbCr][i8x8 * 4 + i4x4][i] = 0;
                        }
                    }
                }
            }
            else if (rbspState.ChromaArrayType() == 3)
            {
                blockBase = ResidualBlockType.Cb16x16DCLevel;
                blockType = blockBase;

                receiveCbfForDC = (x) => CbfCbIntra16x16DCLevel = x;
                receiveCbfForAC = CbfCbIntra16x16ACLevel.Add;
                receiveCbfForLL4 = CbfCbLevel4x4.Add;
                receiveCbfForLL8 = CbfCrLevel8x8.Add;

                setIdx4x4 = (x) =>
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.Cb4x4BlkIdx = x;
                };
                setIdx8x8 = (x) =>
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.Cb8x8BlkIdx = x;
                };

                ResidualLuma(CbIntra16x16DCLevel, CbIntra16x16ACLevel, CbLevel4x4, CbLevel8x8, startIdx, endIdx);

                blockType = ResidualBlockType.Cr16x16DCLevel;
                blockBase = blockType;

                receiveCbfForDC = (x) => CbfCrIntra16x16DCLevel = x;
                receiveCbfForAC = CbfCrIntra16x16ACLevel.Add;
                receiveCbfForLL4 = CbfCrLevel4x4.Add;
                receiveCbfForLL8 = CbfCrLevel8x8.Add;

                setIdx4x4 = (x) =>
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.Cr4x4BlkIdx = x;
                };
                setIdx8x8 = (x) =>
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.Cr8x8BlkIdx = x;
                };

                ResidualLuma(CrIntra16x16DCLevel, CrIntra16x16ACLevel, CrLevel4x4, CrLevel8x8, startIdx, endIdx);
            }

            return new RbspResidual(
                Intra16x16DCLevel.Value,
                Intra16x16ACLevel.Value,
                LumaLevel4x4.Value,
                LumaLevel8x8.Value,
                ChromaDCLevel,
                ChromaACLevel,
                CbIntra16x16DCLevel.Value,
                CbIntra16x16ACLevel.Value,
                CbLevel4x4.Value,
                CbLevel8x8.Value,
                CrIntra16x16DCLevel.Value,
                CrIntra16x16ACLevel.Value,
                CrLevel4x4.Value,
                CrLevel8x8.Value,
                CbfIntra16x16DCLevel,
                CbfIntra16x16ACLevel,
                CbfLumaLevel4x4,
                CbfLumaLevel8x8,
                CbfChromaDCLevel,
                CbfChromaACLevel,
                CbfCbIntra16x16DCLevel,
                CbfCbIntra16x16ACLevel,
                CbfCbLevel4x4,
                CbfCbLevel8x8,
                CbfCrIntra16x16DCLevel,
                CbfCrIntra16x16ACLevel,
                CbfCrLevel4x4,
                CbfCrLevel8x8);

            void ResidualLuma(DcLevel16x16 i16x16DClevel, AcLevel16x16 i16x16AClevel, Level4x4 level4x4, Level8x8 level8x8,
                    int startIdx, int endIdx)
            {
                if (startIdx == 0 && MacroblockTraits.MbPartPredMode(mb, 0) == Intra_16x16)
                {
                    residual_block(i16x16DClevel.Value, 0, 15, 16);
                    receiveCbfForDC(coded_block_flag.Value);
                }

                for (int i8x8 = 0; i8x8 < 4; i8x8++)
                {
                    if (!mb.Rbsp.TransformSize8x8Flag || !Grabber.GetEntropyCodingModeFlag(rbspState))
                    {
                        for (int i4x4 = 0; i4x4 < 4; i4x4++)
                        {
                            if ((mb.Rbsp.GetCodedBlockPatternLuma() & (1 << i8x8)).AsBoolean())
                            {
                                // Set levelListIdx
                                if (dv != null)
                                    dv.LevelListIndex = i8x8 * 4 + i4x4;

                                setIdx4x4(i4x4);
                                setIdx8x8(i8x8);

                                if (MacroblockTraits.MbPartPredMode(mb, 0) == Intra_16x16)
                                {
                                    blockType = blockBase + 1;
                                    residual_block(i16x16AClevel.Value[i8x8 * 4 + i4x4], Math.Max(0, startIdx - 1), endIdx - 1, 15);
                                    receiveCbfForAC(coded_block_flag.Value);
                                }
                                else
                                {
                                    blockType = blockBase + 2;
                                    residual_block(level4x4.Value[i8x8 * 4 + i4x4], startIdx, endIdx, 16);
                                    receiveCbfForLL4(coded_block_flag.Value);
                                }
                            }
                            else if (MacroblockTraits.MbPartPredMode(mb, 0) == Intra_16x16)
                            {
                                for (int i = 0; i < 15; i++)
                                {
                                    blockType = blockBase + 1;
                                    i16x16AClevel.Value[i8x8 * 4 + i4x4][i] = 0;
                                }
                                receiveCbfForAC(false);
                            }
                            else
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    blockType = blockBase + 2;
                                    level4x4.Value[i8x8 * 4 + i4x4][i] = 0;
                                }
                                receiveCbfForLL4(false);
                            }

                            if (!Grabber.GetEntropyCodingModeFlag(rbspState) && mb.Rbsp.TransformSize8x8Flag)
                            {
                                setIdx4x4(i4x4);
                                setIdx8x8(i8x8);

                                for (int i = 0; i < 16; i++)
                                {
                                    blockType = blockBase + 3;

                                    // Set levelListIdx
                                    if (dv != null)
                                        dv.LevelListIndex = i8x8;

                                    level8x8.Value[i8x8][4 * i + i4x4] = level4x4.Value[i8x8 * 4 + i4x4][i];
                                    receiveCbfForLL8(false);
                                }
                            }
                        }
                    }
                    else if ((mb.Rbsp.GetCodedBlockPatternLuma() & (1 << i8x8)).AsBoolean())
                    {
                        blockType = blockBase + 3;

                        // Set levelListIdx
                        if (dv != null)
                            dv.LevelListIndex = i8x8;

                        setIdx8x8(i8x8);

                        residual_block(level8x8.Value[i8x8], 4 * startIdx, 4 * endIdx + 3, 64);
                        receiveCbfForLL8(false);
                    }
                    else
                    {
                        blockType = blockBase + 3;
                        for (int i = 0; i < 64; i++)
                        {
                            level8x8.Value[i8x8][i] = 0;
                        }
                        receiveCbfForLL8(false);
                    }
                }
            }

            void ReadCavlcResidual(List<int> coeffLevel, int start, int end, int maxNumCoeff)
            {
                // TODO: CAVLC Residuals

                throw new NotImplementedException("CAVLC Residual I/O is not yet implemented");
            }

            void ReadCabacResidual(List<int> coeffLevel, int start, int end, int maxNumCoeff)
            {
                bool cbf = (maxNumCoeff != 64 || rbspState.ChromaArrayType() == 3) && syntaxReader.ReadCodedBlockFlag();
                coded_block_flag.Value = cbf;

                for (int i = 0; i < maxNumCoeff; i++)
                    coeffLevel[i] = 0;

                if (cbf)
                {
                    Span<bool> significant_coeff_flag = stackalloc bool[end + 1];
                    Span<bool> last_significant_coeff_flag = stackalloc bool[end + 1];

                    int numCoeff = end + 1;
                    int i = start;

                    if (dv != null)
                    {
                        dv.ReportedCoefficientsForCurrentListEqualTo1 = coeffLevel.Count(x => x == 1);
                        dv.ReportedCoefficientsForCurrentListGreaterThan1 = coeffLevel.Count(x => x > 1);
                        dv.ResidualBlockType = blockType;
                    }

                    while (i < numCoeff - 1)
                    {
                        significant_coeff_flag[i] = syntaxReader.ReadSignificantCoeffFlag();

                        if (significant_coeff_flag[i])
                        {
                            last_significant_coeff_flag[i] = syntaxReader.ReadLastSignificantCoeffFlag();
                            if (last_significant_coeff_flag[i])
                                numCoeff = i + 1;
                        }

                        i++;
                    }

                    uint coeff_abs_level_minus1 = syntaxReader.ReadCoeffAbsLevelMinus1();
                    bool coeff_sign_flag = syntaxReader.ReadCoeffSignFlag();

                    coeffLevel[numCoeff - 1] = (int)(coeff_abs_level_minus1 + 1) * (1 - 2 * coeff_sign_flag.AsInt32());

                    for (i = numCoeff - 2; i >= start; i--)
                    {
                        if (significant_coeff_flag[i])
                        {
                            if (dv != null)
                            {
                                dv.ReportedCoefficientsForCurrentListEqualTo1 = coeffLevel.Count(x => x == 1);
                                dv.ReportedCoefficientsForCurrentListGreaterThan1 = coeffLevel.Count(x => x > 1);
                            }

                            coeff_abs_level_minus1 = syntaxReader.ReadCoeffAbsLevelMinus1();
                            coeff_sign_flag = syntaxReader.ReadCoeffSignFlag();

                            coeffLevel[i] = (int)(coeff_abs_level_minus1 + 1) * (1 - 2 * coeff_sign_flag.AsInt32());
                        }
                    }
                }
            }
        }
    }
}
