namespace ContentDotNet.Abstractions;

/// <summary>
///   Reader State allows one to track the bitstream reader's location.
/// </summary>
public readonly struct ReaderState : IEquatable<ReaderState>
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

    public readonly override bool Equals(object? obj)
    {
        return obj is ReaderState state && Equals(state);
    }

    public readonly bool Equals(ReaderState other)
    {
        return ByteOffset == other.ByteOffset &&
               BitPosition == other.BitPosition &&
               CurrentByte == other.CurrentByte;
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(ByteOffset, BitPosition, CurrentByte);
    }

    public static bool operator ==(ReaderState left, ReaderState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ReaderState left, ReaderState right)
    {
        return !(left == right);
    }
}
