namespace ContentDotNet.Abstractions;

/// <summary>
///   Reader State allows one to track the bitstream reader's location.
/// </summary>
public readonly struct ReaderState
{
    public static readonly ReaderState Blank = new(-1, 255, 255);

    /// <summary>
    ///   Byte offset.
    /// </summary>
    public readonly long ByteOffset;

    /// <summary>
    ///   Bit position.
    /// </summary>
    public readonly byte BitPosition;

    /// <summary>
    ///   Current byte.
    /// </summary>
    public readonly byte CurrentByte;

    public ReaderState(long byteOffset, byte bitPosition, byte currentByte)
    {
        ByteOffset = byteOffset;
        BitPosition = bitPosition;
        CurrentByte = currentByte;
    }
}
