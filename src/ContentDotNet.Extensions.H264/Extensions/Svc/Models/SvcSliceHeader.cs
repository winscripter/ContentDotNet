using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Extensions.Svc.Models;

/// <summary>
/// Represents the header of an SVC (Scalable Video Coding) slice in an H.264 bitstream.
/// </summary>
public struct SvcSliceHeader : IEquatable<SvcSliceHeader>
{
    /// <summary>
    /// The macroblock address of the first macroblock in the slice.
    /// </summary>
    public uint FirstMbInSlice;

    /// <summary>
    /// The type of the slice (e.g., I, P, B slice).
    /// </summary>
    public uint SliceType;

    /// <summary>
    /// The ID of the picture parameter set (PPS) used by this slice.
    /// </summary>
    public uint PpsId;

    /// <summary>
    /// The color plane ID for slices in a 4:4:4 profile.
    /// </summary>
    public uint ColorPlaneId;

    /// <summary>
    /// The frame number of the current slice.
    /// </summary>
    public uint FrameNum;

    /// <summary>
    /// Indicates whether the slice is part of a field picture.
    /// </summary>
    public bool FieldPicFlag;

    /// <summary>
    /// Indicates whether the slice is part of the bottom field of a field picture.
    /// </summary>
    public bool BottomFieldFlag;

    /// <summary>
    /// The ID of the IDR (Instantaneous Decoding Refresh) picture.
    /// </summary>
    public uint IdrPicId;

    /// <summary>
    /// The least significant bits of the picture order count.
    /// </summary>
    public uint PicOrderCntLsb;

    /// <summary>
    /// The difference between the picture order count of the bottom field and the top field.
    /// </summary>
    public int DeltaPicOrderCntBottom;

    /// <summary>
    /// The first picture order count difference for inter prediction.
    /// </summary>
    public int DeltaPicOrderCnt0;

    /// <summary>
    /// The second picture order count difference for inter prediction.
    /// </summary>
    public int DeltaPicOrderCnt1;

    /// <summary>
    /// The count of redundant pictures in the slice.
    /// </summary>
    public uint RedundantPicCnt;

    /// <summary>
    /// Indicates whether direct spatial motion vector prediction is used.
    /// </summary>
    public bool DirectSpatialMvPredFlag;

    /// <summary>
    /// Indicates whether the number of active reference indices is overridden.
    /// </summary>
    public bool NumRefIdxActiveOverrideFlag;

    /// <summary>
    /// The number of active reference indices for list 0 minus 1.
    /// </summary>
    public uint NumRefIdxL0ActiveMinus1;

    /// <summary>
    /// The number of active reference indices for list 1 minus 1.
    /// </summary>
    public uint NumRefIdxL1ActiveMinus1;

    /// <summary>
    /// The reference picture list modification data.
    /// </summary>
    public RefPicListModification RefPicListModification;

    /// <summary>
    /// Indicates whether the base prediction weight table is used.
    /// </summary>
    public bool BasePredWeightTableFlag;

    /// <summary>
    /// The prediction weight table.
    /// </summary>
    public PredWeightTable? PredWeightTable;

    /// <summary>
    /// The decoded reference picture marking.
    /// </summary>
    public DecRefPicMarking? DecRefPicMarking;

    /// <summary>
    /// The decoded reference base picture marking.
    /// </summary>
    public SvcDecRefBasePicMarking? DecRefBasePicMarking;

    /// <summary>
    /// Indicates whether the reference base picture is stored.
    /// </summary>
    public bool StoreRefBasePicFlag;

    /// <summary>
    /// The CABAC (Context-Adaptive Binary Arithmetic Coding) initialization index.
    /// </summary>
    public uint CabacInitIdc;

    /// <summary>
    /// The slice QP (Quantization Parameter) delta.
    /// </summary>
    public int SliceQpDelta;

    /// <summary>
    /// Indicates whether deblocking is disabled for the slice.
    /// </summary>
    public uint DisableDeblockingFilterIdc;

    /// <summary>
    /// The alpha offset divided by 2 for the deblocking filter.
    /// </summary>
    public int SliceAlphaC0OffsetDiv2;

    /// <summary>
    /// The beta offset divided by 2 for the deblocking filter.
    /// </summary>
    public int SliceBetaOffsetDiv2;

    /// <summary>
    /// The slice group change cycle for FMO (Flexible Macroblock Ordering).
    /// </summary>
    public uint SliceGroupChangeCycle;

    /// <summary>
    /// The dependency quality ID of the reference layer.
    /// </summary>
    public uint RefLayerDqId;

    /// <summary>
    /// Indicates whether inter-layer deblocking is disabled.
    /// </summary>
    public uint DisableInterLayerDeblockingFilterIdc;

    /// <summary>
    /// The alpha offset divided by 2 for inter-layer deblocking.
    /// </summary>
    public int InterLayerSliceAlphaC0OffsetDiv2;

    /// <summary>
    /// The beta offset divided by 2 for inter-layer deblocking.
    /// </summary>
    public int InterLayerSliceBetaOffsetDiv2;

    /// <summary>
    /// Indicates whether constrained intra resampling is used.
    /// </summary>
    public bool ConstrainedIntraResamplingFlag;

    /// <summary>
    /// Indicates whether the chroma phase X offset is incremented by 1 for the reference layer.
    /// </summary>
    public bool RefLayerChromaPhaseXPlus1Flag;

    /// <summary>
    /// The chroma phase Y offset incremented by 1 for the reference layer.
    /// </summary>
    public uint RefLayerChromaPhaseYPlus1;

    /// <summary>
    /// The left offset for the scaled reference layer.
    /// </summary>
    public int ScaledRefLayerLeftOffset;

    /// <summary>
    /// The top offset for the scaled reference layer.
    /// </summary>
    public int ScaledRefLayerTopOffset;

    /// <summary>
    /// The right offset for the scaled reference layer.
    /// </summary>
    public int ScaledRefLayerRightOffset;

    /// <summary>
    /// The bottom offset for the scaled reference layer.
    /// </summary>
    public int ScaledRefLayerBottomOffset;

    /// <summary>
    /// Indicates whether slice skipping is enabled.
    /// </summary>
    public bool SliceSkipFlag;

    /// <summary>
    /// The number of macroblocks in the slice minus 1.
    /// </summary>
    public uint NumMbsInSliceMinus1;

    /// <summary>
    /// Indicates whether adaptive base mode is used.
    /// </summary>
    public bool AdaptiveBaseModeFlag;

    /// <summary>
    /// Indicates whether the default base mode is used.
    /// </summary>
    public bool DefaultBaseModeFlag;

    /// <summary>
    /// Indicates whether adaptive motion prediction is used.
    /// </summary>
    public bool AdaptiveMotionPredictionFlag;

    /// <summary>
    /// Indicates whether the default motion prediction is used.
    /// </summary>
    public bool DefaultMotionPredictionFlag;

    /// <summary>
    /// Indicates whether adaptive residual prediction is used.
    /// </summary>
    public bool AdaptiveResidualPredictionFlag;

    /// <summary>
    /// Indicates whether the default residual prediction is used.
    /// </summary>
    public bool DefaultResidualPredictionFlag;

    /// <summary>
    /// Indicates whether transform coefficient level prediction is used.
    /// </summary>
    public bool TCoeffLevelPredictionFlag;

    /// <summary>
    /// The starting scan index for the slice.
    /// </summary>
    public uint ScanIdxStart;

    /// <summary>
    /// The ending scan index for the slice.
    /// </summary>
    public uint ScanIdxEnd;

