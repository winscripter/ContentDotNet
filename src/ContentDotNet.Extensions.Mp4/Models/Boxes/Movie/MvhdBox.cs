namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
///   Represents an MP4 MVHD (Movie Header) box.
/// </summary>
public struct MvhdBox
{
    /// <summary>
    ///   A single byte that represents the Movie Header Version. If the value is 0, certain fields (like
    ///   Creation Time, Modification Time, Duration) are 4 bytes. Otherwise, if the value
    ///   is 1, they are 8 bytes.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    ///   Represents 3 bytes worth of flags for the entire movie.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    ///   The time when the movie was created. If <see cref="Version"/> is 0,
    ///   this is 4 bytes. If <see cref="Version"/> is 1, this is 8 bytes.
    /// </summary>
    public ulong CreationTime { get; set; }

    /// <summary>
    ///   The time when the movie was modified. If <see cref="Version"/> is 0,
    ///   this is 4 bytes. If <see cref="Version"/> is 1, this is 8 bytes.
    /// </summary>
    public ulong ModificationTime { get; set; }

    /// <summary>
    ///   Defines the time units per second. Always 4 bytes.
    /// </summary>
    public uint Timescale { get; set; }

    /// <summary>
    ///   The duration of the movie. If <see cref="Version"/> is 0,
    ///   this is 4 bytes. If <see cref="Version"/> is 1, this is 8 bytes.
    /// </summary>
    public ulong Duration { get; set; }

    /// <summary>
    ///   Represents the default playback speed (default value is 1.0)
    /// </summary>
    public FixedPointHalf PreferredRate { get; set; }

    /// <summary>
    ///   Represents the default preferred volume (default value is 1.0)
    /// </summary>
    public FixedPointHalf PreferredVolume { get; set; }
    public ulong Reserved1Part1 { get; set; }
    public ushort Reserved1Part2 { get; set; }
    public Matrix36 Matrix { get; set; }
    public ulong PredefinedPart1 { get; set; }
    public ulong PredefinedPart2 { get; set; }
    public ulong PredefinedPart3 { get; set; }
    public uint NextTrackId { get; set; }

}
