namespace ContentDotNet.Extensions.Png.Chunks.Transparency;

public sealed class TrnsChunk : IChunkData
{
    public ITrnsChunkValue Value { get; }

    public TrnsChunk(ITrnsChunkValue value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void Write(BinaryWriter writer)
    {
        this.Value.Write(writer);
    }
}
