using System.Text;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
/// Represents a URN Box ('urn ') in an MP4 file, which may reference a data location.
/// </summary>
[BoxInfo("urn ", "URN Box", "Represents a URN Box ('urn ') in an MP4 file, which may reference a data location.")]
public sealed class UrnBox
{
    private readonly DinfBox? _dinf;

    /// <summary>
    /// Gets or sets the flags for the URN box.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    /// Gets or sets the URN name.
    /// </summary>
    public string UrnName { get; set; }

    /// <summary>
    /// Gets or sets the URN URL.
    /// </summary>
    public string UrnUrl { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UrnBox"/> class.
    /// </summary>
    /// <param name="dinf">The owning <see cref="DinfBox"/>.</param>
    /// <param name="flags">The flags for the URN box.</param>
    /// <param name="urnName">The URN name.</param>
    /// <param name="urnUrl">The URN URL.</param>
    public UrnBox(DinfBox? dinf, uint flags, string urnName, string urnUrl)
    {
        _dinf = dinf;
        Flags = flags;
        UrnName = urnName;
        UrnUrl = urnUrl;
    }

    /// <summary>
    /// Gets the owner <see cref="DinfBox"/> of this URN box.
    /// </summary>
    /// <returns>The owner <see cref="DinfBox"/>.</returns>
    public DinfBox? GetOwner() => _dinf;

    /// <summary>
    /// Reads a <see cref="UrnBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <param name="owner">The owning <see cref="DinfBox"/>.</param>
    /// <returns>A new instance of <see cref="UrnBox"/>.</returns>
    public static UrnBox Read(BinaryReader reader, DinfBox? owner)
    {
        uint flags = reader.ReadUInt32();
        if (flags == 1)
            return new UrnBox(owner, flags, string.Empty, string.Empty);

        var urnNameBuilder = new StringBuilder();
        byte last;
        while ((last = reader.ReadByte()) != 0)
            urnNameBuilder.Append((char)last);

        var urnUrlBuilder = new StringBuilder();
        while ((last = reader.ReadByte()) != 0)
            urnUrlBuilder.Append((char)last);

        return new UrnBox(owner, flags, urnNameBuilder.ToString(), urnUrlBuilder.ToString());
    }

    /// <summary>
    /// Writes this <see cref="UrnBox"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(Flags);
        if (Flags == 0)
        {
            foreach (char c in UrnName)
                writer.Write((byte)c);
            writer.Write((byte)0); // Null-terminator for urnName
            foreach (char c in UrnUrl)
                writer.Write((byte)c);
            writer.Write((byte)0); // Null-terminator for urnUrl
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="UrnBox"/> is equal to the current <see cref="UrnBox"/>.
    /// </summary>
    /// <param name="other">The <see cref="UrnBox"/> to compare with the current <see cref="UrnBox"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="UrnBox"/> is equal to the current <see cref="UrnBox"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(UrnBox? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Flags == other.Flags &&
               UrnName == other.UrnName &&
               UrnUrl == other.UrnUrl;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="UrnBox"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="UrnBox"/>.</param>
    /// <returns><c>true</c> if the specified object is equal to the current <see cref="UrnBox"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as UrnBox);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="UrnBox"/>.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Flags, UrnName, UrnUrl);
    }

    /// <summary>
    /// Determines whether two <see cref="UrnBox"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="UrnBox"/> to compare.</param>
    /// <param name="right">The second <see cref="UrnBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(UrnBox? left, UrnBox? right)
    {
        return EqualityComparer<UrnBox?>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="UrnBox"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="UrnBox"/> to compare.</param>
    /// <param name="right">The second <see cref="UrnBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(UrnBox? left, UrnBox? right)
    {
        return !(left == right);
    }
}
