namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents a reference picture list.
/// </summary>
public sealed class ReferencePictureList : IDisposable
{
    private bool _isDisposed = false;
    private ReferencePicture[]? pics;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ReferencePictureList"/> class.
    /// </summary>
    public ReferencePictureList()
    {
        pics = new ReferencePicture[16];
    }

    /// <summary>
    ///   Is this reference picture list disposed?
    /// </summary>
    public bool IsDisposed => _isDisposed;

    /// <summary>
    ///   Throws <see cref="ObjectDisposedException"/> if <see cref="IsDisposed"/> is <c>true</c>.
    /// </summary>
    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
    }

    /// <summary>
    ///   Gets/sets a frame at index.
    /// </summary>
    /// <param name="index">Frame index</param>
    /// <returns>Frame at index</returns>
    public ReferencePicture this[int index]
    {
        get => pics![index];
        set => pics![index] = value;
    }

    /// <summary>
    ///   Number of frames.
    /// </summary>
    public int FrameCount { get; set; } = 1;

    /// <summary>
    ///   Resets all frames to their default values.
    /// </summary>
    public void Clear() => Array.Clear(pics!);

    /// <summary>
    ///   Actual disposal logic is here.
    /// </summary>
    private void DisposeInternal() => pics = null;

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
