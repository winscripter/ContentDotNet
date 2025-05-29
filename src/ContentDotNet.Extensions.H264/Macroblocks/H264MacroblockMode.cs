namespace ContentDotNet.Extensions.H264.Macroblocks;

/// <summary>
///   Represents the macroblock mode for H.264.
/// </summary>
public enum H264MacroblockMode
{
    /// <summary>
    ///   A PCM macroblock (raw data).
    /// </summary>
    Pcm,

    /// <summary>
    ///   An Intra macroblock (I-frame).
    /// </summary>
    Intra,

    /// <summary>
    ///   An Inter macroblock (P- and B-frame).
    /// </summary>
    Inter,

    /// <summary>
    ///   The macroblock mode is derived automatically.
    /// </summary>
    Auto
}
