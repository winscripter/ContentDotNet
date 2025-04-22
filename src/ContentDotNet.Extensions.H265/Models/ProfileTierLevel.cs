using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H265.Models;

/// <summary>
///   Represents H.265 Profile, Tier and Level.
/// </summary>
public struct ProfileTierLevel
{
    public uint GeneralProfileSpace;
    public bool GeneralTierFlag;
    public uint GeneralProfileIdc;
    public PackedFlags32 GeneralProfileCompatibilityFlag;
    public bool GeneralProgressiveSourceFlag;
    public bool GeneralInterlacedSourceFlag;
    public bool GeneralNonPackedConstraintFlag;
    public bool GeneralFrameOnlyConstraintFlag;
    public bool GeneralMax12BitConstraintFlag;
    public bool GeneralMax10BitConstraintFlag;
    public bool GeneralMax8BitConstraintFlag;
    public bool GeneralMax422ChromaConstraintFlag;
    public bool GeneralMax420ChromaConstraintFlag;
    public bool GeneralMonochromeConstraintFlag;
    public bool GeneralIntraConstraintFlag;
    public bool GeneralOnePictureOnlyConstraintFlag;
    public bool GeneralLowerBitRateConstraintFlag;
    public bool GeneralMax14BitConstraintFlag;
    public bool GeneralInbldFlag;
    public uint GeneralLevelIdc;
    public ArrayReferrer SubLayerPresentFlags;

}
