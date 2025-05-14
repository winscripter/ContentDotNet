namespace ContentDotNet.Extensions.Mp4.Models.Samples;

/// <summary>
///   Represents a Sample To Chunk entry, typically used in an MP4 STSC box.
/// </summary>
public record struct SampleToChunkEntry
{
    /// <summary>
    ///   The first chunk.
    /// </summary>
    public int FirstChunk { get; set; }

    /// <summary>
    ///   Samples per chunk.
    /// </summary>
    public int SamplesPerChunk { get; set; }

    /// <summary>
    ///   The sample description index.
    /// </summary>
    public int SampleDescriptionIndex { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="SampleToChunkEntry"/> structure.
    /// </summary>
    /// <param name="firstChunk">The first chunk.</param>
    /// <param name="samplesPerChunk">Samples per chunk.</param>
    /// <param name="sampleDescriptionIndex">The sample description index.</param>
    public SampleToChunkEntry(int firstChunk, int samplesPerChunk, int sampleDescriptionIndex)
    {
        FirstChunk = firstChunk;
        SamplesPerChunk = samplesPerChunk;
        SampleDescriptionIndex = sampleDescriptionIndex;
    }
}
