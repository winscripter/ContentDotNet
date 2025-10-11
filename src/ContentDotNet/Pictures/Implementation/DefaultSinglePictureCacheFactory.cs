namespace ContentDotNet.Pictures.Implementation
{
    using ContentDotNet.Colors;

    internal class DefaultSinglePictureCacheFactory : ISinglePictureCacheFactory
    {
        public ISinglePictureCache<T> CreateSinglePictureCache<T>()
            where T : unmanaged, IColor
        {
            return new DefaultSinglePictureCache<T>();
        }
    }
}
