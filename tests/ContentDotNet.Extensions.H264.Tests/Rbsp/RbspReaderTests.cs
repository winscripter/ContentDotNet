using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Rbsp;
namespace ContentDotNet.Extensions.H264.Tests.Rbsp;

public sealed class RbspReaderTests
{
    [Fact]
    public void RbspBitStreamReader_ReadsCorrectly()
    {
        byte[] stream = [0x00, 0x05, 0x30];
        var rbsp = new RbspBitstreamReader(new BitStreamReader(new MemoryStream(stream)));
        rbsp.Update(); // Initialize the next EP3B offset

        Assert.Equal(0u, rbsp.ReadByte());
        Assert.Equal(5u, rbsp.ReadByte());
        Assert.Equal(0x30u, rbsp.ReadByte());
    }
}
