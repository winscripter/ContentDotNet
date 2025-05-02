using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Extensions.H26x;
using System.Data;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents a macroblock layer in an H.264 video stream.
/// </summary>
public struct MacroblockLayer : IEquatable<MacroblockLayer>
{
    /// <summary>
    /// Gets or sets the macroblock type.
    /// </summary>
    public uint MbType;

    /// <summary>
    /// Gets or sets the PCM luma values for the macroblock.
    /// </summary>
    public Container256UInt32 PcmLuma;

    /// <summary>
    /// Gets or sets the PCM chroma values for the macroblock.
    /// </summary>
    public Container512UInt32 PcmChroma;

    /// <summary>
    /// Gets or sets a value indicating whether the transform size is 8x8.
    /// </summary>
    public bool TransformSize8x8Flag;

    /// <summary>
    /// Gets or sets the coded block pattern for the macroblock.
    /// </summary>
    public int CodedBlockPattern;

    /// <summary>
    /// Gets or sets the macroblock quantization parameter delta.
    /// </summary>
    public int MbQpDelta;

    /// <summary>
    /// Gets or sets the residual data for Intra 16x16 prediction mode.
    /// </summary>
    public Residual? Intra16x16Residual;

    /// <summary>
    /// Macroblock prediction
    /// </summary>
    public MacroblockPrediction? Prediction;

    /// <summary>
    /// Submacroblock prediction
    /// </summary>
    public SubMacroblockPrediction? SubMacroblockPrediction;

