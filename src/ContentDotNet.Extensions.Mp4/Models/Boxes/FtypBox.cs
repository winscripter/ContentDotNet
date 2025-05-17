using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   Represents an MP4 FTYP (File Type) box.
/// </summary>
public struct FtypBox : IEquatable<FtypBox>
{
    /// <summary>
    ///   The major brand.
    /// </summary>
    public FourCC MajorBrand { get; set; }

    /// <summary>
    ///   The minor version.
    /// </summary>
    public FourCC MinorVersion { get; set; }

    /// <summary>
    ///   Number of elements in <see cref="CompatibleBrands"/>.
    /// </summary>
    public uint CompatibleBrandsCount { get; set; }

    /// <summary>
    ///   List of compatible brands.
    /// </summary>
    public List<FourCC> CompatibleBrands { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="FtypBox"/> structure.
    /// </summary>
    /// <param name="majorBrand">The major brand.</param>
    /// <param name="minorVersion">The minor version.</param>
    /// <param name="compatibleBrandsCount">The number of compatible brands.</param>
    /// <param name="compatibleBrands">All compatible brands.</param>
    public FtypBox(FourCC majorBrand, FourCC minorVersion, uint compatibleBrandsCount, List<FourCC> compatibleBrands)
    {
        MajorBrand = majorBrand;
        MinorVersion = minorVersion;
        CompatibleBrandsCount = compatibleBrandsCount;
        CompatibleBrands = compatibleBrands;
    }

    /// <summary>
    ///   Reads an <see cref="FtypBox"/> structure from a binary stream.
    /// </summary>
    /// <param name="br">The binary reader.</param>
    /// <returns>An instance of <see cref="FtypBox"/> read from the stream.</returns>
    public static FtypBox Read(BinaryReader br)
    {
        FourCC majorBrand = br.ReadUInt32();
        FourCC minorVersion = br.ReadUInt32();
        uint compatibleBrandsCount = br.ReadUInt32();

        var compatibleBrands = new List<FourCC>();
        for (int i = 0; i < compatibleBrandsCount; i++)
            compatibleBrands.Add(br.ReadUInt32());

        return new FtypBox(majorBrand, minorVersion, compatibleBrandsCount, compatibleBrands);
    }

    /// <summary>
    ///   Writes this <see cref="FtypBox"/> to a binary stream.
    /// </summary>
    /// <param name="writer">The binary writer.</param>
    public readonly void Write(BinaryWriter writer)
    {
        writer.Write(MajorBrand);
        writer.Write(MinorVersion);
        writer.Write(CompatibleBrandsCount);
        for (int i = 0; i < CompatibleBrandsCount; i++)
            writer.Write(CompatibleBrands[i]);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is FtypBox box && Equals(box);
    }

    /// <summary>
    ///   Determines whether this instance and another specified <see cref="FtypBox"/> object have the same value.
    /// </summary>
    /// <param name="other">The <see cref="FtypBox"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(FtypBox other)
    {
        return MajorBrand.Equals(other.MajorBrand) &&
               MinorVersion.Equals(other.MinorVersion) &&
               CompatibleBrandsCount == other.CompatibleBrandsCount &&
               EqualityComparer<List<FourCC>>.Default.Equals(CompatibleBrands, other.CompatibleBrands);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(MajorBrand, MinorVersion, CompatibleBrandsCount, CompatibleBrands);
    }

    /// <summary>
    ///   Determines whether two specified <see cref="FtypBox"/> instances have the same value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if both instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(FtypBox left, FtypBox right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two specified <see cref="FtypBox"/> instances have different values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(FtypBox left, FtypBox right)
    {
        return !(left == right);
    }
}
