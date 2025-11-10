namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;
    using System;
    using System.IO;

    public class MovieHeaderBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public ulong CreationTime { get; set; }
        public ulong ModificationTime { get; set; }
        public uint Timescale { get; set; }
        public ulong Duration { get; set; }

        public uint Rate { get; set; }       // 16.16 fixed-point
        public ushort Volume { get; set; }   // 8.8  fixed-point
        public uint NextTrackId { get; set; }

        // Parse mvhd
        public static MovieHeaderBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new MovieHeaderBox
            {
                Size = boxSize,
                Type = new FourCC("mvhd"),
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
            else // version 0
            {
                box.CreationTime = reader.ReadUInt32();
                box.ModificationTime = reader.ReadUInt32();
                box.Timescale = reader.ReadUInt32();
                box.Duration = reader.ReadUInt32();
            }

            box.Rate = reader.ReadUInt32();
            box.Volume = reader.ReadUInt16();

            reader.ReadBytes(10); // reserved
            reader.ReadBytes(36); // matrix
            reader.ReadBytes(24); // pre_defined

            box.NextTrackId = reader.ReadUInt32();

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            // Compute size (fixed fields only)
            int timeFields = Version == 1 ? (8 + 8 + 4 + 8) : (4 + 4 + 4 + 4);
            Size = 8 + 4 + timeFields + 4 + 2 + 10 + 36 + 24 + 4;

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

            writer.Write(Rate);
            writer.Write(Volume);
            writer.Write(new byte[10]); // reserved
            writer.Write(new byte[36]); // matrix (identity by default)
            writer.Write(new byte[24]); // pre_defined
            writer.Write(NextTrackId);
        }

        public override string ToString()
        {
            return $"mvhd: version={Version}, timescale={Timescale}, duration={Duration}, nextTrackId={NextTrackId}";
        }
    }
}
