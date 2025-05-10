namespace ContentDotNet.Extensions.H264.Pictures;

/// <summary>
///   Represents the Decoded Picture Buffer (DPB).
/// </summary>
public sealed class Dpb : IDisposable
{
    /// <summary>
    ///   All reference pictures.
    /// </summary>
    public List<ReferencePicture?> Pictures { get; set; }

    /// <summary>
    ///   Maximum DPB size.
    /// </summary>
    public int MaxSize { get; set; }

    internal Dpb(List<ReferencePicture?> pictures, int maxSize)
    {
        Pictures = pictures;
        MaxSize = maxSize;
    }

    /// <summary>
    ///   Adds a reference picture.
    /// </summary>
    /// <param name="picture">Reference picture to add</param>
    public void AddPicture(ReferencePicture picture)
    {
        if (Pictures.Count >= MaxSize)
        {
            RemoveOldestShortTerm();
        }

        Pictures.Add(picture);
    }

    private void RemoveOldestShortTerm()
    {
        ReferencePicture? oldest = Pictures.FirstOrDefault(p => p?.ReferenceType == PictureReferenceType.ShortTerm);
        if (oldest is not null)
            Pictures.Remove(oldest);
    }

    /// <summary>
    ///   Releases memory.
    /// </summary>
    public void Dispose()
    {
        foreach (var picture in Pictures)
            picture?.Dispose();

        Pictures.Clear();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Gets/sets a reference picture at index in the DPB.
    /// </summary>
    /// <param name="index">Index of the reference picture.</param>
    /// <returns>A reference picture, indexed by <paramref name="index"/>.</returns>
    public ReferencePicture? this[int index]
    {
        get => this.Pictures[index];
        set => this.Pictures[index] = value;
    }
}
