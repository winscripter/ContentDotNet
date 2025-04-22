namespace ContentDotNet.Extensions.H264.Internal;

internal static class SamplingUtils
{
    private static int PGet(Span<int> p, int x, int y)
    {
        return p[x == -1 ? y + 1 : x + 17];
    }

    public static bool AnyMarkedAvailable(Span<int> availability, int xStart, int yStart, int xEnd, int yEnd)
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

    public static bool AnyMarkedNotAvailable(Span<int> availability, int xStart, int yStart, int xEnd, int yEnd)
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

    public static bool AllMarkedAvailable(Span<int> availability, int xStart, int yStart, int xEnd, int yEnd)
    {
        return AnyMarkedAvailable(availability, xStart, yStart, xEnd, yEnd);
    }

    public static bool AllMarkedNotAvailable(Span<int> availability, int xStart, int yStart, int xEnd, int yEnd)
    {
        return AnyMarkedNotAvailable(availability, xStart, yStart, xEnd, yEnd);
    }

    public static bool XAnyMarkedAvailable(Span<int> availability, int xStart, int xEnd)
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

    public static bool XAnyMarkedNotAvailable(Span<int> availability, int xStart, int xEnd)
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

    public static bool XAllMarkedAvailable(Span<int> availability, int xStart, int xEnd)
    {
        return XAnyMarkedAvailable(availability, xStart, xEnd);
    }

    public static bool XAllMarkedNotAvailable(Span<int> availability, int xStart, int xEnd)
    {
        return XAnyMarkedNotAvailable(availability, xStart, xEnd);
    }

    public static bool YAnyMarkedAvailable(Span<int> availability, int yStart, int yEnd)
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

    public static bool YAnyMarkedNotAvailable(Span<int> availability, int yStart, int yEnd)
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

    public static bool YAllMarkedAvailable(Span<int> availability, int yStart, int yEnd)
    {
        return XAnyMarkedAvailable(availability, yStart, yEnd);
    }

    public static bool YAllMarkedNotAvailable(Span<int> availability, int yStart, int yEnd)
    {
        return XAnyMarkedNotAvailable(availability, yStart, yEnd);
    }
}
