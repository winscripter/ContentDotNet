using System.Buffers.Binary;

namespace ContentDotNet.Extensions.Png.Chunks.Critical;

public sealed class IhdrChunk : IChunkData
{
    public uint Width { get; set; }
    public uint Height { get; set; }
    public byte BitDepth { get; set; }
    public byte ColorType { get; set; }
    public byte CompressionMethod { get; set; }
    public byte FilterMethod { get; set; }
    public byte InterlaceMethod { get; set; }

    public IhdrChunk(uint width, uint height, byte bitDepth, byte colorType, byte compressionMethod, byte filterMethod, byte interlaceMethod)
    {
        Width = width;
        Height = height;
        BitDepth = bitDepth;
        ColorType = colorType;
        CompressionMethod = compressionMethod;
        FilterMethod = filterMethod;
        InterlaceMethod = interlaceMethod;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(BinaryPrimitives.ReverseEndianness(Width));
        writer.Write(BinaryPrimitives.ReverseEndianness(Height));
        writer.Write(BitDepth);
        writer.Write(ColorType);
        writer.Write(CompressionMethod);
        writer.Write(FilterMethod);
        writer.Write(InterlaceMethod);
    }
}
