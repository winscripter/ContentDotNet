using ContentDotNet.Abstractions;

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
}
