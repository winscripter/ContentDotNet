using ContentDotNet.BitStream;
using System.Drawing;

internal class Program
{
    /// <summary>
    ///   A stream that automatically appends emulation prevention 3 bytes.
    /// </summary>
    class NalSafeStream : Stream
    {
        public Stream InnerStream { get; set; }

        private int prevByte0, prevByte1;

        public NalSafeStream(Stream innerStream)
        {
            InnerStream = innerStream;
        }

        public override bool CanRead => false;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        public override long Length => InnerStream.Length;
        public override long Position
        {
            get => InnerStream.Position;
            set => InnerStream.Position = value;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void WriteByte(byte value)
        {
            if (prevByte0 == 0 && prevByte1 == 0)
            {
                InnerStream.WriteByte(3); // emulation prevention
                ShiftBytes(3);
            }
            InnerStream.WriteByte(value);
            ShiftBytes(value);
        }

        private void ShiftBytes(byte newByte)
        {
            prevByte0 = prevByte1;
            prevByte1 = newByte;
        }
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    private static BitStreamWriter CreateNalSafeBitStreamWriter(Stream stream) => new(new NalSafeStream(stream));

    private static void WriteSPS(BitStreamWriter writer, Size frameSize)
    {
        NalSafeStream nss = (NalSafeStream)writer.BaseStream;
        nss.InnerStream.WriteByte(0);
        nss.InnerStream.WriteByte(0);
        nss.InnerStream.WriteByte(1);

        writer.WriteBit(false); // forbidden_zero_bit
        writer.WriteBits(7, 5); // SPS
        writer.WriteBits(0, 2); // NRI

        writer.WriteBits(66, 8); // profile
        writer.WriteBits(0, 6); // No constraints
        writer.WriteBits(0, 2); // reserved zero 2 bits
        writer.WriteBits(42, 8); // 4.2, up to 1080p60
        writer.WriteUE(0); // SPS ID of 0

        // No chroma format or bit depth because Baseline profile

        writer.WriteUE(0); // log2_max_frame_num_minus4
        writer.WriteUE(0); // POC type is 0
        writer.WriteUE(0); // log2_max_pic_order_cnt_lsb_minus4
        writer.WriteUE(1); // max_num_ref_frames
        writer.WriteBit(false); // gaps_in_frame_num_value_allowed_flag
        writer.WriteUE(((uint)frameSize.Width / 16u) - 1); // pic_width_in_mbs_minus1
        writer.WriteUE(((uint)frameSize.Height / 16u) - 1); // pic_height_in_map_units_minus1
        writer.WriteBit(true); // frame_mbs_only_flag
        writer.WriteBit(false); // direct_8x8_inference_flag
        writer.WriteBit(false); // frame_cropping_flag
        writer.WriteBit(false); // vui_parameters_present_flag
    }

    private static void WritePPS(BitStreamWriter writer)
    {
        NalSafeStream nss = (NalSafeStream)writer.BaseStream;
        nss.InnerStream.WriteByte(0);
        nss.InnerStream.WriteByte(0);
        nss.InnerStream.WriteByte(1);

        writer.WriteBit(false); // forbidden_zero_bit
        writer.WriteBits(8, 5); // PPS
        writer.WriteBits(0, 2); // NRI

        writer.WriteUE(0); // pic_parameter_set_id
        writer.WriteUE(0); // seq_parameter_set_id
        writer.WriteBit(false); // entropy_coding_mode_flag, use CAVLC
        writer.WriteBit(false); // bottom_field_pic_order_in_frame_present_flag
        writer.WriteUE(0); // num_slice_groups_minus1
        writer.WriteUE(1); // num_ref_idx_l0_default_active_minus1
        writer.WriteUE(1); // num_ref_idx_l1_default_active_minus1
        writer.WriteBit(true); // weighted_pred_flag
        writer.WriteUE(0); // weighted_bipred_idc
        writer.WriteSE(0); // pic_init_qp_minus26
        writer.WriteSE(0); // pic_init_qs_minus26
        writer.WriteSE(0); // chroma_qp_index_offset
        writer.WriteBit(false); // deblocking_filter_control_present_flag
        writer.WriteBit(false); // constrained_intra_pred_flag
        writer.WriteBit(false); // redundant_pic_cnt_present_flag

        while (writer.GetState().BitPosition != 1)
            writer.WriteBit(false); // Filler data

        var state = writer.GetState();
        state.BitPosition = 0;
        writer.GoTo(state);
    }

    private static void WriteFrame(BitStreamWriter writer,
        byte[,] y, byte[,] cb, byte[,] cr)
    {
        NalSafeStream nss = (NalSafeStream)writer.BaseStream;
        nss.InnerStream.WriteByte(0);
        nss.InnerStream.WriteByte(0);
        nss.InnerStream.WriteByte(1);

        writer.WriteBit(false); // forbidden_zero_bit
        writer.WriteBits(5, 5); // IDR
        writer.WriteBits(0, 2); // NRI

        // Slice header first
        writer.WriteUE(0); // first_mb_in_slice
        writer.WriteUE(2); // slice_type is Intra
        writer.WriteUE(0); // PPS id is 0
        // TBD
    }
}
