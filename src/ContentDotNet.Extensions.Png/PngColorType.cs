namespace ContentDotNet.Extensions.Png;

/// <summary>
///   Represents the PNG color type.
/// </summary>
public enum PngColorType : byte
{
    Grayscale = 0,
    Truecolor = 2,
    IndexedColor = 3,
    GrayscaleWithAlpha = 4,
    TruecolorWithAlpha = 6
}
