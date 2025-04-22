namespace ContentDotNet.Extensions.H264.Models;

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
