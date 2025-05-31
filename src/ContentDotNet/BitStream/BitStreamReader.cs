namespace ContentDotNet.BitStream;

/// <summary>
/// Reads data at bit level
/// </summary>
public class BitStreamReader(Stream input) : IDisposable
{
    protected internal readonly Stream Stream = input ?? throw new ArgumentNullException(nameof(input));
    protected internal int CurrentByte;
    protected internal int BitPosition = 0; // Start at 0

    /// <summary>
    /// Reads a single bit from the bitstream.
    /// </summary>
    /// <returns>Bit from the bitstream.</returns>
    /// <exception cref="EndOfStreamException"></exception>
    public virtual bool ReadBit()
    {
        if (BitPosition == 0)
        {
            CurrentByte = Stream.ReadByte();
            if (CurrentByte == -1)
                throw new EndOfStreamException();
        }

        bool bit = (CurrentByte >> 7 - BitPosition & 1) == 1;
        BitPosition = (BitPosition + 1) % 8; // Reset to 0 after 8 bits
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
        Stream?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Represents the backing stream of this bitstream reader.
    /// </summary>
    public Stream BaseStream => Stream;

    /// <summary>
    /// Length, in bytes, in the entire stream.
    /// </summary>
    public long Length => Stream.Length;

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
            b = ReadBits(count);
        }
        catch
        {
            this.GoTo(activeState);
            throw;
        }

        this.GoTo(activeState);
        return b;
    }

    public uint ReadByte()
    {
        uint b = ReadBits(8);
        return b;
    }

    public void GoToStart()
    {
        Stream.Position = 0;
        CurrentByte = 0;
        BitPosition = 0;
    }
}
