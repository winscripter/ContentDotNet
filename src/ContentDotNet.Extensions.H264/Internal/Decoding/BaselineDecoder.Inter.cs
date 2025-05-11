using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Pictures;
using ContentDotNet.Extensions.H264.Utilities;
using System.Drawing;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class BaselineDecoder
{
    public sealed class Inter
    {
        private MacroblockTypeHistory mbTypeArray;
        private MacroblockTypeHistory subMbTypeArray;
        private GeneralSliceType sliceType;
        private int mbType;
        private int subMbType;
        private int mbPartIdx;
        private int subMbPartIdx;

        // NOTE: Initialization of the following fields is performed in
        //       the InitializeInterPrediction method, which is invoked
        //       by the constructor. The constructor doesn't directly
        //       initialize them. Use null! to suppress CS8618 warnings.

        private int[] refIdxL0 = null!;
        private int[] refIdxL1 = null!;
        private ArrayMatrix4x4x2 mvL0 = null!;
        private ArrayMatrix4x4x2 mvL1 = null!;
        private ArrayMatrix4x4x2 mvCL0 = null!;
        private ArrayMatrix4x4x2 mvCL1 = null!;

        private bool predFlagL0 = false;
        private bool predFlagL1 = false;

        private bool[] predFlagL0Array = new bool[16];
        private bool[] predFlagL1Array = new bool[16];

        private DerivationContext _derivationContext;
        private IMacroblockUtility _macroblockUtility;

        private Size frameSize = default;

        private ReferencePicture? CurrPic = null;

        private SequenceParameterSet sps = default;
        private PictureParameterSet pps = default;
        private NalUnit nalu = default;
        private SliceHeader sliceHeader = default;

        private PocContext pocCtx = default;

        private int currMbAddr = 0;

        public Dpb RefPicListL0 { get; private set; } = null!;
        public Dpb? RefPicListL1 { get; private set; }

        public SequenceParameterSet SequenceParameterSet
        {
            get => sps;
            set => sps = value;
        }

        public PictureParameterSet PictureParameterSet
        {
            get => pps;
            set => pps = value;
        }

        public NalUnit NalUnit
        {
            get => nalu;
            set => nalu = value;
        }

        public SliceHeader SliceHeader
        {
            get => sliceHeader;
            set => sliceHeader = value;
        }

        public int CurrMbAddr
        {
            get => currMbAddr;
            set => currMbAddr = value;
        }

        public bool MbFieldDecodingFlag { get; set; } = false;

        public int PicWidthInMbs => (int)(sps.PicWidthInMbsMinus1 + 1);

        public Inter(DerivationContext derivationContext, IMacroblockUtility macroblockUtility, Size frameSize)
        {
            _derivationContext = derivationContext;
            _macroblockUtility = macroblockUtility;

            InitializeInterPrediction(frameSize);
        }

        public DerivationContext DerivationContext
        {
            get => _derivationContext;
            set => _derivationContext = value;
        }

        private void InitializeInterPrediction(Size frameSize)
        {
            sliceType = GeneralSliceType.I; // Default
            mbTypeArray = new();
            subMbTypeArray = new();
            mbType = 0;
            subMbType = 0;
            mbPartIdx = 0;
            subMbPartIdx = 0;
            refIdxL0 = new int[16];
            refIdxL1 = new int[16];
            mvL0 = new ArrayMatrix4x4x2();
            mvL1 = new ArrayMatrix4x4x2();
            mvCL0 = new ArrayMatrix4x4x2();
            mvCL1 = new ArrayMatrix4x4x2();

            this.frameSize = frameSize;

            this.RefPicListL0 = new([], 16);
            this.RefPicListL1 = null; // What if it's not a B slice? Then we're wasting memory. Store factory and frameSize separately.
        }

        // 8.4.1.3.2
        private void DeriveMotionDataOfNeighboringPartitions(int currSubMbType, bool listSuffixFlag,
            out int mbAddrA, out int mbAddrB, out int mbAddrC,
            out MotionVector mvL0A, out MotionVector mvL0B, out MotionVector mvL0C,
            out MotionVector mvL1A, out MotionVector mvL1B, out MotionVector mvL1C,
            out int refIdxL0A, out int refIdxL0B, out int refIdxL0C,
            out int refIdxL1A, out int refIdxL1B, out int refIdxL1C,
            out bool validA, out bool validB, out bool validC)
        {
            mbAddrA = 0;
            mbAddrB = 0;
            mbAddrC = 0;

            int mbAddrD = 0;
            int mbPartIdxA = 0, mbPartIdxB = 0, mbPartIdxC = 0, mbPartIdxD = 0;
            int subMbPartIdxA = 0, subMbPartIdxB = 0, subMbPartIdxC = 0, subMbPartIdxD = 0;
            validA = false;
            validB = false;
            validC = false;
            bool validD = false;

            Scanning.DeriveNeighboringPartitions(
                sliceType,
                _derivationContext,
                mbPartIdx,
                currSubMbType,
                subMbPartIdx,
                mbType,
                mbTypeArray,
                subMbTypeArray,
                ref mbAddrA, ref mbPartIdxA, ref subMbPartIdxA, ref validA,
                ref mbAddrB, ref mbPartIdxB, ref subMbPartIdxB, ref validB,
                ref mbAddrC, ref mbPartIdxC, ref subMbPartIdxC, ref validC,
                ref mbAddrD, ref mbPartIdxD, ref subMbPartIdxD, ref validD
            );

            if (!validC)
            {
                mbAddrC = mbAddrD;
                mbPartIdxC = mbPartIdxD;
                subMbPartIdxC = subMbPartIdxD;
            }

            mvL0A = default;
            mvL0B = default;
            mvL0C = default;
            mvL1A = default;
            mvL1B = default;
            mvL1C = default;

            refIdxL0A = 0;
            refIdxL0B = 0;
            refIdxL0C = 0;
            refIdxL1A = 0;
            refIdxL1B = 0;
            refIdxL1C = 0;

            if (listSuffixFlag)
            {
                Derive(ref mvL1A, mvL1, refIdxL1, validA, predFlagL1, ref refIdxL1A, _derivationContext, mbPartIdxA, subMbPartIdxA, mbAddrA);
                Derive(ref mvL1B, mvL1, refIdxL1, validB, predFlagL1, ref refIdxL1B, _derivationContext, mbPartIdxB, subMbPartIdxB, mbAddrB);
                Derive(ref mvL1C, mvL1, refIdxL1, validC, predFlagL1, ref refIdxL1C, _derivationContext, mbPartIdxC, subMbPartIdxC, mbAddrC);
            }
            else
            {
                Derive(ref mvL0A, mvL0, refIdxL0, validA, predFlagL0, ref refIdxL0A, _derivationContext, mbPartIdxA, subMbPartIdxA, mbAddrA);
                Derive(ref mvL0B, mvL0, refIdxL0, validB, predFlagL0, ref refIdxL0B, _derivationContext, mbPartIdxB, subMbPartIdxB, mbAddrB);
                Derive(ref mvL0C, mvL0, refIdxL0, validC, predFlagL0, ref refIdxL0C, _derivationContext, mbPartIdxC, subMbPartIdxC, mbAddrC);
            }

            void Derive(ref MotionVector mvLXN, ArrayMatrix4x4x2 mvLX, int[] refIdxLX, bool validN, bool predFlagLX, ref int refIdxLXN, DerivationContext dc, int mbPartIdxN, int subMbPartIdxN, int mbAddrN)
            {
                if (!validN || _macroblockUtility.IsCodedWithIntra(dc.CurrMbAddr) || !predFlagLX)
                {
                    mvLXN = (0, 0);
                    refIdxLXN = -1;
                    return;
                }

                mvLXN = (mvLX[mbPartIdxN, subMbPartIdxN, 0], mvLX[mbPartIdxN, subMbPartIdxN, 1]);
                refIdxLXN = refIdxLX[mbPartIdxN];

                if (dc.IsMbaffFieldMacroblock && _macroblockUtility.IsFrameMacroblock(mbAddrN))
                {
                    mvLXN.Y /= 2;
                    refIdxLXN *= 2;
                }
                else if (dc.MbAddrXFrameFlag && _macroblockUtility.IsFieldMacroblock(mbAddrN))
                {
                    mvLXN.Y *= 2;
                    refIdxLXN /= 2;
                }
            }
        }

        private static void DeriveMedianLumaMotionVectorPrediction(
            bool validA,
            bool validB,
            bool validC,
            ref MotionVector mvLXA, ref MotionVector mvLXB, ref MotionVector mvLXC,
            ref int refIdxLXA, ref int refIdxLXB, ref int refIdxLXC,
            int refIdxLX,
            out MotionVector mvpLX)
        {
            if (!validB && !validC && validA)
            {
                mvLXB = mvLXA;
                mvLXC = mvLXA;
                refIdxLXB = refIdxLXA;
                refIdxLXC = refIdxLXA;
            }

            if (refIdxLXA == refIdxLX)
            {
                mvpLX = mvLXA;
            }
            else if (refIdxLXB == refIdxLX)
            {
                mvpLX = mvLXB;
            }
            else if (refIdxLXC == refIdxLX)
            {
                mvpLX = mvLXC;
            }
            else
            {
                mvpLX = (0, 0);
                mvpLX.X = Util264.Median(mvLXA.X, mvLXB.X, mvLXC.X);
                mvpLX.X = Util264.Median(mvLXA.Y, mvLXB.Y, mvLXC.Y);
            }
        }

        private void DeriveCoLocated4x4SubMacroblockPartitions(out ReferencePicture? colPic, out int mbAddrCol, out MotionVector mvCol, out int refIdxCol, out MotionVectorScale vertMvScale)
        {
            mvCol = default;
            refIdxCol = 0;

            if (RefPicListL1 is null)
                throw new InvalidOperationException("Cannot co-locate 4x4 sub-macroblock partitions when the L1 reference picture list isn't available");

            if (RefPicListL1[0] is null)
                throw new InvalidOperationException("First reference picture in list 1 is unavailable");

            colPic = null;

            int topAbsDiffPoc = 0;
            int bottomAbsDiffPoc = 0;

            if (IsFrameOrComplementaryFieldPair(RefPicListL1[0]!))
            {
                var (firstRefPicL1Top, firstRefPicL1Bottom) = GetFields(RefPicListL1[0]!);

                if (firstRefPicL1Top is null || firstRefPicL1Bottom is null)
                    throw new InvalidOperationException("Cannot get top/bottom fields");

                topAbsDiffPoc = DiffPicOrderCnt(firstRefPicL1Top, CurrPic!);
                bottomAbsDiffPoc = DiffPicOrderCnt(firstRefPicL1Bottom, CurrPic!);

                if (sliceHeader.FieldPicFlag)
                {
                    if (RefPicListL1[0]!.PictureStructure is PictureStructure.TopField or PictureStructure.BottomField)
                    {
                        ReferencePicture? ownerPic = null;
                        for (int i = 0; i < this.RefPicListL1.Pictures.Count; i++)
                        {
                            var pic = this.RefPicListL1.Pictures[i];
                            if (pic is null)
                                continue;

                            if (pic.PairField is null)
                                continue;

                            if (pic.PairField.FrameNumber == RefPicListL1[0]!.FrameNumber)
                            {
                                ownerPic = pic;
                                break;
                            }
                        }

                        if (ownerPic is null)
                            throw new InvalidOperationException("Cannot find picture that owns the first one in the DPB");

                        colPic = ownerPic;
                    }
                    else
                    {
                        colPic = RefPicListL1[0]!;
                    }
                }
                else
                {
                    if (!RefPicListL1[0]!.IsField)
                    {
                        colPic = RefPicListL1[0]!;
                    }
                    else if (CurrPic is not null && RefPicListL1[0]!.IsComplementaryTo(CurrPic))
                    {
                        if (!this.MbFieldDecodingFlag)
                        {
                            if (topAbsDiffPoc < bottomAbsDiffPoc)
                            {
                                colPic = firstRefPicL1Top;
                            }
                            else if (topAbsDiffPoc >= bottomAbsDiffPoc)
                            {
                                colPic = firstRefPicL1Bottom;
                            }
                        }
                        else
                        {
                            if ((CurrMbAddr & 1) == 0)
                            {
                                colPic = firstRefPicL1Top;
                            }
                            else
                            {
                                colPic = firstRefPicL1Bottom;
                            }
                        }
                    }
                }
            }

            int luma8x8BlkIdx = !sps.Direct8X8InferenceFlag ? (4 * this.mbPartIdx + this.subMbPartIdx) : 5 * this.mbPartIdx;

            int xCol = 0, yCol = 0;
            Scanning.Inverse4x4LumaScan(luma8x8BlkIdx, ref xCol, ref yCol);

            int mbAddrCol1 = 2 * PicWidthInMbs * (CurrMbAddr / PicWidthInMbs) + (CurrMbAddr % PicWidthInMbs) + PicWidthInMbs * (yCol / 8);
            int mbAddrCol2 = 2 * CurrMbAddr + (yCol / 8);
            int mbAddrCol3 = 2 * CurrMbAddr + Int32Boolean.I32(sliceHeader.BottomFieldFlag);
            int mbAddrCol4 = PicWidthInMbs * (CurrMbAddr / (2 * PicWidthInMbs)) + (CurrMbAddr % PicWidthInMbs);
            int mbAddrCol5 = CurrMbAddr / 2;
            int mbAddrCol6 = 2 * (CurrMbAddr / 2) + ((topAbsDiffPoc < bottomAbsDiffPoc) ? 0 : 1);
            int mbAddrCol7 = 2 * (CurrMbAddr / 2) + (yCol / 8);

            bool fieldDecodingFlagX = this._macroblockUtility.IsFieldMacroblock(colPic!.Context.MbAddrX);

            mbAddrCol = 0;
            int yM = 0;
            vertMvScale = default;

            switch (PicCodingStruct(CurrPic!))
            {
                case PictureCodingStruct.Fld:
                    {
                        switch (PicCodingStruct(colPic!))
                        {
                            case PictureCodingStruct.Fld:
                                {
                                    mbAddrCol = CurrMbAddr;
                                    yM = yCol;
                                    vertMvScale = MotionVectorScale.OneToOne;
                                    break;
                                }

                            case PictureCodingStruct.Frm:
                                {
                                    mbAddrCol = mbAddrCol1;
                                    yM = (2 * yCol) % 16;
                                    vertMvScale = MotionVectorScale.FrmToFld;
                                    break;
                                }

                            case PictureCodingStruct.Afrm:
                                {
                                    if (this._derivationContext.MbAddrX == 2 * CurrMbAddr)
                                    {
                                        if (!fieldDecodingFlagX)
                                        {
                                            mbAddrCol = mbAddrCol2;
                                            yM = (2 * yCol) % 16;
                                            vertMvScale = MotionVectorScale.FrmToFld;
                                        }
                                        else
                                        {
                                            mbAddrCol = mbAddrCol3;
                                            yM = yCol;
                                            vertMvScale = MotionVectorScale.OneToOne;
                                        }
                                    }

                                    break;
                                }
                        }

                        break;
                    }

                case PictureCodingStruct.Frm:
                    {
                        switch (PicCodingStruct(colPic))
                        {
                            case PictureCodingStruct.Fld:
                                {
                                    mbAddrCol = mbAddrCol4;
                                    yM = 8 * (CurrMbAddr % 2) + 4 * (yCol / 8);
                                    vertMvScale = MotionVectorScale.FldToFrm;

                                    break;
                                }

                            case PictureCodingStruct.Frm:
                                {
                                    mbAddrCol = CurrMbAddr;
                                    yM = yCol;
                                    vertMvScale = MotionVectorScale.OneToOne;

                                    break;
                                }
                        }

                        break;
                    }

                case PictureCodingStruct.Afrm:
                    {
                        switch (PicCodingStruct(colPic))
                        {
                            case PictureCodingStruct.Fld:
                                {
                                    mbAddrCol = mbAddrCol5;
                                    if (!this.MbFieldDecodingFlag)
                                    {
                                        yM = 8 * (CurrMbAddr % 2) + 4 * (yCol / 8);
                                        vertMvScale = MotionVectorScale.FldToFrm;
                                    }
                                    else
                                    {
                                        yM = yCol;
                                        vertMvScale = MotionVectorScale.OneToOne;
                                    }

                                    break;
                                }

                            case PictureCodingStruct.Afrm:
                                {
                                    if (this._derivationContext.MbAddrX == CurrMbAddr)
                                    {
                                        if (!this.MbFieldDecodingFlag)
                                        {
                                            if (!fieldDecodingFlagX)
                                            {
                                                mbAddrCol = CurrMbAddr;
                                                yM = yCol;
                                                vertMvScale = MotionVectorScale.OneToOne;
                                            }
                                            else
                                            {
                                                mbAddrCol = mbAddrCol6;
                                                yM = 8 * (CurrMbAddr % 2) + 4 * (yCol / 8);
                                                vertMvScale = MotionVectorScale.FldToFrm;
                                            }
                                        }
                                        else
                                        {
                                            if (!fieldDecodingFlagX)
                                            {
                                                mbAddrCol = mbAddrCol7;
                                                yM = 2 * yCol % 16;
                                                vertMvScale = MotionVectorScale.FrmToFld;
                                            }
                                            else
                                            {
                                                mbAddrCol = CurrMbAddr;
                                                yM = yCol;
                                                vertMvScale = MotionVectorScale.OneToOne;
                                            }
                                        }
                                    }

                                    break;
                                }
                        }

                        break;
                    }
            }

            int mbTypeCol = (int)this._macroblockUtility.GetMbType(mbAddrCol);
            int subMbTypeCol = 0;
            if (mbTypeCol is P_8x8 or P_8x8ref0 or B_8x8)
                subMbTypeCol = (int)this._macroblockUtility.GetSubMbType(mbAddrCol);

            int mbPartIdxCol = colPic!.MbPartIdx;
            int subMbPartIdxCol = colPic!.SubMbPartIdx;

            Scanning.DeriveMacroblockAndSubMacroblockPartitionIndices(
                this.sliceType, xCol, yM, this.mbType, this.subMbTypeArray, ref mbTypeCol, ref subMbTypeCol);

            if (this._macroblockUtility.IsCodedWithIntra(mbAddrCol))
            {
                mvCol = (0, 0);
                refIdxCol = -1;
            }
            else
            {
                bool predFlagL0Col = this.predFlagL0Array[mbPartIdxCol];
                bool predFlagL1Col = this.predFlagL1Array[mbPartIdxCol];

                if (predFlagL0Col)
                {
                    mvCol = (this.mvL0[mbPartIdxCol, subMbPartIdxCol, 0], this.mvL0[mbPartIdxCol, subMbPartIdxCol, 1]);
                    refIdxCol = this.refIdxL0[mbPartIdxCol];
                }
                else if (predFlagL1Col)
                {
                    mvCol = (this.mvL1[mbPartIdxCol, subMbPartIdxCol, 0], this.mvL1[mbPartIdxCol, subMbPartIdxCol, 1]);
                    refIdxCol = this.refIdxL1[mbPartIdxCol];
                }
            }
        }

        private static PictureCodingStruct PicCodingStruct(ReferencePicture refPic)
        {
            return (refPic.SliceHeader.FieldPicFlag, refPic.Sps.MbAdaptiveFrameFieldFlag) switch
            {
                (true, false) or (true, true) => PictureCodingStruct.Fld,
                (false, false) => PictureCodingStruct.Frm,
                (false, true) => PictureCodingStruct.Afrm
            };
        }

        private int DiffPicOrderCnt(ReferencePicture x, ReferencePicture y) =>
            Util264.PicOrderCnt(x.Sps, x.Pps, x.SliceHeader, pocCtx.PrevPicOrderCntLsb, pocCtx.PrevPicOrderCntMsb, x.NalUnit.NalRefIdc)
            - Util264.PicOrderCnt(y.Sps, y.Pps, y.SliceHeader, pocCtx.PrevPicOrderCntLsb, pocCtx.PrevPicOrderCntMsb, y.NalUnit.NalRefIdc);

        private bool IsFrameOrComplementaryFieldPair(ReferencePicture refPic)
        {
            return !refPic.IsField || (CurrPic is not null && refPic.IsComplementaryTo(CurrPic));
        }

        private static (ReferencePicture? TopField, ReferencePicture? BottomField) GetFields(ReferencePicture referencePicture)
        {
            if (!referencePicture.IsField)
            {
                return (null, null);
            }

            ReferencePicture? topField = referencePicture.PictureStructure == PictureStructure.TopField
                ? referencePicture
                : referencePicture.PairField;

            ReferencePicture? bottomField = referencePicture.PictureStructure == PictureStructure.BottomField
                ? referencePicture
                : referencePicture.PairField;

            return (topField, bottomField);
        }

        private void DeriveLumaMotionVectorPrediction(
            int refIdxLX, bool listSuffixFlag, int currSubMbType,
            out MotionVector mvpLX)
        {
            DeriveMotionDataOfNeighboringPartitions(
                currSubMbType, listSuffixFlag,
                out _, out _, out _,
                out MotionVector mvL0A, out MotionVector mvL0B, out MotionVector mvL0C,
                out MotionVector mvL1A, out MotionVector mvL1B, out MotionVector mvL1C,
                out int refIdxL0A, out int refIdxL0B, out int refIdxL0C,
                out int refIdxL1A, out int refIdxL1B, out int refIdxL1C,
                out bool validA, out bool validB, out bool validC);

            MotionVector mvLXA = listSuffixFlag ? mvL1A : mvL0A;
            MotionVector mvLXB = listSuffixFlag ? mvL1B : mvL0B;
            MotionVector mvLXC = listSuffixFlag ? mvL1C : mvL0C;

            int refIdxLXA = listSuffixFlag ? refIdxL1A : refIdxL0A;
            int refIdxLXB = listSuffixFlag ? refIdxL1B : refIdxL0B;
            int refIdxLXC = listSuffixFlag ? refIdxL1C : refIdxL0C;

            if (Util264.MbPartWidth(this.mbType, this.sliceType) == 16 && Util264.MbPartHeight(this.mbType, this.sliceType) == 8 && this.mbPartIdx == 0 && refIdxLXB == refIdxLX)
            {
                mvpLX = mvLXB;
            }
            else if (Util264.MbPartWidth(this.mbType, this.sliceType) == 16 && Util264.MbPartHeight(this.mbType, this.sliceType) == 8 && this.mbPartIdx == 1 && refIdxLXA == refIdxLX)
            {
                mvpLX = mvLXA;
            }
            else if (Util264.MbPartWidth(this.mbType, this.sliceType) == 8 && Util264.MbPartHeight(this.mbType, this.sliceType) == 16 && this.mbPartIdx == 0 && refIdxLXA == refIdxLX)
            {
                mvpLX = mvLXA;
            }
            else
            {
                DeriveMedianLumaMotionVectorPrediction(
                    validA, validB, validC, ref mvLXA, ref mvLXB, ref mvLXC, ref refIdxLXA, ref refIdxLXB, ref refIdxLXC, refIdxLX, out mvpLX);
            }
        }

        private void DeriveLumaMotionVectorsForSkippedMacroblocksInPAndSPSlices(out MotionVector mvL0, out int refIdxL0)
        {
            refIdxL0 = 0;

            DeriveMotionDataOfNeighboringPartitions(
                na, false,
                out _, out _, out _,
                out MotionVector mvL0A, out MotionVector mvL0B, out _,
                out _, out _, out _,
                out int refIdxL0A, out int refIdxL0B, out _,
                out _, out _, out _,
                out bool validA, out bool validB, out _);

            if (!validA || !validB || (refIdxL0A == 0 && mvL0A == (0, 0)) || (refIdxL0B == 0 && mvL0B == (0, 0)))
            {
                mvL0 = (0, 0);
            }
            else
            {
                DeriveLumaMotionVectorPrediction(refIdxL0, false, na, out mvL0);
            }
        }
    }
}
