namespace ContentDotNet.Binary;

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
    public static async Task WriteAsync(this BinaryWriter writer, byte value)
    {
        await writer.BaseStream.WriteAsync(new byte[] { value });
    }

    /// <summary>
    /// Asynchronously writes a <see cref="sbyte"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="sbyte"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, sbyte value)
    {
        await writer.BaseStream.WriteAsync(new byte[] { (byte)value });
    }

    /// <summary>
    /// Asynchronously writes a <see cref="ushort"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="ushort"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, ushort value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes a <see cref="short"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="short"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, short value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes an <see cref="int"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="int"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, int value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes a <see cref="uint"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="uint"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, uint value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes a <see cref="long"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="long"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, long value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes a <see cref="ulong"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="ulong"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, ulong value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes a <see cref="float"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="float"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, float value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// Asynchronously writes a <see cref="double"/> value to the underlying stream of the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    /// <param name="value">The <see cref="double"/> value to write.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task WriteAsync(this BinaryWriter writer, double value)
    {
        await writer.BaseStream.WriteAsync(BitConverter.GetBytes(value));
    }
}
