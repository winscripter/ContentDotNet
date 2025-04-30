using ContentDotNet.Extensions.H264.Models;
using System.Runtime.CompilerServices;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Helpers;

internal static class ResidualHelpers
{
    public static bool IsIntra(int mbType, bool transformSize8x8Flag, GeneralSliceType sliceType)
    {
        return Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) is
            Intra_4x4 or Intra_8x8 or Intra_16x16;
    }

    public static bool IsIntra16x16(int mbType, bool transformSize8x8Flag, GeneralSliceType sliceType)
    {
        return Util264.MbPartPredMode(mbType, 0, transformSize8x8Flag, sliceType) is Intra_16x16;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsChroma(int blockIndex)
    {
        return blockIndex >= 16;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetChromaBlocksPerComponent(int chromaArrayType)
    {
        return chromaArrayType switch
        {
            1 => 4,  // 4:2:0
            2 => 8,  // 4:2:2
            3 => 16, // 4:4:4
            _ => 0   // Monochrome or undefined
        };
    }


    public static ChromaComponent GetChromaComponent(int blockIndex, bool isChroma, int chromaBlocksPerComponent)
    {
        if (!isChroma)
            return ChromaComponent.None;

        int chromaBlockIndex = blockIndex - 16;

        return chromaBlockIndex < chromaBlocksPerComponent
            ? ChromaComponent.Cb
            : ChromaComponent.Cr;
    }

    public static bool IsDC(int blockIndex, int chromaBlocksPerComponent, bool isIntra16x16, bool isChroma)
    {
        if (isIntra16x16 && !isChroma)
        {
            return blockIndex == 0;
        }

        if (isChroma)
        {
            int chromaBlockIndex = blockIndex - 16;
            return chromaBlockIndex % chromaBlocksPerComponent == 0;
        }

        return false;
    }

    public static ResidualMode GetResidualMode(
        bool isIntra16x16,
        bool isDC,
        bool isChroma,
        ChromaComponent chromaComponent)
    {
        if (isChroma)
        {
            if (isDC)
            {
                return chromaComponent switch
                {
                    ChromaComponent.Cb => ResidualMode.CbIntra16x16DcLevel,
                    ChromaComponent.Cr => ResidualMode.CrIntra16x16DcLevel,
                    _ => ResidualMode.ChromaDcLevel
                };
            }
            else
            {
                return chromaComponent switch
                {
                    ChromaComponent.Cb when isIntra16x16 => ResidualMode.CbIntra16x16AcLevel,
                    ChromaComponent.Cr when isIntra16x16 => ResidualMode.CrIntra16x16AcLevel,
                    ChromaComponent.Cb => ResidualMode.CbLevel4x4,
                    ChromaComponent.Cr => ResidualMode.CrLevel4x4,
                    _ => throw new ArgumentException("Chroma component must be specified for chroma blocks.")
                };
            }
        }

        if (isIntra16x16)
        {
            return isDC ? ResidualMode.Intra16x16DcLevel : ResidualMode.Intra16x16AcLevel;
        }

        return ResidualMode.LumaLevel4x4;
    }
}
