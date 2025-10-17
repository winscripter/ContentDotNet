namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Tests.H264.TestTools;

    public class CtxIdxIncDerivativeTests
    {
        [Fact]
        public void Mb_Skip_Flag_I()
        {
            // Create a grid of macroblocks:
            //
            // A B C
            // D[E]F
            // G H I
            // 
            // Macroblocks inside brackets are current macroblocks
            //
            // Set PicWidthInMbs to be 3 since each row has 3 macroblocks.
            //
            // CurrMbAddr is 4 because it's the fifth macroblock in the left-to-right, top-to-bottom order.

            const H264SliceType SLICE = H264SliceType.I;

            var macroblocks = new[]
            {
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = true
                }, // A
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // B
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // C
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = true
                }, // D
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // E
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // F
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // G
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // H
                new H264MacroblockInfo(SLICE, new(), false)
                {
                    MbSkipFlag = false
                }, // I
            };

            var mbu = new CustomMacroblockUtility((uint)SLICE, macroblocks);

            var h264State = new H264State(mbu)
            {
                CurrMbAddr = 4,
                H264RbspState = new H264RbspState()
                {
                    SequenceParameterSetData = new Extensions.Video.H264.RbspModels.RbspSequenceParameterSetData(
                        0, false, false, false, false, false, false, 0, 0, 0, 0, false, 0, 0, false, false, [], new Extensions.Video.H264.RbspModels.ScalingMatrixData([], [], [], []),
                        0, 0, 0, false, 0, 0, 0, [], 0, false, 2, 2, false, false, false, false, 0, 0, 0, 0, false, null)
                }
            };

            var ctxIdxInc = H264CabacCtxIdxIncDerivative.MbSkipFlag(h264State);

            Assert.Equal(1, ctxIdxInc); // Because both MBs are available, and top MB has 0 mb_skip_flag
        }
    }
}
