namespace ContentDotNet.Extensions.Video.H264.Components.IO.Rbsp
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Collections.Bits;
    using ContentDotNet.Collections.Generic;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using ContentDotNet.Extensions.Video.H264.Extensions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;
    using ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using ContentDotNet.Primitives;
    using System.Threading.Tasks;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.MacroblockTypes;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.PredictionModes;
    using Grabber = Utilities.SyntaxElementGrabber;

    internal delegate void ResidualBlockProcessor(List<int> coeffLevel, int start, int end, int maxNumCoeff);
    internal delegate Task AsyncResidualBlockProcessor(List<int> coeffLevel, int start, int end, int maxNumCoeff);

    internal class DefaultRbspReader : IH264IOReader
    {
        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly DefaultRbspReader Instance = new();

        private const int Extended_SAR = 255;
        private const string DecodingVariablesConstant = "CabacDecodingVariables";

        public H264MacroblockInfo? CurrentMacroblock { get; set; }

        public RbspDecRefPicMarking ReadDecRefPicMarking(H264RbspState rbspState, BitStreamReader reader)
        {
            if (rbspState.IdrPicFlag())
            {
                return new RbspDecRefPicMarking(
                    reader.ReadBit(),
                    reader.ReadBit(),
                    null,
                    null);
            }
            else
            {
                bool adaptive_ref_pic_marking_mode_flag = reader.ReadBit();
                if (adaptive_ref_pic_marking_mode_flag)
                {
                    List<MemoryManagementControl> mmc = [];
                    uint memory_management_control_operation;
                    do
                    {
                        memory_management_control_operation = reader.ReadUE();
                        uint? difference_of_pic_nums_minus1 = null;
                        uint? long_term_pic_num = null;
                        uint? long_term_frame_idx = null;
                        uint? max_long_term_frame_idx_plus1 = null;
                        if (memory_management_control_operation is 1 or 3)
                            difference_of_pic_nums_minus1 = reader.ReadUE();
                        if (memory_management_control_operation == 2)
                            long_term_pic_num = reader.ReadUE();
                        if (memory_management_control_operation is 3 or 6)
                            long_term_frame_idx = reader.ReadUE();
                        if (memory_management_control_operation == 4)
                            max_long_term_frame_idx_plus1 = reader.ReadUE();
                        mmc.Add(new MemoryManagementControl(memory_management_control_operation, difference_of_pic_nums_minus1, long_term_pic_num, long_term_frame_idx, max_long_term_frame_idx_plus1));
                    }
                    while (memory_management_control_operation != 0);
                    return new RbspDecRefPicMarking(null, null, true, mmc);
                }
                else
                {
                    return new RbspDecRefPicMarking(null, null, false, null);
                }
            }
        }

        public async Task<RbspDecRefPicMarking> ReadDecRefPicMarkingAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            if (rbspState.IdrPicFlag())
            {
                return new RbspDecRefPicMarking(
                    await reader.ReadBitAsync(),
                    await reader.ReadBitAsync(),
                    null,
                    null);
            }
            else
            {
                bool adaptive_ref_pic_marking_mode_flag = await reader.ReadBitAsync();
                if (adaptive_ref_pic_marking_mode_flag)
                {
                    List<MemoryManagementControl> mmc = [];
                    uint memory_management_control_operation;
                    do
                    {
                        memory_management_control_operation = await reader.ReadUEAsync();
                        uint? difference_of_pic_nums_minus1 = null;
                        uint? long_term_pic_num = null;
                        uint? long_term_frame_idx = null;
                        uint? max_long_term_frame_idx_plus1 = null;
                        if (memory_management_control_operation is 1 or 3)
                            difference_of_pic_nums_minus1 = await reader.ReadUEAsync();
                        if (memory_management_control_operation == 2)
                            long_term_pic_num = await reader.ReadUEAsync();
                        if (memory_management_control_operation is 3 or 6)
                            long_term_frame_idx = await reader.ReadUEAsync();
                        if (memory_management_control_operation == 4)
                            max_long_term_frame_idx_plus1 = await reader.ReadUEAsync();
                        mmc.Add(new MemoryManagementControl(memory_management_control_operation, difference_of_pic_nums_minus1, long_term_pic_num, long_term_frame_idx, max_long_term_frame_idx_plus1));
                    }
                    while (memory_management_control_operation != 0);
                    return new RbspDecRefPicMarking(null, null, true, mmc);
                }
                else
                {
                    return new RbspDecRefPicMarking(null, null, false, null);
                }
            }
        }

        public void ReadMacroblockLayer(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState)
        {
            CurrentMacroblock = mb;
            syntaxReader.MacroblockInfo = CurrentMacroblock;

            H264SliceType sliceType = H264SliceTypes.FetchSliceType(rbspState.SliceHeader!);
            mb.SliceType = sliceType;

            uint mb_type = 0;
            if (mb != B_Skip && mb != P_Skip)
                mb_type = syntaxReader.ReadMbType();
            mb.Rbsp.MbType = mb_type;

            if (Grabber.FetchEntropyCodingModeFlag(rbspState) == true)
            {
                if (mb.Rbsp.MbType >= 23)
                {
                    mb.SliceType = H264SliceType.I;
                    sliceType = H264SliceType.I;
                    CurrentMacroblock.SliceType = H264SliceType.I;
                    CurrentMacroblock.Rbsp.MbType -= 23;
                    mb_type -= 23;
                }
            }

            if (sliceType == H264SliceType.I /*I slice*/ &&
                mb_type == 25 /*I_PCM*/)
            {
                while (syntaxReader.Reader.GetState().BitPosition != 0)
                    _ = syntaxReader.Reader.ReadBit();

                List<byte> pcm_sample_luma = [];
                List<byte> pcm_sample_chroma = [];

                for (int i = 0; i < 256; i++)
                    pcm_sample_luma.Add((byte)syntaxReader.Reader.ReadBits((uint)rbspState.BitDepthY()));

                H264MacroblockChromaSizes chromaSizes = rbspState.ChromaMacroblockSizes();

                for (int i = 0; i < 2 * chromaSizes.MbWidthC * chromaSizes.MbHeightC; i++)
                    pcm_sample_chroma.Add((byte)syntaxReader.Reader.ReadBits((uint)rbspState.BitDepthC()));

                mb.Rbsp = new RbspMacroblockLayer(
                    mb_type, pcm_sample_luma, pcm_sample_chroma, null, false, null, 0, 0, null);
            }
            else
            {
                H264DecodingVariables? dv = GetDecodingVariables(syntaxReader);

                RbspSubMbPred? sub_mb_pred;
                bool transform_size_8x8_flag;
                RbspMbPred? mb_pred;
                RbspResidual? residual;
                uint coded_block_pattern;
                int mb_qp_delta;

                int noSubMbPartSizeLessThan8x8Flag = 1;
                if (mb == I_NxN &&
                    MacroblockTraits.MbPartPredMode(mb, 0) != Intra_16x16 &&
                    MacroblockTraits.NumMbPart((int)mb_type, CurrentMacroblock?.Inferred == true, false, sliceType) == 4)
                {
                    sub_mb_pred = ReadSubMbPred(syntaxReader, rbspState);
                    mb.Rbsp.SubMbPred = sub_mb_pred;
                    for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
                    {
                        if (dv != null)
                            dv.MbPartIdx = mbPartIdx;

                        if (MacroblockEquality.SubMacroblocksEqual((int)sub_mb_pred.SubMbType[mbPartIdx], sliceType, B_Direct_8x8))
                        {
                            if (MacroblockTraits.NumSubMbPart((int)sub_mb_pred.SubMbType[mbPartIdx], false, sliceType) > 1)
                                noSubMbPartSizeLessThan8x8Flag = 0;
                        }
                        else if (Grabber.GetDirect8x8InferenceFlag(rbspState))
                        {
                            noSubMbPartSizeLessThan8x8Flag = 0;
                        }
                    }
                }
                else
                {
                    // NOTE: The PPS might not always have transform_8x8_mode_flag, so
                    // if it doesn't exist, default to 0.

                    if (Grabber.FetchTransform8x8ModeFlag(rbspState) == true && mb == I_NxN)
                    {
                        transform_size_8x8_flag = syntaxReader.ReadTransformSize8X8Flag();
                        mb.Rbsp.TransformSize8x8Flag = transform_size_8x8_flag;
                    }

                    mb_pred = ReadMbPred(syntaxReader, rbspState);
                    mb.Rbsp.MbPred = mb_pred;
                }

                if (MacroblockTraits.MbPartPredMode(mb, 0) != Intra_16x16)
                {
                    coded_block_pattern = syntaxReader.ReadCodedBlockPattern();
                    mb.Rbsp.CodedBlockPattern = (int)coded_block_pattern;

                    if (mb.Rbsp.GetCodedBlockPatternLuma() > 0 &&
                        Grabber.FetchTransform8x8ModeFlag(rbspState) == true && mb != I_NxN &&
                        noSubMbPartSizeLessThan8x8Flag.AsBoolean() &&
                        (mb != B_Direct_16x16 || Grabber.GetDirect8x8InferenceFlag(rbspState)))
                    {
                        transform_size_8x8_flag = syntaxReader.ReadTransformSize8X8Flag();
                        mb.Rbsp.TransformSize8x8Flag = transform_size_8x8_flag;
                    }
                }

                if (mb.Rbsp.GetCodedBlockPatternLuma() > 0 || mb.Rbsp.GetCodedBlockPatternChroma() > 0 ||
                    MacroblockTraits.MbPartPredMode(mb, 0) == Intra_16x16)
                {
                    mb_qp_delta = syntaxReader.ReadMbQpDelta();
                    mb.Rbsp.MbQpDelta = mb_qp_delta;

                    residual = ReadResidual(syntaxReader, mb, rbspState);
                    mb.Rbsp.Residual = residual;
                }
            }
        }

        public async Task ReadMacroblockLayerAsync(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState)
        {
            CurrentMacroblock = mb;
            syntaxReader.MacroblockInfo = CurrentMacroblock;

            H264SliceType sliceType = H264SliceTypes.FetchSliceType(rbspState.SliceHeader!);
            mb.SliceType = sliceType;

            uint mb_type = 0;
            if (mb != B_Skip && mb != P_Skip)
                mb_type = syntaxReader.ReadMbType();
            mb.Rbsp.MbType = mb_type;

            if (sliceType == H264SliceType.I /*I slice*/ &&
                mb_type == 25 /*I_PCM*/)
            {
                while (syntaxReader.Reader.GetState().BitPosition != 0)
                    _ = syntaxReader.Reader.ReadBit();

                List<byte> pcm_sample_luma = [];
                List<byte> pcm_sample_chroma = [];

                for (int i = 0; i < 256; i++)
                    pcm_sample_luma.Add((byte)await syntaxReader.Reader.ReadBitsAsync((uint)rbspState.BitDepthY()));

                H264MacroblockChromaSizes chromaSizes = rbspState.ChromaMacroblockSizes();

                for (int i = 0; i < 2 * chromaSizes.MbWidthC * chromaSizes.MbHeightC; i++)
                    pcm_sample_chroma.Add((byte)await syntaxReader.Reader.ReadBitsAsync((uint)rbspState.BitDepthC()));

                mb.Rbsp = new RbspMacroblockLayer(
                    mb_type, pcm_sample_luma, pcm_sample_chroma, null, false, null, 0, 0, null);
            }
            else
            {
                H264DecodingVariables? dv = GetDecodingVariables(syntaxReader);

                RbspSubMbPred? sub_mb_pred;
                bool transform_size_8x8_flag;
                RbspMbPred? mb_pred;
                RbspResidual? residual;
                uint coded_block_pattern;
                int mb_qp_delta;

                int noSubMbPartSizeLessThan8x8Flag = 1;
                if (mb == I_NxN &&
                    MacroblockTraits.MbPartPredMode(mb, 0) != Intra_16x16 &&
                    MacroblockTraits.NumMbPart((int)mb_type, CurrentMacroblock?.Inferred == true, false, sliceType) == 4)
                {
                    sub_mb_pred = await ReadSubMbPredAsync(syntaxReader, rbspState);
                    mb.Rbsp.SubMbPred = sub_mb_pred;
                    for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
                    {
                        if (dv != null)
                            dv.MbPartIdx = mbPartIdx;

                        if (MacroblockEquality.SubMacroblocksEqual((int)sub_mb_pred.SubMbType[mbPartIdx], sliceType, B_Direct_8x8))
                        {
                            if (MacroblockTraits.NumSubMbPart((int)sub_mb_pred.SubMbType[mbPartIdx], false, sliceType) > 1)
                                noSubMbPartSizeLessThan8x8Flag = 0;
                        }
                        else if (Grabber.GetDirect8x8InferenceFlag(rbspState))
                        {
                            noSubMbPartSizeLessThan8x8Flag = 0;
                        }
                    }
                }
                else
                {
                    // NOTE: The PPS might not always have transform_8x8_mode_flag, so
                    // if it doesn't exist, default to 0.

                    if (Grabber.FetchTransform8x8ModeFlag(rbspState) == true && mb == I_NxN)
                    {
                        transform_size_8x8_flag = await syntaxReader.ReadTransformSize8X8FlagAsync();
                        mb.Rbsp.TransformSize8x8Flag = transform_size_8x8_flag;
                    }

                    mb_pred = await ReadMbPredAsync(syntaxReader, rbspState);
                    mb.Rbsp.MbPred = mb_pred;
                }

                if (MacroblockTraits.MbPartPredMode(mb, 0) != Intra_16x16)
                {
                    coded_block_pattern = await syntaxReader.ReadCodedBlockPatternAsync();
                    mb.Rbsp.CodedBlockPattern = (int)coded_block_pattern;

                    if (mb.Rbsp.GetCodedBlockPatternLuma() > 0 &&
                        Grabber.GetTransform8x8ModeFlag(rbspState) && mb != I_NxN &&
                        noSubMbPartSizeLessThan8x8Flag.AsBoolean() &&
                        (mb != B_Direct_16x16 || Grabber.GetDirect8x8InferenceFlag(rbspState)))
                    {
                        transform_size_8x8_flag = await syntaxReader.ReadTransformSize8X8FlagAsync();
                        mb.Rbsp.TransformSize8x8Flag = transform_size_8x8_flag;
                    }
                }

                if (mb.Rbsp.GetCodedBlockPatternLuma() > 0 || mb.Rbsp.GetCodedBlockPatternChroma() > 0 ||
                    MacroblockTraits.MbPartPredMode(mb, 0) == Intra_16x16)
                {
                    mb_qp_delta = await syntaxReader.ReadMbQpDeltaAsync();
                    mb.Rbsp.MbQpDelta = mb_qp_delta;

                    residual = await ReadResidualAsync(syntaxReader, mb, rbspState);
                    mb.Rbsp.Residual = residual;
                }
            }
        }

        public RbspMbPred ReadMbPred(IH264SyntaxReader syntaxReader, H264RbspState rbspState)
        {
            int mbpp = MacroblockTraits.MbPartPredMode(CurrentMacroblock!, 0);

            List<bool>? prev_intra_4x4_pred_mode_flag = null;
            List<bool>? prev_intra_8x8_pred_mode_flag = null;
            List<uint>? rem_intra_4x4_pred_mode = null;
            List<uint>? rem_intra_8x8_pred_mode = null;

            uint? intra_chroma_pred_mode = null;

            List<uint> ref_idx_l0 = [];
            List<uint> ref_idx_l1 = [];
            H264MotionVectorDifference mvd_l0 = new();
            H264MotionVectorDifference mvd_l1 = new();

            int num_ref_idx_l0_active_minus1 = (int?)Grabber.FetchNumRefIdxL0ActiveMinus1(rbspState) ?? 0;
            int num_ref_idx_l1_active_minus1 = (int?)Grabber.FetchNumRefIdxL1ActiveMinus1(rbspState) ?? 0;
            bool mb_field_decoding_flag = CurrentMacroblock!.MbFieldDecodingFlag;
            bool field_pic_flag = Grabber.FetchFieldPicFlag(rbspState) ?? false;

            H264DecodingVariables? variables = GetDecodingVariables(syntaxReader);

            if (variables != null)
            {
                variables.RefIdxL0 = ref_idx_l0;
                variables.RefIdxL1 = ref_idx_l1;
                variables.MvdL0 = mvd_l0;
                variables.MvdL1 = mvd_l1;
                variables.ChromaArrayType = rbspState.ChromaArrayType();
            }

            if (mbpp is Intra_4x4 or Intra_8x8 or Intra_16x16)
            {
                if (mbpp == Intra_4x4)
                {
                    prev_intra_4x4_pred_mode_flag = [];
                    rem_intra_4x4_pred_mode = [];

                    for (int i = 0; i < 16; i++)
                    {
                        if (variables != null)
                        {
                            CodedBlockFlagDerivationOptions cbfOptions = variables.CodedBlockFlagOptions;
                            cbfOptions.Luma4x4BlkIdx = i;
                            variables.CodedBlockFlagOptions = cbfOptions;
                        }

                        prev_intra_4x4_pred_mode_flag.Add(syntaxReader.ReadPrevIntra4x4PredModeFlag());
                        if (!prev_intra_4x4_pred_mode_flag[i])
                            rem_intra_4x4_pred_mode.Add(syntaxReader.ReadRemIntra4x4PredMode());
                        else
                            rem_intra_4x4_pred_mode.Add(0);
                    }
                }

                if (mbpp == Intra_8x8)
                {
                    prev_intra_8x8_pred_mode_flag = [];
                    rem_intra_8x8_pred_mode = [];

                    for (int i = 0; i < 4; i++)
                    {
                        if (variables != null)
                        {
                            CodedBlockFlagDerivationOptions cbfOptions = variables.CodedBlockFlagOptions;
                            cbfOptions.Luma8x8BlkIdx = i;
                            variables.CodedBlockFlagOptions = cbfOptions;
                        }

                        prev_intra_8x8_pred_mode_flag.Add(syntaxReader.ReadPrevIntra8x8PredModeFlag());
                        if (!prev_intra_8x8_pred_mode_flag[i])
                            rem_intra_8x8_pred_mode.Add(syntaxReader.ReadRemIntra8x8PredMode());
                        else
                            rem_intra_8x8_pred_mode.Add(0);
                    }
                }

                int chromaArrayType = rbspState.ChromaArrayType();
                if (chromaArrayType is 1 or 2)
                    intra_chroma_pred_mode = syntaxReader.ReadIntraChromaPredMode();
            }
            else if (mbpp != Direct)
            {
                int nmbp = MacroblockTraits.NumMbPart((int?)CurrentMacroblock?.Rbsp.MbType ?? 0, CurrentMacroblock?.Inferred == true, CurrentMacroblock?.Rbsp.TransformSize8x8Flag ?? false, CurrentMacroblock!.SliceType);

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if ((num_ref_idx_l0_active_minus1 > 0 ||
                         mb_field_decoding_flag != field_pic_flag) &&
                         MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L1)
                    {
                        ref_idx_l0[mbPartIdx] = syntaxReader.ReadRefIdxL0();
                    }
                }

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if ((num_ref_idx_l1_active_minus1 > 0 ||
                         mb_field_decoding_flag != field_pic_flag) &&
                         MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L0)
                    {
                        ref_idx_l1[mbPartIdx] = syntaxReader.ReadRefIdxL1();
                    }
                }

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if (MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L1)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l0[mbPartIdx, 0, compIdx] = syntaxReader.ReadMvdL0();
                        }
                    }
                }

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if (MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L0)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l1[mbPartIdx, 0, compIdx] = syntaxReader.ReadMvdL1();
                        }
                    }
                }
            }

            return new RbspMbPred(prev_intra_4x4_pred_mode_flag, rem_intra_4x4_pred_mode, prev_intra_8x8_pred_mode_flag, rem_intra_8x8_pred_mode, intra_chroma_pred_mode, ref_idx_l0, ref_idx_l1, mvd_l0.Raw, mvd_l1.Raw);
        }

        public async Task<RbspMbPred> ReadMbPredAsync(IH264SyntaxReader syntaxReader, H264RbspState rbspState)
        {
            int mbpp = MacroblockTraits.MbPartPredMode(CurrentMacroblock!, 0);

            List<bool>? prev_intra_4x4_pred_mode_flag = null;
            List<bool>? prev_intra_8x8_pred_mode_flag = null;
            List<uint>? rem_intra_4x4_pred_mode = null;
            List<uint>? rem_intra_8x8_pred_mode = null;

            uint? intra_chroma_pred_mode = null;

            List<uint> ref_idx_l0 = [];
            List<uint> ref_idx_l1 = [];
            H264MotionVectorDifference mvd_l0 = new();
            H264MotionVectorDifference mvd_l1 = new();

            int num_ref_idx_l0_active_minus1 = (int)Grabber.GetNumRefIdxL0ActiveMinus1(rbspState);
            int num_ref_idx_l1_active_minus1 = (int)Grabber.GetNumRefIdxL1ActiveMinus1(rbspState);
            bool mb_field_decoding_flag = CurrentMacroblock!.MbFieldDecodingFlag;
            bool field_pic_flag = Grabber.GetFieldPicFlag(rbspState);

            H264DecodingVariables? variables = GetDecodingVariables(syntaxReader);

            if (variables != null)
            {
                variables.RefIdxL0 = ref_idx_l0;
                variables.RefIdxL1 = ref_idx_l1;
                variables.MvdL0 = mvd_l0;
                variables.MvdL1 = mvd_l1;
                variables.ChromaArrayType = rbspState.ChromaArrayType();
            }

            if (mbpp is Intra_4x4 or Intra_8x8 or Intra_16x16)
            {
                if (mbpp == Intra_4x4)
                {
                    prev_intra_4x4_pred_mode_flag = [];
                    rem_intra_4x4_pred_mode = [];

                    for (int i = 0; i < 16; i++)
                    {
                        if (variables != null)
                        {
                            CodedBlockFlagDerivationOptions cbfOptions = variables.CodedBlockFlagOptions;
                            cbfOptions.Luma4x4BlkIdx = i;
                            variables.CodedBlockFlagOptions = cbfOptions;
                        }

                        prev_intra_4x4_pred_mode_flag.Add(await syntaxReader.ReadPrevIntra4x4PredModeFlagAsync());
                        if (!prev_intra_4x4_pred_mode_flag[i])
                            rem_intra_4x4_pred_mode.Add(await syntaxReader.ReadRemIntra4x4PredModeAsync());
                        else
                            rem_intra_4x4_pred_mode.Add(0);
                    }
                }

                if (mbpp == Intra_8x8)
                {
                    prev_intra_8x8_pred_mode_flag = [];
                    rem_intra_8x8_pred_mode = [];

                    for (int i = 0; i < 4; i++)
                    {
                        if (variables != null)
                        {
                            CodedBlockFlagDerivationOptions cbfOptions = variables.CodedBlockFlagOptions;
                            cbfOptions.Luma8x8BlkIdx = i;
                            variables.CodedBlockFlagOptions = cbfOptions;
                        }

                        prev_intra_8x8_pred_mode_flag.Add(await syntaxReader.ReadPrevIntra8x8PredModeFlagAsync());
                        if (!prev_intra_8x8_pred_mode_flag[i])
                            rem_intra_8x8_pred_mode.Add(await syntaxReader.ReadRemIntra8x8PredModeAsync());
                        else
                            rem_intra_8x8_pred_mode.Add(0);
                    }
                }

                int chromaArrayType = rbspState.ChromaArrayType();
                if (chromaArrayType is 1 or 2)
                    intra_chroma_pred_mode = await syntaxReader.ReadIntraChromaPredModeAsync();
            }
            else if (mbpp != Direct)
            {
                int nmbp = MacroblockTraits.NumMbPart((int?)CurrentMacroblock?.Rbsp.MbType ?? 0, CurrentMacroblock?.Inferred == true, CurrentMacroblock?.Rbsp.TransformSize8x8Flag ?? false, CurrentMacroblock!.SliceType);

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if ((num_ref_idx_l0_active_minus1 > 0 ||
                         mb_field_decoding_flag != field_pic_flag) &&
                         MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L1)
                    {
                        ref_idx_l0[mbPartIdx] = await syntaxReader.ReadRefIdxL0Async();
                    }
                }

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if ((num_ref_idx_l1_active_minus1 > 0 ||
                         mb_field_decoding_flag != field_pic_flag) &&
                         MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L0)
                    {
                        ref_idx_l1[mbPartIdx] = await syntaxReader.ReadRefIdxL1Async();
                    }
                }

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if (MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L1)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l0[mbPartIdx, 0, compIdx] = await syntaxReader.ReadMvdL0Async();
                        }
                    }
                }

                for (int mbPartIdx = 0; mbPartIdx < nmbp; mbPartIdx++)
                {
                    if (variables != null)
                        variables.MbPartIdx = mbPartIdx;

                    if (MacroblockTraits.MbPartPredMode(CurrentMacroblock!, mbPartIdx) != Pred_L0)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l1[mbPartIdx, 0, compIdx] = await syntaxReader.ReadMvdL1Async();
                        }
                    }
                }
            }

            return new RbspMbPred(prev_intra_4x4_pred_mode_flag, rem_intra_4x4_pred_mode, prev_intra_8x8_pred_mode_flag, rem_intra_8x8_pred_mode, intra_chroma_pred_mode, ref_idx_l0, ref_idx_l1, mvd_l0.Raw, mvd_l1.Raw);
        }

        public MvcRbspRefPicListMvcModification ReadMvcRbspRefPicListMvcModification(H264RbspState rbspState, BitStreamReader reader)
        {
            bool? ref_pic_list_modification_flag_l0 = null;
            bool? ref_pic_list_modification_flag_l1 = null;
            List<MvcRefPicListModificationEntry> l0 = [];
            List<MvcRefPicListModificationEntry> l1 = [];
            int slice_type = (int?)rbspState.SliceHeader?.SliceType ?? 0;

            if (slice_type % 5 != 2 && slice_type % 5 != 4)
            {
                ref_pic_list_modification_flag_l0 = reader.ReadBit();
                if (ref_pic_list_modification_flag_l0 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = reader.ReadUE();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        uint? abs_diff_view_idx_minus1 = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = reader.ReadUE();
                        else if (modification_of_pic_nums_idc == 2)
                            long_term_pic_num = reader.ReadUE();
                        else if (modification_of_pic_nums_idc == 4 ||
                                 modification_of_pic_nums_idc == 5)
                            abs_diff_view_idx_minus1 = reader.ReadUE();
                        l0.Add(new(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num, abs_diff_view_idx_minus1));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }

            if (slice_type % 5 == 1)
            {
                ref_pic_list_modification_flag_l1 = reader.ReadBit();
                if (ref_pic_list_modification_flag_l1 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = reader.ReadUE();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        uint? abs_diff_view_idx_minus1 = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = reader.ReadUE();
                        else if (modification_of_pic_nums_idc == 2)
                            long_term_pic_num = reader.ReadUE();
                        else if (modification_of_pic_nums_idc == 4 ||
                                 modification_of_pic_nums_idc == 5)
                            abs_diff_view_idx_minus1 = reader.ReadUE();
                        l1.Add(new(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num, abs_diff_view_idx_minus1));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }

            return new(ref_pic_list_modification_flag_l0, l0, ref_pic_list_modification_flag_l1, l1);
        }

        public async Task<MvcRbspRefPicListMvcModification> ReadMvcRbspRefPicListMvcModificationAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            bool? ref_pic_list_modification_flag_l0 = null;
            bool? ref_pic_list_modification_flag_l1 = null;
            List<MvcRefPicListModificationEntry> l0 = [];
            List<MvcRefPicListModificationEntry> l1 = [];
            int slice_type = (int?)rbspState.SliceHeader?.SliceType ?? 0;

            if (slice_type % 5 != 2 && slice_type % 5 != 4)
            {
                ref_pic_list_modification_flag_l0 = await reader.ReadBitAsync();
                if (ref_pic_list_modification_flag_l0 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = await reader.ReadUEAsync();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        uint? abs_diff_view_idx_minus1 = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = await reader.ReadUEAsync();
                        else if (modification_of_pic_nums_idc == 2)
                            long_term_pic_num = await reader.ReadUEAsync();
                        else if (modification_of_pic_nums_idc == 4 ||
                                 modification_of_pic_nums_idc == 5)
                            abs_diff_view_idx_minus1 = await reader.ReadUEAsync();
                        l0.Add(new(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num, abs_diff_view_idx_minus1));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }

            if (slice_type % 5 == 1)
            {
                ref_pic_list_modification_flag_l1 = await reader.ReadBitAsync();
                if (ref_pic_list_modification_flag_l1 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = await reader.ReadUEAsync();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        uint? abs_diff_view_idx_minus1 = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = await reader.ReadUEAsync();
                        else if (modification_of_pic_nums_idc == 2)
                            long_term_pic_num = await reader.ReadUEAsync();
                        else if (modification_of_pic_nums_idc == 4 ||
                                 modification_of_pic_nums_idc == 5)
                            abs_diff_view_idx_minus1 = await reader.ReadUEAsync();
                        l1.Add(new(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num, abs_diff_view_idx_minus1));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }

            return new(ref_pic_list_modification_flag_l0, l0, ref_pic_list_modification_flag_l1, l1);
        }

        public RbspPictureParameterSet ReadPPSData(H264RbspState rbspState, BitStreamReader reader)
        {
            // Just take the risk and parse even if the SPS isn't available :D
            //if (rbspState.SequenceParameterSetData == null)
            //    throw new H264Exception("Cannot decode PPS without SPS");

            uint pic_parameter_set_id = reader.ReadUE();
            uint seq_parameter_set_id = reader.ReadUE();
            bool entropy_coding_mode_flag = reader.ReadBit(); // 1 - CABAC, 0 - CAVLC
            bool bottom_field_pic_order_in_frame_present_flag = reader.ReadBit();
            uint num_slice_groups_minus1 = reader.ReadUE();

            uint? slice_group_map_type = null;
            List<uint> run_length_minus1 = [];
            List<uint> top_left = [];
            List<uint> bottom_right = [];
            bool? slice_group_change_direction_flag = null;
            uint? slice_group_change_rate_minus1 = null;
            uint? pic_size_in_map_units_minus1 = null;
            List<uint> slice_group_id = [];

            if (num_slice_groups_minus1 > 0)
            {
                slice_group_map_type = reader.ReadUE();
                if (slice_group_map_type == 0)
                {
                    for (int iGroup = 0; iGroup <= num_slice_groups_minus1; iGroup++)
                    {
                        run_length_minus1.Add(reader.ReadUE());
                    }
                }
                else if (slice_group_map_type == 2)
                {
                    for (int iGroup = 0; iGroup < num_slice_groups_minus1; iGroup++)
                    {
                        top_left.Add(reader.ReadUE());
                        bottom_right.Add(reader.ReadUE());
                    }
                }
                else if (slice_group_map_type is 3 or 4 or 5)
                {
                    slice_group_change_direction_flag = reader.ReadBit();
                    slice_group_change_rate_minus1 = reader.ReadUE();
                }
                else if (slice_group_map_type == 6)
                {
                    pic_size_in_map_units_minus1 = reader.ReadUE();
                    for (int i = 0; i <= pic_size_in_map_units_minus1; i++)
                        slice_group_id.Add(reader.ReadBits((uint)Math.Ceiling(Math.Log2(num_slice_groups_minus1 + 1))));
                }
            }

            uint num_ref_idx_l0_default_active_minus1 = reader.ReadUE();
            uint num_ref_idx_l1_default_active_minus1 = reader.ReadUE();
            bool weighted_pred_flag = reader.ReadBit();
            uint weighted_bipred_idc = reader.ReadBits(2);
            int pic_init_qp_minus26 = reader.ReadSE();
            int pic_init_qs_minus26 = reader.ReadSE();
            int chroma_qp_index_offset = reader.ReadSE();
            bool deblocking_filter_control_present_flag = reader.ReadBit();
            bool constrained_intra_pred_flag = reader.ReadBit();
            bool redundant_pic_cnt_present_flag = reader.ReadBit();

            bool? transform_8x8_mode_flag = null;
            bool? pic_scaling_matrix_present_flag = null;
            bool[] pic_scaling_list_present_flag = new bool[12];
            int? second_chroma_qp_index_offset = null;

            #region Scaling List Initialization Code

            List<List<int>> ScalingList4x4 = [];
            List<List<int>> ScalingList8x8 = [];
            bool[] UseDefaultScalingMatrix4x4Flag = new bool[6];
            bool[] UseDefaultScalingMatrix8x8Flag = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                ScalingList4x4.Add([.. new int[16]]);
                ScalingList8x8.Add([.. new int[64]]);
            }

            #endregion

            if (rbspState.MoreRbspData())
            {
                transform_8x8_mode_flag = reader.ReadBit();
                pic_scaling_matrix_present_flag = reader.ReadBit();
                if (pic_scaling_matrix_present_flag == true)
                {
                    for (int i = 0; i < 6 + (((int)rbspState.SequenceParameterSetData!.ChromaFormatIdc != 3) ? 2 : 6) * (transform_8x8_mode_flag == true ? 1 : 0); i++)
                    {
                        pic_scaling_list_present_flag[i] = reader.ReadBit();
                        if (pic_scaling_list_present_flag[i])
                        {
                            if (i < 6)
                                ParseScalingList(reader, ScalingList4x4[i], 16, ref UseDefaultScalingMatrix4x4Flag[i]);
                            else
                                ParseScalingList(reader, ScalingList8x8[i - 6], 64, ref UseDefaultScalingMatrix8x8Flag[i - 6]);
                        }
                    }
                }
                second_chroma_qp_index_offset = reader.ReadSE();
            }

            while (rbspState.MoreRbspData())
                _ = reader.ReadBit();

            return new RbspPictureParameterSet(
                pic_parameter_set_id, seq_parameter_set_id, entropy_coding_mode_flag, bottom_field_pic_order_in_frame_present_flag, num_slice_groups_minus1,
                slice_group_map_type, [.. run_length_minus1], [.. top_left], [.. bottom_right], slice_group_change_direction_flag, slice_group_change_rate_minus1,
                pic_size_in_map_units_minus1, [.. slice_group_id], num_ref_idx_l0_default_active_minus1, num_ref_idx_l1_default_active_minus1,
                weighted_pred_flag, weighted_bipred_idc, pic_init_qp_minus26, pic_init_qs_minus26, chroma_qp_index_offset, deblocking_filter_control_present_flag,
                constrained_intra_pred_flag, redundant_pic_cnt_present_flag, transform_8x8_mode_flag, pic_scaling_matrix_present_flag,
                pic_scaling_list_present_flag, ScalingList4x4, ScalingList8x8, UseDefaultScalingMatrix4x4Flag, UseDefaultScalingMatrix8x8Flag, second_chroma_qp_index_offset);
        }

        public async Task<RbspPictureParameterSet> ReadPPSDataAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            if (rbspState.SequenceParameterSetData == null)
                throw new H264Exception("Cannot decode PPS without SPS");
            int chroma_format_idc = (int)rbspState.SequenceParameterSetData.ChromaFormatIdc;

            uint pic_parameter_set_id = await reader.ReadUEAsync();
            uint seq_parameter_set_id = await reader.ReadUEAsync();
            bool entropy_coding_mode_flag = await reader.ReadBitAsync(); // 1 - CABAC, 0 - CAVLC
            bool bottom_field_pic_order_in_frame_present_flag = await reader.ReadBitAsync();
            uint num_slice_groups_minus1 = await reader.ReadUEAsync();

            uint? slice_group_map_type = null;
            List<uint> run_length_minus1 = [];
            List<uint> top_left = [];
            List<uint> bottom_right = [];
            bool? slice_group_change_direction_flag = null;
            uint? slice_group_change_rate_minus1 = null;
            uint? pic_size_in_map_units_minus1 = null;
            List<uint> slice_group_id = [];

            if (num_slice_groups_minus1 > 0)
            {
                slice_group_map_type = await reader.ReadUEAsync();
                if (slice_group_map_type == 0)
                {
                    for (int iGroup = 0; iGroup <= num_slice_groups_minus1; iGroup++)
                    {
                        run_length_minus1.Add(await reader.ReadUEAsync());
                    }
                }
                else if (slice_group_map_type == 2)
                {
                    for (int iGroup = 0; iGroup < num_slice_groups_minus1; iGroup++)
                    {
                        top_left.Add(await reader.ReadUEAsync());
                        bottom_right.Add(await reader.ReadUEAsync());
                    }
                }
                else if (slice_group_map_type is 3 or 4 or 5)
                {
                    slice_group_change_direction_flag = await reader.ReadBitAsync();
                    slice_group_change_rate_minus1 = await reader.ReadUEAsync();
                }
                else if (slice_group_map_type == 6)
                {
                    pic_size_in_map_units_minus1 = await reader.ReadUEAsync();
                    for (int i = 0; i <= pic_size_in_map_units_minus1; i++)
                        slice_group_id.Add(await reader.ReadBitsAsync((uint)Math.Ceiling(Math.Log2(num_slice_groups_minus1 + 1))));
                }
            }

            uint num_ref_idx_l0_default_active_minus1 = await reader.ReadUEAsync();
            uint num_ref_idx_l1_default_active_minus1 = await reader.ReadUEAsync();
            bool weighted_pred_flag = await reader.ReadBitAsync();
            uint weighted_bipred_idc = await reader.ReadBitsAsync(2);
            int pic_init_qp_minus26 = await reader.ReadSEAsync();
            int pic_init_qs_minus26 = await reader.ReadSEAsync();
            int chroma_qp_index_offset = await reader.ReadSEAsync();
            bool deblocking_filter_control_present_flag = await reader.ReadBitAsync();
            bool constrained_intra_pred_flag = await reader.ReadBitAsync();
            bool redundant_pic_cnt_present_flag = await reader.ReadBitAsync();

            bool? transform_8x8_mode_flag = null;
            bool? pic_scaling_matrix_present_flag = null;
            bool[] pic_scaling_list_present_flag = new bool[12];
            int? second_chroma_qp_index_offset = null;

            #region Scaling List Initialization Code

            List<List<int>> ScalingList4x4 = [];
            List<List<int>> ScalingList8x8 = [];
            bool[] UseDefaultScalingMatrix4x4Flag = new bool[6];
            bool[] UseDefaultScalingMatrix8x8Flag = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                ScalingList4x4.Add([.. new int[16]]);
                ScalingList8x8.Add([.. new int[64]]);
            }

            #endregion

            if (rbspState.MoreRbspData())
            {
                transform_8x8_mode_flag = await reader.ReadBitAsync();
                pic_scaling_matrix_present_flag = await reader.ReadBitAsync();
                if (pic_scaling_matrix_present_flag == true)
                {
                    for (int i = 0; i < 6 + ((chroma_format_idc != 3) ? 2 : 6) * (transform_8x8_mode_flag == true ? 1 : 0); i++)
                    {
                        pic_scaling_list_present_flag[i] = await reader.ReadBitAsync();
                        if (pic_scaling_list_present_flag[i])
                        {
                            if (i < 6)
                                UseDefaultScalingMatrix4x4Flag[i] = await ParseScalingListAsync(reader, ScalingList4x4[i], 16, UseDefaultScalingMatrix4x4Flag[i]);
                            else
                                UseDefaultScalingMatrix8x8Flag[i - 6] = await ParseScalingListAsync(reader, ScalingList8x8[i - 6], 64, UseDefaultScalingMatrix8x8Flag[i - 6]);
                        }
                    }
                }
                second_chroma_qp_index_offset = await reader.ReadSEAsync();
            }

            while (rbspState.MoreRbspData())
                _ = await reader.ReadBitAsync();

            return new RbspPictureParameterSet(
                pic_parameter_set_id, seq_parameter_set_id, entropy_coding_mode_flag, bottom_field_pic_order_in_frame_present_flag, num_slice_groups_minus1,
                slice_group_map_type, [.. run_length_minus1], [.. top_left], [.. bottom_right], slice_group_change_direction_flag, slice_group_change_rate_minus1,
                pic_size_in_map_units_minus1, [.. slice_group_id], num_ref_idx_l0_default_active_minus1, num_ref_idx_l1_default_active_minus1,
                weighted_pred_flag, weighted_bipred_idc, pic_init_qp_minus26, pic_init_qs_minus26, chroma_qp_index_offset, deblocking_filter_control_present_flag,
                constrained_intra_pred_flag, redundant_pic_cnt_present_flag, transform_8x8_mode_flag, pic_scaling_matrix_present_flag,
                pic_scaling_list_present_flag, ScalingList4x4, ScalingList8x8, UseDefaultScalingMatrix4x4Flag, UseDefaultScalingMatrix8x8Flag, second_chroma_qp_index_offset);
        }

        public RbspPredWeightTable ReadPredWeightTable(H264RbspState rbspState, BitStreamReader reader)
        {
            uint luma_log2_weight_denom = reader.ReadUE();
            uint? chroma_log2_weight_denom = null;
            if (rbspState.ChromaArrayType() != 0)
                chroma_log2_weight_denom = reader.ReadUE();
            List<PredWeightL0Entry> l0 = [];
            List<PredWeightL1Entry>? l1 = null;
            for (int i = 0; i <= (rbspState.PictureParameterSet?.NumRefIdxL1DefaultActiveMinus1 ?? 0); i++)
            {
                l0.Add(ReadEntry0());
            }
            if ((rbspState.SliceHeader?.SliceType % 5) == 1)
            {
                l1 = [];
                for (int i = 0; i <= (rbspState.PictureParameterSet?.NumRefIdxL1DefaultActiveMinus1 ?? 0); i++)
                {
                    l1.Add(ReadEntry1());
                }
            }
            return new RbspPredWeightTable(luma_log2_weight_denom, chroma_log2_weight_denom, l0, l1);

            PredWeightL0Entry ReadEntry0()
            {
                bool luma_weight_l0_flag = reader.ReadBit();
                int? luma_weight_l0 = null;
                int? luma_offset_l0 = null;
                if (luma_weight_l0_flag)
                {
                    luma_weight_l0 = reader.ReadSE();
                    luma_offset_l0 = reader.ReadSE();
                }
                bool? chroma_weight_l0_flag = null;
                int[]? chroma_weight_l0 = null;
                int[]? chroma_offset_l0 = null;
                if (rbspState.ChromaArrayType() != 0)
                {
                    chroma_weight_l0_flag = reader.ReadBit();
                    if (chroma_weight_l0_flag == true)
                    {
                        chroma_weight_l0 = new int[2];
                        chroma_offset_l0 = new int[2];
                        for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                        {
                            chroma_weight_l0[iCbCr] = reader.ReadSE();
                            chroma_offset_l0[iCbCr] = reader.ReadSE();
                        }
                    }
                }
                return new(luma_weight_l0_flag, luma_weight_l0, luma_offset_l0, chroma_weight_l0_flag, chroma_weight_l0, chroma_offset_l0);
            }

            PredWeightL1Entry ReadEntry1()
            {
                bool luma_weight_l1_flag = reader.ReadBit();
                int? luma_weight_l1 = null;
                int? luma_offset_l1 = null;
                if (luma_weight_l1_flag)
                {
                    luma_weight_l1 = reader.ReadSE();
                    luma_offset_l1 = reader.ReadSE();
                }
                bool? chroma_weight_l1_flag = null;
                int[]? chroma_weight_l1 = null;
                int[]? chroma_offset_l1 = null;
                if (rbspState.ChromaArrayType() != 0)
                {
                    chroma_weight_l1_flag = reader.ReadBit();
                    if (chroma_weight_l1_flag == true)
                    {
                        chroma_weight_l1 = new int[2];
                        chroma_offset_l1 = new int[2];
                        for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                        {
                            chroma_weight_l1[iCbCr] = reader.ReadSE();
                            chroma_offset_l1[iCbCr] = reader.ReadSE();
                        }
                    }
                }
                return new(luma_weight_l1_flag, luma_weight_l1, luma_offset_l1, chroma_weight_l1_flag, chroma_weight_l1, chroma_offset_l1);
            }
        }

        public async Task<RbspPredWeightTable> ReadPredWeightTableAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            uint luma_log2_weight_denom = await reader.ReadUEAsync();
            uint? chroma_log2_weight_denom = null;
            if (rbspState.ChromaArrayType() != 0)
                chroma_log2_weight_denom = await reader.ReadUEAsync();
            List<PredWeightL0Entry> l0 = [];
            List<PredWeightL1Entry>? l1 = null;
            for (int i = 0; i <= (rbspState.PictureParameterSet?.NumRefIdxL1DefaultActiveMinus1 ?? 0); i++)
            {
                l0.Add(await ReadEntry0());
            }
            if ((rbspState.SliceHeader?.SliceType % 5) == 1)
            {
                l1 = [];
                for (int i = 0; i <= (rbspState.PictureParameterSet?.NumRefIdxL1DefaultActiveMinus1 ?? 0); i++)
                {
                    l1.Add(await ReadEntry1());
                }
            }
            return new RbspPredWeightTable(luma_log2_weight_denom, chroma_log2_weight_denom, l0, l1);

            async Task<PredWeightL0Entry> ReadEntry0()
            {
                bool luma_weight_l0_flag = await reader.ReadBitAsync();
                int? luma_weight_l0 = null;
                int? luma_offset_l0 = null;
                if (luma_weight_l0_flag)
                {
                    luma_weight_l0 = await reader.ReadSEAsync();
                    luma_offset_l0 = await reader.ReadSEAsync();
                }
                bool? chroma_weight_l0_flag = null;
                int[]? chroma_weight_l0 = null;
                int[]? chroma_offset_l0 = null;
                if (rbspState.ChromaArrayType() != 0)
                {
                    chroma_weight_l0_flag = await reader.ReadBitAsync();
                    if (chroma_weight_l0_flag == true)
                    {
                        chroma_weight_l0 = new int[2];
                        chroma_offset_l0 = new int[2];
                        for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                        {
                            chroma_weight_l0[iCbCr] = await reader.ReadSEAsync();
                            chroma_offset_l0[iCbCr] = await reader.ReadSEAsync();
                        }
                    }
                }
                return new(luma_weight_l0_flag, luma_weight_l0, luma_offset_l0, chroma_weight_l0_flag, chroma_weight_l0, chroma_offset_l0);
            }

            async Task<PredWeightL1Entry> ReadEntry1()
            {
                bool luma_weight_l1_flag = await reader.ReadBitAsync();
                int? luma_weight_l1 = null;
                int? luma_offset_l1 = null;
                if (luma_weight_l1_flag)
                {
                    luma_weight_l1 = await reader.ReadSEAsync();
                    luma_offset_l1 = await reader.ReadSEAsync();
                }
                bool? chroma_weight_l1_flag = null;
                int[]? chroma_weight_l1 = null;
                int[]? chroma_offset_l1 = null;
                if (rbspState.ChromaArrayType() != 0)
                {
                    chroma_weight_l1_flag = await reader.ReadBitAsync();
                    if (chroma_weight_l1_flag == true)
                    {
                        chroma_weight_l1 = new int[2];
                        chroma_offset_l1 = new int[2];
                        for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                        {
                            chroma_weight_l1[iCbCr] = await reader.ReadSEAsync();
                            chroma_offset_l1[iCbCr] = await reader.ReadSEAsync();
                        }
                    }
                }
                return new(luma_weight_l1_flag, luma_weight_l1, luma_offset_l1, chroma_weight_l1_flag, chroma_weight_l1, chroma_offset_l1);
            }
        }

        public RbspRefPicListModification ReadRefPicListModification(H264RbspState rbspState, BitStreamReader reader)
        {
            bool? ref_pic_list_modification_flag_l0 = null;
            bool? ref_pic_list_modification_flag_l1 = null;
            List<RefPicListModificationEntry> l0 = [];
            List<RefPicListModificationEntry> l1 = [];
            int slice_type = (int?)rbspState.SliceHeader?.SliceType ?? 0;

            if (slice_type % 5 != 2 && slice_type % 5 != 4)
            {
                ref_pic_list_modification_flag_l0 = reader.ReadBit();
                if (ref_pic_list_modification_flag_l0 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = reader.ReadUE();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = reader.ReadUE();
                        else if (modification_of_pic_nums_idc is 2)
                            long_term_pic_num = reader.ReadUE();
                        l0.Add(new RefPicListModificationEntry(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }
            if (slice_type % 5 == 1)
            {
                ref_pic_list_modification_flag_l1 = reader.ReadBit();
                if (ref_pic_list_modification_flag_l1 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = reader.ReadUE();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = reader.ReadUE();
                        else if (modification_of_pic_nums_idc is 2)
                            long_term_pic_num = reader.ReadUE();
                        l1.Add(new RefPicListModificationEntry(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }
            return new RbspRefPicListModification(ref_pic_list_modification_flag_l0, l0, ref_pic_list_modification_flag_l1, l1);
        }

        public async Task<RbspRefPicListModification> ReadRefPicListModificationAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            bool? ref_pic_list_modification_flag_l0 = null;
            bool? ref_pic_list_modification_flag_l1 = null;
            List<RefPicListModificationEntry> l0 = [];
            List<RefPicListModificationEntry> l1 = [];
            int slice_type = (int?)rbspState.SliceHeader?.SliceType ?? 0;

            if (slice_type % 5 != 2 && slice_type % 5 != 4)
            {
                ref_pic_list_modification_flag_l0 = await reader.ReadBitAsync();
                if (ref_pic_list_modification_flag_l0 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = await reader.ReadUEAsync();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = await reader.ReadUEAsync();
                        else if (modification_of_pic_nums_idc is 2)
                            long_term_pic_num = await reader.ReadUEAsync();
                        l0.Add(new RefPicListModificationEntry(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }
            if (slice_type % 5 == 1)
            {
                ref_pic_list_modification_flag_l1 = await reader.ReadBitAsync();
                if (ref_pic_list_modification_flag_l1 == true)
                {
                    uint modification_of_pic_nums_idc;
                    do
                    {
                        modification_of_pic_nums_idc = await reader.ReadUEAsync();
                        uint? abs_diff_pic_num_minus1 = null;
                        uint? long_term_pic_num = null;
                        if (modification_of_pic_nums_idc == 0 ||
                            modification_of_pic_nums_idc == 1)
                            abs_diff_pic_num_minus1 = await reader.ReadUEAsync();
                        else if (modification_of_pic_nums_idc is 2)
                            long_term_pic_num = await reader.ReadUEAsync();
                        l1.Add(new RefPicListModificationEntry(modification_of_pic_nums_idc, abs_diff_pic_num_minus1, long_term_pic_num));
                    }
                    while (modification_of_pic_nums_idc != 3);
                }
            }
            return new RbspRefPicListModification(ref_pic_list_modification_flag_l0, l0, ref_pic_list_modification_flag_l1, l1);
        }

        public RbspResidual ReadResidual(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState)
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

            H264DecodingVariables? dv = GetDecodingVariables(syntaxReader);

            Ref<bool> coded_block_flag = new();

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

                receiveCbfForDC = (x) => CbfChromaDCLevel.Add(x);

                for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                {
                    if (dv != null)
                        dv.CodedBlockFlagOptions.ICbCr = iCbCr;

                    if ((mb.Rbsp.GetCodedBlockPatternChroma() & 3).AsBoolean() && startIdx == 0)
                    {
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

                ResidualLuma(CbIntra16x16DCLevel, CbIntra16x16ACLevel, CbLevel4x4, CbLevel8x8, startIdx, endIdx);

                blockType = ResidualBlockType.Cr16x16DCLevel;
                blockBase = blockType;

                receiveCbfForDC = (x) => CbfCrIntra16x16DCLevel = x;
                receiveCbfForAC = CbfCrIntra16x16ACLevel.Add;
                receiveCbfForLL4 = CbfCrLevel4x4.Add;
                receiveCbfForLL8 = CbfCrLevel8x8.Add;

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
                                for (int i = 0; i < 16; i++)
                                {
                                    blockType = blockBase + 3;
                                    level8x8.Value[i8x8][4 * i + i4x4] = level4x4.Value[i8x8 * 4 + i4x4][i];
                                    receiveCbfForLL8(false);
                                }
                            }
                        }
                    }
                    else if ((mb.Rbsp.GetCodedBlockPatternLuma() & (1 << i8x8)).AsBoolean())
                    {
                        blockType = blockBase + 3;
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

                if (dv != null)
                    dv.LevelListIndex++;
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
                        if (dv != null)
                            dv.LevelListIndex = i;

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
                                dv.LevelListIndex = i;
                            }

                            coeff_abs_level_minus1 = syntaxReader.ReadCoeffAbsLevelMinus1();
                            coeff_sign_flag = syntaxReader.ReadCoeffSignFlag();

                            coeffLevel[i] = (int)(coeff_abs_level_minus1 + 1) * (1 - 2 * coeff_sign_flag.AsInt32());
                        }
                    }
                }
            }
        }

        public async Task<RbspResidual> ReadResidualAsync(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState)
        {
            throw new NotImplementedException("Async residuals");
        }

        public RbspSliceHeader ReadSliceHeader(H264RbspState rbspState, BitStreamReader reader)
        {
            //
            // SPS FIELDS
            //

            int log2_max_frame_num_minus4 = (int?)rbspState.SequenceParameterSetData?.Log2MaxFrameNumMinus4 ?? 0;
            bool frame_mbs_only_flag = rbspState.SequenceParameterSetData?.FrameMbsOnlyFlag ?? false;
            int pic_order_cnt_type = (int?)rbspState.SequenceParameterSetData?.PicOrderCntType ?? 0;
            int log2_max_pic_order_cnt_lsb_minus4 = (int?)rbspState.SequenceParameterSetData?.Log2MaxPicOrderCntLsbMinus4 ?? 0;
            bool delta_pic_order_always_zero_flag = rbspState.SequenceParameterSetData?.DeltaPicOrderAlwaysZeroFlag ?? false;
            bool redundant_pic_cnt_present_flag = rbspState.PictureParameterSet?.RedundantPicCntPresentFlag ?? false;

            //
            // PPS FIELDS
            //

            bool bottom_field_pic_order_in_frame_present_flag = rbspState.PictureParameterSet?.BottomFieldPicOrderInFramePresentFlag ?? false;
            bool weighted_pred_flag = rbspState.PictureParameterSet?.WeightedPredFlag ?? false;
            int weighted_bipred_idc = (int?)rbspState.PictureParameterSet?.WeightedBiPredIdc ?? 0;
            bool entropy_coding_mode_flag = rbspState.PictureParameterSet?.EntropyCodingModeFlag ?? false;
            bool deblocking_filter_control_present_flag = rbspState.PictureParameterSet?.DeblockingFilterControlPresentFlag ?? false;
            uint num_slice_groups_minus1 = rbspState.PictureParameterSet?.NumSliceGroupsMinus1 ?? 0;
            uint slice_group_map_type = rbspState.PictureParameterSet?.SliceGroupMapType ?? 0;

            //
            // NAL FIELDS
            //

            int nal_unit_type = (int?)rbspState.NalUnit?.NalUnitType ?? 0;
            int nal_ref_idc = (int?)rbspState.NalUnit?.NalRefIdc ?? 0;

            //
            // SLICE HEADER PARSING
            //

            uint first_mb_in_slice = reader.ReadUE();
            uint slice_type = reader.ReadUE();
            uint pic_parameter_set_id = reader.ReadUE();
            uint? colour_plane_id = null;
            if (rbspState?.SequenceParameterSetData?.SeparateColourPlaneFlag == true)
                colour_plane_id = reader.ReadBits(2);

            uint frame_num = reader.ReadBits((uint)log2_max_frame_num_minus4 + 4);
            bool? field_pic_flag = null;
            bool? bottom_field_flag = null;
            if (!frame_mbs_only_flag)
            {
                field_pic_flag = reader.ReadBit();
                if (field_pic_flag == true)
                    bottom_field_flag = reader.ReadBit();
            }

            uint? idr_pic_id = null;
            if (rbspState.IdrPicFlag())
                idr_pic_id = reader.ReadUE();

            uint? pic_order_cnt_lsb = null;
            int? delta_pic_order_cnt_bottom = null;

            if (pic_order_cnt_type == 0)
            {
                pic_order_cnt_lsb = reader.ReadBits((uint)log2_max_pic_order_cnt_lsb_minus4 + 4);
                if (bottom_field_pic_order_in_frame_present_flag && field_pic_flag == false)
                    delta_pic_order_cnt_bottom = reader.ReadSE();
            }

            int[] delta_pic_order_cnt = new int[2];
            if (pic_order_cnt_type == 1 && !delta_pic_order_always_zero_flag)
            {
                delta_pic_order_cnt[0] = reader.ReadSE();
                if (bottom_field_pic_order_in_frame_present_flag && field_pic_flag == false)
                    delta_pic_order_cnt[1] = reader.ReadSE();
            }

            uint redundant_pic_cnt = 0;
            if (redundant_pic_cnt_present_flag)
                redundant_pic_cnt = reader.ReadUE();

            bool? direct_spatial_mv_pred_flag = null;
            if (H264SliceTypes.IsB(slice_type))
                direct_spatial_mv_pred_flag = reader.ReadBit();

            bool? num_ref_idx_active_override_flag = null;
            uint? num_ref_idx_l0_active_minus1 = null;
            uint? num_ref_idx_l1_active_minus1 = null;
            if (H264SliceTypes.IsP(slice_type) || H264SliceTypes.IsSP(slice_type) || H264SliceTypes.IsB(slice_type))
            {
                num_ref_idx_active_override_flag = reader.ReadBit();
                if (num_ref_idx_active_override_flag == true)
                {
                    num_ref_idx_l0_active_minus1 = reader.ReadUE();
                    if (H264SliceTypes.IsB(slice_type))
                    {
                        num_ref_idx_l1_active_minus1 = reader.ReadUE();
                    }
                }
            }

            MvcRbspRefPicListMvcModification? refPicListMvcModification = null;
            RbspRefPicListModification? refPicListModification = null;
            RbspPredWeightTable? predWeightTable = null;
            RbspDecRefPicMarking? decRefPicMarking = null;

            if (nal_unit_type is 20 or 21) refPicListMvcModification = ReadMvcRbspRefPicListMvcModification(rbspState!, reader);
            else refPicListModification = ReadRefPicListModification(rbspState!, reader);

            if ((weighted_pred_flag && (H264SliceTypes.IsP(slice_type) || H264SliceTypes.IsSP(slice_type))) ||
                (weighted_bipred_idc == 1 && H264SliceTypes.IsB(slice_type)))
            {
                predWeightTable = ReadPredWeightTable(rbspState!, reader);
            }

            if (nal_ref_idc != 0)
                decRefPicMarking = ReadDecRefPicMarking(rbspState!, reader);

            uint? cabac_init_idc = null;
            if (entropy_coding_mode_flag && !H264SliceTypes.IsI(slice_type) && !H264SliceTypes.IsSI(slice_type))
                cabac_init_idc = reader.ReadUE();

            int slice_qp_delta = reader.ReadSE();

            bool? sp_for_switch_flag = null;
            int? slice_qs_delta = null;

            if (H264SliceTypes.IsSI(slice_type) || H264SliceTypes.IsSP(slice_type))
            {
                if (H264SliceTypes.IsSP(slice_type))
                    sp_for_switch_flag = reader.ReadBit();
                slice_qs_delta = reader.ReadSE();
            }

            uint? disable_deblocking_filter_idc = null;
            int? slice_alpha_c0_offset_div2 = null;
            int? slice_beta_offset_div2 = null;

            if (deblocking_filter_control_present_flag)
            {
                disable_deblocking_filter_idc = reader.ReadUE();
                if (disable_deblocking_filter_idc != 1)
                {
                    slice_alpha_c0_offset_div2 = reader.ReadSE();
                    slice_beta_offset_div2 = reader.ReadSE();
                }
            }

            uint? slice_group_change_cycle = null;
            if (num_slice_groups_minus1 > 0 && slice_group_map_type >= 3 && slice_group_map_type <= 5)
                slice_group_change_cycle = reader.ReadUE();

            return new RbspSliceHeader(
                first_mb_in_slice, slice_type, pic_parameter_set_id, colour_plane_id, frame_num, field_pic_flag, bottom_field_flag, idr_pic_id,
                pic_order_cnt_lsb, delta_pic_order_cnt_bottom, delta_pic_order_cnt, redundant_pic_cnt, direct_spatial_mv_pred_flag, num_ref_idx_active_override_flag,
                num_ref_idx_l0_active_minus1, num_ref_idx_l1_active_minus1, refPicListMvcModification, refPicListModification, predWeightTable, decRefPicMarking, cabac_init_idc,
                slice_qp_delta, sp_for_switch_flag, slice_qs_delta, disable_deblocking_filter_idc, slice_alpha_c0_offset_div2, slice_beta_offset_div2, slice_group_change_cycle);
        }

        public async Task<RbspSliceHeader> ReadSliceHeaderAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            //
            // SPS FIELDS
            //

            int log2_max_frame_num_minus4 = (int?)rbspState.SequenceParameterSetData?.Log2MaxFrameNumMinus4 ?? 0;
            bool frame_mbs_only_flag = rbspState.SequenceParameterSetData?.FrameMbsOnlyFlag ?? false;
            int pic_order_cnt_type = (int?)rbspState.SequenceParameterSetData?.PicOrderCntType ?? 0;
            int log2_max_pic_order_cnt_lsb_minus4 = (int?)rbspState.SequenceParameterSetData?.Log2MaxPicOrderCntLsbMinus4 ?? 0;
            bool delta_pic_order_always_zero_flag = rbspState.SequenceParameterSetData?.DeltaPicOrderAlwaysZeroFlag ?? false;
            bool redundant_pic_cnt_present_flag = rbspState.PictureParameterSet?.RedundantPicCntPresentFlag ?? false;

            //
            // PPS FIELDS
            //

            bool bottom_field_pic_order_in_frame_present_flag = rbspState.PictureParameterSet?.BottomFieldPicOrderInFramePresentFlag ?? false;
            bool weighted_pred_flag = rbspState.PictureParameterSet?.WeightedPredFlag ?? false;
            int weighted_bipred_idc = (int?)rbspState.PictureParameterSet?.WeightedBiPredIdc ?? 0;
            bool entropy_coding_mode_flag = rbspState.PictureParameterSet?.EntropyCodingModeFlag ?? false;
            bool deblocking_filter_control_present_flag = rbspState.PictureParameterSet?.DeblockingFilterControlPresentFlag ?? false;
            uint num_slice_groups_minus1 = rbspState.PictureParameterSet?.NumSliceGroupsMinus1 ?? 0;
            uint slice_group_map_type = rbspState.PictureParameterSet?.SliceGroupMapType ?? 0;

            //
            // NAL FIELDS
            //

            int nal_unit_type = (int?)rbspState.NalUnit?.NalUnitType ?? 0;
            int nal_ref_idc = (int?)rbspState.NalUnit?.NalRefIdc ?? 0;

            //
            // SLICE HEADER PARSING
            //

            uint first_mb_in_slice = await reader.ReadUEAsync();
            uint slice_type = await reader.ReadUEAsync();
            uint pic_parameter_set_id = await reader.ReadUEAsync();
            uint? colour_plane_id = null;
            if (rbspState?.SequenceParameterSetData?.SeparateColourPlaneFlag == true)
                colour_plane_id = await reader.ReadBitsAsync(2);

            uint frame_num = await reader.ReadBitsAsync((uint)log2_max_frame_num_minus4 + 4);
            bool? field_pic_flag = null;
            bool? bottom_field_flag = null;
            if (!frame_mbs_only_flag)
            {
                field_pic_flag = await reader.ReadBitAsync();
                if (field_pic_flag == true)
                    bottom_field_flag = await reader.ReadBitAsync();
            }

            uint? idr_pic_id = null;
            if (rbspState.IdrPicFlag())
                idr_pic_id = await reader.ReadUEAsync();

            uint? pic_order_cnt_lsb = null;
            int? delta_pic_order_cnt_bottom = null;

            if (pic_order_cnt_type == 0)
            {
                pic_order_cnt_lsb = await reader.ReadBitsAsync((uint)log2_max_pic_order_cnt_lsb_minus4 + 4);
                if (bottom_field_pic_order_in_frame_present_flag && field_pic_flag == false)
                    delta_pic_order_cnt_bottom = await reader.ReadSEAsync();
            }

            int[] delta_pic_order_cnt = new int[2];
            if (pic_order_cnt_type == 1 && !delta_pic_order_always_zero_flag)
            {
                delta_pic_order_cnt[0] = await reader.ReadSEAsync();
                if (bottom_field_pic_order_in_frame_present_flag && field_pic_flag == false)
                    delta_pic_order_cnt[1] = await reader.ReadSEAsync();
            }

            uint redundant_pic_cnt = 0;
            if (redundant_pic_cnt_present_flag)
                redundant_pic_cnt = await reader.ReadUEAsync();

            bool? direct_spatial_mv_pred_flag = null;
            if (H264SliceTypes.IsB(slice_type))
                direct_spatial_mv_pred_flag = await reader.ReadBitAsync();

            bool? num_ref_idx_active_override_flag = null;
            uint? num_ref_idx_l0_active_minus1 = null;
            uint? num_ref_idx_l1_active_minus1 = null;
            if (H264SliceTypes.IsP(slice_type) || H264SliceTypes.IsSP(slice_type) || H264SliceTypes.IsB(slice_type))
            {
                num_ref_idx_active_override_flag = await reader.ReadBitAsync();
                if (num_ref_idx_active_override_flag == true)
                {
                    num_ref_idx_l0_active_minus1 = await reader.ReadUEAsync();
                    if (H264SliceTypes.IsB(slice_type))
                    {
                        num_ref_idx_l1_active_minus1 = await reader.ReadUEAsync();
                    }
                }
            }

            MvcRbspRefPicListMvcModification? refPicListMvcModification = null;
            RbspRefPicListModification? refPicListModification = null;
            RbspPredWeightTable? predWeightTable = null;
            RbspDecRefPicMarking? decRefPicMarking = null;

            if (nal_unit_type is 20 or 21) refPicListMvcModification = ReadMvcRbspRefPicListMvcModification(rbspState!, reader);
            else refPicListModification = ReadRefPicListModification(rbspState!, reader);

            if ((weighted_pred_flag && (H264SliceTypes.IsP(slice_type) || H264SliceTypes.IsSP(slice_type))) ||
                (weighted_bipred_idc == 1 && H264SliceTypes.IsB(slice_type)))
            {
                predWeightTable = ReadPredWeightTable(rbspState!, reader);
            }

            if (nal_ref_idc != 0)
                decRefPicMarking = ReadDecRefPicMarking(rbspState!, reader);

            uint? cabac_init_idc = null;
            if (entropy_coding_mode_flag && !H264SliceTypes.IsI(slice_type) && !H264SliceTypes.IsSI(slice_type))
                cabac_init_idc = await reader.ReadUEAsync();

            int slice_qp_delta = await reader.ReadSEAsync();

            bool? sp_for_switch_flag = null;
            int? slice_qs_delta = null;

            if (H264SliceTypes.IsSI(slice_type) || H264SliceTypes.IsSP(slice_type))
            {
                if (H264SliceTypes.IsSP(slice_type))
                    sp_for_switch_flag = await reader.ReadBitAsync();
                slice_qs_delta = await reader.ReadSEAsync();
            }

            uint? disable_deblocking_filter_idc = null;
            int? slice_alpha_c0_offset_div2 = null;
            int? slice_beta_offset_div2 = null;

            if (deblocking_filter_control_present_flag)
            {
                disable_deblocking_filter_idc = await reader.ReadUEAsync();
                if (disable_deblocking_filter_idc != 1)
                {
                    slice_alpha_c0_offset_div2 = await reader.ReadSEAsync();
                    slice_beta_offset_div2 = await reader.ReadSEAsync();
                }
            }

            uint? slice_group_change_cycle = null;
            if (num_slice_groups_minus1 > 0 && slice_group_map_type >= 3 && slice_group_map_type <= 5)
                slice_group_change_cycle = await reader.ReadUEAsync();

            return new RbspSliceHeader(
                first_mb_in_slice, slice_type, pic_parameter_set_id, colour_plane_id, frame_num, field_pic_flag, bottom_field_flag, idr_pic_id,
                pic_order_cnt_lsb, delta_pic_order_cnt_bottom, delta_pic_order_cnt, redundant_pic_cnt, direct_spatial_mv_pred_flag, num_ref_idx_active_override_flag,
                num_ref_idx_l0_active_minus1, num_ref_idx_l1_active_minus1, refPicListMvcModification, refPicListModification, predWeightTable, decRefPicMarking, cabac_init_idc,
                slice_qp_delta, sp_for_switch_flag, slice_qs_delta, disable_deblocking_filter_idc, slice_alpha_c0_offset_div2, slice_beta_offset_div2, slice_group_change_cycle);
        }

        public RbspSequenceParameterSetData ReadSPSData(H264RbspState rbspState, BitStreamReader reader)
        {
            #region Scaling List Initialization Code

            List<List<int>> ScalingList4x4 = [];
            List<List<int>> ScalingList8x8 = [];
            bool[] UseDefaultScalingMatrix4x4Flag = new bool[6];
            bool[] UseDefaultScalingMatrix8x8Flag = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                ScalingList4x4.Add([.. new int[16]]);
                ScalingList8x8.Add([.. new int[64]]);
            }

            #endregion

            uint profile_idc = reader.ReadBits(8);
            bool constraint_set0_flag = reader.ReadBit();
            bool constraint_set1_flag = reader.ReadBit();
            bool constraint_set2_flag = reader.ReadBit();
            bool constraint_set3_flag = reader.ReadBit();
            bool constraint_set4_flag = reader.ReadBit();
            bool constraint_set5_flag = reader.ReadBit();
            uint reserved_zero_2bits = reader.ReadBits(2);
            uint level_idc = reader.ReadBits(8);
            uint seq_parameter_set_id = reader.ReadUE();

            uint chroma_format_idc = default;
            bool separate_colour_plane_flag = default;
            uint bit_depth_luma_minus8 = default;
            uint bit_depth_chroma_minus8 = default;
            bool qpprime_y_zero_transform_bypass_flag = default;
            bool seq_scaling_matrix_present_flag = false;
            bool[] seq_scaling_list_present_flag = new bool[12];

            if (profile_idc is 100 or 110 or 244 or 44 or 83 or 86 or 118 or 128 or 138 or 139 or 134 or 135)
            {
                chroma_format_idc = reader.ReadUE();
                if (chroma_format_idc == 3)
                    separate_colour_plane_flag = reader.ReadBit();
                bit_depth_luma_minus8 = reader.ReadUE();
                bit_depth_chroma_minus8 = reader.ReadUE();
                qpprime_y_zero_transform_bypass_flag = reader.ReadBit();
                seq_scaling_matrix_present_flag = reader.ReadBit();
                if (seq_scaling_matrix_present_flag)
                {
                    for (int i = 0; i < ((chroma_format_idc != 3) ? 8 : 12); i++)
                    {
                        seq_scaling_list_present_flag[i] = reader.ReadBit();
                        if (seq_scaling_list_present_flag[i])
                        {
                            if (i < 6)
                                ParseScalingList(reader, ScalingList4x4[i], 16, ref UseDefaultScalingMatrix4x4Flag[i]);
                            else
                                ParseScalingList(reader, ScalingList8x8[i - 6], 64, ref UseDefaultScalingMatrix8x8Flag[i - 6]);
                        }
                    }
                }
            }

            uint log2_max_frame_num_minus4 = reader.ReadUE();
            uint pic_order_cnt_type = reader.ReadUE();

            uint log2_max_pic_order_cnt_lsb_minus4 = default;

            bool delta_pic_order_always_zero_flag = default;
            int offset_for_non_ref_pic = default;
            int offset_for_top_to_bottom_field = default;
            uint num_ref_frames_in_pic_order_cnt_cycle = default;
            List<uint> offset_for_ref_frame = [];

            if (pic_order_cnt_type == 0)
            {
                log2_max_pic_order_cnt_lsb_minus4 = reader.ReadUE();
            }
            else if (pic_order_cnt_type == 1)
            {
                delta_pic_order_always_zero_flag = reader.ReadBit();
                offset_for_non_ref_pic = reader.ReadSE();
                offset_for_top_to_bottom_field = reader.ReadSE();
                num_ref_frames_in_pic_order_cnt_cycle = reader.ReadUE();
                for (int i = 0; i < num_ref_frames_in_pic_order_cnt_cycle; i++)
                    offset_for_ref_frame.Add(reader.ReadUE());
            }

            uint max_num_ref_frames = reader.ReadUE();
            bool gaps_in_frame_num_value_allowed_flag = reader.ReadBit();
            uint pic_width_in_mbs_minus1 = reader.ReadUE();
            uint pic_height_in_map_units_minus1 = reader.ReadUE();

            bool frame_mbs_only_flag = reader.ReadBit();
            bool mb_adaptive_frame_field_flag = default;
            if (!frame_mbs_only_flag)
                mb_adaptive_frame_field_flag = reader.ReadBit();

            bool direct_8x8_inference_flag = reader.ReadBit();
            bool frame_cropping_flag = reader.ReadBit();

            uint frame_crop_left_offset = 0,
                frame_crop_right_offset = 0,
                frame_crop_top_offset = 0,
                frame_crop_bottom_offset = 0;
            if (frame_cropping_flag)
            {
                frame_crop_left_offset = reader.ReadUE();
                frame_crop_right_offset = reader.ReadUE();
                frame_crop_top_offset = reader.ReadUE();
                frame_crop_bottom_offset = reader.ReadUE();
            }
            bool vui_parameters_present_flag = reader.ReadBit();
            RbspVuiParameters? vui_parameters = null;
            if (vui_parameters_present_flag)
                vui_parameters = ReadVuiParameters();
            return new RbspSequenceParameterSetData(
                profile_idc, constraint_set0_flag, constraint_set1_flag, constraint_set2_flag, constraint_set3_flag, constraint_set4_flag, constraint_set5_flag,
                reserved_zero_2bits, level_idc, seq_parameter_set_id, chroma_format_idc, separate_colour_plane_flag, bit_depth_luma_minus8, bit_depth_chroma_minus8,
                qpprime_y_zero_transform_bypass_flag, seq_scaling_matrix_present_flag, seq_scaling_list_present_flag,
                new ScalingMatrixData(ScalingList4x4, ScalingList8x8, [.. UseDefaultScalingMatrix4x4Flag], [.. UseDefaultScalingMatrix8x8Flag]),
                log2_max_frame_num_minus4, pic_order_cnt_type, log2_max_pic_order_cnt_lsb_minus4, delta_pic_order_always_zero_flag, offset_for_non_ref_pic, offset_for_top_to_bottom_field,
                num_ref_frames_in_pic_order_cnt_cycle, offset_for_ref_frame.AsInt32Array(), max_num_ref_frames, gaps_in_frame_num_value_allowed_flag, pic_width_in_mbs_minus1,
                pic_height_in_map_units_minus1, frame_mbs_only_flag, mb_adaptive_frame_field_flag, direct_8x8_inference_flag, frame_cropping_flag, frame_crop_left_offset, frame_crop_right_offset,
                frame_crop_top_offset, frame_crop_bottom_offset, vui_parameters_present_flag, vui_parameters);

            RbspVuiParameters ReadVuiParameters()
            {
                bool aspect_ratio_info_present_flag = reader.ReadBit();
                uint? aspect_ratio_idc = null;
                uint? sar_width = null;
                uint? sar_height = null;
                if (aspect_ratio_info_present_flag)
                {
                    aspect_ratio_idc = reader.ReadBits(8);
                    if (aspect_ratio_idc == Extended_SAR)
                    {
                        sar_width = reader.ReadBits(16);
                        sar_height = reader.ReadBits(16);
                    }
                }
                bool overscan_info_present_flag = reader.ReadBit();
                bool overscan_info_appropriate_flag = false;
                if (overscan_info_present_flag)
                    overscan_info_appropriate_flag = reader.ReadBit();
                bool video_signal_type_present_flag = reader.ReadBit();
                uint? video_format = null;
                bool? video_full_range_flag = null;
                bool? colour_description_present_flag = null;
                uint? colour_primaries = null;
                uint? transfer_characteristics = null;
                uint? matrix_coefficients = 0;
                if (video_signal_type_present_flag)
                {
                    video_format = reader.ReadBits(3);
                    video_full_range_flag = reader.ReadBit();
                    colour_description_present_flag = reader.ReadBit();
                    if (colour_description_present_flag == true)
                    {
                        colour_primaries = reader.ReadBits(8);
                        transfer_characteristics = reader.ReadBits(8);
                        matrix_coefficients = reader.ReadBits(8);
                    }
                }
                bool chroma_loc_info_present_flag = reader.ReadBit();
                uint? chroma_sample_loc_type_top_field = null;
                uint? chroma_sample_loc_type_bottom_field = null;
                if (chroma_loc_info_present_flag)
                {
                    chroma_sample_loc_type_top_field = reader.ReadUE();
                    chroma_sample_loc_type_bottom_field = reader.ReadUE();
                }
                bool timing_info_present_flag = reader.ReadBit();
                uint? num_units_in_tick = null;
                uint? time_scale = null;
                bool? fixed_frame_rate_flag = null;
                if (timing_info_present_flag)
                {
                    num_units_in_tick = reader.ReadBits(32);
                    time_scale = reader.ReadBits(32);
                    fixed_frame_rate_flag = reader.ReadBit();
                }
                RbspHrdParameters? nalHRD = null;
                RbspHrdParameters? vclHRD = null;
                bool nal_hrd_parameters_present_flag = reader.ReadBit();
                if (nal_hrd_parameters_present_flag)
                    nalHRD = ReadHrdParameters();
                bool vcl_hrd_parameters_present_flag = reader.ReadBit();
                if (vcl_hrd_parameters_present_flag)
                    vclHRD = ReadHrdParameters();
                bool low_delay_hrd_flag = false;
                if (nal_hrd_parameters_present_flag || vcl_hrd_parameters_present_flag)
                    low_delay_hrd_flag = reader.ReadBit();
                bool pic_struct_present_flag = reader.ReadBit();
                bool bitstream_restriction_flag = reader.ReadBit();
                bool motion_vectors_over_pic_boundaries_flag = false;
                uint? max_bytes_per_pic_denom = null;
                uint? max_bits_per_mb_denom = null;
                uint? log2_max_mv_length_horizontal = null;
                uint? log2_max_mv_length_vertical = null;
                uint? max_num_reorder_frames = null;
                uint? max_dec_frame_buffering = null;
                if (bitstream_restriction_flag)
                {
                    motion_vectors_over_pic_boundaries_flag = reader.ReadBit();
                    max_bytes_per_pic_denom = reader.ReadUE();
                    max_bits_per_mb_denom = reader.ReadUE();
                    log2_max_mv_length_horizontal = reader.ReadUE();
                    log2_max_mv_length_vertical = reader.ReadUE();
                    max_num_reorder_frames = reader.ReadUE();
                    max_dec_frame_buffering = reader.ReadUE();
                }
                return new RbspVuiParameters(
                    aspect_ratio_info_present_flag, aspect_ratio_idc, sar_width, sar_height, overscan_info_present_flag, overscan_info_appropriate_flag, video_signal_type_present_flag,
                    video_format, video_full_range_flag, colour_description_present_flag, colour_primaries, transfer_characteristics, matrix_coefficients, chroma_loc_info_present_flag,
                    chroma_sample_loc_type_top_field, chroma_sample_loc_type_bottom_field, timing_info_present_flag, num_units_in_tick, time_scale, fixed_frame_rate_flag,
                    nal_hrd_parameters_present_flag, nalHRD, vcl_hrd_parameters_present_flag, vclHRD, low_delay_hrd_flag, pic_struct_present_flag, bitstream_restriction_flag,
                    motion_vectors_over_pic_boundaries_flag, max_bytes_per_pic_denom, max_bits_per_mb_denom, log2_max_mv_length_horizontal, log2_max_mv_length_vertical,
                    max_num_reorder_frames, max_dec_frame_buffering);
            }

            RbspHrdParameters ReadHrdParameters()
            {
                uint cpb_cnt_minus1 = reader.ReadUE();
                uint bit_rate_scale = reader.ReadBits(4);
                uint cpb_size_scale = reader.ReadBits(4);
                List<uint> bit_rate_value_minus1 = [];
                List<uint> cpb_size_value_minus1 = [];
                BitCollection cbr_flag = [];
                for (int SchedSchelIdx = 0; SchedSchelIdx <= cpb_cnt_minus1; SchedSchelIdx++)
                {
                    bit_rate_value_minus1.Add(reader.ReadUE());
                    cpb_size_value_minus1.Add(reader.ReadUE());
                    cbr_flag.Add(reader.ReadBit());
                }
                uint initial_cpb_removal_delay_length_minus1 = reader.ReadBits(5);
                uint cpb_removal_delay_length_minus1 = reader.ReadBits(5);
                uint dpb_output_delay_length_minus1 = reader.ReadBits(5);
                uint time_offset_length = reader.ReadBits(5);

                return new RbspHrdParameters(cpb_cnt_minus1, bit_rate_scale, cpb_size_scale, [.. bit_rate_value_minus1], [.. cpb_size_value_minus1], cbr_flag, initial_cpb_removal_delay_length_minus1, cpb_removal_delay_length_minus1, dpb_output_delay_length_minus1, time_offset_length);
            }
        }

        public async Task<RbspSequenceParameterSetData> ReadSPSDataAsync(H264RbspState rbspState, BitStreamReader reader)
        {
            #region Scaling List Initialization Code

            List<List<int>> ScalingList4x4 = [];
            List<List<int>> ScalingList8x8 = [];
            bool[] UseDefaultScalingMatrix4x4Flag = new bool[6];
            bool[] UseDefaultScalingMatrix8x8Flag = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                ScalingList4x4.Add([.. new int[16]]);
                ScalingList8x8.Add([.. new int[64]]);
            }

            #endregion

            uint profile_idc = await reader.ReadBitsAsync(8);
            bool constraint_set0_flag = await reader.ReadBitAsync();
            bool constraint_set1_flag = await reader.ReadBitAsync();
            bool constraint_set2_flag = await reader.ReadBitAsync();
            bool constraint_set3_flag = await reader.ReadBitAsync();
            bool constraint_set4_flag = await reader.ReadBitAsync();
            bool constraint_set5_flag = await reader.ReadBitAsync();
            uint reserved_zero_2bits = await reader.ReadBitsAsync(2);
            uint level_idc = await reader.ReadBitsAsync(8);
            uint seq_parameter_set_id = await reader.ReadUEAsync();

            uint chroma_format_idc = default;
            bool separate_colour_plane_flag = default;
            uint bit_depth_luma_minus8 = default;
            uint bit_depth_chroma_minus8 = default;
            bool qpprime_y_zero_transform_bypass_flag = default;
            bool seq_scaling_matrix_present_flag = false;
            bool[] seq_scaling_list_present_flag = new bool[12];

            if (profile_idc is 100 or 110 or 244 or 44 or 83 or 86 or 118 or 128 or 138 or 139 or 134 or 135)
            {
                chroma_format_idc = await reader.ReadUEAsync();
                if (chroma_format_idc == 3)
                    separate_colour_plane_flag = await reader.ReadBitAsync();
                bit_depth_luma_minus8 = await reader.ReadUEAsync();
                bit_depth_chroma_minus8 = await reader.ReadUEAsync();
                qpprime_y_zero_transform_bypass_flag = await reader.ReadBitAsync();
                seq_scaling_matrix_present_flag = await reader.ReadBitAsync();
                if (seq_scaling_matrix_present_flag)
                {
                    for (int i = 0; i < ((chroma_format_idc != 3) ? 8 : 12); i++)
                    {
                        seq_scaling_list_present_flag[i] = await reader.ReadBitAsync();
                        if (seq_scaling_list_present_flag[i])
                        {
                            if (i < 6)
                            {
                                UseDefaultScalingMatrix4x4Flag[i] = await ParseScalingListAsync(reader, ScalingList4x4[i], 16, UseDefaultScalingMatrix4x4Flag[i]);
                            }
                            else
                            {
                                UseDefaultScalingMatrix8x8Flag[i] = await ParseScalingListAsync(reader, ScalingList8x8[i - 6], 64, UseDefaultScalingMatrix8x8Flag[i - 6]);
                            }
                        }
                    }
                }
            }

            uint log2_max_frame_num_minus4 = await reader.ReadUEAsync();
            uint pic_order_cnt_type = await reader.ReadUEAsync();

            uint log2_max_pic_order_cnt_lsb_minus4 = default;

            bool delta_pic_order_always_zero_flag = default;
            int offset_for_non_ref_pic = default;
            int offset_for_top_to_bottom_field = default;
            uint num_ref_frames_in_pic_order_cnt_cycle = default;
            List<uint> offset_for_ref_frame = [];

            if (pic_order_cnt_type == 0)
            {
                log2_max_pic_order_cnt_lsb_minus4 = await reader.ReadUEAsync();
            }
            else if (pic_order_cnt_type == 1)
            {
                delta_pic_order_always_zero_flag = await reader.ReadBitAsync();
                offset_for_non_ref_pic = reader.ReadSE();
                offset_for_top_to_bottom_field = reader.ReadSE();
                num_ref_frames_in_pic_order_cnt_cycle = await reader.ReadUEAsync();
                for (int i = 0; i < num_ref_frames_in_pic_order_cnt_cycle; i++)
                    offset_for_ref_frame.Add(await reader.ReadUEAsync());
            }

            uint max_num_ref_frames = await reader.ReadUEAsync();
            bool gaps_in_frame_num_value_allowed_flag = await reader.ReadBitAsync();
            uint pic_width_in_mbs_minus1 = await reader.ReadUEAsync();
            uint pic_height_in_map_units_minus1 = await reader.ReadUEAsync();

            bool frame_mbs_only_flag = await reader.ReadBitAsync();
            bool mb_adaptive_frame_field_flag = default;
            if (!frame_mbs_only_flag)
                mb_adaptive_frame_field_flag = await reader.ReadBitAsync();

            bool direct_8x8_inference_flag = await reader.ReadBitAsync();
            bool frame_cropping_flag = await reader.ReadBitAsync();

            uint frame_crop_left_offset = 0,
                frame_crop_right_offset = 0,
                frame_crop_top_offset = 0,
                frame_crop_bottom_offset = 0;
            if (frame_cropping_flag)
            {
                frame_crop_left_offset = await reader.ReadUEAsync();
                frame_crop_right_offset = await reader.ReadUEAsync();
                frame_crop_top_offset = await reader.ReadUEAsync();
                frame_crop_bottom_offset = await reader.ReadUEAsync();
            }
            bool vui_parameters_present_flag = await reader.ReadBitAsync();
            RbspVuiParameters? vui_parameters = null;
            if (vui_parameters_present_flag)
                vui_parameters = await ReadVuiParameters();
            return new RbspSequenceParameterSetData(
                profile_idc, constraint_set0_flag, constraint_set1_flag, constraint_set2_flag, constraint_set3_flag, constraint_set4_flag, constraint_set5_flag,
                reserved_zero_2bits, level_idc, seq_parameter_set_id, chroma_format_idc, separate_colour_plane_flag, bit_depth_luma_minus8, bit_depth_chroma_minus8,
                qpprime_y_zero_transform_bypass_flag, seq_scaling_matrix_present_flag, seq_scaling_list_present_flag,
                new ScalingMatrixData(ScalingList4x4, ScalingList8x8, [.. UseDefaultScalingMatrix4x4Flag], [.. UseDefaultScalingMatrix8x8Flag]),
                log2_max_frame_num_minus4, pic_order_cnt_type, log2_max_pic_order_cnt_lsb_minus4, delta_pic_order_always_zero_flag, offset_for_non_ref_pic, offset_for_top_to_bottom_field,
                num_ref_frames_in_pic_order_cnt_cycle, offset_for_ref_frame.AsInt32Array(), max_num_ref_frames, gaps_in_frame_num_value_allowed_flag, pic_width_in_mbs_minus1,
                pic_height_in_map_units_minus1, frame_mbs_only_flag, mb_adaptive_frame_field_flag, direct_8x8_inference_flag, frame_cropping_flag, frame_crop_left_offset, frame_crop_right_offset,
                frame_crop_top_offset, frame_crop_bottom_offset, vui_parameters_present_flag, vui_parameters);

            async Task<RbspVuiParameters> ReadVuiParameters()
            {
                bool aspect_ratio_info_present_flag = await reader.ReadBitAsync();
                uint? aspect_ratio_idc = null;
                uint? sar_width = null;
                uint? sar_height = null;
                if (aspect_ratio_info_present_flag)
                {
                    aspect_ratio_idc = await reader.ReadBitsAsync(8);
                    if (aspect_ratio_idc == Extended_SAR)
                    {
                        sar_width = await reader.ReadBitsAsync(16);
                        sar_height = await reader.ReadBitsAsync(16);
                    }
                }
                bool overscan_info_present_flag = await reader.ReadBitAsync();
                bool overscan_info_appropriate_flag = false;
                if (overscan_info_present_flag)
                    overscan_info_appropriate_flag = await reader.ReadBitAsync();
                bool video_signal_type_present_flag = await reader.ReadBitAsync();
                uint? video_format = null;
                bool? video_full_range_flag = null;
                bool? colour_description_present_flag = null;
                uint? colour_primaries = null;
                uint? transfer_characteristics = null;
                uint? matrix_coefficients = 0;
                if (video_signal_type_present_flag)
                {
                    video_format = await reader.ReadBitsAsync(3);
                    video_full_range_flag = await reader.ReadBitAsync();
                    colour_description_present_flag = await reader.ReadBitAsync();
                    if (colour_description_present_flag == true)
                    {
                        colour_primaries = await reader.ReadBitsAsync(8);
                        transfer_characteristics = await reader.ReadBitsAsync(8);
                        matrix_coefficients = await reader.ReadBitsAsync(8);
                    }
                }
                bool chroma_loc_info_present_flag = await reader.ReadBitAsync();
                uint? chroma_sample_loc_type_top_field = null;
                uint? chroma_sample_loc_type_bottom_field = null;
                if (chroma_loc_info_present_flag)
                {
                    chroma_sample_loc_type_top_field = await reader.ReadUEAsync();
                    chroma_sample_loc_type_bottom_field = await reader.ReadUEAsync();
                }
                bool timing_info_present_flag = await reader.ReadBitAsync();
                uint? num_units_in_tick = null;
                uint? time_scale = null;
                bool? fixed_frame_rate_flag = null;
                if (timing_info_present_flag)
                {
                    num_units_in_tick = await reader.ReadBitsAsync(32);
                    time_scale = await reader.ReadBitsAsync(32);
                    fixed_frame_rate_flag = await reader.ReadBitAsync();
                }
                RbspHrdParameters? nalHRD = null;
                RbspHrdParameters? vclHRD = null;
                bool nal_hrd_parameters_present_flag = await reader.ReadBitAsync();
                if (nal_hrd_parameters_present_flag)
                    nalHRD = await ReadHrdParameters();
                bool vcl_hrd_parameters_present_flag = await reader.ReadBitAsync();
                if (vcl_hrd_parameters_present_flag)
                    vclHRD = await ReadHrdParameters();
                bool low_delay_hrd_flag = false;
                if (nal_hrd_parameters_present_flag || vcl_hrd_parameters_present_flag)
                    low_delay_hrd_flag = await reader.ReadBitAsync();
                bool pic_struct_present_flag = await reader.ReadBitAsync();
                bool bitstream_restriction_flag = await reader.ReadBitAsync();
                bool motion_vectors_over_pic_boundaries_flag = false;
                uint? max_bytes_per_pic_denom = null;
                uint? max_bits_per_mb_denom = null;
                uint? log2_max_mv_length_horizontal = null;
                uint? log2_max_mv_length_vertical = null;
                uint? max_num_reorder_frames = null;
                uint? max_dec_frame_buffering = null;
                if (bitstream_restriction_flag)
                {
                    motion_vectors_over_pic_boundaries_flag = await reader.ReadBitAsync();
                    max_bytes_per_pic_denom = await reader.ReadUEAsync();
                    max_bits_per_mb_denom = await reader.ReadUEAsync();
                    log2_max_mv_length_horizontal = await reader.ReadUEAsync();
                    log2_max_mv_length_vertical = await reader.ReadUEAsync();
                    max_num_reorder_frames = await reader.ReadUEAsync();
                    max_dec_frame_buffering = await reader.ReadUEAsync();
                }
                return new RbspVuiParameters(
                    aspect_ratio_info_present_flag, aspect_ratio_idc, sar_width, sar_height, overscan_info_present_flag, overscan_info_appropriate_flag, video_signal_type_present_flag,
                    video_format, video_full_range_flag, colour_description_present_flag, colour_primaries, transfer_characteristics, matrix_coefficients, chroma_loc_info_present_flag,
                    chroma_sample_loc_type_top_field, chroma_sample_loc_type_bottom_field, timing_info_present_flag, num_units_in_tick, time_scale, fixed_frame_rate_flag,
                    nal_hrd_parameters_present_flag, nalHRD, vcl_hrd_parameters_present_flag, vclHRD, low_delay_hrd_flag, pic_struct_present_flag, bitstream_restriction_flag,
                    motion_vectors_over_pic_boundaries_flag, max_bytes_per_pic_denom, max_bits_per_mb_denom, log2_max_mv_length_horizontal, log2_max_mv_length_vertical,
                    max_num_reorder_frames, max_dec_frame_buffering);
            }

            async Task<RbspHrdParameters> ReadHrdParameters()
            {
                uint cpb_cnt_minus1 = await reader.ReadUEAsync();
                uint bit_rate_scale = await reader.ReadBitsAsync(4);
                uint cpb_size_scale = await reader.ReadBitsAsync(4);
                List<uint> bit_rate_value_minus1 = [];
                List<uint> cpb_size_value_minus1 = [];
                BitCollection cbr_flag = [];
                for (int SchedSchelIdx = 0; SchedSchelIdx <= cpb_cnt_minus1; SchedSchelIdx++)
                {
                    bit_rate_value_minus1.Add(await reader.ReadUEAsync());
                    cpb_size_value_minus1.Add(await reader.ReadUEAsync());
                    cbr_flag.Add(await reader.ReadBitAsync());
                }
                uint initial_cpb_removal_delay_length_minus1 = await reader.ReadBitsAsync(5);
                uint cpb_removal_delay_length_minus1 = await reader.ReadBitsAsync(5);
                uint dpb_output_delay_length_minus1 = await reader.ReadBitsAsync(5);
                uint time_offset_length = await reader.ReadBitsAsync(5);

                return new RbspHrdParameters(cpb_cnt_minus1, bit_rate_scale, cpb_size_scale, [.. bit_rate_value_minus1], [.. cpb_size_value_minus1], cbr_flag, initial_cpb_removal_delay_length_minus1, cpb_removal_delay_length_minus1, dpb_output_delay_length_minus1, time_offset_length);
            }
        }

        public RbspSubMbPred ReadSubMbPred(IH264SyntaxReader syntaxReader, H264RbspState rbspState)
        {
            int num_ref_idx_l0_active_minus1 = (int?)rbspState.PictureParameterSet?.NumRefIdxL0DefaultActiveMinus1 ?? 0;
            int num_ref_idx_l1_active_minus1 = (int?)rbspState.PictureParameterSet?.NumRefIdxL1DefaultActiveMinus1 ?? 0;
            bool mb_field_decoding_flag = CurrentMacroblock!.MbFieldDecodingFlag;
            bool field_pic_flag = rbspState.SliceHeader?.FieldPicFlag ?? false;
            H264SliceType sliceType = H264SliceTypes.FetchSliceType(rbspState.SliceHeader?.SliceType ?? 0);

            List<uint> sub_mb_type = [];
            List<uint> ref_idx_l0 = [.. new uint[4]];
            List<uint> ref_idx_l1 = [.. new uint[4]];
            H264MotionVectorDifference mvd_l0 = new();
            H264MotionVectorDifference mvd_l1 = new();

            H264DecodingVariables? variables = GetDecodingVariables(syntaxReader);

            if (variables != null)
            {
                variables.SubMbType = sub_mb_type;
                variables.RefIdxL0 = ref_idx_l0;
                variables.RefIdxL1 = ref_idx_l1;
                variables.MvdL0 = mvd_l0;
                variables.MvdL1 = mvd_l1;
                variables.ChromaArrayType = rbspState.ChromaArrayType();
            }

            var sub_mb_pred = new RbspSubMbPred(sub_mb_type, ref_idx_l0, ref_idx_l1, mvd_l0.Raw, mvd_l1.Raw);

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                sub_mb_type.Add(syntaxReader.ReadSubMbType());
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if ((num_ref_idx_l0_active_minus1 > 0 ||
                     mb_field_decoding_flag != field_pic_flag) &&
                     CurrentMacroblock != P_8x8ref0 &&
                     MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                     MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L1)
                {
                    ref_idx_l0[mbPartIdx] = syntaxReader.ReadRefIdxL0();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if ((num_ref_idx_l1_active_minus1 > 0 ||
                     mb_field_decoding_flag != field_pic_flag) &&
                     MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                     MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L0)
                {
                    ref_idx_l1[mbPartIdx] = syntaxReader.ReadRefIdxL1();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if (MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                    MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L1)
                {
                    for (int subMbPartIdx = 0;
                        subMbPartIdx < MacroblockTraits.NumSubMbPart((int)sub_mb_type[mbPartIdx], CurrentMacroblock!.Rbsp.TransformSize8x8Flag, sliceType);
                        subMbPartIdx++)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l0[mbPartIdx, subMbPartIdx, compIdx] = syntaxReader.ReadMvdL0();
                        }
                    }
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if (MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                    MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L0)
                {
                    for (int subMbPartIdx = 0;
                        subMbPartIdx < MacroblockTraits.NumSubMbPart((int)sub_mb_type[mbPartIdx], CurrentMacroblock!.Rbsp.TransformSize8x8Flag, sliceType);
                        subMbPartIdx++)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l1[mbPartIdx, subMbPartIdx, compIdx] = syntaxReader.ReadMvdL1();
                        }
                    }
                }
            }

            return sub_mb_pred;
        }

        public async Task<RbspSubMbPred> ReadSubMbPredAsync(IH264SyntaxReader syntaxReader, H264RbspState rbspState)
        {
            int num_ref_idx_l0_active_minus1 = (int?)rbspState.PictureParameterSet?.NumRefIdxL0DefaultActiveMinus1 ?? 0;
            int num_ref_idx_l1_active_minus1 = (int?)rbspState.PictureParameterSet?.NumRefIdxL1DefaultActiveMinus1 ?? 0;
            bool mb_field_decoding_flag = CurrentMacroblock!.MbFieldDecodingFlag;
            bool field_pic_flag = rbspState.SliceHeader?.FieldPicFlag ?? false;
            H264SliceType sliceType = H264SliceTypes.FetchSliceType(rbspState.SliceHeader?.SliceType ?? 0);

            List<uint> sub_mb_type = [];
            List<uint> ref_idx_l0 = [];
            List<uint> ref_idx_l1 = [];
            H264MotionVectorDifference mvd_l0 = new();
            H264MotionVectorDifference mvd_l1 = new();

            H264DecodingVariables? variables = GetDecodingVariables(syntaxReader);

            if (variables != null)
            {
                variables.SubMbType = sub_mb_type;
                variables.RefIdxL0 = ref_idx_l0;
                variables.RefIdxL1 = ref_idx_l1;
                variables.MvdL0 = mvd_l0;
                variables.MvdL1 = mvd_l1;
                variables.ChromaArrayType = rbspState.ChromaArrayType();
            }

            var sub_mb_pred = new RbspSubMbPred(sub_mb_type, ref_idx_l0, ref_idx_l1, mvd_l0.Raw, mvd_l1.Raw);

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                sub_mb_type.Add(await syntaxReader.ReadSubMbTypeAsync());
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if ((num_ref_idx_l0_active_minus1 > 0 ||
                     mb_field_decoding_flag != field_pic_flag) &&
                     CurrentMacroblock != P_8x8ref0 &&
                     MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                     MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L1)
                {
                    ref_idx_l0[mbPartIdx] = await syntaxReader.ReadRefIdxL0Async();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if ((num_ref_idx_l1_active_minus1 > 0 ||
                     mb_field_decoding_flag != field_pic_flag) &&
                     MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                     MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L0)
                {
                    ref_idx_l1[mbPartIdx] = await syntaxReader.ReadRefIdxL1Async();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if (MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                    MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L1)
                {
                    for (int subMbPartIdx = 0;
                        subMbPartIdx < MacroblockTraits.NumSubMbPart((int)sub_mb_type[mbPartIdx], CurrentMacroblock!.Rbsp.TransformSize8x8Flag, sliceType);
                        subMbPartIdx++)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l0[mbPartIdx, subMbPartIdx, compIdx] = await syntaxReader.ReadMvdL0Async();
                        }
                    }
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
            {
                if (variables != null)
                    variables.MbPartIdx = mbPartIdx;

                if (MacroblockEquality.SubMacroblocksEqual((int)sub_mb_type[mbPartIdx], sliceType, B_Direct_8x8) &&
                    MacroblockTraits.SubMbPredMode(CurrentMacroblock, (int)sub_mb_type[mbPartIdx]) != Pred_L0)
                {
                    for (int subMbPartIdx = 0;
                        subMbPartIdx < MacroblockTraits.NumSubMbPart((int)sub_mb_type[mbPartIdx], CurrentMacroblock!.Rbsp.TransformSize8x8Flag, sliceType);
                        subMbPartIdx++)
                    {
                        for (int compIdx = 0; compIdx < 2; compIdx++)
                        {
                            mvd_l1[mbPartIdx, subMbPartIdx, compIdx] = await syntaxReader.ReadMvdL1Async();
                        }
                    }
                }
            }

            return sub_mb_pred;
        }

        private static void ParseScalingList(BitStreamReader reader, List<int> scalingList, int size, ref bool useDefaultScalingMatrixFlag)
        {
            int lastScale = 8;
            int nextScale = 8;
            for (int j = 0; j < size; j++)
            {
                if (nextScale != 0)
                {
                    int delta_scale = reader.ReadSE();
                    nextScale = (lastScale + delta_scale + 256) % 256;
                    useDefaultScalingMatrixFlag = (j == 0 && nextScale == 0);
                }
                scalingList[j] = (nextScale == 0) ? lastScale : nextScale;
                lastScale = scalingList[j];
            }
        }

        private static async Task<bool> ParseScalingListAsync(BitStreamReader reader, List<int> scalingList, int size, bool useDefaultScalingMatrixFlag)
        {
            int lastScale = 8;
            int nextScale = 8;
            for (int j = 0; j < size; j++)
            {
                if (nextScale != 0)
                {
                    int delta_scale = await reader.ReadSEAsync();
                    nextScale = (lastScale + delta_scale + 256) % 256;
                    useDefaultScalingMatrixFlag = (j == 0 && nextScale == 0);
                }
                scalingList[j] = (nextScale == 0) ? lastScale : nextScale;
                lastScale = scalingList[j];
            }
            return useDefaultScalingMatrixFlag;
        }

        public void ReadSliceData(IH264SyntaxReaderFactory syntaxReaderFactory, BitStreamReader bitStream, SliceDataReceiveMacroblockCallback receiveMacroblock, H264State state, ISliceDecoder sliceDecoder)
        {
            if (Grabber.GetEntropyCodingModeFlag(state.H264RbspState))
            {
                while (bitStream.GetState().BitPosition != 0)
                {
                    bool cabac_alignment_one_bit = bitStream.ReadBit();
                    if (!cabac_alignment_one_bit)
                        throw new H264Exception("cabac_alignment_one_bit must be 1");
                }
            }

            IH264SyntaxReader syntaxReader = syntaxReaderFactory.CreateSyntaxReader(state, bitStream); // will read codIOffset automatically if CABAC
            
            state.CurrMbAddr = (int)Grabber.GetFirstMbInSlice(state.H264RbspState) * (1 + state.DeriveMbaffFrameFlag().AsInt32());

            int[] mapUnitToSliceGroupMap = new int[state.DerivePicSizeInMbs() * (1 + state.DeriveMbaffFrameFlag().AsInt32())];
            sliceDecoder.PopulateWithMapUnitToSliceGroupMap(state, mapUnitToSliceGroupMap);

            LimitedList<int> mbToSliceGroupMap = new(state.DerivePicSizeInMbs() * (1 + state.DeriveMbaffFrameFlag().AsInt32()));
            sliceDecoder.ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap(state, mapUnitToSliceGroupMap, mbToSliceGroupMap);

            H264SliceType slice_type = H264SliceTypes.FetchSliceType(Grabber.GetSliceType(state.H264RbspState));

            int moreDataFlag = 1;
            int prevMbSkipped = 0;

            bool mb_field_decoding_flag;
            bool mb_skip_flag = false;

            AddressAndAvailability? prev = null;
            AddressAndAvailability curr = new(state.CurrMbAddr, true);

            H264DecodingVariables? dv = GetDecodingVariables(syntaxReader);

            do
            {
                if (prev != null && dv != null)
                    dv.PreviousMacroblockAddress = prev.Value;

                H264MacroblockInfo mb = new(slice_type, new(), false);
                syntaxReader.MacroblockInfo = mb;
                receiveMacroblock(mb);

                if (slice_type is not (H264SliceType.I or H264SliceType.SI))
                {
                    if (!Grabber.GetEntropyCodingModeFlag(state.H264RbspState))
                    {
                        // CAVLC

                        uint mb_skip_run = syntaxReader.ReadMbSkipRun();
                        prevMbSkipped = (mb_skip_run > 0).AsInt32();
                        for (int i = 0; i < mb_skip_run; i++)
                            state.CurrMbAddr = sliceDecoder.NextMbAddress(state, mbToSliceGroupMap, state.CurrMbAddr);
                        if (mb_skip_run > 0)
                            moreDataFlag = state.H264RbspState!.MoreRbspData().AsInt32();
                    }
                    else
                    {
                        // CABAC

                        mb_skip_flag = syntaxReader.ReadMbSkipFlag();
                        moreDataFlag = (!mb_skip_flag).AsInt32();
                        mb.MbSkipFlag = !moreDataFlag.AsBoolean();
                    }
                }

                mb.Inferred = mb_skip_flag;

                if (moreDataFlag.AsBoolean())
                {
                    if (state.DeriveMbaffFrameFlag() && (state.CurrMbAddr % 2 == 0 ||
                        (state.CurrMbAddr % 2 == 1 && prevMbSkipped == 1)))
                    {
                        mb_field_decoding_flag = syntaxReader.ReadMbFieldDecodingFlag();
                        mb.MbFieldDecodingFlag = mb_field_decoding_flag;
                    }

                    ReadMacroblockLayer(syntaxReader, mb, state.H264RbspState ?? throw new InvalidOperationException("Missing RBSP state"));
                }

                if (!Grabber.GetEntropyCodingModeFlag(state.H264RbspState))
                {
                    moreDataFlag = state.H264RbspState!.MoreRbspData().AsInt32();
                }
                else
                {
                    if (slice_type is not (H264SliceType.I or H264SliceType.SI))
                    {
                        prevMbSkipped = mb_skip_flag.AsInt32();
                    }

                    if (state.DeriveMbaffFrameFlag() && state.CurrMbAddr % 2 == 0)
                    {
                        moreDataFlag = 1;
                    }
                    else
                    {
                        bool end_of_slice_flag = syntaxReader.ReadEndOfSliceFlag();
                        moreDataFlag = (!end_of_slice_flag).AsInt32();
                    }
                }

                state.CurrMbAddr = sliceDecoder.NextMbAddress(state, mbToSliceGroupMap, state.CurrMbAddr);

                prev = curr;
                curr = new(state.CurrMbAddr, true);
            }
            while (moreDataFlag.AsBoolean());
        }

        public async Task ReadSliceDataAsync(IH264SyntaxReaderFactory syntaxReaderFactory, BitStreamReader bitStream, SliceDataReceiveMacroblockCallback receiveMacroblock, H264State state, ISliceDecoder sliceDecoder)
        {
            if (Grabber.GetEntropyCodingModeFlag(state.H264RbspState))
            {
                while (bitStream.GetState().BitPosition != 0)
                {
                    bool cabac_alignment_one_bit = bitStream.ReadBit();
                    if (!cabac_alignment_one_bit)
                        throw new H264Exception("cabac_alignment_one_bit must be 1");
                }
            }

            IH264SyntaxReader syntaxReader = syntaxReaderFactory.CreateSyntaxReader(state, bitStream); // will read codIOffset automatically if CABAC

            LimitedList<int> mapUnitToSliceGroupMap = new(state.DerivePicSizeInMbs() * (1 + state.DeriveMbaffFrameFlag().AsInt32()));
            sliceDecoder.PopulateWithMapUnitToSliceGroupMap(state, mapUnitToSliceGroupMap);

            LimitedList<int> mbToSliceGroupMap = new(state.DerivePicSizeInMbs() * (1 + state.DeriveMbaffFrameFlag().AsInt32()));
            sliceDecoder.ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap(state, mapUnitToSliceGroupMap, mbToSliceGroupMap);

            state.CurrMbAddr = (int)Grabber.GetFirstMbInSlice(state.H264RbspState) * (1 + state.DeriveMbaffFrameFlag().AsInt32());

            H264SliceType slice_type = H264SliceTypes.FetchSliceType(Grabber.GetSliceType(state.H264RbspState));

            int moreDataFlag = 1;
            int prevMbSkipped = 0;

            bool mb_field_decoding_flag;
            bool mb_skip_flag = false;

            do
            {
                H264MacroblockInfo mb = new(slice_type, new(), false);
                syntaxReader.MacroblockInfo = mb;
                receiveMacroblock(mb);

                if (slice_type is not (H264SliceType.I or H264SliceType.SI))
                {
                    if (!Grabber.GetEntropyCodingModeFlag(state.H264RbspState))
                    {
                        // CAVLC

                        uint mb_skip_run = await syntaxReader.ReadMbSkipRunAsync();
                        prevMbSkipped = (mb_skip_run > 0).AsInt32();
                        for (int i = 0; i < mb_skip_run; i++)
                            state.CurrMbAddr = sliceDecoder.NextMbAddress(state, mbToSliceGroupMap, state.CurrMbAddr);
                        if (mb_skip_run > 0)
                            moreDataFlag = state.H264RbspState!.MoreRbspData().AsInt32();
                    }
                    else
                    {
                        // CABAC

                        mb_skip_flag = await syntaxReader.ReadMbSkipFlagAsync();
                        moreDataFlag = (!mb_skip_flag).AsInt32();
                        mb.MbSkipFlag = !moreDataFlag.AsBoolean();
                    }
                }

                if (moreDataFlag.AsBoolean())
                {
                    if (state.DeriveMbaffFrameFlag() && (state.CurrMbAddr % 2 == 0 ||
                        (state.CurrMbAddr % 2 == 1 && prevMbSkipped == 1)))
                    {
                        mb_field_decoding_flag = await syntaxReader.ReadMbFieldDecodingFlagAsync();
                        mb.MbFieldDecodingFlag = mb_field_decoding_flag;
                    }

                    await ReadMacroblockLayerAsync(syntaxReader, mb, state.H264RbspState!);
                }

                if (!Grabber.GetEntropyCodingModeFlag(state.H264RbspState))
                {
                    moreDataFlag = state.H264RbspState!.MoreRbspData().AsInt32();
                }
                else
                {
                    if (slice_type is not (H264SliceType.I or H264SliceType.SI))
                    {
                        prevMbSkipped = mb_skip_flag.AsInt32();
                    }

                    if (state.DeriveMbaffFrameFlag() && state.CurrMbAddr % 2 == 0)
                    {
                        moreDataFlag = 1;
                    }
                    else
                    {
                        bool end_of_slice_flag = await syntaxReader.ReadEndOfSliceFlagAsync();
                        moreDataFlag = (!end_of_slice_flag).AsInt32();
                    }
                }

                state.CurrMbAddr = sliceDecoder.NextMbAddress(state, mbToSliceGroupMap, state.CurrMbAddr);
            }
            while (moreDataFlag.AsBoolean());
        }

        private static H264DecodingVariables? GetDecodingVariables(IH264SyntaxReader reader)
        {
            if (!reader.Miscellaneous.TryGetValue(DecodingVariablesConstant, out object? val))
                return null;

            return (H264DecodingVariables)val;
        }
    }
}
