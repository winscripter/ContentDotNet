namespace ContentDotNet.Api.Colors;

/// <summary>
///   Defines the color format.
/// </summary>
public enum ColorFormat
{
    /// <summary>
    ///   RGB
    /// </summary>
    Rgb,

    /// <summary>
    ///   RGB with Alpha
    /// </summary>
    Rgba,

    /// <summary>
    ///   Y'Cb'Cr (aka YUV)
    /// </summary>
    YCbCr,

    /// <summary>
    ///   Luma only
    /// </summary>
    L
}
