namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class SyncSampleBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public List<uint> SampleNumbers { get; } = [];

        public static SyncSampleBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new SyncSampleBox
            {
                Size = boxSize,
                Type = new FourCC("stss"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            uint entryCount = reader.ReadUInt32();
            for (uint i = 0; i < entryCount; i++)
                box.SampleNumbers.Add(reader.ReadUInt32());

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + (SampleNumbers.Count * 4);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((uint)SampleNumbers.Count);
            foreach (var n in SampleNumbers) writer.Write(n);
        }

        public override string ToString() => $"stss: count={SampleNumbers.Count}";
    }
}
