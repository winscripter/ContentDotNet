namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class MediaInformationBox : Mp4BoxBase
    {
        public Mp4MediaHeaderBox? MediaHeader { get; set; }  // vmhd or smhd
        public DataInformationBox? DataInformation { get; set; }
        public SampleTableBox? SampleTable { get; set; }

        public static MediaInformationBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new MediaInformationBox
            {
                Size = boxSize,
                Type = new FourCC("minf")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());

                switch (childType.ToString())
                {
                    case "vmhd":
                        box.MediaHeader = VideoMediaHeaderBox.Parse(reader, childSize);
                        break;
                    case "smhd":
                        box.MediaHeader = SoundMediaHeaderBox.Parse(reader, childSize);
                        break;
                    case "dinf":
                        box.DataInformation = DataInformationBox.Parse(reader, childSize);
                        break;
                    case "stbl":
                        box.SampleTable = SampleTableBox.Parse(reader, childSize);
                        break;
                    default:
                        reader.BaseStream.Seek(childSize - 8, SeekOrigin.Current);
                        break;
                }
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            using var ms = new MemoryStream();
            using (var w = new BinaryWriter(ms))
            {
                MediaHeader?.Write(w);
                DataInformation?.Write(w);
                SampleTable?.Write(w);
            }

            Size = 8 + ms.Length;
            WriteBase(writer);
            writer.Write(ms.ToArray());
        }

        public override string ToString() => $"minf: header={MediaHeader?.Type}, stbl={(SampleTable != null ? "yes" : "no")}";
    }
}
