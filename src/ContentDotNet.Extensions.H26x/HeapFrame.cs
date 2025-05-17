using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Represents a frame allocated on the heap.
/// </summary>
public sealed class HeapFrame : IFrame
{
    private Yuv[][] _yuv;

    /// <summary>
    ///   Initializes a new instance of the <see cref="HeapFrame"/> class.
    /// </summary>
    /// <param name="yuv">All YUV colors</param>
    public HeapFrame(Yuv[][] yuv)
    {
        _yuv = yuv;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="HeapFrame"/> class, creating an
    ///   empty frame with only pixels of #000000 with the size being <paramref name="width"/>x<paramref name="height"/>,
    ///   accordingly.
    /// </summary>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    public HeapFrame(int width, int height)
    {
        Yuv[][] yuvs = new Yuv[width][];
        for (int x = 0; x < width; x++)
            yuvs[x] = new Yuv[height];
        _yuv = yuvs;
    }

    /// <inheritdoc cref="IFrame.this[int, int]" />
    public Yuv this[int x, int y]
    {
        get => _yuv[x][y];
        set => _yuv[x][y] = value;
    }

    /// <inheritdoc cref="IFrame.Width" />
    public int Width
    {
        get => _yuv.Length;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc cref="IFrame.Height" />
    public int Height
    {
        get => _yuv[0].Length;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc cref="IDisposable.Dispose()" />
    public void Dispose()
    {
        // Since this is heap-allocated, we can only hint the GC
        // to dispose of it.
        _yuv = [];

        GC.SuppressFinalize(this);
    }
}
