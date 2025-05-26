using System.Buffers.Binary;

namespace ContentDotNet.Extensions.Png.Chunks.Transparency;

public sealed class ColorType0TrnsValue : ITrnsChunkValue
{
    public ushort GraySampleValue { get; }

    public ColorType0TrnsValue(ushort graySampleValue)
    {
        GraySampleValue = graySampleValue;
    }

    public void Write(BinaryWriter writer)
    {
        // Write the gray sample value as a 2-byte unsigned short
        writer.Write(BinaryPrimitives.ReverseEndianness(GraySampleValue));
    }
}
