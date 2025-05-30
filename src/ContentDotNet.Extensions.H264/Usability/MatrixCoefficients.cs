namespace ContentDotNet.Extensions.H264.Usability;

/// <summary>
/// Specifies the matrix coefficients used for color space conversion in video encoding.
/// </summary>
public enum MatrixCoefficients : byte
{
    /// <summary>
    /// GBR (Green, Blue, Red) color space.
    /// </summary>
    GBR,

    /// <summary>
    /// ITU-R BT.709 standard.
    /// </summary>
    BT709 = 1,

    /// <summary>
    /// Unspecified matrix coefficients.
    /// </summary>
    Unspecified = 2,

    /// <summary>
    /// United States FCC 73.682 standard.
    /// </summary>
    UnitedStatesFCC = 3,

    /// <summary>
    /// ITU-R BT.470BG standard.
    /// </summary>
    BT470BG = 5,

    /// <summary>
    /// SMPTE 170M standard.
    /// </summary>
    SMPTE170M = 6,

    /// <summary>
    /// SMPTE 240M standard.
    /// </summary>
    SMPTE240M = 7,

    /// <summary>
    /// YCgCoR color space.
    /// </summary>
    YCgCoR = 8,

    /// <summary>
    /// ITU-R BT.2020 non-constant luminance.
    /// </summary>
    BT2020NonConstantLuminance = 9,

    /// <summary>
    /// ITU-R BT.2020 constant luminance.
    /// </summary>
    BT2020ConstantLuminance = 10,

    /// <summary>
    /// SMPTE 2085 standard.
    /// </summary>
    SMPTE2085 = 11,

    /// <summary>
    /// Chromaticity-derived non-constant luminance.
    /// </summary>
    ChromaticityDerivedNonConstantLuminance = 12,

    /// <summary>
    /// Chromaticity-derived constant luminance.
    /// </summary>
    ChromaticityDerivedConstantLuminance = 13,

    /// <summary>
    /// ICtCp color space.
    /// </summary>
    ICtCp = 14,

    /// <summary>
    /// IPTC2 color space.
    /// </summary>
    IPTC2 = 15,

    /// <summary>
    /// YCgCoRe color space.
    /// </summary>
    YCgCoRe = 16,

    /// <summary>
    /// YCgCoRo color space.
    /// </summary>
    YCgCoRo = 17,
}
