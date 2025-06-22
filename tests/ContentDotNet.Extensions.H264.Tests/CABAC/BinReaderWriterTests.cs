using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

public class BinReaderWriterTests
{
    [Fact]
    public void SingleBinRW_ShouldNotThrow()
    {
        const int BinCount = 100;

        using var reader = UseArithmeticWriter(
            writer =>
            {
                var symbols = EncoderSymbols.From(26, true, 20, 0);
                for (int i = 0; i < BinCount; i++)
                    writer.EncodeDecision(ref symbols, i % 2 == 0);
            });

        var binReader = new ArithmeticDecoder(reader);
        var cabac = new CabacContext(26, 0, true, false, 20);
        for (int i = 0; i < BinCount; i++)
            Assert.Equal(i % 2 == 0, binReader.ReadBin(cabac));
    }

    private static BitStreamReader UseArithmeticWriter(
        Action<ArithmeticEncoder> enc)
    {
        using var ms = new MemoryStream();
        using var bsw = new BitStreamWriter(ms);
        var arithEnc = new ArithmeticEncoder(bsw);
        enc(arithEnc);

        bsw.GoToStart();

        byte[] result = ms.ToArray();
        var newMs = new MemoryStream(result);
        return new BitStreamReader(newMs);
    }
}
