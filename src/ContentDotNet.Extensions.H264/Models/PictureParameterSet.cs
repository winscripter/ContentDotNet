using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents a Picture Parameter Set (PPS) model. This structure contains raw data.
/// </summary>
public struct PictureParameterSet : IParameterSet, IEquatable<PictureParameterSet>
{
    private static readonly ScalingMatrixBuilder BogusBuilder = new((int a, int b, Span<int> c, out bool d) => { d = false; });

    /// <summary>
    ///   Represents the type of the parameter set.
    /// </summary>
    public readonly ParameterSetKind Kind => ParameterSetKind.Picture;

    /// <summary>
    /// Represents the sequence parameter set (SPS) ID.
    /// </summary>
    public uint SpsId;

    /// <summary>
    /// Represents the picture parameter set (PPS) ID.
    /// </summary>
    public uint PpsId;

    /// <summary>
    /// Indicates whether entropy coding mode is enabled (CABAC).
    /// </summary>
    public bool EntropyCodingModeFlag;

    /// <summary>
    /// Specifies if bottom field picture order in frame is present.
    /// </summary>
    public bool BottomFieldPicOrderInFramePresentFlag;

    /// <summary>
    /// Specifies the number of slice groups minus 1.
    /// </summary>
    public uint NumSliceGroupsMinus1;

    /// <summary>
    /// Top-left coordinates of slice groups (up to 8).
    /// </summary>
    public uint TopLeft1, TopLeft2, TopLeft3, TopLeft4, TopLeft5, TopLeft6, TopLeft7, TopLeft8;

    /// <summary>
    /// Bottom-right coordinates of slice groups (up to 8).
    /// </summary>
    public uint BottomRight1, BottomRight2, BottomRight3, BottomRight4, BottomRight5, BottomRight6, BottomRight7, BottomRight8;

    /// <summary>
    /// Indicates the direction of slice group change.
    /// </summary>
    public bool SliceGroupChangeDirectionFlag;

    /// <summary>
    /// Specifies the slice group change rate minus 1.
    /// </summary>
    public uint SliceGroupChangeRateMinus1;

    /// <summary>
    /// Specifies the slice group map type.
    /// </summary>
    public uint SliceGroupMapType;

    /// <summary>
    /// Specifies the run lengths.
    /// </summary>
    public Container8UInt32 RunLengthMinus1;

    /// <summary>
    /// Specifies the size of the picture in map units minus 1.
    /// </summary>
    public uint PicSizeInMapUnitsMinus1;

    /// <summary>
    /// Specifies the default number of reference indices for list 0 minus 1.
    /// </summary>
    public uint NumRefIdxL0DefaultActiveMinus1;

    /// <summary>
    /// Specifies the default number of reference indices for list 1 minus 1.
    /// </summary>
    public uint NumRefIdxL1DefaultActiveMinus1;

    /// <summary>
    /// Indicates if weighted prediction is enabled.
    /// </summary>
    public bool WeightedPredFlag;

    /// <summary>
    /// Specifies the weighted bi-prediction IDC value.
    /// </summary>
    public uint WeightedBiPredIdc;

    /// <summary>
    /// Specifies the initial QP value minus 26.
    /// </summary>
    public int PicInitQpMinus26;

    /// <summary>
    /// Specifies the initial QS value minus 26.
    /// </summary>
    public int PicInitQsMinus26;

    /// <summary>
    /// Specifies the chroma QP index offset.
    /// </summary>
    public int ChromaQpIndexOffset;

    /// <summary>
    /// Indicates if deblocking filter control is present.
    /// </summary>
    public bool DeblockingFilterControlPresentFlag;

    /// <summary>
    /// Indicates if constrained intra prediction is enabled.
    /// </summary>
    public bool ConstrainedIntraPredFlag;

    /// <summary>
    /// Indicates if redundant picture count is present.
    /// </summary>
    public bool RedundantPicCntPresentFlag;

