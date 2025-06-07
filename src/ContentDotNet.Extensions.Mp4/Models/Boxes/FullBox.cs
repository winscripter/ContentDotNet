namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   Represents a full box.
/// </summary>
public abstract class FullBox : Box
{
    /// <summary>
    ///   The version.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    ///   The flags.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="FullBox"/> class.
    /// </summary>
    /// <param name="size">Size</param>
    /// <param name="type">Type</param>
    protected FullBox(uint size, uint type)
        : base(size, type)
    {
    }

    protected static (uint size, uint type, byte version, uint flags) ParseFullBoxHeader(BinaryReader reader)
    {
        return (reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadByte(), IsoIOUtils.ReadUInt24(reader));
    }

    protected void WriteFullBoxHeader(BinaryWriter writer)
    {
        writer.Write(Size);
        writer.Write(Type);
        writer.Write(Version);
        IsoIOUtils.WriteUInt24(writer, Flags);
    }
}
