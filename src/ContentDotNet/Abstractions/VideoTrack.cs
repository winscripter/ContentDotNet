using ContentDotNet.DataReferences;
using ContentDotNet.Primitives;
using System.Drawing;

namespace ContentDotNet.Abstractions;

/// <summary>
///   Represents a video track.
/// </summary>
public abstract class VideoTrack
{
    /// <summary>
    ///   Is the video block-based (f.e. like H.264's macroblocks)?
    /// </summary>
    public abstract bool IsBlockBased { get; }

    /// <summary>
    ///   Gets the size of each block.
    /// </summary>
    /// <remarks>
    ///   This property is <see langword="null"/> if the <see cref="IsBlockBased"/> property
    ///   is equal to <see langword="false"/>.
    /// </remarks>
    public abstract Size? BlockSize { get; }

    /// <summary>
    ///   Gets the size of the video in pixels.
    /// </summary>
    public abstract Size Size { get; }

    /// <summary>
    ///   Gets the frame rate of the video (frames per second).
    /// </summary>
    public abstract double FrameRate { get; }

    /// <summary>
    ///   Gets the duration of the video track.
    /// </summary>
    public TimeSpan Duration => TimeSpan.FromMilliseconds(1000) / FrameRate;

    /// <summary>
    ///   Gets the pixel aspect ratio (width:height).
    /// </summary>
    public abstract AspectRatio PixelAspectRatio { get; }

    /// <summary>
    ///   Gets the chroma subsampling format (e.g., 4:2:0, 4:2:2, 4:4:4).
    /// </summary>
    public abstract ChromaSubsampling ChromaSubsampling { get; }

    /// <summary>
    ///   Gets the language of the video track, if specified.
    /// </summary>
    public abstract string? Language { get; }

    /// <summary>
    ///   Gets the rotation of the video in degrees (e.g., 0, 90, 180, 270).
    /// </summary>
    public abstract int Rotation { get; }

    /// <summary>
    ///   Gets the list of metadata entries associated with the video track.
    /// </summary>
    public abstract IReadOnlyDictionary<string, string> Metadata { get; }

    /// <summary>
    ///   Gets the container format (e.g., MP4, AVI, MKV).
    /// </summary>
    public abstract string ContainerFormat { get; }

    /// <summary>
    ///   Gets the list of data references (e.g., external file references).
    /// </summary>
    public abstract List<DataReference> DataReferences { get; }

    /// <summary>
    ///   Gets a pixel at index.
    /// </summary>
    /// <param name="x">X Coordinate</param>
    /// <param name="y">Y Coordinate</param>
    /// <returns>The color at <paramref name="x"/>/<paramref name="y"/>.</returns>
    /// <remarks>
    ///   If this video isn't encoded with RGB/RGBA (but rather, say, <see cref="Yuv"/>), the
    ///   pixel will be converted.
    /// </remarks>
    public abstract Color this[int x, int y] { get; }
}
