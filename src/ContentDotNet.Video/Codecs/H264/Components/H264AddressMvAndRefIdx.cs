namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.Primitives;

    internal record struct H264AddressMvAndRefIdx(H264AddressAndAvailability Address, MotionVector Mv, int RefIdx, bool Availability);
}
