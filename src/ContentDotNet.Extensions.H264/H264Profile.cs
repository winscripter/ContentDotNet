namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents an H.264 profile. Profiles define the set of features available for encoding and decoding video streams.
/// </summary>
public enum H264Profile : byte
{
    /// <summary>
    /// Baseline Profile (profile_idc = 66). Commonly used for video conferencing, mobile, and low-complexity applications.
    /// Widely supported and known for its simplicity and compatibility.
    /// </summary>
    Baseline = 66,

    /// <summary>
    /// Main Profile (profile_idc = 77). Adds support for B-frames and better compression efficiency.
    /// Popular in broadcast and consumer video formats such as digital TV.
    /// </summary>
    Main = 77,

    /// <summary>
    /// Extended Profile (profile_idc = 88). Includes enhanced error resilience features.
    /// Rarely used in practice; designed for streaming but largely supplanted by Baseline and Main.
    /// </summary>
    Extended = 88,

    /// <summary>
    /// High Profile (profile_idc = 100). The most commonly used profile for Blu-ray, streaming, and HDTV.
    /// Adds features such as 8x8 intra prediction, quantization scaling matrices, and more.
    /// </summary>
    High = 100,

    /// <summary>
    /// High 10 Profile (profile_idc = 110). Supports 10-bit video depth.
    /// Used in professional video production and archival content.
    /// </summary>
    High10 = 110,

    /// <summary>
    /// High 4:2:2 Profile (profile_idc = 122). Supports 10-bit color depth and 4:2:2 chroma subsampling.
    /// Common in professional video editing and broadcasting workflows.
    /// </summary>
    High422 = 122,

    /// <summary>
    /// High 4:4:4 Predictive Profile (profile_idc = 244). Supports full 4:4:4 chroma sampling and optionally lossless compression.
    /// Targeted at high-fidelity professional applications and archival.
    /// </summary>
    High444Predictive = 244,

    /// <summary>
    /// CAVLC 4:4:4 Intra Profile (profile_idc = 44). A simpler 4:4:4 profile that uses only CAVLC entropy coding.
    /// Intended for simpler encoders/decoders; rarely used.
    /// </summary>
    Cavlc444Intra = 44,

    /// <summary>
    /// Scalable Baseline Profile (profile_idc = 83). Part of Scalable Video Coding (SVC) for layered video streams.
    /// Used for adaptive streaming but rarely seen in consumer formats.
    /// </summary>
    ScalableBaseline = 83,

    /// <summary>
    /// Scalable High Profile (profile_idc = 86). SVC profile supporting higher quality enhancement layers.
    /// Enables efficient encoding for multi-resolution and multi-bit rate delivery.
    /// </summary>
    ScalableHigh = 86,

    /// <summary>
    /// Scalable High Intra Profile (profile_idc = 91). An intra-only scalable profile.
    /// Used in scenarios requiring fast seeking or editing, but very niche.
    /// </summary>
    ScalableHighIntra = 91,

    /// <summary>
    /// Stereo High Profile (profile_idc = 128). Used for stereoscopic (3D) video encoding.
    /// Part of the Multiview Video Coding (MVC) extension. Specialized use only.
    /// </summary>
    StereoHigh = 128,

    /// <summary>
    /// Multiview High Profile (profile_idc = 118). Supports multiple video views (e.g., 3D, multiple camera angles).
    /// Used in 3D Blu-ray and experimental multi-view systems.
    /// </summary>
    MultiviewHigh = 118
}
