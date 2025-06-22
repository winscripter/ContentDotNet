namespace ContentDotNet.BitStream;

/// <summary>
///   Writer State allows one to track the bitstream writer's location.
/// </summary>
public struct WriterState : IEquatable<WriterState>
{
    public static readonly WriterState Blank = new(-1, 255, 255);

    /// <summary>
    ///   Byte offset.
    /// </summary>
    public long ByteOffset;

    /// <summary>
    ///   Bit position.
    /// </summary>
    public byte BitPosition;

    /// <summary>
    ///   Current byte.
    /// </summary>
    public byte CurrentByte;

    public WriterState(long byteOffset, byte bitPosition, byte currentByte)
    {
        ByteOffset = byteOffset;
        BitPosition = bitPosition;
        CurrentByte = currentByte;
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is ReaderState state && Equals(state);
    }

    public readonly bool Equals(WriterState other)
    {
        return ByteOffset == other.ByteOffset &&
               BitPosition == other.BitPosition &&
               CurrentByte == other.CurrentByte;
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(ByteOffset, BitPosition, CurrentByte);
    }

    public static bool operator ==(WriterState left, WriterState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WriterState left, WriterState right)
    {
        return !(left == right);
    }
}

