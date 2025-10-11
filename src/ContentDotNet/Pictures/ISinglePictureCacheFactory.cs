namespace ContentDotNet.Pictures
{
    using ContentDotNet.Colors;

    /// <summary>
    ///   Factory for <see cref="ISinglePictureCache{T}"/>.
    /// </summary>
    public interface ISinglePictureCacheFactory
    {
        /// <summary>
        ///   Creates the single picture cache.
        /// </summary>
        /// <typeparam name="T">Color type</typeparam>
        /// <returns>The single picture cache.</returns>
        ISinglePictureCache<T> CreateSinglePictureCache<T>()
            where T : unmanaged, IColor;
    }
}
