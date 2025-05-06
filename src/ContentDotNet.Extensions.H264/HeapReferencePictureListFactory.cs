namespace ContentDotNet.Extensions.H264;

internal sealed class HeapReferencePictureListFactory : IReferencePictureListFactory
{
    public static readonly HeapReferencePictureListFactory Instance = new();

    public ReferencePictureList Create(int width, int height, int maximumPicNumber)
    {
        return new MemoryReferencePictureList(width, height, maximumPicNumber);
    }
}
