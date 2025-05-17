using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H26x;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
///   Represents a sub macroblock prediction.
/// </summary>
public struct SubMacroblockPrediction : IEquatable<SubMacroblockPrediction>
{
    /// <summary>
    /// The type of the sub macroblock.
    /// </summary>
    public Container4UInt32 SubMbType;

    /// <summary>
    /// The reference index for list 0.
    /// </summary>
    public Container4UInt32 RefIdxL0;

    /// <summary>
    /// The reference index for list 1.
    /// </summary>
    public Container4UInt32 RefIdxL1;

    /// <summary>
    /// The motion vector difference for list 0.
    /// </summary>
    public ContainerMatrix4x16x2 MvdL0;

    /// <summary>
    /// The motion vector difference for list 1.
    /// </summary>
    public ContainerMatrix4x16x2 MvdL1;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubMacroblockPrediction"/> struct.
    /// </summary>
    /// <param name="subMbType">The type of the sub macroblock.</param>
    /// <param name="refIdxL0">The reference index for list 0.</param>
    /// <param name="refIdxL1">The reference index for list 1.</param>
    /// <param name="mvdL0">The motion vector difference for list 0.</param>
    /// <param name="mvdL1">The motion vector difference for list 1.</param>
    public SubMacroblockPrediction(Container4UInt32 subMbType, Container4UInt32 refIdxL0, Container4UInt32 refIdxL1, ContainerMatrix4x16x2 mvdL0, ContainerMatrix4x16x2 mvdL1)
    {
        SubMbType = subMbType;
        RefIdxL0 = refIdxL0;
        RefIdxL1 = refIdxL1;
        MvdL0 = mvdL0;
        MvdL1 = mvdL1;
    }

