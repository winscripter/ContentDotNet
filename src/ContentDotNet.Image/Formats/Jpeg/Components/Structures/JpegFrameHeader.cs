namespace ContentDotNet.Image.Formats.Jpeg.Components.Structures
{
    using ContentDotNet.Api.Binary;

    /// <summary>
    ///   The frame header.
    /// </summary>
    public struct JpegFrameHeader
    {
        public ushort Lf, Y, X;
        public byte P, Nf;

        public JpegFrameHeader(ushort lf, ushort y, ushort x, byte p, byte nf)
        {
            Lf = lf;
            Y = y;
            X = x;
            P = p;
            Nf = nf;
        }

        public static JpegFrameHeader Read(BinaryReader binaryReader, Span<byte> c, Span<byte> tq, Span<byte> hv)
        {
            ushort lf = binaryReader.ReadUInt16();
            byte p = binaryReader.ReadByte();
            ushort y = binaryReader.ReadUInt16();
            ushort x = binaryReader.ReadUInt16();
            byte nf = binaryReader.ReadByte();
            for (int i = 1; i <= nf; i++)
            {
                c[i] = binaryReader.ReadByte();
                hv[i] = binaryReader.ReadByte();
                tq[i] = binaryReader.ReadByte();
            }
            return new JpegFrameHeader(lf, y, x, p, nf);
        }

        public static async Task<JpegFrameHeader> ReadAsync(BinaryReader binaryReader, Memory<byte> c, Memory<byte> tq, Memory<byte> hv)
        {
            ushort lf = await binaryReader.ReadUInt16Async();
            byte p = await binaryReader.ReadByteAsync();
            ushort y = await binaryReader.ReadUInt16Async();
            ushort x = await binaryReader.ReadUInt16Async();
            byte nf = await binaryReader.ReadByteAsync();
            for (int i = 1; i <= nf; i++)
            {
                c.Span[i] = binaryReader.ReadByte();
                hv.Span[i] = binaryReader.ReadByte();
                tq.Span[i] = binaryReader.ReadByte();
            }
            return new JpegFrameHeader(lf, y, x, p, nf);
        }

        public readonly void Write(BinaryWriter writer, Span<byte> c, Span<byte> tq, Span<byte> hv)
        {
            writer.Write(Lf);
            writer.Write(P);
            writer.Write(Y);
            writer.Write(X);
            writer.Write(Nf);
            for (int i = 1; i <= Nf; i++)
            {
                writer.Write(c[i]);
                writer.Write(tq[i]);
                writer.Write(hv[i]);
            }
        }

        public readonly async Task WriteAsync(BinaryWriter writer, Memory<byte> c, Memory<byte> tq, Memory<byte> hv)
        {
            await writer.WriteAsync(Lf);
            await writer.WriteAsync(P);
            await writer.WriteAsync(Y);
            await writer.WriteAsync(X);
            await writer.WriteAsync(Nf);
            for (int i = 1; i <= Nf; i++)
            {
                writer.Write(c.Span[i]);
                writer.Write(tq.Span[i]);
                writer.Write(hv.Span[i]);
            }
        }
    }
}
