namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264;
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

            var reader = new DefaultSyntaxReaderFactory().CreateSyntaxReader(
                new H264State(null!)
                {
                    H264RbspState = new()
                    {
                        PictureParameterSet = new(
                            0, 0, true, false, 0, 0, null, null, null, null, null, null, null, 0, 0, false, 0, 0, 0, 0, false, false, false, null, null, null, [], [], [], [], null),
                        SliceHeader = new(
                            0, 2, 0, null, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 0, null, null, null, null, null, null)
                    }
                }, bsr);
            var h264Reader = (H264CabacReader)reader;

            Assert.Equal(H264, (uint)h264Reader.CabacDecoder.ArithmeticReader.Offset);
            Assert.Equal(2, h264Reader.Reader.GetState().ByteOffset);
            Assert.Equal(1, h264Reader.Reader.GetState().BitPosition);
        }
    }
}
