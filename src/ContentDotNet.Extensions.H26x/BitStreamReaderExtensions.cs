using ContentDotNet.Abstractions;

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
}
