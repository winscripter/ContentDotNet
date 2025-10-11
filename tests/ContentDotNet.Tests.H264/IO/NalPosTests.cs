namespace ContentDotNet.Tests.H264.IO
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264;

    public class NalPosTests
    {
        [Fact]
        public void Detect_Nal_Start()
        {
            var bytes = new byte[]
            {
                // Random bytes that should be skipped
                0xA5, 0x89, 0x73, 0x63,

                // NAL start
                0x00, 0x00, 0x00, 0x01,

                // Filler bytes
                0x55, 0x00, 0x00,
            };

            var ms = new MemoryStream(bytes)
            {
                Position = 0
            };
            var bsr = new BitStreamReader(ms);

            var dcd = new H264Service().CreateDecoder(bsr);

            if (!dcd.SkipToNalStart())
                Assert.Fail("Could not find NAL start!!!");

            Assert.Equal(8, (int)ms.Position);
        }

        [Fact]
        public void Detect_Nal_Start_Without_Any()
        {
            var bytes = new byte[]
            {
                // Random bytes that should be skipped
                0xA5, 0x89, 0x73, 0x63,

                // Bogus
                0x08, 0x42, 0x76, 0x94,
            };

            var ms = new MemoryStream(bytes)
            {
                Position = 0
            };
            var bsr = new BitStreamReader(ms);

            var dcd = new H264Service().CreateDecoder(bsr);

            // Read one byte to keep track of position
            Assert.Equal(0xA5u, dcd.BitStreamReader.ReadByte());

            // Skip and ensure
            Assert.False(dcd.SkipToNalStart());

            Assert.Equal(1, (int)ms.Position);
        }

        [Fact]
        public void Detect_Nal_Length()
        {
            var bytes = new byte[]
            {
                // SC of first NAL
                0x00, 0x00, 0x00, 0x01,

                // Bogus data of first NAL (5 bytes, remember!)
                0x31, 0x74, 0x83, 0x95, 0x32,

                // SC of second NAL
                0x00, 0x00, 0x00, 0x01,

                // Bogus data of second NAL (2 bytes)
                0xA5, 0x8B,

                // SC of third NAL
                0x00, 0x00, 0x00, 0x01,

                // Bogus data of third NAL
                0x23, 0x76
            };

            var ms = new MemoryStream(bytes)
            {
                Position = 0
            };
            var bsr = new BitStreamReader(ms);

            var dcd = new H264Service().CreateDecoder(bsr);

            Assert.True(dcd.SkipToNalStart());
            Assert.Equal(5, dcd.ProcessNalLength());

            Assert.True(dcd.SkipToNalStart());
            Assert.Equal(2, dcd.ProcessNalLength());

            Assert.True(dcd.SkipToNalStart());
            Assert.Equal(2, dcd.ProcessNalLength());

            Assert.False(dcd.SkipToNalStart());
            // H.264 stream ends here
        }

        [Fact]
        public void Detect_Length_Of_Empty_Nal()
        {
            var bytes = new byte[]
            {
                // SC of NAL
                0x00, 0x00, 0x00, 0x01,

                // No more data; should be length of 0
            };

            var ms = new MemoryStream(bytes)
            {
                Position = 0
            };
            var bsr = new BitStreamReader(ms);

            var dcd = new H264Service().CreateDecoder(bsr);

            Assert.True(dcd.SkipToNalStart());
            Assert.Equal(0, dcd.ProcessNalLength());

            Assert.False(dcd.SkipToNalStart());
            // H.264 stream ends here
        }

        [Fact]
        public void Misaligned_Byte_Search_Nal_Should_Still_Work()
        {
            var bytes = new byte[]
            {
                // Bogus byte
                0x0E,

                // SC of NAL
                0x00, 0x00, 0x00, 0x01,

                // No more data; should be length of 0
            };

            var ms = new MemoryStream(bytes)
            {
                Position = 0
            };
            var bsr = new BitStreamReader(ms);

            var dcd = new H264Service().CreateDecoder(bsr);

            dcd.BitStreamReader.ReadBit(); // Misalign

            Assert.True(dcd.SkipToNalStart());
            Assert.Equal(0, dcd.ProcessNalLength());

            Assert.False(dcd.SkipToNalStart());
            // H.264 stream ends here
        }
    }
}
