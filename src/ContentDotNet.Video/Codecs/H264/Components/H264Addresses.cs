namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.Primitives;

    public record struct H264AddressAndAvailability(int Address, bool Availability);

    internal record struct H264AddressAndJmExtendedPositions(H264AddressAndAvailability Address, H264JmExtendedPositions Positions);

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

    internal record struct H264AddressMvAndRefIdx(H264AddressAndAvailability Address, MotionVector Mv, int RefIdx, bool Availability);
}
