using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Pictures;
using ContentDotNet.Extensions.H264.Utilities;
using ContentDotNet.Primitives;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static ContentDotNet.Extensions.H264.SliceTypes;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal partial class IntraInterDecoder
{
    public sealed partial class Inter
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

        private readonly bool[] predFlagL0Array = new bool[16];
        private readonly bool[] predFlagL1Array = new bool[16];

        private DerivationContext _derivationContext;
        private readonly IMacroblockUtility _macroblockUtility;

        private Size frameSize = default;

        public ReferencePicture? CurrPic { get; set; }

        private SequenceParameterSet sps = default;
        private PictureParameterSet pps = default;
        private NalUnit nalu = new(0, 0, false, false, null, default);
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
        private void DeriveMotionDataOfNeighboringPartitions(
            int mbPartIdx, int subMbPartIdx,
            int currSubMbType, bool listSuffixFlag,
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

        private void DeriveCoLocated4x4SubMacroblockPartitions(int mbPartIdx, int subMbPartIdx, out ReferencePicture? colPic, out int mbAddrCol, out MotionVector mvCol, out int refIdxCol, out MotionVectorScale vertMvScale)
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

            int luma8x8BlkIdx = !sps.Direct8X8InferenceFlag ? (4 * mbPartIdx + subMbPartIdx) : 5 * mbPartIdx;

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

        public int DiffPicOrderCnt(ReferencePicture x, ReferencePicture y) =>
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
                this.mbPartIdx, this.subMbPartIdx,
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

        private void DeriveLumaMotionVectorsForB(int mbPartIdx, int subMbPartIdx, out int refIdxL0, out int refIdxL1, out MotionVector mvL0, out MotionVector mvL1, out int subMvCnt, out bool predFlagL0, out bool predFlagL1)
        {
            if (this.sliceHeader.DirectSpatialMvPredFlag)
            {
                DeriveSpatialDirectLumaMotionVectorAndReferenceIndexPredictionMode(
                    this.mbPartIdx, this.subMbPartIdx,
                    out refIdxL0, out refIdxL1, out mvL0, out mvL1, out subMvCnt, out predFlagL0, out predFlagL1);
            }
            else
            {
                DeriveTemporalDirectLumaMotionVectorAndReferenceIndexPredictionMode(
                    mbPartIdx, subMbPartIdx,
                    out refIdxL0, out refIdxL1, out mvL0, out mvL1, out predFlagL0, out predFlagL1);

                if (subMbPartIdx == 0)
                    subMvCnt = 2;
                else
                    subMvCnt = 0;
            }
        }

        private void DeriveSpatialDirectLumaMotionVectorAndReferenceIndexPredictionMode(int mbPartIdx, int subMbPartIdx, out int refIdxL0, out int refIdxL1, out MotionVector mvL0, out MotionVector mvL1, out int subMvCnt, out bool predFlagL0, out bool predFlagL1)
        {
            int currSubMbType = this.subMbTypeArray[mbPartIdx];
            DeriveMotionDataOfNeighboringPartitions(
                0, 0, currSubMbType, false,
                out _, out _, out _,
                out _, out _, out _,
                out _, out _, out _,
                out var refIdxL0A, out var refIdxL0B, out var refIdxL0C,
                out _, out _, out _,
                out _, out _, out _);
            DeriveMotionDataOfNeighboringPartitions(
                0, 0, currSubMbType, true,
                out _, out _, out _,
                out _, out _, out _,
                out _, out _, out _,
                out _, out _, out _,
                out var refIdxL1A, out var refIdxL1B, out var refIdxL1C,
                out _, out _, out _);

            refIdxL0 = MinPositive(refIdxL0A, MinPositive(refIdxL0B, refIdxL0C));
            refIdxL1 = MinPositive(refIdxL1A, MinPositive(refIdxL1B, refIdxL1C));
            bool directZeroPredictionFlag = false;

            if (refIdxL0 < 0 && refIdxL1 < 0)
            {
                refIdxL0 = 0;
                refIdxL1 = 0;
                directZeroPredictionFlag = true;
            }

            DeriveCoLocated4x4SubMacroblockPartitions(mbPartIdx, subMbPartIdx, out _, out _, out var mvCol, out var refIdxCol, out _);

            bool colZeroFlag =
                (RefPicListL1 is not null && RefPicListL1[0]?.ReferenceType == PictureReferenceType.ShortTerm)
                && refIdxCol == 0
                && (mvCol.X is >= -1 and <= 1 && mvCol.Y is >= -1 and <= 1);

            if (directZeroPredictionFlag || refIdxL0 < 0 || (refIdxL0 == 0 && colZeroFlag))
                mvL0 = (0, 0);
            else
                DeriveLumaMotionVectorPrediction(refIdxL0, false, currSubMbType, out mvL0);

            if (directZeroPredictionFlag || refIdxL1 < 0 || (refIdxL1 == 0 && colZeroFlag))
                mvL1 = (0, 0);
            else
                DeriveLumaMotionVectorPrediction(refIdxL1, false, currSubMbType, out mvL1);

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
                predFlagL1 = false;
            }

            if (subMbPartIdx != 0)
                subMvCnt = 0;
            else
                subMvCnt = Int32Boolean.I32(predFlagL0) + Int32Boolean.I32(predFlagL1);
        }

        private int MapColToList0(ReferencePicture refPicCol, MotionVectorScale vertMvScale)
        {
            if (vertMvScale == MotionVectorScale.OneToOne)
            {
                if (this._macroblockUtility.IsFieldMacroblock(CurrMbAddr) && !this.sliceHeader.FieldPicFlag)
                {
                    int refIdxL0Frm = -1;
                    for (int i = 0; i < this.RefPicListL0.Pictures.Count; i++)
                    {
                        var curr = this.RefPicListL0[i];
                        if (curr is null)
                            continue;

                        if (curr.PairField is null)
                            continue;

                        if (!curr.PairField.IsField || (curr.PairField.IsField && curr.PairField.IsComplementaryTo(refPicCol)))
                        {
                            refIdxL0Frm = i;
                            break;
                        }
                    }

                    if (refIdxL0Frm == -1)
                        throw new InvalidOperationException("refIdxL0Frm is -1");

                    // Wish I knew how to check reference picture parity.
                    return (refIdxL0Frm << 1) + 1;
                }
                else
                {
                    int min = -1;
                    for (int i = 0; i < this.RefPicListL0.Pictures.Count; i++)
                    {
                        var curr = this.RefPicListL0[i];
                        if (curr is null)
                            continue;

                        if (curr.PairField?.FrameNumber == refPicCol.FrameNumber)
                        {
                            min = i;
                            break;
                        }
                    }

                    if (min == -1)
                        throw new InvalidOperationException("min is -1");

                    return min;
                }
            }
            else if (vertMvScale == MotionVectorScale.FrmToFld)
            {
                if (sliceHeader.FieldPicFlag)
                {
                    int min = -1;
                    for (int i = 0; i < this.RefPicListL0.Pictures.Count; i++)
                    {
                        var curr = this.RefPicListL0[i];
                        if (curr is null)
                            continue;

                        if (curr.PairField?.FrameNumber == refPicCol.FrameNumber)
                        {
                            min = i;
                            break;
                        }
                    }

                    if (min == -1)
                        throw new InvalidOperationException("min is -1");

                    return min << 1;
                }
                else
                {
                    int min = -1;
                    for (int i = 0; i < this.RefPicListL0.Pictures.Count; i++)
                    {
                        var curr = this.RefPicListL0[i];
                        if (curr is null)
                            continue;

                        if (curr.PairField?.FrameNumber == refPicCol.FrameNumber)
                        {
                            min = i;
                            break;
                        }
                    }

                    if (min == -1)
                        throw new InvalidOperationException("min is -1");

                    return min << 1;
                }
            }
            else
            {
                int min = -1;
                for (int i = 0; i < this.RefPicListL0.Pictures.Count; i++)
                {
                    var curr = this.RefPicListL0[i];
                    if (curr is null)
                        continue;

                    if (curr.PairField is null)
                        continue;

                    if (!curr.PairField.IsField || (curr.PairField.IsField && curr.PairField.IsComplementaryTo(refPicCol)))
                    {
                        min = i;
                        break;
                    }
                }

                if (min == -1)
                    throw new InvalidOperationException("min is -1");

                return min;
            }
        }

        private void DeriveTemporalDirectLumaMotionVectorAndReferenceIndexPredictionMode(
            int mbPartIdx, int subMbPartIdx,
            out int refIdxL0, out int refIdxL1, out MotionVector mvL0, out MotionVector mvL1, out bool predFlagL0, out bool predFlagL1)
        {
            DeriveCoLocated4x4SubMacroblockPartitions(mbPartIdx, subMbPartIdx, out var colPic, out _, out var mvCol, out var refIdxCol, out var vertMvScale);

            refIdxL0 = (refIdxCol < 0) ? 0 : MapColToList0(colPic!, vertMvScale);
            refIdxL1 = 0;

            if (vertMvScale == MotionVectorScale.FrmToFld)
            {
                mvCol.Y /= 2;
            }
            else if (vertMvScale == MotionVectorScale.FldToFrm)
            {
                mvCol.Y *= 2;
            }

            ReferencePicture currPicOrField, pic0, pic1;

            if (!this.sliceHeader.FieldPicFlag && this._macroblockUtility.IsFieldMacroblock(CurrMbAddr))
            {
                currPicOrField = CurrPic!.PairField!;
                pic1 = RefPicListL1![0]!;
                pic0 = RefPicListL0![refIdxL0 / 2]!.PairField!;
            }
            else
            {
                currPicOrField = CurrPic!;
                pic1 = RefPicListL1![0]!;
                pic0 = RefPicListL0![0]!;
            }

            if (this.RefPicListL0[refIdxL0]!.ReferenceType == PictureReferenceType.LongTerm ||
                DiffPicOrderCnt(pic0, pic1) == 0)
            {
                mvL0 = mvCol;
                mvL1 = (0, 0);
            }
            else
            {
                var tb = Util264.Clip3(-128, 127, DiffPicOrderCnt(currPicOrField, pic0));
                var td = Util264.Clip3(-128, 127, DiffPicOrderCnt(pic1, pic0));

                var tx = (16384 + Math.Abs(td / 2)) / td;
                var distScaleFactor = Util264.Clip3(-1024, 1023, (tb * tx + 32) >> 6);
                mvL0 = ((distScaleFactor * mvCol.X + 128) >> 8, (distScaleFactor * mvCol.Y + 128) >> 8);
                mvL1 = (mvL0.X - mvCol.X, mvL0.Y - mvCol.Y);
            }

            predFlagL0 = true;
            predFlagL1 = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int MinPositive(int x, int y) => x >= 0 && y >= 0 ? Math.Min(x, y) : Math.Max(x, y);

        private void DeriveLumaMotionVectorsForSkippedMacroblocksInPAndSPSlices(out MotionVector mvL0, out int refIdxL0)
        {
            refIdxL0 = 0;

            DeriveMotionDataOfNeighboringPartitions(
                this.mbPartIdx, this.subMbPartIdx,
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

        private void DeriveChromaMotionVectors(int chromaArrayType, bool l1, MotionVector mvLX, int refIdxLX, out MotionVector mvCLX)
        {
            mvCLX = default;
            mvCLX.X = mvLX.X;

            if (chromaArrayType != 0 || this._macroblockUtility.IsFrameMacroblock(CurrMbAddr))
            {
                mvCLX.Y = mvLX.Y;
            }
            else
            {
                if ((l1 ? RefPicListL1! : RefPicListL0)[refIdxLX]!.PictureStructure == PictureStructure.TopField &&
                    CurrPic!.PictureStructure == PictureStructure.BottomField)
                {
                    mvCLX.Y = mvLX.Y + 2;
                }
                else if ((l1 ? RefPicListL1! : RefPicListL0)[refIdxLX]!.PictureStructure == PictureStructure.BottomField &&
                         CurrPic!.PictureStructure == PictureStructure.TopField)
                {
                    mvCLX.Y = mvLX.Y - 2;
                }
                else
                {
                    mvCLX.Y = mvLX.Y;
                }
            }
        }

        private void DeriveMotionVectorComponentsAndReferenceIndices(
            int mbPartIdx, int subMbPartIdx, int mbType, GeneralSliceType sliceType, bool transformSize8x8Flag,
            ContainerMatrix4x4x2 mvdL0, ContainerMatrix4x4x2 mvdL1, int chromaArrayType,
            out MotionVector mvL0, out MotionVector mvL1,
            out MotionVector mvCL0, out MotionVector mvCL1,
            out int refIdxL0, out int refIdxL1,
            out bool predFlagL0, out bool predFlagL1,
            out int subMvCnt)
        {
            mvL0 = default;
            mvL1 = default;
            mvCL0 = default;
            mvCL1 = default;

            if (mbType == P_Skip)
            {
                DeriveLumaMotionVectorsForSkippedMacroblocksInPAndSPSlices(out mvL0, out refIdxL0);
                predFlagL0 = true;

                predFlagL1 = true;
                mvL1 = default;
                refIdxL1 = 0;

                subMvCnt = 1;
            }
            else if (mbType is B_Skip or B_Direct_16x16 || this.subMbTypeArray[mbPartIdx] == B_Direct_8x8)
            {
                DeriveLumaMotionVectorsForB(mbPartIdx, subMbPartIdx, out refIdxL0, out refIdxL1, out mvL0, out mvL1, out subMvCnt, out predFlagL0, out predFlagL1);
            }
            else
            {
                if (Util264.MbPartPredMode(mbType, mbPartIdx, transformSize8x8Flag, sliceType) is Pred_L0 or Pred_L1 or BiPred ||
                    Util264.SubMbPredMode(this.subMbTypeArray[mbPartIdx], sliceType) is Pred_L0 or Pred_L1 or BiPred)
                {
                    refIdxL0 = this.refIdxL0[mbPartIdx];
                    refIdxL1 = this.refIdxL1[mbPartIdx];
                    predFlagL0 = true;
                    predFlagL1 = true;
                }
                else
                {
                    refIdxL0 = -1;
                    refIdxL1 = -1;
                    predFlagL0 = false;
                    predFlagL1 = false;
                }

                subMvCnt = Int32Boolean.I32(predFlagL0) + Int32Boolean.I32(predFlagL1);

                int currSubMbType = mbType == B_8x8 ? this.subMbTypeArray[mbPartIdx] : na;

                if (predFlagL0)
                {
                    DeriveLumaMotionVectorPrediction(refIdxL0, false, currSubMbType, out var mvpL0);
                    mvL0.X = mvpL0.X + mvdL0[mbPartIdx, subMbPartIdx, 0];
                    mvL0.Y = mvpL0.Y + mvdL0[mbPartIdx, subMbPartIdx, 1];
                }

                if (predFlagL1)
                {
                    DeriveLumaMotionVectorPrediction(refIdxL1, false, currSubMbType, out var mvpL1);
                    mvL1.X = mvpL1.X + mvdL1[mbPartIdx, subMbPartIdx, 0];
                    mvL1.Y = mvpL1.Y + mvdL1[mbPartIdx, subMbPartIdx, 1];
                }
            }

            if (chromaArrayType != 0)
            {
                if (predFlagL0)
                {
                    DeriveChromaMotionVectors(chromaArrayType, false, mvL0, refIdxL0, out mvCL0);
                }

                if (predFlagL1)
                {
                    DeriveChromaMotionVectors(chromaArrayType, false, mvL1, refIdxL1, out mvCL1);
                }
            }
        }

        public void Decode(bool isSubMacroblock, int mbIndexX, int mbIndexY, MacroblockSizeChroma size, MacroblockLayer layer, ChromaFormat chromaFormat, Matrix predL, Matrix predCb, Matrix predCr)
        {
            if ((!isSubMacroblock && layer.Prediction is null) ||
                (isSubMacroblock && layer.SubMacroblockPrediction is null))
                throw new InvalidOperationException("No prediction is available");

            GeneralSliceType sliceType = sliceHeader.GetSliceType();
            int chromaArrayType = (int)sps.GetChromaArrayType();
            int mbType = (int)layer.MbType;
            bool transformSize8x8Flag = layer.TransformSize8x8Flag;
            uint sliceTypeNum = sliceHeader.SliceType;

            bool weightedPredFlag = pps.WeightedPredFlag;
            uint weightedBiPredIdc = pps.WeightedBiPredIdc;

            ContainerMatrix4x4x2 mvdL0 = isSubMacroblock ? layer.SubMacroblockPrediction!.Value.MvdL0 : layer.Prediction!.Value.MvdL0;
            ContainerMatrix4x4x2 mvdL1 = isSubMacroblock ? layer.SubMacroblockPrediction!.Value.MvdL1 : layer.Prediction!.Value.MvdL1;

            bool fieldPicFlag = sliceHeader.FieldPicFlag;
            bool currentMbIsField = MbFieldDecodingFlag;
            bool currentMbIsFrame = !currentMbIsField;

            PictureStructure picStruct = sliceHeader.GetPictureStructure();

            Span<int> subMbType = stackalloc int[4];
            if (layer.SubMacroblockPrediction is not null)
                for (int i = 0; i < 4; i++)
                    subMbType[i] = (int)layer.SubMacroblockPrediction!.Value.SubMbType[i];

            int bitDepthY = (int)(sps.BitDepthLumaMinus8 + 8);
            bool mbaffFrameFlag = sps.MbAdaptiveFrameFieldFlag;
            PredWeightTable predWeightTable = sliceHeader.PredWeightTable!.Value;

            for (int i = 0; i <= (mbType is B_Skip or B_Direct_16x16 && IsB(sliceTypeNum) ? 3 : Util264.NumMbPart(mbType, sliceType)); i++)
            {
                int mbPartIdx = i;
                int subMbPartIdx = layer.SubMacroblockPrediction is not null ? subMbType[mbPartIdx] : 0;
                this.mbPartIdx = mbPartIdx;
                this.subMbPartIdx = subMbPartIdx;

                _Core(subMbType);

                void _Core(Span<int> subMbType)
                {
                    int partWidth = 0;
                    int partHeight = 0;
                    if (mbType is not P_8x8 and not P_8x8ref0 and not B_Skip and not B_Direct_16x16 and not B_8x8)
                    {
                        partWidth = Util264.MbPartWidth(mbType, sliceType);
                        partHeight = Util264.MbPartHeight(mbType, sliceType);
                    }
                    else if (mbType is not P_8x8 and not P_8x8ref0 || (mbType == B_8x8 && subMbTypeArray[mbPartIdx] != B_Direct_8x8))
                    {
                        partWidth = Util264.SubMbPartWidth(subMbType[mbPartIdx], sliceType);
                        partHeight = Util264.SubMbPartHeight(subMbType[mbPartIdx], sliceType);
                    }
                    else
                    {
                        partWidth = 4;
                        partHeight = 4;
                    }

                    int partWidthC = 0;
                    int partHeightC = 0;
                    if (chromaArrayType != 0)
                    {
                        partWidthC = partWidth / chromaFormat.ChromaWidth;
                        partHeightC = partHeight / chromaFormat.ChromaHeight;
                    }

                    int MvCnt = 0;

                    DeriveMotionVectorComponentsAndReferenceIndices(
                        mbPartIdx,
                        subMbPartIdx,
                        mbType,
                        sliceType,
                        transformSize8x8Flag,
                        mvdL0,
                        mvdL1,
                        chromaArrayType,
                        out var mvL0,
                        out var mvL1,
                        out var mvCL0,
                        out var mvCL1,
                        out var refIdxL0,
                        out var refIdxL1,
                        out var predFlagL0,
                        out var predFlagL1,
                        out var subMvCnt
                    );

                    MvCnt += subMvCnt;

                    int logWDc = 0;
                    Vector64<int> o = Vector64<int>.Zero;
                    Vector64<int> w = Vector64<int>.Zero;

                    if ((weightedPredFlag && (sliceTypeNum % 5) is 0 or 3) ||
                        (weightedBiPredIdc > 0 && sliceTypeNum % 5 == 1))
                    {
                        WeightedPrediction.Apply(refIdxL0, refIdxL1, fieldPicFlag, currentMbIsField, CurrPic!, RefPicListL0, RefPicListL1!, predFlagL0, predFlagL1, chromaArrayType, sliceTypeNum, weightedPredFlag, weightedBiPredIdc, this, picStruct, predWeightTable, mbaffFrameFlag, bitDepthY, out logWDc, out w, out o);
                    }

                    Span<int> predPartL0LBacking = stackalloc int[16 * 16];
                    Span<int> predPartL0CBBacking = stackalloc int[16 * 16];
                    Span<int> predPartL0CRBacking = stackalloc int[16 * 16];
                    Span<int> predPartL1LBacking = stackalloc int[16 * 16];
                    Span<int> predPartL1CBBacking = stackalloc int[16 * 16];
                    Span<int> predPartL1CRBacking = stackalloc int[16 * 16];
                    Span<int> predPartLBacking = stackalloc int[16 * 16];
                    Span<int> predPartCbBacking = stackalloc int[16 * 16];
                    Span<int> predPartCrBacking = stackalloc int[16 * 16];
                    Matrix16x16 predPartL0L = new(predPartL0LBacking);
                    Matrix16x16 predPartL0CB = new(predPartL0CBBacking);
                    Matrix16x16 predPartL0CR = new(predPartL0CRBacking);
                    Matrix16x16 predPartL1L = new(predPartL1LBacking);
                    Matrix16x16 predPartL1CB = new(predPartL1CBBacking);
                    Matrix16x16 predPartL1CR = new(predPartL1CRBacking);
                    Matrix16x16 predPartL = new(predPartLBacking);
                    Matrix16x16 predPartCb = new(predPartCbBacking);
                    Matrix16x16 predPartCr = new(predPartCrBacking);

                    DecodeInterPredictionSamples(
                        subMbPartIdx,
                        mbIndexX,
                        mbIndexY,
                        fieldPicFlag,
                        predFlagL0,
                        predFlagL1,
                        currentMbIsFrame,
                        picStruct,
                        partWidth,
                        partHeight,
                        partWidthC,
                        partHeightC,
                        refIdxL0,
                        refIdxL1,
                        size,
                        mvL0,
                        mvCL0,
                        mvL1,
                        mvCL1,
                        mbaffFrameFlag,
                        in chromaFormat,
                        sps,
                        in sliceHeader,
                        in pps,
                        chromaArrayType,
                        predPartL0L,
                        predPartL0CB,
                        predPartL0CR,
                        predPartL1L,
                        predPartL1CB,
                        predPartL1CR,
                        logWDc,
                        w,
                        o,
                        predPartL,
                        predPartCb,
                        predPartCr);

                    this.mvL0[mbPartIdx, subMbPartIdx, 0] = mvL0.X;
                    this.mvL0[mbPartIdx, subMbPartIdx, 1] = mvL0.Y;
                    this.mvL1[mbPartIdx, subMbPartIdx, 0] = mvL1.X;
                    this.mvL1[mbPartIdx, subMbPartIdx, 1] = mvL1.Y;
                    this.refIdxL0[mbPartIdx] = refIdxL0;
                    this.refIdxL1[mbPartIdx] = refIdxL1;
                    this.predFlagL0Array[mbPartIdx] = predFlagL0;
                    this.predFlagL1Array[mbPartIdx] = predFlagL1;

                    int xP = 0;
                    int yP = 0;
                    Scanning.InverseMacroblockPartitionScan(mbPartIdx, mbType, sliceType, ref xP, ref yP);

                    int xS = 0;
                    int yS = 0;
                    Scanning.InverseSubMacroblockPartitionScan(subMbPartIdx, this.subMbTypeArray, mbPartIdx, mbType, sliceType, ref xS, ref yS);

                    for (int x = 0; x <= partWidth - 1; x++)
                    {
                        for (int y = 0; y <= partHeight - 1; y++)
                        {
                            predL[xP + xS + x, yP + yS + y] = predPartL[x, y];
                        }
                    }

                    for (int x = 0; x <= partWidthC - 1; x++)
                    {
                        for (int y = 0; y <= partHeightC - 1; y++)
                        {
                            predCb[xP + xS + x, yP + yS + y] = predPartCb[x, y];
                            predCr[xP + xS + x, yP + yS + y] = predPartCr[x, y];
                        }
                    }
                }
            }
        }

        public ReferencePicture SelectReferencePicture(int refIdxLX, bool l0, bool fieldPicFlag, bool currentMbIsFrame, PictureStructure macroblockPictureStructure)
        {
            if (fieldPicFlag)
                return (l0 ? RefPicListL0 : RefPicListL1)?[refIdxLX] ?? throw new VideoCodecDecoderException("Invalid reference picture");
        
            if (currentMbIsFrame)
                return (l0 ? RefPicListL0 : RefPicListL1)?[refIdxLX] ?? throw new VideoCodecDecoderException("Invalid reference picture");
        
            ReferencePicture refFrame = (l0 ? RefPicListL0 : RefPicListL1)?[refIdxLX / 2] ?? throw new VideoCodecDecoderException("Invalid reference picture");
            if (refIdxLX % 2 == 0)
                if (!ReferencePicture.HasMatchingParity(refFrame, macroblockPictureStructure))
                    throw new VideoCodecDecoderException("Parity does not match");
            else
                if (ReferencePicture.HasMatchingParity(refFrame, macroblockPictureStructure))
                    throw new VideoCodecDecoderException("Parity matches");

            return refFrame;
        }

        public ReferencePicture DecodeInterPredictionSamples(
            int subMbPartIdx, int mbIndexX, int mbIndexY, bool fieldPicFlag, bool predFlagL0, bool predFlagL1, bool currentMbIsFrame, PictureStructure macroblockPictureStructure,
            int partWidth,
            int partHeight,
            int partWidthC,
            int partHeightC,
            int refIdxL0,
            int refIdxL1,
            MacroblockSizeChroma size,
            MotionVector mvL0,
            MotionVector mvCL0,
            MotionVector mvL1,
            MotionVector mvCL1,
            bool mbaffFrameFlag,
            in ChromaFormat chromaFormat,
            SequenceParameterSet sps,
            in SliceHeader header,
            in PictureParameterSet pps,
            int chromaArrayType,
            Matrix16x16 predPartL0L,
            Matrix16x16 predPartL0CB,
            Matrix16x16 predPartL0CR,
            Matrix16x16 predPartL1L,
            Matrix16x16 predPartL1CB,
            Matrix16x16 predPartL1CR,
            int logWDc,
            Vector64<int> w,
            Vector64<int> o,
            Matrix16x16 predPartL,
            Matrix16x16 predPartCb,
            Matrix16x16 predPartCr)
        {
            int bitDepthY = (int)(sps.BitDepthLumaMinus8 + 8);
            int bitDepthC = (int)(sps.BitDepthChromaMinus8 + 8);

            uint colorPlaneId = header.ColorPlaneId;
            bool isPSPSlice = IsP(header.SliceType) || IsSP(header.SliceType);
            bool isBSlice = IsB(header.SliceType);

            if (predFlagL0)
            {
                var refPic = SelectReferencePicture(refIdxL0, false, fieldPicFlag, currentMbIsFrame, macroblockPictureStructure);
                InterpolateFractionalSample(mbIndexX, mbIndexY, size, subMbPartIdx, partWidth, partHeight, partWidthC, partHeightC, mvL0, mvCL0, refPic.Frame.Y, refPic.Frame.U, refPic.Frame.V, sps, mbaffFrameFlag, MbFieldDecodingFlag, bitDepthY, in chromaFormat, chromaArrayType, predPartL0L, predPartL0CB, predPartL0CR);

                PredictWeightedSample(sps.SeparateColourPlaneFlag, colorPlaneId, isPSPSlice, isBSlice, pps.WeightedPredFlag, pps.WeightedBiPredIdc, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, logWDc, w, o, partWidth, partHeight, partWidthC, partHeightC, bitDepthY, bitDepthC, predPartL, predPartCb, predPartCr);

                return refPic;
            }
            
            if (predFlagL1)
            {
                var refPic = SelectReferencePicture(refIdxL1, false, fieldPicFlag, currentMbIsFrame, macroblockPictureStructure);
                InterpolateFractionalSample(mbIndexX, mbIndexY, size, subMbPartIdx, partWidth, partHeight, partWidthC, partHeightC, mvL1, mvCL1, refPic.Frame.Y, refPic.Frame.U, refPic.Frame.V, sps, mbaffFrameFlag, MbFieldDecodingFlag, bitDepthY, in chromaFormat, chromaArrayType, predPartL1L, predPartL1CB, predPartL1CR);

                PredictWeightedSample(sps.SeparateColourPlaneFlag, colorPlaneId, isPSPSlice, isBSlice, pps.WeightedPredFlag, pps.WeightedBiPredIdc, predFlagL1, predFlagL1, predPartL1L, predPartL1CB, predPartL1CR, predPartL1L, predPartL1CB, predPartL1CR, logWDc, w, o, partWidth, partHeight, partWidthC, partHeightC, bitDepthY, bitDepthC, predPartL, predPartCb, predPartCr);

                return refPic;
            }

            throw new InvalidOperationException("Nothing to predict");
        }

        public static void PredictWeightedSample(bool separateColorPlaneFlag, uint colorPlaneId, bool isPSPSlice, bool isBSlice, bool weightedPredFlag, uint weightedBiPredIdc, bool predFlagL0, bool predFlagL1, Matrix16x16 predPartL0L, Matrix16x16 predPartL0CB, Matrix16x16 predPartL0CR, Matrix16x16 predPartL1L, Matrix16x16 predPartL1CB, Matrix16x16 predPartL1CR, int logWDc, Vector64<int> w, Vector64<int> o, int partWidth, int partHeight, int partWidthC, int partHeightC, int bitDepthY, int bitDepthC,
            Matrix16x16 predPartL, Matrix16x16 predPartCb, Matrix16x16 predPartCr)
        {
            bool yDerived = (separateColorPlaneFlag && colorPlaneId == 0) || !separateColorPlaneFlag;
            bool cbDerived = (separateColorPlaneFlag && colorPlaneId == 1) || !separateColorPlaneFlag;
            bool crDerived = (separateColorPlaneFlag && colorPlaneId == 2) || !separateColorPlaneFlag;

            if (isPSPSlice && predFlagL0)
            {
                if (weightedPredFlag)
                    PredictDefaultWeightedSample(yDerived, cbDerived, partWidth, partHeight, partWidthC, partHeightC, crDerived, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, predPartL, predPartCb, predPartCr);
                else
                    PredictWeightedSample2(yDerived, cbDerived, o, w, logWDc, partWidth, partHeight, partWidthC, partHeightC, crDerived, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, predPartL, predPartCb, predPartCr, bitDepthY, bitDepthC);
            }

            if (isBSlice && (predFlagL0 || predFlagL1))
            {
                if (weightedBiPredIdc == 0u)
                {
                    PredictDefaultWeightedSample(yDerived, cbDerived, partWidth, partHeight, partWidthC, partHeightC, crDerived, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, predPartL, predPartCb, predPartCr);
                }
                else if (weightedBiPredIdc == 1u)
                {
                    PredictWeightedSample2(yDerived, cbDerived, o, w, logWDc, partWidth, partHeight, partWidthC, partHeightC, crDerived, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, predPartL, predPartCb, predPartCr, bitDepthY, bitDepthC);
                }
                else
                {
                    if (predFlagL0 && predFlagL1)
                    {
                        PredictWeightedSample2(yDerived, cbDerived, o, w, logWDc, partWidth, partHeight, partWidthC, partHeightC, crDerived, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, predPartL, predPartCb, predPartCr, bitDepthY, bitDepthC);
                    }
                    else
                    {
                        PredictDefaultWeightedSample(yDerived, cbDerived, partWidth, partHeight, partWidthC, partHeightC, crDerived, predFlagL0, predFlagL1, predPartL0L, predPartL0CB, predPartL0CR, predPartL1L, predPartL1CB, predPartL1CR, predPartL, predPartCb, predPartCr);
                    }
                }
            }
        }

        private static void PredictDefaultWeightedSample(bool yDerived, bool cbDerived, int partWidth, int partHeight, int partWidthC, int partHeightC, bool crDerived, bool predFlagL0, bool predFlagL1, Matrix16x16 predPartL0L, Matrix16x16 predPartL0CB, Matrix16x16 predPartL0CR, Matrix16x16 predPartL1L, Matrix16x16 predPartL1CB, Matrix16x16 predPartL1CR, Matrix16x16 predPartL, Matrix16x16 predPartCb, Matrix16x16 predPartCr)
        {
            int xMin;
            int xMax;
            int yMin;
            int yMax;

            if (yDerived)
            {
                xMin = 0;
                xMax = partWidth - 1;
                yMin = 0;
                yMax = partHeight - 1;

                if (predFlagL0 && !predFlagL1)
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartL[x, y] = predPartL0L[x, y];
                        }
                    }
                }
                else if (!predFlagL0 && predFlagL1)
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartL[x, y] = predPartL1L[x, y];
                        }
                    }
                }
                else
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartL[x, y] = (predPartL0L[x, y] + predPartL1L[x, y] + 1) >> 1;
                        }
                    }
                }
            }
            else if (cbDerived)
            {
                xMin = 0;
                xMax = partWidthC - 1;
                yMin = 0;
                yMax = partHeightC - 1;

                if (predFlagL0 && !predFlagL1)
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCb[x, y] = predPartL0CB[x, y];
                        }
                    }
                }
                else if (!predFlagL0 && predFlagL1)
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCb[x, y] = predPartL1CB[x, y];
                        }
                    }
                }
                else
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCb[x, y] = (predPartL0CB[x, y] + predPartL1CB[x, y] + 1) >> 1;
                        }
                    }
                }
            }
            else if (crDerived)
            {
                xMin = 0;
                xMax = partWidthC - 1;
                yMin = 0;
                yMax = partHeightC - 1;

                if (predFlagL0 && !predFlagL1)
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCr[x, y] = predPartL0CR[x, y];
                        }
                    }
                }
                else if (!predFlagL0 && predFlagL1)
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCr[x, y] = predPartL1CR[x, y];
                        }
                    }
                }
                else
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCr[x, y] = (predPartL0CR[x, y] + predPartL1CR[x, y] + 1) >> 1;
                        }
                    }
                }
            }
        }

        private static void PredictWeightedSample2(bool yDerived, bool cbDerived, Vector64<int> o, Vector64<int> w, int logWDc, int partWidth, int partHeight, int partWidthC, int partHeightC, bool crDerived, bool predFlagL0, bool predFlagL1, Matrix16x16 predPartL0L, Matrix16x16 predPartL0CB, Matrix16x16 predPartL0CR, Matrix16x16 predPartL1L, Matrix16x16 predPartL1CB, Matrix16x16 predPartL1CR, Matrix16x16 predPartL, Matrix16x16 predPartCb, Matrix16x16 predPartCr, int bitDepthY, int bitDepthC)
        {
            if (yDerived)
            {
                int xMin = 0;
                int xMax = partWidth - 1;
                int yMin = 0;
                int yMax = partHeight - 1;

                int pow = (int)Math.Pow(2, logWDc - 1);

                if (predFlagL0 && !predFlagL1)
                {
                    if (logWDc >= 1)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartL[x, y] = Util264.Clip1Y(((predPartL0L[x, y] * WeightedPredictionSamples.GetW(w, 0) + pow) >> logWDc) + WeightedPredictionSamples.GetO(o, 0), bitDepthY);
                            }
                        }
                    }
                    else
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartL[x, y] = Util264.Clip1Y(predPartL0L[x, y] * WeightedPredictionSamples.GetW(w, 0) + WeightedPredictionSamples.GetO(o, 0), bitDepthY);
                            }
                        }
                    }
                }
                else if (!predFlagL0 && predFlagL1)
                {
                    if (logWDc >= 1)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartL[x, y] = Util264.Clip1Y(((predPartL1L[x, y] * WeightedPredictionSamples.GetW(w, 1) + pow) >> logWDc) + WeightedPredictionSamples.GetO(o, 1), bitDepthY);
                            }
                        }
                    }
                    else
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartL[x, y] = Util264.Clip1Y(predPartL1L[x, y] * WeightedPredictionSamples.GetW(w, 1) + WeightedPredictionSamples.GetO(o, 1), bitDepthY);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartL[x, y] = Util264.Clip1Y(((predPartL0L[x, y] * WeightedPredictionSamples.GetW(w, 0) + predPartL1L[x, y] * WeightedPredictionSamples.GetW(w, 1) + pow) >> (logWDc + 1)) + ((WeightedPredictionSamples.GetO(o, 0) + WeightedPredictionSamples.GetO(o, 1) + 1) >> 1), bitDepthY);
                        }
                    }
                }
            }
            else if (cbDerived)
            {
                int xMin = 0;
                int xMax = partWidthC - 1;
                int yMin = 0;
                int yMax = partHeightC - 1;

                int pow = (int)Math.Pow(2, logWDc - 1);

                if (predFlagL0 && !predFlagL1)
                {
                    if (logWDc >= 1)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCb[x, y] = Util264.Clip1C(((predPartL0CB[x, y] * WeightedPredictionSamples.GetW(w, 0) + pow) >> logWDc) + WeightedPredictionSamples.GetO(o, 0), bitDepthC);
                            }
                        }
                    }
                    else
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCb[x, y] = Util264.Clip1C(predPartL0CB[x, y] * WeightedPredictionSamples.GetW(w, 0) + WeightedPredictionSamples.GetO(o, 0), bitDepthC);
                            }
                        }
                    }
                }
                else if (!predFlagL0 && predFlagL1)
                {
                    if (logWDc >= 1)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCb[x, y] = Util264.Clip1C(((predPartL1CB[x, y] * WeightedPredictionSamples.GetW(w, 1) + pow) >> logWDc) + WeightedPredictionSamples.GetO(o, 1), bitDepthC);
                            }
                        }
                    }
                    else
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCb[x, y] = Util264.Clip1C(predPartL1CB[x, y] * WeightedPredictionSamples.GetW(w, 1) + WeightedPredictionSamples.GetO(o, 1), bitDepthC);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCb[x, y] = Util264.Clip1C(((predPartL0CB[x, y] * WeightedPredictionSamples.GetW(w, 0) + predPartL1CB[x, y] * WeightedPredictionSamples.GetW(w, 1) + pow) >> (logWDc + 1)) + ((WeightedPredictionSamples.GetO(o, 0) + WeightedPredictionSamples.GetO(o, 1) + 1) >> 1), bitDepthC);
                        }
                    }
                }
            }
            else if (crDerived)
            {
                int xMin = 0;
                int xMax = partWidthC - 1;
                int yMin = 0;
                int yMax = partHeightC - 1;

                int pow = (int)Math.Pow(2, logWDc - 1);

                if (predFlagL0 && !predFlagL1)
                {
                    if (logWDc >= 1)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCr[x, y] = Util264.Clip1C(((predPartL0CR[x, y] * WeightedPredictionSamples.GetW(w, 0) + pow) >> logWDc) + WeightedPredictionSamples.GetO(o, 0), bitDepthC);
                            }
                        }
                    }
                    else
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCr[x, y] = Util264.Clip1C(predPartL0CR[x, y] * WeightedPredictionSamples.GetW(w, 0) + WeightedPredictionSamples.GetO(o, 0), bitDepthC);
                            }
                        }
                    }
                }
                else if (!predFlagL0 && predFlagL1)
                {
                    if (logWDc >= 1)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCr[x, y] = Util264.Clip1C(((predPartL1CR[x, y] * WeightedPredictionSamples.GetW(w, 1) + pow) >> logWDc) + WeightedPredictionSamples.GetO(o, 1), bitDepthC);
                            }
                        }
                    }
                    else
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            for (int y = yMin; y <= yMax; y++)
                            {
                                predPartCr[x, y] = Util264.Clip1C(predPartL1CR[x, y] * WeightedPredictionSamples.GetW(w, 1) + WeightedPredictionSamples.GetO(o, 1), bitDepthC);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = xMin; x <= xMax; x++)
                    {
                        for (int y = yMin; y <= yMax; y++)
                        {
                            predPartCr[x, y] = Util264.Clip1C(((predPartL0CR[x, y] * WeightedPredictionSamples.GetW(w, 0) + predPartL1CR[x, y] * WeightedPredictionSamples.GetW(w, 1) + pow) >> (logWDc + 1)) + ((WeightedPredictionSamples.GetO(o, 0) + WeightedPredictionSamples.GetO(o, 1) + 1) >> 1), bitDepthC);
                        }
                    }
                }
            }
        }
    }
}
