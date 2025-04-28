using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents a Sequence Parameter Set (SPS) model. This structure contains raw data.
/// </summary>
public struct SequenceParameterSet : IParameterSet, IEquatable<SequenceParameterSet>
{
    /// <summary>
    ///   Gets the kind of parameter set, which is always <see cref="ParameterSetKind.Sequence" />.
    /// </summary>
    public readonly ParameterSetKind Kind => ParameterSetKind.Sequence;

    /// <summary>
    ///   Indicates the profile IDC for the sequence.
    /// </summary>
    public uint ProfileIdc;

    /// <summary>
    ///   Constraint Set 0 flag for profile compatibility.
    /// </summary>
    public bool ConstraintSet0Flag;

    /// <summary>
    ///   Constraint Set 1 flag for profile compatibility.
    /// </summary>
    public bool ConstraintSet1Flag;

    /// <summary>
    ///   Constraint Set 2 flag for profile compatibility.
    /// </summary>
    public bool ConstraintSet2Flag;

    /// <summary>
    ///   Constraint Set 3 flag for profile compatibility.
    /// </summary>
    public bool ConstraintSet3Flag;

    /// <summary>
    ///   Constraint Set 4 flag for profile compatibility.
    /// </summary>
    public bool ConstraintSet4Flag;

    /// <summary>
    ///   Constraint Set 5 flag for profile compatibility.
    /// </summary>
    public bool ConstraintSet5Flag;

    /// <summary>
    ///   Reserved bits (default value: 0).
    /// </summary>
    public uint ReservedZero2Bits;

    /// <summary>
    ///   Indicates the level IDC for the sequence.
    /// </summary>
    public uint LevelIdc;

    /// <summary>
    ///   Sequence parameter set identifier.
    /// </summary>
    public uint SpsId;

    /// <summary>
    ///   Specifies the chroma format (e.g., 4:2:0, 4:2:2).
    /// </summary>
    public uint ChromaFormatIdc;

    /// <summary>
    ///   Indicates whether separate color planes are used.
    /// </summary>
    public bool SeparateColourPlaneFlag;

    /// <summary>
    ///   Luma bit depth minus 8.
    /// </summary>
    public uint BitDepthLumaMinus8;

    /// <summary>
    ///   Chroma bit depth minus 8.
    /// </summary>
    public uint BitDepthChromaMinus8;

    /// <summary>
    ///   Flag for bypassing zero-transform for Qpprime_Y.
    /// </summary>
    public bool QpprimeYZeroTransformBypassFlag;

    /// <summary>
    ///   Indicates if a scaling matrix is present.
    /// </summary>
    public bool SeqScalingMatrixPresentFlag;

    /// <summary>
    ///   The scaling matrix. This is <see langword="null"/> if <see cref="SeqScalingMatrixPresentFlag"/>
    ///   is <see langword="true"/>.
    /// </summary>
    public ScalingMatrices? ScalingMatrix;

    /// <summary>
    ///   Represents Log2(MaxFrameNum) minus 4.
    /// </summary>
    public uint Log2MaxFrameNumMinus4;

    /// <summary>
    ///   Indicates the picture order count type.
    /// </summary>
    public uint PicOrderCntType;

    /// <summary>
    ///   Represents Log2(MaxPicOrderCntLsb) minus 4.
    /// </summary>
    public uint Log2MaxPicOrderCntLsbMinus4;

    /// <summary>
    ///   Indicates if Delta Pic Order is always zero.
    /// </summary>
    public bool DeltaPicOrderAlwaysZeroFlag;

    /// <summary>
    ///   Offset for non-reference picture in POC.
    /// </summary>
    public int OffsetForNonRefPic;

    /// <summary>
    ///   Offset for top-to-bottom field in POC.
    /// </summary>
    public int OffsetForTopToBottomField;

    /// <summary>
    ///   Number of reference frames in POC cycle.
    /// </summary>
    public uint NumRefFramesInPicOrderCntCycle;

    /// <summary>
    ///   Maximum number of reference frames.
    /// </summary>
    public uint MaxNumRefFrames;

    /// <summary>
    ///   Indicates if gaps in frame number values are allowed.
    /// </summary>
    public bool GapsInFrameNumValueAllowedFlag;

    /// <summary>
    ///   Picture width in macroblocks minus 1.
    /// </summary>
    public uint PicWidthInMbsMinus1;

    /// <summary>
    ///   Picture height in map units minus 1.
    /// </summary>
    public uint PicHeightInMapUnitsMinus1;

    /// <summary>
    ///   Indicates if frames consist only of macroblocks.
    /// </summary>
    public bool FrameMbsOnlyFlag;

    /// <summary>
    ///   Indicates if MB adaptive frame field mode is enabled.
    /// </summary>
    public bool MbAdaptiveFrameFieldFlag;

    /// <summary>
    ///   Indicates if direct 8x8 inference mode is enabled.
    /// </summary>
    public bool Direct8X8InferenceFlag;

    /// <summary>
    ///   Indicates if frame cropping is enabled.
    /// </summary>
    public bool FrameCroppingFlag;

    /// <summary>
    ///   Specifies the left frame cropping offset.
    /// </summary>
    public uint FrameCropLeftOffset;

    /// <summary>
    ///   Specifies the right frame cropping offset.
    /// </summary>
    public uint FrameCropRightOffset;

    /// <summary>
    ///   Specifies the top frame cropping offset.
    /// </summary>
    public uint FrameCropTopOffset;

