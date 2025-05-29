using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using System.Threading;

namespace ContentDotNet.Extensions.H264.PredictionMode;

/// <summary>
///   This class contains the flow for H.264 intra prediction decision-making.
/// </summary>
public static class H264IntraDecisionFlow
{
    private const int FallbackIntra8x8Mode = 2; // DC
    private const int FallbackIntra4x4Mode = 2; // DC

    /// <summary>
    ///   Returns a boolean indicating whether the 8x8/4x4 prediction mode should be inherited from a previous macroblock.
    ///   If the result is <see langword="false"/>, the prediction mode is taken from either <c>rem_intra8x8_pred_mode</c>
    ///   or <c>rem_intra4x4_pred_mode</c> according to the <c>MbPartPredMode(mb_type, 0)</c> function.
    /// </summary>
    /// <param name="mbLayer">Macroblock layer</param>
    /// <param name="sliceType">Type of the slice</param>
    /// <param name="subMacroblockIndex">Index of the sub-macroblock (8x8/4x4)</param>
    /// <returns>A boolean indicating whether the prediction mode should be inherited.</returns>
    public static bool ShouldInheritPredictionMode8x8Or4x4(MacroblockLayer mbLayer, GeneralSliceType sliceType, int subMacroblockIndex)
    {
        int mbPartPredMode = Util264.MbPartPredMode((int)mbLayer.MbType, 0, mbLayer.TransformSize8x8Flag, sliceType);
        if (mbPartPredMode is not SliceTypes.Intra_8x8 and not SliceTypes.Intra_4x4)
            return false;
        var mbPred = mbLayer.Prediction;
        if (mbPred is null)
            return false;
        return mbPartPredMode == SliceTypes.Intra_4x4 ? mbPred.Value.PrevIntra4x4PredModeFlag[subMacroblockIndex] : mbPred.Value.PrevIntra8x8PredModeFlag[subMacroblockIndex];
    }

