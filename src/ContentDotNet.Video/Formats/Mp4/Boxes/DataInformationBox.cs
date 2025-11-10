namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using System.IO;

    public class DataInformationBox : Mp4BoxBase
    {
        public DataReferenceBox? DataReference { get; set; }

        public static DataInformationBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new DataInformationBox
            {
                Size = boxSize,
                Type = new FourCC("dinf")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());

                if (childType.ToString() == "dref")
                {
                    box.DataReference = DataReferenceBox.Parse(reader, childSize);
                }
                else
                {
                    // Skip unknown children
                    reader.BaseStream.Seek(childSize - 8, SeekOrigin.Current);
                }
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            using var ms = new MemoryStream();
            using (var childWriter = new BinaryWriter(ms))
                DataReference?.Write(childWriter);

            Size = 8 + (int)ms.Length;
            WriteBase(writer);
            writer.Write(ms.ToArray());
        }

        public override string ToString() =>
            $"dinf: {(DataReference != null ? DataReference.ToString() : "no dref")}";
    }
}
