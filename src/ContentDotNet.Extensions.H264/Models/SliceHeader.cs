using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents the header of a slice in an H.264 bitstream.
/// </summary>
public struct SliceHeader : IEquatable<SliceHeader>
{
    /// <summary>
    /// The first macroblock in the slice.
    /// </summary>
    public uint FirstMbInSlice;

    /// <summary>
    /// The type of the slice.
    /// </summary>
    public uint SliceType;

    /// <summary>
    /// The ID of the picture parameter set (PPS) used by this slice.
    /// </summary>
    public uint PpsId;

    /// <summary>
    /// The color plane ID, used when separate color planes are enabled.
    /// </summary>
    public uint ColorPlaneId;

    /// <summary>
    /// The frame number of the slice.
    /// </summary>
    public uint FrameNum;

    /// <summary>
    /// Indicates whether the slice is a field picture.
    /// </summary>
    public bool FieldPicFlag;

    /// <summary>
    /// Indicates whether the slice is a bottom field.
    /// </summary>
    public bool BottomFieldFlag;

    /// <summary>
    /// The ID of the IDR picture, if applicable.
    /// </summary>
    public uint IDRPicId;

    /// <summary>
    /// The least significant bits of the picture order count.
    /// </summary>
    public uint PicOrderCntLsb;

    /// <summary>
    /// The difference in picture order count for the bottom field.
    /// </summary>
    public int DeltaPicOrderCntBottom;

    /// <summary>
    /// The differences in picture order count for reference fields.
    /// </summary>
    public (int, int) DeltaPicOrderCnt;

    /// <summary>
    /// The redundant picture count, if present.
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
    /// The reference picture list modification for non-MVC slices.
    /// </summary>
    public RefPicListModification? RefPicListModification;

    /// <summary>
    /// The reference picture list modification for MVC slices.
    /// </summary>
    public RefPicListMvcModification? RefPicListMvcModification;

    /// <summary>
    /// The prediction weight table for the slice.
    /// </summary>
    public PredWeightTable? PredWeightTable;

    /// <summary>
    /// The decoded reference picture marking for the slice.
    /// </summary>
    public DecRefPicMarking? DecRefPicMarking;

    /// <summary>
    /// The CABAC initialization IDC.
    /// </summary>
    public uint CabacInitIdc;

    /// <summary>
    /// The slice QP delta.
    /// </summary>
    public int SliceQpDelta;

    /// <summary>
    /// Indicates whether the slice is a switching SP slice.
    /// </summary>
    public bool SpForSwitchFlag;

    /// <summary>
    /// The slice QS delta.
    /// </summary>
    public int SliceQsDelta;

    /// <summary>
    /// The ID of the deblocking filter to disable.
    /// </summary>
    public uint DisableDeblockingFilterIdc;

    /// <summary>
    /// The alpha C0 offset divided by 2 for the deblocking filter.
    /// </summary>
    public int SliceAlphaC0OffsetDiv2;

    /// <summary>
    /// The beta offset divided by 2 for the deblocking filter.
    /// </summary>
    public int SliceBetaOffsetDiv2;

    /// <summary>
    /// The slice group change cycle.
    /// </summary>
    public uint SliceGroupChangeCycle;

