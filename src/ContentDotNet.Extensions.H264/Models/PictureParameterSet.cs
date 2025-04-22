using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents a Picture Parameter Set (PPS) model. This structure contains raw data.
/// </summary>
public unsafe struct PictureParameterSet : IParameterSet
{
    public readonly ParameterSetKind Kind => ParameterSetKind.Picture;

    public uint SpsId;
    public uint PpsId;
    public bool EntropyCodingModeFlag;
    public bool BottomFieldPicOrderInFramePresentFlag;
    public uint NumSliceGroupsMinus1;
    public fixed uint TopLeft[8];
    public fixed uint BottomRight[8];
    public bool SliceGroupChangeDirectionFlag;
    public uint SliceGroupChangeRateMinus1;
    public uint SliceGroupMapType;
    public uint PicSizeInMapUnits;
    public uint NumRefIdxL0DefaultActiveMinus1;
    public uint NumRefIdxL1DefaultActiveMinus1;
    public bool WeightedPredFlag;
    public uint WeightedBiPredIdc;
    public int PicInitQpMinus26;
    public int PicInitQsMinus26;
    public int ChromaQpIndexOffset;
    public bool DeblockingFilterControlPresentFlag;
    public bool ConstrainedIntraPredFlag;
    public bool RedundantPicCntPresentFlag;
    public bool Transform8x8ModeFlag;
    public bool PicScalingMatrixPresentFlag;
    public ScalingMatrices? ScalingMatrix;

    public PictureParameterSet(uint spsId, uint ppsId, bool entropyCodingModeFlag, bool bottomFieldPicOrderInFramePresentFlag, uint numSliceGroupsMinus1, Span<uint> topLeft, Span<uint> bottomRight, bool sliceGroupChangeDirectionFlag, uint sliceGroupChangeRateMinus1, uint sliceGroupMapType, uint picSizeInMapUnits, uint numRefIdxL0DefaultActiveMinus1, uint numRefIdxL1DefaultActiveMinus1, bool weightedPredFlag, uint weightedBiPredIdc, int picInitQpMinus26, int picInitQsMinus26, int chromaQpIndexOffset, bool deblockingFilterControlPresentFlag, bool constrainedIntraPredFlag, bool redundantPicCntPresentFlag)
    {
        SpsId = spsId;
        PpsId = ppsId;
        EntropyCodingModeFlag = entropyCodingModeFlag;
        BottomFieldPicOrderInFramePresentFlag = bottomFieldPicOrderInFramePresentFlag;
        NumSliceGroupsMinus1 = numSliceGroupsMinus1;
        for (int i = 0; i < topLeft.Length; i++)
            TopLeft[i] = topLeft[i];
        for (int i = 0; i < bottomRight.Length; i++)
            BottomRight[i] = bottomRight[i];
        SliceGroupChangeDirectionFlag = sliceGroupChangeDirectionFlag;
        SliceGroupChangeRateMinus1 = sliceGroupChangeRateMinus1;
        SliceGroupMapType = sliceGroupMapType;
        PicSizeInMapUnits = picSizeInMapUnits;
        NumRefIdxL0DefaultActiveMinus1 = numRefIdxL0DefaultActiveMinus1;
        NumRefIdxL1DefaultActiveMinus1 = numRefIdxL1DefaultActiveMinus1;
        WeightedPredFlag = weightedPredFlag;
        WeightedBiPredIdc = weightedBiPredIdc;
        PicInitQpMinus26 = picInitQpMinus26;
        PicInitQsMinus26 = picInitQsMinus26;
        ChromaQpIndexOffset = chromaQpIndexOffset;
        DeblockingFilterControlPresentFlag = deblockingFilterControlPresentFlag;
        ConstrainedIntraPredFlag = constrainedIntraPredFlag;
        RedundantPicCntPresentFlag = redundantPicCntPresentFlag;
    }

    public static PictureParameterSet Read(BitStreamReader reader)
    {
        uint spsId = reader.ReadUE();
        uint ppsId = reader.ReadUE();
        bool entropyCodingModeFlag = reader.ReadBit();
        bool bottomFieldPicOrderInFramePresentFlag = reader.ReadBit();
        uint numSliceGroupsMinus1 = reader.ReadUE();

        uint sliceGroupMapType = reader.ReadUE();

        if (numSliceGroupsMinus1 > 0)
        {

        }
    }
}
