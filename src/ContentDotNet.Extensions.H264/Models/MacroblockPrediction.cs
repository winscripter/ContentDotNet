using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Cabac;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H26x;
using ContentDotNet.Primitives;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents the prediction data for a macroblock in H.264 video encoding.
/// </summary>
public struct MacroblockPrediction : IEquatable<MacroblockPrediction>
{
    /// <summary>
    /// Flags indicating whether the previous intra 4x4 prediction mode is used.
    /// </summary>
    public Container16Boolean PrevIntra4x4PredModeFlag;

    /// <summary>
    /// Remaining intra 4x4 prediction modes.
    /// </summary>
    public Container16UInt32 RemIntra4x4PredMode;

    /// <summary>
    /// Flags indicating whether the previous intra 8x8 prediction mode is used.
    /// </summary>
    public Container4Boolean PrevIntra8x8PredModeFlag;

    /// <summary>
    /// Remaining intra 8x8 prediction modes.
    /// </summary>
    public Container4UInt32 RemIntra8x8PredMode;

    /// <summary>
    /// The intra chroma prediction mode.
    /// </summary>
    public uint IntraChromaPredMode;

    /// <summary>
    /// Reference indices for L0 prediction.
    /// </summary>
    public Container16UInt32 RefIdxL0;

    /// <summary>
    /// Reference indices for L1 prediction.
    /// </summary>
    public Container16UInt32 RefIdxL1;

    /// <summary>
    /// Motion vector differences for L0 prediction.
    /// </summary>
    public ContainerMatrix4x4x2 MvdL0;

    /// <summary>
    /// Motion vector differences for L1 prediction.
    /// </summary>
    public ContainerMatrix4x4x2 MvdL1;

    /// <summary>
    /// Initializes a new instance of the <see cref="MacroblockPrediction"/> struct.
    /// </summary>
    /// <param name="prevIntra4x4PredModeFlag">Flags for previous intra 4x4 prediction mode.</param>
    /// <param name="remIntra4x4PredMode">Remaining intra 4x4 prediction modes.</param>
    /// <param name="prevIntra8x8PredModeFlag">Flags for previous intra 8x8 prediction mode.</param>
    /// <param name="remIntra8x8PredMode">Remaining intra 8x8 prediction modes.</param>
    /// <param name="intraChromaPredMode">The intra chroma prediction mode.</param>
    /// <param name="refIdxL0">Reference indices for L0 prediction.</param>
    /// <param name="refIdxL1">Reference indices for L1 prediction.</param>
    /// <param name="mvdL0">Motion vector differences for L0 prediction.</param>
    /// <param name="mvdL1">Motion vector differences for L1 prediction.</param>
    public MacroblockPrediction(
        Container16Boolean prevIntra4x4PredModeFlag,
        Container16UInt32 remIntra4x4PredMode,
        Container4Boolean prevIntra8x8PredModeFlag,
        Container4UInt32 remIntra8x8PredMode,
        uint intraChromaPredMode,
        Container16UInt32 refIdxL0,
        Container16UInt32 refIdxL1,
        ContainerMatrix4x4x2 mvdL0,
        ContainerMatrix4x4x2 mvdL1)
    {
        PrevIntra4x4PredModeFlag = prevIntra4x4PredModeFlag;
        RemIntra4x4PredMode = remIntra4x4PredMode;
        PrevIntra8x8PredModeFlag = prevIntra8x8PredModeFlag;
        RemIntra8x8PredMode = remIntra8x8PredMode;
        IntraChromaPredMode = intraChromaPredMode;
        RefIdxL0 = refIdxL0;
        RefIdxL1 = refIdxL1;
        MvdL0 = mvdL0;
        MvdL1 = mvdL1;
    }

