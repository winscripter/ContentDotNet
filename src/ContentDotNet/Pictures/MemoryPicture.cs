namespace ContentDotNet.Pictures;

using ContentDotNet.Colors;
using System.Drawing;

internal sealed class MemoryPicture<TPixel> : Picture<TPixel>
    where TPixel : unmanaged, IColor
{
    private readonly TPixel[,] _pixels;

    public MemoryPicture(int width, int height)
    {
        _pixels = new TPixel[width, height];
    }

    public override Size ImageSize => new(_pixels.GetLength(0), _pixels.GetLength(1));

    public override TPixel this[int x, int y]
    {
        get => _pixels[x, y];
        set => _pixels[x, y] = value;
    }

    public override void Dispose()
    {
        // Too bad you cannot dispose managed objects in .NET
        GC.SuppressFinalize(this);
    }
}
