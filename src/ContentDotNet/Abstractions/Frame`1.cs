using ContentDotNet.Colors;

namespace ContentDotNet.Abstractions;

/// <summary>
///   Defines a frame.
/// </summary>
public abstract class Frame<TPixelFormat> where TPixelFormat : unmanaged, IColor
{
    /// <summary>
    ///   The raster image, with pixels located left-right top-bottom.
    /// </summary>
    public abstract TPixelFormat[] RasterImage { get; set; }

    public abstract int Width { get; set; }
    public abstract int Height { get; set; }

    public TPixelFormat this[int x, int y]
    {
        get => RasterImage[(y * Width) + x];
        set => RasterImage[(y * Width) + x] = value;
    }

    public abstract ulong Id { get; set; }
}