    /// <summary>
    /// Initializes a new instance of the <see cref="SvcSliceHeader"/> struct.
    /// </summary>
    /// <param name="firstMbInSlice">The macroblock address of the first macroblock in the slice.</param>
    /// <param name="sliceType">The type of the slice (e.g., I, P, B slice).</param>
    /// <param name="ppsId">The ID of the picture parameter set (PPS) used by this slice.</param>
    /// <param name="colorPlaneId">The color plane ID for slices in a 4:4:4 profile.</param>
    /// <param name="frameNum">The frame number of the current slice.</param>
    /// <param name="fieldPicFlag">Indicates whether the slice is part of a field picture.</param>
    /// <param name="bottomFieldFlag">Indicates whether the slice is part of the bottom field of a field picture.</param>
    /// <param name="idrPicId">The ID of the IDR (Instantaneous Decoding Refresh) picture.</param>
    /// <param name="picOrderCntLsb">The least significant bits of the picture order count.</param>
    /// <param name="deltaPicOrderCntBottom">The difference between the picture order count of the bottom field and the top field.</param>
    /// <param name="deltaPicOrderCnt0">The first picture order count difference for inter prediction.</param>
    /// <param name="deltaPicOrderCnt1">The second picture order count difference for inter prediction.</param>
    /// <param name="redundantPicCnt">The count of redundant pictures in the slice.</param>
    /// <param name="directSpatialMvPredFlag">Indicates whether direct spatial motion vector prediction is used.</param>
    /// <param name="numRefIdxActiveOverrideFlag">Indicates whether the number of active reference indices is overridden.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for list 0 minus 1.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for list 1 minus 1.</param>
    /// <param name="refPicListModification">The reference picture list modification data.</param>
    /// <param name="basePredWeightTableFlag">Indicates whether the base prediction weight table is used.</param>
    /// <param name="predWeightTable">Prediction weight table</param>
    /// <param name="decRefPicMarking">Decoded reference picture marking</param>
    /// <param name="decRefBasePicMarking">Decoded reference base picture marking</param>
    /// <param name="storeRefBasePicFlag">Indicates whether the reference base picture is stored.</param>
    /// <param name="cabacInitIdc">The CABAC (Context-Adaptive Binary Arithmetic Coding) initialization index.</param>
    /// <param name="sliceQpDelta">The slice QP (Quantization Parameter) delta.</param>
    /// <param name="disableDeblockingFilterIdc">Indicates whether deblocking is disabled for the slice.</param>
    /// <param name="sliceAlphaC0OffsetDiv2">The alpha offset divided by 2 for the deblocking filter.</param>
    /// <param name="sliceBetaOffsetDiv2">The beta offset divided by 2 for the deblocking filter.</param>
    /// <param name="sliceGroupChangeCycle">The slice group change cycle for FMO (Flexible Macroblock Ordering).</param>
    /// <param name="refLayerDqId">The dependency quality ID of the reference layer.</param>
    /// <param name="disableInterLayerDeblockingFilterIdc">Indicates whether inter-layer deblocking is disabled.</param>
    /// <param name="interLayerSliceAlphaC0OffsetDiv2">The alpha offset divided by 2 for inter-layer deblocking.</param>
    /// <param name="interLayerSliceBetaOffsetDiv2">The beta offset divided by 2 for inter-layer deblocking.</param>
    /// <param name="constrainedIntraResamplingFlag">Indicates whether constrained intra resampling is used.</param>
    /// <param name="refLayerChromaPhaseXPlus1Flag">Indicates whether the chroma phase X offset is incremented by 1 for the reference layer.</param>
    /// <param name="refLayerChromaPhaseYPlus1">The chroma phase Y offset incremented by 1 for the reference layer.</param>
    /// <param name="scaledRefLayerLeftOffset">The left offset for the scaled reference layer.</param>
    /// <param name="scaledRefLayerTopOffset">The top offset for the scaled reference layer.</param>
    /// <param name="scaledRefLayerRightOffset">The right offset for the scaled reference layer.</param>
    /// <param name="scaledRefLayerBottomOffset">The bottom offset for the scaled reference layer.</param>
    /// <param name="sliceSkipFlag">Indicates whether slice skipping is enabled.</param>
    /// <param name="numMbsInSliceMinus1">The number of macroblocks in the slice minus 1.</param>
    /// <param name="adaptiveBaseModeFlag">Indicates whether adaptive base mode is used.</param>
    /// <param name="defaultBaseModeFlag">Indicates whether the default base mode is used.</param>
    /// <param name="adaptiveMotionPredictionFlag">Indicates whether adaptive motion prediction is used.</param>
    /// <param name="defaultMotionPredictionFlag">Indicates whether the default motion prediction is used.</param>
    /// <param name="adaptiveResidualPredictionFlag">Indicates whether adaptive residual prediction is used.</param>
    /// <param name="defaultResidualPredictionFlag">Indicates whether the default residual prediction is used.</param>
    /// <param name="tCoeffLevelPredictionFlag">Indicates whether transform coefficient level prediction is used.</param>
    /// <param name="scanIdxStart">The starting scan index for the slice.</param>
    /// <param name="scanIdxEnd">The ending scan index for the slice.</param>
    public SvcSliceHeader(uint firstMbInSlice, uint sliceType, uint ppsId, uint colorPlaneId, uint frameNum, bool fieldPicFlag, bool bottomFieldFlag, uint idrPicId, uint picOrderCntLsb, int deltaPicOrderCntBottom, int deltaPicOrderCnt0, int deltaPicOrderCnt1, uint redundantPicCnt, bool directSpatialMvPredFlag, bool numRefIdxActiveOverrideFlag, uint numRefIdxL0ActiveMinus1, uint numRefIdxL1ActiveMinus1, RefPicListModification refPicListModification, bool basePredWeightTableFlag, PredWeightTable predWeightTable, DecRefPicMarking? decRefPicMarking, SvcDecRefBasePicMarking? decRefBasePicMarking, bool storeRefBasePicFlag, uint cabacInitIdc, int sliceQpDelta, uint disableDeblockingFilterIdc, int sliceAlphaC0OffsetDiv2, int sliceBetaOffsetDiv2, uint sliceGroupChangeCycle, uint refLayerDqId, uint disableInterLayerDeblockingFilterIdc, int interLayerSliceAlphaC0OffsetDiv2, int interLayerSliceBetaOffsetDiv2, bool constrainedIntraResamplingFlag, bool refLayerChromaPhaseXPlus1Flag, uint refLayerChromaPhaseYPlus1, int scaledRefLayerLeftOffset, int scaledRefLayerTopOffset, int scaledRefLayerRightOffset, int scaledRefLayerBottomOffset, bool sliceSkipFlag, uint numMbsInSliceMinus1, bool adaptiveBaseModeFlag, bool defaultBaseModeFlag, bool adaptiveMotionPredictionFlag, bool defaultMotionPredictionFlag, bool adaptiveResidualPredictionFlag, bool defaultResidualPredictionFlag, bool tCoeffLevelPredictionFlag, uint scanIdxStart, uint scanIdxEnd)
    {
        FirstMbInSlice = firstMbInSlice;
        SliceType = sliceType;
        PpsId = ppsId;
        ColorPlaneId = colorPlaneId;
        FrameNum = frameNum;
        FieldPicFlag = fieldPicFlag;
        BottomFieldFlag = bottomFieldFlag;
        IdrPicId = idrPicId;
        PicOrderCntLsb = picOrderCntLsb;
        DeltaPicOrderCntBottom = deltaPicOrderCntBottom;
        DeltaPicOrderCnt0 = deltaPicOrderCnt0;
        DeltaPicOrderCnt1 = deltaPicOrderCnt1;
        RedundantPicCnt = redundantPicCnt;
        DirectSpatialMvPredFlag = directSpatialMvPredFlag;
        NumRefIdxActiveOverrideFlag = numRefIdxActiveOverrideFlag;
        NumRefIdxL0ActiveMinus1 = numRefIdxL0ActiveMinus1;
        NumRefIdxL1ActiveMinus1 = numRefIdxL1ActiveMinus1;
        RefPicListModification = refPicListModification;
        BasePredWeightTableFlag = basePredWeightTableFlag;
        PredWeightTable = predWeightTable;
        DecRefPicMarking = decRefPicMarking;
        DecRefBasePicMarking = decRefBasePicMarking;
        StoreRefBasePicFlag = storeRefBasePicFlag;
        CabacInitIdc = cabacInitIdc;
        SliceQpDelta = sliceQpDelta;
        DisableDeblockingFilterIdc = disableDeblockingFilterIdc;
        SliceAlphaC0OffsetDiv2 = sliceAlphaC0OffsetDiv2;
        SliceBetaOffsetDiv2 = sliceBetaOffsetDiv2;
        SliceGroupChangeCycle = sliceGroupChangeCycle;
        RefLayerDqId = refLayerDqId;
        DisableInterLayerDeblockingFilterIdc = disableInterLayerDeblockingFilterIdc;
        InterLayerSliceAlphaC0OffsetDiv2 = interLayerSliceAlphaC0OffsetDiv2;
        InterLayerSliceBetaOffsetDiv2 = interLayerSliceBetaOffsetDiv2;
        ConstrainedIntraResamplingFlag = constrainedIntraResamplingFlag;
        RefLayerChromaPhaseXPlus1Flag = refLayerChromaPhaseXPlus1Flag;
        RefLayerChromaPhaseYPlus1 = refLayerChromaPhaseYPlus1;
        ScaledRefLayerLeftOffset = scaledRefLayerLeftOffset;
        ScaledRefLayerTopOffset = scaledRefLayerTopOffset;
        ScaledRefLayerRightOffset = scaledRefLayerRightOffset;
        ScaledRefLayerBottomOffset = scaledRefLayerBottomOffset;
        SliceSkipFlag = sliceSkipFlag;
        NumMbsInSliceMinus1 = numMbsInSliceMinus1;
        AdaptiveBaseModeFlag = adaptiveBaseModeFlag;
        DefaultBaseModeFlag = defaultBaseModeFlag;
        AdaptiveMotionPredictionFlag = adaptiveMotionPredictionFlag;
        DefaultMotionPredictionFlag = defaultMotionPredictionFlag;
        AdaptiveResidualPredictionFlag = adaptiveResidualPredictionFlag;
        DefaultResidualPredictionFlag = defaultResidualPredictionFlag;
        TCoeffLevelPredictionFlag = tCoeffLevelPredictionFlag;
        ScanIdxStart = scanIdxStart;
        ScanIdxEnd = scanIdxEnd;
    }

