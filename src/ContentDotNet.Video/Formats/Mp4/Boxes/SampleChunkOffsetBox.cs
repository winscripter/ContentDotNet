namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class SampleChunkOffsetBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public List<uint> Offsets { get; } = [];

        public static SampleChunkOffsetBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new SampleChunkOffsetBox
            {
                Size = boxSize,
                Type = new FourCC("stco"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            for (uint i = 0; i < entryCount; i++) box.Offsets.Add(reader.ReadUInt32());

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + (Offsets.Count * 4);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Offsets.Count);
            foreach (var o in Offsets) writer.Write(o);
        }

        public override string ToString() => $"stco: entries={Offsets.Count}";
    }
}
