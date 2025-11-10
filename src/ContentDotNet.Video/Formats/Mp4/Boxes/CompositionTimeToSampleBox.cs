namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class CompositionTimeToSampleBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public struct Entry { public uint SampleCount; public int SampleOffset; } // offset signed for v1
        public List<Entry> Entries { get; } = [];

        public static CompositionTimeToSampleBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new CompositionTimeToSampleBox
            {
                Size = boxSize,
                Type = new FourCC("ctts"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            for (uint i = 0; i < entryCount; i++)
            {
                uint sampleCount = reader.ReadUInt32();
                int sampleOffset;
                if (box.Version == 0) sampleOffset = (int)reader.ReadUInt32();
                else sampleOffset = reader.ReadInt32();
                box.Entries.Add(new Entry { SampleCount = sampleCount, SampleOffset = sampleOffset });
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + (Entries.Count * 8);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Entries.Count);
            foreach (var e in Entries)
            {
                writer.Write(e.SampleCount);
                if (Version == 0) writer.Write((uint)e.SampleOffset);
                else writer.Write(e.SampleOffset);
            }
        }

        public override string ToString() => $"ctts: entries={Entries.Count}";
    }
}
