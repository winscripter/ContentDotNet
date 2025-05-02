namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Bit stream reader extensions
/// </summary>
public static class BitStreamReaderExtensions
{
    public static int ReadME(this BitStreamReader reader)
    {
        uint codeNum = reader.ReadUE();
        int motionVectorDiff = (int)((codeNum + 1) / 2);

        if ((codeNum & 1) == 0)
        {
            motionVectorDiff = -motionVectorDiff;
        }

        return motionVectorDiff;
    }

    public static uint ReadCE(this BitStreamReader reader)
    {
        int leadingZeroBits = -1;
        while (!reader.ReadBit())
        {
            leadingZeroBits++;
        }

        uint codeNum = (1u << leadingZeroBits) - 1 + reader.ReadBits((uint)leadingZeroBits);
        return codeNum;
    }

    public static int ReadAE(this BitStreamReader reader)
    {
        uint codeNum = reader.ReadUE();
        int absoluteValue = (int)(codeNum / 2);

        if ((codeNum & 1) == 0)
        {
            return -absoluteValue;
        }

        return absoluteValue;
    }

    public static int ReadTE(this BitStreamReader reader, int x)
    {
        if (x == 1)
        {
            // x == 1 - te(v) is a single bit (0 or 1).
            return reader.ReadBit() ? 1 : 0;
        }
        else
        {
            // x > 1 - te(v) is a truncated exponential Golomb code.
            uint codeNum = reader.ReadUE();
            if (codeNum >= x)
            {
                throw new InvalidDataException($"Invalid te(v) value: {codeNum} exceeds the range [0, {x - 1}].");
            }
            return (int)codeNum;
        }
    }
}