    /// <summary>
    /// Initializes a new instance of the <see cref="SliceHeader"/> struct.
    /// </summary>
    /// <param name="firstMbInSlice">The first macroblock in the slice.</param>
    /// <param name="sliceType">The type of the slice.</param>
    /// <param name="ppsId">The ID of the PPS used by this slice.</param>
    /// <param name="colorPlaneId">The color plane ID.</param>
    /// <param name="frameNum">The frame number of the slice.</param>
    /// <param name="fieldPicFlag">Indicates whether the slice is a field picture.</param>
    /// <param name="bottomFieldFlag">Indicates whether the slice is a bottom field.</param>
    /// <param name="iDRPicId">The ID of the IDR picture.</param>
    /// <param name="picOrderCntLsb">The least significant bits of the picture order count.</param>
    /// <param name="deltaPicOrderCntBottom">The difference in picture order count for the bottom field.</param>
    /// <param name="deltaPicOrderCnt">The differences in picture order count for reference fields.</param>
    /// <param name="redundantPicCnt">The redundant picture count.</param>
    /// <param name="directSpatialMvPredFlag">Indicates whether direct spatial motion vector prediction is used.</param>
    /// <param name="numRefIdxActiveOverrideFlag">Indicates whether the number of active reference indices is overridden.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for list 0 minus 1.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for list 1 minus 1.</param>
    /// <param name="refPicListModification">The reference picture list modification for non-MVC slices.</param>
    /// <param name="refPicListMvcModification">The reference picture list modification for MVC slices.</param>
    /// <param name="predWeightTable">The prediction weight table for the slice.</param>
    /// <param name="decRefPicMarking">The decoded reference picture marking for the slice.</param>
    /// <param name="cabacInitIdc">The CABAC initialization IDC.</param>
    /// <param name="sliceQpDelta">The slice QP delta.</param>
    /// <param name="spForSwitchFlag">Indicates whether the slice is a switching SP slice.</param>
    /// <param name="sliceQsDelta">The slice QS delta.</param>
    /// <param name="disableDeblockingFilterIdc">The ID of the deblocking filter to disable.</param>
    /// <param name="sliceAlphaC0OffsetDiv2">The alpha C0 offset divided by 2 for the deblocking filter.</param>
    /// <param name="sliceBetaOffsetDiv2">The beta offset divided by 2 for the deblocking filter.</param>
    /// <param name="sliceGroupChangeCycle">The slice group change cycle.</param>
    public SliceHeader(
        uint firstMbInSlice,
        uint sliceType,
        uint ppsId,
        uint colorPlaneId,
        uint frameNum,
        bool fieldPicFlag,
        bool bottomFieldFlag,
        uint iDRPicId,
        uint picOrderCntLsb,
        int deltaPicOrderCntBottom,
        (int, int) deltaPicOrderCnt,
        uint redundantPicCnt,
        bool directSpatialMvPredFlag,
        bool numRefIdxActiveOverrideFlag,
        uint numRefIdxL0ActiveMinus1,
        uint numRefIdxL1ActiveMinus1,
        RefPicListModification? refPicListModification,
        RefPicListMvcModification? refPicListMvcModification,
        PredWeightTable? predWeightTable,
        DecRefPicMarking? decRefPicMarking,
        uint cabacInitIdc,
        int sliceQpDelta,
        bool spForSwitchFlag,
        int sliceQsDelta,
        uint disableDeblockingFilterIdc,
        int sliceAlphaC0OffsetDiv2,
        int sliceBetaOffsetDiv2,
        uint sliceGroupChangeCycle)
    {
        FirstMbInSlice = firstMbInSlice;
        SliceType = sliceType;
        PpsId = ppsId;
        ColorPlaneId = colorPlaneId;
        FrameNum = frameNum;
        FieldPicFlag = fieldPicFlag;
        BottomFieldFlag = bottomFieldFlag;
        IDRPicId = iDRPicId;
        PicOrderCntLsb = picOrderCntLsb;
        DeltaPicOrderCntBottom = deltaPicOrderCntBottom;
        DeltaPicOrderCnt = deltaPicOrderCnt;
        RedundantPicCnt = redundantPicCnt;
        DirectSpatialMvPredFlag = directSpatialMvPredFlag;
        NumRefIdxActiveOverrideFlag = numRefIdxActiveOverrideFlag;
        NumRefIdxL0ActiveMinus1 = numRefIdxL0ActiveMinus1;
        NumRefIdxL1ActiveMinus1 = numRefIdxL1ActiveMinus1;
        RefPicListModification = refPicListModification;
        RefPicListMvcModification = refPicListMvcModification;
        PredWeightTable = predWeightTable;
        DecRefPicMarking = decRefPicMarking;
        CabacInitIdc = cabacInitIdc;
        SliceQpDelta = sliceQpDelta;
        SpForSwitchFlag = spForSwitchFlag;
        SliceQsDelta = sliceQsDelta;
        DisableDeblockingFilterIdc = disableDeblockingFilterIdc;
        SliceAlphaC0OffsetDiv2 = sliceAlphaC0OffsetDiv2;
        SliceBetaOffsetDiv2 = sliceBetaOffsetDiv2;
        SliceGroupChangeCycle = sliceGroupChangeCycle;
    }

