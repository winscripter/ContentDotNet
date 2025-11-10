namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class SoundMediaHeaderBox : Mp4MediaHeaderBox
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];
        public short Balance { get; set; } // 8.8 fixed-point
        public ushort Reserved { get; set; }

        public static SoundMediaHeaderBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new SoundMediaHeaderBox
            {
                Size = boxSize,
                Type = new FourCC("smhd"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3),
                Balance = reader.ReadInt16(),
                Reserved = reader.ReadUInt16()
            };

            return box;
        }

        public override void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 2 + 2;

            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write(Balance);
            writer.Write(Reserved);
        }

        public override string ToString()
        {
            double balanceValue = Balance / 256.0; // convert 8.8 fixed-point to float
            return $"smhd: balance={balanceValue:0.00}";
        }
    }
}
