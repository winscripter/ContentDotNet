namespace ContentDotNet.Abstractions.Tests
{
    using ContentDotNet.Api.BitStream;

    public class RawSpsLikeReaderTests
    {
        [Fact]
        public void DoTest()
        {
            var stream = new MemoryStream();
            var bsw = new BitStreamWriter(stream);

            bsw.WriteBits(66, 8); // profile_idc
            bsw.WriteBits(0, 8); // constraint_setx_flag, with x being 0..5, + reserved_zero2bits
            bsw.WriteBits(42, 8); // level_idc
            bsw.WriteUE(69); // seq_parameter_set_id

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
            bsw.WriteBits(0, 32);

            // Decoding & assertion time
            stream.Position = 0;

            var reader = new BitStreamReader(stream);

            Assert.Equal(66u, reader.ReadBits(8));
            Assert.Equal(0u, reader.ReadBits(8));
            Assert.Equal(42u, reader.ReadBits(8));
            Assert.Equal(69u, reader.ReadUE());

            Assert.Equal(4u, reader.ReadUE());
            Assert.Equal(2u, reader.ReadUE());
            Assert.Equal(4u, reader.ReadUE());
            Assert.True(reader.ReadBit());
            Assert.Equal(120u, reader.ReadUE());
            Assert.Equal(100u, reader.ReadUE());
            Assert.True(reader.ReadBit());
            Assert.False(reader.ReadBit());
            Assert.False(reader.ReadBit());
            Assert.False(reader.ReadBit());
        }
    }
}
