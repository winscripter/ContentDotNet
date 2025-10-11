namespace ContentDotNet.Pictures
{
    using ContentDotNet.Colors;

    /// <summary>
    ///   The in-memory picture factory.
    /// </summary>
    public class MemoryPictureFactory : IPictureFactory
    {
        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly MemoryPictureFactory Instance = new();

        /// <inheritdoc cref="IPictureFactory.CreatePicture{T}(int, int)" />
        public Picture<T> CreatePicture<T>(int width, int height) where T : unmanaged, IColor
        {
            return new MemoryPicture<T>(width, height);
        }
    }
}
