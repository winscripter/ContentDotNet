namespace ContentDotNet.Video.Formats.Mp4
{
    using ContentDotNet.Api.Binary;
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;

    /// <summary>
    ///   Reads raw MP4 data.
    /// </summary>
    public class Mp4Reader(BinaryReader reader)
    {
        public BinaryReader Reader { get; set; } = reader;

        /// <summary>
        ///   Reads the MP4 box header (size and type).
        /// </summary>
        /// <returns>The header of an MP4 box.</returns>
        public (ulong size, FourCC fourCC) ReadBoxHeader()
        {
            ulong size = Reader.ReadUInt32();
            FourCC fourCC = new(Reader.ReadUInt32());
            if (size == 1)
                size = Reader.ReadUInt64();
            return (size, fourCC);
        }

        /// <summary>
        ///   Reads the MP4 box header (size and type).
        /// </summary>
        /// <returns>The header of an MP4 box.</returns>
        public async Task<(ulong size, FourCC fourCC)> ReadBoxHeaderAsync()
        {
            ulong size = await Reader.ReadUInt32Async();
            FourCC fourCC = new(await Reader.ReadUInt32Async());
            if (size == 1)
                size = await Reader.ReadUInt64Async();
            return (size, fourCC);
        }

        /// <summary>
        ///   Reads a box that's located at the root level - specifically, either ftyp or moov. The
        ///   type of the box is provided by <paramref name="fourCC"/>. See also <see cref="ReadBoxHeader"/>.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        /// <param name="fourCC">The type of the box.</param>
        /// <returns>A root-level MP4 box.</returns>
        public Mp4BoxBase ReadRootBox(ulong size, FourCC fourCC)
        {
            return fourCC.ValueText switch
            {
                "ftyp" => FileTypeBox.Parse(Reader, (long)size),
                "moov" => MovieBox.Parse(Reader, (long)size),
                "mdat" => throw new InvalidOperationException("The MDAT box will not be read with the ReadRootBox method because their contents may be too large to fit into memory. Use ReadMediaData to read the MDAT box data."),
                _ => throw new NotSupportedException($"Box {fourCC.ValueText} is not a known root-level box.")
            };
        }

        /// <summary>
        ///   Reads the MDAT box.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        /// <param name="writer">The box writer.</param>
        /// <exception cref="EndOfStreamException"></exception>
        public void ReadMediaData(ulong size, Stream writer)
        {
            for (int i = 0; i < (long)size; i++)
            {
                int @byte = Reader.ReadByte();
                if (@byte == -1)
                    throw new EndOfStreamException();
                writer.WriteByte((byte)@byte);
            }
        }
    }
}
