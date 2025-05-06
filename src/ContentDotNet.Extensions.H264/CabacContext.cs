namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents context for CABAC bitstream parsing.
/// </summary>
public struct CabacContext
{
    /// <summary>
    ///   PStateIdx
    /// </summary>
    public uint PStateIdx;

    /// <summary>
    ///   ValMps
    /// </summary>
    public bool ValMps;
}
