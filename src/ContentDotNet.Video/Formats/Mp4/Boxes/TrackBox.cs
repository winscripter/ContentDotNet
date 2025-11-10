namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    public class TrackBox : Mp4BoxBase
    {
        public TrackHeaderBox? TrackHeader { get; set; }
        public EditBox? Edit { get; set; }
        public MediaBox? Media { get; set; }

        public static TrackBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new TrackBox
            {
                Size = boxSize,
                Type = new FourCC("trak")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());

                switch (childType.ToString())
                {
                    case "tkhd":
                        box.TrackHeader = TrackHeaderBox.Parse(reader, childSize);
                        break;
                    case "edts":
                        box.Edit = EditBox.Parse(reader, childSize);
                        break;
                    case "mdia":
                        box.Media = MediaBox.Parse(reader, childSize);
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
                TrackHeader?.Write(w);
                Edit?.Write(w);
                Media?.Write(w);
            }

            Size = 8 + ms.Length;
            WriteBase(writer);
            writer.Write(ms.ToArray());
        }

        public override string ToString()
        {
            string handler = Media?.Handler?.HandlerType.ToString() ?? "unknown";
            return $"trak: id={TrackHeader?.TrackId}, type={handler}";
        }
    }
}
