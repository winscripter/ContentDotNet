using Newtonsoft.Json.Linq;

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

    [Fact]
    public void Bit_0()
    {
        UseBSWriterThenReader(
            (writer) =>
            {
                writer.WriteBit(false);
                writer.WriteBits(0u, 16);
            },
            (reader) =>
            {
                bool bit = reader.ReadBit();
                Assert.False(bit);
            });
    }

    [Fact]
    public void CodedUInt8_1()
    {
        TestCodedInteger(0, 8);
    }

    [Fact]
    public void CodedUInt8_2()
    {
        TestCodedInteger(1, 8);
    }

    [Fact]
    public void CodedUInt8_3()
    {
        TestCodedInteger(20, 8);
    }

    [Fact]
    public void CodedUInt8_4()
    {
        TestCodedInteger(8, 8);
    }

    [Fact]
    public void CodedUInt8_5()
    {
        TestCodedInteger(128, 8);
    }

    [Fact]
    public void CodedUInt8_6()
    {
        TestCodedInteger(249, 8);
    }

    [Fact]
    public void CodedUInt8_7()
    {
        TestCodedInteger(21, 8);
    }

    [Fact]
    public void CodedUInt16_1()
    {
        TestCodedInteger(0, 16);
    }

    [Fact]
    public void CodedUInt16_2()
    {
        TestCodedInteger(1, 16);
    }

    [Fact]
    public void CodedUInt16_3()
    {
        TestCodedInteger(120, 16);
    }

    [Fact]
    public void CodedUInt16_4()
    {
        TestCodedInteger(2555, 16);
    }

    [Fact]
    public void CodedUInt16_5()
    {
        TestCodedInteger(4627, 16);
    }

    [Fact]
    public void CodedUInt16_6()
    {
        TestCodedInteger(65535, 16);
    }

    [Fact]
    public void CodedUInt16_7()
    {
        TestCodedInteger(12345, 16);
    }

    [Fact]
    public void CodedUInt16_8()
    {
        TestCodedInteger(54321, 16);
    }

    [Fact]
    public void CodedUInt16_9()
    {
        TestCodedInteger(8, 16);
    }

    [Fact]
    public void CodedUInt32_1()
    {
        TestCodedInteger(0, 32);
    }

    [Fact]
    public void CodedUInt32_2()
    {
        TestCodedInteger(1, 32);
    }

    [Fact]
    public void CodedUInt32_3()
    {
        TestCodedInteger(483483498, 32);
    }

    [Fact]
    public void CodedUInt32_4()
    {
        TestCodedInteger(uint.MaxValue, 32);
    }

    [Fact]
    public void Bits_1()
    {
        TestBits(false, true, false, true, false, true);
    }

    [Fact]
    public void Bits_2()
    {
        TestBits(false, true, true, false, true, false);
    }

    [Fact]
    public void Bits_3()
    {
        TestBits(false, false, false, false, false, false);
    }

    [Fact]
    public void Bits_4()
    {
        TestBits(true, true, true, true, true, true);
    }

    [Fact]
    public void Bits_5()
    {
        TestBits(false, false, true, true, false, false);
    }

    private static void TestBits(params bool[] bits)
    {
        UseBSWriterThenReader(
            writer =>
            {
                foreach (bool b in bits)
                    writer.WriteBit(b);
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                foreach (bool b in bits)
                    if (b)
                        Assert.True(reader.ReadBit());
                    else
                        Assert.False(reader.ReadBit());
            });
    }

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
    public void BitsInteger_1() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(123, 7);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(123u, reader.ReadBits(7)));

    [Fact]
    public void BitsInteger_2() => UseBSWriterThenReader(
           writer =>
           {
               writer.WriteBits(1, 7);
               writer.WriteBits(0, 16);
           },
            reader => Assert.Equal(1u, reader.ReadBits(7)));

    [Fact]
    public void BitsInteger_3() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(1, 1);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(1u, reader.ReadBits(1)));

    [Fact]
    public void BitsInteger_4() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(2, 2);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(2u, reader.ReadBits(2)));

    [Fact]
    public void BitsInteger_5() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(255, 8);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(255u, reader.ReadBits(8)));

    [Fact]
    public void BitsInteger_6() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(12352, 16);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(12352u, reader.ReadBits(16)));

    [Fact]
    public void Byte_1() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(123, 8);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(123u, reader.ReadByte()));

    [Fact]
    public void Byte_2() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(65, 8);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(65u, reader.ReadByte()));

    [Fact]
    public void Byte_3() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(1, 8);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(1u, reader.ReadByte()));

    [Fact]
    public void Byte_4() => UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(255, 8);
                writer.WriteBits(0, 16);
            },
            reader => Assert.Equal(255u, reader.ReadByte()));

    [Fact]
    public void Byte_And_Offsets_1()
    {
        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(123, 8);
                writer.WriteBits(255, 8);
                writer.WriteBits(23, 8);
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                Assert.Equal(123u, reader.ReadByte());
                Assert.Equal(255u, reader.ReadByte());
                Assert.Equal(23u, reader.ReadByte());

                Assert.Equal(0u, reader.GetState().BitPosition);
                Assert.Equal(3L, reader.BaseStream.Position);
            });
    }

    [Fact]
    public void Byte_And_Offsets_2()
    {
        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(123, 8);
                writer.WriteBits(255, 8);
                writer.WriteBits(23, 8);
                writer.WriteBits(1, 2);
                writer.WriteBits(7, 5);
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                Assert.Equal(123u, reader.ReadByte());
                Assert.Equal(255u, reader.ReadByte());
                Assert.Equal(23u, reader.ReadByte());
                Assert.Equal(1u, reader.ReadBits(2));
                Assert.Equal(7u, reader.ReadBits(5));

                Assert.Equal(7u, reader.GetState().BitPosition);
                Assert.Equal(4L, reader.BaseStream.Position);
            });
    }

    private static void TestCodedInteger(uint value, uint bits)
    {
        UseBSWriterThenReader(
            writer =>
            {
                writer.WriteBits(value, bits);
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                uint b = reader.ReadBits(bits);
                Assert.Equal(value, b);
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
