namespace ContentDotNet.Extensions.H264.PredictionMode;

/// <summary>
///   Inter-prediction macroblock size
/// </summary>
public enum InterMacroblockSize : byte
{
    /// <summary>
    ///   8x4
    /// </summary>
    Size8x4,

    /// <summary>
    ///   4x8
    /// </summary>
    Size4x8,

    /// <summary>
    ///   4x4
    /// </summary>
    Size4x4,

    /// <summary>
    ///   8x8
    /// </summary>
    Size8x8,
}
