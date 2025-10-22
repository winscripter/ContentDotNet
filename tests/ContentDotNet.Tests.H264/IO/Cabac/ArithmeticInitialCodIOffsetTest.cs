namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.IOImplementation;

    public class ArithmeticInitialCodIOffsetTest
    {
        private const uint H264 = 264u;

        [Fact]
        public void Test_Assigned_CodIOffset()
        {
            var ms = new MemoryStream();
            var bsw = new BitStreamWriter(ms);

            bsw.WriteBits(H264, 9);

            // Align
            bsw.WriteBits(0, 7);

            ms.Position = 0;
            var bsr = new BitStreamReader(ms);

            var reader = new DefaultSyntaxReaderFactory().CreateSyntaxReader(null!, bsr);
            var h264Reader = (H264CabacReader)reader;

            Assert.Equal(H264, (uint)h264Reader.CabacDecoder.ArithmeticReader.Offset);
        }
    }
}
