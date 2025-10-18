namespace ContentDotNet.Extensions.Image.Webp.Chunks
{
    public interface IWebpChunkSerializer
    {
        Type TypeOfChunk { get; }
        WebpChunkBase ReadChunk(BinaryReader reader);
        Task<WebpChunkBase> ReadChunkAsync(BinaryReader reader);
        void WriteChunk(BinaryWriter writer, WebpChunkBase chunk);
        Task WriteChunkAsync(BinaryWriter writer, WebpChunkBase chunk);
    }
}
