using ContentDotNet.Primitives;
using System.Text;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
///   Represents the MP4 HDLR (Handler) box.
/// </summary>
[BoxInfo("hdlr", "Handler", "Represents the handler reference box in a movie file, which specifies the type of media handler used for the media data.")]
public sealed class HdlrBox : IBoxData, IEquatable<HdlrBox?>
{
    private readonly MdiaBox? _owner;

    /// <summary>
    ///   Gets or sets the version of the box.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    ///   Gets or sets the flags associated with the box.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    ///   Gets or sets the handler type, which indicates the type of media handler.
    /// </summary>
    public FourCC HandlerType { get; set; }

    /// <summary>
    ///   Gets or sets the name of the handler.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="HdlrBox"/> class with the specified version, flags, handler type, and name.
    /// </summary>
    /// <param name="version">The version of the box.</param>
    /// <param name="flags">The flags associated with the box.</param>
    /// <param name="handlerType">The handler type, indicating the type of media handler.</param>
    /// <param name="owner">Owner box</param>
    /// <param name="name">The name of the handler.</param>
    public HdlrBox(byte version, uint flags, FourCC handlerType, string name, MdiaBox? owner)
    {
        Version = version;
        Flags = flags;
        HandlerType = handlerType;
        Name = name;
        _owner = owner;
    }

    /// <summary>
    ///   Returns the owner box.
    /// </summary>
    /// <returns>Box where this HDLR box is contained within.</returns>
    public MdiaBox? GetOwner() => _owner;

    /// <summary>
    /// Reads an <see cref="HdlrBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <param name="owner">Owner box</param>
    /// <returns>A new instance of <see cref="HdlrBox"/> populated with data from the stream.</returns>
    public static HdlrBox Read(BinaryReader reader, MdiaBox? owner)
    {
        byte version = reader.ReadByte();
        uint flags = (uint)(reader.ReadByte() << 16 | reader.ReadByte() << 8 | reader.ReadByte());
        for (int i = 0; i < 3; i++)
            _ = reader.ReadUInt32();
        var handlerType = new FourCC(reader.ReadUInt32());
        var name = new StringBuilder();
        byte last;
        while ((last = reader.ReadByte()) != 0)
            name.Append((char)last);
        return new HdlrBox(version, flags, handlerType, name.ToString(), owner);
    }

    /// <summary>
    /// Writes the <see cref="HdlrBox"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(Version);
        writer.Write((byte)(Flags >> 16));
        writer.Write((byte)(Flags >> 8));
        writer.Write((byte)Flags);
        for (int i = 0; i < 3; i++)
            writer.Write(0u); // Reserved fields
        writer.Write(HandlerType.Value);
        foreach (char c in Name)
            writer.Write((byte)c);
        writer.Write((byte)0); // Null-terminator for the name
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as HdlrBox);
    }

    /// <summary>
    /// Determines whether the specified <see cref="HdlrBox"/> is equal to the current <see cref="HdlrBox"/>.
    /// </summary>
    /// <param name="other">The <see cref="HdlrBox"/> to compare with the current <see cref="HdlrBox"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="HdlrBox"/> is equal to the current <see cref="HdlrBox"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(HdlrBox? other)
    {
        return other is not null &&
               Version == other.Version &&
               Flags == other.Flags &&
               HandlerType.Equals(other.HandlerType) &&
               Name == other.Name;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Version, Flags, HandlerType, Name);
    }

    public static bool operator ==(HdlrBox? left, HdlrBox? right)
    {
        return EqualityComparer<HdlrBox>.Default.Equals(left, right);
    }

    public static bool operator !=(HdlrBox? left, HdlrBox? right)
    {
        return !(left == right);
    }
}
