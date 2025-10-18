namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.Collections.Bits;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Tests.H264.TestTools.IO.Cabac;

    public partial class BinarizationReaderTests
    {
        private static int TestU(BinCustomDecoder bcd)
        {
            return H264Binarization.U(bcd);
        }

        private static TuResult TestTU(BinCustomDecoder bcd, int cMax)
        {
            return H264Binarization.TU(bcd, cMax);
        }

        private static int TestMbType_I(BinCustomDecoder bcd)
        {
            return H264Binarization.MbType(bcd, H264SliceType.I, false);
        }

        private static int TestMbType_P(BinCustomDecoder bcd)
        {
            return H264Binarization.MbType(bcd, H264SliceType.P, false);
        }

        private static int TestMbType_B(BinCustomDecoder bcd)
        {
            return H264Binarization.MbType(bcd, H264SliceType.B, false);
        }

        private static int TestSubMbType_P(BinCustomDecoder bcd)
        {
            return H264Binarization.MbType(bcd, H264SliceType.P, true);
        }

        private static int TestSubMbType_B(BinCustomDecoder bcd)
        {
            return H264Binarization.MbType(bcd, H264SliceType.B, true);
        }

        [Fact]
        public void U_0()
        {
            var bc = new[]
            {
                false
            };

            Assert.Equal(0, TestU(new BinCustomDecoder(bc)));
        }

        [Fact]
        public void U_1()
        {
            var bc = new[]
            {
                true, false
            };

            Assert.Equal(1, TestU(new BinCustomDecoder(bc)));
        }

        [Fact]
        public void U_2()
        {
            var bc = new[]
            {
                true, true, false
            };

            Assert.Equal(2, TestU(new BinCustomDecoder(bc)));
        }

        [Fact]
        public void U_3()
        {
            var bc = new[]
            {
                true, true, true, false
            };

            Assert.Equal(3, TestU(new BinCustomDecoder(bc)));
        }

        [Fact]
        public void TU_0_Within_CMax_Of_3()
        {
            var bc = new[]
            {
                false
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(0, tuResult.Value);
            Assert.Equal(1, tuResult.BinsRead);
        }

        [Fact]
        public void TU_1_Within_CMax_Of_3()
        {
            var bc = new[]
            {
                true, false
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(1, tuResult.Value);
            Assert.Equal(2, tuResult.BinsRead);
        }

        [Fact]
        public void TU_2_Within_CMax_Of_3()
        {
            var bc = new[]
            {
                true, true, false
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(2, tuResult.Value);
            Assert.Equal(3, tuResult.BinsRead);
        }

        [Fact]
        public void TU_3_Not_Within_CMax_Of_3()
        {
            var bc = new[]
            {
                true, true, true, false
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(3, tuResult.Value);
            Assert.Equal(3, tuResult.BinsRead);
        }

        [Fact]
        public void TU_3_Not_Within_CMax_Of_3_V2()
        {
            var bc = new[]
            {
                true, true, true
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(3, tuResult.Value);
            Assert.Equal(3, tuResult.BinsRead);
        }

        [Fact]
        public void TU_5_Not_Within_CMax_Of_3()
        {
            var bc = new[]
            {
                true, true, true, true, true, false
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(3, tuResult.Value);
            Assert.Equal(3, tuResult.BinsRead);
        }

        [Fact]
        public void TU_5_Not_Within_CMax_Of_3_V2()
        {
            var bc = new[]
            {
                true, true, true, true, true
            };

            var tuResult = TestTU(new BinCustomDecoder(bc), 3);
            Assert.Equal(3, tuResult.Value);
            Assert.Equal(3, tuResult.BinsRead);
        }
    }
}
