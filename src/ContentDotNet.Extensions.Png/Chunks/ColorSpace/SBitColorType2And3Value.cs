namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class SBitColorType2And3Value : ISBitChunkData
{
    public byte SignificantRedBits { get; set; }
    public byte SignificantGreenBits { get; set; }
    public byte SignificantBlueBits { get; set; }

    public SBitColorType2And3Value(byte significantRedBits, byte significantGreenBits, byte significantBlueBits)
    {
        SignificantRedBits = significantRedBits;
        SignificantGreenBits = significantGreenBits;
        SignificantBlueBits = significantBlueBits;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(SignificantRedBits);
        writer.Write(SignificantGreenBits);
        writer.Write(SignificantBlueBits);
    }
}
