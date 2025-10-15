namespace ContentDotNet.Extensions.Video.Mp4
{
    /// <summary>
    ///   I/O for an MP4 box.
    /// </summary>
    public abstract class Mp4BoxIO
    {
        /// <summary>
        ///   The type of box data.
        /// </summary>
        public abstract Type TypeOfBoxData { get; }

        /// <summary>
        ///   Writes the box data.
        /// </summary>
        /// <remarks>
        ///   This method does not write the box size and type; just the data.
        /// </remarks>
        /// <param name="box">The box</param>
        /// <param name="stream">The stream</param>
        public abstract void WriteBoxData(Mp4Box box, Stream stream);

        /// <summary>
        ///   Writes the box data.
        /// </summary>
        /// <remarks>
        ///   This method does not write the box size and type; just the data.
        /// </remarks>
        /// <param name="box">The box</param>
        /// <param name="stream">The stream</param>
        public abstract Task WriteBoxDataAsync(Mp4Box box, Stream stream);

        /// <summary>
        ///   Read the box.
        /// </summary>
        /// <param name="box">Input parsed box header. The data is put into the <see cref="Mp4Box.Data"/> property.</param>
        /// <param name="stream">The stream</param>
        /// <returns>The box</returns>
        public abstract Task ReadBoxDataAsync(Mp4Box box, Stream stream);

        /// <summary>
        ///   Read the box.
        /// </summary>
        /// <param name="box">Input parsed box header. The data is put into the <see cref="Mp4Box.Data"/> property.</param>
        /// <param name="stream">The stream</param>
        /// <returns>The box</returns>
        public abstract void ReadBoDatax(Mp4Box box, Stream stream);
    }
}
