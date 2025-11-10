namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class SampleSizeBox : Mp4SampleSizeBoxBase
    {
        private const int MaximumSamples = 32_000_000;

        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public uint SampleSize { get; set; } // if non-zero ⇒ all samples same size
        public uint SampleCount { get; set; }
        public List<uint> EntrySizes { get; } = []; // present only if SampleSize == 0

        public static SampleSizeBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new SampleSizeBox
            {
                Size = boxSize,
                Type = new FourCC("stsz"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3),

                SampleSize = reader.ReadUInt32(),
                SampleCount = reader.ReadUInt32()
            };

            if (box.SampleCount > MaximumSamples)
                throw new InvalidOperationException("Too many stsz samples");

            if (box.SampleSize == 0)
            {
                for (uint i = 0; i < box.SampleCount; i++)
                    box.EntrySizes.Add(reader.ReadUInt32());
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + 4 + 4 + (SampleSize == 0 ? EntrySizes.Count * 4 : 0);
            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write(SampleSize);
            writer.Write(SampleCount);
            if (SampleSize == 0)
                foreach (var s in EntrySizes) writer.Write(s);
        }

        public override string ToString() => $"stsz: sampleSize={SampleSize}, count={SampleCount}";
    }
}