    /// <summary>
    ///   Specifies the bottom frame cropping offset.
    /// </summary>
    public uint FrameCropBottomOffset;

    /// <summary>
    ///   Indicates if VUI parameters are present.
    /// </summary>
    public bool VuiParametersPresentFlag;

    /// <summary>
    ///   VUI parameters. If <see cref="VuiParametersPresentFlag"/> is <see langword="false"/>,
    ///   the value of this field will be <see langword="null"/>.
    /// </summary>
    public VuiParameters? VuiParameters;

    /// <summary>
    ///   Initializes a new instance of the <see cref="SequenceParameterSet"/> structure.
    /// </summary>
    /// <param name="profileIdc">Part of the SPS</param>
    /// <param name="constraintSet0Flag">Part of the SPS</param>
    /// <param name="constraintSet1Flag">Part of the SPS</param>
    /// <param name="constraintSet2Flag">Part of the SPS</param>
    /// <param name="constraintSet3Flag">Part of the SPS</param>
    /// <param name="constraintSet4Flag">Part of the SPS</param>
    /// <param name="constraintSet5Flag">Part of the SPS</param>
    /// <param name="reservedZero2Bits">Part of the SPS</param>
    /// <param name="levelIdc">Part of the SPS</param>
    /// <param name="spsId">Part of the SPS</param>
    /// <param name="chromaFormatIdc">Part of the SPS</param>
    /// <param name="separateColourPlaneFlag">Part of the SPS</param>
    /// <param name="bitDepthLumaMinus8">Part of the SPS</param>
    /// <param name="bitDepthChromaMinus8">Part of the SPS</param>
    /// <param name="qpprimeYZeroTransformBypassFlag">Part of the SPS</param>
    /// <param name="seqScalingMatrixPresentFlag">Part of the SPS</param>
    /// <param name="scalingMatrix">Part of the SPS</param>
    /// <param name="log2MaxFrameNumMinus4">Part of the SPS</param>
    /// <param name="picOrderCntType">Part of the SPS</param>
    /// <param name="log2MaxPicOrderCntLsbMinus4">Part of the SPS</param>
    /// <param name="deltaPicOrderAlwaysZeroFlag">Part of the SPS</param>
    /// <param name="offsetForNonRefPic">Part of the SPS</param>
    /// <param name="offsetForTopToBottomField">Part of the SPS</param>
    /// <param name="numRefFramesInPicOrderCntCycle">Part of the SPS</param>
    /// <param name="maxNumRefFrames">Part of the SPS</param>
    /// <param name="gapsInFrameNumValueAllowedFlag">Part of the SPS</param>
    /// <param name="picWidthInMbsMinus1">Part of the SPS</param>
    /// <param name="picHeightInMapUnitsMinus1">Part of the SPS</param>
    /// <param name="frameMbsOnlyFlag">Part of the SPS</param>
    /// <param name="mbAdaptiveFrameFieldFlag">Part of the SPS</param>
    /// <param name="direct8X8InferenceFlag">Part of the SPS</param>
    /// <param name="frameCroppingFlag">Part of the SPS</param>
    /// <param name="frameCropLeftOffset">Part of the SPS</param>
    /// <param name="frameCropRightOffset">Part of the SPS</param>
    /// <param name="frameCropTopOffset">Part of the SPS</param>
    /// <param name="frameCropBottomOffset">Part of the SPS</param>
    /// <param name="vuiParametersPresentFlag">Part of the SPS</param>
    /// <param name="vuiParameters">Part of the SPS</param>
    public SequenceParameterSet(uint profileIdc, bool constraintSet0Flag, bool constraintSet1Flag, bool constraintSet2Flag, bool constraintSet3Flag, bool constraintSet4Flag, bool constraintSet5Flag, uint reservedZero2Bits, uint levelIdc, uint spsId, uint chromaFormatIdc, bool separateColourPlaneFlag, uint bitDepthLumaMinus8, uint bitDepthChromaMinus8, bool qpprimeYZeroTransformBypassFlag, bool seqScalingMatrixPresentFlag, ScalingMatrices? scalingMatrix, uint log2MaxFrameNumMinus4, uint picOrderCntType, uint log2MaxPicOrderCntLsbMinus4, bool deltaPicOrderAlwaysZeroFlag, int offsetForNonRefPic, int offsetForTopToBottomField, uint numRefFramesInPicOrderCntCycle, uint maxNumRefFrames, bool gapsInFrameNumValueAllowedFlag, uint picWidthInMbsMinus1, uint picHeightInMapUnitsMinus1, bool frameMbsOnlyFlag, bool mbAdaptiveFrameFieldFlag, bool direct8X8InferenceFlag, bool frameCroppingFlag, uint frameCropLeftOffset, uint frameCropRightOffset, uint frameCropTopOffset, uint frameCropBottomOffset, bool vuiParametersPresentFlag, VuiParameters? vuiParameters)
    {
        ProfileIdc = profileIdc;
        ConstraintSet0Flag = constraintSet0Flag;
        ConstraintSet1Flag = constraintSet1Flag;
        ConstraintSet2Flag = constraintSet2Flag;
        ConstraintSet3Flag = constraintSet3Flag;
        ConstraintSet4Flag = constraintSet4Flag;
        ConstraintSet5Flag = constraintSet5Flag;
        ReservedZero2Bits = reservedZero2Bits;
        LevelIdc = levelIdc;
        SpsId = spsId;
        ChromaFormatIdc = chromaFormatIdc;
        SeparateColourPlaneFlag = separateColourPlaneFlag;
        BitDepthLumaMinus8 = bitDepthLumaMinus8;
        BitDepthChromaMinus8 = bitDepthChromaMinus8;
        QpprimeYZeroTransformBypassFlag = qpprimeYZeroTransformBypassFlag;
        SeqScalingMatrixPresentFlag = seqScalingMatrixPresentFlag;
        ScalingMatrix = scalingMatrix;
        Log2MaxFrameNumMinus4 = log2MaxFrameNumMinus4;
        PicOrderCntType = picOrderCntType;
        Log2MaxPicOrderCntLsbMinus4 = log2MaxPicOrderCntLsbMinus4;
        DeltaPicOrderAlwaysZeroFlag = deltaPicOrderAlwaysZeroFlag;
        OffsetForNonRefPic = offsetForNonRefPic;
        OffsetForTopToBottomField = offsetForTopToBottomField;
        NumRefFramesInPicOrderCntCycle = numRefFramesInPicOrderCntCycle;
        MaxNumRefFrames = maxNumRefFrames;
        GapsInFrameNumValueAllowedFlag = gapsInFrameNumValueAllowedFlag;
        PicWidthInMbsMinus1 = picWidthInMbsMinus1;
        PicHeightInMapUnitsMinus1 = picHeightInMapUnitsMinus1;
        FrameMbsOnlyFlag = frameMbsOnlyFlag;
        MbAdaptiveFrameFieldFlag = mbAdaptiveFrameFieldFlag;
        Direct8X8InferenceFlag = direct8X8InferenceFlag;
        FrameCroppingFlag = frameCroppingFlag;
        FrameCropLeftOffset = frameCropLeftOffset;
        FrameCropRightOffset = frameCropRightOffset;
        FrameCropTopOffset = frameCropTopOffset;
        FrameCropBottomOffset = frameCropBottomOffset;
        VuiParametersPresentFlag = vuiParametersPresentFlag;
        VuiParameters = vuiParameters;
    }

