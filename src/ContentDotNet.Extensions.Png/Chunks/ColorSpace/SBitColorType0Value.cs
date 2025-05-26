namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class SBitColorType0Value : ISBitChunkData
{
    public byte SignificantGrayscaleBits { get; set; }

    public SBitColorType0Value(byte significantGrayscaleBits)
    {
        SignificantGrayscaleBits = significantGrayscaleBits;
    }

    public void Write(BinaryWriter writer)
    {
        if (SignificantGrayscaleBits < 1 || SignificantGrayscaleBits > 8)
            throw new InvalidOperationException("Significant bits must be between 1 and 8.");
        writer.Write(SignificantGrayscaleBits);
    }
}
