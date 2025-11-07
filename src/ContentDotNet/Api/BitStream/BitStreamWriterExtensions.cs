namespace ContentDotNet.Api.BitStream;
/// <summary>
///   Bitstream writer extensions.
/// </summary>
public static class BitStreamWriterExtensions
{
    /// <summary>
    ///   Moves the bitstream writer's position to the specified one.
    /// </summary>
    /// <param name="writer">Bitstream writer.</param>
    /// <param name="value">Position to go to.</param>
    public static void GoTo(this BitStreamWriter writer, WriterState value)
    {
        writer.BaseStream.Position = value.ByteOffset;
        writer.BitPosition = value.BitPosition;
        writer.CurrentByte = value.CurrentByte;
    }

    /// <summary>
    ///   Captures the current position of the bitstream, allowing it to be
    ///   reused to jump to the current position in the future.
    /// </summary>
    /// <param name="writer">Input bitstream.</param>
    /// <returns>Current bitstream location.</returns>
    public static WriterState GetState(this BitStreamWriter writer)
    {
        return new WriterState(
            writer.BaseStream.Position,
            (byte)writer.BitPosition,
            (byte)writer.CurrentByte
        );
    }

    /// <summary>
    ///   Seeks the bitstream writer to a new position based on the specified offset and origin.
    /// </summary>
    /// <param name="writer">The bitstream writer to seek.</param>
    /// <param name="offset">The byte offset relative to the origin.</param>
    /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
    public static void Seek(this BitStreamWriter writer, long offset, SeekOrigin origin)
    {
        WriterState state = writer.GetState();
        state.BitPosition = 0;
        switch (origin)
        {
            case SeekOrigin.Begin:
                state.ByteOffset = offset;
                break;

            case SeekOrigin.Current:
                state.ByteOffset += offset;
                break;

            case SeekOrigin.End:
                state.ByteOffset = writer.BaseStream.Length - offset;
                break;
        }
        writer.GoTo(state);
    }

    /// <summary>
    ///   Moves the bitstream writer's position backwards by the specified number of bytes.
    /// </summary>
    /// <param name="writer">The bitstream writer to backtrack.</param>
    /// <param name="by">The number of bytes to move back.</param>
    public static void Backtrack(this BitStreamWriter writer, long by)
    {
        WriterState state = writer.GetState();
        state.BitPosition = 0;
        state.ByteOffset -= by + 1;
        writer.GoTo(state);
    }
}
