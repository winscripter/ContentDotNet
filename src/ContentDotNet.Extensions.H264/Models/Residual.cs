using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H26x;
using ContentDotNet.Primitives;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
///   Specifies the type of residual block in H.264 video coding.
/// </summary>
public enum ResidualBlockType
{
    /// <summary>
    ///   DC coefficients of the Intra 16x16 luma block.
    /// </summary>
    Intra16x16DCLevel,

    /// <summary>
    ///   AC coefficients of the Intra 16x16 luma block.
    /// </summary>
    Intra16x16ACLevel,

    /// <summary>
    ///   Luma coefficients in 4x4 transform blocks.
    /// </summary>
    LumaLevel4x4,

    /// <summary>
    ///   Luma coefficients in 8x8 transform blocks.
    /// </summary>
    LumaLevel8x8,

    /// <summary>
    ///   DC coefficients of the chroma blue (Cb) 16x16 block.
    /// </summary>
    Cb16x16DCLevel,

    /// <summary>
    ///   AC coefficients of the chroma blue (Cb) 16x16 block.
    /// </summary>
    Cb16x16ACLevel,

    /// <summary>
    ///   Chroma blue (Cb) coefficients in 4x4 transform blocks.
    /// </summary>
    CbLevel4x4,

    /// <summary>
    ///   Chroma blue (Cb) coefficients in 8x8 transform blocks.
    /// </summary>
    CbLevel8x8,

    /// <summary>
    ///   DC coefficients of the chroma red (Cr) 16x16 block.
    /// </summary>
    Cr16x16DCLevel,

    /// <summary>
    ///   AC coefficients of the chroma red (Cr) 16x16 block.
    /// </summary>
    Cr16x16ACLevel,

    /// <summary>
    ///   Chroma red (Cr) coefficients in 4x4 transform blocks.
    /// </summary>
    CrLevel4x4,

    /// <summary>
    ///   Chroma red (Cr) coefficients in 8x8 transform blocks.
    /// </summary>
    CrLevel8x8,

    /// <summary>
    ///   DC coefficients of the chroma block.
    /// </summary>
    ChromaDCLevel,

    /// <summary>
    ///   AC coefficients of the chroma block.
    /// </summary>
    ChromaACLevel,
}

/// <summary>
///   Residual Mode can be used to specify the current operation in the residual.
/// </summary>
public enum ResidualMode
{
    /// <summary>
    ///   Invoked for the Chroma DC level.
    /// </summary>
    ChromaDcLevel,

    /// <summary>
    ///   Invoked for the Intra 16x16 DC level.
    /// </summary>
    Intra16x16DcLevel,

    /// <summary>
    ///   Invoked for the Intra 16x16 AC level.
    /// </summary>
    Intra16x16AcLevel,

    /// <summary>
    ///   Invoked for the Intra 16x16 DC Level under the Chrominance #1 channel.
    /// </summary>
    CbIntra16x16DcLevel,

    /// <summary>
    ///   Invoked for the Intra 16x16 DC Level under the Chrominance #2 channel.
    /// </summary>
    CrIntra16x16DcLevel,

    /// <summary>
    ///   Invoked for the Luma 4x4 Level.
    /// </summary>
    LumaLevel4x4,

    /// <summary>
    ///   Invoked for the Chroma 4x4 Level #1.
    /// </summary>
    CbLevel4x4,

    /// <summary>
    ///   Invoked for the Chroma 4x4 Level #2.
    /// </summary>
    CrLevel4x4,

    /// <summary>
    ///   Invoked for Intra 16x16 AC Level for Chrominance #1 channels.
    /// </summary>
    CbIntra16x16AcLevel,

    /// <summary>
    ///   Invoked for Intra 16x16 AC Level for Chrominance #2 channels.
    /// </summary>
    CrIntra16x16AcLevel,
}

/// <summary>
///   Represents a CAVLC residual.
/// </summary>
public struct CavlcResidual : IEquatable<CavlcResidual>
{
    /// <summary>
    ///   The coefficient token.
    /// </summary>
    public uint CoeffToken;

    /// <summary>
    ///   Up to 16 trailing ones sign flags.
    /// </summary>
    public Container16Boolean TrailingOnesSignFlag;

    /// <summary>
    ///   Up to 16 level prefixes.
    /// </summary>
    public Container16UInt32 LevelPrefix;

    /// <summary>
    ///   Up to 16 level suffixes.
    /// </summary>
    public Container16UInt32 LevelSuffix;

    /// <summary>
    ///   Total zeros of type CE(v)
    /// </summary>
    public uint TotalZeros;

    /// <summary>
    ///   Up to 16 Run Before.
    /// </summary>
    public Container16UInt32 RunBefore;

    /// <summary>
    ///   Up to 16 Level values.
    /// </summary>
    public Container16UInt32 LevelVal;

    /// <summary>
    ///   Up to 16 Run values.
    /// </summary>
    public Container16UInt32 RunVal;

    /// <summary>
    ///   Precomputed total coefficients of <see cref="CoeffToken"/>.
    /// </summary>
    public int TotalCoeff;

    /// <summary>
    ///   Precomputed trailing ones of <see cref="CoeffToken"/>.
    /// </summary>
    public int TrailingOnes;

    /// <summary>
    ///   Initializes a new instance of the <see cref="CavlcResidual"/> structure.
    /// </summary>
    /// <param name="coeffToken">Coefficient token</param>
    /// <param name="trailingOnesSignFlag">Up to 16</param>
    /// <param name="levelPrefix">Up to 16</param>
    /// <param name="levelSuffix">Up to 16</param>
    /// <param name="totalZeros">Total zeros</param>
    /// <param name="runBefore">Up to 16</param>
    /// <param name="levelVal">Up to 16</param>
    /// <param name="runVal">Up to 16</param>
    /// <param name="totalCoeff">Total coefficient of <paramref name="coeffToken"/>.</param>
    /// <param name="trailingOnes">Trailing ones of <paramref name="coeffToken"/>.</param>
    public CavlcResidual(uint coeffToken, Container16Boolean trailingOnesSignFlag, Container16UInt32 levelPrefix, Container16UInt32 levelSuffix, uint totalZeros, Container16UInt32 runBefore, Container16UInt32 levelVal, Container16UInt32 runVal, int totalCoeff, int trailingOnes)
    {
        CoeffToken = coeffToken;
        TrailingOnesSignFlag = trailingOnesSignFlag;
        LevelPrefix = levelPrefix;
        LevelSuffix = levelSuffix;
        TotalZeros = totalZeros;
        RunBefore = runBefore;
        LevelVal = levelVal;
        RunVal = runVal;
        TotalCoeff = totalCoeff;
        TrailingOnes = trailingOnes;
    }