    /// <summary>
    /// Indicates if the transform 8x8 mode is enabled.
    /// </summary>
    public bool Transform8x8ModeFlag;

    /// <summary>
    /// Indicates if a picture scaling matrix is present.
    /// </summary>
    public bool PicScalingMatrixPresentFlag;

    /// <summary>
    /// Represents the scaling matrices for the picture.
    /// </summary>
    public ScalingMatrices? ScalingMatrix;

    /// <summary>
    /// Specifies the second chroma QP index offset.
    /// </summary>
    public int SecondChromaQpIndexOffset;

    /// <summary>
    ///   Initializes a new instance of the <see cref="PictureParameterSet"/> structure.
    /// </summary>
    /// <param name="spsId">Part of the PPS.</param>
    /// <param name="ppsId">Part of the PPS.</param>
    /// <param name="entropyCodingModeFlag">Part of the PPS.</param>
    /// <param name="bottomFieldPicOrderInFramePresentFlag">Part of the PPS.</param>
    /// <param name="numSliceGroupsMinus1">Part of the PPS.</param>
    /// <param name="topLeft">Part of the PPS.</param>
    /// <param name="bottomRight">Part of the PPS.</param>
    /// <param name="sliceGroupChangeDirectionFlag">Part of the PPS.</param>
    /// <param name="sliceGroupChangeRateMinus1">Part of the PPS.</param>
    /// <param name="sliceGroupMapType">Part of the PPS.</param>
    /// <param name="runLengthMinus1">Part of the PPS.</param>
    /// <param name="picSizeInMapUnitsMinus1">Part of the PPS.</param>
    /// <param name="numRefIdxL0DefaultActiveMinus1">Part of the PPS.</param>
    /// <param name="numRefIdxL1DefaultActiveMinus1">Part of the PPS.</param>
    /// <param name="weightedPredFlag">Part of the PPS.</param>
    /// <param name="weightedBiPredIdc">Part of the PPS.</param>
    /// <param name="picInitQpMinus26">Part of the PPS.</param>
    /// <param name="picInitQsMinus26">Part of the PPS.</param>
    /// <param name="chromaQpIndexOffset">Part of the PPS.</param>
    /// <param name="deblockingFilterControlPresentFlag">Part of the PPS.</param>
    /// <param name="constrainedIntraPredFlag">Part of the PPS.</param>
    /// <param name="redundantPicCntPresentFlag">Part of the PPS.</param>
    /// <param name="secondChromaQpIndexOffset">Part of the PPS.</param>
    public PictureParameterSet(uint spsId, uint ppsId, bool entropyCodingModeFlag, bool bottomFieldPicOrderInFramePresentFlag, uint numSliceGroupsMinus1, Span<uint> topLeft, Span<uint> bottomRight, bool sliceGroupChangeDirectionFlag, uint sliceGroupChangeRateMinus1, uint sliceGroupMapType, Container8UInt32 runLengthMinus1, uint picSizeInMapUnitsMinus1, uint numRefIdxL0DefaultActiveMinus1, uint numRefIdxL1DefaultActiveMinus1, bool weightedPredFlag, uint weightedBiPredIdc, int picInitQpMinus26, int picInitQsMinus26, int chromaQpIndexOffset, bool deblockingFilterControlPresentFlag, bool constrainedIntraPredFlag, bool redundantPicCntPresentFlag, int secondChromaQpIndexOffset)
    {
        SpsId = spsId;
        PpsId = ppsId;
        EntropyCodingModeFlag = entropyCodingModeFlag;
        BottomFieldPicOrderInFramePresentFlag = bottomFieldPicOrderInFramePresentFlag;
        NumSliceGroupsMinus1 = numSliceGroupsMinus1;
        for (int i = 0; i < topLeft.Length; i++)
            SetTopLeftFast(i, topLeft[i]);
        for (int i = 0; i < bottomRight.Length; i++)
            SetBottomRightFast(i, bottomRight[i]);
        SliceGroupChangeDirectionFlag = sliceGroupChangeDirectionFlag;
        SliceGroupChangeRateMinus1 = sliceGroupChangeRateMinus1;
        SliceGroupMapType = sliceGroupMapType;
        RunLengthMinus1 = runLengthMinus1;
        PicSizeInMapUnitsMinus1 = picSizeInMapUnitsMinus1;
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
        SecondChromaQpIndexOffset = secondChromaQpIndexOffset;
    }