    /// <summary>
    /// Reads an SVC slice header from the bitstream.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <param name="separateColorPlaneFlag">Indicates whether separate color planes are used.</param>
    /// <param name="nalUnitType">The type of the NAL unit.</param>
    /// <param name="log2MaxFrameNumMinus4">The value of log2_max_frame_num_minus4 from the sequence parameter set.</param>
    /// <param name="frameMbsOnlyFlag">Indicates whether only frame macroblocks are used.</param>
    /// <param name="idrPicFlag">Indicates whether the current picture is an IDR picture.</param>
    /// <param name="picOrderCntType">The picture order count type.</param>
    /// <param name="deltaPicOrderAlwaysZeroFlag">Indicates whether delta_pic_order_cnt is always zero.</param>
    /// <param name="bottomFieldPicOrderInFramePresentFlag">Indicates whether bottom field picture order is present in the frame.</param>
    /// <param name="log2MaxPicOrderCntLsbMinus4">The value of log2_max_pic_order_cnt_lsb_minus4 from the sequence parameter set.</param>
    /// <param name="redundantPicCntPresentFlag">Indicates whether redundant picture count is present.</param>
    /// <param name="qualityId">The quality ID of the slice.</param>
    /// <param name="weightedPredFlag">Indicates whether weighted prediction is used.</param>
    /// <param name="weightedBiPredIdc">The weighted bi-prediction indicator.</param>
    /// <param name="noInterLayerPredFlag">Indicates whether inter-layer prediction is disabled.</param>
    /// <param name="nalRefIdc">The NAL reference indicator.</param>
    /// <param name="useRefBasePicFlag">Indicates whether the reference base picture is used.</param>
    /// <param name="sliceHeaderRestrictionFlag">Indicates whether slice header restrictions are applied.</param>
    /// <param name="entropyCodingModeFlag">Indicates whether entropy coding mode is enabled.</param>
    /// <param name="extendedSpatialScalabilityIdc">The extended spatial scalability indicator.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="deblockingFilterControlPresentFlag">Indicates whether deblocking filter control is present.</param>
    /// <param name="sliceGroupMapType">The slice group map type.</param>
    /// <param name="numSliceGroupsMinus1">The number of slice groups minus one.</param>
    /// <param name="picSizeInMapUnits">The size of the picture in map units.</param>
    /// <param name="sliceGroupChangeRate">The slice group change rate.</param>
    /// <param name="interLayerDeblockingFilterControlPresentFlag">Indicates whether inter-layer deblocking filter control is present.</param>
    /// <param name="adaptiveTCoeffLevelPredictionFlag">Indicates whether adaptive transform coefficient level prediction is used.</param>
    /// <returns>An instance of <see cref="SvcSliceHeader"/> representing the parsed slice header.</returns>
    /// <exception cref="InvalidDataException">Thrown if the bitstream contains invalid data.</exception>
    public static SvcSliceHeader Read(BitStreamReader reader, bool separateColorPlaneFlag, uint nalUnitType, uint log2MaxFrameNumMinus4, bool frameMbsOnlyFlag, bool idrPicFlag, uint picOrderCntType, bool deltaPicOrderAlwaysZeroFlag, bool bottomFieldPicOrderInFramePresentFlag, uint log2MaxPicOrderCntLsbMinus4, bool redundantPicCntPresentFlag, uint qualityId, bool weightedPredFlag, uint weightedBiPredIdc, bool noInterLayerPredFlag, uint nalRefIdc, bool useRefBasePicFlag, bool sliceHeaderRestrictionFlag, bool entropyCodingModeFlag, uint extendedSpatialScalabilityIdc, uint chromaArrayType, bool deblockingFilterControlPresentFlag, uint sliceGroupMapType, uint numSliceGroupsMinus1, uint picSizeInMapUnits, uint sliceGroupChangeRate, bool interLayerDeblockingFilterControlPresentFlag, bool adaptiveTCoeffLevelPredictionFlag)
    {
        uint firstMbInSlice = reader.ReadUE();
        uint sliceType = reader.ReadUE();
        uint ppsId = reader.ReadUE();

        uint colorPlaneId = 0u;
        if (separateColorPlaneFlag)
            colorPlaneId = reader.ReadBits(2);

        uint frameNum = reader.ReadBits(log2MaxFrameNumMinus4 + 4);

        bool fieldPicFlag = false;
        bool bottomFieldFlag = false;

        if (!frameMbsOnlyFlag)
        {
            fieldPicFlag = reader.ReadBit();
            if (fieldPicFlag)
                bottomFieldFlag = reader.ReadBit();
        }

        uint idrPicId = 0u;
        if (idrPicFlag)
            idrPicId = reader.ReadUE();

        uint picOrderCntLsb = 0u;
        int deltaPicOrderCntBottom = 0;

        if (picOrderCntType == 0u)
        {
            picOrderCntLsb = reader.ReadBits(log2MaxPicOrderCntLsbMinus4 + 4u);
            if (bottomFieldPicOrderInFramePresentFlag && !fieldPicFlag)
                deltaPicOrderCntBottom = reader.ReadSE();
        }

        int deltaPicOrderCnt0 = 0;
        int deltaPicOrderCnt1 = 0;

        if (picOrderCntType == 1 && !deltaPicOrderAlwaysZeroFlag)
        {
            deltaPicOrderCnt0 = reader.ReadSE();
            if (bottomFieldPicOrderInFramePresentFlag && !fieldPicFlag)
                deltaPicOrderCnt1 = reader.ReadSE();
        }

        uint redundantPicCnt = 0u;
        if (redundantPicCntPresentFlag)
            redundantPicCnt = reader.ReadUE();

        bool directSpatialMvPredFlag = false;
        bool numRefIdxActiveOverrideFlag = false;
        uint numRefIdxL0ActiveMinus1 = 0u;
        uint numRefIdxL1ActiveMinus1 = 0u;
        RefPicListModification modification = default;
        bool basePredWeightTableFlag = false;
        PredWeightTable? predWeightTable = null;
        DecRefPicMarking? decRefPicMarking = null;
        SvcDecRefBasePicMarking? decRefBasePicMarking = null;
        bool storeRefBasePicFlag = false;

        if (qualityId == 0u)
        {
            if (SliceTypes.IsEB(sliceType, nalUnitType))
                directSpatialMvPredFlag = reader.ReadBit();

            if (SliceTypes.IsEP(sliceType, nalUnitType) || SliceTypes.IsEB(sliceType, nalUnitType))
            {
                numRefIdxActiveOverrideFlag = reader.ReadBit();
                if (numRefIdxActiveOverrideFlag)
                {
                    numRefIdxL0ActiveMinus1 = reader.ReadUE();
                    if (SliceTypes.IsEB(sliceType, nalUnitType))
                    {
                        numRefIdxL1ActiveMinus1 = reader.ReadUE();
                    }
                }
            }

            modification = RefPicListModification.Read(reader, sliceType);

            if ((weightedPredFlag && SliceTypes.IsEP(sliceType, nalUnitType)) || (weightedBiPredIdc == 1 && SliceTypes.IsEB(sliceType, nalUnitType)))
            {
                if (!noInterLayerPredFlag)
                    basePredWeightTableFlag = reader.ReadBit();

                if (noInterLayerPredFlag || !basePredWeightTableFlag)
                    predWeightTable = H264.Models.PredWeightTable.Read(reader, (int)chromaArrayType, (int)sliceType, (int)numRefIdxL0ActiveMinus1, (int)numRefIdxL1ActiveMinus1);
            }

            if (nalRefIdc != 0u)
            {
                decRefPicMarking = H264.Models.DecRefPicMarking.Read(reader, idrPicFlag);
                if (!sliceHeaderRestrictionFlag)
                {
                    storeRefBasePicFlag = reader.ReadBit();
                    if ((useRefBasePicFlag || storeRefBasePicFlag) && !idrPicFlag)
                        decRefBasePicMarking = SvcDecRefBasePicMarking.Read(reader);
                }
            }
        }

        uint cabacInitIdc = 0u;
        if (entropyCodingModeFlag && !SliceTypes.IsEI(sliceType, nalUnitType))
            cabacInitIdc = reader.ReadUE();

        int sliceQpDelta = reader.ReadSE();

        uint disableDeblockingFilterIdc = 0u;
        int sliceAlphaC0OffsetDiv2 = 0;
        int sliceBetaOffsetDiv2 = 0;

        if (deblockingFilterControlPresentFlag)
        {
            disableDeblockingFilterIdc = reader.ReadUE();
            if (disableDeblockingFilterIdc != 1u)
            {
                sliceAlphaC0OffsetDiv2 = reader.ReadSE();
                sliceBetaOffsetDiv2 = reader.ReadSE();
            }
        }

        uint sliceGroupChangeCycle = 0u;
        if (numSliceGroupsMinus1 > 0 && sliceGroupMapType >= 3 && sliceGroupMapType <= 5)
            sliceGroupChangeCycle = reader.ReadBits((uint)Math.Ceiling(Math.Log2(picSizeInMapUnits / sliceGroupChangeRate + 1)));

        uint refLayerDqId = 0u;
        uint disableInterLayerDeblockingFilterIdc = 0u;
        int interLayerSliceAlphaC0OffsetDiv2 = 0;
        int interLayerSliceBetaOffsetDiv2 = 0;
        bool constrainedIntraResamplingFlag = false;
        bool refLayerChromaPhaseXPlus1Flag = false;
        uint refLayerChromaPhaseYPlus1 = 0u;
        int scaledRefLayerLeftOffset = 0;
        int scaledRefLayerTopOffset = 0;
        int scaledRefLayerRightOffset = 0;
        int scaledRefLayerBottomOffset = 0;

        if (!noInterLayerPredFlag && qualityId == 0u)
        {
            refLayerDqId = reader.ReadUE();
            if (interLayerDeblockingFilterControlPresentFlag)
            {
                disableInterLayerDeblockingFilterIdc = reader.ReadUE();
                if (disableInterLayerDeblockingFilterIdc != 1u)
                {
                    interLayerSliceAlphaC0OffsetDiv2 = reader.ReadSE();
                    interLayerSliceBetaOffsetDiv2 = reader.ReadSE();
                }
            }

            constrainedIntraResamplingFlag = reader.ReadBit();

            if (extendedSpatialScalabilityIdc == 2u)
            {
                if (chromaArrayType > 0)
                {
                    refLayerChromaPhaseXPlus1Flag = reader.ReadBit();
                    refLayerChromaPhaseYPlus1 = reader.ReadBits(2);
                }

                scaledRefLayerLeftOffset = reader.ReadSE();
                scaledRefLayerTopOffset = reader.ReadSE();
                scaledRefLayerRightOffset = reader.ReadSE();
                scaledRefLayerBottomOffset = reader.ReadSE();
            }
        }

        bool sliceSkipFlag = false;
        uint numMbsInSliceMinus1 = 0u;
        bool adaptiveBaseModeFlag = false;
        bool defaultBaseModeFlag = false;
        bool adaptiveMotionPredictionFlag = false;
        bool defaultMotionPredictionFlag = false;
        bool adaptiveResidualPredictionFlag = false;
        bool defaultResidualPredictionFlag = false;
        bool tCoeffLevelPredictionFlag = false;

        if (!noInterLayerPredFlag)
        {
            sliceSkipFlag = reader.ReadBit();
            if (sliceSkipFlag)
            {
                numMbsInSliceMinus1 = reader.ReadUE();
            }
            else
            {
                adaptiveBaseModeFlag = reader.ReadBit();
                if (!adaptiveBaseModeFlag)
                    defaultBaseModeFlag = reader.ReadBit();

                if (!defaultBaseModeFlag)
                {
                    adaptiveMotionPredictionFlag = reader.ReadBit();
                    if (!adaptiveMotionPredictionFlag)
                        defaultMotionPredictionFlag = reader.ReadBit();
                }

                adaptiveResidualPredictionFlag = reader.ReadBit();
                if (!adaptiveResidualPredictionFlag)
                    defaultResidualPredictionFlag = reader.ReadBit();
            }

            if (adaptiveTCoeffLevelPredictionFlag)
                tCoeffLevelPredictionFlag = reader.ReadBit();
        }

        uint scanIdxStart = 0u;
        uint scanIdxEnd = 0u;

        if (!sliceHeaderRestrictionFlag && !sliceSkipFlag)
        {
            scanIdxStart = reader.ReadBits(4);
            scanIdxEnd = reader.ReadBits(4);
        }

        return new SvcSliceHeader(
            firstMbInSlice,
            sliceType,
            ppsId,
            colorPlaneId,
            frameNum,
            fieldPicFlag,
            bottomFieldFlag,
            idrPicId,
            picOrderCntLsb,
            deltaPicOrderCntBottom,
            deltaPicOrderCnt0,
            deltaPicOrderCnt1,
            redundantPicCnt,
            directSpatialMvPredFlag,
            numRefIdxActiveOverrideFlag,
            numRefIdxL0ActiveMinus1,
            numRefIdxL1ActiveMinus1,
            modification,
            basePredWeightTableFlag,
            predWeightTable ?? default,
            decRefPicMarking,
            decRefBasePicMarking,
            storeRefBasePicFlag,
            cabacInitIdc,
            sliceQpDelta,
            disableDeblockingFilterIdc,
            sliceAlphaC0OffsetDiv2,
            sliceBetaOffsetDiv2,
            sliceGroupChangeCycle,
            refLayerDqId,
            disableInterLayerDeblockingFilterIdc,
            interLayerSliceAlphaC0OffsetDiv2,
            interLayerSliceBetaOffsetDiv2,
            constrainedIntraResamplingFlag,
            refLayerChromaPhaseXPlus1Flag,
            refLayerChromaPhaseYPlus1,
            scaledRefLayerLeftOffset,
            scaledRefLayerTopOffset,
            scaledRefLayerRightOffset,
            scaledRefLayerBottomOffset,
            sliceSkipFlag,
            numMbsInSliceMinus1,
            adaptiveBaseModeFlag,
            defaultBaseModeFlag,
            adaptiveMotionPredictionFlag,
            defaultMotionPredictionFlag,
            adaptiveResidualPredictionFlag,
            defaultResidualPredictionFlag,
            tCoeffLevelPredictionFlag,
            scanIdxStart,
            scanIdxEnd);
    }

