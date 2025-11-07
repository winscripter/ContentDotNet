namespace ContentDotNet.Api.Binary;

public static class BinaryReaderExtensions
{
    public static uint ReadUInt24(this BinaryReader reader) =>
        (uint)(reader.ReadByte() | (reader.ReadByte() << 8) | (reader.ReadByte() << 16));
}