    /// <summary>
    ///   Reads the PPS from the bitstream.
    /// </summary>
    /// <param name="reader">Bitstream reader where the PPS is read from.</param>
    /// <param name="sps">SPS, required for PPS parsing.</param>
    /// <param name="nalLength">Number of bytes in NAL Unit + RBSP</param>
    /// <returns>PPS, parsed from the bitstream.</returns>
    public static PictureParameterSet Read(BitStreamReader reader, long nalLength, SequenceParameterSet sps)
    {
        uint spsId = reader.ReadUE();
        uint ppsId = reader.ReadUE();
        bool entropyCodingModeFlag = reader.ReadBit();
        bool bottomFieldPicOrderInFramePresentFlag = reader.ReadBit();
        uint numSliceGroupsMinus1 = reader.ReadUE();

        uint sliceGroupMapType = 0u;
        Span<uint> topLeft = stackalloc uint[8];
        Span<uint> bottomRight = stackalloc uint[8];
        bool sliceGroupChangeDirectionFlag = false;
        uint sliceGroupChangeRateMinus1 = 0u;
        uint picSizeInMapUnitsMinus1 = 0u;
        uint numRefIdxL0DefaultActiveMinus1 = 0u;
        uint numRefIdxL1DefaultActiveMinus1 = 0u;
        bool weightedPredFlag = false;
        uint weightedBipredIdc = 0u;
        int picInitQpMinus26 = 0;
        int picInitQsMinus26 = 0;
        int chromaQpIndexOffset = 0;
        bool deblockingFilterControlPresentFlag = false;
        bool constrainedIntraPredFlag = false;
        bool redundantPicCntPresentFlag = false;
        bool transform8x8ModeFlag = false;
        bool picScalingMatrixPresentFlag = false;
        int secondChromaQpIndexOffset = 0;
        ScalingMatrices? matrix = null;
        Container8UInt32 runLengthMinus1 = default;

        if (numSliceGroupsMinus1 > 0)
        {
            sliceGroupMapType = reader.ReadUE();
            if (sliceGroupMapType == 0u)
            {
                for (int iGroup = 0; iGroup <= numSliceGroupsMinus1; iGroup++)
                {
                    runLengthMinus1[iGroup] = reader.ReadUE(); // run_length_minus1[iGroup]
                }
            }
            else if (sliceGroupMapType == 2u)
            {
                for (int iGroup = 0; iGroup < numSliceGroupsMinus1; iGroup++)
                {
                    topLeft[iGroup] = reader.ReadUE();
                    bottomRight[iGroup] = reader.ReadUE();
                }
            }
            else if (sliceGroupMapType is 3u or 4u or 5u)
            {
                sliceGroupChangeDirectionFlag = reader.ReadBit();
                sliceGroupChangeRateMinus1 = reader.ReadUE();
            }
            else if (sliceGroupMapType == 6u)
            {
                picSizeInMapUnitsMinus1 = reader.ReadUE();
                uint nBitsToRead = (uint)Math.Ceiling(Math.Log2(numSliceGroupsMinus1 + 1));

                for (int i = 0; i <= picSizeInMapUnitsMinus1; i++)
                    _ = reader.ReadBits(nBitsToRead); // slice_group_id[i]
            }
        }

        numRefIdxL0DefaultActiveMinus1 = reader.ReadUE();
        numRefIdxL1DefaultActiveMinus1 = reader.ReadUE();
        weightedPredFlag = reader.ReadBit();
        weightedBipredIdc = reader.ReadBits(2);
        picInitQpMinus26 = reader.ReadSE();
        picInitQsMinus26 = reader.ReadSE();
        chromaQpIndexOffset = reader.ReadSE();
        deblockingFilterControlPresentFlag = reader.ReadBit();
        constrainedIntraPredFlag = reader.ReadBit();
        redundantPicCntPresentFlag = reader.ReadBit();

        if (Util264.MoreRbspData(reader, nalLength))
        {
            transform8x8ModeFlag = reader.ReadBit();
            picScalingMatrixPresentFlag = reader.ReadBit();
            if (picScalingMatrixPresentFlag)
            {
                int length = 6 + ((sps.ChromaFormatIdc != 3) ? 2 : 6) * Int32Boolean.I32(transform8x8ModeFlag);
                matrix = new ScalingMatrices(reader.GetState(), length);

                for (int i = 0; i < length; i++)
                {
                    if (reader.ReadBit())
                    {
                        if (i < 6) _ = ScalingMatrices.ParseScalingList(reader, 16);
                        else       _ = ScalingMatrices.ParseScalingList(reader, 64);
                    }
                }
            }
            secondChromaQpIndexOffset = reader.ReadSE();
        }

        return new PictureParameterSet(
            spsId,
            ppsId,
            entropyCodingModeFlag,
            bottomFieldPicOrderInFramePresentFlag,
            numSliceGroupsMinus1,
            topLeft,
            bottomRight,
            sliceGroupChangeDirectionFlag,
            sliceGroupChangeRateMinus1,
            sliceGroupMapType,
            runLengthMinus1,
            picSizeInMapUnitsMinus1,
            numRefIdxL0DefaultActiveMinus1,
            numRefIdxL1DefaultActiveMinus1,
            weightedPredFlag,
            weightedBipredIdc,
            picInitQpMinus26,
            picInitQsMinus26,
            chromaQpIndexOffset,
            deblockingFilterControlPresentFlag,
            constrainedIntraPredFlag,
            redundantPicCntPresentFlag,
            secondChromaQpIndexOffset);
    }

