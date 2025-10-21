namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    public class BinarizationAssignmentTests
    {
        #region mb_type

        [Fact]
        public void TestMbType_SI()
        {
            var binarization = H264BaseCtxIdxAssignments.GetParserWithCtxIdx(H264SyntaxElement.MacroblockType, 0, false, H264SliceType.SI);
            Assert.NotNull(binarization);
            Assert.IsType<ContextIndexAffixValue>(binarization.Record.CtxIdxOffset);
            Assert.IsType<ContextIndexAffixValue>(binarization.Record.MaxBinIdxCtx);
            var ctxAffixA = (ContextIndexAffixValue)binarization.Record.CtxIdxOffset!;
            var ctxAffixB = (ContextIndexAffixValue)binarization.Record.MaxBinIdxCtx!;
            Assert.Equal(0, ctxAffixB.Prefix);
            Assert.Equal(6, ctxAffixB.Suffix);
            Assert.Equal(0, ctxAffixA.Prefix);
            Assert.Equal(3, ctxAffixA.Suffix);
        }

        [Fact]
        public void TestMbType_I()
        {
            var binarization = H264BaseCtxIdxAssignments.GetParserWithCtxIdx(H264SyntaxElement.MacroblockType, 0, false, H264SliceType.I);
            Assert.NotNull(binarization);
            Assert.IsType<ContextIndexIntegerValue>(binarization.Record.CtxIdxOffset);
            Assert.IsType<ContextIndexIntegerValue>(binarization.Record.MaxBinIdxCtx);
            var ctxAffixA = (ContextIndexIntegerValue)binarization.Record.CtxIdxOffset!;
            var ctxAffixB = (ContextIndexIntegerValue)binarization.Record.MaxBinIdxCtx!;
            Assert.Equal(6, ctxAffixB.Value);
            Assert.Equal(3, ctxAffixA.Value);
        }

        [Fact]
        public void TestMbType_PSP()
        {
            var binarization = H264BaseCtxIdxAssignments.GetParserWithCtxIdx(H264SyntaxElement.MacroblockType, 0, false, H264SliceType.P);
            Assert.NotNull(binarization);
            Assert.IsType<ContextIndexAffixValue>(binarization.Record.CtxIdxOffset);
            Assert.IsType<ContextIndexAffixValue>(binarization.Record.MaxBinIdxCtx);
            var ctxAffixA = (ContextIndexAffixValue)binarization.Record.CtxIdxOffset!;
            var ctxAffixB = (ContextIndexAffixValue)binarization.Record.MaxBinIdxCtx!;
            Assert.Equal(2, ctxAffixB.Prefix);
            Assert.Equal(5, ctxAffixB.Suffix);
            Assert.Equal(14, ctxAffixA.Prefix);
            Assert.Equal(17, ctxAffixA.Suffix);
        }

        [Fact]
        public void TestMbType_B()
        {
            var binarization = H264BaseCtxIdxAssignments.GetParserWithCtxIdx(H264SyntaxElement.MacroblockType, 0, false, H264SliceType.B);
            Assert.NotNull(binarization);
            Assert.IsType<ContextIndexAffixValue>(binarization.Record.CtxIdxOffset);
            Assert.IsType<ContextIndexAffixValue>(binarization.Record.MaxBinIdxCtx);
            var ctxAffixA = (ContextIndexAffixValue)binarization.Record.CtxIdxOffset!;
            var ctxAffixB = (ContextIndexAffixValue)binarization.Record.MaxBinIdxCtx!;
            Assert.Equal(3, ctxAffixB.Prefix);
            Assert.Equal(5, ctxAffixB.Suffix);
            Assert.Equal(27, ctxAffixA.Prefix);
            Assert.Equal(32, ctxAffixA.Suffix);
        }

        #endregion
    }
}
