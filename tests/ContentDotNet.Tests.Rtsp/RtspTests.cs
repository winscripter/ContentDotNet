namespace ContentDotNet.Tests.Rtsp
{
    using ContentDotNet.Protocols.Rtsp.Headers;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;

    public class RtspTests
    {
        [Fact]
        public void Test_Stringify()
        {
            var factory = new DefaultRtspHeaderFactory();
            var accept = factory.Create<IRtspAcceptHeader>();
            accept.Value.Add(new AcceptRecord("SampleMIME", null));
            accept.Value.Add(new AcceptRecord("SampleMIME2", 0.50D));
            Assert.Equal("SampleMIME, SampleMIME2; q=0.50", accept.ToString());
        }
    }
}