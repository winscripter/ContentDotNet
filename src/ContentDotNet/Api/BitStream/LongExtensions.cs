namespace ContentDotNet.Api.BitStream
{
    /// <summary>
    ///   Bit-stream long I/O extensions.
    /// </summary>
    public static class LongExtensions
    {
        public static void WriteBits64(
            this BitStreamWriter bsw,
            ulong value,
            uint count)
        {
            for (int i = (int)count - 1; i >= 0; i--)
            {
                bsw.WriteBit((value >> i & 1) == 1);
            }
        }

        public static async Task WriteBits64Async(
            this BitStreamWriter bsw,
            ulong value,
            uint count)
        {
            for (int i = (int)count - 1; i >= 0; i--)
            {
                await bsw.WriteBitAsync((value >> i & 1) == 1);
            }
        }
    }
}
