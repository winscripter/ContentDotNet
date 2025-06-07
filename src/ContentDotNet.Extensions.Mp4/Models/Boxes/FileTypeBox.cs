using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
/// Represents the 'ftyp' (File Type) box in an MP4 file, which describes the type of the MP4 video file.
/// </summary>
[BoxInfo("ftyp", "File Type", "Describes the type of the MP4 video file")]
public sealed class FileTypeBox : Box, IEquatable<FileTypeBox?>
{
    /// <summary>
    /// Gets or sets the major brand of the file type.
    /// </summary>
    public FourCC MajorBrand { get; set; }

    /// <summary>
    /// Gets or sets the minor version of the file type.
    /// </summary>
    public FourCC MinorVersion { get; set; }

    /// <summary>
    /// Gets or sets the list of compatible brands.
    /// </summary>
    public List<FourCC> CompatibleBrands { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeBox"/> class.
    /// </summary>
    /// <param name="size">The size of the box.</param>
    /// <param name="type">The type of the box (FourCC).</param>
    /// <param name="majorBrand">The major brand.</param>
    /// <param name="minorVersion">The minor version.</param>
    /// <param name="compatibleBrands">The list of compatible brands.</param>
    public FileTypeBox(uint size, uint type, FourCC majorBrand, FourCC minorVersion, List<FourCC> compatibleBrands)
        : base(size, type)
    {
        MajorBrand = majorBrand;
        MinorVersion = minorVersion;
        CompatibleBrands = compatibleBrands;
        Size = size;
        Type = type;
    }

    /// <inheritdoc/>
    public override bool CanWriteWithoutParameters => true;

    /// <inheritdoc/>
    public override bool RequiresChildBoxes => false;

    /// <inheritdoc/>
    public override bool IsCompatibleWith(Box other)
    {
        return other.Type == this.Type;
    }

    /// <summary>
    /// Reads a <see cref="FileTypeBox"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <returns>A new instance of <see cref="FileTypeBox"/>.</returns>
    public static FileTypeBox Read(BinaryReader reader)
    {
        var (size, type) = ParseSizeType(reader);
        FourCC majorBrand = reader.ReadUInt32();
        FourCC minorVersion = reader.ReadUInt32();
        uint numCBs = reader.ReadUInt32();
        var cbs = new FourCC[numCBs];
        for (int i = 0; i < numCBs; i++)
            cbs[i] = reader.ReadUInt32();
        return new FileTypeBox(size, type, majorBrand, minorVersion, [.. cbs]);
    }

    /// <summary>
    /// Writes this <see cref="FileTypeBox"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public override void Write(BinaryWriter writer)
    {
        base.WriteSizeAndType(writer);
        writer.Write(MajorBrand);
        writer.Write(MinorVersion);
        writer.Write((uint)CompatibleBrands.Count);
        foreach (FourCC cc in CompatibleBrands)
            writer.Write(cc);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as FileTypeBox);
    }

    /// <summary>
    /// Determines whether the specified <see cref="FileTypeBox"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="FileTypeBox"/> to compare with.</param>
    /// <returns><c>true</c> if the specified <see cref="FileTypeBox"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public bool Equals(FileTypeBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               MajorBrand.Equals(other.MajorBrand) &&
               MinorVersion.Equals(other.MinorVersion) &&
               EqualityComparer<List<FourCC>>.Default.Equals(CompatibleBrands, other.CompatibleBrands) &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               RequiresChildBoxes == other.RequiresChildBoxes;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Size);
        hash.Add(Type);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(Children);
        hash.Add(RequiresChildBoxes);
        hash.Add(MajorBrand);
        hash.Add(MinorVersion);
        hash.Add(CompatibleBrands);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="FileTypeBox"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="FileTypeBox"/> to compare.</param>
    /// <param name="right">The second <see cref="FileTypeBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(FileTypeBox? left, FileTypeBox? right)
    {
        return EqualityComparer<FileTypeBox>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="FileTypeBox"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="FileTypeBox"/> to compare.</param>
    /// <param name="right">The second <see cref="FileTypeBox"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(FileTypeBox? left, FileTypeBox? right)
    {
        return !(left == right);
    }
}
