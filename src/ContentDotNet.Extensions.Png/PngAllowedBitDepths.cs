namespace ContentDotNet.Extensions.Png;

internal static class PngAllowedBitDepths
{
    public static readonly Dictionary<PngColorType, int[]> BitDepths = new()
    {
        [PngColorType.Grayscale] = [1, 2, 4, 8, 16],
        [PngColorType.Truecolor] = [8, 16],
        [PngColorType.IndexedColor] = [1, 2, 4, 8],
        [PngColorType.GrayscaleWithAlpha] = [8, 16],
        [PngColorType.TruecolorWithAlpha] = [8, 16]
    };

    public static bool IsValidBitDepth(PngColorType colorType, int bitDepth) => BitDepths[colorType].Contains(bitDepth);

    public static void ThrowIfInvalidBitDepth(PngColorType colorType, int bitDepth)
    {
        if (!IsValidBitDepth(colorType, bitDepth))
        {
            throw new ArgumentOutOfRangeException(nameof(bitDepth), $"The bit depth {bitDepth} is not valid for the color type {colorType}.");
        }
    }
}
