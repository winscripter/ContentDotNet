namespace ContentDotNet.Extensions.H264.PredictionMode;

/// <summary>
///   A dictionary-like class that caches Intra 4x4 and 8x8 prediction modes, to avoid re-fetching
///   neighboring macroblocks to define prediction modes.
/// </summary>
public sealed class IntraPredictionModeCache
{
    private readonly Dictionary<int, int> _cache4x4;
    private readonly Dictionary<int, int> _cache8x8;

    /// <summary>
    ///   Initializes a new instance of the <see cref="IntraPredictionModeCache"/> class without any
    ///   items in the cache.
    /// </summary>
    public IntraPredictionModeCache()
    {
        _cache4x4 = [];
        _cache8x8 = [];
    }
    
    /// <summary>
    ///   Adds the prediction mode by the macroblock address.
    /// </summary>
    /// <param name="mbAddress">Address of the macroblock.</param>
    /// <param name="predMode">Its corresponding prediction mode</param>
    public void Add4x4(int mbAddress, int predMode)
    {
        if (!_cache4x4.TryAdd(mbAddress, predMode))
        {
            _cache4x4[mbAddress] = predMode;
        }
    }

    /// <summary>
    ///   Adds the prediction mode by the macroblock address.
    /// </summary>
    /// <param name="mbAddress">Address of the macroblock.</param>
    /// <param name="predMode">Its corresponding prediction mode</param>
    public void Add8x8(int mbAddress, int predMode)
    {
        if (!_cache8x8.TryAdd(mbAddress, predMode))
        {
            _cache8x8[mbAddress] = predMode;
        }
    }

    /// <summary>
    ///   Determines whether the 4x4 prediction mode cache contains an entry for the specified macroblock address.
    /// </summary>
    /// <param name="mbAddress">The address of the macroblock.</param>
    /// <returns>
    ///   <c>true</c> if the cache contains an entry for the specified macroblock address; otherwise, <c>false</c>.
    /// </returns>
    public bool Contains4x4(int mbAddress)
    {
        return _cache4x4.ContainsKey(mbAddress);
    }

    /// <summary>
    ///   Determines whether the 8x8 prediction mode cache contains an entry for the specified macroblock address.
    /// </summary>
    /// <param name="mbAddress">The address of the macroblock.</param>
    /// <returns>
    ///   <c>true</c> if the cache contains an entry for the specified macroblock address; otherwise, <c>false</c>.
    /// </returns>
    public bool Contains8x8(int mbAddress)
    {
        return _cache8x8.ContainsKey(mbAddress);
    }

    /// <summary>
    ///   Gets the 4x4 prediction mode for the specified macroblock address.
    /// </summary>
    /// <param name="mbAddress">The address of the macroblock.</param>
    /// <returns>
    ///   The prediction mode if found; otherwise, <c>-1</c>.
    /// </returns>
    public int Get4x4(int mbAddress)
    {
        return _cache4x4.TryGetValue(mbAddress, out var predMode) ? predMode : -1;
    }

    /// <summary>
    ///   Gets the 8x8 prediction mode for the specified macroblock address.
    /// </summary>
    /// <param name="mbAddress">The address of the macroblock.</param>
    /// <returns>
    ///   The prediction mode if found; otherwise, <c>-1</c>.
    /// </returns>
    public int Get8x8(int mbAddress)
    {
        return _cache8x8.TryGetValue(mbAddress, out var predMode) ? predMode : -1;
    }
}
