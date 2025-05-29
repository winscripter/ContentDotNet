namespace ContentDotNet.Extensions.H264.PredictionMode;

/// <summary>
///   Represents the H.264 List Index.
/// </summary>
public enum ListIndex
{
    /// <summary>
    ///   List 0
    /// </summary>
    L0,

    /// <summary>
    ///   List 1 (for B slices)
    /// </summary>
    L1,

    /// <summary>
    ///   Bidirectional (for B slices)
    /// </summary>
    Bidirectional
}
