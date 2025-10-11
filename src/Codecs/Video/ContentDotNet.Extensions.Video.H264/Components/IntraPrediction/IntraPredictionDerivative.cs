namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.Common.Derivative;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Internal.AddressesAndBlockIndices;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.PredictionModes;

    /// <summary>
    ///   Derivative for intra prediction. Does not change the variables intra4x4PredMode and intra8x8PredMode,
    ///   it just returns the predicted modes.
    /// </summary>
    public static class IntraPredictionDerivative
    {
        public static int DeriveIntra4x4PredMode(
            H264State state,
            H264MacroblockInfo currMb,
            int[] intra4x4PredMode,
            int[] intra8x8PredMode,
            int luma4x4BlkIdx)
        {
            H264Derivative.DeriveNeighboring4x4LumaBlocks(
                state,
                luma4x4BlkIdx,
                out AddressAnd4x4LumaBlockIndex addrA,
                out AddressAnd4x4LumaBlockIndex addrB);

            bool dcPredModePredictedFlag =
                !addrA.AddressAndAvailability.Availability ||
                !addrB.AddressAndAvailability.Availability ||
                (addrA.AddressAndAvailability.Availability && state.MacroblockUtility.IsInter(addrA.AddressAndAvailability.Address) && state.H264RbspState?.PictureParameterSet?.ConstrainedIntraPredFlag == true) ||
                (addrA.AddressAndAvailability.Availability && state.MacroblockUtility.IsInter(addrA.AddressAndAvailability.Address) && state.H264RbspState?.PictureParameterSet?.ConstrainedIntraPredFlag == true);

            int intraMxMPredModeA = DeriveIntraMxmPredMode(addrA);
            int intraMxMPredModeB = DeriveIntraMxmPredMode(addrB);

            int predIntra4x4PredMode = Math.Min(intraMxMPredModeA, intraMxMPredModeB);

            if (currMb.Rbsp.MbPred!.PrevIntra4x4PredModeFlag![luma4x4BlkIdx])
                return predIntra4x4PredMode;
            else if (currMb.Rbsp.MbPred!.RemIntra4x4PredMode![luma4x4BlkIdx] < predIntra4x4PredMode)
                return (int)currMb.Rbsp.MbPred!.RemIntra4x4PredMode![luma4x4BlkIdx];
            else
                return (int)currMb.Rbsp.MbPred!.RemIntra4x4PredMode![luma4x4BlkIdx] + 1;

            int DeriveIntraMxmPredMode(AddressAnd4x4LumaBlockIndex addrN)
            {
                H264MacroblockInfo? mb = null;
                try
                {
                    mb = state.MacroblockUtility.GetMacroblock(addrN.AddressAndAvailability.Address);
                }
                catch { }

                if (mb is null) return 0;

                int mbppm = MacroblockTraits.MbPartPredMode(mb, 0);

                if (dcPredModePredictedFlag)
                {
                    if (mbppm is not (Intra_4x4 or Intra_8x8))
                    {
                        return 2;
                    }
                }
               
                if (mbppm == Intra_4x4) return intra4x4PredMode[addrN.Luma4x4BlkIdx.BlockIndex];
                else return intra8x8PredMode[addrN.Luma4x4BlkIdx.BlockIndex >> 2];
            }
        }

        public static int DeriveIntra8x8PredMode(
            H264State state,
            H264MacroblockInfo currMb,
            int[] intra4x4PredMode,
            int[] intra8x8PredMode,
            int luma8x8BlkIdx)
        {
            H264Derivative.DeriveNeighboring8x8LumaBlock(
                state,
                luma8x8BlkIdx,
                out AddressAnd8x8LumaBlockIndex addrA,
                out AddressAnd8x8LumaBlockIndex addrB);

            bool dcPredModePredictedFlag =
                !addrA.AddressAndAvailability.Availability ||
                !addrB.AddressAndAvailability.Availability ||
                (addrA.AddressAndAvailability.Availability && state.MacroblockUtility.IsInter(addrA.AddressAndAvailability.Address) && state.H264RbspState?.PictureParameterSet?.ConstrainedIntraPredFlag == true) ||
                (addrA.AddressAndAvailability.Availability && state.MacroblockUtility.IsInter(addrA.AddressAndAvailability.Address) && state.H264RbspState?.PictureParameterSet?.ConstrainedIntraPredFlag == true);

            int intraMxMPredModeA = DeriveIntraMxmPredMode(false, addrA);
            int intraMxMPredModeB = DeriveIntraMxmPredMode(true, addrB);

            int predIntra8x8PredMode = Math.Min(intraMxMPredModeA, intraMxMPredModeB);
            if (currMb.Rbsp.MbPred!.PrevIntra8x8PredModeFlag![luma8x8BlkIdx])
                return predIntra8x8PredMode;
            else
            if (currMb.Rbsp.MbPred!.RemIntra8x8PredMode![luma8x8BlkIdx] < predIntra8x8PredMode)
                return (int)currMb.Rbsp.MbPred!.RemIntra8x8PredMode![luma8x8BlkIdx];
            else
                return (int)currMb.Rbsp.MbPred!.RemIntra8x8PredMode![luma8x8BlkIdx] + 1;

            int DeriveIntraMxmPredMode(bool bFlag, AddressAnd8x8LumaBlockIndex addrN)
            {
                H264MacroblockInfo? mb = null;
                try
                {
                    mb = state.MacroblockUtility.GetMacroblock(addrN.AddressAndAvailability.Address);
                }
                catch { }

                if (mb is null) return 0;

                int mbppm = MacroblockTraits.MbPartPredMode(mb, 0);

                if (dcPredModePredictedFlag)
                {
                    if (mbppm is not (Intra_4x4 or Intra_8x8))
                    {
                        return 2;
                    }
                }

                if (mbppm == Intra_8x8) return intra8x8PredMode[addrN.Luma8x8BlkIdx.BlockIndex];
                else
                {
                    int n = !bFlag /* A */ ? (
                        state.DeriveMbaffFrameFlag() ||
                        state.MacroblockUtility.IsFrame(currMb) &&
                        !state.MacroblockUtility.IsFrame(mb) &&
                        luma8x8BlkIdx == 2
                        ? 3
                        : 1) : 2;

                    return intra4x4PredMode[addrN.Luma8x8BlkIdx.BlockIndex * 4 + n];
                }
            }
        }

        public static int DeriveIntra16x16PredMode(int mbType, bool transformSize8x8Flag) => MacroblockTraits.GetIntra16x16PredictionMode(mbType, transformSize8x8Flag);
    }
}
