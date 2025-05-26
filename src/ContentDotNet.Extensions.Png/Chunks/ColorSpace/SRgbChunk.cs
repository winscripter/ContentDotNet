namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class SRgbChunk : IChunkData
{
    public byte RenderingIntent { get; set; }

    public SRgbChunk(byte renderingIntent)
    {
        if (renderingIntent < 0 || renderingIntent > 3)
            throw new ArgumentOutOfRangeException(nameof(renderingIntent), "Rendering intent must be between 0 and 3.");
        RenderingIntent = renderingIntent;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(RenderingIntent);
    }
}
