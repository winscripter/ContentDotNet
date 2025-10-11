namespace ContentDotNet.Extensions.Video.H264.Models.Internal
{
    using ContentDotNet.Extensions.Video.H264.Models;

    internal struct AddressAndPartitionIndices
    {
        public AddressAndAvailability Address;
        public PartitionIndices Indices;

        public AddressAndPartitionIndices(AddressAndAvailability address, PartitionIndices indices)
        {
            Address = address;
            Indices = indices;
        }
    }
}
