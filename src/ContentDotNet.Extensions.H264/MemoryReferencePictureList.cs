using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   An in-memory reference picture list.
/// </summary>
internal sealed class MemoryReferencePictureList : ReferencePictureList
{
    private readonly IFrame[] _refPics;

    public MemoryReferencePictureList(int w, int h, int count)
    {
        _refPics = new IFrame[count];
        for (int i = 0; i < count; i++)
            _refPics[i] = new HeapFrame(w, h);
    }

    public override IFrame this[int index]
    {
        get => _refPics[index];
        set => _refPics[index] = value;
    }

    public override void Clear()
    {
        int w = _refPics[0].Width;
        int h = _refPics[0].Height;
        Array.Clear(_refPics);
        for (int i = 0; i < _refPics.Length; i++)
            _refPics[i] = new HeapFrame(w, h);
    }

    protected override void DisposeInternal()
    {
        Array.Clear(_refPics);

        GC.Collect();
    }
}
