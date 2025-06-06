namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   A single MP4 box.
/// </summary>
public abstract class Box
{
    /// <summary>
    ///   Size of the box.
    /// </summary>
    public uint Size { get; set; }

    /// <summary>
    ///   Type of the box (a fourCC).
    /// </summary>
    public uint Type { get; set; }

    /// <summary>
    ///   Writes this box to <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">The binary writer where the box is written to.</param>
    public abstract void Write(BinaryWriter writer);

    /// <summary>
    ///   Can this box be written to without additional parameters?
    /// </summary>
    public abstract bool CanWriteWithoutParameters { get; }

    /// <summary>
    ///   Child boxes.
    /// </summary>
    public Box[]? Children { get; }

    /// <summary>
    ///   Does this box require child boxes?
    /// </summary>
    public abstract bool RequiresChildBoxes { get; }

    /// <summary>
    ///   Parses the header.
    /// </summary>
    /// <param name="reader">The reader where the header is parsed from.</param>
    protected void ParseHeader(BinaryReader reader)
    {
        this.Size = reader.ReadUInt32();
        this.Type = reader.ReadUInt32();
    }

    /// <summary>
    ///   Is this box compatible with <paramref name="other"/>?
    /// </summary>
    /// <param name="other">Box to compare with.</param>
    /// <returns>A boolean, indicating compatibility with <paramref name="other"/>.</returns>
    public abstract bool IsCompatibleWith(Box other);

    /// <summary>
    ///   Parses just the size/type.
    /// </summary>
    /// <param name="reader">Binary reader</param>
    /// <returns>Size and type</returns>
    protected static (uint size, uint type) ParseSizeType(BinaryReader reader)
    {
        return (reader.ReadUInt32(), reader.ReadUInt32());
    }

    /// <summary>
    ///   Writes just the size and the type.
    /// </summary>
    /// <param name="writer">Binary writer where the size and type are written to.</param>
    public void WriteSizeAndType(BinaryWriter writer)
    {
        writer.Write(this.Size);
        writer.Write(this.Type);
    }
}
