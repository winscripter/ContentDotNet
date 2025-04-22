namespace ContentDotNet.Extensions.H264.Models;

public struct SvcNalUnitHeaderExtension(bool iDRFlag, uint priorityId, bool noInterLayerPredFlag, uint dependencyId, uint qualityId, uint temporalId, bool useRefPicBaseFlag, bool discardableFlag, bool outputFlag, uint reservedThree2Bits)
    : INalUnitHeaderExtension
{
    public bool IDRFlag = iDRFlag;
    public uint PriorityId = priorityId;
    public bool NoInterLayerPredFlag = noInterLayerPredFlag;
    public uint DependencyId = dependencyId;
    public uint QualityId = qualityId;
    public uint TemporalId = temporalId;
    public bool UseRefPicBaseFlag = useRefPicBaseFlag;
    public bool DiscardableFlag = discardableFlag;
    public bool OutputFlag = outputFlag;
    public uint ReservedThree2Bits = reservedThree2Bits;
}