    /// <summary>
    ///   Reads the SVC Slice Header.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="svcSPS"></param>
    /// <param name="sps"></param>
    /// <param name="pps"></param>
    /// <param name="nalu"></param>
    /// <param name="qualityId"></param>
    /// <param name="noInterLayerPredFlag"></param>
    /// <param name="useRefBasePicFlag"></param>
    /// <returns></returns>
    public static SvcSliceHeader Read(BitStreamReader reader, SvcSequenceParameterSet svcSPS, SequenceParameterSet sps, PictureParameterSet pps, NalUnit nalu, uint qualityId, bool noInterLayerPredFlag, bool useRefBasePicFlag)
        => Read(reader, sps.SeparateColourPlaneFlag, nalu.NalUnitType, sps.Log2MaxFrameNumMinus4, sps.FrameMbsOnlyFlag, nalu.IsIdr(), sps.PicOrderCntType, sps.DeltaPicOrderAlwaysZeroFlag, pps.BottomFieldPicOrderInFramePresentFlag, sps.Log2MaxPicOrderCntLsbMinus4, pps.RedundantPicCntPresentFlag, qualityId, pps.WeightedPredFlag, pps.WeightedBiPredIdc, noInterLayerPredFlag, nalu.NalRefIdc, useRefBasePicFlag, svcSPS.SliceHeaderRestrictionFlag, pps.EntropyCodingModeFlag, svcSPS.ExtendedSpatialScalabilityIdc, sps.GetChromaArrayType(), pps.DeblockingFilterControlPresentFlag, pps.SliceGroupMapType, pps.NumSliceGroupsMinus1, (uint)sps.GetPicSizeInMapUnits(), pps.SliceGroupChangeRateMinus1 + 1u, svcSPS.InterLayerDeblockingFilterControlPresentFlag, svcSPS.AdaptiveTCoeffLevelPredictionFlag);

