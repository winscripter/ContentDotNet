using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents a reference picture list.
/// </summary>
public abstract class ReferencePictureList : IDisposable
{
    private bool _isDisposed = false;

    /// <summary>
    ///   Is this reference picture list disposed?
    /// </summary>
    protected bool IsDisposed => _isDisposed;

    /// <summary>
    ///   Throws <see cref="ObjectDisposedException"/> if <see cref="IsDisposed"/> is <c>true</c>.
    /// </summary>
    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
    }

    /// <summary>
    ///   Gets/sets a frame at index.
    /// </summary>
    /// <param name="index">Frame index</param>
    /// <returns>Frame at index</returns>
    public abstract IFrame this[int index] { get; set; }

    /// <summary>
    ///   Number of frames.
    /// </summary>
    public int FrameCount { get; set; } = 1;

    /// <summary>
    ///   Resets all frames to their default values.
    /// </summary>
    public abstract void Clear();

    /// <summary>
    ///   Actual disposal logic is here.
    /// </summary>
    protected abstract void DisposeInternal();

    /// <summary>
    ///   Releases memory.
    /// </summary>
    public void Dispose()
    {
        ThrowIfDisposed();
        DisposeInternal();
        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}
