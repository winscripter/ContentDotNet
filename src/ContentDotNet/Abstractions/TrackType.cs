namespace ContentDotNet.Abstractions;

/// <summary>
///   Defines the type of the track.
/// </summary>
public enum TrackType
{
    /// <summary>
    ///   A video track.
    /// </summary>
    Video,

    /// <summary>
    ///   A frame track.
    /// </summary>
    Frame,

    /// <summary>
    ///   An image track.
    /// </summary>
    Image,

    /// <summary>
    ///   An audio track.
    /// </summary>
    Audio,

    /// <summary>
    ///   A subtitle track.
    /// </summary>
    Subtitle,

    /// <summary>
    ///   Something else.
    /// </summary>
    Other
}
