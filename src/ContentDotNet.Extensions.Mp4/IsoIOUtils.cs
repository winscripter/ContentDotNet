using System.Text;

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

    public static string ReadNullTerminatedString(this BinaryReader reader)
    {
        var stringBuilder = new StringBuilder();
        char last;
        while ((last = reader.ReadChar()) != 0)
            stringBuilder.Append(last);
        return stringBuilder.ToString();
    }

    public static void WriteNullTerminatedString(this BinaryWriter writer, string str)
    {
        foreach (char c in str)
            writer.Write((byte)c);
        writer.Write((byte)0);
    }
}
