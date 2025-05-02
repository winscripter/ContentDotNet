using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264;
using ContentDotNet.Extensions.H264.Models;

using var fs = File.OpenRead("output.h264");
using var br = new BitStreamReader(fs);

// My H.264 has one AUD followed by SPS
if (!NalUnit.SkipStartCode(br))
    throw new Exception("Cannot skip start code");

_ = NalUnit.Read(br, 1);

if (!NalUnit.SkipStartCode(br))
    throw new Exception("Cannot skip start code");

NalUnit sps = NalUnit.Read(br, 1);
if (!sps.IsSps())
    throw new Exception("Not SPS");

SequenceParameterSet spsValue = SequenceParameterSet.Read(br);
SpsDump(spsValue);

static void SpsDump(SequenceParameterSet sps)
{
    Console.WriteLine("profile_idc: " + sps.ProfileIdc);
    Console.WriteLine("constraint_set0_flag: " + sps.ConstraintSet0Flag);
    Console.WriteLine("constraint_set1_flag: " + sps.ConstraintSet1Flag);
    Console.WriteLine("constraint_set2_flag: " + sps.ConstraintSet2Flag);
    Console.WriteLine("constraint_set3_flag: " + sps.ConstraintSet3Flag);
    Console.WriteLine("constraint_set4_flag: " + sps.ConstraintSet4Flag);
    Console.WriteLine("constraint_set5_flag: " + sps.ConstraintSet5Flag);
    Console.WriteLine("level_idc: " + sps.LevelIdc);
    Console.WriteLine("chroma_format_idc: " + sps.ChromaFormatIdc);
    Console.WriteLine("separate_colour_plane_flag: " + sps.SeparateColourPlaneFlag);
    Console.WriteLine("bit_depth_luma_minus8: " + sps.BitDepthLumaMinus8);
    Console.WriteLine("bit_depth_chroma_minus8: " + sps.BitDepthChromaMinus8);
    Console.WriteLine("qpprime_y_zero_transform_bypass_flag: " + sps.QpprimeYZeroTransformBypassFlag);
    Console.WriteLine("log2_max_frame_num_minus4: " + sps.Log2MaxFrameNumMinus4);
    Console.WriteLine("pic_order_cnt_type: " + sps.PicOrderCntType);
    Console.WriteLine("log2_max_pic_order_cnt_lsb_minus4: " + sps.Log2MaxPicOrderCntLsbMinus4);
    Console.WriteLine("delta_pic_order_always_zero_flag: " + sps.DeltaPicOrderAlwaysZeroFlag);
    Console.WriteLine("offset_for_non_ref_pic: " + sps.OffsetForNonRefPic);
    Console.WriteLine("offset_for_top_to_bottom_field: " + sps.OffsetForTopToBottomField);
    Console.WriteLine("num_ref_frames_in_pic_order_cnt_cycle: " + sps.NumRefFramesInPicOrderCntCycle);
    Console.WriteLine("max_num_ref_frames: " + sps.MaxNumRefFrames);
    Console.WriteLine("gaps_in_frame_num_value_allowed_flag: " + sps.GapsInFrameNumValueAllowedFlag);
    Console.WriteLine("pic_width_in_mbs_minus1: " + sps.PicWidthInMbsMinus1);
    Console.WriteLine("pic_height_in_map_units_minus1: " + sps.PicHeightInMapUnitsMinus1);
    Console.WriteLine("frame_mbs_only_flag: " + sps.FrameMbsOnlyFlag);
    Console.WriteLine("mb_adaptive_frame_field_flag: " + sps.MbAdaptiveFrameFieldFlag);
    Console.WriteLine("direct_8x8_inference_flag: " + sps.Direct8X8InferenceFlag);
    Console.WriteLine("frame_cropping_flag: " + sps.FrameCroppingFlag);
    Console.WriteLine("frame_crop_left_offset: " + sps.FrameCropLeftOffset);
    Console.WriteLine("frame_crop_right_offset: " + sps.FrameCropRightOffset);
    Console.WriteLine("frame_crop_top_offset: " + sps.FrameCropTopOffset);
    Console.WriteLine("frame_crop_bottom_offset: " + sps.FrameCropBottomOffset);
    Console.WriteLine("vui_parameters_present_flag: " + sps.VuiParametersPresentFlag);
}