    /// <summary>
    ///   Writes the PPS to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sliceGroupId"></param>
    /// <param name="moreRBSPData"></param>
    /// <param name="build"></param>
    public readonly void Write(BitStreamWriter writer, Span<uint> sliceGroupId, bool moreRBSPData, ScalingMatrixBuilder build)
    {
        writer.WriteUE(SpsId);
        writer.WriteUE(PpsId);
        writer.WriteBit(EntropyCodingModeFlag);
        writer.WriteBit(BottomFieldPicOrderInFramePresentFlag);
        writer.WriteUE(NumSliceGroupsMinus1);

        if (NumSliceGroupsMinus1 > 0)
        {
            writer.WriteUE(SliceGroupMapType);
            if (SliceGroupMapType == 0u)
            {
                for (int iGroup = 0; iGroup <= NumSliceGroupsMinus1; iGroup++)
                {
                    writer.WriteUE(RunLengthMinus1[iGroup]);
                }
            }
            else if (SliceGroupMapType == 2u)
            {
                for (int iGroup = 0; iGroup < NumSliceGroupsMinus1; iGroup++)
                {
                    writer.WriteUE(GetTopLeftFast(iGroup));
                    writer.WriteUE(GetBottomRightFast(iGroup));
                }
            }
            else if (SliceGroupMapType is 3u or 4u or 5u)
            {
                writer.WriteBit(SliceGroupChangeDirectionFlag);
                writer.WriteUE(SliceGroupChangeRateMinus1);
            }
            else if (SliceGroupMapType == 6u)
            {
                writer.WriteUE(PicSizeInMapUnitsMinus1);
                uint nBitsToRead = (uint)Math.Ceiling(Math.Log2(NumSliceGroupsMinus1 + 1));

                for (int i = 0; i <= PicSizeInMapUnitsMinus1; i++)
                    writer.WriteBits(sliceGroupId[i], nBitsToRead);
            }
        }

        writer.WriteUE(NumRefIdxL0DefaultActiveMinus1);
        writer.WriteUE(NumRefIdxL1DefaultActiveMinus1);
        writer.WriteBit(WeightedPredFlag);
        writer.WriteBits(WeightedBiPredIdc, 2);
        writer.WriteSE(PicInitQpMinus26);
        writer.WriteSE(PicInitQsMinus26);
        writer.WriteSE(ChromaQpIndexOffset);
        writer.WriteBit(DeblockingFilterControlPresentFlag);
        writer.WriteBit(ConstrainedIntraPredFlag);
        writer.WriteBit(RedundantPicCntPresentFlag);

        if (moreRBSPData)
        {
            writer.WriteBit(Transform8x8ModeFlag);
            writer.WriteBit(PicScalingMatrixPresentFlag);

            if (PicScalingMatrixPresentFlag)
            {
                for (int i = 0; i < this.ScalingMatrix!.Value.ListCount; i++)
                {
                    _Core(i, this.ScalingMatrix!.Value, writer, build);

                    static void _Core(int i, ScalingMatrices matrix, BitStreamWriter writer, ScalingMatrixBuilder builder)
                    {
                        Span<int> sp = stackalloc int[i < 6 ? 16 : 64];
                        builder.BuildSink(i, matrix.ListCount, sp, out bool present);

                        writer.WriteBit(present);
                        if (present)
                        {
                            for (int j = 0; j < matrix.ListCount; j++)
                            {
                                writer.WriteSE(sp[j]);
                            }
                        }
                    }
                }
            }

            writer.WriteSE(SecondChromaQpIndexOffset);
        }
    }

