namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("stsz", "Sample Size Box", "Defines the size of each media sample")]
public sealed class SampleSizeBox : FullBox, IEquatable<SampleSizeBox?>
{
    public uint SampleSize { get; set; }
    public uint SampleCount { get; set; }
    public List<uint> SampleSizes { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;
    public override bool RequiresChildBoxes => false;

    public SampleSizeBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static SampleSizeBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var stsz = new SampleSizeBox(size, type)
        {
            Version = version,
            Flags = flags,
            SampleSize = reader.ReadUInt32(),
            SampleCount = reader.ReadUInt32()
        };

        stsz.SampleSizes = new((int)stsz.SampleCount);
        if (stsz.SampleSize == 0) // Variable sample sizes
        {
            for (int i = 0; i < stsz.SampleCount; i++)
                stsz.SampleSizes[i] = reader.ReadUInt32();
        }

        return stsz;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write(SampleSize);
        writer.Write(SampleCount);
        if (SampleSize == 0) // Write variable sample sizes
        {
            foreach (uint size in SampleSizes)
                writer.Write(size);
        }
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SampleSizeBox);
    }

    public bool Equals(SampleSizeBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               SampleSize == other.SampleSize &&
               SampleCount == other.SampleCount &&
               EqualityComparer<List<uint>>.Default.Equals(SampleSizes, other.SampleSizes) &&
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
        hash.Add(Version);
        hash.Add(Flags);
        hash.Add(SampleSize);
        hash.Add(SampleCount);
        hash.Add(SampleSizes);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        return hash.ToHashCode();
    }

    public static bool operator ==(SampleSizeBox? left, SampleSizeBox? right)
    {
        return EqualityComparer<SampleSizeBox>.Default.Equals(left, right);
    }

    public static bool operator !=(SampleSizeBox? left, SampleSizeBox? right)
    {
        return !(left == right);
    }
}
