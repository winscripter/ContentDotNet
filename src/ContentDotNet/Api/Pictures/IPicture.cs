namespace ContentDotNet.Api.Pictures
{
    using System.Drawing;

    /// <summary>
    ///   Abstraction of a picture.
    /// </summary>
    public interface IPicture
    {
        /// <summary>
        ///   Writes the pixel data from the specified plane to the specified stream.
        /// </summary>
        /// <param name="output">The output plane data.</param>
        /// <param name="planeIndex">The plane index.</param>
        void WritePlanePixelData(Stream output, int planeIndex);

        /// <summary>
        ///   Writes the pixel data from the specified plane to the specified stream.
        /// </summary>
        /// <param name="output">The output plane data.</param>
        /// <param name="planeIndex">The plane index.</param>
        Task WritePlanePixelDataAsync(Stream output, int planeIndex);

        /// <summary>
        ///   The size of the picture.
        /// </summary>
        Size PictureSize { get; set; }

        /// <summary>
        ///   Accesses the pixel at the specified plane + X/Y.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int this[int plane, int x, int y] { get; set; }

        /// <summary>
        ///   Number of planes
        /// </summary>
        int Planes { get; }
        
        /// <summary>
        ///   Used by <see cref="WritePlanePixelData(Stream, int)"/> and <see cref="WritePlanePixelDataAsync(Stream, int)"/>
        ///   to specify how many bytes to write per pixel.
        /// </summary>
        int BytesPerPixel { get; set; }
    }
}
