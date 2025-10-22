namespace ContentDotNet.Tests.Sdp
{
    using ContentDotNet.Protocols.Sdp;
    using ContentDotNet.Protocols.Sdp.Lines;

    public class SdpTests
    {
        [Fact]
        public void TestVLine()
        {
            string source = "v=0";
            var sdpV = new SdpVersionLine(source);
            Assert.True(sdpV.TryGetVersion(out int version));
            Assert.Equal(0, version);
        }

        [Fact]
        public void TestVLineWithService()
        {
            var service = new SdpService();
            string source = "v=0";
            var line = service.ReadLine(new StringReader(source));
            if (line is not SdpVersionLine)
                throw service.ExceptionThrownOnRead!;
            ((SdpVersionLine)line).TryGetVersion(out int version);
            Assert.Equal(0, version);
        }
    }
}
