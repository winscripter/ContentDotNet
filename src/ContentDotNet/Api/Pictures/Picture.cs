namespace ContentDotNet.Api.Pictures;

using ContentDotNet.Api;
using ContentDotNet.Api.Colors;
using System.Drawing;

/// <summary>
///   Abstracts a picture
/// </summary>
public abstract class Picture : IDisposable
{
    /// <summary>
    ///   The size of the image.
    /// </summary>
    public abstract Size ImageSize { get; }

    /// <summary>
    ///   The configuration
    /// </summary>
    public Configuration? Configuration { get; set; }

    public abstract void Dispose();
}

/// <summary>
///   Abstracts a picture.
/// </summary>
public abstract class Picture<TPixel> : Picture
    where TPixel : unmanaged, IColor
{
    /// <summary>
    ///   Gets or sets the pixel at the specified coordinates.
    /// </summary>
    /// <param name="x">X Coordinate</param>
    /// <param name="y">Y Coordinate</param>
    /// <returns>Pixel at <paramref name="x"/>/<paramref name="y"/>.</returns>
    public abstract TPixel this[int x, int y] { get; set; }
}
