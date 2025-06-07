namespace ContentDotNet.Compression.Checksum;

/// <summary>
///   Computes CRC32 checksums.
/// </summary>
public static class Crc32
{
    private static readonly uint[] Table = new uint[256];

    static Crc32()
    {
        for (uint i = 0; i < 256; i++)
        {
            uint crc = i;
            for (int j = 0; j < 8; j++)
                crc = (crc >> 1) ^ (crc & 1) * 0xEDB88320;
            Table[i] = crc;
        }
    }

    /// <summary>
    ///   Computes a CRC32 checksum.
    /// </summary>
    /// <param name="stream">The input source</param>
    /// <returns>Computed CRC32 checksum</returns>
    public static uint ComputeChecksum(Stream stream)
    {
        uint crc = 0xFFFFFFFF;
        int lastByte;
        while ((lastByte = stream.ReadByte()) != -1)
            crc = (crc >> 8) ^ Table[(crc ^ (byte)lastByte) & 0xFF];
        return ~crc;
    }

    /// <summary>
    ///   Computes a CRC32 checksum.
    /// </summary>
    /// <param name="span">The input source</param>
    /// <returns>Computed CRC32 checksum</returns>
    public static uint ComputeChecksum(ReadOnlySpan<byte> span)
    {
        uint crc = 0xFFFFFFFF;
        foreach (byte @byte in span)
            crc = (crc >> 8) ^ Table[(crc ^ @byte) & 0xFF];
        return ~crc;
    }
}
