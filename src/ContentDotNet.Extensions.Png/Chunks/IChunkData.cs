namespace ContentDotNet.Extensions.Png.Chunks;

/// <summary>
///   Represents the chunk data.
/// </summary>
public interface IChunkData
{
    /// <summary>
    ///   Writes the chunk data to the binary writer. This includes just the data of the chunk; no base
    ///   chunk values like CRC or type are written.
    /// </summary>
    /// <param name="writer">Binary writer where the chunk is written to.</param>
    void Write(BinaryWriter writer);
}
