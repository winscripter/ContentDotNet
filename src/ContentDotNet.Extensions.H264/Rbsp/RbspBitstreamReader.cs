using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H264.Rbsp;

/// <summary>
///   Reads the bitstream from a Raw Byte Sequence Payload (RBSP).
/// </summary>
public sealed class RbspBitstreamReader : BitStreamReader
{
    // Holds the next EP3B (Emulation Prevention 3 Byte) offset.
    private long _nextEP3BOffset = long.MaxValue; // For now

    /// <summary>
    ///   The Bit Stream Reader that the RBSP Bit Stream Reader depends on.
    /// </summary>
    public BitStreamReader BaseReader { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="RbspBitstreamReader"/> class.
    /// </summary>
    /// <param name="baseReader">Bit Stream Reader that this RBSP Bit Stream Reader depends on.</param>
    public RbspBitstreamReader(BitStreamReader baseReader) : base(baseReader.BaseStream)
    {
        BaseReader = baseReader;
    }

    /// <summary>
    ///   Stores the offset for the next EP3B (Emulation Prevention 3 Byte) so that it can later be skipped.
    /// </summary>
    public void Update()
    {
        if (BaseReader.GetState().BitPosition == 0)
        {
            if (BaseReader.PeekBits(24) == 0x000001)
            {
                _nextEP3BOffset = BaseReader.GetState().ByteOffset + 3; // 3 bytes for the EP3B
            }
            else
            {
                _nextEP3BOffset = long.MaxValue;
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
        if (BaseReader.GetState().BitPosition == 0 &&
            BaseReader.BaseStream.Position == _nextEP3BOffset)
        {
            byte ep3b = (byte)BaseReader.BaseStream.ReadByte();
            if (ep3b != 0x03)
                throw new InvalidDataException("Expected EP3B byte (0x03) but found: " + ep3b);

            long prevPos = BaseReader.BaseStream.Position;
            byte r = (byte)BaseReader.BaseStream.ReadByte();
            BaseReader.BaseStream.Position = prevPos; // Reset to previous position
            base.CurrentByte = r;
            base.BitPosition = 0;
        }
        Update();
        return result;
    }
}
