using System.Buffers.Binary;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   Represents the box header.
/// </summary>
public readonly struct BoxHeader
{
    /// <summary>
    ///   Size of the entire box, in bytes.
    /// </summary>
    public readonly uint Size;

    /// <summary>
    ///   Type of the box.
    /// </summary>
    public readonly uint Type;

    public BoxHeader(uint size, uint type)
    {
        Size = size;
        Type = type;
    }

    /// <summary>
    ///   Parses the MP4 box.
    /// </summary>
    /// <param name="reader">Binary reader from where the box is parsed.</param>
    /// <returns>The box header.</returns>
    public static BoxHeader Read(BinaryReader reader)
    {
        uint size = BinaryPrimitives.ReverseEndianness(reader.ReadUInt32());
        uint type = reader.ReadUInt32();
        return new BoxHeader(size, type);
    }

    /// <summary>
    ///   Writes this box into the specified binary writer.
    /// </summary>
    /// <param name="writer">Binary writer.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(BinaryPrimitives.ReverseEndianness(Size));
        writer.Write(Type);
    }
}
