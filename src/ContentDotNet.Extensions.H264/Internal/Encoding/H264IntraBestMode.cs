namespace ContentDotNet.Extensions.H264.Internal.Encoding;

internal static class H264IntraBestMode
{
    public static int GetBestModeFor4x4(IntraPredictionSamples samples)
    {
        // Vertical

        bool isVertical = true;

        for (int x = 0; x < 4; x++)
        {
            int thisP = samples.GetP(x, -1);
            for (int y = 0; y < 4; y++)
            {
                if (thisP != samples.GetP(x, y))
                {
                    isVertical = false;
                    break;
                }
            }
        }

        if (isVertical)
            return 0;

        // Horizontal

        bool isHorizontal = true;

        for (int y = 0; y < 4; y++)
        {
            int thisP = samples.GetP(-1, y);
            for (int x = 0; x < 4; x++)
            {
                if (thisP != samples.GetP(x, y))
                {
                    isHorizontal = false;
                    break;
                }
            }
        }

        if (isHorizontal)
            return 1;

        // DC
        bool topAvailable = true, leftAvailable = true;
        int sum = 0;

        for (int x = 0; x < 4; x++)
        {
            int top = samples.GetP(x, -1);
            if (top < 0) topAvailable = false;
            sum += top;
        }

        for (int y = 0; y < 4; y++)
        {
            int left = samples.GetP(-1, y);
            if (left < 0) leftAvailable = false;
            sum += left;
        }

        int dcValue;
        if (topAvailable && leftAvailable)
            dcValue = (sum + 4) / 8;
        else if (topAvailable)
            dcValue = (sum + 2) / 4;
        else if (leftAvailable)
            dcValue = (sum + 2) / 4;
        else
            dcValue = 128;

        bool isDC = true;
        for (int y = 0; y < 4 && isDC; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (samples.GetP(x, y) != dcValue)
                {
                    isDC = false;
                    break;
                }
            }
        }

        if (isDC)
            return 2;

        // Diagonal Down Left

        bool isDiagDownLeft = true;

