using ContentDotNet.Extensions.Mp4.Models.Samples;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
///   Represents an MP4 STSC (Sample-To-Chunk) box.
/// </summary>
public sealed class StscBox : IBoxData, IEquatable<StscBox?>
{
    /// <summary>
    ///   Number of entries.
    /// </summary>
    public uint EntryCount { get; set; }

    /// <summary>
    ///   All entries.
    /// </summary>
    /// <remarks>
    ///   This is a list instead of an array to ease adding/removing sample-to-chunk
    ///   entries when it comes to editing contents directly.
    /// </remarks>
    public List<SampleToChunkEntry> Entries { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="StscBox"/> class.
    /// </summary>
    /// <param name="entryCount">Number of entries.</param>
    /// <param name="entries">All entries.</param>
    public StscBox(uint entryCount, List<SampleToChunkEntry> entries)
    {
        EntryCount = entryCount;
        Entries = entries;
    }

    /// <summary>
    ///   Reads a <see cref="StscBox"/> instance from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="br">The <see cref="BinaryReader"/> to read from.</param>
    /// <returns>A new instance of <see cref="StscBox"/> read from the binary stream.</returns>
    public static StscBox Read(BinaryReader reader)
    {
        uint entryCount = reader.ReadUInt32();
        var entries = new List<SampleToChunkEntry>();
        for (int i = 0; i < entryCount; i++)
            entries.Add(new SampleToChunkEntry(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()));
        return new StscBox(entryCount, entries);
    }

    /// <summary>
    ///   Writes the current <see cref="StscBox"/> instance to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(this.EntryCount);
        foreach (SampleToChunkEntry entry in this.Entries)
        {
            writer.Write(entry.FirstChunk);
            writer.Write(entry.SamplesPerChunk);
            writer.Write(entry.SampleDescriptionIndex);
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as StscBox);
    }

    /// <summary>
    /// Determines whether the specified <see cref="StscBox"/> is equal to the current <see cref="StscBox"/>.
    /// </summary>
    /// <param name="other">The <see cref="StscBox"/> to compare with the current <see cref="StscBox"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="StscBox"/> is equal to the current <see cref="StscBox"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(StscBox? other)
    {
        return other is not null &&
               EntryCount == other.EntryCount &&
               EqualityComparer<List<SampleToChunkEntry>>.Default.Equals(Entries, other.Entries);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(EntryCount, Entries);
    }

    /// <summary>
    ///   Determines whether two <see cref="StscBox"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="StscBox"/> to compare.</param>
    /// <param name="right">The second <see cref="StscBox"/> to compare.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="StscBox"/> instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(StscBox? left, StscBox? right)
    {
        return EqualityComparer<StscBox>.Default.Equals(left, right);
    }

    /// <summary>
    ///   Determines whether two <see cref="StscBox"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="StscBox"/> to compare.</param>
    /// <param name="right">The second <see cref="StscBox"/> to compare.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="StscBox"/> instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(StscBox? left, StscBox? right)
    {
        return !(left == right);
    }
}
