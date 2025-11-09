namespace ContentDotNet.Audio.Formats.Wav
{
    /// <summary>
    ///   Wave file reader &amp; demultiplexer.
    /// </summary>
    public class WaveReader
    {
        /// <summary>
        ///   File size.
        /// </summary>
        public uint FileSize { get; set; }

        private readonly BinaryReader reader;

        public WaveReader(BinaryReader reader)
        {
            byte[] first = reader.ReadBytes(4);
            if (first[0] != 'R' ||
                first[1] != 'I' ||
                first[2] != 'F' ||
                first[3] != 'F')
                throw new InvalidOperationException("Not a valid WAV file");

            FileSize = reader.ReadUInt32();

            byte[] second = reader.ReadBytes(4);
            if (second[0] != 'W' ||
                second[1] != 'A' ||
                second[2] != 'V' ||
                second[3] != 'E')
                throw new InvalidOperationException("Not a valid WAV file");

            this.reader = reader;
        }

        public WaveChunkType ReadChunkFourCC()
        {
            byte[] next4bytes = this.reader.ReadBytes(4);
            if (next4bytes[0] == 'f' &&
                next4bytes[1] == 'm' &&
                next4bytes[2] == 't' &&
                next4bytes[3] == ' ')
                return WaveChunkType.Fmt;
            else if (next4bytes[0] == 'd' &&
                next4bytes[1] == 'a' &&
                next4bytes[2] == 't' &&
                next4bytes[3] == 'a')
                return WaveChunkType.Data;
            else
                return WaveChunkType.Unknown;
        }

        public WaveFormatChunk ReadFormatChunk()
        {
            var wfc = new WaveFormatChunk();
            wfc.Read(reader);
            return wfc;
        }

        public WaveDataChunk ReadDataChunk()
        {
            var wdc = new WaveDataChunk();
            wdc.Read(reader);
            return wdc;
        }
    }
}
