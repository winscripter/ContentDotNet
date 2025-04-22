namespace ContentDotNet.Extensions.H264.Models;

public struct MvcNalUnitHeaderExtension : INalUnitHeaderExtension
{
    public bool NonIDRFlag;
    public uint PriorityId;
    public uint ViewId;
    public uint TemporalId;
    public bool AnchorPicFlag;
    public bool InterViewFlag;
    public bool ReservedOneBit;

    public MvcNalUnitHeaderExtension(bool nonIDRFlag, uint priorityId, uint viewId, uint temporalId, bool anchorPicFlag, bool interViewFlag, bool reservedOneBit)
    {
        NonIDRFlag = nonIDRFlag;
        PriorityId = priorityId;
        ViewId = viewId;
        TemporalId = temporalId;
        AnchorPicFlag = anchorPicFlag;
        InterViewFlag = interViewFlag;
        ReservedOneBit = reservedOneBit;
    }
}
