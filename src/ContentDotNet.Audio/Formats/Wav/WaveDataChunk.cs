namespace ContentDotNet.Audio.Formats.Wav
{
    using System.Text;

    public class WaveDataChunk : WaveChunk
    {
        public WaveDataChunk()
        {
            ChunkId = "data";
        }

        public override void Read(BinaryReader reader)
        {
            ChunkSize = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Encoding.ASCII.GetBytes(ChunkId));
        }
    }
}
