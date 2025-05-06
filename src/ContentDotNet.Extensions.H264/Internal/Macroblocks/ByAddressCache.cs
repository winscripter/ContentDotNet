using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

/// <summary>
///   Caches information about last macroblocks by their addresses.
/// </summary>
internal sealed class ByAddressCache
{
    public struct Entry
    {
        public bool IsField;
        public bool IsFrame;
        public bool IsCodedWithIntra4x4, IsCodedWithIntra8x8, IsCodedWithIntra16x16;
        public bool IsCodedWithIntra, IsCodedWithInter;
        public int MacroblockType;
        public int SliceType;
        public int TotalCoefficient;
    }

    /// <summary>
    ///   Initially it's 16.
    /// </summary>
    public int CacheSize { get; }

    /// <summary>
    ///   The cache.
    /// </summary>
    public Queue<UnmanagedKeyValuePair<int, Entry>> Cache { get; }

    public ByAddressCache(int cacheSize)
    {
        CacheSize = cacheSize;
        Cache = [];
    }

    public ByAddressCache()
        : this(16)
    {
    }

    public ByAddressCache(SequenceParameterSet sps)
        : this(((int)sps.PicWidthInMbsMinus1 + 1) * 2)
    {
    }

    public void Register(int mbAddr, Entry entry)
    {
        CleanUp();
        Cache.Enqueue(new UnmanagedKeyValuePair<int, Entry>(mbAddr, entry));
    }

    private void CleanUp()
    {
        if (this.Cache.Count >= this.CacheSize)
        {
            while (this.Cache.Count > (this.CacheSize - 1))
            {
                _ = this.Cache.Dequeue();
            }
        }
    }
}
