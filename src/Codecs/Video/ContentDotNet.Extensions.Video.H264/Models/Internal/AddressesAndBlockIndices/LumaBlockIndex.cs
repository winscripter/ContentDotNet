namespace ContentDotNet.Extensions.Video.H264.Models.Internal.AddressesAndBlockIndices
{
    internal record struct LumaBlockIndex(int BlockIndex, bool Availability)
    {
        public readonly ChromaBlockIndex AsChroma()
        {
            return new(BlockIndex, Availability);
        }
    }
}