    /// <summary>
    /// Reads a <see cref="SliceHeader"/> from the bitstream.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <param name="nalu">The NAL unit containing the slice.</param>
    /// <param name="sps">The sequence parameter set (SPS) associated with the slice.</param>
    /// <param name="pps">The picture parameter set (PPS) associated with the slice.</param>
    /// <returns>A new <see cref="SliceHeader"/> instance.</returns>
    public static SliceHeader Read(BitStreamReader reader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps)
    {
        uint firstMbInSlice = reader.ReadUE();
        uint sliceType = reader.ReadUE();
        uint ppsId = reader.ReadUE();

        uint colorPlaneId = 0u;
        if (sps.SeparateColourPlaneFlag)
            colorPlaneId = reader.ReadBits(2);

        uint frameNum = reader.ReadBits((uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));

        bool fieldPicFlag = false;
        bool bottomFieldFlag = false;

        if (!sps.FrameMbsOnlyFlag)
        {
            fieldPicFlag = reader.ReadBit();
            if (fieldPicFlag)
                bottomFieldFlag = reader.ReadBit();
        }

        uint idrPicId = 0u;
        if (nalu.IsIdr())
            idrPicId = reader.ReadUE();

        uint picOrderCntLsb = 0u;
        int deltaPicOrderCntBottom = 0;

        if (sps.PicOrderCntType == 0u)
        {
            picOrderCntLsb = reader.ReadBits((uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));
            if (pps.BottomFieldPicOrderInFramePresentFlag && fieldPicFlag)
                deltaPicOrderCntBottom = reader.ReadSE();
        }

        (int a, int b) deltaPicOrderCnt = (0, 0);
        if (sps.PicOrderCntType == 1 && !sps.DeltaPicOrderAlwaysZeroFlag)
        {
            deltaPicOrderCnt.a = reader.ReadSE();
            if (pps.BottomFieldPicOrderInFramePresentFlag && fieldPicFlag)
                deltaPicOrderCnt.b = reader.ReadSE();
        }

        uint redundantPicCnt = 0u;
        if (pps.RedundantPicCntPresentFlag)
            redundantPicCnt = reader.ReadUE();

        bool directSpatialMvPredFlag = false;
        if (SliceTypes.IsB(sliceType))
            directSpatialMvPredFlag = reader.ReadBit();

        bool numRefIdxActiveOverrideFlag = false;
        uint numRefIdxL0ActiveMinus1 = 0u;
        uint numRefIdxL1ActiveMinus1 = 0u;
        if (SliceTypes.IsP(sliceType) || SliceTypes.IsSP(sliceType) || SliceTypes.IsB(sliceType))
        {
            numRefIdxActiveOverrideFlag = reader.ReadBit();
            if (numRefIdxActiveOverrideFlag)
            {
                numRefIdxL0ActiveMinus1 = reader.ReadUE();
                if (SliceTypes.IsB(sliceType))
                    numRefIdxL1ActiveMinus1 = reader.ReadUE();
            }
        }

        RefPicListMvcModification? refPicListMvcModification = null;
        RefPicListModification? refPicListModification = null;
        if (nalu.NalUnitType is 20 or 21)
            refPicListMvcModification = Models.RefPicListMvcModification.Read(reader, (int)sliceType);
        else
            refPicListModification = Models.RefPicListModification.Read(reader, sliceType);

        PredWeightTable? predWeightTable = null;
        if ((pps.WeightedPredFlag && (SliceTypes.IsP(sliceType) || SliceTypes.IsSP(sliceType))) ||
            (pps.WeightedBiPredIdc == 1 && SliceTypes.IsB(sliceType)))
            predWeightTable = Models.PredWeightTable.Read(reader, (int)sps.GetChromaArrayType(), (int)sliceType, (int)numRefIdxL0ActiveMinus1, (int)numRefIdxL1ActiveMinus1);

        DecRefPicMarking? decRefPicMarking = null;
        if (nalu.NalRefIdc != 0)
            decRefPicMarking = Models.DecRefPicMarking.Read(reader, nalu.IsIdr());

        uint cabacInitIdc = 0u;
        if (pps.EntropyCodingModeFlag && !SliceTypes.IsI(sliceType) && !SliceTypes.IsSI(sliceType))
            cabacInitIdc = reader.ReadUE();

        int sliceQpDelta = reader.ReadSE();
        bool spForSwitchFlag = false;
        int sliceQsDelta = 0;
        if (SliceTypes.IsSP(sliceType) || SliceTypes.IsSI(sliceType))
        {
            if (SliceTypes.IsSP(sliceType))
                spForSwitchFlag = reader.ReadBit();
            sliceQsDelta = reader.ReadSE();
        }

        uint disableDeblockingFilterIdc = 0u;
        int sliceAlphaC0OffsetDiv2 = 0;
        int sliceBetaOffsetDiv2 = 0;
        if (pps.DeblockingFilterControlPresentFlag)
        {
            disableDeblockingFilterIdc = reader.ReadUE();
            if (disableDeblockingFilterIdc != 1u)
            {
                sliceAlphaC0OffsetDiv2 = reader.ReadSE();
                sliceBetaOffsetDiv2 = reader.ReadSE();
            }
        }

        uint sliceGroupChangeCycle = 0u;
        if (pps.NumSliceGroupsMinus1 > 0 &&
            pps.SliceGroupMapType is >= 3 and <= 5)
            sliceGroupChangeCycle = reader.ReadBits((uint)Math.Ceiling(Math.Log2(pps.PicSizeInMapUnitsMinus1 + 1u / pps.SliceGroupChangeRateMinus1 + 1u)));

        return new SliceHeader(
            firstMbInSlice, sliceType, ppsId, colorPlaneId, frameNum, fieldPicFlag, bottomFieldFlag, idrPicId, picOrderCntLsb,
            deltaPicOrderCntBottom, deltaPicOrderCnt, redundantPicCnt, directSpatialMvPredFlag, numRefIdxActiveOverrideFlag,
            numRefIdxL0ActiveMinus1, numRefIdxL1ActiveMinus1, refPicListModification, refPicListMvcModification,
            predWeightTable, decRefPicMarking, cabacInitIdc, sliceQpDelta, spForSwitchFlag, sliceQsDelta, disableDeblockingFilterIdc,
            sliceAlphaC0OffsetDiv2, sliceBetaOffsetDiv2, sliceGroupChangeCycle);       
    }

