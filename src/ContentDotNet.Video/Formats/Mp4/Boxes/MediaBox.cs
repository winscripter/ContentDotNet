namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class MediaBox : Mp4BoxBase
    {
        public MediaHeaderBox? MediaHeader { get; set; }
        public HandlerBox? Handler { get; set; }
        public MediaInformationBox? MediaInformation { get; set; }

        public static MediaBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new MediaBox
            {
                Size = boxSize,
                Type = new FourCC("mdia")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());

                switch (childType.ToString())
                {
                    case "mdhd":
                        box.MediaHeader = MediaHeaderBox.Parse(reader, childSize);
                        break;
                    case "hdlr":
                        box.Handler = HandlerBox.Parse(reader, childSize);
                        break;
                    case "minf":
                        box.MediaInformation = MediaInformationBox.Parse(reader, childSize);
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
                Handler?.Write(w);
                MediaInformation?.Write(w);
            }

            Size = 8 + ms.Length;
            WriteBase(writer);
            writer.Write(ms.ToArray());
        }

        public override string ToString() => $"mdia: handler={Handler?.HandlerType}, hasMinf={(MediaInformation != null)}";
    }
}
