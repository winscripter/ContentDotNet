namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;
    using System.IO;

    public class MediaHeaderBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public ulong CreationTime { get; set; }
        public ulong ModificationTime { get; set; }
        public uint Timescale { get; set; }
        public ulong Duration { get; set; }

        public ushort Language { get; set; }      // packed ISO-639-2 code
        public ushort Predefined { get; set; }

        public static MediaHeaderBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new MediaHeaderBox
            {
                Size = boxSize,
                Type = new FourCC("mdhd"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            if (box.Version == 1)
            {
                box.CreationTime = reader.ReadUInt64();
                box.ModificationTime = reader.ReadUInt64();
                box.Timescale = reader.ReadUInt32();
                box.Duration = reader.ReadUInt64();
            }
            else
            {
                box.CreationTime = reader.ReadUInt32();
                box.ModificationTime = reader.ReadUInt32();
                box.Timescale = reader.ReadUInt32();
                box.Duration = reader.ReadUInt32();
            }

            box.Language = reader.ReadUInt16();
            box.Predefined = reader.ReadUInt16();

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            int timeFields = Version == 1 ? (8 + 8 + 4 + 8) : (4 + 4 + 4 + 4);
            Size = 8 + 4 + timeFields + 2 + 2;

            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);

            if (Version == 1)
            {
                writer.Write(CreationTime);
                writer.Write(ModificationTime);
                writer.Write(Timescale);
                writer.Write(Duration);
            }
            else
            {
                writer.Write((uint)CreationTime);
                writer.Write((uint)ModificationTime);
                writer.Write(Timescale);
                writer.Write((uint)Duration);
            }

            writer.Write(Language);
            writer.Write(Predefined);
        }

        public string GetLanguageCode()
        {
            // ISO-639-2/T packed 15-bit language code (5 bits per letter)
            int c1 = ((Language >> 10) & 0x1F) + 0x60;
            int c2 = ((Language >> 5) & 0x1F) + 0x60;
            int c3 = (Language & 0x1F) + 0x60;
            return $"{(char)c1}{(char)c2}{(char)c3}";
        }

        public override string ToString()
        {
            string lang = GetLanguageCode();
            return $"mdhd: version={Version}, timescale={Timescale}, duration={Duration}, language={lang}";
        }
    }
}
