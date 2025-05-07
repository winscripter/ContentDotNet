using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Utilities;
using System.Drawing;

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

        private DerivationContext _derivationContext;
        private IMacroblockUtility _macroblockUtility;

        private IReferencePictureListFactory factory = null!;
        private Size frameSize = default;

        public ReferencePictureList RefPicListL0 { get; private set; } = null!;
        public ReferencePictureList? RefPicListL1 { get; private set; }

        public Inter(DerivationContext derivationContext, IMacroblockUtility macroblockUtility, IReferencePictureListFactory refPicList, Size frameSize)
        {
            _derivationContext = derivationContext;
            _macroblockUtility = macroblockUtility;

            InitializeInterPrediction(refPicList, frameSize);
        }

        public DerivationContext DerivationContext
        {
            get => _derivationContext;
            set => _derivationContext = value;
        }

        private void InitializeInterPrediction(IReferencePictureListFactory refPicList, Size frameSize)
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

            this.factory = refPicList;
            this.frameSize = frameSize;

            this.RefPicListL0 = refPicList.Create(frameSize.Width, frameSize.Height, 16);
            this.RefPicListL1 = null; // What if it's not a B slice? Then we're wasting memory. Store factory and frameSize separately.
        }

        // 8.4.1.3.2
        private void DeriveMotionDataOfNeighboringPartitions(int currSubMbType, bool listSuffixFlag,
            out int mbAddrA, out int mbAddrB, out int mbAddrC,
            out MotionVector mvL0A, out MotionVector mvL0B, out MotionVector mvL0C,
            out MotionVector mvL1A, out MotionVector mvL1B, out MotionVector mvL1C,
            out int refIdxL0A, out int refIdxL0B, out int refIdxL0C,
            out int refIdxL1A, out int refIdxL1B, out int refIdxL1C)
        {
            mbAddrA = 0;
            mbAddrB = 0;
            mbAddrC = 0;

            int mbAddrD = 0;
            int mbPartIdxA = 0, mbPartIdxB = 0, mbPartIdxC = 0, mbPartIdxD = 0;
            int subMbPartIdxA = 0, subMbPartIdxB = 0, subMbPartIdxC = 0, subMbPartIdxD = 0;
            bool validA = false, validB = false, validC = false, validD = false;

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
    }
}