    /// <summary>
    /// Writes the SVC slice header to the bitstream.
    /// </summary>
    /// <param name="writer">The bitstream writer to write the slice header to.</param>
    /// <param name="originalReader">The original bitstream reader used to parse the slice header.</param>
    /// <param name="separateColorPlaneFlag">Indicates whether separate color planes are used.</param>
    /// <param name="nalUnitType">The type of the NAL unit.</param>
    /// <param name="log2MaxFrameNumMinus4">The value of log2_max_frame_num_minus4 from the sequence parameter set.</param>
    /// <param name="frameMbsOnlyFlag">Indicates whether only frame macroblocks are used.</param>
    /// <param name="idrPicFlag">Indicates whether the current picture is an IDR picture.</param>
    /// <param name="picOrderCntType">The picture order count type.</param>
    /// <param name="deltaPicOrderAlwaysZeroFlag">Indicates whether delta_pic_order_cnt is always zero.</param>
    /// <param name="bottomFieldPicOrderInFramePresentFlag">Indicates whether bottom field picture order is present in the frame.</param>
    /// <param name="log2MaxPicOrderCntLsbMinus4">The value of log2_max_pic_order_cnt_lsb_minus4 from the sequence parameter set.</param>
    /// <param name="redundantPicCntPresentFlag">Indicates whether redundant picture count is present.</param>
    /// <param name="qualityId">The quality ID of the slice.</param>
    /// <param name="weightedPredFlag">Indicates whether weighted prediction is used.</param>
    /// <param name="weightedBiPredIdc">The weighted bi-prediction indicator.</param>
    /// <param name="noInterLayerPredFlag">Indicates whether inter-layer prediction is disabled.</param>
    /// <param name="nalRefIdc">The NAL reference indicator.</param>
    /// <param name="useRefBasePicFlag">Indicates whether the reference base picture is used.</param>
    /// <param name="sliceHeaderRestrictionFlag">Indicates whether slice header restrictions are applied.</param>
    /// <param name="entropyCodingModeFlag">Indicates whether entropy coding mode is enabled.</param>
    /// <param name="extendedSpatialScalabilityIdc">The extended spatial scalability indicator.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="deblockingFilterControlPresentFlag">Indicates whether deblocking filter control is present.</param>
    /// <param name="sliceGroupMapType">The slice group map type.</param>
    /// <param name="numSliceGroupsMinus1">The number of slice groups minus one.</param>
    /// <param name="picSizeInMapUnits">The size of the picture in map units.</param>
    /// <param name="sliceGroupChangeRate">The slice group change rate.</param>
    /// <param name="interLayerDeblockingFilterControlPresentFlag">Indicates whether inter-layer deblocking filter control is present.</param>
    /// <param name="adaptiveTCoeffLevelPredictionFlag">Indicates whether adaptive transform coefficient level prediction is used.</param>
    public readonly void Write(BitStreamWriter writer, BitStreamReader originalReader, bool separateColorPlaneFlag, uint nalUnitType, uint log2MaxFrameNumMinus4, bool frameMbsOnlyFlag, bool idrPicFlag, uint picOrderCntType, bool deltaPicOrderAlwaysZeroFlag, bool bottomFieldPicOrderInFramePresentFlag, uint log2MaxPicOrderCntLsbMinus4, bool redundantPicCntPresentFlag, uint qualityId, bool weightedPredFlag, uint weightedBiPredIdc, bool noInterLayerPredFlag, uint nalRefIdc, bool useRefBasePicFlag, bool sliceHeaderRestrictionFlag, bool entropyCodingModeFlag, uint extendedSpatialScalabilityIdc, uint chromaArrayType, bool deblockingFilterControlPresentFlag, uint sliceGroupMapType, uint numSliceGroupsMinus1, uint picSizeInMapUnits, uint sliceGroupChangeRate, bool interLayerDeblockingFilterControlPresentFlag, bool adaptiveTCoeffLevelPredictionFlag)
    {
        writer.WriteUE(FirstMbInSlice);
        writer.WriteUE(SliceType);
        writer.WriteUE(PpsId);

        if (separateColorPlaneFlag)
            writer.WriteBits(ColorPlaneId, 2);

        writer.WriteBits(FrameNum, log2MaxFrameNumMinus4 + 4);

        if (!frameMbsOnlyFlag)
        {
            writer.WriteBit(FieldPicFlag);
            if (FieldPicFlag)
                writer.WriteBit(BottomFieldFlag);
        }

        if (idrPicFlag)
            writer.WriteUE(IdrPicId);

        if (picOrderCntType == 0u)
        {
            writer.WriteBits(PicOrderCntLsb, log2MaxPicOrderCntLsbMinus4 + 4u);
            if (bottomFieldPicOrderInFramePresentFlag && !FieldPicFlag)
                writer.WriteSE(DeltaPicOrderCntBottom);
        }

        if (picOrderCntType == 1 && !deltaPicOrderAlwaysZeroFlag)
        {
            writer.WriteSE(DeltaPicOrderCnt0);
            if (bottomFieldPicOrderInFramePresentFlag && !FieldPicFlag)
                writer.WriteSE(DeltaPicOrderCnt1);
        }

        if (redundantPicCntPresentFlag)
            writer.WriteUE(RedundantPicCnt);

        if (qualityId == 0u)
        {
            if (SliceTypes.IsEB(SliceType, nalUnitType))
                writer.WriteBit(DirectSpatialMvPredFlag);

            if (SliceTypes.IsEP(SliceType, nalUnitType) || SliceTypes.IsEB(SliceType, nalUnitType))
            {
                writer.WriteBit(NumRefIdxActiveOverrideFlag);
                if (NumRefIdxActiveOverrideFlag)
                {
                    writer.WriteUE(NumRefIdxL0ActiveMinus1);
                    if (SliceTypes.IsEB(SliceType, nalUnitType))
                    {
                        writer.WriteUE(NumRefIdxL1ActiveMinus1);
                    }
                }
            }

            Span<RefPicListModificationEntry> entries = stackalloc RefPicListModificationEntry[RefPicListModification.NumberOfElements];
            RefPicListModification.Write(writer, entries, SliceType);

            if ((weightedPredFlag && SliceTypes.IsEP(SliceType, nalUnitType)) || (weightedBiPredIdc == 1 && SliceTypes.IsEB(SliceType, nalUnitType)))
            {
                if (!noInterLayerPredFlag)
                    writer.WriteBit(BasePredWeightTableFlag);

                if (noInterLayerPredFlag || !BasePredWeightTableFlag)
                {
                    Span<PredWeightTableWeightOffsetEntry> lumaL0 = stackalloc PredWeightTableWeightOffsetEntry[32];
                    Span<(PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)> chromaL0 = stackalloc (PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)[32];

                    for (int i = 0; i < this.PredWeightTable!.Value.L0.Count; i++)
                    {
                        var (luma, chroma1, chroma2) = this.PredWeightTable!.Value.L0.GetElement(originalReader, i, (int)chromaArrayType);
                        lumaL0[i] = luma;
                        chromaL0[i] = (chroma1, chroma2);
                    }

                    Span<PredWeightTableWeightOffsetEntry> lumaL1 = stackalloc PredWeightTableWeightOffsetEntry[32];
                    Span<(PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)> chromaL1 = stackalloc (PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)[32];

                    if (this.PredWeightTable!.Value.L1 is not null)
                    {
                        for (int i = 0; i < this.PredWeightTable!.Value.L1!.Value.Count; i++)
                        {
                            var (luma, chroma1, chroma2) = this.PredWeightTable!.Value.L1!.Value.GetElement(originalReader, i, (int)chromaArrayType);
                            lumaL0[i] = luma;
                            chromaL0[i] = (chroma1, chroma2);
                        }
                    }

                    Span<bool> includesL0 = stackalloc bool[32];
                    Span<bool> includesL1 = stackalloc bool[32];

                    for (int i = 0; i < this.PredWeightTable!.Value.L0.Count; i++)
                        includesL0[i] = true;

                    if (this.PredWeightTable!.Value.L1 is not null)
                        for (int i = 0; i < this.PredWeightTable!.Value.L1!.Value.Count; i++)
                            includesL1[i] = true;

                    var l0Options = new PredWeightTableListWriteOptions(includesL0, includesL0, lumaL0, chromaL0);
                    var l1Options = new PredWeightTableListWriteOptions(includesL1, includesL1, lumaL1, chromaL1);

                    this.PredWeightTable!.Value.Write(writer, (int)chromaArrayType, (int)SliceType, l0Options, l1Options);
                }
            }

            if (nalRefIdc != 0u)
            {
                Span<DecRefPicMarkingEntry> entriesCnt = stackalloc DecRefPicMarkingEntry[this.DecRefPicMarking!.Value.EntryCount];
                for (int i = 0; i < this.DecRefPicMarking!.Value.EntryCount; i++)
                    entriesCnt[i] = this.DecRefPicMarking!.Value.GetEntry(originalReader, i);

                this.DecRefPicMarking.Value.Write(writer, idrPicFlag, entriesCnt);
                if (!sliceHeaderRestrictionFlag)
                {
                    writer.WriteBit(StoreRefBasePicFlag);
                    if ((useRefBasePicFlag || StoreRefBasePicFlag) && !idrPicFlag)
                        this.DecRefBasePicMarking!.Value.Write(writer, originalReader);
                }
            }
        }

        if (entropyCodingModeFlag && !SliceTypes.IsEI(SliceType, nalUnitType))
            writer.WriteUE(CabacInitIdc);

        writer.WriteSE(SliceQpDelta);

        if (deblockingFilterControlPresentFlag)
        {
            writer.WriteUE(DisableDeblockingFilterIdc);
            if (DisableDeblockingFilterIdc != 1u)
            {
                writer.WriteSE(SliceAlphaC0OffsetDiv2);
                writer.WriteSE(SliceBetaOffsetDiv2);
            }
        }

        if (numSliceGroupsMinus1 > 0 && sliceGroupMapType >= 3 && sliceGroupMapType <= 5)
            writer.WriteBits(SliceGroupChangeCycle, (uint)Math.Ceiling(Math.Log2(picSizeInMapUnits / sliceGroupChangeRate + 1)));

        if (!noInterLayerPredFlag && qualityId == 0u)
        {
            writer.WriteUE(RefLayerDqId);
            if (interLayerDeblockingFilterControlPresentFlag)
            {
                writer.WriteUE(DisableInterLayerDeblockingFilterIdc);
                if (DisableInterLayerDeblockingFilterIdc != 1u)
                {
                    writer.WriteSE(InterLayerSliceAlphaC0OffsetDiv2);
                    writer.WriteSE(InterLayerSliceBetaOffsetDiv2);
                }
            }

            writer.WriteBit(ConstrainedIntraResamplingFlag);

            if (extendedSpatialScalabilityIdc == 2u)
            {
                if (chromaArrayType > 0)
                {
                    writer.WriteBit(RefLayerChromaPhaseXPlus1Flag);
                    writer.WriteBits(RefLayerChromaPhaseYPlus1, 2);
                }

                writer.WriteSE(ScaledRefLayerLeftOffset);
                writer.WriteSE(ScaledRefLayerTopOffset);
                writer.WriteSE(ScaledRefLayerRightOffset);
                writer.WriteSE(ScaledRefLayerBottomOffset);
            }
        }

        if (!noInterLayerPredFlag)
        {
            writer.WriteBit(SliceSkipFlag);
            if (SliceSkipFlag)
            {
                writer.WriteUE(NumMbsInSliceMinus1);
            }
            else
            {
                writer.WriteBit(AdaptiveBaseModeFlag);
                if (!AdaptiveBaseModeFlag)
                    writer.WriteBit(DefaultBaseModeFlag);

                if (!DefaultBaseModeFlag)
                {
                    writer.WriteBit(AdaptiveMotionPredictionFlag);
                    if (!AdaptiveMotionPredictionFlag)
                        writer.WriteBit(DefaultMotionPredictionFlag);
                }

                writer.WriteBit(AdaptiveResidualPredictionFlag);
                if (!AdaptiveResidualPredictionFlag)
                    writer.WriteBit(DefaultResidualPredictionFlag);
            }

            if (adaptiveTCoeffLevelPredictionFlag)
                writer.WriteBit(TCoeffLevelPredictionFlag);
        }

        if (!sliceHeaderRestrictionFlag && !SliceSkipFlag)
        {
            writer.WriteBits(ScanIdxStart, 4);
            writer.WriteBits(ScanIdxEnd, 4);
        }
    }

