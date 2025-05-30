using System.Diagnostics;

namespace ContentDotNet.Primitives;

[DebuggerDisplay("{ToString()}")]
public record struct AspectRatio(int Width, int Height)
{
    public static readonly AspectRatio AR1_1 = new(1, 1);

    public readonly override string ToString() => $"{Width}:{Height}";
}
