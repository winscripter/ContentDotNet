namespace ContentDotNet.Extensions.H264.Usability;

/// <summary>
/// Specifies the transfer characteristics for H.264 video content.
/// These characteristics define the opto-electronic transfer function (OETF)
/// used to map scene-referred values to display-referred values.
/// </summary>
public enum TransferCharacteristics : byte
{
    /// <summary>
    /// Unknown transfer characteristics.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// ITU-R BT.709 transfer characteristics.
    /// </summary>
    BT709 = 1,

    /// <summary>
    /// Unspecified transfer characteristics.
    /// </summary>
    Unspecified = 2,

    /// <summary>
    /// ITU-R BT.470 System M transfer characteristics.
    /// </summary>
    BT470M = 4,

    /// <summary>
    /// ITU-R BT.470 System B, G transfer characteristics.
    /// </summary>
    BT470BG = 5,

    /// <summary>
    /// ITU-R BT.601 transfer characteristics.
    /// </summary>
    BT601 = 6,

    /// <summary>
    /// SMPTE 240M transfer characteristics.
    /// </summary>
    SMPTE240M = 7,

    /// <summary>
    /// Linear transfer characteristics.
    /// </summary>
    Linear = 8,

    /// <summary>
    /// Logarithmic transfer characteristics with 100:1 range.
    /// </summary>
    Log100 = 9,

    /// <summary>
    /// Logarithmic transfer characteristics with 316.22777:1 range.
    /// </summary>
    Log316 = 10,

    /// <summary>
    /// IEC 61966-2-4 transfer characteristics.
    /// </summary>
    IEC61966_2_4 = 11,

    /// <summary>
    /// ITU-R BT.1361 extended color gamut transfer characteristics.
    /// </summary>
    BT1361 = 12,

    /// <summary>
    /// IEC 61966-2-1 sRGB or sYCC transfer characteristics.
    /// </summary>
    SRGB = 13,

    /// <summary>
    /// ITU-R BT.2020 10-bit transfer characteristics.
    /// </summary>
    BT2020_10 = 14,

    /// <summary>
    /// ITU-R BT.2020 12-bit transfer characteristics.
    /// </summary>
    BT2020_12 = 15,

    /// <summary>
    /// SMPTE ST 2084 (PQ) transfer characteristics.
    /// </summary>
    SMPTE2084 = 16,

    /// <summary>
    /// SMPTE ST 428-1 transfer characteristics.
    /// </summary>
    SMPTE428 = 17,

    /// <summary>
    /// ARIB STD-B67 (HLG) transfer characteristics.
    /// </summary>
    ARIB_STD_B67 = 18,
}
