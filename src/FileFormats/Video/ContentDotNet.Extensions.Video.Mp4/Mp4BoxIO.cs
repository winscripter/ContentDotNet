namespace ContentDotNet.Extensions.Video.Mp4
{
    /// <summary>
    ///   I/O for an MP4 box.
    /// </summary>
    public abstract class Mp4BoxIO
    {
        /// <summary>
        ///   Writes the box.
        /// </summary>
        /// <param name="box">The box</param>
        /// <param name="stream">The stream</param>
        public abstract void WriteBox(Mp4Box box, Stream stream);

        /// <summary>
        ///   Writes the box.
        /// </summary>
        /// <param name="box">The box</param>
        /// <param name="stream">The stream</param>
        public abstract Task WriteBoxAsync(Mp4Box box, Stream stream);

        /// <summary>
        ///   Read the box.
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The box</returns>
        public abstract Task<Mp4Box> ReadBoxAsync(Stream stream);

        /// <summary>
        ///   Read the box.
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The box</returns>
        public abstract Mp4Box ReadBox(Stream stream);
    }
}
