using ContentDotNet.Colors;
using ContentDotNet.Pictures;

namespace ContentDotNet.Filters.Image;

/// <summary>
///   The image filter.
/// </summary>
public abstract class ImageFilter
{
    public abstract Task ApplyAsync<TPixel>(
        Picture<TPixel> pic,
        IImageFilterOptions? options = null, 
        CancellationToken cancellationToken = default)
        where TPixel : unmanaged, IColor;

    public void Apply<TPixel>(
        Picture<TPixel> pic,
        IImageFilterOptions? options = null)
        where TPixel : unmanaged, IColor
    {
        ApplyAsync(pic, options, CancellationToken.None)
            .GetAwaiter()
            .GetResult();
    }
}
