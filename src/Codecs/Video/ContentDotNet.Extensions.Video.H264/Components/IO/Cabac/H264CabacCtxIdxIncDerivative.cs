namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.Common.Derivative;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.BlockTypes;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Extensions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Internal;
    using ContentDotNet.Extensions.Video.H264.Models.Internal.AddressesAndBlockIndices;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.MacroblockTypes;

    internal static class H264CabacCtxIdxIncDerivative
    {
        private static ReadOnlySpan<int> NoAdditionalInputCtxBlockCatForCodedBlockFlag => [0, 6, 10];
        private static ReadOnlySpan<int> CtxBlockCat12ForCodedBlockFlag => [1, 2];
        private static ReadOnlySpan<int> CtxBlockCat78ForCodedBlockFlag => [7, 8];
        private static ReadOnlySpan<int> CtxBlockCat1112ForCodedBlockFlag => [11, 12];

        public static int MbSkipFlag(H264State h264)
        {
            H264Derivative.DeriveNeighboringMacroblocks(
                h264,
                out AddressAndAvailability a,
                out AddressAndAvailability b);

            return GetCondTermFlag(a) + GetCondTermFlag(b);

            int GetCondTermFlag(AddressAndAvailability n)
            {
                if (!n.Availability)
                    return 0;

                return h264.MacroblockUtility.GetMacroblock(n.Address).MbSkipFlag ? 0 : 1;
            }
        }

        public static int MbFieldDecodingFlag(H264State h264)
        {
            H264Derivative.DeriveNeighboringMacroblocksAndTheirAvailabilityMbaff(
                h264,
                out AddressAndAvailability a,
                out AddressAndAvailability b,
                out _,
                out _);

            return GetCondTermFlag(a) + GetCondTermFlag(b);

            int GetCondTermFlag(AddressAndAvailability n)
            {
                if (!n.Availability)
                    return 0;

                return h264.MacroblockUtility.IsFrame(n.Address) ? 0 : 1;
            }
        }

        public static int MbType(H264State h264, int ctxIdxOffset)
        {
            H264Derivative.DeriveNeighboringMacroblocksAndTheirAvailability(
                h264,
                out AddressAndAvailability a,
                out AddressAndAvailability b,
                out _,
                out _);

            return GetCondTermFlag(a) + GetCondTermFlag(b);

            int GetCondTermFlag(AddressAndAvailability n)
            {
                if (!n.Availability)
                    return 0;

                var mb = h264.MacroblockUtility.GetMacroblock(n.Address);

                return ctxIdxOffset switch
                {
                    0 when mb == SI => 0,
                    3 when mb == I_NxN => 0,
                    27 when mb == B_Skip || mb == B_Direct_16x16 => 0,
                    _ => 1
                };
            }
        }

        public static int CodedBlockPattern(H264State h264, int ctxIdxOffset, int binIdx, BinHistory cbpBinHistory)
        {
            if (ctxIdxOffset == 73)
            {
                H264Derivative.DeriveNeighboring8x8LumaBlock(h264, binIdx, out var a, out var b);

                return GetCondTermFlag(a) + 2 * GetCondTermFlag(b);

                int GetCondTermFlag(AddressAnd8x8LumaBlockIndex idx)
                {
                    if (!idx.AddressAndAvailability.Availability) return 0;

                    H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(idx.AddressAndAvailability.Address);

                    if (mb == I_PCM) return 0;
                    if ((mb != P_Skip && mb != B_Skip) && idx.AddressAndAvailability.Address != h264.CurrMbAddr &&
                        ((mb.Rbsp.GetCodedBlockPatternLuma() >> idx.Luma8x8BlkIdx.BlockIndex) & 1) != 0) return 0;
                    if (!cbpBinHistory[idx.Luma8x8BlkIdx.BlockIndex]) return 0;

                    return 1;
                }
            }
            else
            {
                H264Derivative.DeriveNeighboringMacroblocks(h264, out var a, out var b);

                return GetCondTermFlag(a) + 2 * GetCondTermFlag(b) + ((binIdx == 1) ? 4 : 0);

                int GetCondTermFlag(AddressAndAvailability idx)
                {
                    if (idx.Availability && h264.MacroblockUtility.GetMacroblock(idx.Address) == I_PCM) return 1;

                    if (!idx.Availability) return 0;
                    H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(idx.Address);
                    if (mb == P_Skip || mb == B_Skip) return 0;
                    if (binIdx == 0 && mb.Rbsp.GetCodedBlockPatternChroma() == 0) return 0;
                    if (binIdx == 1 && mb.Rbsp.GetCodedBlockPatternChroma() == 2) return 0;

                    return 1;
                }
            }
        }

        public static int MbQpDelta(H264State h264, AddressAndAvailability prevMbAddr)
        {
            if (!prevMbAddr.Availability)
                return 0;
            
            var macroblock = h264.MacroblockUtility.GetMacroblock(prevMbAddr.Address);
            if (macroblock == P_Skip ||
                macroblock == B_Skip)
                return 0;

            if (macroblock == I_PCM)
                return 0;

            if (MacroblockTraits.MbPartPredMode(macroblock, 0) != PredictionModes.Intra_16x16 &&
                macroblock.Rbsp.GetCodedBlockPatternChroma() == 0 &&
                macroblock.Rbsp.GetCodedBlockPatternLuma() == 0)
                return 0;


            return 1;
        }

        public static int RefIdxLX(H264State h264, H264MacroblockInfo currMacroblock, H264ListType listType, List<uint> refIdxLX, List<uint> subMbType, int mbPartIdx)
        {
            int Pred_LX = listType == H264ListType.List0 ? PredictionModes.Pred_L0 : PredictionModes.Pred_L1;

            H264Derivative.DeriveNeighboringPartitions(currMacroblock, h264, mbPartIdx, (int)subMbType[mbPartIdx], 0,
                out AddressAndPartitionIndices a,
                out AddressAndPartitionIndices b,
                out _,
                out _);

            bool refIdxZeroFlagA = DeriveRefIdxZeroFlagN(a) == 1;
            bool refIdxZeroFlagB = DeriveRefIdxZeroFlagN(b) == 1;

            bool predModeEqualFlagA = DerivePredModeEqualFlag(a) == 1;
            bool predModeEqualFlagB = DerivePredModeEqualFlag(b) == 1;

            return DeriveCondTermFlag(a, refIdxZeroFlagA, predModeEqualFlagA) + 2
                * DeriveCondTermFlag(b, refIdxZeroFlagB, predModeEqualFlagB);

            int DeriveRefIdxZeroFlagN(AddressAndPartitionIndices addr)
            {
                if (!addr.Address.Availability) return 0;

                int refIdxZeroFlagN =
                    (h264.DeriveMbaffFrameFlag() && h264.MacroblockUtility.IsFrame(currMacroblock) && !h264.MacroblockUtility.IsFrame(addr.Address.Address))
                    ? ((refIdxLX[addr.Indices.MbPartIdx] > 1) ? 0 : 1)
                    : ((refIdxLX[addr.Indices.MbPartIdx] > 0) ? 0 : 1);
                return refIdxZeroFlagN;
            }

            int DerivePredModeEqualFlag(AddressAndPartitionIndices addr)
            {
                if (!addr.Address.Availability) return 0;

                H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(addr.Address.Address);

                int predModeEqualFlagN;
                if (mb == B_Direct_8x8 || mb == B_Skip)
                {
                    predModeEqualFlagN = 0;
                }
                else if (mb == B_8x8 || mb == P_8x8)
                {
                    int predMode = MacroblockTraits.SubMbPredMode(mb, (int)mb.Rbsp.SubMbPred!.SubMbType[addr.Indices.MbPartIdx]);
                    if (predMode != Pred_LX && predMode != PredictionModes.BiPred)
                    {
                        predModeEqualFlagN = 0;
                    }
                    else
                    {
                        predModeEqualFlagN = 1;
                    }
                }
                else
                {
                    int predMode = MacroblockTraits.MbPartPredMode(mb, addr.Indices.MbPartIdx);
                    if (predMode != Pred_LX && predMode != PredictionModes.BiPred)
                    {
                        predModeEqualFlagN = 0;
                    }
                    else
                    {
                        predModeEqualFlagN = 1;
                    }
                }

                return predModeEqualFlagN;
            }

            int DeriveCondTermFlag(AddressAndPartitionIndices avail, bool refIdxZeroFlag, bool predModeEqualFlag)
            {
                if (!avail.Address.Availability) return 0;

                H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(avail.Address.Address);

                if (mb == P_Skip || mb == B_Skip)
                    return 0;

                if (h264.MacroblockUtility.IsIntra(mb))
                    return 0;

                if (!predModeEqualFlag || refIdxZeroFlag) return 0;

                return 1;
            }
        }

        public static int Mvd(H264State h264, H264MacroblockInfo currMacroblock, H264ListType listType, int mbPartIdx, int ctxIdxOffset, H264MotionVectorDifference mvdLX, List<uint> subMbType)
        {
            int Pred_LX = listType == H264ListType.List0 ? PredictionModes.Pred_L0 : PredictionModes.Pred_L1;

            H264Derivative.DeriveNeighboringPartitions(currMacroblock, h264, mbPartIdx, (int)subMbType[mbPartIdx], 0,
                out AddressAndPartitionIndices a,
                out AddressAndPartitionIndices b,
                out _,
                out _);

            int compIdx = ctxIdxOffset == 40 ? 0 : 1;

            int predModeEqualFlagA = GetPredModeEqualFlag(a);
            int predModeEqualFlagB = GetPredModeEqualFlag(b);

            int absMvdCompA = GetAbsMvdComp(predModeEqualFlagA == 1, a);
            int absMvdCompB = GetAbsMvdComp(predModeEqualFlagB == 1, b);

            // From the ITU-T spec:
            //    If absMvdCompA is greater than 32 or absMvdCompA is greater than 32, ctxIdxInc is set equal to 2. 
            // I assume they meant
            //    If absMvdCompA is greater than 32 or absMvdCompB is greater than 32, ctxIdxInc is set equal to 2.
            // (I changed the variable name in the second condition above accordingly.)

            if (absMvdCompA > 32 || absMvdCompB > 32)
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

            int GetPredModeEqualFlag(AddressAndPartitionIndices addr)
            {
                if (!addr.Address.Availability) return 0;

                H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(addr.Address.Address);

                if (mb == B_Direct_16x16 || mb == B_Skip) return 0;
                else if (mb == P_8x8 || mb == B_8x8)
                {
                    int predMode = MacroblockTraits.SubMbPredMode(mb, (int)mb.Rbsp.SubMbPred!.SubMbType[addr.Indices.MbPartIdx]);
                    if (predMode != Pred_LX && predMode != PredictionModes.BiPred) return 0;
                    return 1;
                }
                else
                {
                    int predMode = MacroblockTraits.MbPartPredMode(mb, addr.Indices.MbPartIdx);
                    if (predMode != Pred_LX && predMode != PredictionModes.BiPred) return 0;
                    return 1;
                }
            }

            int GetAbsMvdComp(bool predModeEqualFlag, AddressAndPartitionIndices addr)
            {
                if (!addr.Address.Availability) return 0;
                H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(addr.Address.Address);
                if (mb == P_Skip || mb == B_Skip) return 0;
                if (h264.MacroblockUtility.IsIntra(mb)) return 0;
                if (!predModeEqualFlag) return 0;

                if (compIdx == 1 && h264.DeriveMbaffFrameFlag() &&
                    h264.MacroblockUtility.IsFrame(currMacroblock) &&
                    !h264.MacroblockUtility.IsFrame(mb))
                    return Math.Abs((int)mvdLX[addr.Indices.MbPartIdx, addr.Indices.SubMbPartIdx, compIdx]) * 2;
                else if (compIdx == 1 && h264.DeriveMbaffFrameFlag() &&
                         !h264.MacroblockUtility.IsFrame(currMacroblock) &&
                         h264.MacroblockUtility.IsFrame(mb))
                    return Math.Abs((int)mvdLX[addr.Indices.MbPartIdx, addr.Indices.SubMbPartIdx, compIdx]) / 2;
                else
                    return Math.Abs((int)mvdLX[addr.Indices.MbPartIdx, addr.Indices.SubMbPartIdx, compIdx]);
            }
        }

        public static int IntraChromaPredMode(H264State h264)
        {
            H264Derivative.DeriveNeighboringMacroblocks(h264, out var a, out var b);

            return GetCondTermFlag(a) + GetCondTermFlag(b);

            int GetCondTermFlag(AddressAndAvailability n)
            {
                if (!n.Availability)
                    return 0;
                var mb = h264.MacroblockUtility.GetMacroblock(n.Address);
                if (mb == I_PCM)
                    return 1;
                if (!h264.MacroblockUtility.IsInter(mb))
                    return 0;
                int mode = (int?)mb.Rbsp.MbPred?.IntraChromaPredMode ?? 0;
                return mode == 0 ? 0 : 1;
            }
        }

        // ye this is complex as hell :D
        // NOTE: Make the cbfOptions parameter 'in' because it's an unmanaged struct and we want to avoid copying it unnecessarily.
        //       Can result in minor performance improvements.
        public static int CodedBlockFlag(H264State h264, H264MacroblockInfo currMb, int ctxBlockCat, in CodedBlockFlagDerivationOptions cbfOptions)
        {
            ResidualBlockBase? transBlockA;
            ResidualBlockBase? transBlockB;

            AddressAndAvailability mbAddrA;
            AddressAndAvailability mbAddrB;

            DeriveTransBlocks(in cbfOptions);

            return GetCondTermFlag(transBlockA, mbAddrA) + 2 * GetCondTermFlag(transBlockB, mbAddrB);

            int GetCondTermFlag(ResidualBlockBase? transBlockN, AddressAndAvailability mbAddrN)
            {
                if (!mbAddrN.Availability && h264.MacroblockUtility.IsInter(currMb)) return 0;
                if (mbAddrN.Availability && transBlockN == null && h264.MacroblockUtility.GetMacroblock(mbAddrN.Address) != I_PCM) return 0;
                if (h264.MacroblockUtility.IsIntra(currMb) && h264.H264RbspState!.PictureParameterSet!.ConstrainedIntraPredFlag &&
                    mbAddrN.Availability && h264.MacroblockUtility.IsInter(mbAddrN.Address) &&
                    h264.H264RbspState.NalUnit!.NalUnitType is >= 2 and <= 4) return 0;

                if (!mbAddrN.Availability && h264.MacroblockUtility.IsIntra(currMb)) return 1;
                if (h264.MacroblockUtility.GetMacroblock(mbAddrN.Address) == I_PCM) return 1;

                return transBlockN?.RbspResidual?.CodedBlockFlag == true ? 1 : 0;
            }

            void DeriveTransBlocks(in CodedBlockFlagDerivationOptions options)
            {
                if (NoAdditionalInputCtxBlockCatForCodedBlockFlag.Contains(ctxBlockCat))
                {
                    H264Derivative.DeriveNeighboringMacroblocks(
                        h264,
                        out AddressAndAvailability a, out AddressAndAvailability b);

                    mbAddrA = a;
                    mbAddrB = b;

                    transBlockA = GetTransBlock(a);
                    transBlockB = GetTransBlock(b);

                    ResidualBlockBase? GetTransBlock(AddressAndAvailability n)
                    {
                        if (!n.Availability) return null;
                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.Address);
                        
                        if (mb.Rbsp.Residual == null) return null;

                        if (MacroblockTraits.MbPartPredMode(mb, 0) == PredictionModes.Intra_16x16)
                        {
                            if (ctxBlockCat == 0) return new IntraDcResidualBlock(mb.Rbsp.Residual);
                            else if (ctxBlockCat == 6) return new CbDcResidualBlock(mb.Rbsp.Residual);
                            else return new CrDcResidualBlock(mb.Rbsp.Residual);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else if (CtxBlockCat12ForCodedBlockFlag.Contains(ctxBlockCat))
                {
                    H264Derivative.DeriveNeighboring4x4LumaBlocks(h264, options.luma8x8BlkIdx,
                        out AddressAnd4x4LumaBlockIndex addrA,
                        out AddressAnd4x4LumaBlockIndex addrB);

                    mbAddrA = addrA.AddressAndAvailability;
                    mbAddrB = addrB.AddressAndAvailability;

                    transBlockA = GetTransBlock(addrA);
                    transBlockB = GetTransBlock(addrB);

                    ResidualBlockBase? GetTransBlock(AddressAnd4x4LumaBlockIndex n)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;
                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);
                        if (mb.Rbsp.Residual == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> (n.Luma4x4BlkIdx.BlockIndex >> 2)) & 1) != 0 &&
                            !mb.Rbsp.TransformSize8x8Flag)
                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.LumaLevel4x4, n.Luma4x4BlkIdx.BlockIndex);
                        else if (!(mb == P_Skip || mb == B_Skip) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> (n.Luma4x4BlkIdx.BlockIndex >> 2)) & 1) != 0 &&
                            mb.Rbsp.TransformSize8x8Flag)
                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.LumaLevel8x8, n.Luma4x4BlkIdx.BlockIndex >> 2);
                        else
                            return null;
                    }
                }
                else if (ctxBlockCat == 3)
                {
                    H264Derivative.DeriveNeighboringMacroblocks(
                        h264,
                        out AddressAndAvailability a, out AddressAndAvailability b);

                    mbAddrA = a;
                    mbAddrB = b;

                    transBlockA = GetTransBlock(a, in options);
                    transBlockB = GetTransBlock(b, in options);

                    ResidualBlockBase? GetTransBlock(AddressAndAvailability n, in CodedBlockFlagDerivationOptions options)
                    {
                        if (!n.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.Address);

                        if (mb.Rbsp.Residual == null) return null;
                        if (mb.Rbsp.Residual.ChromaDCLevel == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            (mb.Rbsp.GetCodedBlockPatternChroma() != 0))
                            return new ResidualBlock1D(mb.Rbsp.Residual, mb.Rbsp.Residual.GetChroma16x16Dc(options.iCbCr));

                        return null;
                    }
                }
                else if (ctxBlockCat == 4)
                {
                    H264Derivative.DeriveNeighboring4x4ChromaBlocks(
                        h264,
                        options.chroma4x4BlkIdx,
                        out AddressAnd4x4ChromaBlockIndex a,
                        out AddressAnd4x4ChromaBlockIndex b);

                    mbAddrA = a.AddressAndAvailability;
                    mbAddrB = b.AddressAndAvailability;

                    transBlockA = GetTransBlock(a, in options);
                    transBlockB = GetTransBlock(b, in options);

                    ResidualBlockBase? GetTransBlock(AddressAnd4x4ChromaBlockIndex n, in CodedBlockFlagDerivationOptions options)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);

                        if (mb.Rbsp.Residual == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            (mb.Rbsp.GetCodedBlockPatternChroma() == 2))
                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.GetChromaLevel4x4(options.iCbCr), n.Chroma4x4BlkIdx.BlockIndex);

                        return null;
                    }
                }
                else if (ctxBlockCat == 5)
                {
                    H264Derivative.DeriveNeighboring8x8LumaBlock(
                        h264,
                        options.luma8x8BlkIdx,
                        out var a,
                        out var b);

                    mbAddrA = a.AddressAndAvailability;
                    mbAddrB = b.AddressAndAvailability;

                    transBlockA = GetTransBlock(a);
                    transBlockB = GetTransBlock(b);

                    ResidualBlockBase? GetTransBlock(AddressAnd8x8LumaBlockIndex n)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);

                        if (mb.Rbsp.Residual == null) return null;
                        if (mb.Rbsp.Residual.LumaLevel8x8 == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            ((mb.Rbsp.GetCodedBlockPatternChroma() >> n.Luma8x8BlkIdx.BlockIndex) & 1) != 0 &&
                            mb.Rbsp.TransformSize8x8Flag)
                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.LumaLevel8x8, n.Luma8x8BlkIdx.BlockIndex);

                        return null;
                    }
                }
                else if (CtxBlockCat78ForCodedBlockFlag.Contains(ctxBlockCat))
                {
                    H264Derivative.DeriveNeighboring4x4ChromaBlocks(h264, options.chroma4x4BlkIdx,
                        out AddressAnd4x4ChromaBlockIndex addrA,
                        out AddressAnd4x4ChromaBlockIndex addrB);

                    mbAddrA = addrA.AddressAndAvailability;
                    mbAddrB = addrB.AddressAndAvailability;

                    transBlockA = GetTransBlock(addrA);
                    transBlockB = GetTransBlock(addrB);

                    ResidualBlockBase? GetTransBlock(AddressAnd4x4ChromaBlockIndex n)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);

                        if (mb.Rbsp.Residual == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> (n.Chroma4x4BlkIdx.BlockIndex >> 2)) & 1) != 0 &&
                            !mb.Rbsp.TransformSize8x8Flag)
                        {
                            if (mb.Rbsp.Residual.CbLevel4x4 == null) return null;

                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.CbLevel4x4, n.Chroma4x4BlkIdx.BlockIndex);
                        }
                        else if (!(mb == P_Skip || mb == B_Skip) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> (n.Chroma4x4BlkIdx.BlockIndex >> 2)) & 1) != 0 &&
                            mb.Rbsp.TransformSize8x8Flag)
                        {
                            if (mb.Rbsp.Residual.CbLevel8x8 == null) return null;

                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.CbLevel8x8, n.Chroma4x4BlkIdx.BlockIndex);
                        }

                        return null;
                    }
                }
                else if (ctxBlockCat == 9)
                {
                    H264Derivative.DeriveNeighboring8x8ChromaBlocksCat3(
                        h264,
                        options.cb8x8BlkIdx,
                        out var addrA,
                        out var addrB);

                    mbAddrA = addrA.AddressAndAvailability;
                    mbAddrB = addrB.AddressAndAvailability;

                    transBlockA = GetTransBlock(addrA);
                    transBlockB = GetTransBlock(addrB);

                    ResidualBlockBase? GetTransBlock(AddressAnd8x8ChromaBlockIndex n)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);

                        if (mb.Rbsp.Residual == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> n.Chroma8x8BlkIdx.BlockIndex) & 1) != 0 &&
                            mb.Rbsp.TransformSize8x8Flag)
                        {
                            if (mb.Rbsp.Residual.CbLevel8x8 == null) return null;

                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.CbLevel8x8, n.Chroma8x8BlkIdx.BlockIndex);
                        }
                        

                        return null;
                    }
                }
                else if (CtxBlockCat1112ForCodedBlockFlag.Contains(ctxBlockCat))
                {
                    H264Derivative.DeriveNeighboring4x4ChromaBlocks(
                        h264,
                        options.cr4x4BlkIdx,
                        out var addrA,
                        out var addrB);

                    mbAddrA = addrA.AddressAndAvailability;
                    mbAddrB = addrB.AddressAndAvailability;

                    transBlockA = GetTransBlock(addrA, in options);
                    transBlockB = GetTransBlock(addrB, in options);

                    ResidualBlockBase? GetTransBlock(AddressAnd4x4ChromaBlockIndex n, in CodedBlockFlagDerivationOptions options)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);

                        if (mb.Rbsp.Residual == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> (options.cr4x4BlkIdx >> 2)) & 1) != 0 &&
                            !mb.Rbsp.TransformSize8x8Flag)
                        {
                            if (mb.Rbsp.Residual.CrLevel4x4 == null) return null;

                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.CrLevel4x4, n.Chroma4x4BlkIdx.BlockIndex);
                        }
                        else if (!(mb == P_Skip || mb == B_Skip) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> (options.cr4x4BlkIdx >> 2)) & 1) != 0 &&
                            mb.Rbsp.TransformSize8x8Flag)
                        {
                            if (mb.Rbsp.Residual.CrLevel8x8 == null) return null;

                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.CrLevel8x8, n.Chroma4x4BlkIdx.BlockIndex >> 2);
                        }

                        return null;
                    }
                }
                else
                {
                    H264Derivative.DeriveNeighboring8x8ChromaBlocksCat3(
                        h264,
                        options.cr8x8BlkIdx,
                        out var addrA,
                        out var addrB);

                    mbAddrA = addrA.AddressAndAvailability;
                    mbAddrB = addrB.AddressAndAvailability;

                    transBlockA = GetTransBlock(addrA);
                    transBlockB = GetTransBlock(addrB);

                    ResidualBlockBase? GetTransBlock(AddressAnd8x8ChromaBlockIndex n)
                    {
                        if (!n.AddressAndAvailability.Availability) return null;

                        H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(n.AddressAndAvailability.Address);

                        if (mb.Rbsp.Residual == null) return null;

                        if (!(mb == P_Skip || mb == B_Skip || mb == I_PCM) &&
                            ((mb.Rbsp.GetCodedBlockPatternLuma() >> n.Chroma8x8BlkIdx.BlockIndex) & 1) != 0 &&
                            mb.Rbsp.TransformSize8x8Flag)
                        {
                            if (mb.Rbsp.Residual.CbLevel8x8 == null) return null;

                            return new Indexed2DTo1DResidualBlock(mb.Rbsp.Residual, mb.Rbsp.Residual.CrLevel8x8, n.Chroma8x8BlkIdx.BlockIndex);
                        }


                        return null;
                    }
                }
            }
        }

        public static int TransformSize8x8Flag(H264State h264)
        {
            H264Derivative.DeriveNeighboringMacroblocks(
                h264,
                out var a,
                out var b);

            return GetCondTermFlag(a) + GetCondTermFlag(b);

            int GetCondTermFlag(AddressAndAvailability address)
            {
                if (!address.Availability) return 0;

                H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(address.Address);
                if (!mb.Rbsp.TransformSize8x8Flag) return 0;

                return 1;
            }
        }
    }
}
