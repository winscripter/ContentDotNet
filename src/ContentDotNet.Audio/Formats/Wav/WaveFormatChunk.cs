namespace ContentDotNet.Audio.Formats.Wav
{
    using System.Text;

    // ==============================================
    // "fmt " Chunk (Format)
    // ==============================================
    public class WaveFormatChunk : WaveChunk
    {
        public ushort AudioFormat { get; set; }      // 1 = PCM
        public ushort NumChannels { get; set; }
        public uint SampleRate { get; set; }
        public uint ByteRate { get; set; }
        public ushort BlockAlign { get; set; }
        public ushort BitsPerSample { get; set; }

        public WaveFormatChunk()
        {
            ChunkId = "fmt ";
        }

        public override void Read(BinaryReader reader)
        {
            ChunkSize = reader.ReadInt32();
            AudioFormat = reader.ReadUInt16();
            NumChannels = reader.ReadUInt16();
            SampleRate = reader.ReadUInt32();
            ByteRate = reader.ReadUInt32();
            BlockAlign = reader.ReadUInt16();
            BitsPerSample = reader.ReadUInt16();

            // Skip any extra bytes (non-PCM extensions)
            if (ChunkSize > 16)
                reader.BaseStream.Seek(ChunkSize - 16, SeekOrigin.Current);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Encoding.ASCII.GetBytes(ChunkId));
            writer.Write(16); // PCM format chunk size is always 16
            writer.Write(AudioFormat);
            writer.Write(NumChannels);
            writer.Write(SampleRate);
            writer.Write(ByteRate);
            writer.Write(BlockAlign);
            writer.Write(BitsPerSample);
        }
    }
}
