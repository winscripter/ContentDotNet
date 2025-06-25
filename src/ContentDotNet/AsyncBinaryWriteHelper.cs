namespace ContentDotNet;

/// <summary>
///   Allows writing primitive values like <see cref="int"/> or <see cref="string"/>
///   to <see cref="BinaryWriter"/> asynchronously.
/// </summary>
public static class AsyncBinaryWriteHelper
{
    /// <summary>
    /// Asynchronously writes a <see cref="byte"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="byte"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, byte value)
    {
        var data = new Memory<byte>(new byte[sizeof(byte)]);
        data.Span[0] = value;
        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="sbyte"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="sbyte"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, sbyte value)
    {
        var data = new Memory<byte>(new byte[sizeof(sbyte)]);
        data.Span[0] = (byte)value;
        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="ushort"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="ushort"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, ushort value)
    {
        const int SIZE = sizeof(ushort);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(value & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="short"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="short"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, short value)
    {
        const int SIZE = sizeof(short);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(value & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes an <see cref="int"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="int"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, int value)
    {
        const int SIZE = sizeof(int);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(value & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="uint"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="uint"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, uint value)
    {
        const int SIZE = sizeof(uint);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(value & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="long"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="long"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, long value)
    {
        const int SIZE = sizeof(long);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(value & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="ulong"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="ulong"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, ulong value)
    {
        const int SIZE = sizeof(ulong);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(value & (uint)(1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="float"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="float"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, float value)
    {
        const int SIZE = sizeof(int);

        int actualValue = BitConverter.SingleToInt32Bits(value);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(actualValue & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }

    /// <summary>
    /// Asynchronously writes a <see cref="double"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="double"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(BinaryWriter writer, double value)
    {
        const int SIZE = sizeof(long);

        long actualValue = BitConverter.DoubleToInt64Bits(value);

        var data = new Memory<byte>(new byte[SIZE]);
        for (int i = 0; i < SIZE; i++)
            data.Span[i] = (byte)(actualValue & (1 << i));

        await writer.BaseStream.WriteAsync(data);
    }
}
