namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class SBitChunk : IChunkData
{
    public ISBitChunkData SBitData { get; }

    public SBitChunk(ISBitChunkData sBitData)
    {
        SBitData = sBitData ?? throw new ArgumentNullException(nameof(sBitData), "SBit data cannot be null.");
    }

    public void Write(BinaryWriter writer)
    {
        SBitData.Write(writer);
    }
}
