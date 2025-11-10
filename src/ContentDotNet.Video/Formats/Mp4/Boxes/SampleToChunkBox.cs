namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class SampleToChunkBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public struct Entry { public uint FirstChunk; public uint SamplesPerChunk; public uint SampleDescriptionIndex; }
        public List<Entry> Entries { get; } = [];

        public static SampleToChunkBox Parse(BinaryReader reader, long boxSize, int maxNumberOfEntries)
        {
            var box = new SampleToChunkBox
            {
                Size = boxSize,
                Type = new FourCC("stsc"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            if (entryCount > maxNumberOfEntries)
                throw new InvalidOperationException("Too many entries");

            for (uint i = 0; i < entryCount; i++)
                box.Entries.Add(new Entry { FirstChunk = reader.ReadUInt32(), SamplesPerChunk = reader.ReadUInt32(), SampleDescriptionIndex = reader.ReadUInt32() });

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + (Entries.Count * 12);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Entries.Count);
            foreach (var e in Entries)
            {
                writer.Write(e.FirstChunk);
                writer.Write(e.SamplesPerChunk);
                writer.Write(e.SampleDescriptionIndex);
            }
        }

        public override string ToString() => $"stsc: entries={Entries.Count}";
    }
}
