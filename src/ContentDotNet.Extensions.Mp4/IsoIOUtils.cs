namespace ContentDotNet.Extensions.Mp4;

internal static class IsoIOUtils
{
    public static uint ReadUInt24(BinaryReader reader) =>
        (uint)((byte)(reader.ReadByte() << 16) | (byte)(reader.ReadByte() << 8) | reader.ReadByte());

    public static void WriteUInt24(BinaryWriter writer, uint value)
    {
        writer.Write((byte)((value >> 16) & 0xFF));
        writer.Write((byte)((value >> 8) & 0xFF));
        writer.Write((byte)(value & 0xFF));
    }
}
