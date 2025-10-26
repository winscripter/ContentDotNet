namespace ContentDotNet.Tests.H264.IO.Cabac
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;

    public class ArithmeticDecodingEngineTests
    {
        private static readonly byte[] Test1ResultBytes = [0x00, 0x00, 0xe3, 0x2f, 0xd2, 0x8c, 0x04];
        private static readonly byte[] Test1SourceBytes = [0x89, 0x45, 0x67, 0xf5, 0x98, 0x43, 0x62, 0x95, 0xab];

        [Fact]
        public void Test_1()
        {
            // Generated using webutils/h264/bin-encoder.html.
            // Settings:
            //   - valMPS: 1
            //   - pStateIdx: 5
            //   - type: Decision
            // Source bytes: 0x89 0x45 0x67 0xf5 0x98 0x43 0x62 0x95 0xab

            using var ms = new MemoryStream(Test1ResultBytes)
            {
                Position = 0
            };
            using var bsr = new BitStreamReader(ms);
            var arithmeticEngine = ArithmeticFactory.CreateArithmeticReader(bsr, 510, (int)bsr.ReadBits(9), null);

            int countOfBins = 0;
            int timesToRead = Test1ResultBytes.Length * 8;

            H264ContextVariable cv = new()
            {
                PStateIdx = 5,
                ValMps = 1
            };
            while (countOfBins < timesToRead)
            {
                if (arithmeticEngine.ReadBin(ArithmeticBinType.Decision, cv) != ((Test1SourceBytes[countOfBins / 8] & (1 << (7 - (countOfBins % 8)))) != 0))
                    Assert.Fail($"Opposite bin! Bin number: {countOfBins}");
                countOfBins++;
            }
        }
    }
}
