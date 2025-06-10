namespace ContentDotNet.BitStream;

/// <summary>
///   Bitstream reader extensions.
/// </summary>
public static class BitStreamReaderExtensions
{
    /// <summary>
    ///   Moves the bitstream reader's position to the specified one.
    /// </summary>
    /// <param name="reader">Bitstream reader.</param>
    /// <param name="value">Position to go to.</param>
    public static void GoTo(this BitStreamReader reader, ReaderState value)
    {
        reader.Stream.Position = value.ByteOffset;
        reader.BitPosition = value.BitPosition;
        reader.CurrentByte = value.CurrentByte;
    }

    /// <summary>
    ///   Captures the current position of the bitstream, allowing it to be
    ///   reused to jump to the current position in the future.
    /// </summary>
    /// <param name="reader">Input bitstream.</param>
    /// <returns>Current bitstream location.</returns>
    public static ReaderState GetState(this BitStreamReader reader)
    {
        return new ReaderState(
            reader.Stream.Position,
            (byte)reader.BitPosition,
            (byte)reader.CurrentByte
        );
    }

    /// <summary>
    ///   Seeks the bitstream reader to a new position based on the specified offset and origin.
    /// </summary>
    /// <param name="reader">The bitstream reader to seek.</param>
    /// <param name="offset">The byte offset relative to the origin.</param>
    /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
    public static void Seek(this BitStreamReader reader, long offset, SeekOrigin origin)
    {
        ReaderState state = reader.GetState();
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
                state.ByteOffset = reader.BaseStream.Length - offset;
                break;
        }
        reader.GoTo(state);
    }

    /// <summary>
    ///   Moves the bitstream reader's position backwards by the specified number of bytes.
    /// </summary>
    /// <param name="reader">The bitstream reader to backtrack.</param>
    /// <param name="by">The number of bytes to move back.</param>
    public static void Backtrack(this BitStreamReader reader, long by)
    {
        ReaderState state = reader.GetState();
        state.BitPosition = 0;
        state.ByteOffset -= by;
        reader.GoTo(state);
    }
}