    /// <summary>
    ///   Writes this slice header to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer where the slice header is written to.</param>
    /// <param name="nalu">Current NAL unit</param>
    /// <param name="sps">Last SPS</param>
    /// <param name="pps">Last PPS</param>
    /// <param name="options">Options for writing this slice header</param>
    public readonly void Write(BitStreamWriter writer, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, SliceHeaderWriteOptions options)
    {
        writer.WriteUE(FirstMbInSlice);
        writer.WriteUE(SliceType);
        writer.WriteUE(PpsId);

        if (sps.SeparateColourPlaneFlag)
            writer.WriteBits(ColorPlaneId, 2);

        writer.WriteBits(FrameNum, (uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));

        if (!sps.FrameMbsOnlyFlag)
        {
            writer.WriteBit(FieldPicFlag);
            if (FieldPicFlag)
                writer.WriteBit(BottomFieldFlag);
        }

        if (nalu.IsIdr())
            writer.WriteUE(IDRPicId);

        if (sps.PicOrderCntType == 0u)
        {
            writer.WriteBits(PicOrderCntLsb, (uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));
            if (pps.BottomFieldPicOrderInFramePresentFlag && FieldPicFlag)
                writer.WriteSE(DeltaPicOrderCntBottom);
        }

        if (sps.PicOrderCntType == 1 && !sps.DeltaPicOrderAlwaysZeroFlag)
        {
            writer.WriteSE(DeltaPicOrderCnt.Item1);
            if (pps.BottomFieldPicOrderInFramePresentFlag && FieldPicFlag)
                writer.WriteSE(DeltaPicOrderCnt.Item2);
        }

        if (pps.RedundantPicCntPresentFlag)
            writer.WriteUE(RedundantPicCnt);

        if (SliceTypes.IsB(SliceType))
            writer.WriteBit(DirectSpatialMvPredFlag);

        if (SliceTypes.IsP(SliceType) || SliceTypes.IsSP(SliceType) || SliceTypes.IsB(SliceType))
        {
            writer.WriteBit(NumRefIdxActiveOverrideFlag);
            if (NumRefIdxActiveOverrideFlag)
            {
                writer.WriteUE(NumRefIdxL0ActiveMinus1);
                if (SliceTypes.IsB(SliceType))
                    writer.WriteUE(NumRefIdxL1ActiveMinus1);
            }
        }

        if (nalu.NalUnitType is 20 or 21)
            this.RefPicListMvcModification!.Value.Write(writer, (int)SliceType, options.RefPicListMvcModificationL0, options.RefPicListMvcModificationL1);
        else
            this.RefPicListModification!.Value.Write(writer, options.RefPicListModificationEntries, 0u);

        if ((pps.WeightedPredFlag && (SliceTypes.IsP(SliceType) || SliceTypes.IsSP(SliceType))) ||
            (pps.WeightedBiPredIdc == 1 && SliceTypes.IsB(SliceType)))
            this.PredWeightTable!.Value.Write(writer, (int)sps.GetChromaArrayType(), (int)SliceType, options.PredWeightTableL0, options.PredWeightTableL1);

        if (nalu.NalRefIdc != 0)
            this.DecRefPicMarking!.Value.Write(writer, nalu.IsIdr(), options.DecRefPicMarkingEntries);

        if (pps.EntropyCodingModeFlag && !SliceTypes.IsI(SliceType) && !SliceTypes.IsSI(SliceType))
            writer.WriteUE(CabacInitIdc);

        writer.WriteSE(SliceQpDelta);
        if (SliceTypes.IsSP(SliceType) || SliceTypes.IsSI(SliceType))
        {
            if (SliceTypes.IsSP(SliceType))
                writer.WriteBit(SpForSwitchFlag);
            writer.WriteSE(SliceQsDelta);
        }

        uint disableDeblockingFilterIdc = 0u;
        if (pps.DeblockingFilterControlPresentFlag)
        {
            writer.WriteUE(DisableDeblockingFilterIdc);
            if (disableDeblockingFilterIdc != 1u)
            {
                writer.WriteSE(SliceAlphaC0OffsetDiv2);
                writer.WriteSE(SliceBetaOffsetDiv2);
            }
        }

        if (pps.NumSliceGroupsMinus1 > 0 &&
            pps.SliceGroupMapType is >= 3 and <= 5)
            writer.WriteBits(SliceGroupChangeCycle, (uint)Math.Ceiling(Math.Log2(pps.PicSizeInMapUnitsMinus1 + 1u / pps.SliceGroupChangeRateMinus1 + 1u)));
    }

