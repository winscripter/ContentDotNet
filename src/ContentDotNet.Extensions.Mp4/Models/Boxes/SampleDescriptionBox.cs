namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("stsd", "Sample Description Box", "Describes samples")]
public sealed class SampleDescriptionBox : FullBox, IEquatable<SampleDescriptionBox?>
{
    public List<SampleDescriptionEntry> SampleEntries { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public SampleDescriptionBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static SampleDescriptionBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var stsd = new SampleDescriptionBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        uint entryCount = reader.ReadUInt32();
        stsd.SampleEntries = new((int)entryCount);
        for (int i = 0; i < entryCount; i++)
            stsd.SampleEntries[i] = SampleDescriptionEntry.Read(reader);
        return stsd;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write((uint)SampleEntries.Count);
        foreach (SampleDescriptionEntry entry in SampleEntries)
            entry.Write(writer);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SampleDescriptionBox);
    }

    public bool Equals(SampleDescriptionBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               EqualityComparer<List<SampleDescriptionEntry>>.Default.Equals(SampleEntries, other.SampleEntries) &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               EqualityComparer<SampleTableBox?>.Default.Equals(_stbl, other._stbl);
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
        hash.Add(SampleEntries);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        hash.Add(_stbl);
        return hash.ToHashCode();
    }

    public static bool operator ==(SampleDescriptionBox? left, SampleDescriptionBox? right)
    {
        return EqualityComparer<SampleDescriptionBox>.Default.Equals(left, right);
    }

    public static bool operator !=(SampleDescriptionBox? left, SampleDescriptionBox? right)
    {
        return !(left == right);
    }
}
