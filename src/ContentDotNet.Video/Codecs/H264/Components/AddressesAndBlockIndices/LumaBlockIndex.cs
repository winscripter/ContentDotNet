namespace ContentDotNet.Video.Codecs.H264.Components.AddressesAndBlockIndices
{
    internal record struct LumaBlockIndex(int BlockIndex, bool Availability)
    {
        public readonly ChromaBlockIndex AsChroma()
        {
            return new(BlockIndex, Availability);
        }
    }
}
