namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;
    using System.IO;

    public class TrackHeaderBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public ulong CreationTime { get; set; }
        public ulong ModificationTime { get; set; }
        public uint TrackId { get; set; }
        public ulong Duration { get; set; }

        public short Layer { get; set; }
        public short AlternateGroup { get; set; }
        public short Volume { get; set; }  // 8.8 fixed-point
        public uint Width { get; set; }    // 16.16 fixed-point
        public uint Height { get; set; }   // 16.16 fixed-point

        public static TrackHeaderBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new TrackHeaderBox
            {
                Size = boxSize,
                Type = new FourCC("tkhd"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3)
            };

            if (box.Version == 1)
            {
                box.CreationTime = reader.ReadUInt64();
                box.ModificationTime = reader.ReadUInt64();
                box.TrackId = reader.ReadUInt32();
                reader.ReadUInt32(); // reserved
                box.Duration = reader.ReadUInt64();
            }
            else // version 0
            {
                box.CreationTime = reader.ReadUInt32();
                box.ModificationTime = reader.ReadUInt32();
                box.TrackId = reader.ReadUInt32();
                reader.ReadUInt32(); // reserved
                box.Duration = reader.ReadUInt32();
            }

            reader.ReadUInt64(); // reserved (8 bytes)

            box.Layer = reader.ReadInt16();
            box.AlternateGroup = reader.ReadInt16();
            box.Volume = reader.ReadInt16();
            reader.ReadUInt16(); // reserved

            reader.ReadBytes(36); // transformation matrix (skip for now)

            box.Width = reader.ReadUInt32();
            box.Height = reader.ReadUInt32();

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            int timeFields = Version == 1 ? (8 + 8 + 4 + 4 + 8) : (4 + 4 + 4 + 4 + 4);
            Size = 8 + 4 + timeFields + 8 + 2 + 2 + 2 + 2 + 36 + 4 + 4;

            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);

            if (Version == 1)
            {
                writer.Write(CreationTime);
                writer.Write(ModificationTime);
                writer.Write(TrackId);
                writer.Write(0u); // reserved
                writer.Write(Duration);
            }
            else
            {
                writer.Write((uint)CreationTime);
                writer.Write((uint)ModificationTime);
                writer.Write(TrackId);
                writer.Write(0u); // reserved
                writer.Write((uint)Duration);
            }

            writer.Write(0ul); // reserved
            writer.Write(Layer);
            writer.Write(AlternateGroup);
            writer.Write(Volume);
            writer.Write((ushort)0); // reserved

            writer.Write(new byte[36]); // matrix
            writer.Write(Width);
            writer.Write(Height);
        }

        public override string ToString()
        {
            float w = Width / 65536f;
            float h = Height / 65536f;
            return $"tkhd: id={TrackId}, duration={Duration}, volume={Volume / 256f:F1}, size={w}x{h}";
        }
    }
}
