using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Tests.ModelsIO;

public class NALTests
{
    [Fact]
    public void Nalu_Start_Code_Immediate()
    {
        using var ms = new MemoryStream();
        using var bw = new BitStreamWriter(ms);

        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(1u, 8);

        var nalu = new NalUnit(5, 6, false, false, null);
        nalu.Write(bw);

        bw.WriteBits(0, 10);

        // -----------

        using var br = new BitStreamReader(ms);

        NalUnit.SkipStartCode(br);

        Assert.Equal(4, br.BaseStream.Position);
    }
}
