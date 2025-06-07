namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("stsc", "Sample-to-Chunk Box", "Defines sample grouping into chunks")]
public sealed class SampleToChunkBox : FullBox, IEquatable<SampleToChunkBox?>
{
    public List<SampleToChunkEntry> Entries { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;
    public override bool RequiresChildBoxes => false;

    public SampleToChunkBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static SampleToChunkBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var stsc = new SampleToChunkBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        uint entryCount = reader.ReadUInt32();
        stsc.Entries = new((int)entryCount);
        for (int i = 0; i < entryCount; i++)
            stsc.Entries[i] = SampleToChunkEntry.Read(reader);
        return stsc;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write((uint)Entries.Count);
        foreach (SampleToChunkEntry entry in Entries)
            entry.Write(writer);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SampleToChunkBox);
    }

    public bool Equals(SampleToChunkBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               EqualityComparer<List<SampleToChunkEntry>>.Default.Equals(Entries, other.Entries) &&
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

    public static bool operator ==(SampleToChunkBox? left, SampleToChunkBox? right)
    {
        return EqualityComparer<SampleToChunkBox>.Default.Equals(left, right);
    }

    public static bool operator !=(SampleToChunkBox? left, SampleToChunkBox? right)
    {
        return !(left == right);
    }
}
