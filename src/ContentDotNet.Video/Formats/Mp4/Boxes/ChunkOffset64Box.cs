namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class ChunkOffset64Box : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public List<ulong> Offsets { get; } = [];

        public static ChunkOffset64Box Parse(BinaryReader reader, long boxSize)
        {
            var box = new ChunkOffset64Box
            {
                Size = boxSize,
                Type = new FourCC("co64"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            for (uint i = 0; i < entryCount; i++) box.Offsets.Add(reader.ReadUInt64());

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + (Offsets.Count * 8);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Offsets.Count);
            foreach (var o in Offsets) writer.Write(o);
        }

        public override string ToString() => $"co64: entries={Offsets.Count}";
    }

}
