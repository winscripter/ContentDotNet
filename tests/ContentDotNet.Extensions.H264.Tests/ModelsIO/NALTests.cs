using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Tests.ModelsIO;

public class NALTests
{
    [Fact]
    public void Nalu_Start_Code_Immediate()
    {
        var ms = new MemoryStream();
        var bw = new BitStreamWriter(ms);

        bw.WriteBits(0u, 8);
        Assert.Equal(1, bw.BitPosition);
        bw.WriteBits(0u, 8);
        Assert.Equal(1, bw.BitPosition);
        bw.WriteBits(0u, 8);
        Assert.Equal(1, bw.BitPosition);
        bw.WriteBits(1u, 8);
        Assert.Equal(1, bw.BitPosition);

        var nalu = new NalUnit(1, 6, false, false, null);
        nalu.Write(bw);

        bw.WriteBits(0, 32);

        // -----------

        ms.Position = 0;
        var br = new BitStreamReader(ms);

        NalUnit.SkipStartCode(br);
        _ = br.ReadByte();

        Assert.Equal(5, br.BaseStream.Position);
        Assert.Equal(1, br.GetState().BitPosition);

        NalUnit newNalu = NalUnit.Read(br, 1);

        Assert.Equal(nalu.NalRefIdc, newNalu.NalRefIdc);
        Assert.Equal(nalu.NalUnitType, newNalu.NalUnitType);
        Assert.Equal(nalu.SvcExtensionFlag, newNalu.SvcExtensionFlag);
        Assert.Equal(nalu.Avc3DExtensionFlag, newNalu.Avc3DExtensionFlag);
        Assert.Equal(nalu.Extension, newNalu.Extension);

        try
        {
            br.Dispose();
            bw.Dispose();
            ms.Dispose();
        }
        catch
        {
        }
    }

    [Fact]
    public void Nalu_Start_Code_Immediate_2()
    {
        var ms = new MemoryStream();
        var bw = new BitStreamWriter(ms);

        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(0u, 8);
        bw.WriteBits(1u, 8);

        var nalu = new NalUnit(1, 6, false, false, null);
        nalu.Write(bw);

        bw.WriteBits(0, 32);

        // -----------

        ms.Position = 0;
        var br = new BitStreamReader(ms);

        NalUnit.SkipStartCode(br);
        _ = br.ReadBits(8);

        Assert.Equal(8, br.BaseStream.Position);
        Assert.Equal(1, br.GetState().BitPosition);

        NalUnit newNalu = NalUnit.Read(br, 1);

        Assert.Equal(nalu.NalRefIdc, newNalu.NalRefIdc);
        Assert.Equal(nalu.NalUnitType, newNalu.NalUnitType);
        Assert.Equal(nalu.SvcExtensionFlag, newNalu.SvcExtensionFlag);
        Assert.Equal(nalu.Avc3DExtensionFlag, newNalu.Avc3DExtensionFlag);
        Assert.Equal(nalu.Extension, newNalu.Extension);

        try
        {
            br.Dispose();
            bw.Dispose();
            ms.Dispose();
        }
        catch
        {
        }
    }
}
