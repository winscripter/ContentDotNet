using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal.Entropies.CabacInternal;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   An internal structure. Do not use.
/// </summary>
internal record struct PrefixAndSuffix
{
    public int Prefix { get; set; }
    public int Suffix { get; set; }

    public PrefixAndSuffix(int prefix, int suffix)
    {
        Prefix = prefix;
        Suffix = suffix;
    }
}

/// <summary>
///   Represents context for CABAC bitstream parsing.
/// </summary>
public struct CabacContext
{
    private PrefixAndSuffix maxBinIdxCtx = default;
    private PrefixAndSuffix ctxIdxOffset = default;

    /// <summary>
    ///   PStateIdx
    /// </summary>
    public uint PStateIdx;

    /// <summary>
    ///   ValMps
    /// </summary>
    public bool ValMps;

    /// <summary>
    ///   Initializes a new instance of the <see cref="CabacContext"/> structure.
    /// </summary>
    /// <param name="m">M</param>
    /// <param name="n">N</param>
    /// <param name="sliceQpy">Slice QPY</param>
    public CabacContext(int m, int n, int sliceQpy)
    {
        int preCtxState = Util264.Clip3(1, 126, (m * Util264.Clip3(0, 51, sliceQpy) >> 4) + n);
        if (preCtxState <= 63)
        {
            PStateIdx = 63 - (uint)preCtxState;
            ValMps = false;
        }
        else
        {
            PStateIdx = (uint)preCtxState - 64u;
            ValMps = true;
        }
    }
}