    /// <summary>
    ///   Returns the 8x8 macroblock type for the given macroblock layer, slice type, and other parameters.
    /// </summary>
    /// <param name="mbLayer"></param>
    /// <param name="sliceType"></param>
    /// <param name="picWidthInMbs"></param>
    /// <param name="totalMbsInFrame"></param>
    /// <param name="currMbAddr"></param>
    /// <param name="luma8x8BlkIdx"></param>
    /// <param name="util"></param>
    /// <returns>8x8 macroblock type</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int Get8x8MacroblockType(MacroblockLayer mbLayer, GeneralSliceType sliceType, int picWidthInMbs, int totalMbsInFrame, int currMbAddr, int luma8x8BlkIdx, IMacroblockUtility util)
    {
        if (Util264.MbPartPredMode((int)mbLayer.MbType, 0, mbLayer.TransformSize8x8Flag, sliceType) != SliceTypes.Intra_8x8)
            throw new InvalidOperationException("The macroblock type is not Intra_8x8");

        var pred = mbLayer.Prediction ?? throw new InvalidOperationException("Prediction data is not available");
        MacroblockLayer mb = mbLayer;

        return Derive(mb, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);

        static int Derive(MacroblockLayer mb, int luma8x8BlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred)
        {
            var leftMB = util.GetMacroblockToTheLeft(currMbAddr);
            var topMB = util.GetMacroblockToTheTop(currMbAddr);
            if (leftMB is not null && topMB is not null)
            {
                var leftPred = leftMB.Value.Prediction;
                var topPred = topMB.Value.Prediction;
                if (leftPred is not null && topPred is not null)
                {
                    return Math.Min(
                        Get8x8(false, leftMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred),
                        Get8x8(true, topMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred)
                    );
                }
                else if (leftPred is not null)
                {
                    return Get8x8(false, leftMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
                }
                else if (topPred is not null)
                {
                    return Get8x8(true, topMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
                }
                else
                {
                    return FallbackIntra8x8Mode;
                }
            }
            else if (leftMB is not null)
            {
                var lpred = leftMB.Value.Prediction;
                if (lpred is null)
                    return FallbackIntra8x8Mode;
                else
                    return Get8x8(false, leftMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
            }
            else if (topMB is not null)
            {
                var tpred = topMB.Value.Prediction;
                if (tpred is null)
                    return FallbackIntra8x8Mode;
                else
                    return Get8x8(true, topMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
            }
            else
            {
                return FallbackIntra8x8Mode;
            }
        }

        static int Get8x8(bool top, MacroblockLayer mb, int lumaBlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred)
        {
            return pred.PrevIntra8x8PredModeFlag[lumaBlkIdx]
                   ? Derive(
                       mb, lumaBlkIdx, util, top ? new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Up().Value : new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Left().Value, picWidthInMbs, totalMbsInFrame, pred
                   )
                   : (int)pred.RemIntra8x8PredMode[lumaBlkIdx];
        }
    }

    /// <summary>
    ///   Returns the 4x4 macroblock type for the given macroblock layer, slice type, and other parameters.
    /// </summary>
    /// <param name="mbLayer"></param>
    /// <param name="sliceType"></param>
    /// <param name="picWidthInMbs"></param>
    /// <param name="totalMbsInFrame"></param>
    /// <param name="currMbAddr"></param>
    /// <param name="luma4x4BlkIdx"></param>
    /// <param name="util"></param>
    /// <returns>4x4 macroblock type</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int Get4x4MacroblockType(MacroblockLayer mbLayer, GeneralSliceType sliceType, int picWidthInMbs, int totalMbsInFrame, int currMbAddr, int luma4x4BlkIdx, IMacroblockUtility util)
    {
        if (Util264.MbPartPredMode((int)mbLayer.MbType, 0, mbLayer.TransformSize8x8Flag, sliceType) != SliceTypes.Intra_4x4)
            throw new InvalidOperationException("The macroblock type is not Intra_4x4");

        var pred = mbLayer.Prediction ?? throw new InvalidOperationException("Prediction data is not available");
        MacroblockLayer mb = mbLayer;

        return Derive(mb, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);

        static int Derive(MacroblockLayer mb, int luma4x4BlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred)
        {
            var leftMB = util.GetMacroblockToTheLeft(currMbAddr);
            var topMB = util.GetMacroblockToTheTop(currMbAddr);
            if (leftMB is not null && topMB is not null)
            {
                var leftPred = leftMB.Value.Prediction;
                var topPred = topMB.Value.Prediction;
                if (leftPred is not null && topPred is not null)
                {
                    return Math.Min(
                        Get4x4(false, leftMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred),
                        Get4x4(true, topMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred)
                    );
                }
                else if (leftPred is not null)
                {
                    return Get4x4(false, leftMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
                }
                else if (topPred is not null)
                {
                    return Get4x4(true, topMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
                }
                else
                {
                    return FallbackIntra4x4Mode;
                }
            }
            else if (leftMB is not null)
            {
                var lpred = leftMB.Value.Prediction;
                if (lpred is null)
                    return FallbackIntra4x4Mode;
                else
                    return Get4x4(false, leftMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
            }
            else if (topMB is not null)
            {
                var tpred = topMB.Value.Prediction;
                if (tpred is null)
                    return FallbackIntra4x4Mode;
                else
                    return Get4x4(true, topMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred);
            }
            else
            {
                return FallbackIntra4x4Mode;
            }
        }

        static int Get4x4(bool top, MacroblockLayer mb, int lumaBlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred)
        {
            return pred.PrevIntra4x4PredModeFlag[lumaBlkIdx]
                   ? Derive(
                       mb, lumaBlkIdx, util, top ? new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Up().Value : new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Left().Value, picWidthInMbs, totalMbsInFrame, pred
                   )
                   : (int)pred.RemIntra4x4PredMode[lumaBlkIdx];
        }
    }

    /// <summary>
    ///   Returns the 8x8 macroblock type for the given macroblock layer, slice type, and other parameters.
    /// </summary>
    /// <param name="mbLayer"></param>
    /// <param name="sliceType"></param>
    /// <param name="picWidthInMbs"></param>
    /// <param name="totalMbsInFrame"></param>
    /// <param name="currMbAddr"></param>
    /// <param name="luma8x8BlkIdx"></param>
    /// <param name="util"></param>
    /// <param name="cache"></param>
    /// <returns>4x4 macroblock type</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int Get8x8MacroblockType(MacroblockLayer mbLayer, GeneralSliceType sliceType, int picWidthInMbs, int totalMbsInFrame, int currMbAddr, int luma8x8BlkIdx, IMacroblockUtility util, IntraPredictionModeCache cache)
    {
        if (Util264.MbPartPredMode((int)mbLayer.MbType, 0, mbLayer.TransformSize8x8Flag, sliceType) != SliceTypes.Intra_8x8)
            throw new InvalidOperationException("The macroblock type is not Intra_8x8");

        var pred = mbLayer.Prediction ?? throw new InvalidOperationException("Prediction data is not available");
        MacroblockLayer mb = mbLayer;

        return Derive(mb, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);

        static int Derive(MacroblockLayer mb, int luma8x8BlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred, IntraPredictionModeCache cache)
        {
            var topMB = util.GetMacroblockToTheTop(currMbAddr);
            var leftMB = util.GetMacroblockToTheLeft(currMbAddr);

            if (topMB is not null && leftMB is not null)
            {
                var leftPred = leftMB.Value.Prediction;
                var topPred = topMB.Value.Prediction;

                if (leftPred is not null && topPred is not null)
                {
                    return Math.Min(
                        Get8x8(false, leftMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache),
                        Get8x8(true, topMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache)
                    );
                }
                else if (leftPred is not null)
                {
                    return Get8x8(false, leftMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
                }
                else if (topPred is not null)
                {
                    return Get8x8(true, topMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
                }
                else
                {
                    return FallbackIntra8x8Mode;
                }
            }
            else if (leftMB is not null)
            {
                var lpred = leftMB.Value.Prediction;
                if (lpred is null)
                    return FallbackIntra8x8Mode;
                else
                    return Get8x8(false, leftMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
            }
            else if (topMB is not null)
            {
                var tpred = topMB.Value.Prediction;
                if (tpred is null)
                    return FallbackIntra8x8Mode;
                else
                    return Get8x8(true, topMB.Value, luma8x8BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
            }
            else
            {
                return FallbackIntra8x8Mode;
            }
        }

        static int Get8x8(bool top, MacroblockLayer mb, int lumaBlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred, IntraPredictionModeCache cache)
        {
            int address = top ? new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Up().Value : new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Left().Value;
            return pred.PrevIntra8x8PredModeFlag[lumaBlkIdx]
                   ? cache.Contains8x8(address) ? cache.Get8x8(address) : Derive(
                       mb, lumaBlkIdx, util, address, picWidthInMbs, totalMbsInFrame, pred, cache
                   )
                   : (int)pred.RemIntra8x8PredMode[lumaBlkIdx];
        }
    }

    /// <summary>
    ///   Returns the 4x4 macroblock type for the given macroblock layer, slice type, and other parameters.
    /// </summary>
    /// <param name="mbLayer"></param>
    /// <param name="sliceType"></param>
    /// <param name="picWidthInMbs"></param>
    /// <param name="totalMbsInFrame"></param>
    /// <param name="currMbAddr"></param>
    /// <param name="luma4x4BlkIdx"></param>
    /// <param name="util"></param>
    /// <param name="cache"></param>
    /// <returns>4x4 macroblock type</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int Get4x4MacroblockType(MacroblockLayer mbLayer, GeneralSliceType sliceType, int picWidthInMbs, int totalMbsInFrame, int currMbAddr, int luma4x4BlkIdx, IMacroblockUtility util, IntraPredictionModeCache cache)
    {
        if (Util264.MbPartPredMode((int)mbLayer.MbType, 0, mbLayer.TransformSize8x8Flag, sliceType) != SliceTypes.Intra_4x4)
            throw new InvalidOperationException("The macroblock type is not Intra_4x4");

        var pred = mbLayer.Prediction ?? throw new InvalidOperationException("Prediction data is not available");
        MacroblockLayer mb = mbLayer;

        return Derive(mb, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);

        static int Derive(MacroblockLayer mb, int luma4x4BlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred, IntraPredictionModeCache cache)
        {
            var topMB = util.GetMacroblockToTheTop(currMbAddr);
            var leftMB = util.GetMacroblockToTheLeft(currMbAddr);

            if (topMB is not null && leftMB is not null)
            {
                var leftPred = leftMB.Value.Prediction;
                var topPred = topMB.Value.Prediction;

                if (leftPred is not null && topPred is not null)
                {
                    return Math.Min(
                        Get4x4(false, leftMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache),
                        Get4x4(true, topMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache)
                    );
                }
                else if (leftPred is not null)
                {
                    return Get4x4(false, leftMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
                }
                else if (topPred is not null)
                {
                    return Get4x4(true, topMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
                }
                else
                {
                    return FallbackIntra4x4Mode;
                }
            }
            else if (leftMB is not null)
            {
                var lpred = leftMB.Value.Prediction;
                if (lpred is null)
                    return FallbackIntra4x4Mode;
                else
                    return Get4x4(false, leftMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
            }
            else if (topMB is not null)
            {
                var tpred = topMB.Value.Prediction;
                if (tpred is null)
                    return FallbackIntra4x4Mode;
                else
                    return Get4x4(true, topMB.Value, luma4x4BlkIdx, util, currMbAddr, picWidthInMbs, totalMbsInFrame, pred, cache);
            }
            else
            {
                return FallbackIntra4x4Mode;
            }
        }

        static int Get4x4(bool top, MacroblockLayer mb, int lumaBlkIdx, IMacroblockUtility util, int currMbAddr, int picWidthInMbs, int totalMbsInFrame, MacroblockPrediction pred, IntraPredictionModeCache cache)
        {
            int address = top ? new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Up().Value : new AddressFlow(currMbAddr, picWidthInMbs, totalMbsInFrame).Left().Value;
            return pred.PrevIntra4x4PredModeFlag[lumaBlkIdx]
                   ? cache.Contains4x4(address) ? cache.Get4x4(address) : Derive(
                       mb, lumaBlkIdx, util, address, picWidthInMbs, totalMbsInFrame, pred, cache
                   )
                   : (int)pred.RemIntra4x4PredMode[lumaBlkIdx];
        }
    }
}
