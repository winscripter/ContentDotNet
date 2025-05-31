using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Rbsp;

namespace ContentDotNet.Extensions.H264.Tests.Rbsp;

// Unit tests were AI-generated.

public sealed class RbspReaderTests
{
    [Fact]
    public void ReadBit_ShouldReturnCorrectBit()
    {
        byte[] testData = [0b10101010]; // 8 bits, alternating pattern
        using var stream = new MemoryStream(testData);
        var reader = new RbspBitstreamReader(new BitStreamReader(stream));

        Assert.True(reader.ReadBit()); // 1
        Assert.False(reader.ReadBit()); // 0
        Assert.True(reader.ReadBit()); // 1
        Assert.False(reader.ReadBit()); // 0
    }

    [Fact]
    public void ReadBits_ShouldReturnExpectedValue()
    {
        byte[] testData = [0b11001100]; // 8 bits
        using var stream = new MemoryStream(testData);
        var reader = new RbspBitstreamReader(new BitStreamReader(stream));

        Assert.Equal(0b110u, reader.ReadBits(3)); // Read first 3 bits
        Assert.Equal(0b011u, reader.ReadBits(3)); // Next 3 bits
        Assert.Equal(0b00u, reader.ReadBits(2));  // Remaining 2 bits
    }

    [Fact]
    public void ReadUE_ShouldReturnCorrectValue()
    {
        byte[] testData = [0b00010100]; // Represents UE(5)
        using var stream = new MemoryStream(testData);
        var reader = new RbspBitstreamReader(new BitStreamReader(stream));

        Assert.Equal(5U, reader.ReadUE());
    }

    [Fact]
    public void ReadSE_ShouldReturnCorrectSignedValue()
    {
        byte[] testData = [0b00011000]; // Represents SE(-3)
        using var stream = new MemoryStream(testData);
        var reader = new RbspBitstreamReader(new BitStreamReader(stream));

        Assert.Equal(-3, reader.ReadSE());
    }

    [Fact]
    public void ReadBit_ShouldHandleEmulationPreventionBytes()
    {
        byte[] testData = [0x00, 0x00, 0x03, 0x04]; // EP3B pattern
        using var stream = new MemoryStream(testData);
        var reader = new RbspBitstreamReader(new BitStreamReader(stream));

        reader.ReadBit(); // 0
        reader.ReadBit(); // 0
        reader.ReadBit(); // 0 (before EP3B)

        Assert.Equal(0x04u, reader.ReadByte()); // Should skip EP3B (0x03) and read next byte
    }

    [Fact]
    public void ReadBit_ShouldThrowException_OnInvalidEP3B()
    {
        byte[] testData = [0x00, 0x00, 0x02, 0x04]; // Incorrect EP3B (0x02 instead of 0x03)
        using var stream = new MemoryStream(testData);
        var reader = new RbspBitstreamReader(new BitStreamReader(stream));

        reader.ReadBit(); // 0
        reader.ReadBit(); // 0
        reader.ReadBit(); // 0 (before supposed EP3B)

        Assert.Throws<InvalidDataException>(() => reader.ReadByte()); // Should throw exception
    }
}
