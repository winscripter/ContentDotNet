namespace ContentDotNet.Abstractions.Tests;

public class BitStreamTests
{
    [Fact]
    public void UE_Golomb_1()
    {
        UseBSWriterThenReader(
            (writer) =>
            {
                writer.WriteUE(1);
                writer.WriteBits(0u, 16);
            },
            (reader) =>
            {
                uint ueGolomb = reader.ReadUE();
                Assert.Equal(1u, ueGolomb);
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
