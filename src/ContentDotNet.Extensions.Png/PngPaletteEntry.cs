using ContentDotNet.Colors;
using System.Drawing;

namespace ContentDotNet.Extensions.Png;

public record struct PngPaletteEntry(byte Red, byte Green, byte Blue)
{
    public readonly Rgb ToRgb() => new(Red, Green, Blue);

    public readonly Color ToColor() => Color.FromArgb(Red, Green, Blue);
}
