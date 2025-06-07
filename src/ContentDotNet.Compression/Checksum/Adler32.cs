namespace ContentDotNet.Compression.Checksum;

/// <summary>
///   Computes Adler32 checksums.
/// </summary>
public static class Adler32
{
    private const uint MOD_ADLER = 65521;

    /// <summary>
    ///   Computes an Adler32 checksum.
    /// </summary>
    /// <param name="stream">The input source</param>
    /// <returns>Computed Adler32 checksum</returns>
    public static uint ComputeChecksum(Stream stream)
    {
        uint a = 1;
        uint b = 0;
        int lastByte;
        while ((lastByte = stream.ReadByte()) != -1)
        {
            a = (a + (byte)lastByte) % MOD_ADLER;
            b = (b + a) % MOD_ADLER;
        }
        return (b << 16) | a;
    }

    /// <summary>
    ///   Computes an Adler32 checksum.
    /// </summary>
    /// <param name="span">The input source</param>
    /// <returns>Computed Adler32 checksum</returns>
    public static uint ComputeChecksum(ReadOnlySpan<byte> span)
    {
        uint a = 1;
        uint b = 0;
        foreach (byte @byte in span)
        {
            a = (a + @byte) % MOD_ADLER;
            b = (b + a) % MOD_ADLER; ;
        }
        return (b << 16) | a;
    }
}