    /// <summary>
    /// Parses the SPS from the bitstream.
    /// </summary>
    /// <param name="reader">Bitstream reader to parse from</param>
    /// <returns>An SPS</returns>
    public static SequenceParameterSet Read(BitStreamReader reader)
    {
        uint profileIdc = reader.ReadBits(8);
        bool constraintSet0Flag = reader.ReadBit();
        bool constraintSet1Flag = reader.ReadBit();
        bool constraintSet2Flag = reader.ReadBit();
        bool constraintSet3Flag = reader.ReadBit();
        bool constraintSet4Flag = reader.ReadBit();
        bool constraintSet5Flag = reader.ReadBit();
        uint reservedZero2Bits = reader.ReadBits(2);
        uint levelIdc = reader.ReadBits(8);
        uint spsId = reader.ReadUE();
        uint chromaFormatIdc = 0u;
        bool separateColourPlaneFlag = false;
        uint bitDepthLumaMinus8 = 0u;
        uint bitDepthChromaMinus8 = 0u;
        bool qpPrimeYZeroTransformBypassFlag = false;
        bool seqScalingMatrixPresentFlag = false;
        ScalingMatrices? scalingMatrix = null;
        if (profileIdc == 100 || profileIdc == 110 ||
            profileIdc == 122 || profileIdc == 244 || profileIdc == 44 ||
            profileIdc == 83 || profileIdc == 86 || profileIdc == 118 ||
            profileIdc == 128 || profileIdc == 138 || profileIdc == 139 ||
            profileIdc == 134 || profileIdc == 135)
        {
            chromaFormatIdc = reader.ReadUE();
            separateColourPlaneFlag = false;
            if (chromaFormatIdc == 3)
                separateColourPlaneFlag = reader.ReadBit();
            bitDepthLumaMinus8 = reader.ReadUE();
            bitDepthChromaMinus8 = reader.ReadUE();
            qpPrimeYZeroTransformBypassFlag = reader.ReadBit();
            seqScalingMatrixPresentFlag = reader.ReadBit();
            scalingMatrix = new ScalingMatrices(reader.GetState(), (chromaFormatIdc != 3 ? 8 : 12));
            if (seqScalingMatrixPresentFlag)
            {
                for (int i = 0; i < scalingMatrix.Value.ListCount; i++)
                {
                    bool seqScalingListPresentFlag = reader.ReadBit();
                    if (seqScalingListPresentFlag)
                    {
                        if (i < 6)
                        {
                            _ = ScalingMatrices.ParseScalingList(reader, 16);
                        }
                        else
                        {
                            _ = ScalingMatrices.ParseScalingList(reader, 64);
                        }
                    }
                }
            }
        }
        uint log2MaxFrameNumMinus4 = reader.ReadUE();
        uint picOrderCntType = reader.ReadUE();
        uint log2MaxPicOrderCntLsbMinus4 = 0u;
        if (picOrderCntType == 0u)
            log2MaxPicOrderCntLsbMinus4 = reader.ReadUE();
        bool deltaPicOrderAlwaysZeroFlag = false;
        int offsetForNonRefPic = 0;
        int offsetForTopToBottomField = 0;
        uint numRefFramesInPicOrderCntCycle = 0u;
        if (picOrderCntType == 1u)
        {
            deltaPicOrderAlwaysZeroFlag = reader.ReadBit();
            offsetForNonRefPic = reader.ReadSE();
            offsetForTopToBottomField = reader.ReadSE();
            numRefFramesInPicOrderCntCycle = reader.ReadUE();
            for (int i = 0; i < numRefFramesInPicOrderCntCycle; i++)
                _ = reader.ReadSE(); // offset_for_ref_frame[i] = SE
        }
        uint maxNumRefFrames = reader.ReadUE();
        bool gapsInFrameNumValueAllowedFlag = reader.ReadBit();
        uint picWidthInMbsMinus1 = reader.ReadUE();
        uint picHeightInMapUnitsMinus1 = reader.ReadUE();
        bool frameMbsOnlyFlag = reader.ReadBit();
        bool mbAdaptiveFrameFieldFlag = false;
        if (!frameMbsOnlyFlag)
            mbAdaptiveFrameFieldFlag = reader.ReadBit();
        bool direct8x8InferenceFlag = reader.ReadBit();
        bool frameCroppingFlag = reader.ReadBit();
        uint frameCropLeftOffset = 0u;
        uint frameCropRightOffset = 0u;
        uint frameCropTopOffset = 0u;
        uint frameCropBottomOffset = 0u;
        if (frameCroppingFlag)
        {
            frameCropLeftOffset = reader.ReadUE();
            frameCropRightOffset = reader.ReadUE();
            frameCropTopOffset = reader.ReadUE();
            frameCropBottomOffset = reader.ReadUE();
        }
        bool vuiParametersPresentFlag = reader.ReadBit();
        VuiParameters? vui = null;
        if (vuiParametersPresentFlag)
            vui = Models.VuiParameters.Read(reader);

        return new(
            profileIdc,
            constraintSet0Flag,
            constraintSet1Flag,
            constraintSet2Flag,
            constraintSet3Flag,
            constraintSet4Flag,
            constraintSet5Flag,
            reservedZero2Bits,
            levelIdc,
            spsId,
            chromaFormatIdc,
            separateColourPlaneFlag,
            bitDepthLumaMinus8,
            bitDepthChromaMinus8,
            qpPrimeYZeroTransformBypassFlag,
            seqScalingMatrixPresentFlag,
            scalingMatrix,
            log2MaxFrameNumMinus4,
            picOrderCntType,
            log2MaxPicOrderCntLsbMinus4,
            deltaPicOrderAlwaysZeroFlag,
            offsetForNonRefPic,
            offsetForTopToBottomField,
            numRefFramesInPicOrderCntCycle,
            maxNumRefFrames,
            gapsInFrameNumValueAllowedFlag,
            picWidthInMbsMinus1,
            picHeightInMapUnitsMinus1,
            frameMbsOnlyFlag,
            mbAdaptiveFrameFieldFlag,
            direct8x8InferenceFlag,
            frameCroppingFlag,
            frameCropLeftOffset,
            frameCropRightOffset,
            frameCropTopOffset,
            frameCropBottomOffset,
            vuiParametersPresentFlag,
            vui
        );
    }

    
    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames are required when <see cref="PicOrderCntType"/> is 1.</param>
    /// <param name="vuiWriteOptions">VUI write options, if <see cref="VuiParametersPresentFlag"/> is true.</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Span<int> offsetForRefFrames, VuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder)
    {
        PrepareForWrite(builder, offsetForRefFrames);

        writer.WriteBits(ProfileIdc, 8);
        writer.WriteBit(ConstraintSet0Flag);
        writer.WriteBit(ConstraintSet1Flag);
        writer.WriteBit(ConstraintSet2Flag);
        writer.WriteBit(ConstraintSet3Flag);
        writer.WriteBit(ConstraintSet4Flag);
        writer.WriteBit(ConstraintSet5Flag);
        writer.WriteBits(ReservedZero2Bits, 2);
        writer.WriteBits(LevelIdc, 8);
        writer.WriteUE(SpsId);

        if (ProfileIdc == 100 || ProfileIdc == 110 ||
            ProfileIdc == 122 || ProfileIdc == 244 || ProfileIdc == 44 ||
            ProfileIdc == 83 || ProfileIdc == 86 || ProfileIdc == 118 ||
            ProfileIdc == 128 || ProfileIdc == 138 || ProfileIdc == 139 ||
            ProfileIdc == 134 || ProfileIdc == 135)
        {
            writer.WriteUE(ChromaFormatIdc);
            if (ChromaFormatIdc == 3)
                writer.WriteBit(SeparateColourPlaneFlag);

            writer.WriteUE(BitDepthLumaMinus8);
            writer.WriteUE(BitDepthChromaMinus8);
            writer.WriteBit(QpprimeYZeroTransformBypassFlag);
            writer.WriteBit(SeqScalingMatrixPresentFlag);

            if (SeqScalingMatrixPresentFlag)
            {
                for (int i = 0; i < (ChromaFormatIdc != 3 ? 8 : 12); i++)
                {
                    _Core();

                    void _Core()
                    {
                        int elements = ScalingMatrices.GetListLength(i);
                        Span<int> buffer = stackalloc int[elements];

                        builder!.BuildSink(i, elements, buffer, out bool isPresent);
                        writer.WriteBit(isPresent);
                        if (isPresent)
                        {
                            for (int i = 0; i < elements; i++)
                                writer.WriteSE(buffer[i]);
                        }
                    }
                }
            }
        }
        writer.WriteUE(Log2MaxFrameNumMinus4);
        writer.WriteUE(PicOrderCntType);

        if (PicOrderCntType == 0u)
            writer.WriteUE(Log2MaxPicOrderCntLsbMinus4);

        if (PicOrderCntType == 1u)
        {
            writer.WriteBit(DeltaPicOrderAlwaysZeroFlag);
            writer.WriteSE(OffsetForNonRefPic);
            writer.WriteSE(OffsetForTopToBottomField);
            writer.WriteUE(NumRefFramesInPicOrderCntCycle);
            for (int i = 0; i < NumRefFramesInPicOrderCntCycle; i++)
                writer.WriteSE(offsetForRefFrames[i]);
        }

        writer.WriteUE(MaxNumRefFrames);
        writer.WriteBit(GapsInFrameNumValueAllowedFlag);
        writer.WriteUE(PicWidthInMbsMinus1);
        writer.WriteUE(PicHeightInMapUnitsMinus1);
        writer.WriteBit(FrameMbsOnlyFlag);

        if (!FrameMbsOnlyFlag)
            writer.WriteBit(MbAdaptiveFrameFieldFlag);

        writer.WriteBit(Direct8X8InferenceFlag);
        writer.WriteBit(FrameCroppingFlag);

        if (FrameCroppingFlag)
        {
            writer.WriteUE(FrameCropLeftOffset);
            writer.WriteUE(FrameCropRightOffset);
            writer.WriteUE(FrameCropTopOffset);
            writer.WriteUE(FrameCropBottomOffset);
        }

        writer.WriteBit(VuiParametersPresentFlag);
        
        if (VuiParametersPresentFlag)
        {
            // We already performed a null check, VuiParameters
            // cannot be null here.
            this.VuiParameters!.Value.Write(writer, vuiWriteOptions);
        }
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="options">VUI write options, if <see cref="VuiParametersPresentFlag"/> is true.</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, VuiWriteOptions options, ScalingMatrixBuilder? builder)
    {
        if (this.PicOrderCntType == 1u)
            throw new InvalidOperationException("PicOrderCntType is 1 but offset for ref frames isn't provided");

        Span<int> span = stackalloc int[1];
        Write(writer, span, options, builder);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Span<int> offsetForRefFrames, ScalingMatrixBuilder? builder)
    {
        if (this.VuiParametersPresentFlag)
            throw new InvalidOperationException("VUI parameters are present");

        Span<uint> sp1x1 = stackalloc uint[1];
        Span<uint> sp1x2 = stackalloc uint[1];
        Span<bool> sp1x3 = stackalloc bool[1];

        Span<uint> sp2x1 = stackalloc uint[1];
        Span<uint> sp2x2 = stackalloc uint[1];
        Span<bool> sp2x3 = stackalloc bool[1];

        var writeOptions = new VuiWriteOptions(new HrdWriteOptions(sp1x1, sp1x2, sp1x3), new HrdWriteOptions(sp2x1, sp2x2, sp2x3), false, false);

        Write(writer, offsetForRefFrames, writeOptions, builder);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames</param>
    /// <param name="options">VUI write options.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Span<int> offsetForRefFrames, VuiWriteOptions options)
    {
        if (this.SeqScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix is present but is not provided");

        Write(writer, offsetForRefFrames, options, null);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Span<int> offsetForRefFrames)
    {
        if (this.SeqScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix is present but is not provided");

        Write(writer, offsetForRefFrames, null);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="vuiwriteOptions">VUI write options</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, VuiWriteOptions vuiwriteOptions)
    {
        if (this.SeqScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix is present but is not provided");

        Span<int> offsetForRefFrames = stackalloc int[1];
        Write(writer, offsetForRefFrames, vuiwriteOptions);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="builder">Scaling matrix builder</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, ScalingMatrixBuilder builder)
    {
        if (this.SeqScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix is present but is not provided");

        Span<int> offsetForRefFrames = stackalloc int[1];
        Write(writer, offsetForRefFrames, builder);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="vuiWriteOptions">Writing options for the VUI.</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, MemoryVuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder)
    {
        if (this.PicOrderCntType == 1u)
            throw new InvalidOperationException("PicOrderCntType is 1 but offset for ref frames isn't provided");

        VuiWriteOptions spanVuiWriteOptions = new(
            new HrdWriteOptions(vuiWriteOptions.NalHrdWriteOptions.BitRateValueMinus1.Span, vuiWriteOptions.NalHrdWriteOptions.CpbSizeValueMinus1.Span, vuiWriteOptions.NalHrdWriteOptions.CbrFlag.Span),
            new HrdWriteOptions(vuiWriteOptions.VclHrdWriteOptions.BitRateValueMinus1.Span, vuiWriteOptions.VclHrdWriteOptions.CpbSizeValueMinus1.Span, vuiWriteOptions.VclHrdWriteOptions.CbrFlag.Span),
            vuiWriteOptions.IsNalPresent,
            vuiWriteOptions.IsVclPresent);

        Span<int> span = stackalloc int[1];
        Write(writer, span, spanVuiWriteOptions, builder);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Memory<int> offsetForRefFrames, ScalingMatrixBuilder? builder)
    {
        Write(writer, offsetForRefFrames.Span, builder);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames</param>
    /// <param name="options">VUI write options.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Memory<int> offsetForRefFrames, VuiWriteOptions options)
    {
        Write(writer, offsetForRefFrames.Span, options, null);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, Memory<int> offsetForRefFrames)
    {
        if (this.SeqScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix is present but is not provided");

        Write(writer, offsetForRefFrames.Span, null);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="vuiWriteOptions">VUI write options</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly void Write(BitStreamWriter writer, MemoryVuiWriteOptions vuiWriteOptions)
    {
        if (this.SeqScalingMatrixPresentFlag)
            throw new InvalidOperationException("Scaling matrix is present but is not provided");

        VuiWriteOptions spanVuiWriteOptions = new(
            new HrdWriteOptions(vuiWriteOptions.NalHrdWriteOptions.BitRateValueMinus1.Span, vuiWriteOptions.NalHrdWriteOptions.CpbSizeValueMinus1.Span, vuiWriteOptions.NalHrdWriteOptions.CbrFlag.Span),
            new HrdWriteOptions(vuiWriteOptions.VclHrdWriteOptions.BitRateValueMinus1.Span, vuiWriteOptions.VclHrdWriteOptions.CpbSizeValueMinus1.Span, vuiWriteOptions.VclHrdWriteOptions.CbrFlag.Span),
            vuiWriteOptions.IsNalPresent,
            vuiWriteOptions.IsVclPresent);

        Span<int> offsetForRefFrames = stackalloc int[1];
        Write(writer, offsetForRefFrames, spanVuiWriteOptions, null);
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="offsetForRefFrames">Offset for ref frames are required when <see cref="PicOrderCntType"/> is 1.</param>
    /// <param name="vuiWriteOptions">VUI write options, if <see cref="VuiParametersPresentFlag"/> is true.</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<int> offsetForRefFrames, MemoryVuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder)
    {
        PrepareForWrite(builder, offsetForRefFrames.Span);

        await writer.WriteBitsAsync(ProfileIdc, 8);
        await writer.WriteBitAsync(ConstraintSet0Flag);
        await writer.WriteBitAsync(ConstraintSet1Flag);
        await writer.WriteBitAsync(ConstraintSet2Flag);
        await writer.WriteBitAsync(ConstraintSet3Flag);
        await writer.WriteBitAsync(ConstraintSet4Flag);
        await writer.WriteBitAsync(ConstraintSet5Flag);
        await writer.WriteBitsAsync(ReservedZero2Bits, 2);
        await writer.WriteBitsAsync(LevelIdc, 8);
        await writer.WriteUEAsync(SpsId);

        if (ProfileIdc == 100 || ProfileIdc == 110 ||
            ProfileIdc == 122 || ProfileIdc == 244 || ProfileIdc == 44 ||
            ProfileIdc == 83 || ProfileIdc == 86 || ProfileIdc == 118 ||
            ProfileIdc == 128 || ProfileIdc == 138 || ProfileIdc == 139 ||
            ProfileIdc == 134 || ProfileIdc == 135)
        {
            await writer.WriteUEAsync(ChromaFormatIdc);
            if (ChromaFormatIdc == 3)
                await writer.WriteBitAsync(SeparateColourPlaneFlag);

            await writer.WriteUEAsync(BitDepthLumaMinus8);
            await writer.WriteUEAsync(BitDepthChromaMinus8);
            await writer.WriteBitAsync(QpprimeYZeroTransformBypassFlag);
            await writer.WriteBitAsync(SeqScalingMatrixPresentFlag);

            if (SeqScalingMatrixPresentFlag)
            {
                for (int i = 0; i < (ChromaFormatIdc != 3 ? 8 : 12); i++)
                {
                    await _Core();

                    async Task _Core()
                    {
                        int elements = ScalingMatrices.GetListLength(i);
                        Memory<int> buffer = new(new int[elements]);

                        builder!.BuildSink(i, elements, buffer.Span, out bool isPresent);
                        await writer.WriteBitAsync(isPresent);
                        if (isPresent)
                        {
                            for (int i = 0; i < elements; i++)
                                await writer.WriteSEAsync(buffer.Span[i]);
                        }
                    }
                }
            }
        }
        await writer.WriteUEAsync(Log2MaxFrameNumMinus4);
        await writer.WriteUEAsync(PicOrderCntType);

        if (PicOrderCntType == 0u)
            await writer.WriteUEAsync(Log2MaxPicOrderCntLsbMinus4);

        if (PicOrderCntType == 1u)
        {
            await writer.WriteBitAsync(DeltaPicOrderAlwaysZeroFlag);
            await writer.WriteSEAsync(OffsetForNonRefPic);
            await writer.WriteSEAsync(OffsetForTopToBottomField);
            await writer.WriteUEAsync(NumRefFramesInPicOrderCntCycle);
            for (int i = 0; i < NumRefFramesInPicOrderCntCycle; i++)
                await writer.WriteSEAsync(offsetForRefFrames.Span[i]);
        }

        await writer.WriteUEAsync(MaxNumRefFrames);
        await writer.WriteBitAsync(GapsInFrameNumValueAllowedFlag);
        await writer.WriteUEAsync(PicWidthInMbsMinus1);
        await writer.WriteUEAsync(PicHeightInMapUnitsMinus1);
        await writer.WriteBitAsync(FrameMbsOnlyFlag);

        if (!FrameMbsOnlyFlag)
            await writer.WriteBitAsync(MbAdaptiveFrameFieldFlag);

        await writer.WriteBitAsync(Direct8X8InferenceFlag);
        await writer.WriteBitAsync(FrameCroppingFlag);

        if (FrameCroppingFlag)
        {
            await writer.WriteUEAsync(FrameCropLeftOffset);
            await writer.WriteUEAsync(FrameCropRightOffset);
            await writer.WriteUEAsync(FrameCropTopOffset);
            await writer.WriteUEAsync(FrameCropBottomOffset);
        }

        await writer.WriteBitAsync(VuiParametersPresentFlag);

        if (VuiParametersPresentFlag)
        {
            // We already performed a null check, VuiParameters
            // cannot be null here.
            await this.VuiParameters!.Value.WriteAsync(writer, vuiWriteOptions);
        }
    }

    /// <summary>
    ///   Writes the SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="vuiWriteOptions">VUI write options, if <see cref="VuiParametersPresentFlag"/> is true.</param>
    /// <param name="builder">Builds scaling lists if scaling matrices are present.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public readonly async Task WriteAsync(BitStreamWriter writer, MemoryVuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder)
    {
        if (this.PicOrderCntType == 1u)
            throw new InvalidOperationException("PicOrderCntType is 1 but offset for ref frames isn't provided");

        var span = new Memory<int>();
        await WriteAsync(writer, span, vuiWriteOptions, builder);
    }

    /// <summary>
    /// Writes just the entire scaling matrix out of the SPS into the specified bitstream.
    /// </summary>
    /// <param name="writer">Data where the matrix is written to</param>
    /// <param name="entireH264Reader">The entire H.264 bitstream reader. Doesn't matter where the position of the bitstream is.</param>
    public readonly void WriteScalingMatrix(BitStreamWriter writer, BitStreamReader entireH264Reader)
    {
        if (this.ScalingMatrix is null)
            throw new InvalidOperationException("No scaling matrix");

        for (int i = 0; i < this.ScalingMatrix.Value.ListCount; i++)
        {
            _Core(i, in this);
        }

        void _Core(int index, in SequenceParameterSet source)
        {
            Span<int> buffer = stackalloc int[ScalingMatrices.GetListLength(index)];
            source.ScalingMatrix!.Value.ReadList(entireH264Reader, buffer, out bool isPresent, index);

            writer.WriteBit(isPresent);
            if (isPresent)
            {
                ScalingMatrices.WriteScalingList(writer, index, buffer);
            }
        }
    }

    /// <summary>
    /// Writes just the entire scaling matrix out of the SPS into the specified bitstream.
    /// </summary>
    /// <param name="writer">Data where the matrix is written to</param>
    /// <param name="entireH264Reader">The entire H.264 bitstream reader. Doesn't matter where the position of the bitstream is.</param>
    /// <remarks>
    ///   <b>Warning</b>: This method performs heap allocations. Consider using the
    ///   non-asynchronous method, <see cref="WriteScalingMatrix(BitStreamWriter, BitStreamReader)"/>,
    ///   for a stack-allocated flavor.
    /// </remarks>
    public readonly async Task WriteScalingMatrixAsync(BitStreamWriter writer, BitStreamReader entireH264Reader)
    {
        if (this.ScalingMatrix is null)
            throw new InvalidOperationException("No scaling matrix");

        for (int i = 0; i < this.ScalingMatrix.Value.ListCount; i++)
        {
            Memory<int> buffer = new(new int[i < 6 ? 16 : 64]);
            this.ScalingMatrix!.Value.ReadList(entireH264Reader, buffer, out bool isPresent, i);

            await writer.WriteBitAsync(isPresent);
            if (isPresent)
            {
                await ScalingMatrices.WriteScalingListAsync(writer, i, buffer);
            }
        }
    }

    /// <summary>  
    /// Determines whether the specified type is equal to the current instance.  
    /// </summary>  
    /// <param name="obj">The other type to compare.</param>  
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is SequenceParameterSet set && Equals(set);
    }

    /// <summary>  
    /// Determines whether the specified <see cref="SequenceParameterSet"/> is equal to the current instance.  
    /// </summary>  
    /// <param name="other">The other <see cref="SequenceParameterSet"/> to compare.</param>  
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SequenceParameterSet other)
    {
        return Kind == other.Kind &&
               ProfileIdc == other.ProfileIdc &&
               ConstraintSet0Flag == other.ConstraintSet0Flag &&
               ConstraintSet1Flag == other.ConstraintSet1Flag &&
               ConstraintSet2Flag == other.ConstraintSet2Flag &&
               ConstraintSet3Flag == other.ConstraintSet3Flag &&
               ConstraintSet4Flag == other.ConstraintSet4Flag &&
               ConstraintSet5Flag == other.ConstraintSet5Flag &&
               ReservedZero2Bits == other.ReservedZero2Bits &&
               LevelIdc == other.LevelIdc &&
               SpsId == other.SpsId &&
               ChromaFormatIdc == other.ChromaFormatIdc &&
               SeparateColourPlaneFlag == other.SeparateColourPlaneFlag &&
               BitDepthLumaMinus8 == other.BitDepthLumaMinus8 &&
               BitDepthChromaMinus8 == other.BitDepthChromaMinus8 &&
               QpprimeYZeroTransformBypassFlag == other.QpprimeYZeroTransformBypassFlag &&
               SeqScalingMatrixPresentFlag == other.SeqScalingMatrixPresentFlag &&
               EqualityComparer<ScalingMatrices?>.Default.Equals(ScalingMatrix, other.ScalingMatrix) &&
               Log2MaxFrameNumMinus4 == other.Log2MaxFrameNumMinus4 &&
               PicOrderCntType == other.PicOrderCntType &&
               Log2MaxPicOrderCntLsbMinus4 == other.Log2MaxPicOrderCntLsbMinus4 &&
               DeltaPicOrderAlwaysZeroFlag == other.DeltaPicOrderAlwaysZeroFlag &&
               OffsetForNonRefPic == other.OffsetForNonRefPic &&
               OffsetForTopToBottomField == other.OffsetForTopToBottomField &&
               NumRefFramesInPicOrderCntCycle == other.NumRefFramesInPicOrderCntCycle &&
               MaxNumRefFrames == other.MaxNumRefFrames &&
               GapsInFrameNumValueAllowedFlag == other.GapsInFrameNumValueAllowedFlag &&
               PicWidthInMbsMinus1 == other.PicWidthInMbsMinus1 &&
               PicHeightInMapUnitsMinus1 == other.PicHeightInMapUnitsMinus1 &&
               FrameMbsOnlyFlag == other.FrameMbsOnlyFlag &&
               MbAdaptiveFrameFieldFlag == other.MbAdaptiveFrameFieldFlag &&
               Direct8X8InferenceFlag == other.Direct8X8InferenceFlag &&
               FrameCroppingFlag == other.FrameCroppingFlag &&
               FrameCropLeftOffset == other.FrameCropLeftOffset &&
               FrameCropRightOffset == other.FrameCropRightOffset &&
               FrameCropTopOffset == other.FrameCropTopOffset &&
               FrameCropBottomOffset == other.FrameCropBottomOffset &&
               VuiParametersPresentFlag == other.VuiParametersPresentFlag &&
               EqualityComparer<VuiParameters?>.Default.Equals(VuiParameters, other.VuiParameters);
    }

    /// <summary>
    ///   Determines the hash code for the SPS.
    /// </summary>
    /// <returns>SPS hash code.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Kind);
        hash.Add(ProfileIdc);
        hash.Add(ConstraintSet0Flag);
        hash.Add(ConstraintSet1Flag);
        hash.Add(ConstraintSet2Flag);
        hash.Add(ConstraintSet3Flag);
        hash.Add(ConstraintSet4Flag);
        hash.Add(ConstraintSet5Flag);
        hash.Add(ReservedZero2Bits);
        hash.Add(LevelIdc);
        hash.Add(SpsId);
        hash.Add(ChromaFormatIdc);
        hash.Add(SeparateColourPlaneFlag);
        hash.Add(BitDepthLumaMinus8);
        hash.Add(BitDepthChromaMinus8);
        hash.Add(QpprimeYZeroTransformBypassFlag);
        hash.Add(SeqScalingMatrixPresentFlag);
        hash.Add(ScalingMatrix);
        hash.Add(Log2MaxFrameNumMinus4);
        hash.Add(PicOrderCntType);
        hash.Add(Log2MaxPicOrderCntLsbMinus4);
        hash.Add(DeltaPicOrderAlwaysZeroFlag);
        hash.Add(OffsetForNonRefPic);
        hash.Add(OffsetForTopToBottomField);
        hash.Add(NumRefFramesInPicOrderCntCycle);
        hash.Add(MaxNumRefFrames);
        hash.Add(GapsInFrameNumValueAllowedFlag);
        hash.Add(PicWidthInMbsMinus1);
        hash.Add(PicHeightInMapUnitsMinus1);
        hash.Add(FrameMbsOnlyFlag);
        hash.Add(MbAdaptiveFrameFieldFlag);
        hash.Add(Direct8X8InferenceFlag);
        hash.Add(FrameCroppingFlag);
        hash.Add(FrameCropLeftOffset);
        hash.Add(FrameCropRightOffset);
        hash.Add(FrameCropTopOffset);
        hash.Add(FrameCropBottomOffset);
        hash.Add(VuiParametersPresentFlag);
        hash.Add(VuiParameters);
        return hash.ToHashCode();
    }


    /// <summary>  
    /// Determines whether two <see cref="SequenceParameterSet"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="SequenceParameterSet"/> to compare.</param>  
    /// <param name="right">The second <see cref="SequenceParameterSet"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(SequenceParameterSet left, SequenceParameterSet right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    /// Determines whether two <see cref="SequenceParameterSet"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="SequenceParameterSet"/> to compare.</param>  
    /// <param name="right">The second <see cref="SequenceParameterSet"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(SequenceParameterSet left, SequenceParameterSet right)
    {
        return !(left == right);
    }

    private readonly void PrepareForWrite(ScalingMatrixBuilder? builder, Span<int> offsetForRefFrames)
    {
        if (builder is null && this.SeqScalingMatrixPresentFlag)
            throw new ArgumentNullException(nameof(builder), "Scaling matrices are present in the SPS but the scaling matrix builder is not provided");

        if (this.VuiParametersPresentFlag && this.VuiParameters is null)
            throw new InvalidOperationException("VuiParametersPresentFlag is true but actual VUI parameters aren't provided in the SPS");

        if (PicOrderCntType == 1u && offsetForRefFrames.Length < NumRefFramesInPicOrderCntCycle)
            throw new ArgumentOutOfRangeException(nameof(offsetForRefFrames), "Not enough offsets for ref frames; expected " + NumRefFramesInPicOrderCntCycle + ", got " + offsetForRefFrames.Length);
    }
}