    /// <summary>
    ///   Writes this slice header to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer where the slice header is written to.</param>
    /// <param name="nalu">Current NAL unit</param>
    /// <param name="sps">Last SPS</param>
    /// <param name="pps">Last PPS</param>
    /// <param name="options">Options for writing this slice header</param>
    public readonly void Write(BitStreamWriter writer, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, MemorySliceHeaderWriteOptions options)
    {
        writer.WriteUE(FirstMbInSlice);
        writer.WriteUE(SliceType);
        writer.WriteUE(PpsId);

        if (sps.SeparateColourPlaneFlag)
            writer.WriteBits(ColorPlaneId, 2);

        writer.WriteBits(FrameNum, (uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));

        if (!sps.FrameMbsOnlyFlag)
        {
            writer.WriteBit(FieldPicFlag);
            if (FieldPicFlag)
                writer.WriteBit(BottomFieldFlag);
        }

        if (nalu.IsIdr())
            writer.WriteUE(IDRPicId);

        if (sps.PicOrderCntType == 0u)
        {
            writer.WriteBits(PicOrderCntLsb, (uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));
            if (pps.BottomFieldPicOrderInFramePresentFlag && FieldPicFlag)
                writer.WriteSE(DeltaPicOrderCntBottom);
        }

        if (sps.PicOrderCntType == 1 && !sps.DeltaPicOrderAlwaysZeroFlag)
        {
            writer.WriteSE(DeltaPicOrderCnt.Item1);
            if (pps.BottomFieldPicOrderInFramePresentFlag && FieldPicFlag)
                writer.WriteSE(DeltaPicOrderCnt.Item2);
        }

        if (pps.RedundantPicCntPresentFlag)
            writer.WriteUE(RedundantPicCnt);

        if (SliceTypes.IsB(SliceType))
            writer.WriteBit(DirectSpatialMvPredFlag);

        if (SliceTypes.IsP(SliceType) || SliceTypes.IsSP(SliceType) || SliceTypes.IsB(SliceType))
        {
            writer.WriteBit(NumRefIdxActiveOverrideFlag);
            if (NumRefIdxActiveOverrideFlag)
            {
                writer.WriteUE(NumRefIdxL0ActiveMinus1);
                if (SliceTypes.IsB(SliceType))
                    writer.WriteUE(NumRefIdxL1ActiveMinus1);
            }
        }

        if (nalu.NalUnitType is 20 or 21)
            this.RefPicListMvcModification!.Value.Write(writer, (int)SliceType, options.RefPicListMvcModificationL0.Span, options.RefPicListMvcModificationL1.Span);
        else
            this.RefPicListModification!.Value.Write(writer, options.RefPicListModificationEntries.Span, 0u);

        if ((pps.WeightedPredFlag && (SliceTypes.IsP(SliceType) || SliceTypes.IsSP(SliceType))) ||
            (pps.WeightedBiPredIdc == 1 && SliceTypes.IsB(SliceType)))
            this.PredWeightTable!.Value.Write(writer, (int)sps.GetChromaArrayType(), (int)SliceType, options.PredWeightTableL0, options.PredWeightTableL1);

        if (nalu.NalRefIdc != 0)
            this.DecRefPicMarking!.Value.Write(writer, nalu.IsIdr(), options.DecRefPicMarkingEntries.Span);

        if (pps.EntropyCodingModeFlag && !SliceTypes.IsI(SliceType) && !SliceTypes.IsSI(SliceType))
            writer.WriteUE(CabacInitIdc);

        writer.WriteSE(SliceQpDelta);
        if (SliceTypes.IsSP(SliceType) || SliceTypes.IsSI(SliceType))
        {
            if (SliceTypes.IsSP(SliceType))
                writer.WriteBit(SpForSwitchFlag);
            writer.WriteSE(SliceQsDelta);
        }

        uint disableDeblockingFilterIdc = 0u;
        if (pps.DeblockingFilterControlPresentFlag)
        {
            writer.WriteUE(DisableDeblockingFilterIdc);
            if (disableDeblockingFilterIdc != 1u)
            {
                writer.WriteSE(SliceAlphaC0OffsetDiv2);
                writer.WriteSE(SliceBetaOffsetDiv2);
            }
        }

        if (pps.NumSliceGroupsMinus1 > 0 &&
            pps.SliceGroupMapType is >= 3 and <= 5)
            writer.WriteBits(SliceGroupChangeCycle, (uint)Math.Ceiling(Math.Log2(pps.PicSizeInMapUnitsMinus1 + 1u / pps.SliceGroupChangeRateMinus1 + 1u)));
    }

