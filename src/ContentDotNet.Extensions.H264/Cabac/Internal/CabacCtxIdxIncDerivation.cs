using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Primitives;
using System.Diagnostics;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Cabac.Internal;

// NOTE: I would've normally put this code into CabacFunctions, but had to move it because
//       there was over 2000+ lines of code, which meant that the memory usage and CPU usage
//       went nuts every time I pressed a key. (This ServiceHub.Host.dotnet.x64.exe program is using
//                                              up to 1GB RAM! That's insane!)
internal static class CabacCtxIdxIncDerivation
{
    public static int DeriveCtxIdxIncForMbSkipFlag(IMacroblockUtility mbUtil, DerivationContext dc, int PicWidthInMbs, bool mbaffFrameFlag, bool mbFieldDecodingFlagNotYetDecodedForThisPair, out bool applyInference)
    {
        BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(dc.CurrMbAddr, PicWidthInMbs, mbaffFrameFlag, out var neighboringMBs);

        applyInference = mbaffFrameFlag && mbFieldDecodingFlagNotYetDecodedForThisPair;

        bool condTermFlagA = !(!neighboringMBs.IsMbAddrAAvailable || mbUtil.IsMbSkipFlagForMacroblock(neighboringMBs.MbAddrA));
        bool condTermFlagB = !(!neighboringMBs.IsMbAddrBAvailable || mbUtil.IsMbSkipFlagForMacroblock(neighboringMBs.MbAddrB));

        return Int32Boolean.I32(condTermFlagA) + Int32Boolean.I32(condTermFlagB);
    }

    public static int DeriveCtxIdxIncForMbFieldDecodingFlag(IMacroblockUtility mbUtil, int picWidthInMbs, DerivationContext dc, out bool applyInference)
    {
        BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddressesMbaff(dc.CurrMbAddr, picWidthInMbs, out var neighboringMacroblocks);
        applyInference = (
            (
                neighboringMacroblocks.IsMbAddrAAvailable && (
                    (
                        neighboringMacroblocks.MbAddrA is P_Skip or B_Skip
                    ) || (
                        neighboringMacroblocks.MbAddrA + 1 is P_Skip or B_Skip
                    )
                )
            )
            ||
            (
                neighboringMacroblocks.IsMbAddrBAvailable && (
                    (
                        neighboringMacroblocks.MbAddrB is P_Skip or B_Skip
                    ) || (
                        neighboringMacroblocks.MbAddrB + 1 is P_Skip or B_Skip
                    )
                )
            )
        );

        bool condTermFlagA = !(!neighboringMacroblocks.IsMbAddrAAvailable || mbUtil.IsFrameMacroblock(neighboringMacroblocks.MbAddrA));
        bool condTermFlagB = !(!neighboringMacroblocks.IsMbAddrBAvailable || mbUtil.IsFrameMacroblock(neighboringMacroblocks.MbAddrB));

        return Int32Boolean.I32(condTermFlagA) + Int32Boolean.I32(condTermFlagB);
    }

    public static int DeriveCtxIdxIncForMbType(int ctxIdxOffset, int picWidthInMbs, IMacroblockUtility mbUtil, DerivationContext dc, int mbType)
    {
        BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(dc.CurrMbAddr, picWidthInMbs, dc.IsMbaff, out var macroblocks);

        bool condTermFlagA = !(macroblocks.IsMbAddrAAvailable || (ctxIdxOffset == 0 && IsSI(mbUtil.GetMacroblock(macroblocks.MbAddrA).MbType)) || (ctxIdxOffset == 3 && mbUtil.GetMacroblock(macroblocks.MbAddrA).MbType == I_NxN) || (ctxIdxOffset == 27 && mbUtil.GetMacroblock(macroblocks.MbAddrA).MbType is B_Skip or B_Direct_16x16));
        bool condTermFlagB = !(macroblocks.IsMbAddrBAvailable || (ctxIdxOffset == 0 && IsSI(mbUtil.GetMacroblock(macroblocks.MbAddrB).MbType)) || (ctxIdxOffset == 3 && mbUtil.GetMacroblock(macroblocks.MbAddrB).MbType == I_NxN) || (ctxIdxOffset == 27 && mbUtil.GetMacroblock(macroblocks.MbAddrB).MbType is B_Skip or B_Direct_16x16));

        return Int32Boolean.I32(condTermFlagA) + Int32Boolean.I32(condTermFlagB);
    }

    public static int DeriveCtxIdxIncForCodedBlockPattern(int ctxIdxOffset, int binIdx, int picWidthInMbs, DerivationContext dc, IMacroblockUtility util)
    {
        if (ctxIdxOffset == 73)
        {
            BaselineDecoder.Scanning.Derive8x8LumaBlocks(dc, binIdx,
                out int mbAddrA, out bool mbAddrAAvailable, out int luma8x8BlkIdxA, out bool luma8x8BlkIdxAAvailable,
                out int mbAddrB, out bool mbAddrBAvailable, out int luma8x8BlkIdxB, out bool luma8x8BlkIdxBAvailable
            );

            bool condTermFlagA = !(!mbAddrAAvailable || util.GetMacroblock(mbAddrA).MbType == I_PCM || (mbAddrA != dc.CurrMbAddr && util.GetMacroblock(mbAddrA).MbType is not P_Skip and not B_Skip) || (((util.GetMacroblock(mbAddrA).CodedBlockPattern % 16) >> luma8x8BlkIdxA) & 1) != 0);
            bool condTermFlagB = !(!mbAddrBAvailable || util.GetMacroblock(mbAddrB).MbType == I_PCM || (mbAddrB != dc.CurrMbAddr && util.GetMacroblock(mbAddrB).MbType is not P_Skip and not B_Skip) || (((util.GetMacroblock(mbAddrB).CodedBlockPattern % 16) >> luma8x8BlkIdxB) & 1) != 0);

            return Int32Boolean.I32(condTermFlagA) + 2 * Int32Boolean.I32(condTermFlagB);
        }
        else
        {
            BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(
                dc.CurrMbAddr, picWidthInMbs, dc.IsMbaff, out var neighboringMacroblocks);

            bool condTermFlagA, condTermFlagB;

            if (neighboringMacroblocks.IsMbAddrAAvailable && util.GetMbType(neighboringMacroblocks.MbAddrA) == I_PCM)
                condTermFlagA = true;
            else
                if ((!neighboringMacroblocks.IsMbAddrAAvailable || util.GetMbType(neighboringMacroblocks.MbAddrA) is P_Skip or B_Skip)
                    || (binIdx == 0 && util.GetMacroblock(neighboringMacroblocks.MbAddrA).CodedBlockPattern / 16 == 0)
                    || (binIdx == 1 && util.GetMacroblock(neighboringMacroblocks.MbAddrA).CodedBlockPattern / 16 == 2))
                condTermFlagA = false;
            else
                condTermFlagA = true;

            if (neighboringMacroblocks.IsMbAddrBAvailable && util.GetMbType(neighboringMacroblocks.MbAddrB) == I_PCM)
                condTermFlagB = true;
            else
                if ((!neighboringMacroblocks.IsMbAddrBAvailable || util.GetMbType(neighboringMacroblocks.MbAddrB) is P_Skip or B_Skip)
                    || (binIdx == 0 && util.GetMacroblock(neighboringMacroblocks.MbAddrB).CodedBlockPattern / 16 == 0)
                    || (binIdx == 1 && util.GetMacroblock(neighboringMacroblocks.MbAddrB).CodedBlockPattern / 16 == 2))
                condTermFlagB = false;
            else
                condTermFlagB = true;

            return Int32Boolean.I32(condTermFlagA) + 2 * Int32Boolean.I32(condTermFlagB) + (binIdx == 1 ? 4 : 0);
        }
    }

