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

    // Rec. ITU-T H.264 (V15) (08/2024), Page 56
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMacroblockAddressAvailable(int mbAddr, int currMbAddr) =>
        !(mbAddr < 0 || mbAddr > currMbAddr);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int InverseRasterScan(int a, int b, int c, int d, int e)
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
