namespace ContentDotNet.Extensions.H264.Usability;

/// <summary>
///   Represents the Video Format from VUI parameters.
/// </summary>
public enum VideoFormat
{
    /// <summary>
    ///   Component video format.
    /// </summary>
    Component = 0,

    /// <summary>
    ///   PAL (Phase Alternating Line) video format.
    /// </summary>
    Pal = 1,

    /// <summary>
    ///   NTSC (National Television System Committee) video format.
    /// </summary>
    Ntsc = 2,

    /// <summary>
    ///   SECAM (Séquentiel couleur à mémoire) video format.
    /// </summary>
    Secam = 3,

    /// <summary>
    ///   MAC (Multiplexed Analog Components) video format.
    /// </summary>
    Mac = 4,

    /// <summary>
    ///   Unspecified video format.
    /// </summary>
    Unspecified = 5,

    /// <summary>
    ///   Reserved for future use.
    /// </summary>
    Reserved = 6,
}
