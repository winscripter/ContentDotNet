namespace ContentDotNet.Protocols.Rtp
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Protocols.Rtp.Models;

    /// <summary>
    ///   RTP model I/O.
    /// </summary>
    public static class RtpIO
    {
        /// <summary>
        ///   Writes the specified RTP data structure to the specified bit stream writer.
        /// </summary>
        /// <param name="bsw">The bitstream writer.</param>
        /// <param name="rtph">The data structure.</param>
        public static void Write(
            BitStreamWriter bsw,
            RtpHeader rtph)
        {
            bsw.WriteBits(rtph.Version, 2);
            bsw.WriteBit(rtph.Padding);
            bsw.WriteBit(rtph.Extension);
            bsw.WriteBits(rtph.CsrcCount, 4);
            bsw.WriteBit(rtph.Marker);
            bsw.WriteBits(rtph.PayloadType, 7);
            bsw.WriteBits(rtph.SequenceNumber, 16);
            bsw.WriteBits(rtph.Timestamp, 32);
            bsw.WriteBits(rtph.Ssrc, 32);
            foreach (uint csrc in rtph.CsrcList)
            {
                bsw.WriteBits(csrc, 32);
            }
        }

        /// <summary>
        ///   Writes the specified RTP data structure to the specified bit stream writer.
        /// </summary>
        /// <param name="bsw">The bitstream writer.</param>
        /// <param name="rtph">The data structure.</param>
        public static async Task WriteAsync(
            BitStreamWriter bsw,
            RtpHeader rtph)
        {
            await bsw.WriteBitsAsync(rtph.Version, 2);
            await bsw.WriteBitAsync(rtph.Padding);
            await bsw.WriteBitAsync(rtph.Extension);
            await bsw.WriteBitsAsync(rtph.CsrcCount, 4);
            await bsw.WriteBitAsync(rtph.Marker);
            await bsw.WriteBitsAsync(rtph.PayloadType, 7);
            await bsw.WriteBitsAsync(rtph.SequenceNumber, 16);
            await bsw.WriteBitsAsync(rtph.Timestamp, 32);
            await bsw.WriteBitsAsync(rtph.Ssrc, 32);
            foreach (uint csrc in rtph.CsrcList)
            {
                await bsw.WriteBitsAsync(csrc, 32);
            }
        }

        /// <summary>
        ///   Reads the specified data structure from the specified bit-stream reader.
        /// </summary>
        /// <param name="reader">The input bit-stream reader.</param>
        /// <returns>The specified data structure.</returns>
        public static RtpHeader ReadRtpHeader(
            BitStreamReader reader)
        {
            var rtpHeader = new RtpHeader()
            {
                Version = reader.ReadBits(2),
                Padding = reader.ReadBit(),
                Extension = reader.ReadBit(),
                CsrcCount = reader.ReadBits(4),
                Marker = reader.ReadBit(),
                PayloadType = reader.ReadBits(7),
                SequenceNumber = reader.ReadBits(16),
                Timestamp = reader.ReadBits(32),
                Ssrc = reader.ReadBits(32)
            };
            for (int i = 0; i < rtpHeader.CsrcCount; i++)
            {
                uint csrc = reader.ReadBits(32);
                rtpHeader.CsrcList.Add(csrc);
            }
            return rtpHeader;
        }

        /// <summary>
        ///   Reads the specified data structure from the specified bit-stream reader.
        /// </summary>
        /// <param name="reader">The input bit-stream reader.</param>
        /// <returns>The specified data structure.</returns>
        public static async Task<RtpHeader> ReadRtpHeaderAsync(
            BitStreamReader reader)
        {
            var rtpHeader = new RtpHeader()
            {
                Version = await reader.ReadBitsAsync(2),
                Padding = await reader.ReadBitAsync(),
                Extension = await reader.ReadBitAsync(),
                CsrcCount = await reader.ReadBitsAsync(4),
                Marker = await reader.ReadBitAsync(),
                PayloadType = await reader.ReadBitsAsync(7),
                SequenceNumber = await reader.ReadBitsAsync(16),
                Timestamp = await reader.ReadBitsAsync(32),
                Ssrc = await reader.ReadBitsAsync(32)
            };
            for (int i = 0; i < rtpHeader.CsrcCount; i++)
            {
                uint csrc = await reader.ReadBitsAsync(32);
                rtpHeader.CsrcList.Add(csrc);
            }
            return rtpHeader;
        }
    }
}
