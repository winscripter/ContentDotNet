namespace ContentDotNet.Video.Codecs.H264.Components
{
    internal struct H264AddressAndPartitionIndices
    {
        public H264AddressAndAvailability Address;
        public PartitionIndices Indices;

        public H264AddressAndPartitionIndices(H264AddressAndAvailability address, PartitionIndices indices)
        {
            Address = address;
            Indices = indices;
        }
    }
}
