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
public struct CavlcResidual
{
    public uint CoeffToken;
    public Container16Boolean TrailingOnesSignFlag;
    public Container16UInt32 LevelPrefix;
    public Container16UInt32 LevelSuffix;
    public uint TotalZeros;
    public Container16UInt32 RunBefore;
    public Container16UInt32 LevelVal;
    public Container16UInt32 RunVal;
    public int TotalCoeff;
    public int TrailingOnes;

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

        int TotalCoeff = 0;
        int TrailingOnes = 0;

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
}
