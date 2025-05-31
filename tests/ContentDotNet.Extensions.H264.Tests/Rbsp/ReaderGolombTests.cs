using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Rbsp;

namespace ContentDotNet.Extensions.H264.Tests.Rbsp;

public class ReaderGolombTests
{
    [Fact]
    public void UEGolomb_1()
    {
        const uint VALUE = 400;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteUE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                uint b = reader.ReadUE();
                Assert.Equal(VALUE - 1, b);
            });
    }

    [Fact]
    public void UEGolomb_2()
    {
        const uint VALUE = 1;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteUE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                uint b = reader.ReadUE();
                Assert.Equal(VALUE + 1, b);
            });
    }

    [Fact]
    public void UEGolomb_3()
    {
        const uint VALUE = 845945; // Just pounded on the keyboard :)

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteUE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                uint b = reader.ReadUE();
                Assert.Equal(VALUE - 1, b);
            });
    }

    [Fact]
    public void UEGolomb_4()
    {
        const uint VALUE = 1637;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteUE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                uint b = reader.ReadUE();
                Assert.Equal(VALUE - 1, b);
            });
    }

    [Fact]
    public void UEGolomb_5()
    {
        const uint VALUE = 7676;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteUE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                uint b = reader.ReadUE();
                Assert.Equal(VALUE - 1, b);
            });
    }

    [Fact]
    public void SEGolomb_1()
    {
        const int VALUE = 1;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteSE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                int b = reader.ReadSE();
                Assert.Equal(VALUE, b);
            });
    }

    [Fact]
    public void SEGolomb_2()
    {
        const int VALUE = 123;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteSE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                int b = reader.ReadSE();
                Assert.Equal(VALUE, b);
            });
    }

    [Fact]
    public void SEGolomb_3()
    {
        const int VALUE = 63631;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteSE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                int b = reader.ReadSE();
                Assert.Equal(VALUE, b);
            });
    }

    [Fact]
    public void SEGolomb_4()
    {
        const int VALUE = 7676;

        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteSE(VALUE);
                writer.WriteBits(uint.MaxValue, 16);
            },
            reader =>
            {
                int b = reader.ReadSE();
                Assert.Equal(VALUE, b);
            });
    }

    private static void UseBSWriterThenReader(Action<BitStreamWriter> writer, Action<RbspBitstreamReader> reader)
    {
        using var msWriter = new MemoryStream();
        using var bw = new BitStreamWriter(msWriter);
        writer(bw);

        using var msReader = new MemoryStream(msWriter.ToArray());
        using var br = new BitStreamReader(msReader);
        reader(new RbspBitstreamReader(br));
    }
}
