namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using System.Text;

    public class DataReferenceEntry
    {
        public uint Size { get; set; }
        public FourCC Type { get; set; }
        public byte[] Flags { get; set; } = new byte[3];
        public string Location { get; set; } = string.Empty;

        public static DataReferenceEntry Parse(BinaryReader reader)
        {
            uint size = reader.ReadUInt32();
            var type = new FourCC(reader.ReadUInt32());
            _ = reader.ReadByte(); // version
            byte[] flags = reader.ReadBytes(3);

            int payloadLen = (int)size - 12; // exclude size+type+version+flags
            string location = string.Empty;
            if (payloadLen > 0)
            {
                byte[] bytes = reader.ReadBytes(payloadLen);
                int nullIndex = Array.IndexOf(bytes, (byte)0);
                if (nullIndex >= 0) bytes = bytes[..nullIndex];
                location = Encoding.UTF8.GetString(bytes);
            }

            return new DataReferenceEntry
            {
                Size = size,
                Type = type,
                Flags = flags,
                Location = location
            };
        }

        public void Write(BinaryWriter writer)
        {
            byte[] locBytes = string.IsNullOrEmpty(Location) ? [] :
                Encoding.UTF8.GetBytes(Location + "\0");

            Size = (uint)(12 + locBytes.Length);
            writer.Write(Size);
            writer.Write(Type.Value);
            writer.Write((byte)0); // version
            writer.Write(Flags);
            writer.Write(locBytes);
        }

        public override string ToString() => $"Entry: type='{Type}', location='{Location}'";
    }
}
