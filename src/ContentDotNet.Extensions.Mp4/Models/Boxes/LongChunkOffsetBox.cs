namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("co64", "64-bit Chunk Offset Box", "Provides 64-bit offsets for large files")]
public sealed class LongChunkOffsetBox : FullBox, IEquatable<LongChunkOffsetBox?>
{
    public List<ulong> ChunkOffsets { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public LongChunkOffsetBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static LongChunkOffsetBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var co64 = new LongChunkOffsetBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        uint entryCount = reader.ReadUInt32();
        co64.ChunkOffsets = new((int)entryCount);
        for (int i = 0; i < entryCount; i++)
            co64.ChunkOffsets[i] = reader.ReadUInt64();
        return co64;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write((uint)ChunkOffsets.Count);
        foreach (ulong offset in ChunkOffsets)
            writer.Write(offset);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LongChunkOffsetBox);
    }

    public bool Equals(LongChunkOffsetBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               EqualityComparer<List<ulong>>.Default.Equals(ChunkOffsets, other.ChunkOffsets) &&
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
        hash.Add(ChunkOffsets);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        hash.Add(_stbl);
        return hash.ToHashCode();
    }

    public static bool operator ==(LongChunkOffsetBox? left, LongChunkOffsetBox? right)
    {
        return EqualityComparer<LongChunkOffsetBox>.Default.Equals(left, right);
    }

    public static bool operator !=(LongChunkOffsetBox? left, LongChunkOffsetBox? right)
    {
        return !(left == right);
    }
}
