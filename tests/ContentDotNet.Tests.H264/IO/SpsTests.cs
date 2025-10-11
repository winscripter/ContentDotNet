namespace ContentDotNet.Tests.H264.IO
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    public class SpsTests
    {
        [Fact]
        public void SPS_Without_Advanced_Stuff()
        {
            var stream = new MemoryStream();
            var bsw = new BitStreamWriter(stream);

            bsw.WriteBits(66, 8); // profile_idc
            bsw.WriteBits(0, 8); // constraint_setx_flag, with x being 0..5, + reserved_zero2bits
            bsw.WriteBits(42, 8); // level_idc
            bsw.WriteBits(69, 8); // seq_parameter_set_id

            bsw.WriteUE(4); // log2_max_frame_num_minus4
            bsw.WriteUE(2); // pic_order_cnt_type
            bsw.WriteUE(4); // max_num_ref_frames
            bsw.WriteBit(true); // gaps_in_frame_num_value_allowed_flag
            bsw.WriteUE(120); // pic_width_in_mbs_minus1
            bsw.WriteUE(100); // pic_height_in_map_units_minus1
            bsw.WriteBit(true); // frame_mbs_only_flag
            bsw.WriteBit(false); // direct_8x8_inference_flag
            bsw.WriteBit(false); // frame_cropping_flag
            bsw.WriteBit(false); // vui_parameters_present_flag

            // Do some alignment. These bits won't be read.
            bsw.WriteBits(0, 8);

            // Decoding & assertion time
            var service = new H264Service();
            AbstractH264Decoder h264Dec = service
                .CreateDecoder(new BitStreamReader(stream));

            RbspSequenceParameterSetData sps = h264Dec
                .IOReader!
                .ReadSPSData(
                    h264Dec.State!.H264RbspState!,
                    h264Dec.BitStreamReader);

            Assert.Equal(66u, sps.ProfileIdc);
            Assert.False(sps.ConstraintSet0Flag);
            Assert.False(sps.ConstraintSet1Flag);
            Assert.False(sps.ConstraintSet2Flag);
            Assert.False(sps.ConstraintSet3Flag);
            Assert.False(sps.ConstraintSet4Flag);
            Assert.False(sps.ConstraintSet5Flag);
            Assert.Equal(42u, sps.LevelIdc);
            Assert.Equal(69u, sps.SeqParameterSetId);

            Assert.Equal(4u, sps.Log2MaxFrameNumMinus4);
            Assert.Equal(2u, sps.PicOrderCntType);
            Assert.Equal(4u, sps.MaxNumRefFrames);
            Assert.True(sps.GapsInFrameNumValueAllowedFlag);
            Assert.Equal(120u, sps.PicWidthInMbsMinus1);
            Assert.Equal(100u, sps.PicHeightInMapUnitsMinus1);
            Assert.True(sps.FrameMbsOnlyFlag);
            Assert.False(sps.Direct8x8InferenceFlag);
            Assert.False(sps.FrameCroppingFlag);
            Assert.False(sps.VuiParametersPresentFlag);
            Assert.Null(sps.VuiParameters);
        }
    }
}
