namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

public sealed class SampleToChunkEntry : IEquatable<SampleToChunkEntry?>
{
    public uint FirstChunk { get; set; }
    public uint SamplesPerChunk { get; set; }
    public uint SampleDescriptionIndex { get; set; }

    public SampleToChunkEntry(uint firstChunk, uint samplesPerChunk, uint sampleDescriptionIndex)
    {
        FirstChunk = firstChunk;
        SamplesPerChunk = samplesPerChunk;
        SampleDescriptionIndex = sampleDescriptionIndex;
    }

    public static SampleToChunkEntry Read(BinaryReader reader) =>
        new(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32());

    public void Write(BinaryWriter writer)
    {
        writer.Write(FirstChunk);
        writer.Write(SamplesPerChunk);
        writer.Write(SampleDescriptionIndex);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SampleToChunkEntry);
    }

    public bool Equals(SampleToChunkEntry? other)
    {
        return other is not null &&
               FirstChunk == other.FirstChunk &&
               SamplesPerChunk == other.SamplesPerChunk &&
               SampleDescriptionIndex == other.SampleDescriptionIndex;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstChunk, SamplesPerChunk, SampleDescriptionIndex);
    }

    public static bool operator ==(SampleToChunkEntry? left, SampleToChunkEntry? right)
    {
        return EqualityComparer<SampleToChunkEntry>.Default.Equals(left, right);
    }

    public static bool operator !=(SampleToChunkEntry? left, SampleToChunkEntry? right)
    {
        return !(left == right);
    }
}