    /// <summary>
    /// Reads the macroblock prediction data from the bitstream.
    /// </summary>
    /// <param name="reader">The <see cref="BitStreamReader"/> to read from.</param>
    /// <param name="cabac">CABAC</param>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="mbaffFrameFlag">Indicates if MBAFF is used in the frame.</param>
    /// <param name="codingMode">The entropy coding mode (CAVLC or CABAC).</param>
    /// <param name="sliceType">The general slice type.</param>
    /// <param name="transformSize8x8Flag">Indicates if 8x8 transform size is used.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for L0 minus 1.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for L1 minus 1.</param>
    /// <param name="mbFieldDecodingFlag">Indicates if macroblock field decoding is used.</param>
    /// <param name="fieldPicFlag">Indicates if the picture is a field picture.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <returns>A <see cref="MacroblockPrediction"/> instance containing the read data.</returns>
    public static MacroblockPrediction Read(BitStreamReader reader, CabacReader? cabac, int mbType, bool mbaffFrameFlag, EntropyCodingMode codingMode, GeneralSliceType sliceType, bool transformSize8x8Flag, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, int chromaArrayType)
    {
        Container16Boolean prevIntra4x4PredModeFlag = new();
        Container16UInt32 remIntra4x4PredMode = new();
        Container4Boolean prevIntra8x8PredModeFlag = new();
        Container4UInt32 remIntra8x8PredMode = new();
        uint intraChromaPredMode = 0u;
        Container16UInt32 refIdxL0 = new();
        Container16UInt32 refIdxL1 = new();
        ContainerMatrix4x4x2 mvdL0 = new();
        ContainerMatrix4x4x2 mvdL1 = new();

        if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_4x4 ||
            Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_8x8 ||
            Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_16x16)
        {
            if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_4x4)
            {
                for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
                {
                    prevIntra4x4PredModeFlag[luma4x4BlkIdx] = codingMode == EntropyCodingMode.Cavlc ? reader.ReadBit() : Int32Boolean.B(cabac!.ParsePrevIntraNxNPredModeFlag());
                    if (prevIntra4x4PredModeFlag[luma4x4BlkIdx])
                        remIntra4x4PredMode[luma4x4BlkIdx] = codingMode == EntropyCodingMode.Cavlc ? reader.ReadBits(3) : (uint)cabac!.ParseRemIntraNxNPredMode();
                }
            }

