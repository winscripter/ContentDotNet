
namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class SBitColorType6Value : ISBitChunkData
{
    public byte SignificantRedBits { get; set; }
    public byte SignificantGreenBits { get; set; }
    public byte SignificantBlueBits { get; set; }
    public byte SignificantAlphaBits { get; set; }

    public SBitColorType6Value(byte significantRedBits, byte significantGreenBits, byte significantBlueBits, byte significantAlphaBits)
    {
        if (significantRedBits < 1 || significantRedBits > 8)
            throw new ArgumentOutOfRangeException(nameof(significantRedBits), "Significant red bits must be between 1 and 8.");
        if (significantGreenBits < 1 || significantGreenBits > 8)
            throw new ArgumentOutOfRangeException(nameof(significantGreenBits), "Significant green bits must be between 1 and 8.");
        if (significantBlueBits < 1 || significantBlueBits > 8)
            throw new ArgumentOutOfRangeException(nameof(significantBlueBits), "Significant blue bits must be between 1 and 8.");
        if (significantAlphaBits < 1 || significantAlphaBits > 8)
            throw new ArgumentOutOfRangeException(nameof(significantAlphaBits), "Significant alpha bits must be between 1 and 8.");
        SignificantRedBits = significantRedBits;
        SignificantGreenBits = significantGreenBits;
        SignificantBlueBits = significantBlueBits;
        SignificantAlphaBits = significantAlphaBits;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(SignificantRedBits);
        writer.Write(SignificantGreenBits);
        writer.Write(SignificantBlueBits);
        writer.Write(SignificantAlphaBits);
    }
}
