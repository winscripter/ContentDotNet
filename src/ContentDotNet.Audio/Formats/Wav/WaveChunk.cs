namespace ContentDotNet.Audio.Formats.Wav
{
    public abstract class WaveChunk
    {
        public string ChunkId { get; protected set; } = "";
        public int ChunkSize { get; protected set; }

        public abstract void Read(BinaryReader reader);
        public abstract void Write(BinaryWriter writer);
    }
}
