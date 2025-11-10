namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class CompactSampleSizeBox : Mp4SampleSizeBoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        // stz2 has 3 bytes reserved then a 1-byte field-size (1/2/4), then sample_count and entries
        public byte FieldSize { get; set; } // 1,2 or 4
        public uint SampleCount { get; set; }
        public List<uint> EntrySizes { get; } = [];

        public static CompactSampleSizeBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new CompactSampleSizeBox
            {
                Size = boxSize,
                Type = new FourCC("stz2"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            // reserved 3 bytes
            reader.ReadBytes(3);
            box.FieldSize = reader.ReadByte();
            box.SampleCount = reader.ReadUInt32();

            for (uint i = 0; i < box.SampleCount; i++)
            {
                uint v;
                if (box.FieldSize == 1) v = reader.ReadByte();
                else if (box.FieldSize == 2) v = reader.ReadUInt16();
                else if (box.FieldSize == 4) v = reader.ReadUInt32();
                else throw new InvalidDataException("Unsupported stz2 field size");
                box.EntrySizes.Add(v);
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            long payload = 3 + 1 + 4 + (EntrySizes.Count * FieldSize);
            Size = 8 + 4 + payload; // header + version/flags + payload
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write(new byte[3]); // reserved
            writer.Write(FieldSize);
            writer.Write(SampleCount);
            foreach (var s in EntrySizes)
            {
                if (FieldSize == 1) writer.Write((byte)s);
                else if (FieldSize == 2) writer.Write((ushort)s);
                else writer.Write(s);
            }
        }

        public override string ToString() => $"stz2: fieldSize={FieldSize}, count={SampleCount}";
    }
}
