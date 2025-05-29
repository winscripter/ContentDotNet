namespace ContentDotNet.Extensions.H264.PredictionMode;

/// <summary>
///   H.264 intra prediction mode builder.
/// </summary>
public enum H264IntraPredictionMode : byte
{
    /// <summary>
    ///   Not an intra prediction mode.
    /// </summary>
    None = 0,

    /// <summary>
    ///   Intra 16x16
    /// </summary>
    Intra16x16,

    /// <summary>
    ///   16x16 macroblock with 8x8 blocks
    /// </summary>
    Intra8x8,

    /// <summary>
    ///   16x16 macroblock with 4x4 blocks
    /// </summary>
    Intra4x4,
}
