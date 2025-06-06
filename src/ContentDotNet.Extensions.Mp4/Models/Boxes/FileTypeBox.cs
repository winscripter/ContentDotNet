using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("ftyp", "File Type", "Describes the type of the MP4 video file")]
public sealed class FileTypeBox : Box, IEquatable<FileTypeBox?>
{
    public FourCC MajorBrand { get; set; }
    public FourCC MinorVersion { get; set; }
    public List<FourCC> CompatibleBrands { get; set; }

    public FileTypeBox(uint size, uint type, FourCC majorBrand, FourCC minorVersion, List<FourCC> compatibleBrands)
    {
        MajorBrand = majorBrand;
        MinorVersion = minorVersion;
        CompatibleBrands = compatibleBrands;
        Size = size;
        Type = type;
    }

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public override bool IsCompatibleWith(Box other)
    {
        return other.Type == this.Type;
    }

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

    public override void Write(BinaryWriter writer)
    {
        base.WriteSizeAndType(writer);
        writer.Write(MajorBrand);
        writer.Write(MinorVersion);
        writer.Write((uint)CompatibleBrands.Count);
        foreach (FourCC cc in CompatibleBrands)
            writer.Write(cc);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as FileTypeBox);
    }

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

    public static bool operator ==(FileTypeBox? left, FileTypeBox? right)
    {
        return EqualityComparer<FileTypeBox>.Default.Equals(left, right);
    }

    public static bool operator !=(FileTypeBox? left, FileTypeBox? right)
    {
        return !(left == right);
    }
}
