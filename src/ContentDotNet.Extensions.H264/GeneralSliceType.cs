namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents a general slice type of an H.264 slice header.
/// </summary>
public enum GeneralSliceType
{
    /// <summary>
    ///   I slice
    /// </summary>
    I,

    /// <summary>
    ///   P slice
    /// </summary>
    P,
    
    /// <summary>
    ///   B slice
    /// </summary>
    B,

    /// <summary>
    ///   SI slice
    /// </summary>
    SI,

    /// <summary>
    ///   SP slice
    /// </summary>
    SP
}