    /// <summary>
    ///   Reads the CAVLC residual.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="coeffLevel"></param>
    /// <param name="startIdx"></param>
    /// <param name="endIdx"></param>
    /// <param name="maxNumCoeff"></param>
    /// <param name="nalu"></param>
    /// <param name="dc"></param>
    /// <param name="chromaArrayType"></param>
    /// <param name="luma4x4BlkIdx"></param>
    /// <param name="cb4x4BlkIdx"></param>
    /// <param name="cr4x4BlkIdx"></param>
    /// <param name="chroma4x4BlkIdx"></param>
    /// <param name="mode"></param>
    /// <param name="util"></param>
    /// <param name="constrainedIntraPredFlag"></param>
    /// <returns></returns>
    /// <exception cref="VideoCodecDecoderException"></exception>
    public static CavlcResidual Read(
        BitStreamReader reader,
        Span<uint> coeffLevel,
        int startIdx,
        int endIdx,
        int maxNumCoeff,
        NalUnit nalu,
        DerivationContext dc,
        int chromaArrayType,
        ref int luma4x4BlkIdx,
        ref int cb4x4BlkIdx,
        ref int cr4x4BlkIdx,
        int chroma4x4BlkIdx,
        ResidualMode mode,
        IMacroblockUtility util,
        bool constrainedIntraPredFlag)
    {
        for (int i = 0; i < maxNumCoeff; i++)
            coeffLevel[i] = 0u;

        uint coeffToken = reader.ReadCE();

        int TotalCoeff;
        int TrailingOnes;

        int nC = Cavlc.GetNC(reader, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);
        var totalCoeffAndTrailingOnes = Cavlc.DecodeCoeffToken(reader, nC)
                                        ?? throw new VideoCodecDecoderException("Could not retrieve TotalCoeff(coeff_token) and TrailingOnes(coeff_token) for CAVLC residual block");

        TotalCoeff = totalCoeffAndTrailingOnes.TotalCoeff;
        TrailingOnes = totalCoeffAndTrailingOnes.TrailingOnes;

        Container16Boolean trailingOnesSignFlag = new();
        Container16UInt32 levelVal = new();
        Container16UInt32 levelPrefix = new();
        Container16UInt32 levelSuffix = new();
        uint totalZeros = 0;

        Container16UInt32 runBefore = new();
        Container16UInt32 runVal = new();

        if (TotalCoeff > 0)
        {
            int suffixLength = TotalCoeff > 10 && TrailingOnes < 3 ? 1 : 0;
            for (int i = 0; i < TotalCoeff; i++)
            {
                if (i < TrailingOnes)
                {
                    trailingOnesSignFlag[i] = reader.ReadBit();
                    levelVal[i] = 1u - 2u * Int32Boolean.U32(trailingOnesSignFlag[i]);
                }
                else
                {
                    levelPrefix[i] = reader.ReadCE();
                    uint currLvlPrefix = levelPrefix[i];

                    uint levelCode = Math.Min(15u, currLvlPrefix) << suffixLength;
                    if (suffixLength > 0 || currLvlPrefix >= 14)
                    {
                        levelSuffix[i] = reader.ReadBits(levelCode - 15u);
                        levelCode += levelSuffix[i];
                    }

                    if (currLvlPrefix >= 15 && suffixLength == 0)
                        levelCode += 15;

                    if (currLvlPrefix >= 16)
                        levelCode += (1u << ((int)currLvlPrefix - 3)) - 4096u;

                    if (i == TrailingOnes && TrailingOnes < 3)
                        levelCode += 2;

                    if (levelCode % 2 == 0)
                        levelVal[i] = (levelCode + 2) >> 1;
                    else
                        levelVal[i] = (uint)(int)(-levelCode - 1) >> 1;

                    if (suffixLength == 0)
                        suffixLength = 1;

                    if (Math.Abs(levelVal[i]) > (3 << (suffixLength - 1)) && suffixLength < 6)
                        suffixLength++;
                }
            }

            uint zerosLeft = 0u;

            if (TotalCoeff < endIdx - startIdx + 1)
            {
                totalZeros = reader.ReadCE();
                zerosLeft = totalZeros;
            }

            for (int i = 0; i < TotalCoeff - 1; i++)
            {
                if (zerosLeft > 0)
                {
                    runBefore[i] = reader.ReadCE();
                    runVal[i] = runBefore[i];
                }
                else
                {
                    runVal[i] = 0u;
                }

                zerosLeft -= runVal[i];
            }

            runVal[TotalCoeff - 1] = zerosLeft;

            int coeffNum = -1;
            for (int i = TotalCoeff - 1; i >= 0; i--)
            {
                coeffNum += (int)runVal[i] + 1;
                coeffLevel[startIdx + coeffNum] = levelVal[i];
            }
        }

        return new CavlcResidual(
            coeffToken,
            trailingOnesSignFlag,
            levelPrefix,
            levelSuffix,
            totalZeros,
            runBefore,
            levelVal,
            runVal,
            TotalCoeff,
            TrailingOnes);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified object is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is CavlcResidual residual && Equals(residual);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CavlcResidual"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="CavlcResidual"/> to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="CavlcResidual"/> is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool Equals(CavlcResidual other)
    {
        return CoeffToken == other.CoeffToken &&
               TrailingOnesSignFlag.Equals(other.TrailingOnesSignFlag) &&
               LevelPrefix.Equals(other.LevelPrefix) &&
               LevelSuffix.Equals(other.LevelSuffix) &&
               TotalZeros == other.TotalZeros &&
               RunBefore.Equals(other.RunBefore) &&
               LevelVal.Equals(other.LevelVal) &&
               RunVal.Equals(other.RunVal) &&
               TotalCoeff == other.TotalCoeff &&
               TrailingOnes == other.TrailingOnes;
    }

    /// <summary>
    ///   Returns the hash code for the CAVLC residual.
    /// </summary>
    /// <returns>CAVLC residual hash code.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(CoeffToken);
        hash.Add(TrailingOnesSignFlag);
        hash.Add(LevelPrefix);
        hash.Add(LevelSuffix);
        hash.Add(TotalZeros);
        hash.Add(RunBefore);
        hash.Add(LevelVal);
        hash.Add(RunVal);
        hash.Add(TotalCoeff);
        hash.Add(TrailingOnes);
        return hash.ToHashCode();
    }

    /// <summary>
    ///   Writes this CAVLC residual to the bitstream.
    /// </summary>
    /// <param name="writer">Writer</param>
    /// <param name="coeffLevel">Coefficient level</param>
    /// <param name="startIdx">Start index</param>
    /// <param name="endIdx">End index</param>
    /// <param name="maxNumCoeff">Maximum coefficients</param>
    /// <param name="nC">nC</param>
    public void Write(
        BitStreamWriter writer,
        Span<uint> coeffLevel,
        int startIdx,
        int endIdx,
        int maxNumCoeff,
        int nC)
    {
        for (int i = 0; i < maxNumCoeff; i++)
            coeffLevel[i] = 0u;

        writer.WriteCE(this.CoeffToken);

        (byte vlc, int size) = Cavlc.GetVlcAndSize((int)CoeffToken, nC);
        writer.WriteBits(vlc, (uint)size);

        Container16UInt32 levelVal = new();
        Container16UInt32 runVal = new();

        if (TotalCoeff > 0)
        {
            int suffixLength = TotalCoeff > 10 && TrailingOnes < 3 ? 1 : 0;
            for (int i = 0; i < TotalCoeff; i++)
            {
                if (i < TrailingOnes)
                {
                    writer.WriteBit(TrailingOnesSignFlag[i]);
                    levelVal[i] = 1u - 2u * Int32Boolean.U32(TrailingOnesSignFlag[i]);
                }
                else
                {
                    writer.WriteCE(LevelPrefix[i]);
                    uint currLvlPrefix = LevelPrefix[i];

                    uint levelCode = Math.Min(15u, currLvlPrefix) << suffixLength;
                    if (suffixLength > 0 || currLvlPrefix >= 14)
                    {
                        writer.WriteBits(LevelSuffix[i], levelCode - 15u);
                        levelCode += LevelSuffix[i];
                    }

                    if (currLvlPrefix >= 15 && suffixLength == 0)
                        levelCode += 15;

                    if (currLvlPrefix >= 16)
                        levelCode += (1u << ((int)currLvlPrefix - 3)) - 4096u;

                    if (i == TrailingOnes && TrailingOnes < 3)
                        levelCode += 2;

                    if (levelCode % 2 == 0)
                        levelVal[i] = (levelCode + 2) >> 1;
                    else
                        levelVal[i] = (uint)(int)(-levelCode - 1) >> 1;

                    if (suffixLength == 0)
                        suffixLength = 1;

                    if (Math.Abs(levelVal[i]) > (3 << (suffixLength - 1)) && suffixLength < 6)
                        suffixLength++;
                }
            }

            uint zerosLeft = 0u;

            if (TotalCoeff < endIdx - startIdx + 1)
            {
                writer.WriteCE(TotalZeros);
                zerosLeft = TotalZeros;
            }

            for (int i = 0; i < TotalCoeff - 1; i++)
            {
                if (zerosLeft > 0)
                {
                    writer.WriteCE(RunBefore[i]);
                    runVal[i] = RunBefore[i];
                }
                else
                {
                    runVal[i] = 0u;
                }

                zerosLeft -= runVal[i];
            }

            runVal[TotalCoeff - 1] = zerosLeft;

            int coeffNum = -1;
            for (int i = TotalCoeff - 1; i >= 0; i--)
            {
                coeffNum += (int)runVal[i] + 1;
                coeffLevel[startIdx + coeffNum] = levelVal[i];
            }
        }
    }

    /// <summary>
    ///   Writes this CAVLC residual to the bitstream.
    /// </summary>
    /// <param name="writer">Writer</param>
    /// <param name="coeffLevel">Coefficient level</param>
    /// <param name="startIdx">Start index</param>
    /// <param name="endIdx">End index</param>
    /// <param name="maxNumCoeff">Maximum coefficients</param>
    /// <param name="nC">nC</param>
    public async Task WriteAsync(
        BitStreamWriter writer,
        Memory<uint> coeffLevel,
        int startIdx,
        int endIdx,
        int maxNumCoeff,
        int nC)
    {
        for (int i = 0; i < maxNumCoeff; i++)
            coeffLevel.Span[i] = 0u;

        await writer.WriteCEAsync(this.CoeffToken);

        (byte vlc, int size) = Cavlc.GetVlcAndSize((int)CoeffToken, nC);
        writer.WriteBits(vlc, (uint)size);

        Container16UInt32 levelVal = new();
        Container16UInt32 runVal = new();

        if (TotalCoeff > 0)
        {
            int suffixLength = TotalCoeff > 10 && TrailingOnes < 3 ? 1 : 0;
            for (int i = 0; i < TotalCoeff; i++)
            {
                if (i < TrailingOnes)
                {
                    await writer.WriteBitAsync(TrailingOnesSignFlag[i]);
                    levelVal[i] = 1u - 2u * Int32Boolean.U32(TrailingOnesSignFlag[i]);
                }
                else
                {
                    await writer.WriteCEAsync(LevelPrefix[i]);
                    uint currLvlPrefix = LevelPrefix[i];

                    uint levelCode = Math.Min(15u, currLvlPrefix) << suffixLength;
                    if (suffixLength > 0 || currLvlPrefix >= 14)
                    {
                        writer.WriteBits(LevelSuffix[i], levelCode - 15u);
                        levelCode += LevelSuffix[i];
                    }

                    if (currLvlPrefix >= 15 && suffixLength == 0)
                        levelCode += 15;

                    if (currLvlPrefix >= 16)
                        levelCode += (1u << ((int)currLvlPrefix - 3)) - 4096u;

                    if (i == TrailingOnes && TrailingOnes < 3)
                        levelCode += 2;

                    if (levelCode % 2 == 0)
                        levelVal[i] = (levelCode + 2) >> 1;
                    else
                        levelVal[i] = (uint)(int)(-levelCode - 1) >> 1;

                    if (suffixLength == 0)
                        suffixLength = 1;

                    if (Math.Abs(levelVal[i]) > (3 << (suffixLength - 1)) && suffixLength < 6)
                        suffixLength++;
                }
            }

            uint zerosLeft = 0u;

            if (TotalCoeff < endIdx - startIdx + 1)
            {
                await writer.WriteCEAsync(TotalZeros);
                zerosLeft = TotalZeros;
            }

            for (int i = 0; i < TotalCoeff - 1; i++)
            {
                if (zerosLeft > 0)
                {
                    await writer.WriteCEAsync(RunBefore[i]);
                    runVal[i] = RunBefore[i];
                }
                else
                {
                    runVal[i] = 0u;
                }

                zerosLeft -= runVal[i];
            }

            runVal[TotalCoeff - 1] = zerosLeft;

            int coeffNum = -1;
            for (int i = TotalCoeff - 1; i >= 0; i--)
            {
                coeffNum += (int)runVal[i] + 1;
                coeffLevel.Span[startIdx + coeffNum] = levelVal[i];
            }
        }
    }


