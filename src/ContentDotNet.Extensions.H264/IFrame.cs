namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents a frame.
/// </summary>
public interface IFrame : IDisposable
{
    /// <summary>
    ///   Frame width
    /// </summary>
    int Width { get; set; }

    /// <summary>
    ///   Frame height
    /// </summary>
    int Height { get; set; }

    /// <summary>
    ///   Gets/sets a pixel at index.
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <returns>Pixel at index</returns>
    Yuv this[int x, int y] { get; set; }
}
