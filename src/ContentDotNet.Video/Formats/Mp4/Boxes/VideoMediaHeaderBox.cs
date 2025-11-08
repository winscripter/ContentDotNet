namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class VideoMediaHeaderBox : Mp4MediaHeaderBox
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];
        public ushort GraphicsMode { get; set; } = 0;
        public ushort[] OpColor { get; set; } = new ushort[3]; // R, G, B

        public static VideoMediaHeaderBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new VideoMediaHeaderBox
            {
                Size = boxSize,
                Type = new FourCC("vmhd"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3),
                GraphicsMode = reader.ReadUInt16()
            };
            for (int i = 0; i < 3; i++)
                box.OpColor[i] = reader.ReadUInt16();

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 2 + 6; // header + fields

            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write(GraphicsMode);
            foreach (var c in OpColor)
                writer.Write(c);
        }

        public override string ToString()
        {
            return $"vmhd: graphicsMode={GraphicsMode}, opColor=({OpColor[0]},{OpColor[1]},{OpColor[2]})";
        }
    }
}
