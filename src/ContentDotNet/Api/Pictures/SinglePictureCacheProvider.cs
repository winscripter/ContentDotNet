namespace ContentDotNet.Api.Pictures
{
    using ContentDotNet.Api.Colors;
    using ContentDotNet.Pictures.Implementation;

    /// <summary>
    ///   Single picture cache provider
    /// </summary>
    public static class SinglePictureCacheProvider
    {
        /// <summary>
        ///   Creates the default single picture cache factory.
        /// </summary>
        /// <returns>The single picture cache factory.</returns>
        public static ISinglePictureCacheFactory CreateDefaultFactory() => new DefaultSinglePictureCacheFactory();

        /// <summary>
        ///   Creates the default single picture cache.
        /// </summary>
        /// <typeparam name="T">The color format.</typeparam>
        /// <returns>The single picture cache.</returns>
        public static ISinglePictureCache<T> CreateDefaultSinglePictureCache<T>()
            where T : unmanaged, IColor
            => CreateDefaultFactory().CreateSinglePictureCache<T>();
    }
}
