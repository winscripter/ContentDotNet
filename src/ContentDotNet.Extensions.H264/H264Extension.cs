namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents the H.264 extension to use.
/// </summary>
public enum H264Extension
{
    /// <summary>
    ///   Extensions are not used.
    /// </summary>
    Baseline = 0,

    /// <summary>
    ///   Use Scalable Video Coding (SVC).
    /// </summary>
    ScalableVideoCoding = 1,

    /// <summary>
    ///   Use Multiview Video Coding (MVC).
    /// </summary>
    MultiviewVideoCoding = 2,

    /// <summary>
    ///   3D AVC, aka Multiview and Depth Video with enhanced non-base Coding.
    /// </summary>
    Avc3D = 3,
}
