namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using System.Text;

    public class HandlerBox : Mp4BoxBase
    {
        public byte Version { get; set; }
        public byte[] Flags { get; set; } = new byte[3];

        public uint PreDefined { get; set; }
        public FourCC HandlerType { get; set; }
        public uint[] Reserved { get; set; } = new uint[3];
        public string Name { get; set; } = "";

        public static HandlerBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new HandlerBox
            {
                Size = boxSize,
                Type = new FourCC("hdlr"),
                Version = reader.ReadByte(),
                Flags = reader.ReadBytes(3),
                PreDefined = reader.ReadUInt32(),
                HandlerType = new FourCC(reader.ReadUInt32())
            };
            for (int i = 0; i < 3; i++)
                box.Reserved[i] = reader.ReadUInt32();

            // Remaining bytes → handler name (null-terminated)
            long remaining = boxSize - 32; // 8 header + 24 bytes read
            if (remaining > 0)
            {
                byte[] nameBytes = reader.ReadBytes((int)remaining);
                int nullIndex = Array.IndexOf(nameBytes, (byte)0);
                if (nullIndex >= 0)
                    nameBytes = nameBytes[..nullIndex];

                box.Name = Encoding.UTF8.GetString(nameBytes);
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            byte[] nameBytes = string.IsNullOrEmpty(Name)
                ? []
                : Encoding.UTF8.GetBytes(Name + "\0");

            Size = 8 + 4 + 4 + 12 + nameBytes.Length;

            WriteBase(writer);

            writer.Write(Version);
            writer.Write(Flags);
            writer.Write(PreDefined);
            writer.Write(HandlerType.Value);
            foreach (var r in Reserved)
                writer.Write(r);
            writer.Write(nameBytes);
        }

        public override string ToString()
        {
            return $"hdlr: handler='{HandlerType}', name='{Name}'";
        }
    }
}
