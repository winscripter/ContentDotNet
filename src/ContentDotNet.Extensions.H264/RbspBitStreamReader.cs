using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Reads the bitstream from a Raw Byte Sequence Payload (RBSP).
/// </summary>
public sealed class RbspBitStreamReader : BitStreamReader
{
    private int _prevByte1 = -1;
    private int _prevByte2 = -1;

    /// <summary>
    ///   Initializes a new instance of the <see cref="RbspBitStreamReader"/> class.
    /// </summary>
    /// <param name="baseReader">Bit Stream Reader that this RBSP Bit Stream Reader depends on.</param>
    public RbspBitStreamReader(BitStreamReader baseReader) : base(baseReader.BaseStream)
    {
    }

    /// <summary>
    ///   Reads a bit.
    /// </summary>
    /// <returns>The bit.</returns>
    /// <exception cref="InvalidDataException"></exception>
    public override bool ReadBit()
    {
        if (BitPosition == 0)
        {
            int nextByte;

            while (true)
            {
                nextByte = BaseStream.ReadByte();

                if (nextByte == -1)
                    throw new EndOfStreamException();

                if (_prevByte2 == 0x00 && _prevByte1 == 0x00 && nextByte == 0x03)
                {
                    _prevByte2 = _prevByte1;
                    _prevByte1 = nextByte;
                    continue;
                }

                break;
            }

            CurrentByte = nextByte;
            BitPosition = 0;

            _prevByte2 = _prevByte1;
            _prevByte1 = nextByte;
        }

        bool bit = (CurrentByte >> (7 - BitPosition) & 1) == 1;
        BitPosition = (BitPosition + 1) % 8;
        return bit;
    }

    /// <inheritdoc cref="BitStreamReader.ReadBits(uint)" />
    public override uint ReadBits(uint count)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 1u);

        uint result = 0;
        for (int i = 0; i < count; i++)
        {
            result <<= 1;
            if (ReadBit())
            {
                result |= 1;
            }
        }
        return result;
    }

    /// <inheritdoc cref="BitStreamReader.ReadByte" />
    public override uint ReadByte()
    {
        return ReadBits(8);
    }

    /// <summary>
    /// Reads an Unsigned Exponential Golomb.
    /// </summary>
    /// <returns>Unsigned Exponential Golomb.</returns>
    /// <exception cref="InvalidDataException"></exception>
    public override uint ReadUE()
    {
        uint zeroCount = 0;
        while (!ReadBit() && zeroCount <= 31)
            zeroCount++;

        uint result = (1u << (int)zeroCount) - 1 + (zeroCount < 1 ? 0 : ReadBits(zeroCount));
        return result;
    }

    /// <summary>
    /// Reads an Signed Exponential Golomb.
    /// </summary>
    /// <returns>Signed Exponential Golomb.</returns>
    /// <exception cref="InvalidDataException"></exception>
    public override int ReadSE()
    {
        uint codeNum = ReadUE();
        int val = (int)((codeNum + 1) >> 1);
        return (codeNum & 1) == 0 ? -val : val;
    }
}