    /// <summary>
    ///   Writes this slice header to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer where the slice header is written to.</param>
    /// <param name="nalu">Current NAL unit</param>
    /// <param name="sps">Last SPS</param>
    /// <param name="pps">Last PPS</param>
    /// <param name="options">Options for writing this slice header</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, MemorySliceHeaderWriteOptions options)
    {
        await writer.WriteUEAsync(FirstMbInSlice);
        await writer.WriteUEAsync(SliceType);
        await writer.WriteUEAsync(PpsId);

        if (sps.SeparateColourPlaneFlag)
            await writer.WriteBitsAsync(ColorPlaneId, 2);

        await writer.WriteBitsAsync(FrameNum, (uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));

        if (!sps.FrameMbsOnlyFlag)
        {
            await writer.WriteBitAsync(FieldPicFlag);
            if (FieldPicFlag)
                await writer.WriteBitAsync(BottomFieldFlag);
        }

        if (nalu.IsIdr())
            await writer.WriteUEAsync(IDRPicId);

        if (sps.PicOrderCntType == 0u)
        {
            await writer.WriteBitsAsync(PicOrderCntLsb, (uint)Math.Ceiling(Math.Log2(sps.Log2MaxFrameNumMinus4 + 4u)));
            if (pps.BottomFieldPicOrderInFramePresentFlag && FieldPicFlag)
                await writer.WriteSEAsync(DeltaPicOrderCntBottom);
        }

        if (sps.PicOrderCntType == 1 && !sps.DeltaPicOrderAlwaysZeroFlag)
        {
            await writer.WriteSEAsync(DeltaPicOrderCnt.Item1);
            if (pps.BottomFieldPicOrderInFramePresentFlag && FieldPicFlag)
                await writer.WriteSEAsync(DeltaPicOrderCnt.Item2);
        }

        if (pps.RedundantPicCntPresentFlag)
            await writer.WriteUEAsync(RedundantPicCnt);

        if (SliceTypes.IsB(SliceType))
            await writer.WriteBitAsync(DirectSpatialMvPredFlag);

        if (SliceTypes.IsP(SliceType) || SliceTypes.IsSP(SliceType) || SliceTypes.IsB(SliceType))
        {
            await writer.WriteBitAsync(NumRefIdxActiveOverrideFlag);
            if (NumRefIdxActiveOverrideFlag)
            {
                await writer.WriteUEAsync(NumRefIdxL0ActiveMinus1);
                if (SliceTypes.IsB(SliceType))
                    await writer.WriteUEAsync(NumRefIdxL1ActiveMinus1);
            }
        }

        if (nalu.NalUnitType is 20 or 21)
            this.RefPicListMvcModification!.Value.Write(writer, (int)SliceType, options.RefPicListMvcModificationL0.Span, options.RefPicListMvcModificationL1.Span);
        else
            this.RefPicListModification!.Value.Write(writer, options.RefPicListModificationEntries.Span, 0u);

        if ((pps.WeightedPredFlag && (SliceTypes.IsP(SliceType) || SliceTypes.IsSP(SliceType))) ||
            (pps.WeightedBiPredIdc == 1 && SliceTypes.IsB(SliceType)))
            this.PredWeightTable!.Value.Write(writer, (int)sps.GetChromaArrayType(), (int)SliceType, options.PredWeightTableL0, options.PredWeightTableL1);

        if (nalu.NalRefIdc != 0)
            this.DecRefPicMarking!.Value.Write(writer, nalu.IsIdr(), options.DecRefPicMarkingEntries.Span);

        if (pps.EntropyCodingModeFlag && !SliceTypes.IsI(SliceType) && !SliceTypes.IsSI(SliceType))
            await writer.WriteUEAsync(CabacInitIdc);

        await writer.WriteSEAsync(SliceQpDelta);
        if (SliceTypes.IsSP(SliceType) || SliceTypes.IsSI(SliceType))
        {
            if (SliceTypes.IsSP(SliceType))
                await writer.WriteBitAsync(SpForSwitchFlag);
            await writer.WriteSEAsync(SliceQsDelta);
        }

        uint disableDeblockingFilterIdc = 0u;
        if (pps.DeblockingFilterControlPresentFlag)
        {
            await writer.WriteUEAsync(DisableDeblockingFilterIdc);
            if (disableDeblockingFilterIdc != 1u)
            {
                await writer.WriteSEAsync(SliceAlphaC0OffsetDiv2);
                await writer.WriteSEAsync(SliceBetaOffsetDiv2);
            }
        }

        if (pps.NumSliceGroupsMinus1 > 0 &&
            pps.SliceGroupMapType is >= 3 and <= 5)
            await writer.WriteBitsAsync(SliceGroupChangeCycle, (uint)Math.Ceiling(Math.Log2(pps.PicSizeInMapUnitsMinus1 + 1u / pps.SliceGroupChangeRateMinus1 + 1u)));
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is SliceHeader header && Equals(header);
    }

