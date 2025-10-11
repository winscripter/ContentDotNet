using ContentDotNet.Colors;
using ContentDotNet.Pictures;
using System.Numerics;

namespace ContentDotNet.Filters.Image;

public class ContrastFilter : ImageFilter
{
    public class Options : IImageFilterOptions
    {
        public float Contrast { get; set; } = 1.0f; // 1.0 = no change
    }

    public override async Task ApplyAsync<TPixel>(
        Picture<TPixel> pic,
        IImageFilterOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var contrast = (options as Options)?.Contrast ?? 1.0f;

        for (int y = 0; y < pic.ImageSize.Height; y++)
        {
            for (int x = 0; x < pic.ImageSize.Width; x++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var pixel = pic[x, y];
                var v = ((IColor)pixel).ToVector4(); // Assuming extension method or helper
                v.X = ((v.X - 0.5f) * contrast) + 0.5f;
                v.Y = ((v.Y - 0.5f) * contrast) + 0.5f;
                v.Z = ((v.Z - 0.5f) * contrast) + 0.5f;

                pic[x, y] = TPixel.FromVector4(Vector4.Clamp(v, Vector4.Zero, Vector4.One));
            }
        }

        await Task.CompletedTask;
    }
}
