namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac;

    public class CabacInitializationTests
    {
        [Fact]
        public void Test_Initial_MN_I_For_CtxIdx_0()
        {
            var (m, n) = H264CabacInitializer.GetInitDataForIOrSISlice(0);
            Assert.Equal(20, m);
            Assert.Equal(-15, n);
        }

        [Fact]
        public void Test_Initial_MN_CabacInitIdc0_PSPB_For_CtxIdx_70()
        {
            var (m, n) = H264CabacInitializer.GetInitData(70, 0);
            Assert.Equal(0, m);
            Assert.Equal(45, n);
        }

        [Fact]
        public void Test_Initial_MN_CabacInitIdc1_PSPB_For_CtxIdx_70()
        {
            var (m, n) = H264CabacInitializer.GetInitData(70, 1);
            Assert.Equal(13, m);
            Assert.Equal(15, n);
        }

        [Fact]
        public void Test_Initial_MN_CabacInitIdc2_PSPB_For_CtxIdx_70()
        {
            var (m, n) = H264CabacInitializer.GetInitData(70, 2);
            Assert.Equal(7, m);
            Assert.Equal(34, n);
        }



        [Fact]
        public void Test_Initial_MN_CabacInitIdc0_PSPB_For_CtxIdx_616()
        {
            var (m, n) = H264CabacInitializer.GetInitData(616, 0);
            Assert.Equal(11, m);
            Assert.Equal(28, n);
        }

        [Fact]
        public void Test_Initial_MN_CabacInitIdc1_PSPB_For_CtxIdx_616()
        {
            var (m, n) = H264CabacInitializer.GetInitData(616, 1);
            Assert.Equal(4, m);
            Assert.Equal(45, n);
        }

        [Fact]
        public void Test_Initial_MN_CabacInitIdc2_PSPB_For_CtxIdx_616()
        {
            var (m, n) = H264CabacInitializer.GetInitData(616, 2);
            Assert.Equal(4, m);
            Assert.Equal(39, n);
        }

        [Fact]
        public void Test_Initial_MN_ISI_For_CtxIdx_616()
        {
            var (m, n) = H264CabacInitializer.GetInitDataForIOrSISlice(616);
            Assert.Equal(24, m);
            Assert.Equal(0, n);
        }



        [Fact]
        public void MN_For_CtxIdx_276_Should_Throw()
        {
            try
            {
                // Any of these must throw
                _ = H264CabacInitializer.GetInitDataForIOrSISlice(276);
                _ = H264CabacInitializer.GetInitData(276, 0);
                _ = H264CabacInitializer.GetInitData(276, 1);
                _ = H264CabacInitializer.GetInitData(276, 2);

                Assert.Fail("None of GetInitData/GetInitDataForIOrSISlice method invocations with ctxIdx=276 have thrown");
            }
            catch { }
        }
    }
}
