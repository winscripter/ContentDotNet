using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace ContentDotNet.Extensions.H264.Internal;

internal static class WeightedPredictionSamples
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetO(ref Vector64<int> o, int index, int value)
    {
        o = o.WithElement(index, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetW(ref Vector64<int> w, int index, int value)
    {
        w = w.WithElement(index, value);
    }

    public static int GetO(Vector64<int> o, int index) => o[index];
    public static int GetW(Vector64<int> w, int index) => w[index];
}
