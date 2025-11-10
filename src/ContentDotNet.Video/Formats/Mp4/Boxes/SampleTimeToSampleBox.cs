namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class SampleTimeToSampleBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public struct Entry { public uint SampleCount; public uint SampleDelta; }
        public List<Entry> Entries { get; } = [];

        public static SampleTimeToSampleBox Parse(BinaryReader reader, long boxSize, int maxEntries)
        {
            var box = new SampleTimeToSampleBox
            {
                Size = boxSize,
                Type = new FourCC("stts"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            if (entryCount > maxEntries)
                throw new InvalidOperationException("Too many STTS entries");

            for (uint i = 0; i < entryCount; i++)
            {
                box.Entries.Add(new Entry { SampleCount = reader.ReadUInt32(), SampleDelta = reader.ReadUInt32() });
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
                writer.Write(e.SampleDelta);
            }
        }

        public override string ToString() => $"stts: entries={Entries.Count}";
    }
}
