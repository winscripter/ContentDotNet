namespace ContentDotNet.Abstractions;

/// <summary>
/// Reads data at bit level
/// </summary>
public sealed class BitStreamReader(Stream input) : IDisposable
{
    internal readonly Stream stream = input ?? throw new ArgumentNullException(nameof(input));
    internal int currentByte;
    internal int bitPosition = 8; // Forces initial read

    /// <summary>
    /// Reads a single bit from the bitstream.
    /// </summary>
    /// <returns>Bit from the bitstream.</returns>
    /// <exception cref="EndOfStreamException"></exception>
    public bool ReadBit()
    {
        if (bitPosition == 8)
        {
            currentByte = stream.ReadByte();
            if (currentByte == -1)
                throw new EndOfStreamException();

            bitPosition = 0;
        }

        bool bit = (currentByte >> 7 - bitPosition & 1) == 1;
        bitPosition++;
        return bit;
    }

    /// <summary>
    /// Reads the specified number of bits from the bitstream.
    /// </summary>
    /// <param name="count">Number of bits to read.</param>
    /// <returns>A 32-bit unsigned integer representing bits read.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public uint ReadBits(uint count)
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

    /// <summary>
    /// Reads an Unsigned Exponential Golomb.
    /// </summary>
    /// <returns>Unsigned Exponential Golomb.</returns>
    /// <exception cref="InvalidDataException"></exception>
    public uint ReadUE()
    {
        uint zeroCount = 0;
        while (!ReadBit())
            zeroCount++;

        if (zeroCount > 31)
            throw new InvalidDataException("UE Golomb code too long.");

        uint result = (1u << (int)zeroCount) - 1 + (zeroCount == 0 ? 0 : ReadBits(zeroCount));
        return result;
    }

    /// <summary>
    /// Reads an Signed Exponential Golomb.
    /// </summary>
    /// <returns>Signed Exponential Golomb.</returns>
    /// <exception cref="InvalidDataException"></exception>
    public int ReadSE()
    {
        uint codeNum = ReadUE();
        int val = (int)(codeNum + 1 >> 1);
        return (codeNum & 1) == 0 ? -val : val;
    }

    /// <summary>
    /// Releases memory from the bitstream.
    /// </summary>
    public void Dispose()
    {
        stream?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Represents the backing stream of this bitstream reader.
    /// </summary>
    public Stream BaseStream => stream;

    /// <summary>
    /// Length, in bytes, in the entire stream.
    /// </summary>
    public long Length => stream.Length;

    /// <summary>
    ///   Returns the next number of bits without advancing in the bitstream.
    /// </summary>
    /// <param name="count">Number of bits</param>
    /// <returns>Next <paramref name="count"/> bits.</returns>
    public uint PeekBits(uint count)
    {
        ReaderState activeState = this.GetState();
        uint b;
        try
        {
            b = this.ReadBits(count);
        }
        catch
        {
            this.GoTo(activeState);
            throw;
        }

        this.GoTo(activeState);
        return b;
    }
}
