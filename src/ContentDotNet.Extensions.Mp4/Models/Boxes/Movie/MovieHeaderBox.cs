namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
///   The movie header (mvhd) box.
/// </summary>
public struct MovieHeaderBox : IBoxData
{
    /// <summary>
    ///   Movie version.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    ///   Movie flags.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    ///   Creation time (version 0 - 32 bits; version 1 - 64 bits).
    /// </summary>
    public ulong CreationTime { get; set; }
}
