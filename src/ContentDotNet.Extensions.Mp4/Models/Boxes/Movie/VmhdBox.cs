namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
/// Represents the video media header box in a movie file, containing information about the video track's characteristics.
/// </summary>
[BoxInfo("vmhd", "Video Media Header Box", "Represents the video media header box in a movie file, containing information about the video track's characteristics.")]
public sealed class VmhdBox : IEquatable<VmhdBox?>
{
    private readonly MinfBox? _minf;

    /// <summary>
    /// Gets or sets the version of the video media header box.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    /// Gets or sets the flags associated with the video media header box.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    /// Gets or sets the graphics mode for the video track.
    /// </summary>
    public ushort GraphicsMode { get; set; }

    /// <summary>
    /// Gets or sets the red component of the operation color.
    /// </summary>
    public ushort OpColorR { get; set; }

    /// <summary>
    /// Gets or sets the green component of the operation color.
    /// </summary>
    public ushort OpColorG { get; set; }

    /// <summary>
    /// Gets or sets the blue component of the operation color.
    /// </summary>
    public ushort OpColorB { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VmhdBox"/> class.
    /// </summary>
    /// <param name="minf">The owning <see cref="MinfBox"/> instance.</param>
    /// <param name="version">The version of the box.</param>
    /// <param name="flags">The flags for the box.</param>
    /// <param name="graphicsMode">The graphics mode.</param>
    /// <param name="opColorR">The red component of the operation color.</param>
    /// <param name="opColorG">The green component of the operation color.</param>
    /// <param name="opColorB">The blue component of the operation color.</param>
    public VmhdBox(MinfBox? minf, byte version, uint flags, ushort graphicsMode, ushort opColorR, ushort opColorG, ushort opColorB)
    {
        _minf = minf;
        Version = version;
        Flags = flags;
        GraphicsMode = graphicsMode;
        OpColorR = opColorR;
        OpColorG = opColorG;
        OpColorB = opColorB;
    }

    /// <summary>
    /// Gets the owning <see cref="MinfBox"/> instance.
    /// </summary>
    /// <returns>The owner <see cref="MinfBox"/> or null.</returns>
    public MinfBox? GetOwner() => _minf;

    /// <summary>
    /// Reads a <see cref="VmhdBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <param name="owner">The owning <see cref="MinfBox"/> instance.</param>
    /// <returns>A new <see cref="VmhdBox"/> instance.</returns>
    public static VmhdBox Read(BinaryReader reader, MinfBox? owner)
    {
        byte version = reader.ReadByte();
        uint flags = (uint)(reader.ReadByte() << 16 |
                     reader.ReadByte() << 8 |
                     reader.ReadByte());
        ushort graphicsMode = reader.ReadUInt16();
        ushort opColorR = reader.ReadUInt16();
        ushort opColorG = reader.ReadUInt16();
        ushort opColorB = reader.ReadUInt16();
        return new VmhdBox(owner, version, flags, graphicsMode, opColorR, opColorG, opColorB);
    }

    /// <summary>
    /// Writes the <see cref="VmhdBox"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(Version);
        writer.Write((byte)(Flags >> 16));
        writer.Write((byte)(Flags >> 8));
        writer.Write((byte)Flags);
        writer.Write(GraphicsMode);
        writer.Write(OpColorR);
        writer.Write(OpColorG);
        writer.Write(OpColorB);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as VmhdBox);
    }

    /// <inheritdoc/>
    public bool Equals(VmhdBox? other)
    {
        return other is not null &&
               Version == other.Version &&
               Flags == other.Flags &&
               GraphicsMode == other.GraphicsMode &&
               OpColorR == other.OpColorR &&
               OpColorG == other.OpColorG &&
               OpColorB == other.OpColorB;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Version, Flags, GraphicsMode, OpColorR, OpColorG, OpColorB);
    }

    /// <summary>
    /// Determines whether two <see cref="VmhdBox"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="VmhdBox"/> to compare.</param>
    /// <param name="right">The second <see cref="VmhdBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(VmhdBox? left, VmhdBox? right)
    {
        return EqualityComparer<VmhdBox>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="VmhdBox"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="VmhdBox"/> to compare.</param>
    /// <param name="right">The second <see cref="VmhdBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(VmhdBox? left, VmhdBox? right)
    {
        return !(left == right);
    }
}
