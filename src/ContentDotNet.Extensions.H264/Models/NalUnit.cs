using ContentDotNet.Abstractions;

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
    private static readonly int StartCodeFindingRecursionLimit = DataSize.Megabytes(1);

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

    /// <summary>
    ///   Finds &amp; skips the start code 0x00 0x00 0x00 0x01. After invoking this method, you're essentially in the NAL
    ///   unit itself.
    /// </summary>
    /// <param name="reader">Bitstream reader</param>
    /// <returns><see langword="false"/> if end of stream was reached before there was a chance to find the start code.</returns>
    public static bool SkipStartCode(BitStreamReader reader)
    {
        RecursionCounter recursionCounter = new(StartCodeFindingRecursionLimit);
        int stream = 0;
        while (true)
        {
            try
            {
                recursionCounter.Increment();

                byte current = (byte)reader.ReadBits(8);
                if (stream != 3 && current == 0)
                {
                    stream++;
                    continue;
                }
                else if (stream == 3 && current == 1)
                {
                    _ = reader.ReadBits(8);
                    return true;
                }
                else
                {
                    stream = 0;
                }
            }
            catch (EndOfStreamException)
            {
                return false;
            }
            catch (InfiniteLoopException)
            {
                throw;
            }
        }
    }
}

public struct Avc3DNalUnitHeaderExtension : INalUnitHeaderExtension
{
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
