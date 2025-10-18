namespace ContentDotNet.Extensions.Image.Webp.Chunks
{
    using ContentDotNet.Primitives;

    public abstract class WebpChunkBase
    {
        public FourCC FourCC { get; set; }
        public int Size { get; set; }
    }
}
