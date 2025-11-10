namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using System.Collections.Generic;
    using System.IO;

    public class DataReferenceBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];
        public List<DataReferenceEntry> Entries { get; set; } = [];

        public static DataReferenceBox Parse(BinaryReader reader, long boxSize, int maxEntries = 16)
        {
            var box = new DataReferenceBox
            {
                Size = boxSize,
                Type = new FourCC("dref"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            if (entryCount > maxEntries)
                throw new InvalidDataException($"Too many dref entries ({entryCount})");

            for (uint i = 0; i < entryCount; i++)
                box.Entries.Add(DataReferenceEntry.Parse(reader));

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            using var ms = new MemoryStream();
            using (var childWriter = new BinaryWriter(ms))
            {
                foreach (var entry in Entries)
                    entry.Write(childWriter);
            }

            Size = 8 + 4 + 4 + ms.Length;
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Entries.Count);
            writer.Write(ms.ToArray());
        }

        public override string ToString() =>
            $"dref: {Entries.Count} entries";
    }
}