    /// <summary>
    /// Determines whether the specified <see cref="SliceHeader"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="SliceHeader"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="SliceHeader"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SliceHeader other)
    {
        return FirstMbInSlice == other.FirstMbInSlice &&
               SliceType == other.SliceType &&
               PpsId == other.PpsId &&
               ColorPlaneId == other.ColorPlaneId &&
               FrameNum == other.FrameNum &&
               FieldPicFlag == other.FieldPicFlag &&
               BottomFieldFlag == other.BottomFieldFlag &&
               IDRPicId == other.IDRPicId &&
               PicOrderCntLsb == other.PicOrderCntLsb &&
               DeltaPicOrderCntBottom == other.DeltaPicOrderCntBottom &&
               DeltaPicOrderCnt.Equals(other.DeltaPicOrderCnt) &&
               RedundantPicCnt == other.RedundantPicCnt &&
               DirectSpatialMvPredFlag == other.DirectSpatialMvPredFlag &&
               NumRefIdxActiveOverrideFlag == other.NumRefIdxActiveOverrideFlag &&
               NumRefIdxL0ActiveMinus1 == other.NumRefIdxL0ActiveMinus1 &&
               NumRefIdxL1ActiveMinus1 == other.NumRefIdxL1ActiveMinus1 &&
               EqualityComparer<RefPicListModification?>.Default.Equals(RefPicListModification, other.RefPicListModification) &&
               EqualityComparer<RefPicListMvcModification?>.Default.Equals(RefPicListMvcModification, other.RefPicListMvcModification) &&
               EqualityComparer<PredWeightTable?>.Default.Equals(PredWeightTable, other.PredWeightTable) &&
               EqualityComparer<DecRefPicMarking?>.Default.Equals(DecRefPicMarking, other.DecRefPicMarking) &&
               CabacInitIdc == other.CabacInitIdc &&
               SliceQpDelta == other.SliceQpDelta &&
               SpForSwitchFlag == other.SpForSwitchFlag &&
               SliceQsDelta == other.SliceQsDelta &&
               DisableDeblockingFilterIdc == other.DisableDeblockingFilterIdc &&
               SliceAlphaC0OffsetDiv2 == other.SliceAlphaC0OffsetDiv2 &&
               SliceBetaOffsetDiv2 == other.SliceBetaOffsetDiv2 &&
               SliceGroupChangeCycle == other.SliceGroupChangeCycle;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
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
        hash.Add(IDRPicId);
        hash.Add(PicOrderCntLsb);
        hash.Add(DeltaPicOrderCntBottom);
        hash.Add(DeltaPicOrderCnt);
        hash.Add(RedundantPicCnt);
        hash.Add(DirectSpatialMvPredFlag);
        hash.Add(NumRefIdxActiveOverrideFlag);
        hash.Add(NumRefIdxL0ActiveMinus1);
        hash.Add(NumRefIdxL1ActiveMinus1);
        hash.Add(RefPicListModification);
        hash.Add(RefPicListMvcModification);
        hash.Add(PredWeightTable);
        hash.Add(DecRefPicMarking);
        hash.Add(CabacInitIdc);
        hash.Add(SliceQpDelta);
        hash.Add(SpForSwitchFlag);
        hash.Add(SliceQsDelta);
        hash.Add(DisableDeblockingFilterIdc);
        hash.Add(SliceAlphaC0OffsetDiv2);
        hash.Add(SliceBetaOffsetDiv2);
        hash.Add(SliceGroupChangeCycle);
        return hash.ToHashCode();
    }


    /// <summary>  
    /// Determines whether two <see cref="SliceHeader"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="SliceHeader"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="SliceHeader"/> instance to compare.</param>  
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(SliceHeader left, SliceHeader right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    /// Determines whether two <see cref="SliceHeader"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="SliceHeader"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="SliceHeader"/> instance to compare.</param>  
    /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(SliceHeader left, SliceHeader right)
    {
        return !(left == right);
    }
}
