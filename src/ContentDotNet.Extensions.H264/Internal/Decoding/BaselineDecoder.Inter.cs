using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Utilities;

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

        private int[] refIdxL0N = null!;
        private int[] refIdxL1N = null!;
        private ArrayMatrix4x4x2 mvL0 = null!;
        private ArrayMatrix4x4x2 mvL1 = null!;
        private ArrayMatrix4x4x2 mvCL0 = null!;
        private ArrayMatrix4x4x2 mvCL1 = null!;

        private DerivationContext _derivationContext;
        private IMacroblockUtility _macroblockUtility;

        public Inter(DerivationContext derivationContext, IMacroblockUtility macroblockUtility)
        {
            _derivationContext = derivationContext;
            _macroblockUtility = macroblockUtility;

            InitializeInterPrediction();
        }

        public DerivationContext DerivationContext
        {
            get => _derivationContext;
            set => _derivationContext = value;
        }

        private void InitializeInterPrediction()
        {
            sliceType = GeneralSliceType.I; // Default
            mbTypeArray = new();
            subMbTypeArray = new();
            mbType = 0;
            subMbType = 0;
            mbPartIdx = 0;
            subMbPartIdx = 0;
            refIdxL0N = new int[16];
            refIdxL1N = new int[16];
            mvL0 = new ArrayMatrix4x4x2();
            mvL1 = new ArrayMatrix4x4x2();
            mvCL0 = new ArrayMatrix4x4x2();
            mvCL1 = new ArrayMatrix4x4x2();
        }
    }
}
