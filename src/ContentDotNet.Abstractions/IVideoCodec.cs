namespace ContentDotNet.Abstractions;

/// <summary>
///   Represents a video codec.
/// </summary>
public interface IVideoCodec : IDisposable
{
    /// <summary>
    ///   The video stream.
    /// </summary>
    Stream Stream { get; }

    /// <summary>
    ///   Video context. Always returns original ones. Changing values in this
    ///   video context will immediately reflect playback.
    /// </summary>
    VideoContext Context { get; set; }
}
