namespace ContentDotNet.Image.Formats.Gif
{
    using ContentDotNet.Api.Binary;

    /// <summary>
    ///   GIF file header.
    /// </summary>
    public struct GifHeader
    {
        /// <summary>
        ///   Equal to "GIF".
        /// </summary>
        public uint Signature;

        /// <summary>
        ///   87a or 89a
        /// </summary>
        public uint Version;

        public GifHeader(uint signature, uint version)
        {
            Signature = signature;
            Version = version;
        }

        public static GifHeader Read(BinaryReader reader)
        {
            return new(reader.ReadUInt24(), reader.ReadUInt24());
        }

        public readonly void Write(BinaryWriter writer)
        {
            writer.WriteUInt24(Signature);
            writer.WriteUInt24(Version);
        }
    }
}
