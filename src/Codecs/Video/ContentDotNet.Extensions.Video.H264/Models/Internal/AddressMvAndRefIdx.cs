namespace ContentDotNet.Extensions.Video.H264.Models.Internal
{
    internal record struct AddressMvAndRefIdx(AddressAndAvailability Address, H264MotionVector Mv, int RefIdx, bool Availability);
}
