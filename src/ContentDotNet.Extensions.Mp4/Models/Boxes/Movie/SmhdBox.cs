namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
/// Represents the sound media header box in a movie file, containing information about the sound track's characteristics.
/// </summary>
[BoxInfo("smhd", "Sound Media Header Box", "Represents the sound media header box in a movie file, containing information about the sound track's characteristics.")]
public sealed class SmhdBox : IBoxData, IEquatable<SmhdBox?>
{
    private readonly MinfBox? _minf;

    /// <summary>
    /// Gets or sets the version of the box.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    /// Gets or sets the flags associated with the box.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    /// Gets or sets the balance of the sound track.
    /// </summary>
    public ushort Balance { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SmhdBox"/> class.
    /// </summary>
    /// <param name="minf">The owning <see cref="MinfBox"/>.</param>
    /// <param name="version">The version of the box.</param>
    /// <param name="flags">The flags associated with the box.</param>
    /// <param name="balance">The balance of the sound track.</param>
    public SmhdBox(MinfBox? minf, byte version, uint flags, ushort balance)
    {
        _minf = minf;
        Version = version;
        Flags = flags;
        Balance = balance;
    }

    /// <summary>
    /// Gets the owner <see cref="MinfBox"/> of this box.
    /// </summary>
    /// <returns>The owner <see cref="MinfBox"/>.</returns>
    public MinfBox? GetOwner() => _minf;

    /// <summary>
    /// Reads an <see cref="SmhdBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <param name="owner">The owning <see cref="MinfBox"/>.</param>
    /// <returns>A new instance of <see cref="SmhdBox"/>.</returns>
    public static SmhdBox Read(BinaryReader reader, MinfBox? owner)
    {
        byte version = reader.ReadByte();
        uint flags = (uint)(reader.ReadByte() << 16 | reader.ReadByte() << 8 | reader.ReadByte());
        ushort balance = reader.ReadUInt16();
        _ = reader.ReadUInt16(); // reserved field, typically set to 0

        return new SmhdBox(owner, version, flags, balance);
    }

    /// <summary>
    /// Writes the <see cref="SmhdBox"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(Version);
        writer.Write((byte)(Flags >> 16));
        writer.Write((byte)(Flags >> 8));
        writer.Write((byte)Flags);
        writer.Write(Balance);
        writer.Write((ushort)0); // reserved field, typically set to 0
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as SmhdBox);
    }

    /// <inheritdoc/>
    public bool Equals(SmhdBox? other)
    {
        return other is not null &&
               Version == other.Version &&
               Flags == other.Flags &&
               Balance == other.Balance;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Version, Flags, Balance);
    }

    /// <summary>
    /// Determines whether two <see cref="SmhdBox"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="SmhdBox"/> to compare.</param>
    /// <param name="right">The second <see cref="SmhdBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SmhdBox? left, SmhdBox? right)
    {
        return EqualityComparer<SmhdBox>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="SmhdBox"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="SmhdBox"/> to compare.</param>
    /// <param name="right">The second <see cref="SmhdBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SmhdBox? left, SmhdBox? right)
    {
        return !(left == right);
    }
}
