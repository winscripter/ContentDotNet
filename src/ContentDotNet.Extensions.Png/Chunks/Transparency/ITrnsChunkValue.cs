namespace ContentDotNet.Extensions.Png.Chunks.Transparency;

/// <summary>
///   Specific to the TRNS chunk. See <see cref="ColorType0TrnsValue"/>, <see cref="ColorType1TrnsValue"/>
///   and <see cref="ColorType2TrnsValue"/>.
/// </summary>
public interface ITrnsChunkValue
{
    /// <summary>
    ///   Writes the TRNS chunk data to the binary writer. This includes just the data of the chunk; no base
    ///   chunk values like CRC or type are written.
    /// </summary>
    /// <param name="writer">Binary writer where the chunk is written to.</param>
    void Write(BinaryWriter writer);
}
