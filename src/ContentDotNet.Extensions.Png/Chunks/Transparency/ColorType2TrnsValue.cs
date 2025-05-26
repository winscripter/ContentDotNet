namespace ContentDotNet.Extensions.Png.Chunks.Transparency;

public sealed class ColorType2TrnsValue : ITrnsChunkValue
{
    public List<(byte alphaForPaletteIndex0, byte alphaForPaletteIndex1)> PaletteAlphaValues { get; }

    public ColorType2TrnsValue(List<(byte alphaForPaletteIndex0, byte alphaForPaletteIndex1)> paletteAlphaValues)
    {
        PaletteAlphaValues = paletteAlphaValues;
    }

    public void Write(BinaryWriter writer)
    {
        foreach (var (alphaForPaletteIndex0, alphaForPaletteIndex1) in PaletteAlphaValues)
        {
            // Write the alpha values for each palette index as 1-byte unsigned bytes
            writer.Write(alphaForPaletteIndex0);
            writer.Write(alphaForPaletteIndex1);
        }
    }
}
