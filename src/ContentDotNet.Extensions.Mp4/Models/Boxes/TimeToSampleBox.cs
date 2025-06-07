namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("stts", "Time-to-Sample Box", "Maps samples to timestamps")]
public sealed class TimeToSampleBox : FullBox, IEquatable<TimeToSampleBox?>
{
    public List<TimeToSampleEntry> Entries { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;
    public override bool RequiresChildBoxes => false;

    public TimeToSampleBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static TimeToSampleBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var stts = new TimeToSampleBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        uint entryCount = reader.ReadUInt32();
        stts.Entries = new((int)entryCount);
        for (int i = 0; i < entryCount; i++)
            stts.Entries[i] = TimeToSampleEntry.Read(reader);
        return stts;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write((uint)Entries.Count);
        foreach (TimeToSampleEntry entry in Entries)
            entry.Write(writer);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TimeToSampleBox);
    }

    public bool Equals(TimeToSampleBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               EqualityComparer<List<TimeToSampleEntry>>.Default.Equals(Entries, other.Entries) &&
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
        hash.Add(Entries);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        return hash.ToHashCode();
    }

    public static bool operator ==(TimeToSampleBox? left, TimeToSampleBox? right)
    {
        return EqualityComparer<TimeToSampleBox>.Default.Equals(left, right);
    }

    public static bool operator !=(TimeToSampleBox? left, TimeToSampleBox? right)
    {
        return !(left == right);
    }
}
