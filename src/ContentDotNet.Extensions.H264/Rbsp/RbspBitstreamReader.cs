using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H264.Rbsp;

/// <summary>
///   Reads the bitstream from a Raw Byte Sequence Payload (RBSP).
/// </summary>
public sealed class RbspBitstreamReader : BitStreamReader
{
    private const int TargetBitPositionBeforeUpdate = 8;

    // Holds the next EP3B (Emulation Prevention 3 Byte) offset.
    private long _nextEP3BOffset = long.MaxValue; // For now

    /// <summary>
    ///   Initializes a new instance of the <see cref="RbspBitstreamReader"/> class.
    /// </summary>
    /// <param name="baseReader">Bit Stream Reader that this RBSP Bit Stream Reader depends on.</param>
    public RbspBitstreamReader(BitStreamReader baseReader) : base(baseReader.BaseStream)
    {
    }

    /// <summary>
    ///   Stores the offset for the next EP3B (Emulation Prevention 3 Byte) so that it can later be skipped.
    /// </summary>
    public void Update()
    {
        if (BaseStream.Length - BaseStream.Position >= 3)
        {
            if (this.GetState().BitPosition == TargetBitPositionBeforeUpdate)
            {
                if (this.PeekBits(24) == 0x000001)
                {
                    _nextEP3BOffset = this.GetState().ByteOffset + 3;
                }
                else
                {
                    _nextEP3BOffset = long.MaxValue;
                }
            }
        }
    }

    /// <summary>
    ///   Reads a bit.
    /// </summary>
    /// <returns>The bit.</returns>
    /// <exception cref="InvalidDataException"></exception>
    public override bool ReadBit()
    {
        bool result = base.ReadBit();
        if (this.GetState().BitPosition == TargetBitPositionBeforeUpdate &&
            this.BaseStream.Position == _nextEP3BOffset)
        {
            int ep3b = this.BaseStream.ReadByte();
            if (ep3b != -1)
            {
                if (ep3b != 0x03)
                    throw new InvalidDataException("Expected EP3B byte (0x03) but found: " + ep3b);

                base.CurrentByte = (byte)ep3b;
                base.BitPosition = 0;
            }
        }
        Update();
        return result;
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
        int val = (int)(codeNum + 1 >> 1);
        return (codeNum & 1) == 0 ? -val : val;
    }
}
