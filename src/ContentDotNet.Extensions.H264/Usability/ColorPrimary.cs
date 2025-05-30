namespace ContentDotNet.Extensions.H264.Usability;

/// <summary>
/// Specifies the color primaries used in video content, as defined by various standards.
/// </summary>
public enum ColorPrimary : byte
{
    /// <summary>
    /// ITU-R BT.709, used for HDTV.
    /// </summary>
    Bt709 = 1,

    /// <summary>
    /// Unspecified color primaries.
    /// </summary>
    Unspecified = 2,

    /// <summary>
    /// ITU-R BT.470 System M, used for NTSC.
    /// </summary>
    Bt470M = 4,

    /// <summary>
    /// ITU-R BT.470 System B, G, used for PAL and SECAM.
    /// </summary>
    Bt470BG = 5,

    /// <summary>
    /// SMPTE 170M, used for NTSC.
    /// </summary>
    Smpte170M = 6,

    /// <summary>
    /// SMPTE 240M, used for early HDTV.
    /// </summary>
    Smpte240M = 7,

    /// <summary>
    /// Generic film color primaries.
    /// </summary>
    Film = 8,

    /// <summary>
    /// ITU-R BT.2020, used for UHDTV.
    /// </summary>
    Bt2020 = 9,

    /// <summary>
    /// SMPTE ST 428-1, used for DCI-P3.
    /// </summary>
    Smpte428 = 10,

    /// <summary>
    /// SMPTE ST 431-2, used for DCI white point.
    /// </summary>
    Smpte431 = 11,

    /// <summary>
    /// SMPTE ST 432-1, used for DCI-P3.
    /// </summary>
    Smpte432 = 12,

    /// <summary>
    /// EBU Tech. 3213-E, used for PAL.
    /// </summary>
    Ebu3213E = 22,
}
