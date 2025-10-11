namespace ContentDotNet.Colors;

/// <summary>
///   Represents the Chroma channel.
/// </summary>
public enum ChromaChannel
{
    /// <summary>
    ///   Luminance
    /// </summary>
    Y,

    /// <summary>
    ///   Chrominance
    /// </summary>
    U,

    /// <summary>
    ///   Chrominance
    /// </summary>
    V,

    /// <summary>
    ///   Luminance (alias for <see cref="Y"/>)
    /// </summary>
    L = Y,

    /// <summary>
    ///   Chrominance (alias for <see cref="U"/>)
    /// </summary>
    Cb = U,

    /// <summary>
    ///   Chrominance (alias for <see cref="V"/>)
    /// </summary>
    Cr = V
}
