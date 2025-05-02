using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Helpers;

internal static class Util264
{
    // Rec. ITU-T H.264 (V15) (08/2024), Page 51
    public static MacroblockSizeChroma GetMbWidthHeightC(SequenceParameterSet sps)
    {
        if (sps.SeparateColourPlaneFlag || sps.ChromaFormatIdc == 0)
            return MacroblockSizeChroma.Zero;

        ChromaSubsamplingAndSize size = ChromaSubsamplingLookup.GetSubsamplingAndSize(sps);
        return new MacroblockSizeChroma(16 / size.SubWidthC, 16 / size.SubHeightC);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 52
    public static void InverseMacroblockScan(int mbAddr, bool isFrame, bool isField, bool mbaffFrameFlag, int pictureWidthInSamplesL, ref int x, ref int y, ref int xO, ref int yO)
    {
        if (isFrame)
        {
            x = xO;
            y = yO + mbAddr % 2 * 16;
        }
        else if (isField)
        {
            x = xO;
            y = yO + mbAddr % 2;
        }
        else if (mbaffFrameFlag)
        {
            xO = InverseRasterScan(mbAddr / 2, 16, 32, pictureWidthInSamplesL, 0);
            yO = InverseRasterScan(mbAddr / 2, 16, 32, pictureWidthInSamplesL, 1);
        }
        else
        {
            x = InverseRasterScan(mbAddr, 16, 16, pictureWidthInSamplesL, 0);
            y = InverseRasterScan(mbAddr, 16, 16, pictureWidthInSamplesL, 1);
        }
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 52
    public static void InverseMacroblockPartitionScan(int mbPartIdx, int mbType, GeneralSliceType sliceType, ref int x, ref int y)
    {
        x = InverseRasterScan(mbPartIdx, MbPartWidth(mbType, sliceType), MbPartHeight(mbType, sliceType), 16, 0);
        y = InverseRasterScan(mbPartIdx, MbPartWidth(mbType, sliceType), MbPartHeight(mbType, sliceType), 16, 1);
    }

    public static int MbPartPredMode(int mbType, int a, bool transformSize8x8Flag, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => ISliceFunctions.MbPartPredMode(mbType, transformSize8x8Flag),
            GeneralSliceType.P => PSliceFunctions.MbPartPredMode(mbType, a),
            GeneralSliceType.B => BSliceFunctions.MbPartPredMode(mbType, a),
            GeneralSliceType.SI => ISliceFunctions.MbPartPredMode(mbType, transformSize8x8Flag),
            GeneralSliceType.SP => PSliceFunctions.MbPartPredMode(mbType, a),
            _ => 0
        };

    public static int MbPartWidth(int mbType, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => SliceTypes.na,
            GeneralSliceType.P => PSliceFunctions.MbPartWidth(mbType),
            GeneralSliceType.B => BSliceFunctions.MbPartWidth(mbType),
            GeneralSliceType.SI => SliceTypes.na,
            GeneralSliceType.SP => PSliceFunctions.MbPartWidth(mbType),
            _ => SliceTypes.na
        };

    public static int SubMbPartWidth(int mbType, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => SliceTypes.na,
            GeneralSliceType.P => PSliceFunctions.SubMbPartWidth(mbType),
            GeneralSliceType.B => BSliceFunctions.SubMbPartWidth(mbType),
            GeneralSliceType.SI => SliceTypes.na,
            GeneralSliceType.SP => PSliceFunctions.SubMbPartWidth(mbType),
            _ => SliceTypes.na
        };

    public static int SubMbPartHeight(int mbType, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => SliceTypes.na,
            GeneralSliceType.P => PSliceFunctions.SubMbPartHeight(mbType),
            GeneralSliceType.B => BSliceFunctions.SubMbPartHeight(mbType),
            GeneralSliceType.SI => SliceTypes.na,
            GeneralSliceType.SP => PSliceFunctions.SubMbPartHeight(mbType),
            _ => SliceTypes.na
        };

    public static int NumSubMbPart(int mbType, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => SliceTypes.na,
            GeneralSliceType.P => PSliceFunctions.NumSubMbPart(mbType),
            GeneralSliceType.B => BSliceFunctions.NumSubMbPart(mbType),
            GeneralSliceType.SI => SliceTypes.na,
            GeneralSliceType.SP => PSliceFunctions.NumSubMbPart(mbType),
            _ => SliceTypes.na
        };

    public static int SubMbPredMode(int mbType, GeneralSliceType sliceType)
       => sliceType switch
       {
           GeneralSliceType.I => SliceTypes.na,
           GeneralSliceType.P => PSliceFunctions.SubMbPredMode(mbType),
           GeneralSliceType.B => BSliceFunctions.SubMbPredMode(mbType),
           GeneralSliceType.SI => SliceTypes.na,
           GeneralSliceType.SP => PSliceFunctions.SubMbPredMode(mbType),
           _ => SliceTypes.na
       };

    public static int MbPartHeight(int mbType, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => SliceTypes.na,
            GeneralSliceType.P => PSliceFunctions.MbPartHeight(mbType),
            GeneralSliceType.B => BSliceFunctions.MbPartHeight(mbType),
            GeneralSliceType.SI => SliceTypes.na,
            GeneralSliceType.SP => PSliceFunctions.MbPartHeight(mbType),
            _ => SliceTypes.na
        };

    public static int NumMbPart(int mbType, GeneralSliceType sliceType)
        => sliceType switch
        {
            GeneralSliceType.I => SliceTypes.na,
            GeneralSliceType.P => PSliceFunctions.NumMbPart(mbType),
            GeneralSliceType.B => BSliceFunctions.NumMbPart(mbType),
            GeneralSliceType.SI => SliceTypes.na,
            GeneralSliceType.SP => PSliceFunctions.NumMbPart(mbType),
            _ => SliceTypes.na
        };

    // Rec. ITU-T H.264 (V15) (08/2024), Page 52
    public static void InverseSubMacroblockPartitionScan(int subMbPartIdx, MacroblockTypeHistory subMbType, int mbPartIdx, int mbType, GeneralSliceType sliceType, ref int x, ref int y)
    {
        if (mbType is SliceTypes.P_8x8 or SliceTypes.P_8x8ref0 or SliceTypes.B_8x8)
        {
            x = InverseRasterScan(subMbPartIdx, SubMbPartWidth(subMbType[mbPartIdx], sliceType), SubMbPartHeight(subMbType[mbPartIdx], sliceType), 8, 0);
            y = InverseRasterScan(subMbPartIdx, SubMbPartWidth(subMbType[mbPartIdx], sliceType), SubMbPartHeight(subMbType[mbPartIdx], sliceType), 8, 1);
        }
        else
        {
            x = InverseRasterScan(subMbPartIdx, 4, 4, 8, 0);
            y = InverseRasterScan(subMbPartIdx, 4, 4, 8, 1);
        }
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 55
    public static void Inverse4x4LumaScan(int luma4x4BlkIdx, ref int x, ref int y)
    {
        x = InverseRasterScan(luma4x4BlkIdx / 4, 8, 8, 16, 0) + InverseRasterScan(luma4x4BlkIdx % 4, 4, 4, 8, 0);
        y = InverseRasterScan(luma4x4BlkIdx / 4, 8, 8, 16, 1) + InverseRasterScan(luma4x4BlkIdx % 4, 4, 4, 8, 1);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 55
    public static void Inverse4x4CbCrScan(int cbcr4x4BlkIdx, ref int x, ref int y)
    {
        x = InverseRasterScan(cbcr4x4BlkIdx / 4, 8, 8, 16, 0) + InverseRasterScan(cbcr4x4BlkIdx % 4, 4, 4, 8, 0);
        y = InverseRasterScan(cbcr4x4BlkIdx / 4, 8, 8, 16, 1) + InverseRasterScan(cbcr4x4BlkIdx % 4, 4, 4, 8, 1);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 55
    public static void Inverse8x8LumaScan(int luma8x8BlkIdx, ref int x, ref int y)
    {
        x = InverseRasterScan(luma8x8BlkIdx, 8, 8, 16, 0);
        y = InverseRasterScan(luma8x8BlkIdx, 8, 8, 16, 1);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 55
    public static void Inverse8x8CbCrScan(int cbcr8x8BlkIdx, ref int x, ref int y)
    {
        x = InverseRasterScan(cbcr8x8BlkIdx, 8, 8, 16, 0);
        y = InverseRasterScan(cbcr8x8BlkIdx, 8, 8, 16, 1);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 56
    public static void Inverse4x4ChromaScan(int chroma4x4BlkIdx, out int x, out int y)
    {
        x = InverseRasterScan(chroma4x4BlkIdx, 4, 4, 8, 0);
        y = InverseRasterScan(chroma4x4BlkIdx, 4, 4, 8, 1);
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 56
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMacroblockAddressAvailable(int mbAddr, int currMbAddr) =>
        !(mbAddr < 0 || mbAddr > currMbAddr);

    // Rec. ITU-T H.264 (V15) (08/2024), Page 56
    // NOTE: Can only be invoked if !MbaffFrameFlag
    public static void DeriveNeighboringMacroblockAddresses(int currMbAddr, int picWidthInMbs, out NeighboringMacroblocks macroblocks)
    {
        // "picture width in megabytes" hehehe
        // I'm sorry...

        macroblocks = new NeighboringMacroblocks();

        bool availableA = IsMacroblockAddressAvailable(macroblocks.MbAddrA = currMbAddr - 1, currMbAddr);
        bool availableB = IsMacroblockAddressAvailable(macroblocks.MbAddrB = currMbAddr - picWidthInMbs, currMbAddr);
        bool availableC = IsMacroblockAddressAvailable(macroblocks.MbAddrC = currMbAddr - picWidthInMbs + 1, currMbAddr);
        bool availableD = IsMacroblockAddressAvailable(macroblocks.MbAddrD = currMbAddr - picWidthInMbs - 1, currMbAddr);

        macroblocks.IsMbAddrAAvailable = availableA || currMbAddr % picWidthInMbs == 0;
        macroblocks.IsMbAddrBAvailable = availableB;
        macroblocks.IsMbAddrCAvailable = availableC || (currMbAddr + 1) % picWidthInMbs == 0;
        macroblocks.IsMbAddrDAvailable = availableD || currMbAddr % picWidthInMbs == 0;
    }

    // Rec. ITU-T H.264 (V15) (08/2024), Page 57
    // NOTE: Can only be invoked if MbaffFrameFlag
    public static void DeriveNeighboringMacroblockAddressesMbaff(int currMbAddr, int picWidthInMbs, out NeighboringMacroblocks macroblocks)
    {
        macroblocks = new NeighboringMacroblocks();

        bool availableA = IsMacroblockAddressAvailable(macroblocks.MbAddrA = 2 * (currMbAddr / 2 - 1), currMbAddr);
        bool availableB = IsMacroblockAddressAvailable(macroblocks.MbAddrB = 2 * (currMbAddr / 2 - picWidthInMbs), currMbAddr);
        bool availableC = IsMacroblockAddressAvailable(macroblocks.MbAddrC = 2 * (currMbAddr / 2 - picWidthInMbs + 1), currMbAddr);
        bool availableD = IsMacroblockAddressAvailable(macroblocks.MbAddrD = 2 * (currMbAddr / 2 - picWidthInMbs - 1), currMbAddr);

        macroblocks.IsMbAddrAAvailable = availableA || currMbAddr / 2 % picWidthInMbs == 0;
        macroblocks.IsMbAddrBAvailable = availableB;
        macroblocks.IsMbAddrCAvailable = availableC || (currMbAddr / 2 + 1) % picWidthInMbs == 0;
        macroblocks.IsMbAddrDAvailable = availableD || currMbAddr / 2 % picWidthInMbs == 0;
    }

    public static void DeriveNeighboringMacroblockAddresses(int currMbAddr, int picWidthInMbs, bool isMbaff, out NeighboringMacroblocks macroblocks)
    {
        if (isMbaff) DeriveNeighboringMacroblockAddresses(currMbAddr, picWidthInMbs, out macroblocks);
        else DeriveNeighboringMacroblockAddressesMbaff(currMbAddr, picWidthInMbs, out macroblocks);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InverseRasterScan(int a, int b, int c, int d, int e)
        => e == 0 ? a % (d / b) * b : a / (d / b) * c;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RefinedSliceGroupMapType GetRefinedSliceGroupMapType(int sliceGroupMapType, bool sliceGroupChangeDirectionFlag)
    {
        return sliceGroupMapType == 3 ? sliceGroupChangeDirectionFlag ? RefinedSliceGroupMapType.BoxOutClockwise : RefinedSliceGroupMapType.BoxOutCounterclockwise
             : sliceGroupMapType == 4 ? sliceGroupChangeDirectionFlag ? RefinedSliceGroupMapType.RasterScan : RefinedSliceGroupMapType.ReverseRasterScan
             : sliceGroupMapType == 5 ? sliceGroupChangeDirectionFlag ? RefinedSliceGroupMapType.WipeRight : RefinedSliceGroupMapType.WipeLeft
             : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clip3(int a, int b, int c) => c < a ? a : c > b ? b : c;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clip1Y(int e, int bitDepthY) => Clip3(0, 1 << bitDepthY, e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clip1C(int e, int bitDepthC) => Clip3(0, 1 << bitDepthC, e);

    public static bool MoreRbspData(BitStreamReader reader) => reader.GetState().CurrentByte < reader.Length - 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Median(int x, int y, int z) => Math.Max(Math.Min(x, y), Math.Min(z, Math.Max(x, y)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ByteAligned(BitStreamReader reader) => reader.GetState().BitPosition == 0;
}