            if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_8x8)
            {
                for (int luma8x8BlkIdx = 0; luma8x8BlkIdx < 4; luma8x8BlkIdx++)
                {
                    prevIntra8x8PredModeFlag[luma8x8BlkIdx] = codingMode == EntropyCodingMode.Cavlc ? reader.ReadBit() : Int32Boolean.B(cabac!.ParsePrevIntraNxNPredModeFlag());
                    if (prevIntra8x8PredModeFlag[luma8x8BlkIdx])
                        remIntra8x8PredMode[luma8x8BlkIdx] = codingMode == EntropyCodingMode.Cavlc ? reader.ReadBits(3) : (uint)cabac!.ParseRemIntraNxNPredMode();
                }
            }

            if (chromaArrayType is 1 or 2)
                intraChromaPredMode = codingMode == EntropyCodingMode.Cavlc ? reader.ReadUE() : (uint)cabac!.ParseIntraChromaPredMode();
        }
        else if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) != Direct)
        {
            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if ((numRefIdxL0ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                    Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L1)
                {
                    int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL0ActiveMinus1 : (2 * numRefIdxL0ActiveMinus1 + 1);
                    refIdxL0[mbPartIdx] = codingMode == EntropyCodingMode.Cavlc ? (uint)reader.ReadTE(truncatedRange) : (uint)cabac!.ParseRefIdxLX();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if ((numRefIdxL1ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                    Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L0)
                {
                    int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL1ActiveMinus1 : (2 * numRefIdxL1ActiveMinus1 + 1);
                    refIdxL1[mbPartIdx] = codingMode == EntropyCodingMode.Cavlc ? (uint)reader.ReadTE(truncatedRange) : (uint)cabac!.ParseRefIdxLX();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L1)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        mvdL0[mbPartIdx, 0, compIdx] = codingMode == EntropyCodingMode.Cavlc ? reader.ReadSE() : cabac!.ParseMvdL0();
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L0)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        mvdL1[mbPartIdx, 0, compIdx] = codingMode == EntropyCodingMode.Cavlc ? reader.ReadSE() : cabac!.ParseMvdL1();
                }
            }
        }

        return new MacroblockPrediction(prevIntra4x4PredModeFlag, remIntra4x4PredMode, prevIntra8x8PredModeFlag, remIntra8x8PredMode, intraChromaPredMode, refIdxL0, refIdxL1, mvdL0, mvdL1);
    }

    /// <summary>
    /// Writes the macroblock prediction data to the bitstream.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="mbaffFrameFlag">Indicates if MBAFF is used in the frame.</param>
    /// <param name="codingMode">The entropy coding mode (CAVLC or CABAC).</param>
    /// <param name="sliceType">The general slice type.</param>
    /// <param name="transformSize8x8Flag">Indicates if 8x8 transform size is used.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for L0 minus 1.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for L1 minus 1.</param>
    /// <param name="mbFieldDecodingFlag">Indicates if macroblock field decoding is used.</param>
    /// <param name="fieldPicFlag">Indicates if the picture is a field picture.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    public readonly void Write(BitStreamWriter writer, int mbType, bool mbaffFrameFlag, EntropyCodingMode codingMode, GeneralSliceType sliceType, bool transformSize8x8Flag, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, int chromaArrayType)
    {
        if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_4x4 ||
            Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_8x8 ||
            Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_16x16)
        {
            if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_4x4)
            {
                for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
                {
                    if (codingMode == EntropyCodingMode.Cavlc)
                        writer.WriteBit(PrevIntra4x4PredModeFlag[luma4x4BlkIdx]);
                    else
                        writer.WriteAE(Int32Boolean.I32(PrevIntra4x4PredModeFlag[luma4x4BlkIdx]));

                    if (PrevIntra4x4PredModeFlag[luma4x4BlkIdx])
                        if (codingMode == EntropyCodingMode.Cavlc)
                            writer.WriteBits(RemIntra4x4PredMode[luma4x4BlkIdx], 3);
                        else
                            writer.WriteAE((int)RemIntra4x4PredMode[luma4x4BlkIdx]);
                }
            }

            if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_8x8)
            {
                for (int luma8x8BlkIdx = 0; luma8x8BlkIdx < 4; luma8x8BlkIdx++)
                {
                    if (codingMode == EntropyCodingMode.Cavlc)
                        writer.WriteBit(PrevIntra8x8PredModeFlag[luma8x8BlkIdx]);
                    else
                        writer.WriteAE(Int32Boolean.I32(PrevIntra8x8PredModeFlag[luma8x8BlkIdx]));
                    
                    if (PrevIntra8x8PredModeFlag[luma8x8BlkIdx])
                        if (codingMode == EntropyCodingMode.Cavlc)
                            writer.WriteBits(RemIntra8x8PredMode[luma8x8BlkIdx], 3);
                        else
                            writer.WriteAE((int)RemIntra8x8PredMode[luma8x8BlkIdx]);
                }
            }

            if (chromaArrayType is 1 or 2)
                if (codingMode == EntropyCodingMode.Cavlc)
                    writer.WriteUE(IntraChromaPredMode);
                else
                    writer.WriteAE((int)IntraChromaPredMode);
        }
        else if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) != Direct)
        {
            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if ((numRefIdxL0ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                    Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L1)
                {
                    int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL0ActiveMinus1 : (2 * numRefIdxL0ActiveMinus1 + 1);
                    if (codingMode == EntropyCodingMode.Cavlc)
                        writer.WriteTE((int)RefIdxL0[mbPartIdx], truncatedRange);
                    else
                        writer.WriteAE((int)RefIdxL0[mbPartIdx]);
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if ((numRefIdxL1ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                    Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L0)
                {
                    int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL1ActiveMinus1 : (2 * numRefIdxL1ActiveMinus1 + 1);
                    if (codingMode == EntropyCodingMode.Cavlc)
                        writer.WriteTE((int)RefIdxL1[mbPartIdx], truncatedRange);
                    else
                        writer.WriteAE((int)RefIdxL1[mbPartIdx]);
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L1)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (codingMode == EntropyCodingMode.Cavlc)
                            writer.WriteSE(MvdL0[mbPartIdx, 0, compIdx]);
                        else
                            writer.WriteAE(MvdL0[mbPartIdx, 0, compIdx]);
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L0)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (codingMode == EntropyCodingMode.Cavlc)
                            writer.WriteSE(MvdL1[mbPartIdx, 0, compIdx]);
                        else
                            writer.WriteAE(MvdL1[mbPartIdx, 0, compIdx]);
                }
            }
        }
    }

    /// <summary>
    /// Writes the macroblock prediction data to the bitstream.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="mbaffFrameFlag">Indicates if MBAFF is used in the frame.</param>
    /// <param name="codingMode">The entropy coding mode (CAVLC or CABAC).</param>
    /// <param name="sliceType">The general slice type.</param>
    /// <param name="transformSize8x8Flag">Indicates if 8x8 transform size is used.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for L0 minus 1.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for L1 minus 1.</param>
    /// <param name="mbFieldDecodingFlag">Indicates if macroblock field decoding is used.</param>
    /// <param name="fieldPicFlag">Indicates if the picture is a field picture.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, int mbType, bool mbaffFrameFlag, EntropyCodingMode codingMode, GeneralSliceType sliceType, bool transformSize8x8Flag, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, int chromaArrayType)
    {
        if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_4x4 ||
            Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_8x8 ||
            Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_16x16)
        {
            if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_4x4)
            {
                for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
                {
                    if (codingMode == EntropyCodingMode.Cavlc)
                        await writer.WriteBitAsync(PrevIntra4x4PredModeFlag[luma4x4BlkIdx]);
                    else
                        await writer.WriteAEAsync(Int32Boolean.I32(PrevIntra4x4PredModeFlag[luma4x4BlkIdx]));

                    if (PrevIntra4x4PredModeFlag[luma4x4BlkIdx])
                        if (codingMode == EntropyCodingMode.Cavlc)
                            await writer.WriteBitsAsync(RemIntra4x4PredMode[luma4x4BlkIdx], 3);
                        else
                            await writer.WriteAEAsync((int)RemIntra4x4PredMode[luma4x4BlkIdx]);
                }
            }

            if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) == Intra_8x8)
            {
                for (int luma8x8BlkIdx = 0; luma8x8BlkIdx < 4; luma8x8BlkIdx++)
                {
                    if (codingMode == EntropyCodingMode.Cavlc)
                        await writer.WriteBitAsync(PrevIntra8x8PredModeFlag[luma8x8BlkIdx]);
                    else
                        await writer.WriteAEAsync(Int32Boolean.I32(PrevIntra8x8PredModeFlag[luma8x8BlkIdx]));

                    if (PrevIntra8x8PredModeFlag[luma8x8BlkIdx])
                        if (codingMode == EntropyCodingMode.Cavlc)
                            await writer.WriteBitsAsync(RemIntra8x8PredMode[luma8x8BlkIdx], 3);
                        else
                            await writer.WriteAEAsync((int)RemIntra8x8PredMode[luma8x8BlkIdx]);
                }
            }

            if (chromaArrayType is 1 or 2)
                if (codingMode == EntropyCodingMode.Cavlc)
                    await writer.WriteUEAsync(IntraChromaPredMode);
                else
                    await writer.WriteAEAsync((int)IntraChromaPredMode);
        }
        else if (Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) != Direct)
        {
            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if ((numRefIdxL0ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                    Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L1)
                {
                    int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL0ActiveMinus1 : (2 * numRefIdxL0ActiveMinus1 + 1);
                    if (codingMode == EntropyCodingMode.Cavlc)
                        await writer.WriteTEAsync((int)RefIdxL0[mbPartIdx], truncatedRange);
                    else
                        await writer.WriteAEAsync((int)RefIdxL0[mbPartIdx]);
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if ((numRefIdxL1ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                    Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L0)
                {
                    int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL1ActiveMinus1 : (2 * numRefIdxL1ActiveMinus1 + 1);
                    if (codingMode == EntropyCodingMode.Cavlc)
                        await writer.WriteTEAsync((int)RefIdxL1[mbPartIdx], truncatedRange);
                    else
                        await writer.WriteAEAsync((int)RefIdxL1[mbPartIdx]);
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L1)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (codingMode == EntropyCodingMode.Cavlc)
                            await writer.WriteSEAsync(MvdL0[mbPartIdx, 0, compIdx]);
                        else
                            await writer.WriteAEAsync(MvdL0[mbPartIdx, 0, compIdx]);
                }
            }

            for (int mbPartIdx = 0; mbPartIdx < Util264.NumMbPart(mbType, sliceType); mbPartIdx++)
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) != Pred_L0)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (codingMode == EntropyCodingMode.Cavlc)
                            await writer.WriteSEAsync(MvdL1[mbPartIdx, 0, compIdx]);
                        else
                            await writer.WriteAEAsync(MvdL1[mbPartIdx, 0, compIdx]);
                }
            }
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is MacroblockPrediction prediction && Equals(prediction);
    }

    /// <summary>
    /// Determines whether the specified <see cref="MacroblockPrediction"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="MacroblockPrediction"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="MacroblockPrediction"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(MacroblockPrediction other)
    {
        return PrevIntra4x4PredModeFlag.Equals(other.PrevIntra4x4PredModeFlag) &&
               RemIntra4x4PredMode.Equals(other.RemIntra4x4PredMode) &&
               PrevIntra8x8PredModeFlag.Equals(other.PrevIntra8x8PredModeFlag) &&
               RemIntra8x8PredMode.Equals(other.RemIntra8x8PredMode) &&
               IntraChromaPredMode == other.IntraChromaPredMode &&
               RefIdxL0.Equals(other.RefIdxL0) &&
               RefIdxL1.Equals(other.RefIdxL1) &&
               EqualityComparer<ContainerMatrix4x4x2>.Default.Equals(MvdL0, other.MvdL0) &&
               EqualityComparer<ContainerMatrix4x4x2>.Default.Equals(MvdL1, other.MvdL1);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(PrevIntra4x4PredModeFlag);
        hash.Add(RemIntra4x4PredMode);
        hash.Add(PrevIntra8x8PredModeFlag);
        hash.Add(RemIntra8x8PredMode);
        hash.Add(IntraChromaPredMode);
        hash.Add(RefIdxL0);
        hash.Add(RefIdxL1);
        hash.Add(MvdL0);
        hash.Add(MvdL1);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="MacroblockPrediction"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MacroblockPrediction left, MacroblockPrediction right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="MacroblockPrediction"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MacroblockPrediction left, MacroblockPrediction right)
    {
        return !(left == right);
    }
}