    /// <summary>
    /// Determines whether two <see cref="CavlcResidual"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="CavlcResidual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="CavlcResidual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="CavlcResidual"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(CavlcResidual left, CavlcResidual right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="CavlcResidual"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="CavlcResidual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="CavlcResidual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="CavlcResidual"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(CavlcResidual left, CavlcResidual right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a CABAC residual.
/// </summary>
public struct CabacResidual : IEquatable<CabacResidual>
{
    /// <summary>
    /// Indicates whether the block is coded.
    /// </summary>
    public bool CodedBlockFlag;

    /// <summary>
    /// The maximum number of coefficients in the residual block.
    /// </summary>
    public int MaxNumCoeff;

    /// <summary>
    /// The chroma array type of the residual block.
    /// </summary>
    public int ChromaArrayType;

    /// <summary>
    /// Flags indicating whether each coefficient is significant.
    /// </summary>
    public Container64Boolean SignificantCoeffFlag;

    /// <summary>
    /// Flags indicating whether each coefficient is the last significant coefficient.
    /// </summary>
    public Container64Boolean LastSignificantCoeffFlag;

    /// <summary>
    /// The absolute level minus one for each coefficient.
    /// </summary>
    public Container64UInt32 CoeffAbsLevelMinus1;

    /// <summary>
    /// Flags indicating the sign of each coefficient.
    /// </summary>
    public Container64Boolean CoeffSignFlag;

    /// <summary>
    /// Start index.
    /// </summary>
    public int StartIdx;

    /// <summary>
    /// End index.
    /// </summary>
    public int EndIdx;

    /// <summary>
    /// Initializes a new instance of the <see cref="CabacResidual"/> struct.
    /// </summary>
    /// <param name="codedBlockFlag">Indicates whether the block is coded.</param>
    /// <param name="maxNumCoeff">The maximum number of coefficients in the residual block.</param>
    /// <param name="chromaArrayType">The chroma array type of the residual block.</param>
    /// <param name="significantCoeffFlag">Flags indicating whether each coefficient is significant.</param>
    /// <param name="lastSignificantCoeffFlag">Flags indicating whether each coefficient is the last significant coefficient.</param>
    /// <param name="coeffAbsLevelMinus1">The absolute level minus one for each coefficient.</param>
    /// <param name="coeffSignFlag">Flags indicating the sign of each coefficient.</param>
    /// <param name="startIdx">Start index.</param>
    /// <param name="endIdx">End index.</param>
    public CabacResidual(
        bool codedBlockFlag,
        int maxNumCoeff,
        int chromaArrayType,
        Container64Boolean significantCoeffFlag,
        Container64Boolean lastSignificantCoeffFlag,
        Container64UInt32 coeffAbsLevelMinus1,
        Container64Boolean coeffSignFlag,
        int startIdx,
        int endIdx)
    {
        CodedBlockFlag = codedBlockFlag;
        MaxNumCoeff = maxNumCoeff;
        ChromaArrayType = chromaArrayType;
        SignificantCoeffFlag = significantCoeffFlag;
        LastSignificantCoeffFlag = lastSignificantCoeffFlag;
        CoeffAbsLevelMinus1 = coeffAbsLevelMinus1;
        CoeffSignFlag = coeffSignFlag;
        StartIdx = startIdx;
        EndIdx = endIdx;
    }

    /// <summary>
    ///   Reads the CABAC residual from the given bitstream.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="coeffLevel"></param>
    /// <param name="startIdx"></param>
    /// <param name="endIdx"></param>
    /// <param name="maxNumCoeff"></param>
    /// <param name="chromaArrayType"></param>
    /// <returns></returns>
    public static CabacResidual Read(BitStreamReader reader, Span<uint> coeffLevel, int startIdx, int endIdx, int maxNumCoeff, int chromaArrayType)
    {
        bool codedBlockFlag = false;
        if (maxNumCoeff != 64 || chromaArrayType == 3)
            codedBlockFlag = Int32Boolean.B(reader.ReadAE());

        for (int i = 0; i < maxNumCoeff; i++)
            coeffLevel[i] = 0u;

        Container64Boolean significantCoeffFlag = new();
        Container64Boolean lastSignificantCoeffFlag = new();
        Container64UInt32 coeffAbsLevelMinus1 = new();
        Container64Boolean coeffSignFlag = new();

        if (codedBlockFlag)
        {
            int numCoeff = endIdx + 1;
            int i = startIdx;
            while (i < numCoeff - 1)
            {
                significantCoeffFlag[i] = Int32Boolean.B(reader.ReadAE());
                if (significantCoeffFlag[i])
                {
                    lastSignificantCoeffFlag[i] = Int32Boolean.B(reader.ReadAE());
                    if (lastSignificantCoeffFlag[i])
                        numCoeff = i + 1;
                }
                i++;
            }

            coeffAbsLevelMinus1[numCoeff - 1] = (uint)reader.ReadAE();
            coeffSignFlag[numCoeff - 1] = Int32Boolean.B(reader.ReadAE());

            coeffLevel[numCoeff - 1] = (coeffAbsLevelMinus1[numCoeff - 1] + 1) *
                                       (1 - 2 * Int32Boolean.U32(coeffSignFlag[numCoeff - 1]));

            for (i = numCoeff - 2; i >= startIdx; i--)
            {
                if (significantCoeffFlag[i])
                {
                    coeffAbsLevelMinus1[i] = (uint)reader.ReadAE();
                    coeffSignFlag[i] = Int32Boolean.B(reader.ReadAE());
                    coeffLevel[i] = (coeffAbsLevelMinus1[i] + 1) *
                                    (1 - 2 * Int32Boolean.U32(coeffSignFlag[i]));
                }
            }
        }

        return new CabacResidual(codedBlockFlag, maxNumCoeff, chromaArrayType, significantCoeffFlag, lastSignificantCoeffFlag, coeffAbsLevelMinus1, coeffSignFlag, startIdx, endIdx);
    }

    /// <summary>
    ///   Writes the CABAC residual to the given bitstream writer.
    /// </summary>
    /// <param name="writer">Bitstream writer</param>
    /// <param name="coeffLevel">All coefficient levels.</param>
    public readonly void Write(BitStreamWriter writer, Span<uint> coeffLevel)
    {
        if (MaxNumCoeff != 64 || ChromaArrayType == 3)
            writer.WriteAE(Int32Boolean.I32(CodedBlockFlag));

        for (int i = 0; i < MaxNumCoeff; i++)
            coeffLevel[i] = 0u;

        if (CodedBlockFlag)
        {
            int numCoeff = EndIdx + 1;
            int i = StartIdx;
            while (i < numCoeff - 1)
            {
                writer.WriteAE(Int32Boolean.I32(SignificantCoeffFlag[i]));
                if (SignificantCoeffFlag[i])
                {
                    writer.WriteAE(Int32Boolean.I32(LastSignificantCoeffFlag[i]));
                    if (LastSignificantCoeffFlag[i])
                        numCoeff = i + 1;
                }
                i++;
            }

            writer.WriteAE((int)CoeffAbsLevelMinus1[numCoeff - 1]);
            writer.WriteAE(Int32Boolean.I32(CoeffSignFlag[numCoeff - 1]));

            coeffLevel[numCoeff - 1] = (CoeffAbsLevelMinus1[numCoeff - 1] + 1) *
                                       (1 - 2 * Int32Boolean.U32(CoeffSignFlag[numCoeff - 1]));

            for (i = numCoeff - 2; i >= StartIdx; i--)
            {
                if (SignificantCoeffFlag[i])
                {
                    writer.WriteAE((int)CoeffAbsLevelMinus1[i]);
                    writer.WriteAE(Int32Boolean.I32(CoeffSignFlag[i]));
                    coeffLevel[i] = (CoeffAbsLevelMinus1[i] + 1) *
                                    (1 - 2 * Int32Boolean.U32(CoeffSignFlag[i]));
                }
            }
        }
    }