        for (int y = 0; y < 4 && isDiagDownLeft; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                int pred = samples.GetP(x + y + 1, -1);

                if (samples.GetP(x, y) != pred)
                {
                    isDiagDownLeft = false;
                    break;
                }
            }
        }

        if (isDiagDownLeft)
            return 3;

        // Diagonal Down Right

        bool isDiagDownRight = true;

        for (int y = 0; y < 4 && isDiagDownRight; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                int pred = x - y >= 0 ? samples.GetP(x - y, -1) : samples.GetP(-1, y - x);

                if (samples.GetP(x, y) != pred)
                {
                    isDiagDownRight = false;
                    break;
                }
            }
        }

        if (isDiagDownRight)
            return 4;

        // Vertical Right

        bool isVerticalRight = true;

        for (int y = 0; y < 4 && isVerticalRight; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                int pred = x + 2 * y <= 3 ? samples.GetP(x + 2 * y + 1, -1) : samples.GetP(-1, x + 2 * y - 3);

                if (samples.GetP(x, y) != pred)
                {
                    isVerticalRight = false;
                    break;
                }
            }
        }

        if (isVerticalRight)
            return 5;

        // Horizontal Down

        bool isHorizontalDown = true;

        for (int y = 0; y < 4 && isHorizontalDown; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                int pred = 2 * x + y <= 3 ? samples.GetP(2 * x + y + 1, -1) : samples.GetP(-1, 2 * x + y - 3);

                if (samples.GetP(x, y) != pred)
                {
                    isHorizontalDown = false;
                    break;
                }
            }
        }

        if (isHorizontalDown)
            return 6;

        // Vertical Left

        bool isVerticalLeft = true;

        for (int y = 0; y < 4 && isVerticalLeft; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                int pred;
                if (x + 2 * y - 1 >= 0 && x + 2 * y - 1 <= 3)
                    pred = samples.GetP(x + 2 * y - 1, -1);
                else
                    pred = samples.GetP(-1, x + 2 * y - 7);

                if (samples.GetP(x, y) != pred)
                {
                    isVerticalLeft = false;
                    break;
                }
            }
        }

        if (isVerticalLeft)
            return 7;

        // Horizontal Up

        bool isHorizontalUp = true;

        for (int y = 0; y < 4 && isHorizontalUp; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                int pred;
                if (2 * x + y - 1 >= 0 && 2 * x + y - 1 <= 3)
                    pred = samples.GetP(-1, 2 * x + y - 1);
                else
                    pred = samples.GetP(2 * x + y - 7, -1);

                if (samples.GetP(x, y) != pred)
                {
                    isHorizontalUp = false;
                    break;
                }
            }
        }

        if (isHorizontalUp)
            return 8;

        return -1;
    }

    public static int GetBestModeFor8x8(IntraPredictionSamples samples)
    {
        // Vertical

        bool isVertical = true;

        for (int x = 0; x < 8; x++)
        {
            int thisP = samples.GetP(x, -1);
            for (int y = 0; y < 8; y++)
            {
                if (thisP != samples.GetP(x, y))
                {
                    isVertical = false;
                    break;
                }
            }
        }

        if (isVertical)
            return 0;

        // Horizontal

        bool isHorizontal = true;

        for (int y = 0; y < 8; y++)
        {
            int thisP = samples.GetP(-1, y);
            for (int x = 0; x < 8; x++)
            {
                if (thisP != samples.GetP(x, y))
                {
                    isHorizontal = false;
                    break;
                }
            }
        }

        if (isHorizontal)
            return 1;

        // DC

        bool topAvailable = true, leftAvailable = true;
        int sum = 0;

        for (int x = 0; x < 8; x++)
        {
            int top = samples.GetP(x, -1);
            if (top < 0) topAvailable = false;
            sum += top;
        }

        for (int y = 0; y < 8; y++)
        {
            int left = samples.GetP(-1, y);
            if (left < 0) leftAvailable = false;
            sum += left;
        }

        int dcValue;
        if (topAvailable && leftAvailable)
            dcValue = (sum + 8) / 16;
        else if (topAvailable)
            dcValue = (sum + 4) / 8;
        else if (leftAvailable)
            dcValue = (sum + 4) / 8;
        else
            dcValue = 128;

        bool isDC = true;
        for (int y = 0; y < 8 && isDC; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (samples.GetP(x, y) != dcValue)
                {
                    isDC = false;
                    break;
                }
            }
        }

        if (isDC)
            return 2;

        // Diagonal Down Left

        bool isDiagDownLeft = true;

        for (int y = 0; y < 8 && isDiagDownLeft; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int pred = samples.GetP(x + y + 1, -1);
                if (samples.GetP(x, y) != pred)
                {
                    isDiagDownLeft = false;
                    break;
                }
            }
        }

        if (isDiagDownLeft)
            return 3;

        // Diagonal Down Right

        bool isDiagDownRight = true;

        for (int y = 0; y < 8 && isDiagDownRight; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int pred = x - y >= 0 ? samples.GetP(x - y, -1) : samples.GetP(-1, y - x);
                if (samples.GetP(x, y) != pred)
                {
                    isDiagDownRight = false;
                    break;
                }
            }
        }

        if (isDiagDownRight)
            return 4;

        // Vertical Right

        bool isVerticalRight = true;

        for (int y = 0; y < 8 && isVerticalRight; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int pred = (x + 2 * y <= 7)
                    ? samples.GetP(x + 2 * y + 1, -1)
                    : samples.GetP(-1, x + 2 * y - 7);

                if (samples.GetP(x, y) != pred)
                {
                    isVerticalRight = false;
                    break;
                }
            }
        }

        if (isVerticalRight)
            return 5;

        // Horizontal Down

        bool isHorizontalDown = true;

        for (int y = 0; y < 8 && isHorizontalDown; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int pred = (2 * x + y <= 7)
                    ? samples.GetP(2 * x + y + 1, -1)
                    : samples.GetP(-1, 2 * x + y - 7);

                if (samples.GetP(x, y) != pred)
                {
                    isHorizontalDown = false;
                    break;
                }
            }
        }

        if (isHorizontalDown)
            return 6;

        // Vertical Left

        bool isVerticalLeft = true;

        for (int y = 0; y < 8 && isVerticalLeft; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int pred;
                if (x + 2 * y - 1 >= 0 && x + 2 * y - 1 <= 7)
                    pred = samples.GetP(x + 2 * y - 1, -1);
                else
                    pred = samples.GetP(-1, x + 2 * y - 15);

                if (samples.GetP(x, y) != pred)
                {
                    isVerticalLeft = false;
                    break;
                }
            }
        }

        if (isVerticalLeft)
            return 7;

        // Horizontal Up

        bool isHorizontalUp = true;

        for (int y = 0; y < 8 && isHorizontalUp; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int pred;
                if (2 * x + y - 1 >= 0 && 2 * x + y - 1 <= 7)
                    pred = samples.GetP(-1, 2 * x + y - 1);
                else
                    pred = samples.GetP(2 * x + y - 15, -1);

                if (samples.GetP(x, y) != pred)
                {
                    isHorizontalUp = false;
                    break;
                }
            }
        }

        if (isHorizontalUp)
            return 8;

        return -1;
    }

    public static int GetBestModeFor16x16(IntraPredictionSamples samples)
    {
        // Vertical

        bool isVertical = true;

        for (int x = 0; x < 16; x++)
        {
            int thisP = samples.GetP(x, -1);
            for (int y = 0; y < 16; y++)
            {
                if (thisP != samples.GetP(x, y))
                {
                    isVertical = false;
                    break;
                }
            }
        }

        if (isVertical)
            return 0;

        // Horizontal

        bool isHorizontal = true;

        for (int y = 0; y < 16; y++)
        {
            int thisP = samples.GetP(-1, y);
            for (int x = 0; x < 16; x++)
            {
                if (thisP != samples.GetP(x, y))
                {
                    isHorizontal = false;
                    break;
                }
            }
        }

        if (isHorizontal)
            return 1;

        // DC

        bool topAvailable = true, leftAvailable = true;
        int sum = 0;

        for (int x = 0; x < 16; x++)
        {
            int top = samples.GetP(x, -1);
            if (top < 0) topAvailable = false;
            sum += top;
        }

        for (int y = 0; y < 16; y++)
        {
            int left = samples.GetP(-1, y);
            if (left < 0) leftAvailable = false;
            sum += left;
        }

        int dcValue;
        if (topAvailable && leftAvailable)
            dcValue = (sum + 16) / 32;
        else if (topAvailable)
            dcValue = (sum + 8) / 16;
        else if (leftAvailable)
            dcValue = (sum + 8) / 16;
        else
            dcValue = 128;

        bool isDC = true;
        for (int y = 0; y < 16 && isDC; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                if (samples.GetP(x, y) != dcValue)
                {
                    isDC = false;
                    break;
                }
            }
        }

        if (isDC)
            return 2;

        // Plane

        int H = 0, V = 0;
        for (int i = 1; i <= 8; i++)
        {
            H += i * (samples.GetP(8 + i - 1, -1) - samples.GetP(8 - i - 1, -1));
            V += i * (samples.GetP(-1, 8 + i - 1) - samples.GetP(-1, 8 - i - 1));
        }
        int a = 16 * (samples.GetP(15, -1) + samples.GetP(-1, 15));
        int b = (5 * H + 32) >> 6;
        int c = (5 * V + 32) >> 6;

        bool isPlane = true;
        for (int y = 0; y < 16 && isPlane; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                int pred = (a + b * (x - 7) + c * (y - 7) + 16) >> 5;
                pred = Math.Clamp(pred, 0, 255);
                if (samples.GetP(x, y) != pred)
                {
                    isPlane = false;
                    break;
                }
            }
        }

        if (isPlane)
            return 3;

        return -1;
    }
}
