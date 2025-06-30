using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Internal.Macroblocks;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Helpers;

internal static class Util264
{
    // Rec. ITU-T H.264 (V15) (08/2024), Page 51
    public static MacroblockSizeChroma GetMbWidthHeightC(SequenceParameterSet sps)
    {
        if (sps.SeparateColourPlaneFlag || sps.ChromaFormatIdc == 0)
            return MacroblockSizeChroma.Zero;

        ChromaFormat size = ChromaFormat.GetSubsamplingAndSize(sps);
        return new MacroblockSizeChroma(16 / size.ChromaWidth, 16 / size.ChromaHeight);
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

    public static bool MoreRbspData(BitStreamReader reader, long nalLen) => reader.BaseStream.Position < nalLen;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Median(int x, int y, int z) => Math.Max(Math.Min(x, y), Math.Min(z, Math.Max(x, y)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Middle(int x, int y) => (x + y + 1) >> 1; // '>> 1' over '/ 2' for performance

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ByteAligned(BitStreamReader reader) => reader.GetState().BitPosition == 0;

    public static int PicOrderCnt(SequenceParameterSet sps, PictureParameterSet pps, SliceHeader shd, int prevPicOrderCntLsb, int prevPicOrderCntMsb, uint nalRefIdc)
    {
        int maxPicOrderCntLsb = 1 << ((int)sps.Log2MaxPicOrderCntLsbMinus4 + 4);
        int topFieldOrderCnt = 0;
        int bottomFieldOrderCnt = 0;

        int picOrderCntMsb;

        switch (sps.PicOrderCntType)
        {
            case 0u:
                {
                    if (shd.PicOrderCntLsb < prevPicOrderCntLsb &&
                       (prevPicOrderCntLsb - shd.PicOrderCntLsb) >= maxPicOrderCntLsb / 2)
                    {
                        picOrderCntMsb = prevPicOrderCntMsb + maxPicOrderCntLsb;
                    }
                    else if (shd.PicOrderCntLsb > prevPicOrderCntLsb &&
                             (shd.PicOrderCntLsb - prevPicOrderCntLsb) > maxPicOrderCntLsb / 2)
                    {
                        picOrderCntMsb = prevPicOrderCntMsb - maxPicOrderCntLsb;
                    }
                    else
                    {
                        picOrderCntMsb = prevPicOrderCntMsb;
                    }

                    topFieldOrderCnt = picOrderCntMsb + (int)shd.PicOrderCntLsb;
                    bottomFieldOrderCnt = topFieldOrderCnt + shd.DeltaPicOrderCntBottom;
                }
                break;

            case 1u:
                {
                    int absFrameNum = (int)shd.FrameNum;
                    if (nalRefIdc == 0 && absFrameNum > 0)
                        absFrameNum--;

                    int expectedPicOrderCnt = 0;
                    if (absFrameNum > 0)
                    {
                        int picOrderCntCycleCnt = (absFrameNum - 1) / (int)sps.NumRefFramesInPicOrderCntCycle;
                        int frameNumInCycle = (absFrameNum - 1) % (int)sps.NumRefFramesInPicOrderCntCycle;

                        int expectedDeltaPerCycle = 0;
                        for (int i = 0; i < sps.NumRefFramesInPicOrderCntCycle; i++)
                            expectedDeltaPerCycle += (int)sps.OffsetForRefFrame[i];

                        expectedPicOrderCnt = picOrderCntCycleCnt * expectedDeltaPerCycle;
                        for (int i = 0; i <= frameNumInCycle; i++)
                            expectedPicOrderCnt += (int)sps.OffsetForRefFrame[i];
                    }

                    if (nalRefIdc == 0)
                        expectedPicOrderCnt += sps.OffsetForNonRefPic;

                    topFieldOrderCnt = expectedPicOrderCnt + shd.DeltaPicOrderCnt.Item1;
                    bottomFieldOrderCnt = topFieldOrderCnt + sps.OffsetForTopToBottomField + shd.DeltaPicOrderCnt.Item2;
                }
                break;

            case 2u:
                {
                    int poc = 2 * (int)shd.FrameNum;
                    if (nalRefIdc == 0)
                        poc -= 1;

                    topFieldOrderCnt = poc;
                    bottomFieldOrderCnt = poc;
                }
                break;

            default:
                break;
        }

        return Math.Min(topFieldOrderCnt, bottomFieldOrderCnt);
    }

    public static int? PrevMbAddress(int mbAddr) => mbAddr == 0 ? null : (mbAddr - 1);

    public static int ConvertToMapped(int value)
    {
        int val = (int)((value + 1) >> 1);
        return (value & 1) == 0 ? -val : val;
    }
}
