using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

public struct Avc3DNalUnitHeaderExtension : INalUnitHeaderExtension
{
    private const uint StructureSize = 23;

    public bool NonIDRFlag;
    public uint PriorityId;
    public uint ViewId;
    public uint TemporalId;
    public bool AnchorPicFlag;
    public bool InterViewFlag;
    public bool ReservedOneBit;

    public Avc3DNalUnitHeaderExtension(bool nonIDRFlag, uint priorityId, uint viewId, uint temporalId, bool anchorPicFlag, bool interViewFlag, bool reservedOneBit)
    {
        NonIDRFlag = nonIDRFlag;
        PriorityId = priorityId;
        ViewId = viewId;
        TemporalId = temporalId;
        AnchorPicFlag = anchorPicFlag;
        InterViewFlag = interViewFlag;
        ReservedOneBit = reservedOneBit;
    }

    /// <inheritdoc cref="IBitstreamWriteable.SupportsAsynchronousWrite" />
    public readonly bool SupportsAsynchronousWrite => true;

    public readonly uint GetSizeInBits() => StructureSize;

    public void Write(BitStreamWriter writer)
    {
    }

    public Task WriteAsync(BitStreamWriter writer)
    {
        throw new NotImplementedException();
    }
}
