namespace ContentDotNet.Extensions.Video.Mp4
{
    using ContentDotNet.Binary;
    using ContentDotNet.Primitives;

    /// <summary>
    ///   The MP4 box.
    /// </summary>
    public class Mp4Box
    {
        private IMp4BoxData? data;
        private long size;
        private FourCC type;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Mp4Box"/> class.
        /// </summary>
        public Mp4Box()
        {
        }

        /// <summary>
        ///   MP4 box 4CC to IO mapping.
        /// </summary>
        public Dictionary<string, Mp4BoxIO> FourCCToIO { get; set; } = [];

        /// <summary>
        ///   The MP4 box data.
        /// </summary>
        public IMp4BoxData? Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        /// <summary>
        ///   The box size.
        /// </summary>
        public long Size
        {
            get => size;
            set => size = value;
        }

        /// <summary>
        ///   The box type.
        /// </summary>
        public FourCC Type
        {
            get => type;
            set => type = value;
        }

        /// <summary>
        ///   Parses the box header.
        /// </summary>
        /// <param name="reader">Box header</param>
        public void Parse(BinaryReader reader)
        {
            this.Size = reader.ReadInt32();
            this.Type = new FourCC(reader.ReadUInt32());

            if (this.Size == 1)
                this.Size = reader.ReadInt64();
        }

        /// <summary>
        ///   Reads the specified MP4 box.
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="fourCCtoIO">4CC to I/O mapping</param>
        /// <returns>The MP4 box</returns>
        public static Mp4Box Read(BinaryReader reader, Dictionary<string, Mp4BoxIO> fourCCtoIO)
        {
            var box = new Mp4Box();
            box.Parse(reader);
            box.FourCCToIO = fourCCtoIO;
            fourCCtoIO[box.Type.ValueText].ReadBoxData(box, reader);
            return box;
        }

        /// <summary>
        ///   Reads the specified MP4 box.
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="fourCCtoIO">4CC to I/O mapping</param>
        /// <returns>The MP4 box</returns>
        public static async Task<Mp4Box> ReadAsync(BinaryReader reader, Dictionary<string, Mp4BoxIO> fourCCtoIO)
        {
            var box = new Mp4Box();
            box.Parse(reader);
            box.FourCCToIO = fourCCtoIO;
            await fourCCtoIO[box.Type.ValueText].ReadBoxDataAsync(box, reader);
            return box;
        }

        /// <summary>
        ///   Writes this MP4 box.
        /// </summary>
        /// <param name="writer">The specified binary writer.</param>
        public void Write(BinaryWriter writer)
        {
            if (this.Size <= int.MaxValue)
                writer.Write((int)this.Size);
            else
                writer.Write(1);
            writer.Write(this.Type.Value);
            if (this.Size > int.MaxValue)
                writer.Write(this.Size);

            this.FourCCToIO[this.Type.ValueText].WriteBoxData(this, writer);
        }

        /// <summary>
        ///   Writes this MP4 box.
        /// </summary>
        /// <param name="writer">The specified binary writer.</param>
        public async Task WriteAsync(BinaryWriter writer)
        {
            if (this.Size <= int.MaxValue)
                await writer.WriteAsync((int)this.Size);
            else
                await writer.WriteAsync(1);
            await writer.WriteAsync(this.Type.Value);
            if (this.Size > int.MaxValue)
                await writer.WriteAsync(this.Size);

            await this.FourCCToIO[this.Type.ValueText].WriteBoxDataAsync(this, writer);
        }
    }
}
