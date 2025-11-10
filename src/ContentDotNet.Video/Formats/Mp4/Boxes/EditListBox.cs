namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;
    using System.Collections.Generic;
    using System.IO;

    public class EditListEntry
    {
        public ulong SegmentDuration { get; set; }
        public long MediaTime { get; set; }
        public short MediaRateInteger { get; set; }
        public short MediaRateFraction { get; set; }
    }

    public class EditListBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];
        public List<EditListEntry> Entries { get; set; } = [];

        public static EditListBox Parse(BinaryReader reader, long boxSize, int maxEntries = 1000)
        {
            var box = new EditListBox
            {
                Size = boxSize,
                Type = new FourCC("elst"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            if (entryCount > maxEntries)
                throw new InvalidDataException($"Too many elst entries ({entryCount} > {maxEntries})");

            for (uint i = 0; i < entryCount; i++)
            {
                var entry = new EditListEntry();
                if (box.Version == 1)
                {
                    entry.SegmentDuration = reader.ReadUInt64();
                    entry.MediaTime = reader.ReadInt64();
                }
                else
                {
                    entry.SegmentDuration = reader.ReadUInt32();
                    entry.MediaTime = reader.ReadInt32();
                }

                entry.MediaRateInteger = reader.ReadInt16();
                entry.MediaRateFraction = reader.ReadInt16();

                box.Entries.Add(entry);
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + Entries.Count * (Version == 1 ? 20 : 12);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Entries.Count);

            foreach (var e in Entries)
            {
                if (Version == 1)
                {
                    writer.Write(e.SegmentDuration);
                    writer.Write(e.MediaTime);
                }
                else
                {
                    writer.Write((uint)e.SegmentDuration);
                    writer.Write((int)e.MediaTime);
                }

                writer.Write(e.MediaRateInteger);
                writer.Write(e.MediaRateFraction);
            }
        }

        public override string ToString()
        {
            return $"elst: entries={Entries.Count}";
        }
    }
}
