namespace ContentDotNet.Extensions.H26x;

public static class BitStreamWriterExtensions
{
    public static void WriteME(this BitStreamWriter writer, int motionVectorDiff)
    {
        uint codeNum = (uint)Math.Abs(motionVectorDiff) * 2;
        if (motionVectorDiff < 0)
        {
            codeNum |= 1; 
        }

        writer.WriteUE(codeNum);
    }

    public static async Task WriteMEAsync(this BitStreamWriter writer, int motionVectorDiff)
    {
        uint codeNum = (uint)Math.Abs(motionVectorDiff) * 2;
        if (motionVectorDiff < 0)
        {
            codeNum |= 1;
        }

        await writer.WriteUEAsync(codeNum);
    }

    public static void WriteCE(this BitStreamWriter writer, uint codeNum)
    {
        int leadingZeroBits = 0;
        uint temp = codeNum + 1;
        while (temp > 1)
        {
            leadingZeroBits++;
            temp >>= 1;
        }

        for (int i = 0; i < leadingZeroBits; i++)
        {
            writer.WriteBit(false);
        }

        writer.WriteBits(codeNum & ((1u << leadingZeroBits) - 1), (uint)leadingZeroBits);
    }

    public static async Task WriteCEAsync(this BitStreamWriter writer, uint codeNum)
    {
        int leadingZeroBits = 0;
        uint temp = codeNum + 1;
        while (temp > 1)
        {
            leadingZeroBits++;
            temp >>= 1;
        }

        for (int i = 0; i < leadingZeroBits; i++)
        {
            await writer.WriteBitAsync(false);
        }

        await writer.WriteBitsAsync(codeNum & ((1u << leadingZeroBits) - 1), (uint)leadingZeroBits);
    }

    public static void WriteAE(this BitStreamWriter writer, int absoluteExpGolombValue)
    {
        uint codeNum = (uint)(absoluteExpGolombValue * 2);
        writer.WriteUE(codeNum);
    }

    public static async Task WriteAEAsync(this BitStreamWriter writer, int absoluteExpGolombValue)
    {
        uint codeNum = (uint)(absoluteExpGolombValue * 2);
        await writer.WriteUEAsync(codeNum);
    }

    public static void WriteTE(this BitStreamWriter writer, int value, int x)
    {
        if (x == 1)
        {
            // x == 1 - te(v) is a single bit (0 or 1).
            writer.WriteBit(value == 1);
        }
        else
        {
            // x > 1 - te(v) is a truncated exponential Golomb code.
            if (value < 0 || value >= x)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Value must be in the range [0, {x - 1}].");
            }

            writer.WriteUE((uint)value);
        }
    }

    public static async Task WriteTEAsync(this BitStreamWriter writer, int value, int x)
    {
        if (x == 1)
        {
            // x == 1 - te(v) is a single bit (0 or 1).
            await writer.WriteBitAsync(value == 1);
        }
        else
        {
            // x > 1 - te(v) is a truncated exponential Golomb code.
            if (value < 0 || value >= x)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Value must be in the range [0, {x - 1}].");
            }

            await writer.WriteUEAsync((uint)value);
        }
    }
}
