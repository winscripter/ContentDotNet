namespace ContentDotNet.Api.Pictures.Implementation
{
    using ContentDotNet.Api.Colors;
    using ContentDotNet.Api.Pictures;

    internal class DefaultSinglePictureCacheFactory : ISinglePictureCacheFactory
    {
        public ISinglePictureCache<T> CreateSinglePictureCache<T>()
            where T : unmanaged, IColor
        {
            return new DefaultSinglePictureCache<T>();
        }
    }
}