    /// <summary>
    ///   Writes the CABAC residual to the given bitstream writer.
    /// </summary>
    /// <param name="writer">Bitstream writer</param>
    /// <param name="coeffLevel">All coefficient levels.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<uint> coeffLevel)
    {
        if (MaxNumCoeff != 64 || ChromaArrayType == 3)
            await writer.WriteAEAsync(Int32Boolean.I32(CodedBlockFlag));

        for (int i = 0; i < MaxNumCoeff; i++)
            coeffLevel.Span[i] = 0u;

        if (CodedBlockFlag)
        {
            int numCoeff = EndIdx + 1;
            int i = StartIdx;
            while (i < numCoeff - 1)
            {
                await writer.WriteAEAsync(Int32Boolean.I32(SignificantCoeffFlag[i]));
                if (SignificantCoeffFlag[i])
                {
                    await writer.WriteAEAsync(Int32Boolean.I32(LastSignificantCoeffFlag[i]));
                    if (LastSignificantCoeffFlag[i])
                        numCoeff = i + 1;
                }
                i++;
            }

            await writer.WriteAEAsync((int)CoeffAbsLevelMinus1[numCoeff - 1]);
            await writer.WriteAEAsync(Int32Boolean.I32(CoeffSignFlag[numCoeff - 1]));

            coeffLevel.Span[numCoeff - 1] = (CoeffAbsLevelMinus1[numCoeff - 1] + 1) *
                                       (1 - 2 * Int32Boolean.U32(CoeffSignFlag[numCoeff - 1]));

            for (i = numCoeff - 2; i >= StartIdx; i--)
            {
                if (SignificantCoeffFlag[i])
                {
                    await writer.WriteAEAsync((int)CoeffAbsLevelMinus1[i]);
                    await writer.WriteAEAsync(Int32Boolean.I32(CoeffSignFlag[i]));
                    coeffLevel.Span[i] = (CoeffAbsLevelMinus1[i] + 1) *
                                    (1 - 2 * Int32Boolean.U32(CoeffSignFlag[i]));
                }
            }
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is CabacResidual residual && Equals(residual);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CabacResidual"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="CabacResidual"/> to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="CabacResidual"/> is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool Equals(CabacResidual other)
    {
        return CodedBlockFlag == other.CodedBlockFlag &&
               MaxNumCoeff == other.MaxNumCoeff &&
               ChromaArrayType == other.ChromaArrayType &&
               SignificantCoeffFlag.Equals(other.SignificantCoeffFlag) &&
               LastSignificantCoeffFlag.Equals(other.LastSignificantCoeffFlag) &&
               CoeffAbsLevelMinus1.Equals(other.CoeffAbsLevelMinus1) &&
               CoeffSignFlag.Equals(other.CoeffSignFlag) &&
               StartIdx == other.StartIdx &&
               EndIdx == other.EndIdx;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(CodedBlockFlag);
        hash.Add(MaxNumCoeff);
        hash.Add(ChromaArrayType);
        hash.Add(SignificantCoeffFlag);
        hash.Add(LastSignificantCoeffFlag);
        hash.Add(CoeffAbsLevelMinus1);
        hash.Add(CoeffSignFlag);
        hash.Add(StartIdx);
        hash.Add(EndIdx);

        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="CabacResidual"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="CabacResidual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="CabacResidual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="CabacResidual"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(CabacResidual left, CabacResidual right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="CabacResidual"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="CabacResidual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="CabacResidual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="CabacResidual"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(CabacResidual left, CabacResidual right)
    {
        return !(left == right);
    }
}

/// <summary>
///   Represents a residual luma.
/// </summary>
public struct ResidualLuma : IEquatable<ResidualLuma>
{
    /// <summary>
    ///   Use <see cref="CabacResidualBlocks"/> (<c>true</c>) or
    ///   <see cref="CavlcResidualBlocks"/> (<c>false</c>)?
    /// </summary>
    public bool UsesCabac;

    /// <summary>
    ///   Initial residual block, only used if 1. <see cref="StartIndex"/>
    ///   is <c>0</c>, 2. <see cref="MbPartPredMode"/> is <see cref="Intra_16x16"/>,
    ///   and 3. <see cref="UsesCabac"/> is <c>true</c>
    /// </summary>
    public CabacResidual? StartResidualCabac;

    /// <summary>
    ///   Initial residual block, only used if 1. <see cref="StartIndex"/>
    ///   is <c>0</c>, 2. <see cref="MbPartPredMode"/> is <see cref="Intra_16x16"/>,
    ///   and 3. <see cref="UsesCabac"/> is <c>false</c>
    /// </summary>
    public CavlcResidual? StartResidualCavlc;

    /// <summary>
    ///   All CABAC residual blocks
    /// </summary>
    public Container16CabacResidual? CabacResidualBlocks;

    /// <summary>
    ///   All CAVLC residual blocks
    /// </summary>
    public Container16CavlcResidual? CavlcResidualBlocks;

    /// <summary>
    ///   Coded block pattern luma used to read the residual.
    /// </summary>
    public int CodedBlockPatternLuma;

    /// <summary>
    ///   MbPartPredMode
    /// </summary>
    public int MbPartPredMode;

    /// <summary>
    ///   Entropy coding mode flag.
    /// </summary>
    public bool EntropyCodingModeFlag;

    /// <summary>
    ///   Transform size 8x8 flag
    /// </summary>
    public bool TransformSize8x8Flag;

    /// <summary>
    ///   Start index
    /// </summary>
    public int StartIndex;

    /// <summary>
    ///   End index.
    /// </summary>
    public int EndIndex;

    /// <summary>  
    /// Initializes a new instance of the <see cref="ResidualLuma"/> struct.  
    /// </summary>  
    /// <param name="usesCabac">Indicates whether CABAC is used (<c>true</c>) or CAVLC (<c>false</c>).</param>
    /// <param name="startCabac">Initial CABAC</param>
    /// <param name="startCavlc">Initial CAVLC</param>
    /// <param name="cabacResidualBlocks">The CABAC residual blocks, if CABAC is used.</param>
    /// <param name="cavlcResidualBlocks">The CAVLC residual blocks, if CAVLC is used.</param>
    /// <param name="codedBlockPatternLuma">The coded block pattern for luma.</param>
    /// <param name="mbPartPredMode">The macroblock partition prediction mode.</param>
    /// <param name="entropyCodingModeFlag">The entropy coding mode flag.</param>
    /// <param name="transformSize8x8Flag">Indicates whether 8x8 transform size is used.</param>
    /// <param name="startIndex">The start index of the residual luma.</param>
    /// <param name="endIndex">The end index of the residual luma.</param>
    public ResidualLuma(
        bool usesCabac,
        CabacResidual? startCabac,
        CavlcResidual? startCavlc,
        Container16CabacResidual? cabacResidualBlocks,
        Container16CavlcResidual? cavlcResidualBlocks,
        int codedBlockPatternLuma,
        int mbPartPredMode,
        bool entropyCodingModeFlag,
        bool transformSize8x8Flag,
        int startIndex,
        int endIndex)
    {
        UsesCabac = usesCabac;
        StartResidualCabac = startCabac;
        StartResidualCavlc = startCavlc;
        CabacResidualBlocks = cabacResidualBlocks;
        CavlcResidualBlocks = cavlcResidualBlocks;
        CodedBlockPatternLuma = codedBlockPatternLuma;
        MbPartPredMode = mbPartPredMode;
        EntropyCodingModeFlag = entropyCodingModeFlag;
        TransformSize8x8Flag = transformSize8x8Flag;
        StartIndex = startIndex;
        EndIndex = endIndex;
    }

    /// <summary>
    ///   Reads the residual luma.
    /// </summary>
    /// <param name="reader">Bitstream to read from</param>
    /// <param name="entropyCodingMode">Taken from PPS</param>
    /// <param name="transformSize8x8Flag">Taken from PPS</param>
    /// <param name="mbType">Macroblock type</param>
    /// <param name="codedBlockPatternLuma">Coded block pattern luma</param>
    /// <param name="chromaArrayType">See <see cref="H264Extensions.GetChromaArrayType(SequenceParameterSet)"/></param>
    /// <param name="i16x16DCLevel">DC Level</param>
    /// <param name="i16x16ACLevel">AC Level</param>
    /// <param name="level8x8">8x8 Level</param>
    /// <param name="level4x4">4x4 Level</param>
    /// <param name="sliceType">Taken from the Slice Header</param>
    /// <param name="startIdx">Start</param>
    /// <param name="endIdx">End</param>
    /// <param name="nalu">NAL unit</param>
    /// <param name="dc">Derivation info</param>
    /// <param name="luma4x4BlkIdx"></param>
    /// <param name="cb4x4BlkIdx"></param>
    /// <param name="cr4x4BlkIdx"></param>
    /// <param name="chroma4x4BlkIdx"></param>
    /// <param name="util">Macroblock utility</param>
    /// <param name="mode">Mode of the residual</param>
    /// <param name="constrainedIntraPredFlag">Taken from PPS</param>
    /// <returns>Residual luma, parsed from <paramref name="reader"/></returns>
    public static ResidualLuma Read(
        BitStreamReader reader,
        EntropyCodingMode entropyCodingMode,
        bool transformSize8x8Flag,
        int mbType,
        int codedBlockPatternLuma,
        int chromaArrayType,
        out Container64UInt32 i16x16DCLevel,
        out ContainerMatrix16x16 i16x16ACLevel,
        out ContainerMatrix4x64 level8x8,
        out ContainerMatrix4x64 level4x4,
        GeneralSliceType sliceType,
        int startIdx,
        int endIdx,
        NalUnit nalu,
        DerivationContext dc,
        ref int luma4x4BlkIdx,
        ref int cb4x4BlkIdx,
        ref int cr4x4BlkIdx,
        int chroma4x4BlkIdx,
        IMacroblockUtility util,
        ResidualMode mode,
        bool constrainedIntraPredFlag)
    {
        i16x16ACLevel = new();
        i16x16DCLevel = new();
        level8x8 = new();
        level4x4 = new();

        int mbPartPredMode = Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType);

        CabacResidual? startCABAC = null;
        CavlcResidual? startCAVLC = null;

        Container16CabacResidual cabacs = new();
        Container16CavlcResidual cavlcs = new();

        if (startIdx == 0 && mbPartPredMode == Intra_16x16)
        {
            if (entropyCodingMode == EntropyCodingMode.Cabac)
            {
                Span<uint> sp = stackalloc uint[64];

                startCABAC = CabacResidual.Read(reader, sp, 0, 15, 16, chromaArrayType);

                for (int i = 0; i < 64; i++)
                    i16x16DCLevel[i] = sp[i];
            }
            else
            {
                Span<uint> sp = stackalloc uint[64];

                startCAVLC = CavlcResidual.Read(reader, sp, 0, 15, 16, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, ResidualMode.Intra16x16DcLevel, util, constrainedIntraPredFlag);

                for (int i = 0; i < 64; i++)
                    i16x16DCLevel[i] = sp[i];
            }
        }

        int residualTop = 0;

        for (int i8x8 = 0; i8x8 < 4; i8x8++)
        {
            if (!transformSize8x8Flag || entropyCodingMode == EntropyCodingMode.Cabac)
            {
                for (int i4x4 = 0; i4x4 < 4; i4x4++)
                {
                    if (Int32Boolean.B(codedBlockPatternLuma & (1 << i8x8)))
                    {
                        if (mbPartPredMode == Intra_16x16)
                        {
                            if (entropyCodingMode == EntropyCodingMode.Cabac)
                            {
                                _Core(i16x16ACLevel);

                                void _Core(ContainerMatrix16x16 i16x16ACLevel)
                                {
                                    Span<uint> acLevel = stackalloc uint[64];

                                    cabacs[residualTop++] = CabacResidual.Read(
                                        reader, acLevel, Math.Max(0, startIdx - 1), endIdx - 1, 15, chromaArrayType);

                                    for (int i = 0; i < 16; i++)
                                        i16x16ACLevel[i8x8 * 4 + i4x4, i] = acLevel[i];
                                }
                            }
                            else
                            {
                                _Core(i16x16ACLevel, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                                void _Core(ContainerMatrix16x16 i16x16ACLevel, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                                {
                                    Span<uint> acLevel = stackalloc uint[16];

                                    cavlcs[residualTop++] = CavlcResidual.Read(
                                        reader, acLevel, Math.Max(0, startIdx - 1), endIdx - 1, 15, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);

                                    for (int i = 0; i < 16; i++)
                                        i16x16ACLevel[i8x8 * 4 + i4x4, i] = acLevel[i];
                                }
                            }
                        }
                        else
                        {
                            if (entropyCodingMode == EntropyCodingMode.Cabac)
                            {
                                _Core(level4x4);

                                void _Core(ContainerMatrix4x64 level4x4)
                                {
                                    Span<uint> lvl4x4 = stackalloc uint[4];

                                    cabacs[residualTop++] = CabacResidual.Read(
                                        reader, lvl4x4, Math.Max(0, startIdx - 1), endIdx - 1, 15, chromaArrayType);

                                    for (int i = 0; i < 4; i++)
                                        level4x4[i8x8 * 4 + i4x4, i] = lvl4x4[i];
                                }
                            }
                            else
                            {
                                _Core(level4x4, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                                void _Core(ContainerMatrix4x64 level4x4, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                                {
                                    Span<uint> lvl4x4 = stackalloc uint[4];

                                    cavlcs[residualTop++] = CavlcResidual.Read(
                                        reader, lvl4x4, Math.Max(0, startIdx - 1), endIdx - 1, 15, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);

                                    for (int i = 0; i < 4; i++)
                                        level4x4[i8x8 * 4 + i4x4, i] = lvl4x4[i];
                                }
                            }
                        }
                    }
                    else if (mbPartPredMode == Intra_16x16)
                    {
                        for (int i = 0; i < 15; i++)
                            i16x16ACLevel[i8x8 * 4 + i4x4, i] = 0;
                    }
                    else
                    {
                        for (int i = 0; i < 16; i++)
                            level4x4[i8x8 * 4 + i4x4, i] = 0;
                    }

                    if (entropyCodingMode == EntropyCodingMode.Cabac && transformSize8x8Flag)
                    {
                        for (int i = 0; i < 16; i++)
                            level8x8[i8x8, 4 * i + i4x4] = (uint)level4x4[i8x8 * 4 + i4x4, i];
                    }
                }
            }
            else if (Int32Boolean.B(codedBlockPatternLuma & (1 << i8x8)))
            {
                if (entropyCodingMode == EntropyCodingMode.Cabac)
                {
                    _Core(level8x8);

                    void _Core(ContainerMatrix4x64 level8x8)
                    {
                        Span<uint> coeffLevel = stackalloc uint[64];

                        cabacs[residualTop++] = CabacResidual.Read(reader, coeffLevel, 4 * startIdx, 4 * endIdx, 64, chromaArrayType);

                        for (int i = 0; i < 64; i++)
                            level8x8[i8x8, i] = coeffLevel[i];
                    }
                }
                else
                {
                    _Core(level8x8, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                    void _Core(ContainerMatrix4x64 level8x8, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                    {
                        Span<uint> coeffLevel = stackalloc uint[64];

                        for (int i = 0; i < 64; i++)
                            coeffLevel[i] = level8x8[i8x8, i];

                        cavlcs[residualTop++] = CavlcResidual.Read(reader, coeffLevel, 4 * startIdx, 4 * endIdx, 64, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 64; i++)
                    level8x8[i8x8, i] = 0;
            }
        }

        return new ResidualLuma(
            entropyCodingMode == EntropyCodingMode.Cabac,
            startCABAC,
            startCAVLC,
            cabacs,
            cavlcs,
            codedBlockPatternLuma,
            mbPartPredMode,
            entropyCodingMode == EntropyCodingMode.Cavlc,
            transformSize8x8Flag,
            startIdx,
            endIdx);
    }

    /// <summary>
    ///   Writes the residual luma.
    /// </summary>
    /// <param name="writer">Bitstream to write to</param>
    /// <param name="entropyCodingMode">Taken from PPS</param>
    /// <param name="transformSize8x8Flag">Taken from PPS</param>
    /// <param name="mbType">Macroblock type</param>
    /// <param name="codedBlockPatternLuma">Coded block pattern luma</param>
    /// <param name="chromaArrayType">See <see cref="H264Extensions.GetChromaArrayType(SequenceParameterSet)"/></param>
    /// <param name="sliceType">Taken from the Slice Header</param>
    /// <param name="startIdx">Start</param>
    /// <param name="endIdx">End</param>
    /// <param name="nalu">NAL unit</param>
    /// <param name="dc">Derivation info</param>
    /// <param name="luma4x4BlkIdx"></param>
    /// <param name="cb4x4BlkIdx"></param>
    /// <param name="cr4x4BlkIdx"></param>
    /// <param name="chroma4x4BlkIdx"></param>
    /// <param name="util">Macroblock utility</param>
    /// <param name="mode">Mode of the residual</param>
    /// <param name="I16x16AcLevel">16x16 AC level</param>
    /// <param name="I16x16DcLevel">16x16 DC level</param>
    /// <param name="Level4x4">Level 4x4</param>
    /// <param name="Level8x8">Level 8x8</param>
    /// <param name="constrainedIntraPredFlag">Taken from PPS</param>
    public readonly void Write(
        BitStreamWriter writer,
        EntropyCodingMode entropyCodingMode,
        bool transformSize8x8Flag,
        int mbType,
        int codedBlockPatternLuma,
        int chromaArrayType,
        GeneralSliceType sliceType,
        int startIdx,
        int endIdx,
        NalUnit nalu,
        DerivationContext dc,
        ref int luma4x4BlkIdx,
        ref int cb4x4BlkIdx,
        ref int cr4x4BlkIdx,
        int chroma4x4BlkIdx,
        IMacroblockUtility util,
        ResidualMode mode,
        ContainerMatrix4x64 Level4x4,
        ContainerMatrix4x64 Level8x8,
        Container64UInt32 I16x16DcLevel,
        ContainerMatrix16x16 I16x16AcLevel,
        bool constrainedIntraPredFlag)
    {
        int mbPartPredMode = Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType);

        if (startIdx == 0 && mbPartPredMode == Intra_16x16)
        {
            if (entropyCodingMode == EntropyCodingMode.Cabac)
            {
                Span<uint> sp = stackalloc uint[64];
                for (int i = 0; i < 64; i++)
                    sp[i] = I16x16DcLevel[i];

                StartResidualCabac!.Value.Write(writer, sp);
            }
            else
            {
                Span<uint> sp = stackalloc uint[64];
                for (int i = 0; i < 64; i++)
                    sp[i] = I16x16DcLevel[i];

                // TODO: CAVLC residual writes
                throw new InvalidOperationException("CAVLC residual writes are not supported");
                //StartResidualCavlc!.Value.Write(writer, sp, 0, 15, 16, Cav);
            }
        }

        int residualTop = 0;

        for (int i8x8 = 0; i8x8 < 4; i8x8++)
        {
            if (!transformSize8x8Flag || entropyCodingMode == EntropyCodingMode.Cabac)
            {
                for (int i4x4 = 0; i4x4 < 4; i4x4++)
                {
                    if (Int32Boolean.B(codedBlockPatternLuma & (1 << i8x8)))
                    {
                        if (mbPartPredMode == Intra_16x16)
                        {
                            if (entropyCodingMode == EntropyCodingMode.Cabac)
                            {
                                _Core(I16x16AcLevel, CabacResidualBlocks);

                                void _Core(ContainerMatrix16x16 i16x16ACLevel, Container16CabacResidual? cabacs)
                                {
                                    Span<uint> acLevel = stackalloc uint[64];

                                    for (int i = 0; i < 16; i++)
                                        acLevel[i] = i16x16ACLevel[i8x8 * 4 + i4x4, i];

                                    cabacs!.Value[residualTop++].Write(writer, acLevel);
                                }
                            }
                            else
                            {
                                _Core(I16x16AcLevel, CavlcResidualBlocks, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                                void _Core(ContainerMatrix16x16 i16x16ACLevel, Container16CavlcResidual? cavlcs, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                                {
                                    Span<uint> acLevel = stackalloc uint[16];

                                    for (int i = 0; i < 16; i++)
                                        acLevel[i] = (uint)i16x16ACLevel[i8x8 * 4 + i4x4, i];

                                    // TODO: CAVLC residual writes
                                    throw new InvalidOperationException("CAVLC residual writes are not supported");
                                    //cavlcs!.Value[residualTop++].Write(
                                    //    writer, acLevel, Math.Max(0, startIdx - 1), endIdx - 1, 15, Cav);
                                }
                            }
                        }
                        else
                        {
                            if (entropyCodingMode == EntropyCodingMode.Cabac)
                            {
                                _Core(Level4x4, CabacResidualBlocks);

                                void _Core(ContainerMatrix4x64 level4x4, Container16CabacResidual? cabacs)
                                {
                                    Span<uint> lvl4x4 = stackalloc uint[4];

                                    for (int i = 0; i < 4; i++)
                                        lvl4x4[i] = (uint)level4x4[i8x8 * 4 + i4x4, i];

                                    cabacs!.Value[residualTop++].Write(writer, lvl4x4);
                                }
                            }
                            else
                            {
                                _Core(Level4x4, CavlcResidualBlocks, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                                void _Core(ContainerMatrix4x64 level4x4, Container16CavlcResidual? cavlcs, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                                {
                                    Span<uint> lvl4x4 = stackalloc uint[4];

                                    for (int i = 0; i < 4; i++)
                                        lvl4x4[i] = (uint)level4x4[i8x8 * 4 + i4x4, i];

                                    // TODO: CAVLC residual writes
                                    throw new InvalidOperationException("CAVLC residual writes are not supported");
                                    //cavlcs!.Value[residualTop++].Write(
                                    //    writer, lvl4x4, Math.Max(0, startIdx - 1), endIdx - 1, 15, Cav);
                                }
                            }
                        }
                    }
                    else if (mbPartPredMode == Intra_16x16)
                    {
                        for (int i = 0; i < 15; i++)
                            I16x16AcLevel[i8x8 * 4 + i4x4, i] = 0;
                    }
                    else
                    {
                        for (int i = 0; i < 16; i++)
                            Level4x4[i8x8 * 4 + i4x4, i] = 0;
                    }

                    if (entropyCodingMode == EntropyCodingMode.Cabac && transformSize8x8Flag)
                    {
                        for (int i = 0; i < 16; i++)
                            Level8x8[i8x8, 4 * i + i4x4] = (uint)Level4x4[i8x8 * 4 + i4x4, i];
                    }
                }
            }
            else if (Int32Boolean.B(codedBlockPatternLuma & (1 << i8x8)))
            {
                if (entropyCodingMode == EntropyCodingMode.Cabac)
                {
                    _Core(Level8x8, CabacResidualBlocks);

                    void _Core(ContainerMatrix4x64 level8x8, Container16CabacResidual? cabacs)
                    {
                        Span<uint> coeffLevel = stackalloc uint[64];

                        for (int i = 0; i < 64; i++)
                            coeffLevel[i] = level8x8[i8x8, i];

                        cabacs!.Value[residualTop++].Write(writer, coeffLevel);
                    }
                }
                else
                {
                    _Core(Level8x8, CavlcResidualBlocks, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                    void _Core(ContainerMatrix4x64 level8x8, Container16CavlcResidual? cavlcs, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                    {
                        Span<uint> coeffLevel = stackalloc uint[64];

                        for (int i = 0; i < 64; i++)
                            coeffLevel[i] = level8x8[i8x8, i];

                        // TODO: CAVLC residual writes
                        throw new InvalidOperationException("CAVLC residual writes are not supported");
                        //cavlcs.Value![residualTop++].Write(writer, coeffLevel, 4 * startIdx, 4 * endIdx, 64, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 64; i++)
                    Level8x8[i8x8, i] = 0;
            }
        }
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified object is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is ResidualLuma luma && Equals(luma);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ResidualLuma"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="ResidualLuma"/> to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="ResidualLuma"/> is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool Equals(ResidualLuma other)
    {
        return UsesCabac == other.UsesCabac &&
               EqualityComparer<Container16CabacResidual?>.Default.Equals(CabacResidualBlocks, other.CabacResidualBlocks) &&
               EqualityComparer<Container16CavlcResidual?>.Default.Equals(CavlcResidualBlocks, other.CavlcResidualBlocks) &&
               CodedBlockPatternLuma == other.CodedBlockPatternLuma &&
               MbPartPredMode == other.MbPartPredMode &&
               EntropyCodingModeFlag == other.EntropyCodingModeFlag &&
               TransformSize8x8Flag == other.TransformSize8x8Flag &&
               StartIndex == other.StartIndex &&
               EndIndex == other.EndIndex;
    }

    /// <summary>
    /// Returns the hash code for the current instance.
    /// </summary>
    /// <returns>The hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(UsesCabac);
        hash.Add(CabacResidualBlocks);
        hash.Add(CavlcResidualBlocks);
        hash.Add(CodedBlockPatternLuma);
        hash.Add(MbPartPredMode);
        hash.Add(EntropyCodingModeFlag);
        hash.Add(TransformSize8x8Flag);
        hash.Add(StartIndex);
        hash.Add(EndIndex);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="ResidualLuma"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="ResidualLuma"/> instance to compare.</param>
    /// <param name="right">The second <see cref="ResidualLuma"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="ResidualLuma"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(ResidualLuma left, ResidualLuma right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="ResidualLuma"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="ResidualLuma"/> instance to compare.</param>
    /// <param name="right">The second <see cref="ResidualLuma"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="ResidualLuma"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(ResidualLuma left, ResidualLuma right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a residual structure used in H.264 video encoding.
/// </summary>
public struct Residual : IEquatable<Residual>
{
    /// <summary>
    /// The first luma residual handle.
    /// </summary>
    public ResidualLuma FirstLumaResidual;

    /// <summary>
    /// CABAC residual handles for chroma blue and red channels.
    /// </summary>
    public (CabacResidual First, CabacResidual Second)? CabacCbCr;

    /// <summary>
    /// CAVLC residual handles for chroma blue and red channels.
    /// </summary>
    public (CavlcResidual First, CavlcResidual Second)? CavlcCbCr;

    /// <summary>
    /// CABAC residual blocks for AC coefficients.
    /// </summary>
    public ContainerMatrix2x4x4CabacResidual? CabacAcResidualBlocks;

    /// <summary>
    /// CAVLC residual blocks for AC coefficients.
    /// </summary>
    public ContainerMatrix2x4x4CavlcResidual? CavlcAcResidualBlocks;

    /// <summary>
    /// Residual luma handle for YUV 4:4:4 chroma blue channel.
    /// </summary>
    public ResidualLuma? Yuv444Cb;

    /// <summary>
    /// Residual luma handle for YUV 4:4:4 chroma red channel.
    /// </summary>
    public ResidualLuma? Yuv444Cr;

    /// <summary>
    /// Indicates whether the chroma array type is 3.
    /// </summary>
    public bool IsChromaArrayType3;

    /// <summary>
    /// Indicates whether CABAC is preferred over CAVLC.
    /// </summary>
    public bool PreferCabac;

    /// <summary>
    ///   Container for the DC coefficients of the Intra 16x16 luma block.
    /// </summary>
    public Container64UInt32 Intra16x16DCLevel;

    /// <summary>
    ///   Container for the AC coefficients of the Intra 16x16 luma block.
    /// </summary>
    public ContainerMatrix16x16 Intra16x16ACLevel;

    /// <summary>
    ///   Container for the luma coefficients in 8x8 transform blocks.
    /// </summary>
    public ContainerMatrix4x64 Level8x8;

    /// <summary>
    ///   Container for the luma coefficients in 4x4 transform blocks.
    /// </summary>
    public ContainerMatrix4x64 Level4x4;

    /// <summary>
    ///   Container for the DC coefficients of the chroma blue (Cb) 16x16 block.
    /// </summary>
    public Container64UInt32 Cb16x16DCLevel;

    /// <summary>
    ///   Container for the AC coefficients of the chroma blue (Cb) 16x16 block.
    /// </summary>
    public ContainerMatrix16x16 Cb16x16ACLevel;

    /// <summary>
    ///   Container for the chroma blue (Cb) coefficients in 8x8 transform blocks.
    /// </summary>
    public ContainerMatrix4x64 CbLevel8x8;

    /// <summary>
    ///   Container for the chroma blue (Cb) coefficients in 4x4 transform blocks.
    /// </summary>
    public ContainerMatrix4x64 CbLevel4x4;

    /// <summary>
    ///   Container for the DC coefficients of the chroma red (Cr) 16x16 block.
    /// </summary>
    public Container64UInt32 Cr16x16DCLevel;

    /// <summary>
    ///   Container for the AC coefficients of the chroma red (Cr) 16x16 block.
    /// </summary>
    public ContainerMatrix16x16 Cr16x16ACLevel;

    /// <summary>
    ///   Container for the chroma red (Cr) coefficients in 8x8 transform blocks.
    /// </summary>
    public ContainerMatrix4x64 CrLevel8x8;

    /// <summary>
    ///   Container for the chroma red (Cr) coefficients in 4x4 transform blocks.
    /// </summary>
    public ContainerMatrix4x64 CrLevel4x4;

    /// <summary>
    /// Initializes a new instance of the <see cref="Residual"/> struct.
    /// </summary>
    /// <param name="firstLumaResidual">The first luma residual.</param>
    /// <param name="cabacCbCr">CABAC residual for chroma blue and red channels.</param>
    /// <param name="cavlcCbCr">CAVLC residual for chroma blue and red channels.</param>
    /// <param name="cabacAcResidualBlocks">CABAC residual blocks for AC coefficients.</param>
    /// <param name="cavlcAcResidualBlocks">CAVLC residual blocks for AC coefficients.</param>
    /// <param name="yuv444Cb">Residual luma for YUV 4:4:4 chroma blue channel.</param>
    /// <param name="yuv444Cr">Residual luma for YUV 4:4:4 chroma red channel.</param>
    /// <param name="isChromaArrayType3">Indicates whether the chroma array type is 3.</param>
    /// <param name="preferCabac">Indicates whether CABAC is preferred over CAVLC.</param>
    /// <param name="intra16x16DCLevel">Container for the DC coefficients of the Intra 16x16 luma block.</param>
    /// <param name="intra16x16ACLevel">Container for the AC coefficients of the Intra 16x16 luma block.</param>
    /// <param name="level8x8">Container for the luma coefficients in 8x8 transform blocks.</param>
    /// <param name="level4x4">Container for the luma coefficients in 4x4 transform blocks.</param>
    /// <param name="cb16x16DCLevel">Container for the DC coefficients of the Chroma 16x16 luma block.</param>
    /// <param name="cb16x16ACLevel">Container for the AC coefficients of the Chroma 16x16 luma block.</param>
    /// <param name="cbLevel8x8">Container for the chroma coefficients in 8x8 transform blocks.</param>
    /// <param name="cbLevel4x4">Container for the chroma coefficients in 4x4 transform blocks.</param>
    /// <param name="cr16x16DCLevel">Container for the DC coefficients of the Chroma 16x16 luma block.</param>
    /// <param name="cr16x16ACLevel">Container for the AC coefficients of the Chroma 16x16 luma block.</param>
    /// <param name="crLevel8x8">Container for the chroma coefficients in 8x8 transform blocks.</param>
    /// <param name="crLevel4x4">Container for the chroma coefficients in 4x4 transform blocks.</param>
    public Residual(
        ResidualLuma firstLumaResidual,
        (CabacResidual First, CabacResidual Second)? cabacCbCr,
        (CavlcResidual First, CavlcResidual Second)? cavlcCbCr,
        ContainerMatrix2x4x4CabacResidual? cabacAcResidualBlocks,
        ContainerMatrix2x4x4CavlcResidual? cavlcAcResidualBlocks,
        ResidualLuma? yuv444Cb,
        ResidualLuma? yuv444Cr,
        bool isChromaArrayType3,
        bool preferCabac,
        Container64UInt32 intra16x16DCLevel,
        ContainerMatrix16x16 intra16x16ACLevel,
        ContainerMatrix4x64 level8x8,
        ContainerMatrix4x64 level4x4,
        Container64UInt32 cb16x16DCLevel,
        ContainerMatrix16x16 cb16x16ACLevel,
        ContainerMatrix4x64 cbLevel8x8,
        ContainerMatrix4x64 cbLevel4x4,
        Container64UInt32 cr16x16DCLevel,
        ContainerMatrix16x16 cr16x16ACLevel,
        ContainerMatrix4x64 crLevel8x8,
        ContainerMatrix4x64 crLevel4x4)
    {
        FirstLumaResidual = firstLumaResidual;
        CabacCbCr = cabacCbCr;
        CavlcCbCr = cavlcCbCr;
        CabacAcResidualBlocks = cabacAcResidualBlocks;
        CavlcAcResidualBlocks = cavlcAcResidualBlocks;
        Yuv444Cb = yuv444Cb;
        Yuv444Cr = yuv444Cr;
        IsChromaArrayType3 = isChromaArrayType3;
        PreferCabac = preferCabac;
        Intra16x16DCLevel = intra16x16DCLevel;
        Intra16x16ACLevel = intra16x16ACLevel;
        Level8x8 = level8x8;
        Level4x4 = level4x4;
        Cb16x16DCLevel = cb16x16DCLevel;
        Cb16x16ACLevel = cb16x16ACLevel;
        CbLevel8x8 = cbLevel8x8;
        CbLevel4x4 = cbLevel4x4;
        Cr16x16DCLevel = cr16x16DCLevel;
        Cr16x16ACLevel = cr16x16ACLevel;
        CrLevel8x8 = crLevel8x8;
        CrLevel4x4 = crLevel4x4;
    }

#pragma warning disable
    public static Residual Read(
#pragma warning restore
        BitStreamReader reader,
        EntropyCodingMode codingMode,
        int chromaArrayType,
        bool transformSize8x8Flag,
        int mbType,
        int codedBlockPatternLuma,
        GeneralSliceType sliceType,
        int startIdx,
        int endIdx,
        NalUnit nalu,
        DerivationContext dc,
        ref int luma4x4BlkIdx,
        ref int cb4x4BlkIdx,
        ref int cr4x4BlkIdx,
        int chroma4x4BlkIdx,
        IMacroblockUtility util,
        ResidualMode mode,
        bool constrainedIntraPredFlag,
        int subWidthC,
        int subHeightC,
        int codedBlockPatternChroma)
    {
        ResidualLuma firstResidualLuma = ResidualLuma.Read(reader, codingMode, transformSize8x8Flag, mbType, codedBlockPatternLuma, chromaArrayType, out var i16x16DCLevel, out var i16x16ACLevel, out var level8x8, out var level4x4, sliceType, startIdx, endIdx, nalu, dc, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, util, mode, constrainedIntraPredFlag);

        Container64UInt32 Intra16x16DCLevel = i16x16DCLevel;
        ContainerMatrix16x16 Intra16x16ACLevel = i16x16ACLevel;
        ContainerMatrix4x64 LumaLevel8x8 = level8x8;
        ContainerMatrix4x64 LumaLevel4x4 = level4x4;

        Container64UInt32 Cb16x16DCLevel = default;
        ContainerMatrix16x16 Cb16x16ACLevel = default;
        ContainerMatrix4x64 CbLevel8x8 = default;
        ContainerMatrix4x64 CbLevel4x4 = default;

        Container64UInt32 Cr16x16DCLevel = default;
        ContainerMatrix16x16 Cr16x16ACLevel = default;
        ContainerMatrix4x64 CrLevel8x8 = default;
        ContainerMatrix4x64 CrLevel4x4 = default;

        ContainerMatrix16x16 Chroma16x16DCLevel = new();
        ContainerMatrix2x16x16 Chroma16x16ACLevel = new();

        (CabacResidual First, CabacResidual Second)? cabacCbCr = null;
        (CavlcResidual First, CavlcResidual Second)? cavlcCbCr = null;

        ContainerMatrix2x4x4CabacResidual cabacs = new();
        ContainerMatrix2x4x4CavlcResidual cavlcs = new();

        ResidualLuma? yuv444Cb = null;
        ResidualLuma? yuv444Cr = null;

        int cabacsTop = 0;
        int cavlcsTop = 0;

        if (chromaArrayType is 1 or 2)
        {
            int NumC8x8 = 4 / (subWidthC * subHeightC);
            for (int iCbCr = 0; iCbCr < 2; iCbCr++)
            {
                if (Int32Boolean.B(codedBlockPatternChroma & 3) && startIdx == 0)
                {
                    if (codingMode == EntropyCodingMode.Cabac)
                    {
                        cabacCbCr ??= default;

                        CabacResidual cabac = _Core(ref Chroma16x16DCLevel);

                        CabacResidual _Core(ref ContainerMatrix16x16 chromaDCLevel)
                        {
                            Span<uint> sp = stackalloc uint[16];

                            CabacResidual res = CabacResidual.Read(reader, sp, 0, 4 * NumC8x8 - 1, 4 * NumC8x8, chromaArrayType);

                            for (int i = 0; i < sp.Length; i++)
                                chromaDCLevel[iCbCr, i] = sp[i];

                            return res;
                        }

                        if (iCbCr == 0)
                        {
                            var temp = cabacCbCr.Value;
                            temp.First = cabac;
                            cabacCbCr = temp;
                        }
                        else
                        {
                            var temp = cabacCbCr.Value;
                            temp.Second = cabac;
                            cabacCbCr = temp;
                        }
                    }
                    else
                    {
                        cavlcCbCr ??= default;

                        CavlcResidual cavlc = _Core(ref Chroma16x16DCLevel, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                        CavlcResidual _Core(ref ContainerMatrix16x16 chromaDCLevel, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                        {
                            Span<uint> sp = stackalloc uint[16];

                            CavlcResidual res = CavlcResidual.Read(reader, sp, 0, 4 * NumC8x8 - 1, 4 * NumC8x8, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);

                            for (int i = 0; i < sp.Length; i++)
                                chromaDCLevel[iCbCr, i] = sp[i];

                            return res;
                        }

                        if (iCbCr == 0)
                        {
                            var temp = cavlcCbCr.Value;
                            temp.First = cavlc;
                            cavlcCbCr = temp;
                        }
                        else
                        {
                            var temp = cavlcCbCr.Value;
                            temp.Second = cavlc;
                            cavlcCbCr = temp;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4 * NumC8x8; i++)
                        Chroma16x16DCLevel[iCbCr, i] = 0;
                }
            }

            for (int iCbCr = 0; iCbCr < 2; iCbCr++)
            {
                for (int i8x8 = 0; i8x8 < NumC8x8; i8x8++)
                {
                    for (int i4x4 = 0; i4x4 < 4; i4x4++)
                    {
                        if (Int32Boolean.B(codedBlockPatternChroma & 2))
                        {
                            if (codingMode == EntropyCodingMode.Cabac)
                            {
                                CabacResidual cabac = _Core(ref Chroma16x16ACLevel);

                                cabacs[cabacsTop++] = cabac;

                                CabacResidual _Core(ref ContainerMatrix2x16x16 Chroma16x16ACLevel)
                                {
                                    Span<uint> span = stackalloc uint[16];

                                    CabacResidual cabac = CabacResidual.Read(reader, span, Math.Max(0, startIdx - 1), endIdx - 1, 15, chromaArrayType);

                                    for (int i = 0; i < 16; i++)
                                        Chroma16x16ACLevel[iCbCr, i8x8 * 4 + i4x4, i] = span[i];

                                    return cabac;
                                }
                            }
                            else
                            {
                                CavlcResidual cavlc = _Core(ref Chroma16x16ACLevel, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx);

                                cavlcs[cavlcsTop++] = cavlc;

                                CavlcResidual _Core(ref ContainerMatrix2x16x16 Chroma16x16ACLevel, ref int luma4x4BlkIdx, ref int cb4x4BlkIdx, ref int cr4x4BlkIdx)
                                {
                                    Span<uint> span = stackalloc uint[16];

                                    CavlcResidual cavlc = CavlcResidual.Read(reader, span, Math.Max(0, startIdx - 1), endIdx - 1, 15, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);

                                    for (int i = 0; i < 16; i++)
                                        Chroma16x16ACLevel[iCbCr, i8x8 * 4 + i4x4, i] = span[i];

                                    return cavlc;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 15; i++)
                                Chroma16x16ACLevel[iCbCr, i8x8 * 4 + i4x4, i] = 0;
                        }
                    }
                }
            }
        }
        else if (chromaArrayType == 3)
        {
            yuv444Cb = ResidualLuma.Read(reader, codingMode, transformSize8x8Flag, mbType, codedBlockPatternLuma, chromaArrayType, out Cb16x16DCLevel, out Cb16x16ACLevel, out CbLevel8x8, out CbLevel4x4, sliceType, startIdx, endIdx, nalu, dc, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, util, mode, constrainedIntraPredFlag);
            yuv444Cr = ResidualLuma.Read(reader, codingMode, transformSize8x8Flag, mbType, codedBlockPatternLuma, chromaArrayType, out Cr16x16DCLevel, out Cr16x16ACLevel, out CrLevel8x8, out CrLevel4x4, sliceType, startIdx, endIdx, nalu, dc, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, util, mode, constrainedIntraPredFlag);
        }

        return new Residual(
            firstResidualLuma, cabacCbCr, cavlcCbCr, cabacs, cavlcs, yuv444Cb, yuv444Cr, chromaArrayType == 3, codingMode == EntropyCodingMode.Cabac,
            Intra16x16DCLevel, Intra16x16ACLevel, level8x8, level4x4,
            Cb16x16DCLevel, Cb16x16ACLevel, CbLevel8x8, CbLevel4x4,
            Cr16x16DCLevel, Cr16x16ACLevel, CrLevel8x8, CrLevel4x4
        );
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is Residual residual && Equals(residual);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Residual"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="Residual"/> to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="Residual"/> is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool Equals(Residual other)
    {
        return FirstLumaResidual.Equals(other.FirstLumaResidual) &&
               EqualityComparer<(CabacResidual First, CabacResidual Second)?>.Default.Equals(CabacCbCr, other.CabacCbCr) &&
               EqualityComparer<(CavlcResidual First, CavlcResidual Second)?>.Default.Equals(CavlcCbCr, other.CavlcCbCr) &&
               EqualityComparer<ContainerMatrix2x4x4CabacResidual?>.Default.Equals(CabacAcResidualBlocks, other.CabacAcResidualBlocks) &&
               EqualityComparer<ContainerMatrix2x4x4CavlcResidual?>.Default.Equals(CavlcAcResidualBlocks, other.CavlcAcResidualBlocks) &&
               EqualityComparer<ResidualLuma?>.Default.Equals(Yuv444Cb, other.Yuv444Cb) &&
               EqualityComparer<ResidualLuma?>.Default.Equals(Yuv444Cr, other.Yuv444Cr) &&
               IsChromaArrayType3 == other.IsChromaArrayType3 &&
               PreferCabac == other.PreferCabac &&
               EqualityComparer<Container64UInt32>.Default.Equals(Intra16x16DCLevel, other.Intra16x16DCLevel) &&
               EqualityComparer<ContainerMatrix16x16>.Default.Equals(Intra16x16ACLevel, other.Intra16x16ACLevel) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(Level8x8, other.Level8x8) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(Level4x4, other.Level4x4) &&
               EqualityComparer<Container64UInt32>.Default.Equals(Cb16x16DCLevel, other.Cb16x16DCLevel) &&
               EqualityComparer<ContainerMatrix16x16>.Default.Equals(Cb16x16ACLevel, other.Cb16x16ACLevel) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(CbLevel8x8, other.CbLevel8x8) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(CbLevel4x4, other.CbLevel4x4) &&
               EqualityComparer<Container64UInt32>.Default.Equals(Cr16x16DCLevel, other.Cr16x16DCLevel) &&
               EqualityComparer<ContainerMatrix16x16>.Default.Equals(Cr16x16ACLevel, other.Cr16x16ACLevel) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(CrLevel8x8, other.CrLevel8x8) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(CrLevel4x4, other.CrLevel4x4);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(FirstLumaResidual);
        hash.Add(CabacCbCr);
        hash.Add(CavlcCbCr);
        hash.Add(CabacAcResidualBlocks);
        hash.Add(CavlcAcResidualBlocks);
        hash.Add(Yuv444Cb);
        hash.Add(Yuv444Cr);
        hash.Add(IsChromaArrayType3);
        hash.Add(PreferCabac);
        hash.Add(Intra16x16ACLevel);
        hash.Add(Intra16x16DCLevel);
        hash.Add(Level8x8);
        hash.Add(Level4x4);
        hash.Add(Cb16x16ACLevel);
        hash.Add(Cb16x16DCLevel);
        hash.Add(CbLevel8x8);
        hash.Add(CbLevel4x4);
        hash.Add(Cr16x16ACLevel);
        hash.Add(Cr16x16DCLevel);
        hash.Add(CrLevel8x8);
        hash.Add(CrLevel4x4);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="Residual"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Residual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Residual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="Residual"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Residual left, Residual right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Residual"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Residual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Residual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="Residual"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Residual left, Residual right)
    {
        return !(left == right);
    }
}
