using System.Buffers.Binary;

namespace ContentDotNet.Extensions.Png.Chunks.Transparency;

public sealed class ColorType1TrnsValue : ITrnsChunkValue
{
    public ushort RedSampleValue { get; set; }
    public ushort GreenSampleValue { get; set; }
    public ushort BlueSampleValue { get; set; }

    public ColorType1TrnsValue(ushort redSampleValue, ushort greenSampleValue, ushort blueSampleValue)
    {
        RedSampleValue = redSampleValue;
        GreenSampleValue = greenSampleValue;
        BlueSampleValue = blueSampleValue;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(BinaryPrimitives.ReverseEndianness(RedSampleValue));
        writer.Write(BinaryPrimitives.ReverseEndianness(GreenSampleValue));
        writer.Write(BinaryPrimitives.ReverseEndianness(BlueSampleValue));
    }
}