    /// <summary>
    ///   Reads the sub macroblock prediction from the bitstream.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="mbaffFrameFlag"></param>
    /// <param name="sliceType"></param>
    /// <param name="entropyCodingMode"></param>
    /// <param name="mbType"></param>
    /// <param name="numRefIdxL0ActiveMinus1"></param>
    /// <param name="numRefIdxL1ActiveMinus1"></param>
    /// <param name="mbFieldDecodingFlag"></param>
    /// <param name="fieldPicFlag"></param>
    /// <returns>Sub macroblock prediction</returns>
    public static SubMacroblockPrediction Read(BitStreamReader reader, bool mbaffFrameFlag, GeneralSliceType sliceType, EntropyCodingMode entropyCodingMode, int mbType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag)
    {
        Container4UInt32 subMbType = new();
        Container4UInt32 refIdxL0 = new();
        Container4UInt32 refIdxL1 = new();
        ContainerMatrix4x16x2 mvdL0 = new();
        ContainerMatrix4x16x2 mvdL1 = new();

        for (int i = 0; i < 4; i++)
            subMbType[i] = entropyCodingMode == EntropyCodingMode.Cavlc ? reader.ReadUE() : (uint)reader.ReadAE();

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if ((numRefIdxL0ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                mbType != P_8x8ref0 &&
                subMbType[mbPartIdx] != B_Direct_8x8 &&
                Util264.SubMbPredMode((int)subMbType[mbPartIdx], sliceType) != Pred_L1)
            {
                int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL0ActiveMinus1 : (2 * numRefIdxL0ActiveMinus1 + 1);
                refIdxL0[mbPartIdx] =
                    entropyCodingMode == EntropyCodingMode.Cavlc ? (uint)reader.ReadTE(truncatedRange) : (uint)reader.ReadAE();
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if ((numRefIdxL1ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                subMbType[mbPartIdx] != B_Direct_8x8 &&
                Util264.SubMbPredMode((int)subMbType[mbPartIdx], sliceType) != Pred_L0)
            {
                int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL1ActiveMinus1 : (2 * numRefIdxL1ActiveMinus1 + 1);
                refIdxL1[mbPartIdx] =
                    entropyCodingMode == EntropyCodingMode.Cavlc ? (uint)reader.ReadTE(truncatedRange) : (uint)reader.ReadAE();
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if (subMbType[mbPartIdx] != B_Direct_8x8 && Util264.SubMbPredMode((int)subMbType[mbPartIdx], sliceType) != Pred_L1)
            {
                for (int subMbPartIdx = 0; subMbPartIdx < Util264.NumSubMbPart((int)subMbType[mbPartIdx], sliceType); subMbPartIdx++)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        mvdL0[mbPartIdx, subMbPartIdx, compIdx] = entropyCodingMode == EntropyCodingMode.Cavlc ? reader.ReadSE() : reader.ReadAE();
                }
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if (subMbType[mbPartIdx] != B_Direct_8x8 && Util264.SubMbPredMode((int)subMbType[mbPartIdx], sliceType) != Pred_L0)
            {
                for (int subMbPartIdx = 0; subMbPartIdx < Util264.NumSubMbPart((int)subMbType[mbPartIdx], sliceType); subMbPartIdx++)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        mvdL1[mbPartIdx, subMbPartIdx, compIdx] = entropyCodingMode == EntropyCodingMode.Cavlc ? reader.ReadSE() : reader.ReadAE();
                }
            }
        }

        return new SubMacroblockPrediction(subMbType, refIdxL0, refIdxL1, mvdL0, mvdL1);
    }

    /// <summary>
    ///   Writes the sub macroblock prediction to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="entropyCodingMode"></param>
    /// <param name="numRefIdxL0ActiveMinus1"></param>
    /// <param name="numRefIdxL1ActiveMinus1"></param>
    /// <param name="mbFieldDecodingFlag"></param>
    /// <param name="fieldPicFlag"></param>
    /// <param name="sliceType"></param>
    /// <param name="mbType"></param>
    /// <param name="mbaffFrameFlag"></param>
    public readonly void Write(BitStreamWriter writer, EntropyCodingMode entropyCodingMode, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, GeneralSliceType sliceType, int mbType, bool mbaffFrameFlag)
    {
        for (int i = 0; i < 4; i++)
            if (entropyCodingMode == EntropyCodingMode.Cavlc)
                writer.WriteUE(SubMbType[i]);
            else
                writer.WriteAE((int)SubMbType[i]);

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if ((numRefIdxL0ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                mbType != P_8x8ref0 &&
                SubMbType[mbPartIdx] != B_Direct_8x8 &&
                Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L1)
            {
                int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL0ActiveMinus1 : (2 * numRefIdxL0ActiveMinus1 + 1);
                if (entropyCodingMode == EntropyCodingMode.Cavlc)
                    writer.WriteTE((int)RefIdxL0[mbPartIdx], truncatedRange);
                else
                    writer.WriteAE((int)RefIdxL0[mbPartIdx]);
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if ((numRefIdxL1ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                SubMbType[mbPartIdx] != B_Direct_8x8 &&
                Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L0)
            {
                int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL1ActiveMinus1 : (2 * numRefIdxL1ActiveMinus1 + 1);
                if (entropyCodingMode == EntropyCodingMode.Cavlc)
                    writer.WriteTE((int)RefIdxL1[mbPartIdx], truncatedRange);
                else
                    writer.WriteAE((int)RefIdxL1[mbPartIdx]);
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if (SubMbType[mbPartIdx] != B_Direct_8x8 && Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L1)
            {
                for (int subMbPartIdx = 0; subMbPartIdx < Util264.NumSubMbPart((int)SubMbType[mbPartIdx], sliceType); subMbPartIdx++)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (entropyCodingMode == EntropyCodingMode.Cavlc)
                            writer.WriteSE(MvdL0[mbPartIdx, subMbPartIdx, compIdx]);
                        else
                            writer.WriteAE(MvdL0[mbPartIdx, subMbPartIdx, compIdx]);
                }
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if (SubMbType[mbPartIdx] != B_Direct_8x8 && Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L0)
            {
                for (int subMbPartIdx = 0; subMbPartIdx < Util264.NumSubMbPart((int)SubMbType[mbPartIdx], sliceType); subMbPartIdx++)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (entropyCodingMode == EntropyCodingMode.Cavlc)
                            writer.WriteSE(MvdL1[mbPartIdx, subMbPartIdx, compIdx]);
                        else
                            writer.WriteAE(MvdL1[mbPartIdx, subMbPartIdx, compIdx]);
                }
            }
        }
    }

    /// <summary>
    ///   Writes the sub macroblock prediction to the bitstream.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="entropyCodingMode"></param>
    /// <param name="numRefIdxL0ActiveMinus1"></param>
    /// <param name="numRefIdxL1ActiveMinus1"></param>
    /// <param name="mbFieldDecodingFlag"></param>
    /// <param name="fieldPicFlag"></param>
    /// <param name="sliceType"></param>
    /// <param name="mbType"></param>
    /// <param name="mbaffFrameFlag"></param>
    public readonly async Task WriteAsync(BitStreamWriter writer, EntropyCodingMode entropyCodingMode, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, GeneralSliceType sliceType, int mbType, bool mbaffFrameFlag)
    {
        for (int i = 0; i < 4; i++)
            if (entropyCodingMode == EntropyCodingMode.Cavlc)
                await writer.WriteUEAsync(SubMbType[i]);
            else
                await writer.WriteAEAsync((int)SubMbType[i]);

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if ((numRefIdxL0ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                mbType != P_8x8ref0 &&
                SubMbType[mbPartIdx] != B_Direct_8x8 &&
                Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L1)
            {
                int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL0ActiveMinus1 : (2 * numRefIdxL0ActiveMinus1 + 1);
                if (entropyCodingMode == EntropyCodingMode.Cavlc)
                    await writer.WriteTEAsync((int)RefIdxL0[mbPartIdx], truncatedRange);
                else
                    await writer.WriteAEAsync((int)RefIdxL0[mbPartIdx]);
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if ((numRefIdxL1ActiveMinus1 > 0 || mbFieldDecodingFlag != fieldPicFlag) &&
                SubMbType[mbPartIdx] != B_Direct_8x8 &&
                Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L0)
            {
                int truncatedRange = (!mbaffFrameFlag || !mbFieldDecodingFlag) ? numRefIdxL1ActiveMinus1 : (2 * numRefIdxL1ActiveMinus1 + 1);
                if (entropyCodingMode == EntropyCodingMode.Cavlc)
                    await writer.WriteTEAsync((int)RefIdxL1[mbPartIdx], truncatedRange);
                else
                    await writer.WriteAEAsync((int)RefIdxL1[mbPartIdx]);
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if (SubMbType[mbPartIdx] != B_Direct_8x8 && Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L1)
            {
                for (int subMbPartIdx = 0; subMbPartIdx < Util264.NumSubMbPart((int)SubMbType[mbPartIdx], sliceType); subMbPartIdx++)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (entropyCodingMode == EntropyCodingMode.Cavlc)
                            await writer.WriteSEAsync(MvdL0[mbPartIdx, subMbPartIdx, compIdx]);
                        else
                            await writer.WriteAEAsync(MvdL0[mbPartIdx, subMbPartIdx, compIdx]);
                }
            }
        }

        for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
        {
            if (SubMbType[mbPartIdx] != B_Direct_8x8 && Util264.SubMbPredMode((int)SubMbType[mbPartIdx], sliceType) != Pred_L0)
            {
                for (int subMbPartIdx = 0; subMbPartIdx < Util264.NumSubMbPart((int)SubMbType[mbPartIdx], sliceType); subMbPartIdx++)
                {
                    for (int compIdx = 0; compIdx < 2; compIdx++)
                        if (entropyCodingMode == EntropyCodingMode.Cavlc)
                            await writer.WriteSEAsync(MvdL1[mbPartIdx, subMbPartIdx, compIdx]);
                        else
                            await writer.WriteAEAsync(MvdL1[mbPartIdx, subMbPartIdx, compIdx]);
                }
            }
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is SubMacroblockPrediction prediction && Equals(prediction);
    }

    /// <summary>
    /// Determines whether the current instance is equal to another instance of <see cref="SubMacroblockPrediction"/>.
    /// </summary>
    /// <param name="other">The other instance to compare with.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SubMacroblockPrediction other)
    {
        return SubMbType.Equals(other.SubMbType) &&
               RefIdxL0.Equals(other.RefIdxL0) &&
               RefIdxL1.Equals(other.RefIdxL1) &&
               MvdL0.Equals(other.MvdL0) &&
               MvdL1.Equals(other.MvdL1);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(SubMbType, RefIdxL0, RefIdxL1, MvdL0, MvdL1);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="SubMacroblockPrediction"/> are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SubMacroblockPrediction left, SubMacroblockPrediction right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="SubMacroblockPrediction"/> are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SubMacroblockPrediction left, SubMacroblockPrediction right)
    {
        return !(left == right);
    }
}
