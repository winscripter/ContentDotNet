using ContentDotNet.Abstractions;
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

                Assert.Equal(nalu.IDRFlag, composed.IDRFlag);
                Assert.Equal(nalu.PriorityId, composed.PriorityId);
                Assert.Equal(nalu.NoInterLayerPredFlag, composed.NoInterLayerPredFlag);
                Assert.Equal(nalu.DependencyId, composed.DependencyId);
                Assert.Equal(nalu.QualityId, composed.QualityId);
                Assert.Equal(nalu.TemporalId, composed.TemporalId);
                Assert.Equal(nalu.UseRefPicBaseFlag, composed.UseRefPicBaseFlag);
                Assert.Equal(nalu.DiscardableFlag, composed.DiscardableFlag);
                Assert.Equal(nalu.OutputFlag, composed.OutputFlag);
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
