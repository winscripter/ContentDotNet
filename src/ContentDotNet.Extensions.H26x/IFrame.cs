using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H26x;

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
    ///   The Y plane.
    /// </summary>
    Matrix Y { get; set; }

    /// <summary>
    ///   The U plane.
    /// </summary>
    Matrix U { get; set; }

    /// <summary>
    ///   The V plane.
    /// </summary>
    Matrix V { get; set; }
}
