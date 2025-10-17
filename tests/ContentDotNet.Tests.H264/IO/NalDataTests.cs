namespace ContentDotNet.Tests.H264.IO
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    public class NalDataTests
    {
        [Fact]
        public void Test_Seq_Of_Three_NALs()
        {
            var bytes = new byte[]
            {
                // SC 1
                0x00, 0x00, 0x01,
            
                // Header 1
                0x29, // type = 9 (AUD), nal_ref_idc = 1

                0x03, // 3 for AUD data

                // SC 2
                0x00, 0x00, 0x01,

                // Header 2
                0x48, // type = 8 (PPS), nal_ref_idc = 2

                0x89, 0x74, // Bogus data; not actual PPS

                // SC 3
                0x00, 0x00, 0x01,

                // Header 3
                0x25, // type = 5 (IDR), nal_ref_idc = 1
                      // Of course in reality this just isn't valid,
                      // IDR won't work without SPS, but like,
                      // this is bogus anyway

                // Bogus bytes
                0xAB, 0xCD, 0xEF
            };

            var ms = new MemoryStream(bytes);
            var bsr = new BitStreamReader(ms);

            var dcd = new H264Service().CreateDecoder(bsr);

            Assert.Equal(NalType.Aud, dcd.DecodeNal(true, false));
            Assert.NotNull(dcd.State);
            Assert.NotNull(dcd.State.H264RbspState);
            Assert.NotNull(dcd.State.H264RbspState.NalUnit);
            Assert.Equal(1u, dcd.State.H264RbspState.NalUnit.NalRefIdc);

            dcd.State.H264RbspState.NalUnit.RbspByte.Position = 0;
            Assert.Equal([0x03], ReadFully(dcd.State.H264RbspState.NalUnit.RbspByte));

            Assert.Equal(NalType.Pps, dcd.DecodeNal(true, false));
            Assert.NotNull(dcd.State.H264RbspState.NalUnit);
            Assert.Equal(2u, dcd.State.H264RbspState.NalUnit.NalRefIdc);

            dcd.State.H264RbspState.NalUnit.RbspByte.Position = 0;
            Assert.Equal([0x89, 0x74], ReadFully(dcd.State.H264RbspState.NalUnit.RbspByte));

            try
            {
                dcd.DebugNals = true;
                Assert.Equal(4, dcd.ProcessNalLength());
                Assert.Equal(NalType.Idr, dcd.DecodeNal(true, false));
                Assert.NotNull(dcd.State.H264RbspState.NalUnit);
                Assert.Equal(5u, dcd.State.H264RbspState.NalUnit.NalRefIdc);

                dcd.State.H264RbspState.NalUnit.RbspByte.Position = 0;
                Assert.Equal([0xAB, 0xCD, 0xEF], ReadFully(dcd.State.H264RbspState.NalUnit.RbspByte));
                dcd.DebugNals = false;
            }
            catch
            {
                dcd.DebugNals = false;
                throw;
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