    public static int DeriveCtxIdxIncForMbQpDelta(IMacroblockUtility mbUtil, DerivationContext dc, bool transformSize8x8Flag, GeneralSliceType sliceType)
    {
        int? prevMbAddr = Util264.PrevMbAddress(dc.CurrMbAddr);

        if (prevMbAddr is null)
            return 0;

        MacroblockLayer mb = mbUtil.GetMacroblock(prevMbAddr.Value);
        if (mb.MbType is P_Skip or B_Skip)
            return 0;

        if (mb.MbType is I_PCM)
            return 0;

        if (Util264.MbPartPredMode((int)mb.MbType, 0, transformSize8x8Flag, sliceType) == Intra_16x16 &&
            mb.CodedBlockPattern / 16 == 0 &&
            mb.CodedBlockPattern % 16 == 0)
            return 0;

        if (mb.MbQpDelta == 0)
            return 0;

        return 1;
    }

    public static int DeriveCtxIdxIncForRefIdxLX(int mbPartIdx, IMacroblockUtility mbUtil, bool transformSize8x8Flag, bool useL0OverL1, int mbType, MacroblockTypeHistory mbTypeArray, ReadOnlySpan<int> refIdxLX, MacroblockTypeHistory subMbTypeArray, GeneralSliceType sliceType, DerivationContext dc)
    {
        int predLX = useL0OverL1 ? Pred_L0 : Pred_L1;

        // C and D is ignored; it's just there so we can pass the parameters to DeriveNeighboringPartitions

        int mbAddrA = 0, mbPartIdxA = 0, subMbPartIdxA = 0;
        int mbAddrB = 0, mbPartIdxB = 0, subMbPartIdxB = 0;
        int mbAddrC = 0, mbPartIdxC = 0, subMbPartIdxC = 0;
        int mbAddrD = 0, mbPartIdxD = 0, subMbPartIdxD = 0;
        bool validA = false, validB = false, validC = false, validD = false;

        BaselineDecoder.Scanning.DeriveNeighboringPartitions(
            sliceType: sliceType,
            dc: dc,
            mbPartIdx: mbPartIdx,
            currSubMbType: subMbTypeArray[mbPartIdx],
            subMbPartIdx: 0,
            mbType: mbType,
            mbTypeArray: mbTypeArray,
            subMbType: subMbTypeArray,
            ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA,
            ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB,
            ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC,
            ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD
        );

        bool refIdxZeroFlagA = dc.IsMbaff && mbUtil.IsFrameMacroblock(dc.CurrMbAddr) && mbUtil.IsFieldMacroblock(mbAddrA) ? (refIdxLX[mbPartIdxA] > 1) ? false : true : (refIdxLX[mbPartIdxA] > 0) ? false : true;
        bool refIdxZeroFlagB = dc.IsMbaff && mbUtil.IsFrameMacroblock(dc.CurrMbAddr) && mbUtil.IsFieldMacroblock(mbAddrB) ? (refIdxLX[mbPartIdxB] > 1) ? false : true : (refIdxLX[mbPartIdxB] > 0) ? false : true;

        MacroblockLayer mbA = mbUtil.GetMacroblock(mbAddrA);
        MacroblockLayer mbB = mbUtil.GetMacroblock(mbAddrB);

        bool predModeEqualFlagA;
        bool predModeEqualFlagB;

        if (mbA.MbType is B_Direct_16x16 or B_Skip)
        {
            predModeEqualFlagA = true;
        }
        else
        {
            if (mbType is P_8x8 or B_8x8)
            {
                SubMacroblockPrediction? subMbTypeArrayA = mbA.SubMacroblockPrediction;
                if (subMbTypeArrayA is null)
                {
                    predModeEqualFlagA = false;
                }
                else
                {
                    uint subMbTypeA = subMbTypeArrayA.Value.SubMbType[mbPartIdxA];
                    if (subMbTypeA != predLX && subMbTypeA != BiPred)
                    {
                        predModeEqualFlagA = false;
                    }
                    else
                    {
                        predModeEqualFlagA = true;
                    }
                }
            }
            else
            {
                int mbPartPredMode = Util264.MbPartPredMode((int)mbA.MbType, mbPartIdxA, transformSize8x8Flag, sliceType);
                if (mbPartPredMode != predLX && mbPartPredMode != BiPred)
                {
                    predModeEqualFlagA = false;
                }
                else
                {
                    predModeEqualFlagA = true;
                }
            }
        }

        if (mbB.MbType is B_Direct_16x16 or B_Skip)
        {
            predModeEqualFlagB = true;
        }
        else
        {
            if (mbType is P_8x8 or B_8x8)
            {
                SubMacroblockPrediction? subMbTypeArrayB = mbB.SubMacroblockPrediction;
                if (subMbTypeArrayB is null)
                {
                    predModeEqualFlagB = false;
                }
                else
                {
                    uint subMbTypeB = subMbTypeArrayB.Value.SubMbType[mbPartIdxB];
                    if (subMbTypeB != predLX && subMbTypeB != BiPred)
                    {
                        predModeEqualFlagB = false;
                    }
                    else
                    {
                        predModeEqualFlagB = true;
                    }
                }
            }
            else
            {
                MacroblockPrediction? mbTypeArrayB = mbB.Prediction;
                if (mbTypeArrayB is null)
                {
                    predModeEqualFlagB = false;
                }
                else
                {
                    int mbPartPredMode = Util264.MbPartPredMode((int)mbB.MbType, mbPartIdxB, transformSize8x8Flag, sliceType);
                    if (mbPartPredMode != predLX && mbPartPredMode != BiPred)
                    {
                        predModeEqualFlagB = false;
                    }
                    else
                    {
                        predModeEqualFlagB = true;
                    }
                }
            }
        }

        bool condTermFlagA = !(!validA || mbA.MbType is P_Skip or B_Skip || mbUtil.IsCodedWithIntra(mbAddrA) || !predModeEqualFlagA || refIdxZeroFlagA);
        bool condTermFlagB = !(!validB || mbB.MbType is P_Skip or B_Skip || mbUtil.IsCodedWithIntra(mbAddrB) || !predModeEqualFlagB || refIdxZeroFlagB);

        return Int32Boolean.I32(condTermFlagA) + 2 * Int32Boolean.I32(condTermFlagB);
    }

