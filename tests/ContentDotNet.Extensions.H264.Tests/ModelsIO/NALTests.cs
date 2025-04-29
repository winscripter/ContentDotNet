using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Tests.ModelsIO;

public class NALTests
{
    [Fact]
    public void TestStartCodes()
    {
        UseBSWriterThenReader(
            writer =>
            {
                for (int i = 0; i < 6; i++)
                    writer.WriteBits(0, 8);
                writer.WriteBits(1u, 8);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                Assert.True(NalUnit.SkipStartCode(reader));
                Assert.Equal(7, reader.BaseStream.Position);
            });
    }

    private static void UseBSWriterThenReader(Action<BitStreamWriter> writer, Action<BitStreamReader> reader)
    {
        using var msWriter = new MemoryStream();
        using var bw = new BitStreamWriter(msWriter);
        writer(bw);

        using var msReader = new MemoryStream(msWriter.ToArray());
        using var br = new BitStreamReader(msReader);
        reader(br);
    }
}
