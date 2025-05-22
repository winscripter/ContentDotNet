using ContentDotNet.Extensions.H264.Internal;
using ContentDotNet.Extensions.H264.Internal.Decoding;

namespace ContentDotNet.Extensions.H264.Helpers;

internal static class SamplingUtils
{
    private static int PGet(IntraPredictionSamples p, int x, int y)
    {
        return IntraInterDecoder.Intra.PGet(p, x, y);
    }

    public static bool AnyMarkedAvailable(IntraPredictionSamples availability, int xStart, int yStart, int xEnd, int yEnd)
    {
        bool available = true;

        for (int x = xStart; x < xEnd; x++)
        {
            for (int y = yStart; y < yEnd; y++)
            {
                if (PGet(availability, x, y) != 1)
                {
                    available = false;
                }
            }
        }

        return available;
    }

    public static bool AnyMarkedNotAvailable(IntraPredictionSamples availability, int xStart, int yStart, int xEnd, int yEnd)
    {
        bool available = false;

        for (int x = xStart; x < xEnd; x++)
        {
            for (int y = yStart; y < yEnd; y++)
            {
                if (PGet(availability, x, y) != 1)
                {
                    available = true;
                }
            }
        }

        return available;
    }

    public static bool AllMarkedAvailable(IntraPredictionSamples availability, int xStart, int yStart, int xEnd, int yEnd)
    {
        return AnyMarkedAvailable(availability, xStart, yStart, xEnd, yEnd);
    }

    public static bool AllMarkedNotAvailable(IntraPredictionSamples availability, int xStart, int yStart, int xEnd, int yEnd)
    {
        return AnyMarkedNotAvailable(availability, xStart, yStart, xEnd, yEnd);
    }

    public static bool XAnyMarkedAvailable(IntraPredictionSamples availability, int xStart, int xEnd)
    {
        bool available = true;

        for (int x = xStart; x < xEnd; x++)
        {
            if (PGet(availability, x, -1) != 1)
            {
                available = false;
            }
        }

        return available;
    }

    public static bool XAnyMarkedNotAvailable(IntraPredictionSamples availability, int xStart, int xEnd)
    {
        bool available = false;

        for (int x = xStart; x < xEnd; x++)
        {
            if (PGet(availability, x, -1) != 1)
            {
                available = true;
            }
        }

        return available;
    }

    public static bool XAllMarkedAvailable(IntraPredictionSamples availability, int xStart, int xEnd)
    {
        return XAnyMarkedAvailable(availability, xStart, xEnd);
    }

    public static bool XAllMarkedNotAvailable(IntraPredictionSamples availability, int xStart, int xEnd)
    {
        return XAnyMarkedNotAvailable(availability, xStart, xEnd);
    }

    public static bool YAnyMarkedAvailable(IntraPredictionSamples availability, int yStart, int yEnd)
    {
        bool available = true;

        for (int y = yStart; y < yEnd; y++)
        {
            if (PGet(availability, -1, y) != 1)
            {
                available = false;
            }
        }

        return available;
    }

    public static bool YAnyMarkedNotAvailable(IntraPredictionSamples availability, int yStart, int yEnd)
    {
        bool available = false;

        for (int y = yStart; y < yEnd; y++)
        {
            if (PGet(availability, -1, y) != 1)
            {
                available = true;
            }
        }

        return available;
    }

    public static bool YAllMarkedAvailable(IntraPredictionSamples availability, int yStart, int yEnd)
    {
        return XAnyMarkedAvailable(availability, yStart, yEnd);
    }

    public static bool YAllMarkedNotAvailable(IntraPredictionSamples availability, int yStart, int yEnd)
    {
        return XAnyMarkedNotAvailable(availability, yStart, yEnd);
    }
}
