namespace ContentDotNet.Abstractions;

public record struct AspectRatio(int Width, int Height)
{
    public static readonly AspectRatio AR1_1 = new(1, 1);
}
