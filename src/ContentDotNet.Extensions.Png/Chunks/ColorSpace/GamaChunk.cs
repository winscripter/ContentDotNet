namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class GamaChunk : IChunkData
{
    public uint Gamma { get; }

    public GamaChunk(uint gamma)
    {
        if (gamma == 0)
            throw new ArgumentOutOfRangeException(nameof(gamma), "Gamma value cannot be zero.");

        Gamma = gamma;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(Gamma);
    }
}
