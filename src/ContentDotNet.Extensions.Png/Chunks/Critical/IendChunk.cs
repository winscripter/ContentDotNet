namespace ContentDotNet.Extensions.Png.Chunks.Critical;

public sealed class IendChunk : IChunkData
{
    public static readonly IendChunk Instance = new();

    public IendChunk()
    {
    }

    public void Write(BinaryWriter writer)
    {
        // IEND chunk is empty, so nothing to write.
        // The CRC is automatically calculated by the PNG writer.
    }
}
