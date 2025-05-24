namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
///   Represents the MP4 MDHD (Media Header) box.
/// </summary>
[BoxInfo("mdhd", "Media Header", "Represents the header of the media in a movie file")]
public sealed class MdhdBox : IBoxData, IEquatable<MdhdBox?>
{
    private readonly MdiaBox? _mdia;

    /// <summary>
    ///   Gets or sets the version of the box.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    ///   Gets or sets the flags associated with the box.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    ///   Gets or sets the creation time of the media.
    /// </summary>
    public ulong CreationTime { get; set; }

    /// <summary>
    ///   Gets or sets the modification time of the media.
    /// </summary>
    public ulong ModificationTime { get; set; }

    /// <summary>
    ///   Gets or sets the timescale of the media.
    /// </summary>
    public uint Timescale { get; set; }

    /// <summary>
    ///   Gets or sets the duration of the media.
    /// </summary>
    public ulong Duration { get; set; }

    /// <summary>
    ///   Gets or sets the language code of the media.
    /// </summary>
    public ushort LanguageCode { get; set; }

    /// <summary>
    ///   Gets or sets the pre-defined field, typically reserved for future use.
    /// </summary>
    public ushort PreDefined { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="MdhdBox"/> class with the specified parameters.
    /// </summary>
    /// <param name="version">The version of the box.</param>
    /// <param name="flags">The flags associated with the box.</param>
    /// <param name="creationTime">The creation time of the media.</param>
    /// <param name="modificationTime">The modification time of the media.</param>
    /// <param name="timescale">The timescale of the media.</param>
    /// <param name="duration">The duration of the media.</param>
    /// <param name="languageCode">The language code of the media.</param>
    /// <param name="preDefined">The pre-defined field, typically reserved for future use.</param>
    /// <param name="mdia">Owner box</param>
    public MdhdBox(byte version, uint flags, ulong creationTime, ulong modificationTime, uint timescale, ulong duration, ushort languageCode, ushort preDefined, MdiaBox? mdia)
    {
        Version = version;
        Flags = flags;
        CreationTime = creationTime;
        ModificationTime = modificationTime;
        Timescale = timescale;
        Duration = duration;
        LanguageCode = languageCode;
        PreDefined = preDefined;
        _mdia = mdia;
    }

    /// <summary>
    ///   Returns the owner box.
    /// </summary>
    /// <returns>The box where this MDHD box is contained within.</returns>
    public MdiaBox? GetOwner() => _mdia;

    /// <summary>
    ///   Reads an <see cref="MdhdBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <param name="owner">The owner box.</param>
    /// <returns>A new instance of <see cref="MdhdBox"/> populated with data from the stream.</returns>
    public static MdhdBox Read(BinaryReader reader, MdiaBox? owner)
    {
        byte version = reader.ReadByte();
        uint flags = (uint)(reader.ReadByte() << 16) |
                     (uint)(reader.ReadByte() << 8) |
                     reader.ReadByte();
        ulong creationTime = version == 1 ? reader.ReadUInt64() : reader.ReadUInt32();
        ulong modificationTime = version == 1 ? reader.ReadUInt64() : reader.ReadUInt32();
        uint timescale = reader.ReadUInt32();
        ulong duration = version == 1 ? reader.ReadUInt64() : reader.ReadUInt32();
        ushort languageCode = reader.ReadUInt16();
        ushort preDefined = reader.ReadUInt16();
        return new MdhdBox(version, flags, creationTime, modificationTime, timescale, duration, languageCode, preDefined, owner);
    }

    /// <summary>
    ///   Writes the <see cref="MdhdBox"/> data to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(Version);
        writer.Write((byte)(Flags >> 16) | 0xFF);
        writer.Write((byte)(Flags >> 8) | 0xFF);
        writer.Write((byte)Flags | 0xFF);
        if (Version == 1)
        {
            writer.Write(CreationTime);
            writer.Write(ModificationTime);
            writer.Write(Timescale);
            writer.Write(Duration);
        }
        else
        {
            writer.Write((uint)CreationTime);
            writer.Write((uint)ModificationTime);
            writer.Write(Timescale);
            writer.Write((uint)Duration);
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as MdhdBox);
    }

    /// <inheritdoc/>
    public bool Equals(MdhdBox? other)
    {
        return other is not null &&
               Version == other.Version &&
               Flags == other.Flags &&
               CreationTime == other.CreationTime &&
               ModificationTime == other.ModificationTime &&
               Timescale == other.Timescale &&
               Duration == other.Duration &&
               LanguageCode == other.LanguageCode &&
               PreDefined == other.PreDefined;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Version, Flags, CreationTime, ModificationTime, Timescale, Duration, LanguageCode, PreDefined);
    }

    public static bool operator ==(MdhdBox? left, MdhdBox? right)
    {
        return EqualityComparer<MdhdBox>.Default.Equals(left, right);
    }

    public static bool operator !=(MdhdBox? left, MdhdBox? right)
    {
        return !(left == right);
    }
}