    /// <summary>
    /// Writes the SVC slice header to the bitstream.
    /// </summary>
    /// <param name="writer">The bitstream writer to write the slice header to.</param>
    /// <param name="originalReader">The original bitstream reader used to parse the slice header.</param>
    /// <param name="separateColorPlaneFlag">Indicates whether separate color planes are used.</param>
    /// <param name="nalUnitType">The type of the NAL unit.</param>
    /// <param name="log2MaxFrameNumMinus4">The value of log2_max_frame_num_minus4 from the sequence parameter set.</param>
    /// <param name="frameMbsOnlyFlag">Indicates whether only frame macroblocks are used.</param>
    /// <param name="idrPicFlag">Indicates whether the current picture is an IDR picture.</param>
    /// <param name="picOrderCntType">The picture order count type.</param>
    /// <param name="deltaPicOrderAlwaysZeroFlag">Indicates whether delta_pic_order_cnt is always zero.</param>
    /// <param name="bottomFieldPicOrderInFramePresentFlag">Indicates whether bottom field picture order is present in the frame.</param>
    /// <param name="log2MaxPicOrderCntLsbMinus4">The value of log2_max_pic_order_cnt_lsb_minus4 from the sequence parameter set.</param>
    /// <param name="redundantPicCntPresentFlag">Indicates whether redundant picture count is present.</param>
    /// <param name="qualityId">The quality ID of the slice.</param>
    /// <param name="weightedPredFlag">Indicates whether weighted prediction is used.</param>
    /// <param name="weightedBiPredIdc">The weighted bi-prediction indicator.</param>
    /// <param name="noInterLayerPredFlag">Indicates whether inter-layer prediction is disabled.</param>
    /// <param name="nalRefIdc">The NAL reference indicator.</param>
    /// <param name="useRefBasePicFlag">Indicates whether the reference base picture is used.</param>
    /// <param name="sliceHeaderRestrictionFlag">Indicates whether slice header restrictions are applied.</param>
    /// <param name="entropyCodingModeFlag">Indicates whether entropy coding mode is enabled.</param>
    /// <param name="extendedSpatialScalabilityIdc">The extended spatial scalability indicator.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="deblockingFilterControlPresentFlag">Indicates whether deblocking filter control is present.</param>
    /// <param name="sliceGroupMapType">The slice group map type.</param>
    /// <param name="numSliceGroupsMinus1">The number of slice groups minus one.</param>
    /// <param name="picSizeInMapUnits">The size of the picture in map units.</param>
    /// <param name="sliceGroupChangeRate">The slice group change rate.</param>
    /// <param name="interLayerDeblockingFilterControlPresentFlag">Indicates whether inter-layer deblocking filter control is present.</param>
    /// <param name="adaptiveTCoeffLevelPredictionFlag">Indicates whether adaptive transform coefficient level prediction is used.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, BitStreamReader originalReader, bool separateColorPlaneFlag, uint nalUnitType, uint log2MaxFrameNumMinus4, bool frameMbsOnlyFlag, bool idrPicFlag, uint picOrderCntType, bool deltaPicOrderAlwaysZeroFlag, bool bottomFieldPicOrderInFramePresentFlag, uint log2MaxPicOrderCntLsbMinus4, bool redundantPicCntPresentFlag, uint qualityId, bool weightedPredFlag, uint weightedBiPredIdc, bool noInterLayerPredFlag, uint nalRefIdc, bool useRefBasePicFlag, bool sliceHeaderRestrictionFlag, bool entropyCodingModeFlag, uint extendedSpatialScalabilityIdc, uint chromaArrayType, bool deblockingFilterControlPresentFlag, uint sliceGroupMapType, uint numSliceGroupsMinus1, uint picSizeInMapUnits, uint sliceGroupChangeRate, bool interLayerDeblockingFilterControlPresentFlag, bool adaptiveTCoeffLevelPredictionFlag)
    {
        await writer.WriteUEAsync(FirstMbInSlice);
        await writer.WriteUEAsync(SliceType);
        await writer.WriteUEAsync(PpsId);

        if (separateColorPlaneFlag)
            await writer.WriteBitsAsync(ColorPlaneId, 2);

        await writer.WriteBitsAsync(FrameNum, log2MaxFrameNumMinus4 + 4);

        if (!frameMbsOnlyFlag)
        {
            await writer.WriteBitAsync(FieldPicFlag);
            if (FieldPicFlag)
                await writer.WriteBitAsync(BottomFieldFlag);
        }

        if (idrPicFlag)
            await writer.WriteUEAsync(IdrPicId);

        if (picOrderCntType == 0u)
        {
            await writer.WriteBitsAsync(PicOrderCntLsb, log2MaxPicOrderCntLsbMinus4 + 4u);
            if (bottomFieldPicOrderInFramePresentFlag && !FieldPicFlag)
                await writer.WriteSEAsync(DeltaPicOrderCntBottom);
        }

        if (picOrderCntType == 1 && !deltaPicOrderAlwaysZeroFlag)
        {
            await writer.WriteSEAsync(DeltaPicOrderCnt0);
            if (bottomFieldPicOrderInFramePresentFlag && !FieldPicFlag)
                await writer.WriteSEAsync(DeltaPicOrderCnt1);
        }

        if (redundantPicCntPresentFlag)
            await writer.WriteUEAsync(RedundantPicCnt);

        if (qualityId == 0u)
        {
            if (SliceTypes.IsEB(SliceType, nalUnitType))
                await writer.WriteBitAsync(DirectSpatialMvPredFlag);

            if (SliceTypes.IsEP(SliceType, nalUnitType) || SliceTypes.IsEB(SliceType, nalUnitType))
            {
                await writer.WriteBitAsync(NumRefIdxActiveOverrideFlag);
                if (NumRefIdxActiveOverrideFlag)
                {
                    await writer.WriteUEAsync(NumRefIdxL0ActiveMinus1);
                    if (SliceTypes.IsEB(SliceType, nalUnitType))
                    {
                        await writer.WriteUEAsync(NumRefIdxL1ActiveMinus1);
                    }
                }
            }

            Memory<RefPicListModificationEntry> entries = new(new RefPicListModificationEntry[RefPicListModification.NumberOfElements]);
            RefPicListModification.Write(writer, entries.Span, SliceType);

            if ((weightedPredFlag && SliceTypes.IsEP(SliceType, nalUnitType)) || (weightedBiPredIdc == 1 && SliceTypes.IsEB(SliceType, nalUnitType)))
            {
                if (!noInterLayerPredFlag)
                    await writer.WriteBitAsync(BasePredWeightTableFlag);

                if (noInterLayerPredFlag || !BasePredWeightTableFlag)
                {
                    Memory<PredWeightTableWeightOffsetEntry> lumaL0 = new(new PredWeightTableWeightOffsetEntry[32]);
                    Memory<(PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)> chromaL0 = new(new (PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)[32]);

                    for (int i = 0; i < this.PredWeightTable!.Value.L0.Count; i++)
                    {
                        var (luma, chroma1, chroma2) = this.PredWeightTable!.Value.L0.GetElement(originalReader, i, (int)chromaArrayType);
                        lumaL0.Span[i] = luma;
                        chromaL0.Span[i] = (chroma1, chroma2);
                    }

                    Memory<PredWeightTableWeightOffsetEntry> lumaL1 = new(new PredWeightTableWeightOffsetEntry[32]);
                    Memory<(PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)> chromaL1 = new(new (PredWeightTableWeightOffsetEntry, PredWeightTableWeightOffsetEntry)[32]);

                    if (this.PredWeightTable!.Value.L1 is not null)
                    {
                        for (int i = 0; i < this.PredWeightTable!.Value.L1!.Value.Count; i++)
                        {
                            var (luma, chroma1, chroma2) = this.PredWeightTable!.Value.L1!.Value.GetElement(originalReader, i, (int)chromaArrayType);
                            lumaL0.Span[i] = luma;
                            chromaL0.Span[i] = (chroma1, chroma2);
                        }
                    }

                    Memory<bool> includesL0 = new(new bool[32]);
                    Memory<bool> includesL1 = new(new bool[32]);

                    for (int i = 0; i < this.PredWeightTable!.Value.L0.Count; i++)
                        includesL0.Span[i] = true;

                    if (this.PredWeightTable!.Value.L1 is not null)
                        for (int i = 0; i < this.PredWeightTable!.Value.L1!.Value.Count; i++)
                            includesL1.Span[i] = true;

                    var l0Options = new MemoryPredWeightTableListWriteOptions(includesL0, includesL0, lumaL0, chromaL0);
                    var l1Options = new MemoryPredWeightTableListWriteOptions(includesL1, includesL1, lumaL1, chromaL1);

                    await this.PredWeightTable!.Value.WriteAsync(writer, (int)chromaArrayType, (int)SliceType, l0Options, l1Options);
                }
            }

            if (nalRefIdc != 0u)
            {
                Memory<DecRefPicMarkingEntry> entriesCnt = new(new DecRefPicMarkingEntry[this.DecRefPicMarking!.Value.EntryCount]);
                for (int i = 0; i < this.DecRefPicMarking!.Value.EntryCount; i++)
                    entriesCnt.Span[i] = this.DecRefPicMarking!.Value.GetEntry(originalReader, i);

                await this.DecRefPicMarking.Value.WriteAsync(writer, idrPicFlag, entriesCnt);
                if (!sliceHeaderRestrictionFlag)
                {
                    await writer.WriteBitAsync(StoreRefBasePicFlag);
                    if ((useRefBasePicFlag || StoreRefBasePicFlag) && !idrPicFlag)
                        await this.DecRefBasePicMarking!.Value.WriteAsync(writer, originalReader);
                }
            }
        }

        if (entropyCodingModeFlag && !SliceTypes.IsEI(SliceType, nalUnitType))
            await writer.WriteUEAsync(CabacInitIdc);

        await writer.WriteSEAsync(SliceQpDelta);

        if (deblockingFilterControlPresentFlag)
        {
            await writer.WriteUEAsync(DisableDeblockingFilterIdc);
            if (DisableDeblockingFilterIdc != 1u)
            {
                await writer.WriteSEAsync(SliceAlphaC0OffsetDiv2);
                await writer.WriteSEAsync(SliceBetaOffsetDiv2);
            }
        }

        if (numSliceGroupsMinus1 > 0 && sliceGroupMapType >= 3 && sliceGroupMapType <= 5)
            await writer.WriteBitsAsync(SliceGroupChangeCycle, (uint)Math.Ceiling(Math.Log2(picSizeInMapUnits / sliceGroupChangeRate + 1)));

        if (!noInterLayerPredFlag && qualityId == 0u)
        {
            await writer.WriteUEAsync(RefLayerDqId);
            if (interLayerDeblockingFilterControlPresentFlag)
            {
                await writer.WriteUEAsync(DisableInterLayerDeblockingFilterIdc);
                if (DisableInterLayerDeblockingFilterIdc != 1u)
                {
                    await writer.WriteSEAsync(InterLayerSliceAlphaC0OffsetDiv2);
                    await writer.WriteSEAsync(InterLayerSliceBetaOffsetDiv2);
                }
            }

            await writer.WriteBitAsync(ConstrainedIntraResamplingFlag);

            if (extendedSpatialScalabilityIdc == 2u)
            {
                if (chromaArrayType > 0)
                {
                    await writer.WriteBitAsync(RefLayerChromaPhaseXPlus1Flag);
                    await writer.WriteBitsAsync(RefLayerChromaPhaseYPlus1, 2);
                }

                await writer.WriteSEAsync(ScaledRefLayerLeftOffset);
                await writer.WriteSEAsync(ScaledRefLayerTopOffset);
                await writer.WriteSEAsync(ScaledRefLayerRightOffset);
                await writer.WriteSEAsync(ScaledRefLayerBottomOffset);
            }
        }

        if (!noInterLayerPredFlag)
        {
            await writer.WriteBitAsync(SliceSkipFlag);
            if (SliceSkipFlag)
            {
                await writer.WriteUEAsync(NumMbsInSliceMinus1);
            }
            else
            {
                await writer.WriteBitAsync(AdaptiveBaseModeFlag);
                if (!AdaptiveBaseModeFlag)
                    await writer.WriteBitAsync(DefaultBaseModeFlag);

                if (!DefaultBaseModeFlag)
                {
                    await writer.WriteBitAsync(AdaptiveMotionPredictionFlag);
                    if (!AdaptiveMotionPredictionFlag)
                        await writer.WriteBitAsync(DefaultMotionPredictionFlag);
                }

                await writer.WriteBitAsync(AdaptiveResidualPredictionFlag);
                if (!AdaptiveResidualPredictionFlag)
                    await writer.WriteBitAsync(DefaultResidualPredictionFlag);
            }

            if (adaptiveTCoeffLevelPredictionFlag)
                await writer.WriteBitAsync(TCoeffLevelPredictionFlag);
        }

        if (!sliceHeaderRestrictionFlag && !SliceSkipFlag)
        {
            await writer.WriteBitsAsync(ScanIdxStart, 4);
            await writer.WriteBitsAsync(ScanIdxEnd, 4);
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is SvcSliceHeader header && Equals(header);
    }

    /// <inheritdoc/>
    public readonly bool Equals(SvcSliceHeader other)
    {
        return FirstMbInSlice == other.FirstMbInSlice &&
               SliceType == other.SliceType &&
               PpsId == other.PpsId &&
               ColorPlaneId == other.ColorPlaneId &&
               FrameNum == other.FrameNum &&
               FieldPicFlag == other.FieldPicFlag &&
               BottomFieldFlag == other.BottomFieldFlag &&
               IdrPicId == other.IdrPicId &&
               PicOrderCntLsb == other.PicOrderCntLsb &&
               DeltaPicOrderCntBottom == other.DeltaPicOrderCntBottom &&
               DeltaPicOrderCnt0 == other.DeltaPicOrderCnt0 &&
               DeltaPicOrderCnt1 == other.DeltaPicOrderCnt1 &&
               RedundantPicCnt == other.RedundantPicCnt &&
               DirectSpatialMvPredFlag == other.DirectSpatialMvPredFlag &&
               NumRefIdxActiveOverrideFlag == other.NumRefIdxActiveOverrideFlag &&
               NumRefIdxL0ActiveMinus1 == other.NumRefIdxL0ActiveMinus1 &&
               NumRefIdxL1ActiveMinus1 == other.NumRefIdxL1ActiveMinus1 &&
               RefPicListModification.Equals(other.RefPicListModification) &&
               BasePredWeightTableFlag == other.BasePredWeightTableFlag &&
               StoreRefBasePicFlag == other.StoreRefBasePicFlag &&
               CabacInitIdc == other.CabacInitIdc &&
               SliceQpDelta == other.SliceQpDelta &&
               DisableDeblockingFilterIdc == other.DisableDeblockingFilterIdc &&
               SliceAlphaC0OffsetDiv2 == other.SliceAlphaC0OffsetDiv2 &&
               SliceBetaOffsetDiv2 == other.SliceBetaOffsetDiv2 &&
               SliceGroupChangeCycle == other.SliceGroupChangeCycle &&
               RefLayerDqId == other.RefLayerDqId &&
               DisableInterLayerDeblockingFilterIdc == other.DisableInterLayerDeblockingFilterIdc &&
               InterLayerSliceAlphaC0OffsetDiv2 == other.InterLayerSliceAlphaC0OffsetDiv2 &&
               InterLayerSliceBetaOffsetDiv2 == other.InterLayerSliceBetaOffsetDiv2 &&
               ConstrainedIntraResamplingFlag == other.ConstrainedIntraResamplingFlag &&
               RefLayerChromaPhaseXPlus1Flag == other.RefLayerChromaPhaseXPlus1Flag &&
               RefLayerChromaPhaseYPlus1 == other.RefLayerChromaPhaseYPlus1 &&
               ScaledRefLayerLeftOffset == other.ScaledRefLayerLeftOffset &&
               ScaledRefLayerTopOffset == other.ScaledRefLayerTopOffset &&
               ScaledRefLayerRightOffset == other.ScaledRefLayerRightOffset &&
               ScaledRefLayerBottomOffset == other.ScaledRefLayerBottomOffset &&
               SliceSkipFlag == other.SliceSkipFlag &&
               NumMbsInSliceMinus1 == other.NumMbsInSliceMinus1 &&
               AdaptiveBaseModeFlag == other.AdaptiveBaseModeFlag &&
               DefaultBaseModeFlag == other.DefaultBaseModeFlag &&
               AdaptiveMotionPredictionFlag == other.AdaptiveMotionPredictionFlag &&
               DefaultMotionPredictionFlag == other.DefaultMotionPredictionFlag &&
               AdaptiveResidualPredictionFlag == other.AdaptiveResidualPredictionFlag &&
               DefaultResidualPredictionFlag == other.DefaultResidualPredictionFlag &&
               TCoeffLevelPredictionFlag == other.TCoeffLevelPredictionFlag &&
               ScanIdxStart == other.ScanIdxStart &&
               ScanIdxEnd == other.ScanIdxEnd;
    }

    /// <summary>
    ///   Determines the hash code.
    /// </summary>
    /// <returns>Hash code</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(FirstMbInSlice);
        hash.Add(SliceType);
        hash.Add(PpsId);
        hash.Add(ColorPlaneId);
        hash.Add(FrameNum);
        hash.Add(FieldPicFlag);
        hash.Add(BottomFieldFlag);
        hash.Add(IdrPicId);
        hash.Add(PicOrderCntLsb);
        hash.Add(DeltaPicOrderCntBottom);
        hash.Add(DeltaPicOrderCnt0);
        hash.Add(DeltaPicOrderCnt1);
        hash.Add(RedundantPicCnt);
        hash.Add(DirectSpatialMvPredFlag);
        hash.Add(NumRefIdxActiveOverrideFlag);
        hash.Add(NumRefIdxL0ActiveMinus1);
        hash.Add(NumRefIdxL1ActiveMinus1);
        hash.Add(RefPicListModification);
        hash.Add(BasePredWeightTableFlag);
        hash.Add(StoreRefBasePicFlag);
        hash.Add(CabacInitIdc);
        hash.Add(SliceQpDelta);
        hash.Add(DisableDeblockingFilterIdc);
        hash.Add(SliceAlphaC0OffsetDiv2);
        hash.Add(SliceBetaOffsetDiv2);
        hash.Add(SliceGroupChangeCycle);
        hash.Add(RefLayerDqId);
        hash.Add(DisableInterLayerDeblockingFilterIdc);
        hash.Add(InterLayerSliceAlphaC0OffsetDiv2);
        hash.Add(InterLayerSliceBetaOffsetDiv2);
        hash.Add(ConstrainedIntraResamplingFlag);
        hash.Add(RefLayerChromaPhaseXPlus1Flag);
        hash.Add(RefLayerChromaPhaseYPlus1);
        hash.Add(ScaledRefLayerLeftOffset);
        hash.Add(ScaledRefLayerTopOffset);
        hash.Add(ScaledRefLayerRightOffset);
        hash.Add(ScaledRefLayerBottomOffset);
        hash.Add(SliceSkipFlag);
        hash.Add(NumMbsInSliceMinus1);
        hash.Add(AdaptiveBaseModeFlag);
        hash.Add(DefaultBaseModeFlag);
        hash.Add(AdaptiveMotionPredictionFlag);
        hash.Add(DefaultMotionPredictionFlag);
        hash.Add(AdaptiveResidualPredictionFlag);
        hash.Add(DefaultResidualPredictionFlag);
        hash.Add(TCoeffLevelPredictionFlag);
        hash.Add(ScanIdxStart);
        hash.Add(ScanIdxEnd);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="SvcSliceHeader"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SvcSliceHeader left, SvcSliceHeader right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SvcSliceHeader"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SvcSliceHeader left, SvcSliceHeader right)
    {
        return !(left == right);
    }
}
