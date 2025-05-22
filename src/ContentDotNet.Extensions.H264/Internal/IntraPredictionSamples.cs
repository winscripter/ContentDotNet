using System.Drawing;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Internal;

internal ref struct IntraPredictionSamples
{
    private readonly int _sqrtNumElementsInP;
    private readonly Size _size;

    public Span<int> P;
    public Span<int> Left;
    public Span<int> Top;
    public int LeftTop;

    public IntraPredictionSamples(Span<int> p, Span<int> left, Span<int> top, int leftTop)
    {
        P = p;
        Left = left;
        Top = top;
        LeftTop = leftTop;

        _sqrtNumElementsInP = (int)Math.Sqrt(p.Length);
        _size = new(_sqrtNumElementsInP, _sqrtNumElementsInP);
    }

    public readonly Size Size => _size;
    public readonly int WidthAndHeight => _sqrtNumElementsInP;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int GetP(int x, int y)
    {
#if !SUPPRESS_CHECKS_THAT_COULD_SLOW_THINGS_DOWN
        if (x < -1 || x >= WidthAndHeight)
            throw new ArgumentOutOfRangeException(nameof(x), "x must range between -1 and " + WidthAndHeight);
        if (y < -1 || y >= WidthAndHeight)
            throw new ArgumentOutOfRangeException(nameof(y), "y must range between -1 and " + WidthAndHeight);
#endif

        if (x == -1 && y == -1)
            return LeftTop;

        if (x == -1)
            return Left[y];

        if (y == -1)
            return Top[x];

        return P[x * 4 + y];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetP(int x, int y, int value)
    {
#if !SUPPRESS_CHECKS_THAT_COULD_SLOW_THINGS_DOWN
        if (x < -1 || x >= WidthAndHeight)
            throw new ArgumentOutOfRangeException(nameof(x), "x must range between -1 and " + WidthAndHeight);
        if (y < -1 || y >= WidthAndHeight)
            throw new ArgumentOutOfRangeException(nameof(y), "y must range between -1 and " + WidthAndHeight);
#endif

        if (x == -1 && y == -1)
            LeftTop = value;
        else if (x == -1)
            Left[y] = value;
        else if (y == -1)
            Top[x] = value;
        else
            P[x * 4 + y] = value;
    }

    public void FillP(int value)
    {
        for (int i = 0; i < P.Length; i++)
            P[i] = value;
    }
}
