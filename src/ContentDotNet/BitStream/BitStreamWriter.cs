namespace ContentDotNet.BitStream;

/// <summary>
/// Writes data at bit level
/// </summary>
public class BitStreamWriter(Stream output) : IDisposable
{
    private readonly Stream stream = output ?? throw new ArgumentNullException(nameof(output));
    private int currentByte = 0;
    private int bitPosition = 0; // Tracks current bit position in the byte

    /// <summary>
    ///   The bit position.
    /// </summary>
    public int BitPosition
    {
        get => bitPosition;
        set => bitPosition = value;
    }

    /// <summary>
    ///   The current byte.
    /// </summary>
    public int CurrentByte
    {
        get => currentByte;
        set => currentByte = value;
    }

    /// <summary>
    /// Writes a single bit to the bitstream.
    /// </summary>
    /// <param name="bit">The bit to write.</param>
    public void WriteBit(bool bit)
    {
        if (bit)
        {
            currentByte |= 1 << 7 - bitPosition;
        }

        bitPosition++;
        if (bitPosition == 8) // Byte is full
        {
            FlushByte();
        }
    }

    /// <summary>
    /// Writes a single bit to the bitstream.
    /// </summary>
    /// <param name="bit">The bit to write.</param>
    public async Task WriteBitAsync(bool bit)
    {
        if (bit)
        {
            currentByte |= 1 << 7 - bitPosition;
        }

        bitPosition++;
        if (bitPosition == 8) // Byte is full
        {
            await FlushByteAsync();
        }
    }

    /// <summary>
    /// Writes the specified number of bits to the bitstream.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="count">Number of bits to write.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void WriteBits(uint value, uint count)
    {
        if (count < 1 || count > 32)
            throw new ArgumentOutOfRangeException(nameof(count));

        for (int i = (int)count - 1; i >= 0; i--)
        {
            WriteBit((value >> i & 1) == 1);
        }
    }

    /// <summary>
    /// Writes the specified number of bits to the bitstream.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="count">Number of bits to write.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public async Task WriteBitsAsync(uint value, uint count)
    {
        if (count < 1 || count > 32)
            throw new ArgumentOutOfRangeException(nameof(count));

        for (int i = (int)count - 1; i >= 0; i--)
        {
            await WriteBitAsync((value >> i & 1) == 1);
        }
    }

    /// <summary>
    /// Writes an Unsigned Exponential Golomb.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void WriteUE(uint value)
    {
        if (value > uint.MaxValue >> 1)
            throw new ArgumentOutOfRangeException(nameof(value), "UE Golomb value too large.");

        uint leadingZeros = 0;
        uint temp = value + 1;

        while (temp > 1)
        {
            temp >>= 1;
            leadingZeros++;
        }

        for (uint i = 0; i < leadingZeros; i++)
        {
            WriteBit(false);
        }

        WriteBit(true);
        WriteBits(value + 1 - (1u << (int)leadingZeros), leadingZeros);

        //WriteBit(true);
    }

    /// <summary>
    /// Writes an Unsigned Exponential Golomb.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public async Task WriteUEAsync(uint value)
    {
        if (value > uint.MaxValue >> 1)
            throw new ArgumentOutOfRangeException(nameof(value), "UE Golomb value too large.");

        uint leadingZeros = 0;
        uint temp = value + 1;

        while (temp > 1)
        {
            temp >>= 1;
            leadingZeros++;
        }

        for (uint i = 0; i < leadingZeros; i++)
        {
            await WriteBitAsync(false);
        }

        await WriteBitAsync(true);
        await WriteBitsAsync(value + 1 - (1u << (int)leadingZeros), leadingZeros);

        //while (bitPosition > 0)
        //{
        //    await WriteBitAsync(false);
        //}
    }

    /// <summary>
    /// Writes a Signed Exponential Golomb.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteSE(int value)
    {
        uint codeNum = (uint)(value < 0 ? -value * 2 - 1 : value * 2);
        WriteUE(codeNum);
    }

    /// <summary>
    /// Writes a Signed Exponential Golomb.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public async Task WriteSEAsync(int value)
    {
        uint codeNum = (uint)(value < 0 ? -value * 2 - 1 : value * 2);
        await WriteUEAsync(codeNum);
    }

    /// <summary>
    /// Flushes the current byte to the stream.
    /// </summary>
    private void FlushByte()
    {
        stream.WriteByte((byte)currentByte);
        currentByte = 0;
        bitPosition = 0;
    }

    /// <summary>
    /// Flushes the current byte to the stream.
    /// </summary>
    private async Task FlushByteAsync()
    {
        await stream.WriteAsync(new([(byte)currentByte]));
        currentByte = 0;
        bitPosition = 0;
    }

    /// <summary>
    /// Releases memory from the bitstream.
    /// </summary>
    public void Dispose()
    {
        if (bitPosition > 0)
        {
            FlushByte();
        }

        stream?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Represents a base stream where data is written to.
    /// </summary>
    public Stream BaseStream => stream;

    public void GoToStart()
    {
        BaseStream.Position = 0;
        CurrentByte = 0;
        BitPosition = 0;
    }
}
