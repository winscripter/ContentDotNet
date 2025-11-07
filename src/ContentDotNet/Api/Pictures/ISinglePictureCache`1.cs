namespace ContentDotNet.Api.Pictures
{
    using ContentDotNet.Api.Colors;

    /// <summary>
    ///   Defines picture caching for one picture.
    /// </summary>
    /// <typeparam name="T">Color type</typeparam>
    public interface ISinglePictureCache<T> : IDisposable
        where T : unmanaged, IColor
    {
        /// <summary>
        ///   Stores the picture into the cache.
        /// </summary>
        /// <param name="picture">The picture to cache.</param>
        void StorePicture(Picture<T> picture);

        /// <summary>
        ///   Returns the picture that's cached.
        /// </summary>
        /// <returns>The cached picture.</returns>
        Picture<T> GetPicture();

        /// <summary>
        ///   Has the picture been cached?
        /// </summary>
        bool Cached { get; }
    }
}
