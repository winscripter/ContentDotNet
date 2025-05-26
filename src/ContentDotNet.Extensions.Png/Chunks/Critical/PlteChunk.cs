namespace ContentDotNet.Extensions.Png.Chunks.Critical;

public sealed class PlteChunk : IChunkData
{
    public List<PngPaletteEntry> Palette { get; set; } = [];

    public PlteChunk(IEnumerable<PngPaletteEntry> palette)
    {
        Palette = [.. palette];
    }

    public void Write(BinaryWriter writer)
    {
        foreach (var entry in Palette)
        {
            writer.Write(entry.Red);
            writer.Write(entry.Green);
            writer.Write(entry.Blue);
        }
    }

    public static int GetPaletteLength(int lengthOfChunk)
    {
        if (lengthOfChunk % 3 != 0)
            throw new ArgumentException("Length of PLTE chunk must be a multiple of 3.", nameof(lengthOfChunk));
        return lengthOfChunk / 3;
    }
}
