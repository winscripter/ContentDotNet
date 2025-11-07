namespace ContentDotNet.Api.Pictures;

using ContentDotNet.Api.Colors;

/// <summary>
///   An image that could be animated or still.
/// </summary>
/// <typeparam name="TPixel"></typeparam>
public abstract class AnimatedImage<TPixel>
    where TPixel : unmanaged, IColor
{
    /// <summary>
    ///   Does the entire animated image consist of just one frame?
    /// </summary>
    public abstract bool IsStill { get; }

    /// <summary>
    ///   Returns the frame at the specified index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>A frame at <paramref name="index"/>.</returns>
    public abstract Picture<TPixel> this[int index] { get; set; }

    /// <summary>
    ///   Adds an image, if possible.
    /// </summary>
    /// <param name="image"></param>
    public abstract void AppendImage(Picture<TPixel> image);

    /// <summary>
    ///   Is the image read-only?
    /// </summary>
    public abstract bool IsReadOnly { get; }

    /// <summary>
    ///   Number of frames.
    /// </summary>
    public abstract int FrameCount { get; }
}
