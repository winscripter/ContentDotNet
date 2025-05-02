using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Tests.ModelsIO;

public class NALTests
{
    [Fact]
    public void Test_Start_Codes()
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

    [Fact]
    public void Test_Start_Codes_And_Data()
    {
        var nalu = new NalUnit(nalRefIdc: 1u, nalUnitType: 6u, false, false, null);

        UseBSWriterThenReader(
            writer =>
            {
                for (int i = 0; i < 6; i++)
                    writer.WriteBits(0, 8);
                writer.WriteBits(1u, 8);

                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                Assert.True(NalUnit.SkipStartCode(reader));
                Assert.Equal(7, reader.BaseStream.Position);

                NalUnit newNalu = NalUnit.Read(reader, 1);
                Assert.Equal(nalu, newNalu);
            });
    }

    [Fact]
    public void Test_Start_Codes_And_Data_With_Bigger_Start_Codes()
    {
        var nalu = new NalUnit(nalRefIdc: 1u, nalUnitType: 6u, false, false, null);

        UseBSWriterThenReader(
            writer =>
            {
                for (int i = 0; i < 10; i++)
                    writer.WriteBits(0, 8);
                writer.WriteBits(1u, 8);

                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                Assert.True(NalUnit.SkipStartCode(reader));
                Assert.Equal(11, reader.BaseStream.Position);

                NalUnit newNalu = NalUnit.Read(reader, 1);
                Assert.Equal(nalu, newNalu);
            });
    }

    [Fact]
    public void Test_NALU_Data()
    {
        var nalu = new NalUnit(nalRefIdc: 1u, nalUnitType: 6, false, false, null);

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                NalUnit composed = NalUnit.Read(reader, 1);

                Assert.Equal(nalu, composed);
            });
    }

    [Fact]
    public void Test_NALU_Data_1()
    {
        var nalu = new NalUnit(nalRefIdc: 1u, nalUnitType: 14, true, false, new SvcNalUnitHeaderExtension(true, 2, false, 3, 2, 3, true, false, true, 3));

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                NalUnit composed = NalUnit.Read(reader, 1);

                Assert.Equal(nalu, composed);
            });
    }

    [Fact]
    public void Test_NALU_Data_2()
    {
        var nalu = new NalUnit(nalRefIdc: 1u, nalUnitType: 20, false, false, new MvcNalUnitHeaderExtension(true, 2, 3, 2, false, true, true));

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                NalUnit composed = NalUnit.Read(reader, 1);

                Assert.Equal(nalu, composed);
            });
    }

    [Fact]
    public void Test_NALU_Data_3()
    {
        var nalu = new NalUnit(nalRefIdc: 1u, nalUnitType: 21, false, true, new Avc3DNalUnitHeaderExtension(2, false, true, 3, true, false));

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                NalUnit composed = NalUnit.Read(reader, 1);

                Assert.Equal(nalu, composed);
            });
    }

    [Fact]
    public void Test_NALU_Extension_SVC()
    {
        var nalu = new SvcNalUnitHeaderExtension(true, 2, false, 3, 2, 3, true, false, true, 3);

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                SvcNalUnitHeaderExtension composed = SvcNalUnitHeaderExtension.Read(reader);

                Assert.Equal(nalu, composed);
            });
    }

    [Fact]
    public void Test_NALU_Extension_MVC()
    {
        var nalu = new MvcNalUnitHeaderExtension(true, 2, 3, 2, false, true, true);

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                MvcNalUnitHeaderExtension composed = MvcNalUnitHeaderExtension.Read(reader);

                Assert.Equal(nalu, composed);
            });
    }

    [Fact]
    public void Test_NALU_Extension_AVC3D()
    {
        var nalu = new Avc3DNalUnitHeaderExtension(2, false, true, 3, true, false);

        UseBSWriterThenReader(
            writer =>
            {
                nalu.Write(writer);

                // filler data
                writer.WriteBits(0, 16);
            },
            reader =>
            {
                Avc3DNalUnitHeaderExtension composed = Avc3DNalUnitHeaderExtension.Read(reader);

                Assert.Equal(nalu, composed);
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
