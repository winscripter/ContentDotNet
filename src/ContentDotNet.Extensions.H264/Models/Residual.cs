using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264.Models;

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

        int nC = CavlcResidualHelpers.GetNC(reader, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);
        var totalCoeffAndTrailingOnes = CavlcResidualHelpers.DecodeCoeffToken(reader, nC)
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

        (byte vlc, int size) = CavlcResidualHelpers.GetVlcAndSize((int)CoeffToken, nC);
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

        (byte vlc, int size) = CavlcResidualHelpers.GetVlcAndSize((int)CoeffToken, nC);
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
