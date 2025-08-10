using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

public class BinReaderWriterTests
{
    [Fact]
    public void DoTest()
    {
        using var writerData = new MemoryStream();
        using var writerBs = new BitStreamWriter(writerData);
        var writer = new ArithmeticEncoder(writerBs);

        var context = new CabacContext(0, 0, false, false, 0);
        for (int i = 0; i < 10000; i++)
            writer.EncodeDecision(ref context, i % 4 == 0);

        writerData.Position = 0;

        using var reader = new BitStreamReader(writerData);
        var readerBs = new ArithmeticDecoder(reader);
        var readerContext = new CabacContext(0, 0, false, false, 0);

        for (int i = 0; i < 10000; i++)
        {
            bool decodedValue = readerBs.ReadBin(ref readerContext);
            Assert.Equal(i % 4 == 0, decodedValue);
        }
    }
}
