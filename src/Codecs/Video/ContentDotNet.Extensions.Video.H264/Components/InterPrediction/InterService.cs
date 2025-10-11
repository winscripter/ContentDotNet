namespace ContentDotNet.Extensions.Video.H264.Components.InterPrediction
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.Common.Derivative;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures;
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Internal;
    using ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks;
    using ContentDotNet.Extensions.Video.H264.Models.Weights;
    using ContentDotNet.Extensions.Video.H264.Utilities;
    using ContentDotNet.Pictures;
    using ContentDotNet.Primitives;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.MacroblockTypes;
    using static ContentDotNet.Extensions.Video.H264.Components.Common.PredictionModes;

    internal struct DeriveMotionDataOfNeighboringPartitionsResult
    {
        public AddressAndPartitionIndices A;
        public AddressAndPartitionIndices B;
        public AddressAndPartitionIndices C;
        public H264MotionVector MvL0A;
        public H264MotionVector MvL0B;
        public H264MotionVector MvL0C;
        public int RefIdxL0A;
        public int RefIdxL0B;
        public int RefIdxL0C;
        public H264MotionVector MvL1A;
        public H264MotionVector MvL1B;
        public H264MotionVector MvL1C;
        public int RefIdxL1A;
        public int RefIdxL1B;
        public int RefIdxL1C;

        public readonly AddressAndAvailability MbAddrA => A.Address;
        public readonly AddressAndAvailability MbAddrB => B.Address;
        public readonly AddressAndAvailability MbAddrC => C.Address;

        public readonly int MbPartIdxA => A.Indices.MbPartIdx;
        public readonly int MbPartIdxB => B.Indices.MbPartIdx;
        public readonly int MbPartIdxC => C.Indices.MbPartIdx;

        public readonly int SubMbPartIdxA => A.Indices.SubMbPartIdx;
        public readonly int SubMbPartIdxB => B.Indices.SubMbPartIdx;
        public readonly int SubMbPartIdxC => C.Indices.SubMbPartIdx;
    }

    internal enum VerticalMotionVectorScale
    {
        OneToOne,
        FrameToField,
        FieldToFrame
    }

    internal enum PictureCodingStructure
    {
        FLD,
        FRM,
        AFRM
    }

    internal delegate int ClipDelegate(int source);
    internal delegate ClipDelegate ClipDelegateFactory(int bitDepth);

    internal class InterService
    {
        private static readonly ClipDelegateFactory CdfL = (bitDepth) => (source) => Clipping.Clip1Y(source, bitDepth);
        private static readonly ClipDelegateFactory CdfC = (bitDepth) => (source) => Clipping.Clip1C(source, bitDepth);

        private static readonly H264MotionVector MotionVector0 = new(0, 0);

        public H264MacroblockInfo? CurrentMacroblock { get; set; }
        public H264State? H264State { get; set; }
        public IDecodedPictureBuffer? RefPicList0 { get; set; }
        public IDecodedPictureBuffer? RefPicList1 { get; set; }
        public PictureDescriptor? CurrentPicture { get; set; }
        public ISliceDecoder? SliceDecoder { get; set; }

        private void DeriveLumaMvForSkippedMacroblocksInPSpSlices(out H264MotionVector mvL0, out int refIdxL0)
        {
            refIdxL0 = 0;
            DeriveMotionDataOfNeighboringPartitions(0, 0, na, 0, out var data);

            if (!data.A.Address.Availability || !data.B.Address.Availability ||
                (data.RefIdxL0A == 0 && data.MvL0A == MotionVector0) ||
                (data.RefIdxL0B == 0 && data.MvL0B == MotionVector0))
            {
                mvL0 = MotionVector0;
            }
            else
            {
                DeriveLumaMvPrediction(0, 0, refIdxL0, 0, na, out mvL0);
            }
        }

        private static H264MotionVector AsMV(List<int> list) => new(list[0], list[1]);

        private void DeriveMotionDataOfNeighboringPartitions(int mbPartIdx, int subMbPartIdx, int currSubMbType, int listSuffixFlag,
            out DeriveMotionDataOfNeighboringPartitionsResult result)
        {
            result = new();

            H264Derivative.DeriveNeighboringPartitions(
                CurrentMacroblock!,
                H264State!,
                mbPartIdx, subMbPartIdx, currSubMbType,
                out result.A,
                out result.B,
                out result.C,
                out AddressAndPartitionIndices d);

            if (!result.C.Address.Availability)
            {
                result.C = d;
            }

            Derive(result.A, ref listSuffixFlag == 1 ? ref result.MvL1A : ref result.MvL0A, ref listSuffixFlag == 1 ? ref result.RefIdxL1A : ref result.RefIdxL0A);
            Derive(result.B, ref listSuffixFlag == 1 ? ref result.MvL1B : ref result.MvL0B, ref listSuffixFlag == 1 ? ref result.RefIdxL1B : ref result.RefIdxL0B);
            Derive(result.C, ref listSuffixFlag == 1 ? ref result.MvL1C : ref result.MvL0C, ref listSuffixFlag == 1 ? ref result.RefIdxL1C : ref result.RefIdxL0C);

            void Derive(AddressAndPartitionIndices N, ref H264MotionVector mvLXN, ref int refIdxLXN)
            {
                if (!N.Address.Availability)
                {
                    mvLXN = MotionVector0;
                    refIdxLXN = -1;
                    return;
                }

                H264MacroblockInfo mb = H264State!.MacroblockUtility.GetMacroblock(N.Address.Address);

                if (H264State.MacroblockUtility.IsIntra(mb))
                {
                    mvLXN = MotionVector0;
                    refIdxLXN = -1;
                    return;
                }

                mvLXN = listSuffixFlag == 1 ? AsMV(CurrentMacroblock!.Rbsp!.SubMbPred!.MvdL1![mbPartIdx][subMbPartIdx]) : AsMV(CurrentMacroblock!.Rbsp!.SubMbPred!.MvdL0![mbPartIdx][subMbPartIdx]);
                refIdxLXN = (int)(listSuffixFlag == 1 ? CurrentMacroblock.Rbsp.SubMbPred.RefIdxL1![mbPartIdx] : CurrentMacroblock.Rbsp.SubMbPred.RefIdxL0![mbPartIdx]);

                if (!H264State!.MacroblockUtility.IsFrame(CurrentMacroblock) && H264State.MacroblockUtility.IsFrame(mb))
                {
                    mvLXN.Y /= 2;
                    refIdxLXN *= 2;
                }
                else if (H264State!.MacroblockUtility.IsFrame(CurrentMacroblock) && !H264State.MacroblockUtility.IsFrame(mb))
                {
                    mvLXN.Y *= 2;
                    refIdxLXN /= 2;
                }
            }
        }

        private void DeriveLumaMvPrediction(int mbPartIdx, int subMbPartIdx, int refIdxLX, int listSuffixFlag, int currSubMbType, out H264MotionVector mvpLX)
        {
            DeriveMotionDataOfNeighboringPartitions(mbPartIdx, subMbPartIdx, currSubMbType, listSuffixFlag,
                out DeriveMotionDataOfNeighboringPartitionsResult result);

            int MbPartWidth = MacroblockTraits.MbPartWidth(CurrentMacroblock!);
            int MbPartHeight = MacroblockTraits.MbPartHeight(CurrentMacroblock!);

            if (MbPartWidth == 16 && MbPartHeight == 8 && mbPartIdx == 0 &&
                (listSuffixFlag == 0 ? result.RefIdxL0B : result.RefIdxL1B) == refIdxLX)
            {
                mvpLX = listSuffixFlag == 1 ? result.MvL0B : result.MvL1B;
            }
            else if (MbPartWidth == 16 && MbPartHeight == 8 && mbPartIdx == 1 &&
                (listSuffixFlag == 0 ? result.RefIdxL0A : result.RefIdxL1A) == refIdxLX)
            {
                mvpLX = listSuffixFlag == 1 ? result.MvL0A : result.MvL1A;
            }
            else if (MbPartWidth == 8 && MbPartHeight == 16 && mbPartIdx == 0 &&
                (listSuffixFlag == 0 ? result.RefIdxL0A : result.RefIdxL1A) == refIdxLX)
            {
                mvpLX = listSuffixFlag == 1 ? result.MvL0A : result.MvL1A;
            }
            else if (MbPartWidth == 8 && MbPartHeight == 16 && mbPartIdx == 1 &&
                (listSuffixFlag == 0 ? result.RefIdxL0C : result.RefIdxL1C) == refIdxLX)
            {
                mvpLX = listSuffixFlag == 1 ? result.MvL0C : result.MvL1C;
            }
            else
            {
                DeriveMedianLumaMvs(result.A, result.B, result.C,
                    listSuffixFlag == 1 ? result.MvL1A : result.MvL0A,
                    listSuffixFlag == 1 ? result.MvL1B : result.MvL0B,
                    listSuffixFlag == 1 ? result.MvL1C : result.MvL0C,
                    listSuffixFlag == 1 ? result.RefIdxL1A : result.RefIdxL0A,
                    listSuffixFlag == 1 ? result.RefIdxL1B : result.RefIdxL0B,
                    listSuffixFlag == 1 ? result.RefIdxL1C : result.RefIdxL0C,
                    refIdxLX,
                    out mvpLX);
            }
        }

        private static void DeriveMedianLumaMvs(
            AddressAndPartitionIndices a,
            AddressAndPartitionIndices b,
            AddressAndPartitionIndices c,
            H264MotionVector mvLXA,
            H264MotionVector mvLXB,
            H264MotionVector mvLXC,
            int refIdxLXA,
            int refIdxLXB,
            int refIdxLXC,
            int currPartRefIdxLX,
            out H264MotionVector mvpLX)
        {
            if (a.Address.Availability && !b.Address.Availability && !c.Address.Availability)
            {
                mvLXB = mvLXA;
                mvLXC = mvLXA;
                refIdxLXB = refIdxLXA;
                refIdxLXC = refIdxLXB;
            }

            if ((refIdxLXA == currPartRefIdxLX) ^ (refIdxLXB == currPartRefIdxLX) ^ (refIdxLXC == currPartRefIdxLX))
            {
                H264MotionVector mv = refIdxLXA == currPartRefIdxLX
                                    ? mvLXA : refIdxLXB == currPartRefIdxLX
                                    ? mvLXB : mvLXC;

                mvpLX = mv;
            }
            else
            {
                int x = CommonFunctions.Median(mvLXA.X, mvLXB.X, mvLXC.X);
                int y = CommonFunctions.Median(mvLXA.Y, mvLXB.Y, mvLXC.Y);

                mvpLX = new(x, y);
            }
        }

        public void DeriveLumaMvsForBSlices(int mbPartIdx, int subMbPartIdx,
            out int refIdxL0, out int refIdxL1, out H264MotionVector mvL0, out H264MotionVector mvL1,
            out int subMvCnt, out bool predFlagL0, out bool predFlagL1)
        {
            bool useSpatialPrediction = H264State?.H264RbspState?.SliceHeader?.DirectSpatialMvPredFlag == true;

            if (useSpatialPrediction)
            {
                DeriveSpatialDirectLumaMvs(mbPartIdx, subMbPartIdx, out refIdxL0, out refIdxL1, out mvL0, out mvL1, out subMvCnt, out predFlagL0, out predFlagL1);
            }
            else
            {
                DeriveTemporalDirectLumaMvs(mbPartIdx, subMbPartIdx, out refIdxL0, out refIdxL1, out mvL0, out mvL1, out predFlagL0, out predFlagL1);
                subMvCnt = subMbPartIdx == 0 ? 2 : 0;
            }
        }

        public void DeriveSpatialDirectLumaMvs(int mbPartIdx, int subMbPartIdx,
            out int refIdxL0, out int refIdxL1, out H264MotionVector mvL0, out H264MotionVector mvL1,
            out int subMvCnt, out bool predFlagL0, out bool predFlagL1)
        {
            int currSubMbType = (int?)CurrentMacroblock?.Rbsp.SubMbPred?.SubMbType?[mbPartIdx] ?? 0;

            DeriveMotionDataOfNeighboringPartitions(mbPartIdx, subMbPartIdx, currSubMbType, 0,
                out DeriveMotionDataOfNeighboringPartitionsResult motionDataL0);

            refIdxL0 = MinPositive(motionDataL0.RefIdxL0A, MinPositive(motionDataL0.RefIdxL0B, motionDataL0.RefIdxL0C));
            refIdxL1 = MinPositive(motionDataL0.RefIdxL1A, MinPositive(motionDataL0.RefIdxL1B, motionDataL0.RefIdxL1C));
            int directZeroPredictionFlag = 0;

            if (refIdxL0 < 0 && refIdxL1 < 0)
            {
                refIdxL0 = 0;
                refIdxL1 = 0;
                directZeroPredictionFlag = 1;
            }

            DeriveCoLocated4x4SubMacroblockPartitions(mbPartIdx, subMbPartIdx, out _, out _, out var mvCol, out var refIdxCol, out _);

            int colZeroFlag = (RefPicList1![0].Duration == PictureDuration.ShortTerm && refIdxCol == 0 && mvCol.X is -1 or 0 or 1 && mvCol.Y is -1 or 0 or 1).AsInt32();

            if (directZeroPredictionFlag == 1 || refIdxL0 < 0 || (refIdxL0 == 0 && colZeroFlag == 1))
            {
                mvL0 = MotionVector0;
            }
            else
            {
                DeriveLumaMvPrediction(mbPartIdx, subMbPartIdx, refIdxL0, 0, currSubMbType, out mvL0);
            }

            if (directZeroPredictionFlag == 1 || refIdxL1 < 0 || (refIdxL1 == 0 && colZeroFlag == 1))
            {
                mvL1 = MotionVector0;
            }
            else
            {
                DeriveLumaMvPrediction(mbPartIdx, subMbPartIdx, refIdxL1, 1, currSubMbType, out mvL1);
            }

            if (refIdxL0 >= 0 && refIdxL1 >= 0)
            {
                predFlagL0 = true;
                predFlagL1 = true;
            }
            else if (refIdxL0 >= 0 && refIdxL1 < 0)
            {
                predFlagL0 = true;
                predFlagL1 = false;
            }
            else if (refIdxL0 < 0 && refIdxL1 >= 0)
            {
                predFlagL0 = false;
                predFlagL1 = true;
            }
            else
            {
                predFlagL0 = false;
                predFlagL1 = true;
            }

            subMvCnt = subMbPartIdx != 0 ? 0 : (predFlagL0.AsInt32() + predFlagL1.AsInt32());

            static int MinPositive(int x, int y)
            {
                if (x >= 0 && y >= 0) return Math.Min(x, y);
                else return Math.Max(x, y);
            }
        }

        public void DeriveCoLocated4x4SubMacroblockPartitions(
            int mbPartIdx, int subMbPartIdx,
            out PictureDescriptor colPic, out AddressAndAvailability mbAddrCol, out H264MotionVector mvCol, out int refIdxCol, out VerticalMotionVectorScale vertMvScale)
        {
            PictureDescriptor? firstRefPicL1Top = null;
            PictureDescriptor? firstRefPicL1Bottom = null;
            int topAbsDiffPOC = 0;
            int bottomAbsDiffPOC = 0;
            if (RefPicList1![0].Picture is ComplementaryFieldPair cfp)
            {
                firstRefPicL1Top = cfp.Top;
                firstRefPicL1Bottom = cfp.Bottom;

                topAbsDiffPOC = Math.Abs(SliceDecoder!.DiffPicOrderCnt(firstRefPicL1Top.Poc.PictureOrderCount, CurrentPicture!.Poc.PictureOrderCount));
                bottomAbsDiffPOC = Math.Abs(SliceDecoder!.DiffPicOrderCnt(firstRefPicL1Bottom.Poc.PictureOrderCount, CurrentPicture!.Poc.PictureOrderCount));
            }


            colPic = null!;
            if (H264State?.H264RbspState?.SliceHeader?.FieldPicFlag == true)
            {
                bool aFieldOfDecodedFrame = IsFieldOfDecodedFrame(RefPicList1![0], RefPicList1);
                if (aFieldOfDecodedFrame)
                {
                    colPic = GetFrameOwner(RefPicList1![0], RefPicList1);
                }
                else if (IsDecodedField(RefPicList1![0]))
                {
                    colPic = RefPicList1![0];
                }
                else
                {
                    // TODO: Meaningful exception???
                    throw new InvalidOperationException();
                }
            }
            else
            {
                if (IsDecodedFrame(RefPicList1![0]))
                {
                    colPic = RefPicList1![0];
                }
                else if (IsComplementaryFieldPair(RefPicList1![0]))
                {
                    if (CurrentMacroblock?.MbFieldDecodingFlag == false)
                    {
                        if (topAbsDiffPOC < bottomAbsDiffPOC)
                        {
                            colPic = firstRefPicL1Top!;
                        }
                        else
                        {
                            colPic = firstRefPicL1Bottom!;
                        }
                    }
                    else
                    {
                        if ((H264State?.CurrMbAddr & 1) == 0)
                        {
                            colPic = firstRefPicL1Top!;
                        }
                        else
                        {
                            colPic = firstRefPicL1Bottom!;
                        }
                    }
                }
            }

            int luma4x4BlkIdx = H264State?.H264RbspState?.SequenceParameterSetData?.Direct8x8InferenceFlag == true ? (4 * mbPartIdx + subMbPartIdx) : (5 * mbPartIdx);

            XY xy = H264Derivative.Inverse4x4LumaBlockScan(luma4x4BlkIdx);
            int xCol = xy.X;
            int yCol = xy.Y;

            int PicWidthInMbs = H264State?.DerivePicWidthInMbs() ?? 0;
            int CurrMbAddr = H264State?.CurrMbAddr ?? 0;
            int bottom_field_flag = (H264State?.H264RbspState?.SliceHeader?.BottomFieldFlag == true).AsInt32();

            int mbAddrCol1 = 2 * PicWidthInMbs * (CurrMbAddr / PicWidthInMbs) + (CurrMbAddr % PicWidthInMbs) + PicWidthInMbs * (yCol / 8);
            int mbAddrCol2 = 2 * CurrMbAddr + (yCol / 8);
            int mbAddrCol3 = 2 * CurrMbAddr + bottom_field_flag;
            int mbAddrCol4 = PicWidthInMbs * (CurrMbAddr / (2 * PicWidthInMbs)) + (CurrMbAddr % PicWidthInMbs);
            int mbAddrCol5 = CurrMbAddr / 2;
            int mbAddrCol6 = 2 * (CurrMbAddr / 2) + ((topAbsDiffPOC < bottomAbsDiffPOC) ? 0 : 1);
            int mbAddrCol7 = 2 * (CurrMbAddr / 2) + (yCol / 8);

            int mbAddrX = 0;
            int yM = 0;

            PictureCodingStructure pcsCurrPic = PicCodingStruct(CurrentPicture!);
            PictureCodingStructure pcsColPic = PicCodingStruct(colPic);

            bool fieldDecodingFlagX = false;

            if (pcsCurrPic == PictureCodingStructure.FLD)
            {
                if (pcsColPic == PictureCodingStructure.FLD)
                {
                    mbAddrCol = new AddressAndAvailability(CurrMbAddr, true); // Current MB address always available
                    yM = yCol;
                    vertMvScale = VerticalMotionVectorScale.OneToOne;
                }
                else if (pcsColPic == PictureCodingStructure.FRM)
                {
                    mbAddrCol = new AddressAndAvailability(mbAddrCol1, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol1) == true);
                    yM = (2 * yCol) % 16;
                    vertMvScale = VerticalMotionVectorScale.FrameToField;
                }
                else /*AFRM*/
                {
                    mbAddrX = 2 * CurrMbAddr;
                    fieldDecodingFlagX = ComputeFieldDecodingFlagX(mbAddrX, colPic);
                    if (!fieldDecodingFlagX)
                    {
                        mbAddrCol = new AddressAndAvailability(mbAddrCol2, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol2) == true);
                        yM = (2 * yCol) % 16;
                        vertMvScale = VerticalMotionVectorScale.FrameToField;
                    }
                    else
                    {
                        mbAddrCol = new AddressAndAvailability(mbAddrCol3, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol3) == true);
                        yM = yCol;
                        vertMvScale = VerticalMotionVectorScale.OneToOne;
                    }
                }
            }
            else if (pcsCurrPic == PictureCodingStructure.FRM)
            {
                if (pcsColPic == PictureCodingStructure.FLD)
                {
                    mbAddrCol = new AddressAndAvailability(mbAddrCol4, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol4) == true);
                    yM = 8 * ((CurrMbAddr / PicWidthInMbs) % 2) + 4 * (yCol / 8);
                    vertMvScale = VerticalMotionVectorScale.FieldToFrame;
                }
                else
                {
                    mbAddrCol = new AddressAndAvailability(CurrMbAddr, true); // Current MB address always available
                    yM = yCol;
                    vertMvScale = VerticalMotionVectorScale.OneToOne;
                }
            }
            else /*AFRM*/
            {
                if (pcsColPic == PictureCodingStructure.FLD)
                {
                    mbAddrCol = new AddressAndAvailability(mbAddrCol5, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol5) == true);
                    if (CurrentMacroblock?.MbFieldDecodingFlag == false)
                    {
                        yM = 8 * (CurrMbAddr % 2) + 4 * (yCol / 8);
                        vertMvScale = VerticalMotionVectorScale.FieldToFrame;
                    }
                    else
                    {
                        yM = yCol;
                        vertMvScale = VerticalMotionVectorScale.OneToOne;
                    }
                }
                else /*AFRM*/
                {
                    mbAddrX = CurrMbAddr;
                    fieldDecodingFlagX = ComputeFieldDecodingFlagX(mbAddrX, colPic);
                    if (CurrentMacroblock?.MbFieldDecodingFlag == false)
                    {
                        if (!fieldDecodingFlagX)
                        {
                            mbAddrCol = new AddressAndAvailability(CurrMbAddr, true); // Current MB address always available
                            vertMvScale = VerticalMotionVectorScale.OneToOne;
                        }
                        else
                        {
                            mbAddrCol = new AddressAndAvailability(mbAddrCol6, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol6) == true);
                            yM = 8 * (CurrMbAddr % 2) + 4 * (yCol / 8);
                            vertMvScale = VerticalMotionVectorScale.FieldToFrame;
                        }
                    }
                    else
                    {
                        if (!fieldDecodingFlagX)
                        {
                            mbAddrCol = new AddressAndAvailability(mbAddrCol7, H264State?.MacroblockUtility?.IsMacroblock(mbAddrCol7) == true);
                            yM = (2 * yCol) % 16;
                            vertMvScale = VerticalMotionVectorScale.FrameToField;
                        }
                        else
                        {
                            mbAddrCol = new AddressAndAvailability(CurrMbAddr, true); // Current MB address always available
                            yM = yCol;
                            vertMvScale = VerticalMotionVectorScale.OneToOne;
                        }
                    }
                }
            }

            var refPicImg = GetReferencePictureImage(colPic);
            var mbTypeCol = refPicImg[mbAddrCol.Address];
            Span<int> sub_mb_type = stackalloc int[4];
            int top = 0;
            foreach (uint mbType in mbTypeCol.MacroblockInfo.Rbsp.SubMbPred?.SubMbType ?? [])
                sub_mb_type[top++] = (int)mbType;

            PartitionIndices pi = H264Derivative.DeriveMacroblockAndSubMacroblockPartitionIndices(mbTypeCol.MacroblockInfo, (int)mbTypeCol.MacroblockInfo.Rbsp.MbType, new XpYp(xCol, yM), sub_mb_type);
            
            if (colPic.Picture.State?.MacroblockUtility.IsIntra(mbTypeCol.MacroblockInfo) == true)
            {
                mvCol = MotionVector0;
                refIdxCol = -1;
            }
            else
            {
                if (mbTypeCol == P_8x8 || mbTypeCol == P_8x8ref0 || mbTypeCol == B_8x8)
                {
                    mvCol = AsMV(mbTypeCol.MacroblockInfo.Rbsp.SubMbPred!.MvdL0![pi.MbPartIdx][pi.SubMbPartIdx]);
                    refIdxCol = (int)mbTypeCol.MacroblockInfo.Rbsp.SubMbPred!.RefIdxL0![pi.MbPartIdx];
                }
                else
                {
                    mvCol = AsMV(mbTypeCol.MacroblockInfo.Rbsp.SubMbPred!.MvdL1![pi.MbPartIdx][pi.SubMbPartIdx]);
                    refIdxCol = (int)mbTypeCol.MacroblockInfo.Rbsp.SubMbPred!.RefIdxL1![pi.MbPartIdx];
                }
            }

            static PictureCodingStructure PicCodingStruct(PictureDescriptor X)
            {
                if (X.Picture.State?.H264RbspState?.SliceHeader?.FieldPicFlag == true)
                {
                    return PictureCodingStructure.FLD;
                }
                return X.Picture.State?.H264RbspState?.SequenceParameterSetData?.MbAdaptiveFrameFieldFlag == true ? PictureCodingStructure.FRM : PictureCodingStructure.AFRM;
            }
        }

        private void DeriveTemporalDirectLumaMvs(int mbPartIdx, int subMbPartIdx,
            out int refIdxL0, out int refIdxL1, out H264MotionVector mvL0, out H264MotionVector mvL1,
            out bool predFlagL0, out bool predFlagL1)
        {
            DeriveCoLocated4x4SubMacroblockPartitions(mbPartIdx, subMbPartIdx, out _, out _, out var mvCol, out var refIdxCol, out var vertMvScale);

            var refPicCol = RefPicList1![refIdxCol];

            refIdxL0 = ((refIdxCol < 0) ? 0 : MapColToList0());
            refIdxL1 = 0;

            if (vertMvScale == VerticalMotionVectorScale.FrameToField)
                mvCol.Y /= 2;
            else if (vertMvScale == VerticalMotionVectorScale.FieldToFrame)
                mvCol.Y *= 2;

            PictureDescriptor currPicOrField, pic0, pic1;

            if (H264State?.H264RbspState?.SliceHeader?.FieldPicFlag == false &&
                H264State?.MacroblockUtility.IsFrame(CurrentMacroblock!) == false)
            {
                currPicOrField = WithParity(CurrentPicture!, true);
                pic1 = WithParity(RefPicList1![0], true);
                pic0 = refIdxL0 % 2 == 0 ? WithParity(RefPicList0![refIdxL0 / 2], true) : WithParity(RefPicList0![refIdxL0 / 2], false);
            }
            else
            {
                currPicOrField = CurrentPicture!;
                pic1 = RefPicList1![0];
                pic0 = RefPicList0![refIdxL0];
            }

            if (RefPicList0![refIdxL0].Duration == PictureDuration.LongTerm ||
                SliceDecoder?.DiffPicOrderCnt(pic1.Poc.PictureOrderCount, pic0.Poc.PictureOrderCount) == 0)
            {
                mvL0 = mvCol;
                mvL1 = MotionVector0;
            }
            else
            {
                int tb = Clipping.Clip(-128, 127, SliceDecoder!.DiffPicOrderCnt(currPicOrField.Poc.PictureOrderCount, pic0.Poc.PictureOrderCount));
                int td = Clipping.Clip(-128, 127, SliceDecoder!.DiffPicOrderCnt(pic1.Poc.PictureOrderCount, pic0.Poc.PictureOrderCount));

                int tx = (16384 + Math.Abs(td / 2)) / td;
                int DistScaleFactor = Clipping.Clip(-1024, 1023, (tb * tx + 32) >> 6);
                mvL0 = new((DistScaleFactor * mvCol.X + 128) >> 8, (DistScaleFactor * mvCol.Y + 128) >> 8);
                mvL1 = new(mvL0.X - mvCol.X, mvL0.Y - mvCol.Y);
            }

            predFlagL0 = true;
            predFlagL1 = true;

            int MapColToList0()
            {
                if (vertMvScale == VerticalMotionVectorScale.OneToOne)
                {
                    if (H264State?.H264RbspState?.SliceHeader?.FieldPicFlag == false &&
                        H264State?.MacroblockUtility.IsFrame(CurrentMacroblock!) == false)
                    {
                        var refIdxL0FrmInt = GetRefIdxL0Frm(refPicCol); // Int means intermediate
                        if (refIdxL0FrmInt.cfp == null)
                            throw new InterDecoderException("Cannot get refIdxL0Frm");

                        int refIdxL0Frm = refIdxL0FrmInt.refIdxL0Frm;

                        if (refIdxL0FrmInt.cfp.IsSameParity(H264State!))
                            return refIdxL0Frm << 1;
                        else
                            return (refIdxL0Frm << 1) + 1;
                    }
                    else
                    {
                        // TODO: Might be incorrect
                        return GetRefIdxL0Frm(refPicCol).refIdxL0Frm;
                    }
                }
                else if (vertMvScale == VerticalMotionVectorScale.FrameToField)
                {
                    if (H264State?.H264RbspState?.SliceHeader?.FieldPicFlag == false)
                    {
                        var refIdxL0FrmInt = GetRefIdxL0Frm(refPicCol); // Int means intermediate
                        if (refIdxL0FrmInt.cfp == null)
                            throw new InterDecoderException("Cannot get refIdxL0Frm");

                        int refIdxL0Frm = refIdxL0FrmInt.refIdxL0Frm;

                        return refIdxL0Frm << 1;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    var refIdxL0FrmInt = GetRefIdxL0Frm(refPicCol); // Int means intermediate
                    if (refIdxL0FrmInt.cfp == null)
                        throw new InterDecoderException("Cannot get refIdxL0Frm");

                    int refIdxL0Frm = refIdxL0FrmInt.refIdxL0Frm;

                    return refIdxL0Frm;
                }
            }
        }

        public void DeriveChromaMvs(H264MotionVector mvLX, int refIdxLX, int listSuffixFlag, out H264MotionVector mvCLX)
        {
            if (H264State?.DeriveChromaArrayType() != 1 || H264State?.MacroblockUtility.IsFrame(CurrentMacroblock!) == true)
            {
                mvCLX = mvLX;
            }
            else
            {
                if (IsTopField(listSuffixFlag == 1 ? RefPicList1![refIdxLX] : RefPicList0![refIdxLX], listSuffixFlag == 1) && IsBottomField(CurrentPicture!, listSuffixFlag == 1))
                {
                    mvCLX = new(0, mvLX.Y + 2);
                }
                else if (IsBottomField(listSuffixFlag == 1 ? RefPicList1![refIdxLX] : RefPicList0![refIdxLX], listSuffixFlag == 1) && IsTopField(CurrentPicture!, listSuffixFlag == 1))
                {
                    mvCLX = new(0, mvLX.Y - 2);
                }
                else
                {
                    mvCLX = new(0, mvLX.Y);
                }
            }
        }

        private void PredictWeightedSample(bool predFlagL0, bool predFlagL1,
            Picture<YCbCr> predPartL0,
            Picture<YCbCr> predPartL1,
            LogWDc logWDc, OArray o, WArray w,
            int partWidth, int partHeight,
            int partWidthC, int partHeightC,
            Picture<YCbCr> predPart)
        {
            if (CurrentMacroblock?.SliceType is H264SliceType.P or H264SliceType.SP && predFlagL0)
            {
                if (!SyntaxElementGrabber.GetWeightedPredFlag(this.H264State?.H264RbspState))
                {
                    PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.L, (x, y) =>
                        {
                            x.Y = (byte)y;
                            return x;
                        }, (x) => x.Y);
                    PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cb, (x, y) =>
                        {
                            x.Cb = (byte)y;
                            return x;
                        }, (x) => x.Cb);
                    PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cr, (x, y) =>
                        {
                            x.Cr = (byte)y;
                            return x;
                        }, (x) => x.Cr);
                }
                else
                {
                    PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Y, (x, y) =>
                        {
                            x.Y = (byte)y;
                            return x;
                        }, (x) => x.Y,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                    PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cb, (x, y) =>
                        {
                            x.Cb = (byte)y;
                            return x;
                        }, (x) => x.Cb,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                    PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cr, (x, y) =>
                        {
                            x.Cr = (byte)y;
                            return x;
                        }, (x) => x.Cr,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                }
            }
            else if (CurrentMacroblock?.SliceType is H264SliceType.B && (predFlagL0 || predFlagL1))
            {
                if (SyntaxElementGrabber.GetWeightedBiPredIdc(this.H264State?.H264RbspState) == 0)
                {
                    PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.L, (x, y) =>
                        {
                            x.Y = (byte)y;
                            return x;
                        }, (x) => x.Y);
                    PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cb, (x, y) =>
                        {
                            x.Cb = (byte)y;
                            return x;
                        }, (x) => x.Cb);
                    PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cr, (x, y) =>
                        {
                            x.Cr = (byte)y;
                            return x;
                        }, (x) => x.Cr);
                }
                else if (SyntaxElementGrabber.GetWeightedBiPredIdc(this.H264State?.H264RbspState) == 1)
                {
                    PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Y, (x, y) =>
                        {
                            x.Y = (byte)y;
                            return x;
                        }, (x) => x.Y,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                    PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cb, (x, y) =>
                        {
                            x.Cb = (byte)y;
                            return x;
                        }, (x) => x.Cb,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                    PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                        ChromaChannel.Cr, (x, y) =>
                        {
                            x.Cr = (byte)y;
                            return x;
                        }, (x) => x.Cr,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                }
                else
                {
                    if (predFlagL0 && predFlagL1)
                    {
                        PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                            ChromaChannel.Y, (x, y) =>
                            {
                                x.Y = (byte)y;
                                return x;
                            }, (x) => x.Y,
                        this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                        PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                            ChromaChannel.Cb, (x, y) =>
                            {
                                x.Cb = (byte)y;
                                return x;
                            }, (x) => x.Cb,
                            this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                        PredictExplicitWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                            ChromaChannel.Cr, (x, y) =>
                            {
                                x.Cr = (byte)y;
                                return x;
                            }, (x) => x.Cr,
                            this.H264State!.DeriveBitDepthY(), this.H264State!.DeriveBitDepthC(), in logWDc, in w, in o);
                    }
                    else
                    {
                        PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                            ChromaChannel.L, (x, y) =>
                            {
                                x.Y = (byte)y;
                                return x;
                            }, (x) => x.Y);
                        PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                            ChromaChannel.Cb, (x, y) =>
                            {
                                x.Cb = (byte)y;
                                return x;
                            }, (x) => x.Cb);
                        PredictDefaultWeightedSample(predFlagL0, predFlagL1, predPart, predPartL0, predPartL1, partWidth, partHeight, partWidthC, partHeightC,
                            ChromaChannel.Cr, (x, y) =>
                            {
                                x.Cr = (byte)y;
                                return x;
                            }, (x) => x.Cr);
                    }
                }
            }
        }

        public static void PredictDefaultWeightedSample(
            bool predFlagL0, bool predFlagL1, Picture<YCbCr> predPart, Picture<YCbCr> predPartL0, Picture<YCbCr> predPartL1,
            int partWidth, int partHeight, int partWidthC, int partHeightC, ChromaChannel cc, Func<YCbCr, int, YCbCr> takePixelAndMutateChannel, Func<YCbCr, int> getPart)
        {
            int xEnd = cc == ChromaChannel.L ? partWidth - 1 : partWidthC - 1;
            int yEnd = cc == ChromaChannel.L ? partHeight - 1 : partHeightC - 1;

            if (predFlagL0 && !predFlagL1)
            {
                for (int x = 0; x <= xEnd; x++)
                {
                    for (int y = 0; y <= yEnd; y++)
                    {
                        YCbCr pixel = predPart[x, y];
                        pixel = takePixelAndMutateChannel(pixel, getPart(predPartL0[x, y]));
                        predPart[x, y] = pixel;
                    }
                }
            }
            else if (!predFlagL0 && predFlagL1)
            {
                for (int x = 0; x <= xEnd; x++)
                {
                    for (int y = 0; y <= yEnd; y++)
                    {
                        YCbCr pixel = predPart[x, y];
                        pixel = takePixelAndMutateChannel(pixel, getPart(predPartL1[x, y]));
                        predPart[x, y] = pixel;
                    }
                }
            }
            else
            {
                for (int x = 0; x <= xEnd; x++)
                {
                    for (int y = 0; y <= yEnd; y++)
                    {
                        YCbCr pixel = predPart[x, y];
                        pixel = takePixelAndMutateChannel(pixel, (getPart(predPartL0[x, y]) + getPart(predPartL1[x, y]) + 1) >> 1);
                        predPart[x, y] = pixel;
                    }
                }
            }
        }

        public static void PredictExplicitWeightedSample(
            bool predFlagL0, bool predFlagL1, Picture<YCbCr> predPart, Picture<YCbCr> predPartL0, Picture<YCbCr> predPartL1,
            int partWidth, int partHeight, int partWidthC, int partHeightC, ChromaChannel cc, Func<YCbCr, int, YCbCr> takePixelAndMutateChannel, Func<YCbCr, int> getPart,
            int BitDepthY, int BitDepthC, in LogWDc logWDc, in WArray w, in OArray o)
        {
            int xEnd = cc == ChromaChannel.L ? partWidth - 1 : partWidthC - 1;
            int yEnd = cc == ChromaChannel.L ? partHeight - 1 : partHeightC - 1;

            ClipDelegate Clip = cc == ChromaChannel.L ? CdfL(BitDepthY) : CdfC(BitDepthC);

            if (predFlagL0 && !predFlagL1)
            {
                for (int x = 0; x <= xEnd; x++)
                {
                    for (int y = 0; y <= yEnd; y++)
                    {
                        if (logWDc.GetElement(cc) >= 1)
                        {
                            YCbCr ycc = predPart[x, y];
                            YCbCr newPix = takePixelAndMutateChannel(ycc, Clip(((getPart(predPartL0[x, y]) * w.GetElementOrMinus1(0, cc) + (int)Math.Pow(2, logWDc.GetElement(cc)) - 1) >> logWDc.GetElement(cc)) + o.GetElementOrMinus1(0, cc)));
                            predPart[x, y] = newPix;
                        }
                        else
                        {
                            YCbCr ycc = predPart[x, y];
                            YCbCr newPix = takePixelAndMutateChannel(ycc, Clip(getPart(predPartL0[x, y]) * w.GetElementOrMinus1(0, cc) + o.GetElementOrMinus1(0, cc)));
                            predPart[x, y] = newPix;
                        }
                    }
                }
            }
            else if (!predFlagL0 && predFlagL1)
            {
                for (int x = 0; x <= xEnd; x++)
                {
                    for (int y = 0; y <= yEnd; y++)
                    {
                        if (logWDc.GetElement(cc) >= 1)
                        {
                            YCbCr ycc = predPart[x, y];
                            YCbCr newPix = takePixelAndMutateChannel(ycc, Clip(((getPart(predPartL1[x, y]) * w.GetElementOrMinus1(1, cc) + (int)Math.Pow(2, logWDc.GetElement(cc)) - 1) >> logWDc.GetElement(cc)) + o.GetElementOrMinus1(1, cc)));
                            predPart[x, y] = newPix;
                        }
                        else
                        {
                            YCbCr ycc = predPart[x, y];
                            YCbCr newPix = takePixelAndMutateChannel(ycc, Clip(getPart(predPartL1[x, y]) * w.GetElementOrMinus1(1, cc) + o.GetElementOrMinus1(1, cc)));
                            predPart[x, y] = newPix;
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x <= xEnd; x++)
                {
                    for (int y = 0; y <= yEnd; y++)
                    {
                        YCbCr ycc = predPart[x, y];
                        YCbCr newPix = takePixelAndMutateChannel(ycc, Clip(((getPart(predPartL0[x, y]) * w.GetElementOrMinus1(0, cc) + getPart(predPartL1[x, y]) * w.GetElementOrMinus1(1, cc) + (int)Math.Pow(2, logWDc.GetElement(cc))) >>
                                            (logWDc.GetElement(cc) + 1)) + ((o.GetElementOrMinus1(0, cc) + o.GetElementOrMinus1(1, cc) + 1) >> 1)));
                        predPart[x, y] = newPix;
                    }
                }
            }
        }

        private void DeriveMvComponents(int mbPartIdx, int subMbPartIdx,
            out H264MotionVector mvL0, out H264MotionVector mvL1, out H264MotionVector mvCL0, out H264MotionVector mvCL1,
            out int refIdxL0, out int refIdxL1,
            out bool predFlagL0, out bool predFlagL1,
            out int subMvCnt)
        {
            mvCL0 = default;
            mvCL1 = default;

            if (CurrentMacroblock! == P_Skip)
            {
                DeriveLumaMvForSkippedMacroblocksInPSpSlices(out mvL0, out refIdxL0);
                mvL1 = MotionVector0;
                refIdxL1 = 0;
                predFlagL0 = true;
                predFlagL1 = false;
                subMvCnt = 1;
            }
            else if (CurrentMacroblock! == B_Skip ||
                CurrentMacroblock! == B_Direct_16x16 ||
                MacroblockEquality.SubMacroblocksEqual((int)CurrentMacroblock!.Rbsp.SubMbPred!.SubMbType[mbPartIdx], CurrentMacroblock!.SliceType, B_Direct_8x8))
            {
                DeriveLumaMvsForBSlices(mbPartIdx, subMbPartIdx, out refIdxL0, out refIdxL1, out mvL0, out mvL1, out subMvCnt, out predFlagL0, out predFlagL1);
            }
            else
            {
                Derive(false, out predFlagL0, out mvL0, out refIdxL0);
                Derive(true, out predFlagL1, out mvL1, out refIdxL1);

                if (H264State!.DeriveChromaArrayType() != 0 && predFlagL0) DeriveChromaMvs(mvL0, refIdxL0, 0, out mvCL0); // L0
                if (H264State!.DeriveChromaArrayType() != 0 && predFlagL1) DeriveChromaMvs(mvL1, refIdxL1, 1, out mvCL1); // L1

                subMvCnt = predFlagL0.AsInt32() + predFlagL1.AsInt32();

                void Derive(bool l1, out bool predFlagLX, out H264MotionVector mvLX, out int refIdxLX)
                {
                    int Pred_LX = l1 ? Pred_L1 : Pred_L0;

                    int mbppm = MacroblockTraits.MbPartPredMode(CurrentMacroblock!, 0);
                    int submbppm = MacroblockTraits.SubMbPredMode(CurrentMacroblock!, (int)CurrentMacroblock!.Rbsp.SubMbPred!.SubMbType[mbPartIdx]);
                    if (mbppm == Pred_LX || mbppm == BiPred)
                    {
                        refIdxLX = (int)(l1 ? CurrentMacroblock!.Rbsp.MbPred!.RefIdxL1! : CurrentMacroblock!.Rbsp.MbPred!.RefIdxL0!)[mbPartIdx];
                        predFlagLX = true;
                    }
                    else if (submbppm == Pred_LX || submbppm == BiPred)
                    {
                        refIdxLX = (int)(l1 ? CurrentMacroblock!.Rbsp.SubMbPred!.RefIdxL1! : CurrentMacroblock!.Rbsp.SubMbPred!.RefIdxL0!)[mbPartIdx];
                        predFlagLX = true;
                    }
                    else
                    {
                        refIdxLX = -1;
                        predFlagLX = false;
                    }

                    int currSubMbType = CurrentMacroblock == B_8x8 ? (int)CurrentMacroblock!.Rbsp.SubMbPred!.SubMbType[mbPartIdx] : 0;

                    if (predFlagLX)
                    {
                        DeriveLumaMvPrediction(mbPartIdx, subMbPartIdx, refIdxLX, l1.AsInt32(), currSubMbType, out H264MotionVector mvpLX);

                        mvLX = new(
                            mvpLX.X + (l1 ? CurrentMacroblock!.Rbsp.MbPred!.MvdL1! : CurrentMacroblock!.Rbsp.MbPred!.MvdL0)![mbPartIdx][subMbPartIdx][0],
                            mvpLX.Y + (l1 ? CurrentMacroblock!.Rbsp.MbPred!.MvdL1! : CurrentMacroblock!.Rbsp.MbPred!.MvdL0)![mbPartIdx][subMbPartIdx][1]
                        );
                    }
                    else
                    {
                        mvLX = MotionVector0;
                    }
                }
            }
        }

        private void SelectReferencePicture(int refIdxLX, bool l1, out PictureDescriptor refPicLX)
        {
            IDecodedPictureBuffer RefPicListX = l1 ? this.RefPicList1! : this.RefPicList0!;

            if (this.H264State!.MacroblockUtility.IsFrame(CurrentMacroblock!))
            {
                refPicLX = RefPicListX[refIdxLX];
            }
            else
            {
                var refFrame = RefPicListX[refIdxLX / 2];
                if (refIdxLX % 2 == 0)
                    refPicLX = WithParity(refFrame, true);
                else
                    refPicLX = WithParity(refFrame, false);
            }
        }

        private void DecodeInterPredictionSample(
            int partWidth, int partHeight, int partWidthC, int partHeightC,
            int mbIndexX, int mbIndexY,
            H264MotionVector mvL0, H264MotionVector mvL1, H264MotionVector mvCL0, H264MotionVector mvCL1,
            int refIdxL0, int refIdxL1,
            bool predFlagL0, bool predFlagL1,
            LogWDc logWDc, OArray o, WArray w,
            out Picture<YCbCr> outPic)
        {
            Picture<YCbCr> predPartL0, predPartL1;
            if (predFlagL0)
                Derive(mvL0, mvCL0, false, refIdxL0, out predPartL0);
            else
                predPartL0 = MemoryPictureFactory.Instance.CreatePicture<YCbCr>(partWidth, partHeight);
            if (predFlagL1)
                Derive(mvL1, mvCL1, true, refIdxL1, out predPartL1);
            else
                predPartL1 = MemoryPictureFactory.Instance.CreatePicture<YCbCr>(partWidth, partHeight);

            outPic = MemoryPictureFactory.Instance.CreatePicture<YCbCr>(partWidth, partHeight);

            PredictWeightedSample(predFlagL0, predFlagL1, predPartL0, predPartL1, logWDc, o, w, partWidth, partHeight, partWidthC, partHeightC, outPic);

            void Derive(H264MotionVector mvLX, H264MotionVector mvCLX, bool l1, int refIdxLX, out Picture<YCbCr> predPartLX)
            {
                predPartLX = MemoryPictureFactory.Instance.CreatePicture<YCbCr>(partWidth, partHeight);

                SelectReferencePicture(refIdxLX, l1, out var desc);

                var spc = SinglePictureCacheProvider.CreateDefaultFactory().CreateSinglePictureCache<YCbCr>();
                var pic = desc.Picture.GetAndCacheRaw(spc);

                MotionCompensation.InterpolateFractionalSample(
                    mbIndexX, mbIndexY, partWidth, partHeight, CurrentMacroblock!.MbFieldDecodingFlag, this.H264State!, mvLX, mvCLX, pic, predPartLX);
            }
        }

        private bool IsTopField(PictureDescriptor pic, bool listSuffix)
        {
            IDecodedPictureBuffer dpb = listSuffix ? RefPicList1! : RefPicList0!;
            foreach (PictureDescriptor pd in dpb.Descriptors)
            {
                if (pd.Picture is ComplementaryFieldPair cfp && cfp.Top == pic)
                    return true;
            }
            return false;
        }

        private bool IsBottomField(PictureDescriptor pic, bool listSuffix)
        {
            IDecodedPictureBuffer dpb = listSuffix ? RefPicList1! : RefPicList0!;
            foreach (PictureDescriptor pd in dpb.Descriptors)
            {
                if (pd.Picture is ComplementaryFieldPair cfp && cfp.Bottom == pic)
                    return true;
            }
            return false;
        }

        private (int refIdxL0Frm, ComplementaryFieldPair? cfp) GetRefIdxL0Frm(PictureDescriptor refPicCol)
        {
            for (int refIdx = 0; refIdx < RefPicList0!.Descriptors.Count; refIdx++)
            {
                if (RefPicList0[refIdx].Picture is ComplementaryFieldPair cfp)
                {
                    if (cfp.Top == refPicCol || cfp.Bottom == refPicCol)
                        return (refIdx, cfp);
                }
            }
            return (-1, null);
        }

        private static bool IsFieldOfDecodedFrame(PictureDescriptor pic, IDecodedPictureBuffer dpb)
        {
            foreach (PictureDescriptor p in dpb.Descriptors)
            {
                if (p.Picture is ComplementaryFieldPair cfp)
                {
                    return cfp.Top == pic || cfp.Bottom == pic;
                }
            }
            return false;
        }

        private static bool IsDecodedField(PictureDescriptor pic)
        {
            return pic.Picture is FieldPicture;
        }

        private static bool IsDecodedFrame(PictureDescriptor pic) => pic.Picture is FramePicture;
        private static bool IsComplementaryFieldPair(PictureDescriptor pic) => pic.Picture is ComplementaryFieldPair;

        private static PictureDescriptor GetFrameOwner(PictureDescriptor pic, IDecodedPictureBuffer dpb)
        {
            foreach (PictureDescriptor p in dpb.Descriptors)
            {
                if (p.Picture is ComplementaryFieldPair cfp)
                {
                    if (cfp.Top == pic || cfp.Bottom == pic)
                    {
                        return p;
                    }
                }
            }
            // TODO: Meaningful exception?
            throw new NotImplementedException();
        }

        private static bool ComputeFieldDecodingFlagX(int mbAddrX, PictureDescriptor colPic)
        {
            if (colPic.Picture is FieldPicture fi)
            {
                return !colPic.Picture.State!.MacroblockUtility.IsFrame(fi.Picture.Picture[mbAddrX].MacroblockInfo);
            }
            else if (colPic.Picture is FramePicture fr)
            {
                return !colPic.Picture.State!.MacroblockUtility.IsFrame(fr.Picture[mbAddrX].MacroblockInfo);
            }
            else
            {
                // TODO: Meaningful exception? ? ? ?? ?  ?? ? ? ?? ? ?
                throw new InvalidOperationException();
            }
        }

        private static H264ReferencePictureImage GetReferencePictureImage(PictureDescriptor pic)
        {
            if (pic.Picture is FieldPicture fi)
            {
                return fi.Picture.Picture;
            }
            else if (pic.Picture is FramePicture fr)
            {
                return fr.Picture;
            }
            else
            {
                // TODO: Meaningful exception? ? ? ?? ?  ?? ? ? ?? ? ?
                throw new InvalidOperationException();
            }
        }

        private PictureDescriptor WithParity(PictureDescriptor pic, bool parity)
        {
            if (pic.Picture is ComplementaryFieldPair cfp)
            {
                if (cfp.Top.Picture.IsSameParity(H264State!) == parity)
                    return cfp.Top;
                else if (cfp.Bottom.Picture.IsSameParity(H264State!) == parity)
                    return cfp.Bottom;
                else
                    return cfp.Top;
            }
            else
            {
                return pic;
            }
        }
    }
}
