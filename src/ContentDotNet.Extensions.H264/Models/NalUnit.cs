namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
///   This is a marker interface that can represent one of the following types:
///   <list type="bullet">
///     <item><see cref="Avc3DNalUnitHeaderExtension"/></item>
///     <item><see cref="MvcNalUnitHeaderExtension"/></item>
///     <item><see cref="SvcNalUnitHeaderExtension"/></item>
///   </list>
/// </summary>
public interface INalUnitHeaderExtension
{
}

/// <summary>
/// Represents a Network Abstraction Layer unit.
/// </summary>
public struct NalUnit
{
    /// <summary>
    /// NAL Ref Idc.
    /// </summary>
    public uint NalRefIdc;

    /// <summary>
    ///   Represents the NAL unit type.
    /// </summary>
    public uint NalUnitType;

    /// <summary>
    /// Is SVC extension present?
    /// </summary>
    public bool SvcExtensionFlag;

    /// <summary>
    /// Is AVC extension present?
    /// </summary>
    public bool Avc3DExtensionFlag;

    /// <summary>
    ///   NAL unit extension.
    /// </summary>
    public INalUnitHeaderExtension? Extension;

    public NalUnit(uint nalRefIdc, uint nalUnitType, bool svcExtensionFlag, bool avc3DExtensionFlag, INalUnitHeaderExtension? extension)
    {
        NalRefIdc = nalRefIdc;
        NalUnitType = nalUnitType;
        SvcExtensionFlag = svcExtensionFlag;
        Avc3DExtensionFlag = avc3DExtensionFlag;
        Extension = extension;
    }
}

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
}

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

public struct SvcNalUnitHeaderExtension : INalUnitHeaderExtension
{
    public bool IDRFlag;
    public uint PriorityId;
    public bool NoInterLayerPredFlag;
    public uint DependencyId;
    public uint QualityId;
    public uint TemporalId;
    public bool UseRefPicBaseFlag;
    public bool DiscardableFlag;
    public bool OutputFlag;
    public uint ReservedThree2Bits;

    public SvcNalUnitHeaderExtension(bool iDRFlag, uint priorityId, bool noInterLayerPredFlag, uint dependencyId, uint qualityId, uint temporalId, bool useRefPicBaseFlag, bool discardableFlag, bool outputFlag, uint reservedThree2Bits)
    {
        IDRFlag = iDRFlag;
        PriorityId = priorityId;
        NoInterLayerPredFlag = noInterLayerPredFlag;
        DependencyId = dependencyId;
        QualityId = qualityId;
        TemporalId = temporalId;
        UseRefPicBaseFlag = useRefPicBaseFlag;
        DiscardableFlag = discardableFlag;
        OutputFlag = outputFlag;
        ReservedThree2Bits = reservedThree2Bits;
    }
}
