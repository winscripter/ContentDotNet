namespace ContentDotNet.Binary;

public static class BinaryWriterExtensions
{
    public static void WriteUInt24(this BinaryWriter writer, uint value)
    {
        writer.Write((byte)(value & 0xFF));
        writer.Write((byte)((value >> 8) & 0xFF));
        writer.Write((byte)((value >> 16) & 0xFF));
    }
}
