namespace ContentDotNet;

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
        reader.stream.Position = value.ByteOffset;
        reader.bitPosition = value.BitPosition;
        reader.currentByte = value.CurrentByte;
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
            reader.stream.Position,
            (byte)reader.bitPosition,
            (byte)reader.currentByte
        );
    }
}
