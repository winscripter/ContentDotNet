namespace ContentDotNet.Abstractions.Tests;

public class BitStreamTests
{
    [Fact]
    public void Bit_1()
    {
        UseBSWriterThenReader(
            (writer) =>
            {
                writer.WriteBit(true);
                writer.WriteBits(0u, 16);
            },
            (reader) =>
            {
                bool bit = reader.ReadBit();
                Assert.True(bit);
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
