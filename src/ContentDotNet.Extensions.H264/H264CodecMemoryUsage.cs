namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Memory specific codec settings.
/// </summary>
[Flags]
public enum H264CodecMemoryUsage
{
    /// <summary>
    ///   Everything is default.
    /// </summary>
    None = 0,

    /// <summary>
    ///   Rather than continuously re-encoding reference picture lists,
    ///   cache the last few of them. This will result in significant
    ///   heap allocations, but will be faster.
    /// </summary>
    CacheRefPicLists = 1,
}
