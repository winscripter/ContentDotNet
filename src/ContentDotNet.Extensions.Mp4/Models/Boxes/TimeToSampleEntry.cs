namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

public sealed class TimeToSampleEntry : IEquatable<TimeToSampleEntry?>
{
    public uint SampleCount { get; set; }
    public uint SampleDelta { get; set; }

    public TimeToSampleEntry(uint sampleCount, uint sampleDelta)
    {
        SampleCount = sampleCount;
        SampleDelta = sampleDelta;
    }

    public static TimeToSampleEntry Read(BinaryReader reader) =>
        new TimeToSampleEntry(reader.ReadUInt32(), reader.ReadUInt32());

    public void Write(BinaryWriter writer)
    {
        writer.Write(SampleCount);
        writer.Write(SampleDelta);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TimeToSampleEntry);
    }

    public bool Equals(TimeToSampleEntry? other)
    {
        return other is not null &&
               SampleCount == other.SampleCount &&
               SampleDelta == other.SampleDelta;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SampleCount, SampleDelta);
    }

    public static bool operator ==(TimeToSampleEntry? left, TimeToSampleEntry? right)
    {
        return EqualityComparer<TimeToSampleEntry>.Default.Equals(left, right);
    }

    public static bool operator !=(TimeToSampleEntry? left, TimeToSampleEntry? right)
    {
        return !(left == right);
    }
}
