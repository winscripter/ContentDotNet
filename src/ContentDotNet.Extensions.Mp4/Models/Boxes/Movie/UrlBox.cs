using System.Text;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
/// Represents a URL Box ('url ') in an MP4 file, which may reference a data location.
/// </summary>
[BoxInfo("url ", "URL Box", "Represents a URL Box ('url ') in an MP4 file, which may reference a data location.")]
public sealed class UrlBox : IEquatable<UrlBox?>
{
    private readonly DinfBox? _dinf;

    /// <summary>
    /// Gets or sets the flags associated with this URL box.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    /// Gets or sets the URL string referenced by this box.
    /// </summary>
    public string UrlString { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UrlBox"/> class.
    /// </summary>
    /// <param name="dinf">The owning <see cref="DinfBox"/> instance, or null.</param>
    /// <param name="flags">The flags value for this box.</param>
    /// <param name="urlString">The URL string for this box.</param>
    public UrlBox(DinfBox? dinf, uint flags, string urlString)
    {
        _dinf = dinf;
        Flags = flags;
        UrlString = urlString;
    }

    /// <summary>
    /// Gets the owner <see cref="DinfBox"/> of this URL box.
    /// </summary>
    /// <returns>The owner <see cref="DinfBox"/>, or null if not set.</returns>
    public DinfBox? GetOwner() => _dinf;

    /// <summary>
    /// Reads a <see cref="UrlBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <param name="owner">The owning <see cref="DinfBox"/>, or null.</param>
    /// <returns>A new instance of <see cref="UrlBox"/>.</returns>
    public static UrlBox Read(BinaryReader reader, DinfBox? owner)
    {
        uint flags = reader.ReadUInt32();
        if (flags == 1)
            return new UrlBox(owner, flags, string.Empty);

        var builder = new StringBuilder();
        byte last;
        while ((last = reader.ReadByte()) != 0)
            builder.Append((char)last);

        return new UrlBox(owner, flags, builder.ToString());
    }

    /// <summary>
    /// Writes this <see cref="UrlBox"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(Flags);
        if (Flags == 0)
        {
            foreach (char c in UrlString)
                writer.Write((byte)c);
            writer.Write((byte)0); // Null-terminator
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as UrlBox);
    }

    /// <inheritdoc/>
    public bool Equals(UrlBox? other)
    {
        return other is not null &&
               EqualityComparer<DinfBox?>.Default.Equals(_dinf, other._dinf) &&
               Flags == other.Flags &&
               UrlString == other.UrlString;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(_dinf, Flags, UrlString);
    }

    /// <summary>
    /// Determines whether two <see cref="UrlBox"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="UrlBox"/> to compare.</param>
    /// <param name="right">The second <see cref="UrlBox"/> to compare.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(UrlBox? left, UrlBox? right)
    {
        return EqualityComparer<UrlBox>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="UrlBox"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="UrlBox"/> to compare.</param>
    /// <param name="right">The second <see cref="UrlBox"/> to compare.</param>
    /// <returns>True if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(UrlBox? left, UrlBox? right)
    {
        return !(left == right);
    }
}
