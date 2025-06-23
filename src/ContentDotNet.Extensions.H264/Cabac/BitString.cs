using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
/// Represents a bit string with a specified value and length.
/// </summary>
public struct BitString
{
    private int _value;
    private int _length;

    /// <summary>
    /// Initializes a new instance of the <see cref="BitString"/> struct with the specified value and length.
    /// </summary>
    /// <param name="value">The integer value representing the bits.</param>
    /// <param name="length">The number of bits in the bit string.</param>
    public BitString(int value, int length)
    {
        _value = value;
        _length = length;
    }

    /// <summary>
    /// Creates a <see cref="BitString"/> from an integer value, using the position of the highest set bit as the length.
    /// </summary>
    /// <param name="value">The integer value to convert.</param>
    /// <returns>A <see cref="BitString"/> representing the value.</returns>
    public static BitString From(int value)
    {
        const int bits = 32;
        for (int i = 0; i < bits; i++)
        {
            if ((value & (1 << i)) != 0)
                return new BitString(value, i + 1);
        }
        return new BitString(value, 0);
    }

    /// <summary>
    /// Creates a <see cref="BitString"/> from an unsigned integer value, using the position of the highest set bit as the length.
    /// </summary>
    /// <param name="value">The unsigned integer value to convert.</param>
    /// <returns>A <see cref="BitString"/> representing the value.</returns>
    public static BitString From(uint value)
    {
        const int bits = 32;
        for (int i = 0; i < bits; i++)
        {
            if ((value & (1 << i)) != 0)
                return new BitString((int)value, i + 1);
        }
        return new BitString((int)value, 0);
    }

    /// <summary>
    /// Gets or sets the bit at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the bit.</param>
    /// <returns><c>true</c> if the bit is set; otherwise, <c>false</c>.</returns>
    public bool this[int index]
    {
        readonly get => (_value & (1 << index)) != 0;
        set
        {
            if (value)
                _value |= (1 << index);
            else
                _value &= ~(1 << index);
        }
    }

    /// <summary>
    /// Gets the integer value representing the bits.
    /// </summary>
    public readonly int Value => _value;

    /// <summary>
    /// Gets or sets the number of bits in the bit string.
    /// </summary>
    public int Length
    {
        readonly get => _length;
        set => _length = value;
    }

    /// <summary>
    /// Gets a value indicating whether the bits are contiguous ones.
    /// </summary>
    public readonly bool IsContiguousOnes => Intrinsic.IsContiguousOnes(_value);

    /// <summary>
    /// Concatenates two <see cref="BitString"/> instances.
    /// </summary>
    /// <param name="left">The left <see cref="BitString"/>.</param>
    /// <param name="right">The right <see cref="BitString"/>.</param>
    /// <returns>A new <see cref="BitString"/> representing the concatenation of the two bit strings.</returns>
    public static BitString operator +(BitString left, BitString right)
    {
        return new BitString((left.Value << right.Length) | right.Value, left.Length + right.Length);
    }
}