    /// <summary>
    ///   Writes the PPS to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sliceGroupId"></param>
    /// <param name="build"></param>
    public readonly void Write(BitStreamWriter writer, Span<uint> sliceGroupId, ScalingMatrixBuilder build) =>
        Write(writer, sliceGroupId, true, build);

    /// <summary>
    ///   Writes the PPS to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sliceGroupId"></param>
    /// <param name="moreRBSPData"></param>
    public readonly void Write(BitStreamWriter writer, Span<uint> sliceGroupId, bool moreRBSPData)
    {
        if (moreRBSPData && this.PicScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix builder not provided");

        Write(writer, sliceGroupId, moreRBSPData, BogusBuilder);
    }

    /// <summary>
    ///   Writes the PPS to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sliceGroupId"></param>
    /// <param name="moreRBSPData"></param>
    /// <param name="build"></param>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<uint> sliceGroupId, bool moreRBSPData, ScalingMatrixBuilder build)
    {
        await writer.WriteUEAsync(SpsId);
        await writer.WriteUEAsync(PpsId);
        await writer.WriteBitAsync(EntropyCodingModeFlag);
        await writer.WriteBitAsync(BottomFieldPicOrderInFramePresentFlag);
        await writer.WriteUEAsync(NumSliceGroupsMinus1);

        if (NumSliceGroupsMinus1 > 0)
        {
            await writer.WriteUEAsync(SliceGroupMapType);
            if (SliceGroupMapType == 0u)
            {
                for (int iGroup = 0; iGroup <= NumSliceGroupsMinus1; iGroup++)
                {
                    await writer.WriteUEAsync(RunLengthMinus1[iGroup]);
                }
            }
            else if (SliceGroupMapType == 2u)
            {
                for (int iGroup = 0; iGroup < NumSliceGroupsMinus1; iGroup++)
                {
                    await writer.WriteUEAsync(GetTopLeftFast(iGroup));
                    await writer.WriteUEAsync(GetBottomRightFast(iGroup));
                }
            }
            else if (SliceGroupMapType is 3u or 4u or 5u)
            {
                await writer.WriteBitAsync(SliceGroupChangeDirectionFlag);
                await writer.WriteUEAsync(SliceGroupChangeRateMinus1);
            }
            else if (SliceGroupMapType == 6u)
            {
                await writer.WriteUEAsync(PicSizeInMapUnitsMinus1);
                uint nBitsToRead = (uint)Math.Ceiling(Math.Log2(NumSliceGroupsMinus1 + 1));

                for (int i = 0; i <= PicSizeInMapUnitsMinus1; i++)
                    await writer.WriteBitsAsync(sliceGroupId.Span[i], nBitsToRead);
            }
        }

        await writer.WriteUEAsync(NumRefIdxL0DefaultActiveMinus1);
        await writer.WriteUEAsync(NumRefIdxL1DefaultActiveMinus1);
        await writer.WriteBitAsync(WeightedPredFlag);
        await writer.WriteBitsAsync(WeightedBiPredIdc, 2);
        await writer.WriteSEAsync(PicInitQpMinus26);
        await writer.WriteSEAsync(PicInitQsMinus26);
        await writer.WriteSEAsync(ChromaQpIndexOffset);
        await writer.WriteBitAsync(DeblockingFilterControlPresentFlag);
        await writer.WriteBitAsync(ConstrainedIntraPredFlag);
        await writer.WriteBitAsync(RedundantPicCntPresentFlag);

        if (moreRBSPData)
        {
            await writer.WriteBitAsync(Transform8x8ModeFlag);
            await writer.WriteBitAsync(PicScalingMatrixPresentFlag);

            if (PicScalingMatrixPresentFlag)
            {
                for (int i = 0; i < this.ScalingMatrix!.Value.ListCount; i++)
                {
                    _Core(i, this.ScalingMatrix!.Value, writer, build);

                    static void _Core(int i, ScalingMatrices matrix, BitStreamWriter writer, ScalingMatrixBuilder builder)
                    {
                        Span<int> sp = stackalloc int[i < 6 ? 16 : 64];
                        builder.BuildSink(i, matrix.ListCount, sp, out bool present);

                        writer.WriteBit(present);
                        if (present)
                        {
                            for (int j = 0; j < matrix.ListCount; j++)
                            {
                                writer.WriteSE(sp[j]);
                            }
                        }
                    }
                }
            }

            await writer.WriteSEAsync(SecondChromaQpIndexOffset);
        }
    }

    /// <summary>
    ///   Writes the PPS to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sliceGroupId"></param>
    /// <param name="build"></param>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<uint> sliceGroupId, ScalingMatrixBuilder build) =>
        await WriteAsync(writer, sliceGroupId, true, build);

    /// <summary>
    ///   Writes the PPS to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sliceGroupId"></param>
    /// <param name="moreRBSPData"></param>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<uint> sliceGroupId, bool moreRBSPData)
    {
        if (moreRBSPData && this.PicScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix builder not provided");

        await WriteAsync(writer, sliceGroupId, moreRBSPData, BogusBuilder);
    }

    /// <summary>
    ///   Returns the top-left value from <see cref="TopLeft1"/> to <see cref="TopLeft8"/>
    ///   using the given index. When the index is out of bounds or invalid, returns 0
    ///   instead of throwing an exception.
    /// </summary>
    /// <param name="i">Index of the top-left value.</param>
    /// <returns>Value of the top-left field by the given index.</returns>
    public readonly uint GetTopLeftFast(int i)
    {
        return i switch
        {
            0 => TopLeft1,
            1 => TopLeft2,
            2 => TopLeft3,
            3 => TopLeft4,
            4 => TopLeft5,
            5 => TopLeft6,
            6 => TopLeft7,
            7 => TopLeft8,
            _ => 0u
        };
    }

    /// <summary>
    ///   Sets the value of the top-left field from <see cref="TopLeft1"/>
    ///   to <see cref="TopLeft8"/> by the given index. Does not do anything
    ///   if index is invalid or out of bounds.
    /// </summary>
    /// <param name="i">Index of the top-left value to set.</param>
    /// <param name="value">Value to set to the top-left field.</param>
    public void SetTopLeftFast(int i, uint value)
    {
        if (i == 0) TopLeft1 = value;
        if (i == 1) TopLeft2 = value;
        if (i == 2) TopLeft3 = value;
        if (i == 3) TopLeft4 = value;
        if (i == 4) TopLeft5 = value;
        if (i == 5) TopLeft6 = value;
        if (i == 6) TopLeft7 = value;
        if (i == 7) TopLeft8 = value;
    }

    /// <summary>
    ///   Returns the bottom-right value from <see cref="BottomRight1"/> to <see cref="BottomRight8"/>
    ///   using the given index. When the index is out of bounds or invalid, returns 0
    ///   instead of throwing an exception.
    /// </summary>
    /// <param name="i">Index of the bottom-right value.</param>
    /// <returns>Value of the bottom-right field by the given index.</returns>
    public readonly uint GetBottomRightFast(int i)
    {
        return i switch
        {
            0 => BottomRight1,
            1 => BottomRight2,
            2 => BottomRight3,
            3 => BottomRight4,
            4 => BottomRight5,
            5 => BottomRight6,
            6 => BottomRight7,
            7 => BottomRight8,
            _ => 0u
        };
    }

    /// <summary>
    ///   Sets the value of the bottom-right field from <see cref="BottomRight1"/>
    ///   to <see cref="BottomRight8"/> by the given index. Does not do anything
    ///   if index is invalid or out of bounds.
    /// </summary>
    /// <param name="i">Index of the bottom-right value to set.</param>
    /// <param name="value">Value to set to the bottom-right field.</param>
    public void SetBottomRightFast(int i, uint value)
    {
        if (i == 0) BottomRight1 = value;
        if (i == 1) BottomRight2 = value;
        if (i == 2) BottomRight3 = value;
        if (i == 3) BottomRight4 = value;
        if (i == 4) BottomRight5 = value;
        if (i == 5) BottomRight6 = value;
        if (i == 6) BottomRight7 = value;
        if (i == 7) BottomRight8 = value;
    }

    /// <summary>  
    /// Determines whether the specified object is equal to the current instance.  
    /// </summary>  
    /// <param name="obj">The object to compare with the current instance.</param>  
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>  
    public readonly override bool Equals(object? obj)
    {
        return obj is PictureParameterSet set && Equals(set);
    }

    /// <summary>  
    /// Determines whether the specified <see cref="PictureParameterSet"/> is equal to the current instance.  
    /// </summary>  
    /// <param name="other">The <see cref="PictureParameterSet"/> to compare with the current instance.</param>  
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>  
    public readonly bool Equals(PictureParameterSet other)
    {
        return Kind == other.Kind &&
               SpsId == other.SpsId &&
               PpsId == other.PpsId &&
               EntropyCodingModeFlag == other.EntropyCodingModeFlag &&
               BottomFieldPicOrderInFramePresentFlag == other.BottomFieldPicOrderInFramePresentFlag &&
               NumSliceGroupsMinus1 == other.NumSliceGroupsMinus1 &&
               TopLeft1 == other.TopLeft1 &&
               TopLeft2 == other.TopLeft2 &&
               TopLeft3 == other.TopLeft3 &&
               TopLeft4 == other.TopLeft4 &&
               TopLeft5 == other.TopLeft5 &&
               TopLeft6 == other.TopLeft6 &&
               TopLeft7 == other.TopLeft7 &&
               TopLeft8 == other.TopLeft8 &&
               BottomRight1 == other.BottomRight1 &&
               BottomRight2 == other.BottomRight2 &&
               BottomRight3 == other.BottomRight3 &&
               BottomRight4 == other.BottomRight4 &&
               BottomRight5 == other.BottomRight5 &&
               BottomRight6 == other.BottomRight6 &&
               BottomRight7 == other.BottomRight7 &&
               BottomRight8 == other.BottomRight8 &&
               SliceGroupChangeDirectionFlag == other.SliceGroupChangeDirectionFlag &&
               SliceGroupChangeRateMinus1 == other.SliceGroupChangeRateMinus1 &&
               SliceGroupMapType == other.SliceGroupMapType &&
               RunLengthMinus1 == other.RunLengthMinus1 &&
               PicSizeInMapUnitsMinus1 == other.PicSizeInMapUnitsMinus1 &&
               NumRefIdxL0DefaultActiveMinus1 == other.NumRefIdxL0DefaultActiveMinus1 &&
               NumRefIdxL1DefaultActiveMinus1 == other.NumRefIdxL1DefaultActiveMinus1 &&
               WeightedPredFlag == other.WeightedPredFlag &&
               WeightedBiPredIdc == other.WeightedBiPredIdc &&
               PicInitQpMinus26 == other.PicInitQpMinus26 &&
               PicInitQsMinus26 == other.PicInitQsMinus26 &&
               ChromaQpIndexOffset == other.ChromaQpIndexOffset &&
               DeblockingFilterControlPresentFlag == other.DeblockingFilterControlPresentFlag &&
               ConstrainedIntraPredFlag == other.ConstrainedIntraPredFlag &&
               RedundantPicCntPresentFlag == other.RedundantPicCntPresentFlag &&
               Transform8x8ModeFlag == other.Transform8x8ModeFlag &&
               PicScalingMatrixPresentFlag == other.PicScalingMatrixPresentFlag &&
               EqualityComparer<ScalingMatrices?>.Default.Equals(ScalingMatrix, other.ScalingMatrix) &&
               SecondChromaQpIndexOffset == other.SecondChromaQpIndexOffset;
    }

    /// <summary>
    ///   Determines the hash code for the PPS.
    /// </summary>
    /// <returns>PPS hash code.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Kind);
        hash.Add(SpsId);
        hash.Add(PpsId);
        hash.Add(EntropyCodingModeFlag);
        hash.Add(BottomFieldPicOrderInFramePresentFlag);
        hash.Add(NumSliceGroupsMinus1);
        hash.Add(TopLeft1);
        hash.Add(TopLeft2);
        hash.Add(TopLeft3);
        hash.Add(TopLeft4);
        hash.Add(TopLeft5);
        hash.Add(TopLeft6);
        hash.Add(TopLeft7);
        hash.Add(TopLeft8);
        hash.Add(BottomRight1);
        hash.Add(BottomRight2);
        hash.Add(BottomRight3);
        hash.Add(BottomRight4);
        hash.Add(BottomRight5);
        hash.Add(BottomRight6);
        hash.Add(BottomRight7);
        hash.Add(BottomRight8);
        hash.Add(SliceGroupChangeDirectionFlag);
        hash.Add(SliceGroupChangeRateMinus1);
        hash.Add(SliceGroupMapType);
        hash.Add(RunLengthMinus1);
        hash.Add(PicSizeInMapUnitsMinus1);
        hash.Add(NumRefIdxL0DefaultActiveMinus1);
        hash.Add(NumRefIdxL1DefaultActiveMinus1);
        hash.Add(WeightedPredFlag);
        hash.Add(WeightedBiPredIdc);
        hash.Add(PicInitQpMinus26);
        hash.Add(PicInitQsMinus26);
        hash.Add(ChromaQpIndexOffset);
        hash.Add(DeblockingFilterControlPresentFlag);
        hash.Add(ConstrainedIntraPredFlag);
        hash.Add(RedundantPicCntPresentFlag);
        hash.Add(Transform8x8ModeFlag);
        hash.Add(PicScalingMatrixPresentFlag);
        hash.Add(ScalingMatrix);
        hash.Add(SecondChromaQpIndexOffset);
        return hash.ToHashCode();
    }


    /// <summary>  
    /// Determines whether two <see cref="PictureParameterSet"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="PictureParameterSet"/> to compare.</param>  
    /// <param name="right">The second <see cref="PictureParameterSet"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(PictureParameterSet left, PictureParameterSet right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    /// Determines whether two <see cref="PictureParameterSet"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="PictureParameterSet"/> to compare.</param>  
    /// <param name="right">The second <see cref="PictureParameterSet"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(PictureParameterSet left, PictureParameterSet right)
    {
        return !(left == right);
    }
}
