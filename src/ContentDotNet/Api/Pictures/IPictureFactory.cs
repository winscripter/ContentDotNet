namespace ContentDotNet.Api.Pictures
{
    using ContentDotNet.Api.Colors;

    /// <summary>
    ///   The picture factory.
    /// </summary>
    public interface IPictureFactory
    {
        /// <summary>
        ///   Creates the picture.
        /// </summary>
        /// <typeparam name="T">Pixel type</typeparam>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>The created picture</returns>
        Picture<T> CreatePicture<T>(int width, int height)
            where T : unmanaged, IColor;
    }
}
