namespace ContentDotNet.Compression.Checksum;

/// <summary>
///   Computes XXHash128 checksums.
/// </summary>
public static class XxHash128
{
    private const ulong Prime1 = 0x9E3779B185EBCA87;
    private const ulong Prime2 = 0xC2B2AE3D27D4EB4F;
    private const ulong Prime3 = 0x165667B19E3779F9;
    private const ulong Prime4 = 0x85EBCA77C2B2AE63;
    private const ulong Prime5 = 0x27D4EB2F165667C5;

    /// <summary>
    ///   Computes an XXHash128 checksum.
    /// </summary>
    /// <param name="span">The input source</param>
    /// <returns>Computed XXHash128 checksum</returns>
    public static (ulong low, ulong high) ComputeChecksum(ReadOnlySpan<byte> span)
    {
        ulong h1 = unchecked(Prime1 + Prime2);
        ulong h2 = Prime2;
        ulong h3 = 0;
        ulong h4 = unchecked(-(long)Prime1);

        foreach (byte @byte in span)
        {
            h1 = (h1 + @byte * Prime5) * Prime1;
            h2 = (h2 + @byte * Prime4) * Prime2;
            h3 = (h3 + @byte * Prime3) * Prime3;
            h4 = (h4 + @byte * Prime2) * Prime4;
        }

        h1 ^= h1 >> 33;
        h2 ^= h2 >> 29;
        h3 ^= h3 >> 31;
        h4 ^= h4 >> 27;

        return (h1 + h2, h3 + h4);
    }

    /// <summary>
    ///   Computes an XXHash128 checksum.
    /// </summary>
    /// <param name="stream">The input source</param>
    /// <returns>Computed XXHash128 checksum</returns>
    public static (ulong low, ulong high) ComputeChecksum(Stream stream)
    {
        ulong h1 = unchecked(Prime1 + Prime2);
        ulong h2 = Prime2;
        ulong h3 = 0;
        ulong h4 = unchecked(-(long)Prime1);

        int lastByte;
        while ((lastByte = stream.ReadByte()) != 0)
        {
            byte @byte = (byte)lastByte;
            h1 = (h1 + @byte * Prime5) * Prime1;
            h2 = (h2 + @byte * Prime4) * Prime2;
            h3 = (h3 + @byte * Prime3) * Prime3;
            h4 = (h4 + @byte * Prime2) * Prime4;
        }

        h1 ^= h1 >> 33;
        h2 ^= h2 >> 29;
        h3 ^= h3 >> 31;
        h4 ^= h4 >> 27;

        return (h1 + h2, h3 + h4);
    }
}
