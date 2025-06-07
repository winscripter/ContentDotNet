namespace ContentDotNet.Compression.Checksum;

/// <summary>
///   Computes CRC64 checksums.
/// </summary>
public static class CRC64
{
    private static readonly ulong[] Table = new ulong[256];

    static CRC64()
    {
        for (ulong i = 0; i < 256; i++)
        {
            ulong crc = i;
            for (int j = 0; j < 8; j++)
                crc = (crc >> 1) ^ (crc & 1) * 0xC96C5795D7870F42;
            Table[i] = crc;
        }
    }

    /// <summary>
    ///   Computes a CRC64 checksum.
    /// </summary>
    /// <param name="stream">The input source</param>
    /// <returns>Computed CRC64 checksum</returns>
    public static ulong ComputeChecksum(Stream stream)
    {
        ulong crc = 0xFFFFFFFFFFFFFFFF;
        int lastByte;
        while ((lastByte = stream.ReadByte()) != -1)
            crc = (crc >> 8) ^ Table[(crc ^ (byte)lastByte) & 0xFF];
        return ~crc;
    }

    /// <summary>
    ///   Computes a CRC64 checksum.
    /// </summary>
    /// <param name="span">The input source</param>
    /// <returns>Computed CRC64 checksum</returns>
    public static ulong ComputeChecksum(ReadOnlySpan<byte> span)
    {
        ulong crc = 0xFFFFFFFFFFFFFFFF;
        foreach (byte @byte in span)
            crc = (crc >> 8) ^ Table[(crc ^ @byte) & 0xFF];
        return ~crc;
    }
}
