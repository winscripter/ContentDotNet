namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

public sealed class CompositionTimeToSampleEntry : IEquatable<CompositionTimeToSampleEntry?>
{
    public uint SampleCount { get; set; }
    public uint CompositionOffset { get; set; }

    public CompositionTimeToSampleEntry(uint sampleCount, uint compositionOffset)
    {
        SampleCount = sampleCount;
        CompositionOffset = compositionOffset;
    }

    public static CompositionTimeToSampleEntry Read(BinaryReader reader)
        => new(reader.ReadUInt32(), reader.ReadUInt32());

    public void Write(BinaryWriter writer)
    {
        writer.Write(SampleCount);
        writer.Write(CompositionOffset);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CompositionTimeToSampleEntry);
    }

    public bool Equals(CompositionTimeToSampleEntry? other)
    {
        return other is not null &&
               SampleCount == other.SampleCount &&
               CompositionOffset == other.CompositionOffset;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SampleCount, CompositionOffset);
    }

    public static bool operator ==(CompositionTimeToSampleEntry? left, CompositionTimeToSampleEntry? right)
    {
        return EqualityComparer<CompositionTimeToSampleEntry>.Default.Equals(left, right);
    }

    public static bool operator !=(CompositionTimeToSampleEntry? left, CompositionTimeToSampleEntry? right)
    {
        return !(left == right);
    }
}
