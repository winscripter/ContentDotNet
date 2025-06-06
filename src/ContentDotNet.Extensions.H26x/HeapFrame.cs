using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Represents a frame allocated on the heap.
/// </summary>
public sealed class HeapFrame : IFrame
{
    private Matrix? _y;
    private Matrix? _u;
    private Matrix? _v;

    /// <summary>
    ///   Initializes a new instance of the <see cref="HeapFrame"/> class.
    /// </summary>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    public HeapFrame(int width, int height)
    {
        _y = new Matrix(width, height);
        _u = new Matrix(width, height);
        _v = new Matrix(width, height);
    }

    /// <inheritdoc cref="IFrame.Width" />
    public int Width
    {
        get => _y!.Width;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc cref="IFrame.Height" />
    public int Height
    {
        get => _y!.Height;
        set => throw new NotImplementedException();
    }

    public Matrix Y
    {
        get => _y!;
        set => _y = value;
    }

    public Matrix U
    {
        get => _u!;
        set => _u = value;
    }

    public Matrix V
    {
        get => _v!;
        set => _v = value;
    }

    /// <inheritdoc cref="IDisposable.Dispose()" />
    public void Dispose()
    {
        // Since this is heap-allocated, we can only hint the GC
        // to dispose of it.
        _y = null;
        _u = null;
        _v = null;

        GC.SuppressFinalize(this);
    }
}