    public static int DeriveCtxIdxIncForMvdLX(int mbPartIdx, int subMbPartIdx, int ctxIdxOffset, bool transformSize8x8Flag, IMacroblockUtility mbUtil, GeneralSliceType sliceType, DerivationContext dc, MacroblockTypeHistory mbTypeArray, MacroblockTypeHistory subMbTypeArray, int mbType, ContainerMatrix4x4x2 mvdLX, bool invokedForL0)
    {
        int predLX = invokedForL0 ? Pred_L0 : Pred_L1;

        // C and D is ignored; it's just there so we can pass the parameters to DeriveNeighboringPartitions

        int mbAddrA = 0, mbPartIdxA = 0, subMbPartIdxA = 0;
        int mbAddrB = 0, mbPartIdxB = 0, subMbPartIdxB = 0;
        int mbAddrC = 0, mbPartIdxC = 0, subMbPartIdxC = 0;
        int mbAddrD = 0, mbPartIdxD = 0, subMbPartIdxD = 0;
        bool validA = false, validB = false, validC = false, validD = false;

        BaselineDecoder.Scanning.DeriveNeighboringPartitions(
            sliceType: sliceType,
            dc: dc,
            mbPartIdx: mbPartIdx,
            currSubMbType: subMbTypeArray[mbPartIdx],
            subMbPartIdx: subMbPartIdx,
            mbType: mbType,
            mbTypeArray: mbTypeArray,
            subMbType: subMbTypeArray,
            ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA,
            ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB,
            ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC,
            ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD
        );

        int compIdx = ctxIdxOffset == 40 ? 0 : 1;

        bool predModeEqualFlagA;
        bool predModeEqualFlagB;

        MacroblockLayer mbA = mbUtil.GetMacroblock(mbAddrA);
        MacroblockLayer mbB = mbUtil.GetMacroblock(mbAddrB);

        if (mbA.MbType is B_Direct_16x16 or B_Skip)
        {
            predModeEqualFlagA = false;
        }
        else
        {
            if (mbA.MbType is P_8x8 or B_8x8)
            {
                SubMacroblockPrediction? prediction = mbA.SubMacroblockPrediction;
                if (prediction is null)
                {
                    predModeEqualFlagA = false;
                }
                else
                {
                    uint subMbTypeA = prediction.Value.SubMbType[mbPartIdxA];
                    int subMbPredMode = Util264.SubMbPredMode((int)subMbTypeA, sliceType);

                    if (subMbPredMode != predLX && subMbPredMode != BiPred)
                    {
                        predModeEqualFlagA = false;
                    }
                    else
                    {
                        predModeEqualFlagA = true;
                    }
                }
            }
            else
            {
                int mbTypeA = (int)mbA.MbType;
                int mbPartPredMode = Util264.MbPartPredMode(mbTypeA, mbPartIdxA, transformSize8x8Flag, sliceType);
                if (mbPartPredMode != predLX && mbPartPredMode != BiPred)
                {
                    predModeEqualFlagA = false;
                }
                else
                {
                    predModeEqualFlagA = true;
                }
            }
        }

        if (mbB.MbType is B_Direct_16x16 or B_Skip)
        {
            predModeEqualFlagB = false;
        }
        else
        {
            if (mbB.MbType is P_8x8 or B_8x8)
            {
                SubMacroblockPrediction? prediction = mbB.SubMacroblockPrediction;
                if (prediction is null)
                {
                    predModeEqualFlagB = false;
                }
                else
                {
                    uint subMbTypeB = prediction.Value.SubMbType[mbPartIdxB];
                    int subMbPredMode = Util264.SubMbPredMode((int)subMbTypeB, sliceType);

                    if (subMbPredMode != predLX && subMbPredMode != BiPred)
                    {
                        predModeEqualFlagB = false;
                    }
                    else
                    {
                        predModeEqualFlagB = true;
                    }
                }
            }
            else
            {
                int mbTypeB = (int)mbB.MbType;
                int mbPartPredMode = Util264.MbPartPredMode(mbTypeB, mbPartIdxB, transformSize8x8Flag, sliceType);
                if (mbPartPredMode != predLX && mbPartPredMode != BiPred)
                {
                    predModeEqualFlagB = false;
                }
                else
                {
                    predModeEqualFlagB = true;
                }
            }
        }

        int absMvdCompA;
        int absMvdCompB;

        if (!validA ||
            mbA.MbType is P_Skip or B_Skip ||
            mbUtil.IsCodedWithIntra(mbAddrA) ||
            !predModeEqualFlagA)
        {
            absMvdCompA = 0;
        }
        else
        {
            if (compIdx == 1 &&
                dc.IsMbaff &&
                mbUtil.IsFrameMacroblock(dc.CurrMbAddr) &&
                mbUtil.IsFieldMacroblock(mbAddrA))
            {
                absMvdCompA = Math.Abs(mvdLX[mbPartIdxA, subMbPartIdxA, compIdx]) * 2;
            }
            else if (compIdx == 1 &&
                     dc.IsMbaff &&
                     mbUtil.IsFieldMacroblock(dc.CurrMbAddr) &&
                     mbUtil.IsFrameMacroblock(mbAddrA))
            {
                absMvdCompA = Math.Abs(mvdLX[mbPartIdxA, subMbPartIdxA, compIdx]) / 2;
            }
            else
            {
                absMvdCompA = Math.Abs(mvdLX[mbPartIdxA, subMbPartIdxA, compIdx]);
            }
        }

        if (!validB ||
            mbB.MbType is P_Skip or B_Skip ||
            mbUtil.IsCodedWithIntra(mbAddrB) ||
            !predModeEqualFlagB)
        {
            absMvdCompB = 0;
        }
        else
        {
            if (compIdx == 1 &&
                dc.IsMbaff &&
                mbUtil.IsFrameMacroblock(dc.CurrMbAddr) &&
                mbUtil.IsFieldMacroblock(mbAddrB))
            {
                absMvdCompB = Math.Abs(mvdLX[mbPartIdxB, subMbPartIdxB, compIdx]) * 2;
            }
            else if (compIdx == 1 &&
                     dc.IsMbaff &&
                     mbUtil.IsFieldMacroblock(dc.CurrMbAddr) &&
                     mbUtil.IsFrameMacroblock(mbAddrB))
            {
                absMvdCompB = Math.Abs(mvdLX[mbPartIdxB, subMbPartIdxB, compIdx]) / 2;
            }
            else
            {
                absMvdCompB = Math.Abs(mvdLX[mbPartIdxB, subMbPartIdxB, compIdx]);
            }
        }

        // Love how the H.264 spec has that funky typo at page 291:
        // "If absMvdCompA is greater than 32 or absMvdCompA is greater than 32"

        if (absMvdCompA > 32)
        {
            return 2;
        }
        else if (absMvdCompA + absMvdCompB > 32)
        {
            return 2;
        }
        else if (absMvdCompA + absMvdCompB > 2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static int DeriveCtxIdxIncForIntraChromaPredMode(DerivationContext dc, IMacroblockUtility mbUtil, int picWidthInMbs)
    {
        BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(
            dc.CurrMbAddr,
            picWidthInMbs,
            dc.IsMbaff,
            out var neighboringMacroblocks
        );

        MacroblockLayer mbA = mbUtil.GetMacroblock(neighboringMacroblocks.MbAddrA);
        MacroblockLayer mbB = mbUtil.GetMacroblock(neighboringMacroblocks.MbAddrB);

        bool condTermFlagA = !(neighboringMacroblocks.IsMbAddrAAvailable || mbUtil.IsCodedWithInter(neighboringMacroblocks.MbAddrA) || mbA.MbType is I_PCM || mbA.Prediction?.IntraChromaPredMode == 0);
        bool condTermFlagB = !(neighboringMacroblocks.IsMbAddrBAvailable || mbUtil.IsCodedWithInter(neighboringMacroblocks.MbAddrA) || mbB.MbType is I_PCM || mbB.Prediction?.IntraChromaPredMode == 0);

        return Int32Boolean.I32(condTermFlagA) + Int32Boolean.I32(condTermFlagB);
    }

    // [?] Depending on ctxBlockCat, two parameters play a role.
    //     0, 6, 10 - both are zero.
    //     1, 2 - parameter1 is luma4x4BlkIdx
    //     3 - parameter1 is iCbCr
    //     4 - parameter1 is chroma8x8BlkIdx, parameter2 is iCbCr
    //     5 - parameter1 is luma8x8BlkIdx
    //     7, 8 - parameter1 is cb4x4BlkIdx
    //     9 - parameter1 is cr4x4BlkIdx
    //     11, 12 - parameter1 is cb8x8BlkIdx
    //     13 - parameter1 is cr8x8BlkIdx
    public static int DeriveCtxIdxIncForCodedBlockFlag(DerivationContext dc, IMacroblockUtility mbUtil, bool constrainedIntraPredFlag, int nalUnitType, int picWidthInMbs, int ctxBlockCat, int parameter1, int parameter2)
    {
        // The spec just determines transBlock[A/B] based on ctxBlockCat.
        // And then they're like "is set to equal to coded_block_flag of the macroblock that owns transBlock".
        // I just don't get it - why not immediately set coded_block_flag instead of undergoing
        // setting the transform block that could have multiple types? I just immediately set coded_block_flag
        // immediately.
        // [!] I might have overlooked, though. Let me know if this the case.

        bool transformCodedBlockFlagA = false;
        bool transformCodedBlockFlagB = false;
        int mbAddrA;
        int mbAddrB;
        bool mbAddrAAvailable;
        bool mbAddrBAvailable;

        if (ctxBlockCat is 0 or 6 or 10)
        {
            BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(
                dc.CurrMbAddr,
                picWidthInMbs,
                dc.IsMbaff,
                out var neighboringMacroblocks
            );

            MacroblockLayer mbA = mbUtil.GetMacroblock(neighboringMacroblocks.MbAddrA);
            MacroblockLayer mbB = mbUtil.GetMacroblock(neighboringMacroblocks.MbAddrB);

            Residual residualA = mbA.Intra16x16Residual ?? default;
            Residual residualB = mbB.Intra16x16Residual ?? default;

            mbAddrA = neighboringMacroblocks.MbAddrA;
            mbAddrAAvailable = neighboringMacroblocks.IsMbAddrAAvailable;
            mbAddrB = neighboringMacroblocks.MbAddrB;
            mbAddrBAvailable = neighboringMacroblocks.IsMbAddrBAvailable;

            Debug.Assert(residualA.PreferCabac || residualB.PreferCabac, "Preference of CAVLC on a CABAC operation");

            if (residualA.PreferCabac && residualB.PreferCabac)
            {
                if (ctxBlockCat == 0)
                {
                    transformCodedBlockFlagA = residualA.FirstLumaResidual.StartResidualCabac!.Value.CodedBlockFlag;
                    transformCodedBlockFlagB = residualB.FirstLumaResidual.StartResidualCabac!.Value.CodedBlockFlag;
                }
                else if (ctxBlockCat == 6)
                {
                    if (residualA.Yuv444Cb is not null && residualB.Yuv444Cb is not null)
                    {
                        transformCodedBlockFlagA = residualA.Yuv444Cb.Value.StartResidualCabac!.Value.CodedBlockFlag;
                        transformCodedBlockFlagB = residualB.Yuv444Cb.Value.StartResidualCabac!.Value.CodedBlockFlag;
                    }
                }
                else // ctxBlockCat is 10
                {
                    if (residualA.Yuv444Cr is not null && residualB.Yuv444Cr is not null)
                    {
                        transformCodedBlockFlagA = residualA.Yuv444Cr.Value.StartResidualCabac!.Value.CodedBlockFlag;
                        transformCodedBlockFlagB = residualB.Yuv444Cr.Value.StartResidualCabac!.Value.CodedBlockFlag;
                    }
                }
            }
        }
        else if (ctxBlockCat is 1 or 2)
        {
            BaselineDecoder.Scanning.Derive4x4LumaBlocks(
                parameter1,
                dc,
                out mbAddrA, out mbAddrAAvailable, out int luma4x4BlkIdxA, out _,
                out mbAddrB, out mbAddrBAvailable, out int luma4x4BlkIdxB, out _
            );

            if (mbAddrAAvailable || mbAddrBAvailable)
            {
                MacroblockLayer mbA = mbUtil.GetMacroblock(mbAddrA);
                MacroblockLayer mbB = mbUtil.GetMacroblock(mbAddrB);

                transformCodedBlockFlagA = ProcessMacroblockLayer(mbA, luma4x4BlkIdxA, true);
                transformCodedBlockFlagB = ProcessMacroblockLayer(mbB, luma4x4BlkIdxB, false);

                static bool ProcessMacroblockLayer(MacroblockLayer mbN, int luma4x4BlkIdxN, bool nIsA)
                {
                    bool condition = mbN.MbType is not P_Skip and not B_Skip;
                    if (nIsA)
                        condition &= mbN.MbType is not I_PCM;

                    if (condition)
                    {
                        int codedBlockPatternLuma = mbN.GetCodedBlockPatternLuma();
                        if (((codedBlockPatternLuma >> (luma4x4BlkIdxN >> 2)) & 1) != 0)
                        {
                            if (mbN.TransformSize8x8Flag)
                            {
                                // When ctxBlockCat is 1 or 2, we're operating under the Luma
                                // channel, so pull the coded_block_flag from the luma channel
                                // in the residuals.

                                return mbN.Intra16x16Residual!.Value.FirstLumaResidual.StartResidualCabac!.Value.CodedBlockFlag;
                            }
                        }
                    }

                    // Auto-default to false when some data is inaccessible,
                    // invalid, or does not suit.

                    return false;
                }
            }
        }
        else if (ctxBlockCat == 3)
        {
            BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(
                dc.CurrMbAddr,
                picWidthInMbs,
                dc.IsMbaff,
                out var neighboringMacroblocks
            );

            transformCodedBlockFlagA = Derive(neighboringMacroblocks.MbAddrA, neighboringMacroblocks.IsMbAddrAAvailable);
            transformCodedBlockFlagB = Derive(neighboringMacroblocks.MbAddrB, neighboringMacroblocks.IsMbAddrBAvailable);

            mbAddrA = neighboringMacroblocks.MbAddrA;
            mbAddrAAvailable = neighboringMacroblocks.IsMbAddrAAvailable;
            mbAddrB = neighboringMacroblocks.MbAddrB;
            mbAddrBAvailable = neighboringMacroblocks.IsMbAddrBAvailable;

            bool Derive(int mbAddrN, bool mbAddrNAvailable)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);
                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (mbN.GetCodedBlockPatternChroma() != 0)
                        {
                            // We operate under the Chroma channel right here,
                            // so pull that.

                            Residual? residual = mbN.Intra16x16Residual;
                            if (residual is null)
                                return false;

                            return residual.Value.GetCbCrResidualBlockCabac(parameter1)?.CodedBlockFlag ?? false;
                        }
                    }
                }

                // Auto-default to false.

                return false;
            }
        }
        else if (ctxBlockCat == 4)
        {
            BaselineDecoder.Scanning.Derive4x4ChromaBlocks(
                dc,
                parameter1,
                out mbAddrA, out mbAddrAAvailable, out _, out _,
                out mbAddrB, out mbAddrBAvailable, out _, out _
            );

            transformCodedBlockFlagA = Derive(mbAddrA, mbAddrAAvailable);
            transformCodedBlockFlagB = Derive(mbAddrB, mbAddrBAvailable);

            bool Derive(int mbAddrN, bool mbAddrNAvailable)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);
                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (mbN.GetCodedBlockPatternChroma() == 2)
                        {
                            // We operate under the Chroma channel right here,
                            // so pull that.
                            Residual? residual = mbN.Intra16x16Residual;
                            if (residual is null)
                                return false;

                            return residual.Value.GetCbCrResidualBlockCabac(parameter2)?.CodedBlockFlag ?? false;
                        }
                    }
                }

                return false;
            }
        }
        else if (ctxBlockCat == 5)
        {
            BaselineDecoder.Scanning.Derive8x8LumaBlocks(
                dc,
                parameter1,
                out mbAddrA, out mbAddrAAvailable, out _, out _,
                out mbAddrB, out mbAddrBAvailable, out _, out _
            );

            transformCodedBlockFlagA = Derive(mbAddrA, mbAddrAAvailable);
            transformCodedBlockFlagB = Derive(mbAddrB, mbAddrBAvailable);

            bool Derive(int mbAddrN, bool mbAddrNAvailable)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);
                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (((mbN.GetCodedBlockPatternLuma() >> parameter1) & 1) != 0)
                        {
                            // We operate under the Luma channel right here,
                            // so pull the coded_block_flag from the luma channel
                            // in the residuals.

                            return mbN.Intra16x16Residual!.Value.FirstLumaResidual.StartResidualCabac!.Value.CodedBlockFlag;
                        }
                    }
                }

                // Auto-default to false.

                return false;
            }
        }
        else if (ctxBlockCat is 7 or 8)
        {
            BaselineDecoder.Scanning.Derive4x4CbBlocks(
                parameter1,
                dc,
                out mbAddrA, out mbAddrAAvailable, out int cb4x4BlkIdxA, out _,
                out mbAddrB, out mbAddrBAvailable, out int cb4x4BlkIdxB, out _
            );

            transformCodedBlockFlagA = Derive(mbAddrA, mbAddrAAvailable, cb4x4BlkIdxA);
            transformCodedBlockFlagB = Derive(mbAddrB, mbAddrBAvailable, cb4x4BlkIdxB);

            bool Derive(int mbAddrN, bool mbAddrNAvailable, int cb4x4BlkIdxN)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);

                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (((mbN.GetCodedBlockPatternLuma() >> (cb4x4BlkIdxN >> 2)) & 1) != 0)
                        {
                            Residual? residual = mbN.Intra16x16Residual;
                            if (residual is null)
                                return false;

                            var cabacCbCr = residual.Value.CabacCbCr;
                            if (cabacCbCr is null)
                                return false;

                            return cabacCbCr.Value.First.CodedBlockFlag;
                        }
                    }
                }

                // Auto-default to false.

                return false;
            }
        }
        else if (ctxBlockCat == 9)
        {
            BaselineDecoder.Scanning.Derive8x8CbBlocks(
                dc,
                parameter1,
                out mbAddrA, out mbAddrAAvailable, out int cb8x8BlkIdxA, out _,
                out mbAddrB, out mbAddrBAvailable, out int cb8x8BlkIdxB, out _
            );

            transformCodedBlockFlagA = Derive(mbAddrA, mbAddrAAvailable, cb8x8BlkIdxA);
            transformCodedBlockFlagB = Derive(mbAddrB, mbAddrBAvailable, cb8x8BlkIdxB);

            bool Derive(int mbAddrN, bool mbAddrNAvailable, int cb8x8BlkIdxN)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);

                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (((mbN.GetCodedBlockPatternLuma() >> cb8x8BlkIdxN) & 1) != 0)
                        {
                            Residual? residual = mbN.Intra16x16Residual;
                            if (residual is null)
                                return false;

                            var cabacCbCr = residual.Value.CabacCbCr;
                            if (cabacCbCr is null)
                                return false;

                            return cabacCbCr.Value.First.CodedBlockFlag;
                        }
                    }
                }

                // Auto-default to false.

                return false;
            }
        }
        else if (ctxBlockCat is 11 or 12)
        {
            BaselineDecoder.Scanning.Derive4x4CrBlocks(
                parameter1,
                dc,
                out mbAddrA, out mbAddrAAvailable, out int cr4x4BlkIdxA, out _,
                out mbAddrB, out mbAddrBAvailable, out int cr4x4BlkIdxB, out _
            );

            transformCodedBlockFlagA = Derive(mbAddrA, mbAddrAAvailable, cr4x4BlkIdxA);
            transformCodedBlockFlagB = Derive(mbAddrB, mbAddrBAvailable, cr4x4BlkIdxB);

            bool Derive(int mbAddrN, bool mbAddrNAvailable, int cr4x4BlkIdxN)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);

                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (((mbN.GetCodedBlockPatternLuma() >> (cr4x4BlkIdxN >> 2)) & 1) != 0)
                        {
                            Residual? residual = mbN.Intra16x16Residual;
                            if (residual is null)
                                return false;

                            var cabacCbCr = residual.Value.CabacCbCr;
                            if (cabacCbCr is null)
                                return false;

                            return cabacCbCr.Value.Second.CodedBlockFlag;
                        }
                    }
                }

                // Auto-default to false.

                return false;
            }
        }
        else
        {
            BaselineDecoder.Scanning.Derive8x8CrBlocks(
                dc,
                parameter1,
                out mbAddrA, out mbAddrAAvailable, out int cr8x8BlkIdxA, out _,
                out mbAddrB, out mbAddrBAvailable, out int cr8x8BlkIdxB, out _
            );

            transformCodedBlockFlagA = Derive(mbAddrA, mbAddrAAvailable, cr8x8BlkIdxA);
            transformCodedBlockFlagB = Derive(mbAddrB, mbAddrBAvailable, cr8x8BlkIdxB);

            bool Derive(int mbAddrN, bool mbAddrNAvailable, int cr8x8BlkIdxN)
            {
                if (mbAddrNAvailable)
                {
                    MacroblockLayer mbN = mbUtil.GetMacroblock(mbAddrN);

                    if (mbN.MbType is not P_Skip and not B_Skip and not I_PCM)
                    {
                        if (((mbN.GetCodedBlockPatternLuma() >> cr8x8BlkIdxN) & 1) != 0)
                        {
                            Residual? residual = mbN.Intra16x16Residual;
                            if (residual is null)
                                return false;

                            var cabacCbCr = residual.Value.CabacCbCr;
                            if (cabacCbCr is null)
                                return false;

                            return cabacCbCr.Value.Second.CodedBlockFlag;
                        }
                    }
                }

                // Auto-default to false.

                return false;
            }
        }

        bool condTermFlagA = DeriveCondTermFlag(mbAddrA, mbAddrAAvailable, transformCodedBlockFlagA);
        bool condTermFlagB = DeriveCondTermFlag(mbAddrB, mbAddrBAvailable, transformCodedBlockFlagB);

        return Int32Boolean.I32(condTermFlagA) + 2 * Int32Boolean.I32(condTermFlagB);

        bool DeriveCondTermFlag(int mbAddrN, bool mbAddrNAvailable, bool transformCodedBlockFlagN)
        {
            if ((!mbAddrNAvailable && mbUtil.IsCodedWithInter(dc.CurrMbAddr)) ||
                (mbAddrNAvailable && !transformCodedBlockFlagN && mbUtil.GetMbType(mbAddrN) != I_PCM) ||
                (mbUtil.IsCodedWithIntra(dc.CurrMbAddr) && constrainedIntraPredFlag && (mbAddrNAvailable && mbUtil.IsCodedWithInter(mbAddrN)) && nalUnitType is >= 2 and <= 4))
            {
                return false;
            }
            else if ((!mbAddrNAvailable && mbUtil.IsCodedWithInter(dc.CurrMbAddr)) ||
                mbUtil.GetMbType(mbAddrN) == I_PCM)
            {
                return true;
            }

            return transformCodedBlockFlagN;
        }
    }

    public static int DeriveCtxIdxIncForTransformSize8x8Flag(DerivationContext dc, IMacroblockUtility mbUtil, int picWidthInMbs)
    {
        BaselineDecoder.Scanning.DeriveNeighboringMacroblockAddresses(
            dc.CurrMbAddr,
            picWidthInMbs,
            dc.IsMbaff,
            out var neighboringMacroblocks
        );

        MacroblockLayer mbA = mbUtil.GetMacroblock(neighboringMacroblocks.MbAddrA);
        MacroblockLayer mbB = mbUtil.GetMacroblock(neighboringMacroblocks.MbAddrB);

        bool condTermFlagA = !(!neighboringMacroblocks.IsMbAddrAAvailable || !mbA.TransformSize8x8Flag);
        bool condTermFlagB = !(!neighboringMacroblocks.IsMbAddrBAvailable || !mbB.TransformSize8x8Flag);

        return Int32Boolean.I32(condTermFlagA) + Int32Boolean.I32(condTermFlagB);
    }

    public static int AssignCtxIdxIncUsingPriorDecodedBinValues(int b1, int b2, int b3, int ctxIdxOffset, int binIdx)
    {
        if (ctxIdxOffset == 3)
            return binIdx == 4 ? (b3 != 0 ? 5 : 6) : (b3 != 0 ? 6 : 7);
        else if (ctxIdxOffset == 14 && binIdx == 2)
            return (b1 != 1) ? 2 : 3;
        else if (ctxIdxOffset == 17 && binIdx == 4)
            return (b3 != 0) ? 2 : 3;
        else if (ctxIdxOffset == 27 && binIdx == 2)
            return (b1 != 0) ? 4 : 5;
        else if (ctxIdxOffset == 32 && binIdx == 4)
            return (b3 != 0) ? 2 : 3;
        else if (ctxIdxOffset == 36 && binIdx == 2)
            return (b1 != 0) ? 2 : 3;
        else
            return 0;
    }

    /// <summary>
    ///   Assigns for significant_coeff_flag, last_significant_coeff_flag, and coeff_abs_level_minus1
    /// </summary>
    /// <remarks>
    ///   <c>mode</c> is 1 for significant_coeff_flag, 2 for last_significant_coeff_flag, and 3 for coeff_abs_level_minus1.
    /// </remarks>
    public static int AssignCtxIdxIncForCoeffFlagsAndAbsLevel(int ctxIdxOffset, int binIdx, int NumC8x8, int levelListIdx, ResidualBlockType blkType, int mode, bool isFrame, int numDecodAbsLevelEq1, int numDecodAbsLevelGt1)
    {
        int maxNumCoeff = 0;
        int ctxBlockCat = 0;

        switch (blkType)
        {
            case ResidualBlockType.Intra16x16DCLevel:
                maxNumCoeff = 16;
                ctxBlockCat = 0;
                break;

            case ResidualBlockType.Intra16x16ACLevel:
                maxNumCoeff = 15;
                ctxBlockCat = 1;
                break;

            case ResidualBlockType.LumaLevel4x4:
                maxNumCoeff = 16;
                ctxBlockCat = 2;
                break;

            case ResidualBlockType.ChromaDCLevel:
                maxNumCoeff = 4 * NumC8x8;
                ctxBlockCat = 3;
                break;

            case ResidualBlockType.ChromaACLevel:
                maxNumCoeff = 15;
                ctxBlockCat = 4;
                break;

            case ResidualBlockType.LumaLevel8x8:
                maxNumCoeff = 64;
                ctxBlockCat = 5;
                break;

            case ResidualBlockType.Cb16x16DCLevel:
                maxNumCoeff = 16;
                ctxBlockCat = 6;
                break;

            case ResidualBlockType.Cb16x16ACLevel:
                maxNumCoeff = 15;
                ctxBlockCat = 7;
                break;

            case ResidualBlockType.CbLevel4x4:
                maxNumCoeff = 16;
                ctxBlockCat = 8;
                break;

            case ResidualBlockType.CbLevel8x8:
                maxNumCoeff = 64;
                ctxBlockCat = 9;
                break;

            case ResidualBlockType.Cr16x16DCLevel:
                maxNumCoeff = 16;
                ctxBlockCat = 10;
                break;

            case ResidualBlockType.Cr16x16ACLevel:
                maxNumCoeff = 15;
                ctxBlockCat = 11;
                break;

            case ResidualBlockType.CrLevel4x4:
                maxNumCoeff = 16;
                ctxBlockCat = 12;
                break;

            case ResidualBlockType.CrLevel8x8:
                maxNumCoeff = 64;
                ctxBlockCat = 13;
                break;

            default:
                throw new NotImplementedException($"ResidualBlockType {blkType} is not implemented.");
        }

        if ((mode is 1 or 2) && (ctxBlockCat is not 3 and not 5 and not 9 and not 13))
            return levelListIdx;

        if ((mode is 1 or 2) && ctxBlockCat == 3)
            return Math.Min(levelListIdx / NumC8x8, 2);

        int ctxIdxInc = 0;

        // BEGIN GENERATED CODE

        switch (levelListIdx)
        {
            case 0:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 0;
                    else
                        ctxIdxInc = 0;
                else
                    ctxIdxInc = 0;
                break;

            case 1:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 1;
                    else
                        ctxIdxInc = 1;
                else
                    ctxIdxInc = 1;
                break;

            case 2:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 2;
                    else
                        ctxIdxInc = 1;
                else
                    ctxIdxInc = 1;
                break;

            case 3:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 3;
                    else
                        ctxIdxInc = 2;
                else
                    ctxIdxInc = 1;
                break;

            case 4:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 2;
                else
                    ctxIdxInc = 1;
                break;

            case 5:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 5;
                    else
                        ctxIdxInc = 3;
                else
                    ctxIdxInc = 1;
                break;

            case 6:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 5;
                    else
                        ctxIdxInc = 3;
                else
                    ctxIdxInc = 1;
                break;

            case 7:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 4;
                else
                    ctxIdxInc = 1;
                break;

            case 8:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 5;
                else
                    ctxIdxInc = 1;
                break;

            case 9:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 3;
                    else
                        ctxIdxInc = 6;
                else
                    ctxIdxInc = 1;
                break;

            case 10:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 3;
                    else
                        ctxIdxInc = 7;
                else
                    ctxIdxInc = 1;
                break;

            case 11:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 7;
                else
                    ctxIdxInc = 1;
                break;

            case 12:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 7;
                else
                    ctxIdxInc = 1;
                break;

            case 13:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 8;
                else
                    ctxIdxInc = 1;
                break;

            case 14:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 5;
                    else
                        ctxIdxInc = 4;
                else
                    ctxIdxInc = 1;
                break;

            case 15:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 5;
                    else
                        ctxIdxInc = 5;
                else
                    ctxIdxInc = 1;
                break;

            case 16:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 6;
                else
                    ctxIdxInc = 2;
                break;

            case 17:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 2;
                break;

            case 18:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 2;
                break;

            case 19:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 4;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 2;
                break;

            case 20:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 3;
                    else
                        ctxIdxInc = 8;
                else
                    ctxIdxInc = 2;
                break;

            case 21:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 3;
                    else
                        ctxIdxInc = 11;
                else
                    ctxIdxInc = 2;
                break;

            case 22:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 6;
                    else
                        ctxIdxInc = 12;
                else
                    ctxIdxInc = 2;
                break;

            case 23:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 7;
                    else
                        ctxIdxInc = 11;
                else
                    ctxIdxInc = 2;
                break;

            case 24:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 7;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 2;
                break;

            case 25:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 7;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 2;
                break;

            case 26:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 8;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 2;
                break;

            case 27:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 9;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 2;
                break;

            case 28:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 10;
                    else
                        ctxIdxInc = 8;
                else
                    ctxIdxInc = 2;
                break;

            case 29:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 9;
                    else
                        ctxIdxInc = 11;
                else
                    ctxIdxInc = 2;
                break;

            case 30:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 8;
                    else
                        ctxIdxInc = 12;
                else
                    ctxIdxInc = 2;
                break;

            case 31:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 7;
                    else
                        ctxIdxInc = 11;
                else
                    ctxIdxInc = 2;
                break;

            case 32:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 7;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 3;
                break;

            case 33:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 6;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 3;
                break;

            case 34:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 11;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 3;
                break;

            case 35:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 12;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 3;
                break;

            case 36:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 13;
                    else
                        ctxIdxInc = 8;
                else
                    ctxIdxInc = 3;
                break;

            case 37:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 11;
                    else
                        ctxIdxInc = 11;
                else
                    ctxIdxInc = 3;
                break;

            case 38:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 6;
                    else
                        ctxIdxInc = 12;
                else
                    ctxIdxInc = 3;
                break;

            case 39:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 7;
                    else
                        ctxIdxInc = 11;
                else
                    ctxIdxInc = 3;
                break;

            case 40:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 8;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 4;
                break;

            case 41:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 9;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 4;
                break;

            case 42:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 14;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 4;
                break;

            case 43:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 10;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 4;
                break;

            case 44:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 9;
                    else
                        ctxIdxInc = 8;
                else
                    ctxIdxInc = 4;
                break;

            case 45:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 8;
                    else
                        ctxIdxInc = 13;
                else
                    ctxIdxInc = 4;
                break;

            case 46:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 6;
                    else
                        ctxIdxInc = 13;
                else
                    ctxIdxInc = 4;
                break;

            case 47:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 11;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 4;
                break;

            case 48:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 12;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 5;
                break;

            case 49:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 13;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 5;
                break;

            case 50:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 11;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 5;
                break;

            case 51:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 6;
                    else
                        ctxIdxInc = 8;
                else
                    ctxIdxInc = 5;
                break;

            case 52:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 9;
                    else
                        ctxIdxInc = 13;
                else
                    ctxIdxInc = 6;
                break;

            case 53:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 14;
                    else
                        ctxIdxInc = 13;
                else
                    ctxIdxInc = 6;
                break;

            case 54:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 10;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 6;
                break;

            case 55:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 9;
                    else
                        ctxIdxInc = 9;
                else
                    ctxIdxInc = 6;
                break;

            case 56:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 11;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 7;
                break;

            case 57:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 12;
                    else
                        ctxIdxInc = 10;
                else
                    ctxIdxInc = 7;
                break;

            case 58:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 13;
                    else
                        ctxIdxInc = 14;
                else
                    ctxIdxInc = 7;
                break;

            case 59:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 11;
                    else
                        ctxIdxInc = 14;
                else
                    ctxIdxInc = 7;
                break;

            case 60:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 14;
                    else
                        ctxIdxInc = 14;
                else
                    ctxIdxInc = 8;
                break;

            case 61:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 10;
                    else
                        ctxIdxInc = 14;
                else
                    ctxIdxInc = 8;
                break;

            case 62:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 12;
                    else
                        ctxIdxInc = 14;
                else
                    ctxIdxInc = 8;
                break;

            case 63:
                if (mode == 1)
                    if (isFrame)
                        ctxIdxInc = 0;
                    else
                        ctxIdxInc = 0;
                else
                    ctxIdxInc = 0;
                break;
        }

        // END GENERATED CODE

        if (mode != 3)
            return ctxIdxInc;

        // Mode is 3 here

        if (binIdx == 0)
            return (numDecodAbsLevelGt1 != 0) ? 0 : Math.Min(4, 1 + numDecodAbsLevelEq1);
        else
            return 5 + Math.Min(4 - ((ctxBlockCat == 3) ? 1 : 0), numDecodAbsLevelGt1);
    }
}
