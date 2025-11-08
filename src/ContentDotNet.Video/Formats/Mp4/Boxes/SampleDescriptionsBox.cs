namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class SampleDescriptionsBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public List<StsdSampleEntry> Entries { get; } = [];

        public static SampleDescriptionsBox Parse(BinaryReader reader, long boxSize, int maxPayloadSizePerEntry)
        {
            var box = new SampleDescriptionsBox
            {
                Size = boxSize,
                Type = new FourCC("stsd"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            for (uint i = 0; i < entryCount; i++)
            {
                uint childSize = reader.ReadUInt32();
                uint childType = reader.ReadUInt32();

                if (childSize < 8) throw new InvalidDataException("Invalid sample-entry size");

                int payloadLen = (int)childSize - 8;
                if (payloadLen > maxPayloadSizePerEntry)
                    throw new InvalidOperationException("There cannot be more than " + maxPayloadSizePerEntry + " bytes per entry.");
                byte[] payload = reader.ReadBytes(payloadLen);

                var entry = new StsdSampleEntry { Type = new FourCC(childType), Data = payload };
                box.Entries.Add(entry);
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            // compute size: 8 (header) + 4 (version/flags) + 4 (entry_count) + sum(each child box)
            long childrenSize = 0;
            foreach (var e in Entries)
            {
                childrenSize += 8 + (e.Data?.Length ?? 0);
            }

            Size = 8 + 4 + 4 + childrenSize;
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)Entries.Count);

            foreach (var e in Entries)
            {
                writer.Write((uint)(8 + (e.Data?.Length ?? 0)));
                writer.Write(e.Type.Value);
                if (e.Data != null && e.Data.Length > 0) writer.Write(e.Data);
            }
        }

        public override string ToString() => $"stsd: entries={Entries.Count}";
    }
}
