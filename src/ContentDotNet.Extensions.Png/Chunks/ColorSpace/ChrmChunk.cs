namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class ChrmChunk : IChunkData
{
    public (uint x, uint y) White { get; }
    public (uint x, uint y) Red { get; }
    public (uint x, uint y) Green { get; }
    public (uint x, uint y) Blue { get; }

    public ChrmChunk((uint x, uint y) white, (uint x, uint y) red, (uint x, uint y) green, (uint x, uint y) blue)
    {
        White = white;
        Red = red;
        Green = green;
        Blue = blue;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(White.x);
        writer.Write(White.y);
        writer.Write(Red.x);
        writer.Write(Red.y);
        writer.Write(Green.x);
        writer.Write(Green.y);
        writer.Write(Blue.x);
        writer.Write(Blue.y);
    }
}
