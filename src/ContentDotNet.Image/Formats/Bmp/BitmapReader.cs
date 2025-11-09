namespace ContentDotNet.Image.Formats.Bmp
{
    using ContentDotNet.Api.Binary;

    /// <summary>
    ///   Bitmap file reader.
    /// </summary>
    public class BitmapReader
    {
        private readonly BinaryReader _binaryReader;

        public BitmapReader(BinaryReader reader) => _binaryReader = reader;

        /// <summary>
        ///   Backing binary reader.
        /// </summary>
        public BinaryReader BinaryReader => _binaryReader;

        /// <summary>
        ///   Reads the bitmap file header.
        /// </summary>
        /// <returns>The bitmap file header.</returns>
        public BitmapFileHeader ReadFileHeader() => BitmapFileHeader.Read(_binaryReader);

        /// <summary>
        ///   Reads the bitmap file header.
        /// </summary>
        /// <returns>The bitmap file header.</returns>
        public async Task<BitmapFileHeader> ReadFileHeaderAsync() => await BitmapFileHeader.ReadAsync(_binaryReader);

        /// <summary>
        ///   Depending on the input bitmap file header, reads the DIB header.
        /// </summary>
        /// <returns>The DIB header.</returns>
        /// <exception cref="NotImplementedException">Thrown if DIB size is unknown</exception>
        public IBitmapHeader ReadDibHeader()
        {
            uint dibSize = _binaryReader.ReadUInt32();
            return dibSize switch
            {
                12 => BitmapCoreHeader.Read(_binaryReader),
                40 => BitmapInfoHeader.Read(_binaryReader),
                108 => BitmapV4Header.Read(_binaryReader),
                124 => BitmapV5Header.Read(_binaryReader),
                _ => throw new NotImplementedException($"Unknown DIB header size {dibSize}. Either that means ContentDotNet does not support reading this DIB header (yet?), or, most likely, it means your BMP file is corrupt")
            };
        }

        /// <summary>
        ///   Depending on the input bitmap file header, reads the DIB header. (Asynchronous version)
        /// </summary>
        /// <returns>The DIB header.</returns>
        /// <exception cref="NotImplementedException">Thrown if DIB size is unknown</exception>
        public async Task<IBitmapHeader> ReadDibHeaderAsync()
        {
            uint dibSize = await _binaryReader.ReadUInt32Async();
            return dibSize switch
            {
                12 => await BitmapCoreHeader.ReadAsync(_binaryReader),
                40 => await BitmapInfoHeader.ReadAsync(_binaryReader),
                108 => await BitmapV4Header.ReadAsync(_binaryReader),
                124 => await BitmapV5Header.ReadAsync(_binaryReader),
                _ => throw new NotImplementedException($"Unknown DIB header size {dibSize}. Either that means ContentDotNet does not support reading this DIB header (yet?), or, most likely, it means your BMP file is corrupt")
            };
        }
    }
}
