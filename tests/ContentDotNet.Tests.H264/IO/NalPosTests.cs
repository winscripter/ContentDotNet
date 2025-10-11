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
    }
}
