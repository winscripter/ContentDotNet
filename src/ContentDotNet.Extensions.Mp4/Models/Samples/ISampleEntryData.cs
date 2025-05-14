namespace ContentDotNet.Extensions.Mp4.Models.Samples;

/// <summary>
///   Represents the data of an MP4 sample entry.
/// </summary>
public interface ISampleEntryData
{
    /// <summary>
    ///   Writes the sample entry's data (not accounting for other fields provided
    ///   by the general sample entry) to the given binary writer.
    /// </summary>
    /// <param name="writer">Binary writer where the sample entry's data is written to.</param>
    void Write(BinaryWriter writer);
}
