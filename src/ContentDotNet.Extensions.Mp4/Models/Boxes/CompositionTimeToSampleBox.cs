namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("ctts", "Composition Time-to-Sample Box", "Defines presentation timestamps for samples")]
public sealed class CompositionTimeToSampleBox : FullBox, IEquatable<CompositionTimeToSampleBox?>
{
    public List<CompositionTimeToSampleEntry> Entries { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public CompositionTimeToSampleBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static CompositionTimeToSampleBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var ctts = new CompositionTimeToSampleBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        uint entriesCount = reader.ReadUInt32();
        ctts.Entries = new((int)entriesCount);
        for (int i = 0; i < entriesCount; i++)
            ctts.Entries[i] = CompositionTimeToSampleEntry.Read(reader);
        return ctts;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write((uint)Entries.Count);
        foreach (CompositionTimeToSampleEntry entry in Entries)
            entry.Write(writer);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CompositionTimeToSampleBox);
    }

    public bool Equals(CompositionTimeToSampleBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               EqualityComparer<List<CompositionTimeToSampleEntry>>.Default.Equals(Entries, other.Entries) &&
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
        hash.Add(Entries);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        hash.Add(_stbl);
        return hash.ToHashCode();
    }

    public static bool operator ==(CompositionTimeToSampleBox? left, CompositionTimeToSampleBox? right)
    {
        return EqualityComparer<CompositionTimeToSampleBox>.Default.Equals(left, right);
    }

    public static bool operator !=(CompositionTimeToSampleBox? left, CompositionTimeToSampleBox? right)
    {
        return !(left == right);
    }
}
