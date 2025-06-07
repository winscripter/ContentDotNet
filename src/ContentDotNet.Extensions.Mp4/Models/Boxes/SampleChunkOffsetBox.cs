namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("stco", "Sample Chunk Offset", "Defines the offset for each media sample")]
public sealed class SampleChunkOffsetBox : FullBox, IEquatable<SampleChunkOffsetBox?>
{
    public List<uint> ChunkOffsets { get; set; } = [];

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public SampleChunkOffsetBox(uint size, uint type)
        : base(size, type)
    {
    }

    private SampleTableBox? _stbl;

    public SampleTableBox? GetStbl() => _stbl;
    public void UseStbl(SampleTableBox? stbl) => _stbl = stbl;

    public static SampleChunkOffsetBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var stco = new SampleChunkOffsetBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        stco.ChunkOffsets = new((int)reader.ReadUInt32());
        for (int i = 0; i < stco.ChunkOffsets.Count; i++)
            stco.ChunkOffsets[i] = reader.ReadUInt32();
        return stco;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write((uint)ChunkOffsets.Count);
        foreach (uint sample in ChunkOffsets)
            writer.Write(sample);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SampleChunkOffsetBox);
    }

    public bool Equals(SampleChunkOffsetBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               EqualityComparer<List<uint>>.Default.Equals(ChunkOffsets, other.ChunkOffsets) &&
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

    public static bool operator ==(SampleChunkOffsetBox? left, SampleChunkOffsetBox? right)
    {
        return EqualityComparer<SampleChunkOffsetBox>.Default.Equals(left, right);
    }

    public static bool operator !=(SampleChunkOffsetBox? left, SampleChunkOffsetBox? right)
    {
        return !(left == right);
    }
}
