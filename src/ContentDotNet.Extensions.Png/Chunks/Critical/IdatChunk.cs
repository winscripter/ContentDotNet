namespace ContentDotNet.Extensions.Png.Chunks.Critical;

public sealed class IdatChunk : IChunkData
{
    public byte[] CompressedData { get; }

    public IdatChunk(byte[] compressedData)
    {
        CompressedData = compressedData ?? throw new ArgumentNullException(nameof(compressedData));
    }

    public void Write(BinaryWriter writer)
    {
        if (CompressedData.Length == 0)
            throw new InvalidOperationException("IDAT chunk cannot be empty.");
        writer.Write(CompressedData);
    }
}
