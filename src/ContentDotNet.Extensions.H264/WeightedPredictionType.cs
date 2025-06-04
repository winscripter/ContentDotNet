namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents the type of Weighted Prediction.
/// </summary>
public enum WeightedPredictionType
{
    /// <summary>
    ///   Weighted Prediction is disabled.
    /// </summary>
    None = 0,

    /// <summary>
    ///   Implicit Weighted Prediction.
    /// </summary>
    Implicit = 1,

    /// <summary>
    ///   Explicit Weighted Prediction
    /// </summary>
    Explicit = 2,

    /// <summary>
    ///   Determine automatically. Should only be used during encoding.
    /// </summary>
    Auto = 3,
}
