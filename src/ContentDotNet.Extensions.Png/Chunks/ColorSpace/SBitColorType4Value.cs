namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class SBitColorType4Value : ISBitChunkData
{
    public byte SignificantGrayscaleBits { get; set; }
    public byte SignificantAlphaBits { get; set; }

    public SBitColorType4Value(byte significantGrayscaleBits, byte significantAlphaBits)
    {
        if (significantGrayscaleBits < 1 || significantGrayscaleBits > 8)
            throw new ArgumentOutOfRangeException(nameof(significantGrayscaleBits), "Significant grayscale bits must be between 1 and 8.");
        if (significantAlphaBits < 1 || significantAlphaBits > 8)
            throw new ArgumentOutOfRangeException(nameof(significantAlphaBits), "Significant alpha bits must be between 1 and 8.");
        SignificantGrayscaleBits = significantGrayscaleBits;
        SignificantAlphaBits = significantAlphaBits;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(SignificantGrayscaleBits);
        writer.Write(SignificantAlphaBits);
    }
}
