using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Usability;

internal static class AspectRatioMapping
{
    public static readonly Dictionary<uint, AspectRatio> AspectRatioIdcMapping = new()
    {
        [0] = new AspectRatio(0, 0),
        [1] = new AspectRatio(1, 1),
        [2] = new AspectRatio(12, 11),
        [3] = new AspectRatio(10, 11),
        [4] = new AspectRatio(16, 11),
        [5] = new AspectRatio(40, 33),
        [6] = new AspectRatio(24, 11),
        [7] = new AspectRatio(20, 11),
        [8] = new AspectRatio(32, 11),
        [9] = new AspectRatio(80, 33),
        [10] = new AspectRatio(18, 11),
        [11] = new AspectRatio(15, 11),
        [12] = new AspectRatio(64, 33),
        [13] = new AspectRatio(160, 99),
        [14] = new AspectRatio(4, 3),
        [15] = new AspectRatio(3, 2),
        [16] = new AspectRatio(2, 1)
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExtendedSAR(uint aspectRatioIdc) => aspectRatioIdc == 255;

    public static AspectRatio GetAspectRatio(VuiParameters vuip)
    {
        if (IsExtendedSAR(vuip.AspectRatioIdc))
        {
            return new AspectRatio((int)vuip.SarWidth, (int)vuip.SarHeight);
        }

        return AspectRatioIdcMapping.TryGetValue(vuip.AspectRatioIdc, out AspectRatio aspectRatio)
            ? aspectRatio
            : new AspectRatio(0, 0); // Default to 0,0 if not found
    }

    public static (uint aspectRatioIdc, int sarWidth, int sarHeight) GetAspectRatioFields(AspectRatio aspectRatio)
    {
        if (aspectRatio.Width == 0 && aspectRatio.Height == 0)
        {
            return (0, 0, 0);
        }

        if (aspectRatio.Width == 1 && aspectRatio.Height == 1)
        {
            return (1, 0, 0);
        }

        foreach (var kvp in AspectRatioIdcMapping)
        {
            if (kvp.Value.Equals(aspectRatio))
            {
                return (kvp.Key, 0, 0);
            }
        }

        // If not found in predefined mappings, return as extended SAR
        return (255, aspectRatio.Width, aspectRatio.Height);
    }
}
