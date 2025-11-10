namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection.PortableExecutable;

    public class MovieBox : Mp4BoxBase
    {
        public MovieHeaderBox? MovieHeader { get; set; }
        public InitialObjectDescriptorBox? Iods { get; set; }
        public List<TrackBox> Tracks { get; set; } = [];

        public static MovieBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new MovieBox
            {
                Size = boxSize,
                Type = new FourCC("moov")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());

                switch (childType.ToString())
                {
                    case "mvhd":
                        box.MovieHeader = MovieHeaderBox.Parse(reader, childSize);
                        break;
                    case "iods":
                        box.Iods = InitialObjectDescriptorBox.Parse(reader, childSize);
                        break;
                    case "trak":
                        box.Tracks.Add(TrackBox.Parse(reader, childSize));
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
                MovieHeader?.Write(w);
                foreach (var trak in Tracks)
                    trak.Write(w);
                Iods?.Write(w);
            }

            Size = 8 + ms.Length;
            WriteBase(writer);
            writer.Write(ms.ToArray());
        }

        public override string ToString() => $"moov: {Tracks.Count} track(s), iods={(Iods != null)}";
    }
}