    /// <summary>
    /// Initializes a new instance of the <see cref="MacroblockLayer"/> struct.
    /// </summary>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="pcmLuma">The PCM luma values.</param>
    /// <param name="pcmChroma">The PCM chroma values.</param>
    /// <param name="transformSize8x8Flag">Indicates whether the transform size is 8x8.</param>
    /// <param name="codedBlockPattern">The coded block pattern.</param>
    /// <param name="mbQpDelta">The macroblock quantization parameter delta.</param>
    /// <param name="intra16x16Residual">The residual data for Intra 16x16 prediction mode.</param>
    /// <param name="mbPred">The macroblock prediction</param>
    /// <param name="subMbPred">The submacroblock prediction</param>
    public MacroblockLayer(uint mbType, Container256UInt32 pcmLuma, Container512UInt32 pcmChroma, bool transformSize8x8Flag, int codedBlockPattern, int mbQpDelta, Residual? intra16x16Residual, MacroblockPrediction? mbPred, SubMacroblockPrediction? subMbPred)
    {
        MbType = mbType;
        PcmLuma = pcmLuma;
        PcmChroma = pcmChroma;
        TransformSize8x8Flag = transformSize8x8Flag;
        CodedBlockPattern = codedBlockPattern;
        MbQpDelta = mbQpDelta;
        Intra16x16Residual = intra16x16Residual;
        Prediction = mbPred;
        SubMacroblockPrediction = subMbPred;
    }

#pragma warning disable
    public static MacroblockLayer Read(BitStreamReader reader, bool transform8x8ModeFlag, EntropyCodingMode codingMode, uint bitDepthLumaMinus8, uint bitDepthChromaMinus8, MacroblockSizeChroma sizes, int chromaArrayType, GeneralSliceType sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool mbaffFrameFlag, bool fieldPicFlag, bool direct8x8InferenceFlag, NalUnit nalu, DerivationContext dc, IMacroblockUtility util, ResidualMode mode, bool constrainedIntraPredFlag, int subWidthC, int subHeightC)
#pragma warning restore
    {
        Container256UInt32 pcmLuma = new();
        Container512UInt32 pcmChroma = new();
        SubMacroblockPrediction? subMbPrediction = null;
        MacroblockPrediction? mbPrediction = null;

        uint mbType = codingMode == EntropyCodingMode.Cavlc ? reader.ReadUE() : (uint)reader.ReadAE();

        bool transformSize8x8Flag = false;
        int codedBlockPattern = 0;

        int CodedBlockPatternLuma = 0;
        int CodedBlockPatternChroma = 0;

        int mbQpDelta = 0;

        Residual residual = new();

        if (mbType is I_PCM)
        {
            while (!Util264.ByteAligned(reader))
                _ = reader.ReadBit(); // pcm_alignment_zero_bit

            for (int i = 0; i < 256; i++) pcmLuma[i] = reader.ReadBits(bitDepthLumaMinus8 + 8u);
            for (int i = 0; i < 2 * sizes.Width * sizes.Height; i++) pcmChroma[i] = reader.ReadBits(bitDepthChromaMinus8 + 8u);
        }
        else
        {
            bool noSubMbPartSizeLessThan8x8Flag = true;
            if (mbType != I_NxN &&
                Util264.MbPartPredMode((int)mbType, 0, false, sliceType) != Intra_16x16 &&
                Util264.NumMbPart((int)mbType, sliceType) == 4)
            {
                subMbPrediction = Models.SubMacroblockPrediction.Read(reader, mbaffFrameFlag, sliceType, codingMode, (int)mbType, numRefIdxL0ActiveMinus1, numRefIdxL1ActiveMinus1, mbFieldDecodingFlag, fieldPicFlag);
                for (int mbPartIdx = 0; mbPartIdx < 4; mbPartIdx++)
                {
                    if (subMbPrediction.Value.SubMbType[mbPartIdx] != B_Direct_8x8)
                    {
                        if (Util264.NumSubMbPart((int)subMbPrediction.Value.SubMbType[mbPartIdx], sliceType) > 1)
                            noSubMbPartSizeLessThan8x8Flag = false;
                    }
                    else if (!direct8x8InferenceFlag)
                    {
                        noSubMbPartSizeLessThan8x8Flag = false;
                    }
                }
            }
            else
            {
                if (transform8x8ModeFlag && mbType == I_NxN)
                    transformSize8x8Flag = codingMode == EntropyCodingMode.Cavlc ? reader.ReadBit() : Int32Boolean.B(reader.ReadAE());
                mbPrediction = MacroblockPrediction.Read(reader, (int)mbType, mbaffFrameFlag, codingMode, sliceType, transformSize8x8Flag, numRefIdxL0ActiveMinus1, numRefIdxL1ActiveMinus1, mbFieldDecodingFlag, fieldPicFlag, chromaArrayType);
            }

            if (Util264.MbPartPredMode((int)mbType, 0, transformSize8x8Flag, sliceType) != Intra_16x16)
            {
                codedBlockPattern = codingMode == EntropyCodingMode.Cavlc ? reader.ReadME() : reader.ReadAE();

                CodedBlockPatternLuma = codedBlockPattern % 16;
                CodedBlockPatternChroma = codedBlockPattern / 16;

                if (CodedBlockPatternLuma > 0 && transformSize8x8Flag && mbType != I_NxN &&
                    noSubMbPartSizeLessThan8x8Flag && (mbType != B_Direct_16x16 || direct8x8InferenceFlag))
                {
                    transformSize8x8Flag = codingMode == EntropyCodingMode.Cavlc ? reader.ReadBit() : Int32Boolean.B(reader.ReadAE());
                }
            }

            if (CodedBlockPatternLuma > 0 || CodedBlockPatternChroma > 0 ||
                Util264.MbPartPredMode((int)mbType, 0, transformSize8x8Flag, sliceType) == Intra_16x16)
            {
                mbQpDelta = codingMode == EntropyCodingMode.Cavlc ? reader.ReadSE() : reader.ReadAE();
                int z = 0;
                residual = Models.Residual.Read(reader, codingMode, chromaArrayType, transformSize8x8Flag, (int)mbType, CodedBlockPatternLuma, sliceType, 0, 15, nalu, dc, ref z, ref z, ref z, 0, util, mode, constrainedIntraPredFlag, subWidthC, subHeightC, CodedBlockPatternChroma);
            }
        }

        return new MacroblockLayer(mbType, pcmLuma, pcmChroma, transformSize8x8Flag, codedBlockPattern, mbQpDelta, residual, mbPrediction, subMbPrediction);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is MacroblockLayer layer && Equals(layer);
    }

    /// <summary>
    /// Determines whether the current instance is equal to another <see cref="MacroblockLayer"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="MacroblockLayer"/> instance to compare with.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(MacroblockLayer other)
    {
        return MbType == other.MbType &&
               PcmLuma.Equals(other.PcmLuma) &&
               PcmChroma.Equals(other.PcmChroma) &&
               TransformSize8x8Flag == other.TransformSize8x8Flag &&
               CodedBlockPattern == other.CodedBlockPattern &&
               MbQpDelta == other.MbQpDelta &&
               EqualityComparer<Residual?>.Default.Equals(Intra16x16Residual, other.Intra16x16Residual);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(MbType, PcmLuma, PcmChroma, TransformSize8x8Flag, CodedBlockPattern, MbQpDelta, Intra16x16Residual);
    }

    /// <summary>
    /// Determines whether two <see cref="MacroblockLayer"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="MacroblockLayer"/> instance.</param>
    /// <param name="right">The second <see cref="MacroblockLayer"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MacroblockLayer left, MacroblockLayer right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="MacroblockLayer"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="MacroblockLayer"/> instance.</param>
    /// <param name="right">The second <see cref="MacroblockLayer"/> instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MacroblockLayer left, MacroblockLayer right)
    {
        return !(left == right);
    }
}
