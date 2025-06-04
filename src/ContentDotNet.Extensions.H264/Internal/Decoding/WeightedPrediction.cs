using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Pictures;
using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal static class WeightedPrediction
{
    public static void Apply(
        int refIdxL0,
        int refIdxL1,
        bool fieldPicFlag,
        bool currentMbIsField,
        ReferencePicture currPic,
        Dpb refPicListL0,
        Dpb refPicListL1,
        bool predFlagL0,
        bool predFlagL1,
        int chromaArrayType,
        uint sliceTypeNum,
        bool weightedPredFlag,
        uint weightedBiPredIdc,
        IntraInterDecoder.Inter inter,
        PictureStructure pictureStructureOfCurrentSlice,
        PredWeightTable predWeightTable,
        bool mbaffFrameFlag,
        int bitDepthY,
        out int logWDc,
        out Vector256<int> w,
        out Vector256<int> o)
    {
        bool implicitModeFlag = false;
        bool explicitModeFlag = false;

        if (weightedBiPredIdc == 2 && (sliceTypeNum % 5) == 1 && predFlagL0 && predFlagL1)
        {
            implicitModeFlag = true;
            explicitModeFlag = false;
        }
        else if (weightedBiPredIdc == 1 && (sliceTypeNum % 5) == 1 && Int32Boolean.I32(predFlagL0) + Int32Boolean.I32(predFlagL1) is 1 or 2)
        {
            implicitModeFlag = false;
            explicitModeFlag = true;
        }
        else if (weightedPredFlag && (sliceTypeNum % 5) is 0 or 3 && predFlagL0)
        {
            implicitModeFlag = false;
            explicitModeFlag = true;
        }
        // Otherwise both are zero like they already are.

        logWDc = 0;
        o = Vector256<int>.Zero;
        w = Vector256<int>.Zero;

        if (implicitModeFlag)
        {
            logWDc = 5;

            // PERF: Don't set o0c and o1c, they're already zero

            ReferencePicture? currPicOrField = null;
            ReferencePicture? pic0 = null;
            ReferencePicture? pic1 = null;

            if (!fieldPicFlag && currentMbIsField)
            {
                currPicOrField = currPic;
                var pic0Derived = refPicListL0[refIdxL0 / 2] ?? throw new VideoCodecDecoderException("Unable to retrieve reference pictures");
                if (refIdxL0 % 2 == 0)
                {
                    if (!ReferencePicture.HasMatchingParity(pic0Derived, pictureStructureOfCurrentSlice))
                        throw new VideoCodecDecoderException("pic must have same parity as the current slice");
                }
                else
                {
                    if (ReferencePicture.HasMatchingParity(pic0Derived, pictureStructureOfCurrentSlice))
                        throw new VideoCodecDecoderException("pic must have opposite parity of the current slice");
                }

                pic0 = pic0Derived;

                var pic1Derived = refPicListL1[refIdxL0 / 2] ?? throw new VideoCodecDecoderException("Unable to retrieve reference pictures");
                if (refIdxL1 % 2 == 0)
                {
                    if (!ReferencePicture.HasMatchingParity(pic1Derived, pictureStructureOfCurrentSlice))
                        throw new VideoCodecDecoderException("pic must have same parity as the current slice");
                }
                else
                {
                    if (ReferencePicture.HasMatchingParity(pic1Derived, pictureStructureOfCurrentSlice))
                        throw new VideoCodecDecoderException("pic must have opposite parity of the current slice");
                }

                pic1 = pic1Derived;
            }
            else
            {
                currPicOrField = currPic;
                pic0 = refPicListL0[refIdxL0] ?? throw new VideoCodecDecoderException("Unable to retrieve reference pictures");
                pic1 = refPicListL1[refIdxL1] ?? throw new VideoCodecDecoderException("Unable to retrieve reference pictures");
            }

            int tb = Util264.Clip3(-128, 127, inter.DiffPicOrderCnt(currPicOrField, pic0));
            int td = Util264.Clip3(-128, 127, inter.DiffPicOrderCnt(pic1, pic0));

            int tx = (16384 + Math.Abs(td / 2)) / td;
            int distScaleFactor = Util264.Clip3(-1024, 1023, (tb * tx + 32) >> 6);

            if (inter.DiffPicOrderCnt(pic1, pic0) == 0 ||
                (pic1.ReferenceType == PictureReferenceType.LongTerm || pic0.ReferenceType == PictureReferenceType.LongTerm) ||
                (distScaleFactor >> 2) < -64 ||
                (distScaleFactor >> 2) > 128)
            {
                SetW(ref w, 0, 32);
                SetW(ref w, 1, 32);
            }
            else
            {
                SetW(ref w, 0, 64 - (distScaleFactor >> 2));
                SetW(ref w, 1, distScaleFactor >> 2);
            }
        }
        else if (explicitModeFlag)
        {
            int refIdxL0WP = (mbaffFrameFlag && currentMbIsField) ? refIdxL0 >> 1 : refIdxL0;
            int refIdxL1WP = (mbaffFrameFlag && currentMbIsField) ? refIdxL1 >> 1 : refIdxL1;

            if (chromaArrayType == 0)
            {
                logWDc = (int)predWeightTable.LumaLog2WeightDenom;
                SetW(ref w, 0, predWeightTable.LumaWeightL0[refIdxL0WP]);
                SetW(ref w, 1, predWeightTable.LumaWeightL1[refIdxL1WP]);
                SetO(ref o, 0, predWeightTable.LumaOffsetL0[refIdxL0WP] * (1 << (bitDepthY - 8)));
                SetO(ref o, 1, predWeightTable.LumaOffsetL1[refIdxL1WP] * (1 << (bitDepthY - 8)));
            }
            else
            {
                logWDc = (int)predWeightTable.ChromaLog2WeightDenom;
                for (int iCbCr = 0; iCbCr < 2; iCbCr++)
                {
                    SetW(ref w, 0, predWeightTable.ChromaWeightL0[iCbCr, refIdxL0WP]);
                    SetW(ref w, 1, predWeightTable.ChromaWeightL1[iCbCr, refIdxL1WP]);
                    SetO(ref o, 0, predWeightTable.ChromaWeightL1[iCbCr, refIdxL0WP] * (1 << (bitDepthY - 8)));
                    SetO(ref o, 1, predWeightTable.ChromaWeightL1[iCbCr, refIdxL1WP] * (1 << (bitDepthY - 8)));
                }
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetO(ref Vector256<int> o, int index, int value)
    {
        o = o.WithElement(index, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetW(ref Vector256<int> w, int index, int value)
    {
        w = w.WithElement(index, value);
    }
}
