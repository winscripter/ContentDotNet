using System.Buffers.Binary;
using System.Text;
namespace ContentDotNet;

/// <summary>
/// BinaryReader variant that supports reading primitives in specified endianness without heap allocations using Span and stackalloc.
/// </summary>
public class EndianAwareBinaryReader : BinaryReader
{
    private readonly Endianness _endianness;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndianAwareBinaryReader"/> class.
    /// </summary>
    /// <param name="input">The input stream.</param>
    /// <param name="endianness">The endianness to use when reading data.</param>
    /// <param name="encoding">The character encoding (optional, defaults to UTF8).</param>
    public EndianAwareBinaryReader(Stream input, Endianness endianness, Encoding? encoding = null)
        : base(input, encoding ?? Encoding.UTF8, leaveOpen: false)
    {
        _endianness = endianness;
    }

    /// <inheritdoc />
    public override short ReadInt16()
    {
        Span<byte> buffer = stackalloc byte[2];
        FillBuffer(buffer);
        return _endianness == Endianness.BigEndian
            ? BinaryPrimitives.ReadInt16BigEndian(buffer)
            : BinaryPrimitives.ReadInt16LittleEndian(buffer);
    }

    /// <inheritdoc />
    public override ushort ReadUInt16()
    {
        Span<byte> buffer = stackalloc byte[2];
        FillBuffer(buffer);
        return _endianness == Endianness.BigEndian
            ? BinaryPrimitives.ReadUInt16BigEndian(buffer)
            : BinaryPrimitives.ReadUInt16LittleEndian(buffer);
    }

    /// <inheritdoc />
    public override int ReadInt32()
    {
        Span<byte> buffer = stackalloc byte[4];
        FillBuffer(buffer);
        return _endianness == Endianness.BigEndian
            ? BinaryPrimitives.ReadInt32BigEndian(buffer)
            : BinaryPrimitives.ReadInt32LittleEndian(buffer);
    }

    /// <inheritdoc />
    public override uint ReadUInt32()
    {
        Span<byte> buffer = stackalloc byte[4];
        FillBuffer(buffer);
        return _endianness == Endianness.BigEndian
            ? BinaryPrimitives.ReadUInt32BigEndian(buffer)
            : BinaryPrimitives.ReadUInt32LittleEndian(buffer);
    }

    /// <inheritdoc />
    public override long ReadInt64()
    {
        Span<byte> buffer = stackalloc byte[8];
        FillBuffer(buffer);
        return _endianness == Endianness.BigEndian
            ? BinaryPrimitives.ReadInt64BigEndian(buffer)
            : BinaryPrimitives.ReadInt64LittleEndian(buffer);
    }

    /// <inheritdoc />
    public override ulong ReadUInt64()
    {
        Span<byte> buffer = stackalloc byte[8];
        FillBuffer(buffer);
        return _endianness == Endianness.BigEndian
            ? BinaryPrimitives.ReadUInt64BigEndian(buffer)
            : BinaryPrimitives.ReadUInt64LittleEndian(buffer);
    }

    /// <inheritdoc />
    public override float ReadSingle()
    {
        int intValue = ReadInt32();
        return BitConverter.Int32BitsToSingle(intValue);
    }

    /// <inheritdoc />
    public override double ReadDouble()
    {
        long longValue = ReadInt64();
        return BitConverter.Int64BitsToDouble(longValue);
    }

    /// <summary>
    /// Fills the provided buffer with bytes from the stream.
    /// Throws <see cref="EndOfStreamException"/> if not enough bytes are available.
    /// </summary>
    /// <param name="buffer">The buffer to fill.</param>
    private void FillBuffer(Span<byte> buffer)
    {
        int read = 0;
        while (read < buffer.Length)
        {
            int n = BaseStream.Read(buffer[read..]);
            if (n == 0)
                throw new EndOfStreamException($"Could not read {buffer.Length} bytes from stream.");
            read += n;
        }
    }
}

/// <summary>
/// BinaryWriter variant that supports writing primitives in specified endianness without heap allocations using Span and stackalloc.
/// </summary>
public class EndianAwareBinaryWriter : BinaryWriter
{
    private readonly Endianness _endianness;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndianAwareBinaryWriter"/> class.
    /// </summary>
    /// <param name="output">The output stream.</param>
    /// <param name="endianness">The endianness to use when writing data.</param>
    /// <param name="encoding">The character encoding (optional, defaults to UTF8).</param>
    public EndianAwareBinaryWriter(Stream output, Endianness endianness, Encoding? encoding = null)
        : base(output, encoding ?? Encoding.UTF8, leaveOpen: false)
    {
        _endianness = endianness;
    }

    /// <inheritdoc />
    public override void Write(short value)
    {
        Span<byte> buffer = stackalloc byte[2];
        if (_endianness == Endianness.BigEndian)
            BinaryPrimitives.WriteInt16BigEndian(buffer, value);
        else
            BinaryPrimitives.WriteInt16LittleEndian(buffer, value);

        BaseStream.Write(buffer);
    }

    /// <inheritdoc />
    public override void Write(ushort value)
    {
        Span<byte> buffer = stackalloc byte[2];
        if (_endianness == Endianness.BigEndian)
            BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
        else
            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);

        BaseStream.Write(buffer);
    }

    /// <inheritdoc />
    public override void Write(int value)
    {
        Span<byte> buffer = stackalloc byte[4];
        if (_endianness == Endianness.BigEndian)
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
        else
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);

        BaseStream.Write(buffer);
    }

    /// <inheritdoc />
    public override void Write(uint value)
    {
        Span<byte> buffer = stackalloc byte[4];
        if (_endianness == Endianness.BigEndian)
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
        else
            BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);

        BaseStream.Write(buffer);
    }

    /// <inheritdoc />
    public override void Write(long value)
    {
        Span<byte> buffer = stackalloc byte[8];
        if (_endianness == Endianness.BigEndian)
            BinaryPrimitives.WriteInt64BigEndian(buffer, value);
        else
            BinaryPrimitives.WriteInt64LittleEndian(buffer, value);

        BaseStream.Write(buffer);
    }

    /// <inheritdoc />
    public override void Write(ulong value)
    {
        Span<byte> buffer = stackalloc byte[8];
        if (_endianness == Endianness.BigEndian)
            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
        else
            BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);

        BaseStream.Write(buffer);
    }

    /// <inheritdoc />
    public override void Write(float value)
    {
        int intValue = BitConverter.SingleToInt32Bits(value);
        Write(intValue);
    }

    /// <inheritdoc />
    public override void Write(double value)
    {
        long longValue = BitConverter.DoubleToInt64Bits(value);
        Write(longValue);
    }
}
