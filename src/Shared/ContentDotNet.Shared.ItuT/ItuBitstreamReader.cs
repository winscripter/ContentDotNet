namespace ContentDotNet.Shared.ItuT
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   ITU-T Bitstream RBSP reader
    /// </summary>
    public class ItuBitStreamReader : BitStreamReader
    {
        private int _prevByte1 = -1;
        private int _prevByte2 = -1;

        /// <summary>
        ///   Initializes a new instance of the <see cref="RbspBitStreamReader"/> class.
        /// </summary>
        /// <param name="baseReader">Bit Stream Reader that this RBSP Bit Stream Reader depends on.</param>
        public ItuBitStreamReader(BitStreamReader baseReader) : base(baseReader.BaseStream)
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
    }
}
