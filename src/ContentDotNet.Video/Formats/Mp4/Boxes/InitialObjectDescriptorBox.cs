namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class InitialObjectDescriptorBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];
        public byte[] DescriptorData { get; set; } = [];

        public static InitialObjectDescriptorBox Parse(BinaryReader reader, long boxSize, int maxDescriptorSize = 4096)
        {
            var box = new InitialObjectDescriptorBox
            {
                Size = boxSize,
                Type = new FourCC("iods"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            int descriptorLength = (int)boxSize - 12;
            if (descriptorLength < 0 || descriptorLength > maxDescriptorSize)
                throw new InvalidDataException("Invalid iods descriptor length");

            box.DescriptorData = reader.ReadBytes(descriptorLength);

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            Size = 8 + 4 + DescriptorData.Length;
            WriteBase(writer);
            writer.Write(Version);
            writer.Write(Flags);
            writer.Write(DescriptorData);
        }

        public override string ToString() => $"iods: descriptor={DescriptorData.Length} bytes";
    }
}
