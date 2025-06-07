namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[NotActuallyBox]
public class SampleDescriptionEntry : Box, IEquatable<SampleDescriptionEntry?>
{
    public ushort DataReferenceIndex { get; set; }
    public byte[] PrivateCodecSpecificData { get; set; } = []; // Varies

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public SampleDescriptionEntry(uint size, uint type)
        : base(size, type)
    {
    }

    public static SampleDescriptionEntry Read(BinaryReader reader)
    {
        var (size, type) = ParseSizeType(reader);
        var entry = new SampleDescriptionEntry(size, type);
        _ = reader.ReadUInt32();
        _ = reader.ReadUInt16();
        entry.DataReferenceIndex = reader.ReadUInt16();
        entry.PrivateCodecSpecificData = reader.ReadBytes((int)size - (4 + 4 + 6 + 2));
        return entry;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteSizeAndType(writer);
        writer.Write((uint)0);
        writer.Write((ushort)0);
        writer.Write(DataReferenceIndex);
        writer.Write(PrivateCodecSpecificData);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SampleDescriptionEntry);
    }

    public bool Equals(SampleDescriptionEntry? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               DataReferenceIndex == other.DataReferenceIndex &&
               EqualityComparer<byte[]>.Default.Equals(PrivateCodecSpecificData, other.PrivateCodecSpecificData) &&
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
        hash.Add(DataReferenceIndex);
        hash.Add(PrivateCodecSpecificData);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        return hash.ToHashCode();
    }

    public static bool operator ==(SampleDescriptionEntry? left, SampleDescriptionEntry? right)
    {
        return EqualityComparer<SampleDescriptionEntry>.Default.Equals(left, right);
    }

    public static bool operator !=(SampleDescriptionEntry? left, SampleDescriptionEntry? right)
    {
        return !(left == right);
    }
}
