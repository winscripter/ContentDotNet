using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Primitives;

/// <summary>
/// Represents a FourCC (Four Character Code), backed by a <see cref="uint"/>.
/// </summary>
[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public struct FourCC : IEquatable<FourCC>
#if NET7_0_OR_GREATER // IParsable was introduced in .NET 7.0
    , IParsable<FourCC>
#endif
{
    private uint _fcc;

    /// <summary>
    /// Initializes a new instance of the <see cref="FourCC"/> struct with a specified unsigned integer value.
    /// </summary>
    /// <param name="fourCC">The FourCC value.</param>
    public FourCC(uint fourCC)
    {
        _fcc = fourCC;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FourCC"/> struct with a specified signed integer value.
    /// </summary>
    /// <param name="fourCC">The FourCC value.</param>
    public FourCC(int fourCC)
        : this(unchecked((uint)fourCC))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FourCC"/> struct from a four-character string.
    /// </summary>
    /// <param name="fourCC">A string consisting of exactly four characters.</param>
    /// <exception cref="ArgumentException">Thrown when the input string does not have exactly four characters.</exception>
    public FourCC(string fourCC)
    {
        if (fourCC.Length != 4)
            throw new ArgumentException("Argument must have exactly 4 characters; got " + fourCC.Length, nameof(fourCC));

        _fcc = (uint)(fourCC[0] << 24 | fourCC[1] << 16 | fourCC[2] << 8 | fourCC[3]);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FourCC"/> struct from four characters.
    /// </summary>
    /// <param name="a">The first character.</param>
    /// <param name="b">The second character.</param>
    /// <param name="c">The third character.</param>
    /// <param name="d">The fourth character.</param>
    public FourCC(char a, char b, char c, char d)
    {
        _fcc = (uint)(a << 24 | b << 16 | c << 8 | d);
    }

    /// <summary>
    /// Gets or sets the FourCC value as a 32-bit unsigned integer.
    /// </summary>
    public uint Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => _fcc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _fcc = value;
    }

    /// <summary>
    /// Gets the FourCC value as a four-character string.
    /// </summary>
    public readonly string ValueText
    {
        get
        {
            return string.Create(4, Value, (span, value) =>
            {
                span[0] = (char)(value >> 24 & 0xFF);
                span[1] = (char)(value >> 16 & 0xFF);
                span[2] = (char)(value >> 8 & 0xFF);
                span[3] = (char)(value & 0xFF);
            });
        }
    }

    /// <summary>
    /// Attempts to parse a four-character string into a <see cref="FourCC"/> instance.
    /// </summary>
    /// <param name="fourCC">A string consisting of exactly four characters.</param>
    /// <param name="provider">The format provider</param>
    /// <param name="result">The parsed <see cref="FourCC"/> instance if successful.</param>
    /// <returns><see langword="true"/> if parsing is successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? fourCC, IFormatProvider? provider, out FourCC result)
    {
        if (fourCC?.Length != 4)
        {
            result = default;
            return false;
        }

        result = new FourCC(fourCC);
        return true;
    }

    /// <summary>
    /// Parses a four-character string into a <see cref="FourCC"/> instance.
    /// </summary>
    /// <param name="fourCC">A string consisting of exactly four characters.</param>
    /// <param name="prv">The format provider</param>
    /// <returns>A new <see cref="FourCC"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the input string does not have exactly four characters.</exception>
    public static FourCC Parse(string fourCC, IFormatProvider? prv = null)
    {
        if (fourCC.Length != 4)
            throw new ArgumentException("Argument must have exactly 4 characters; got " + fourCC.Length, nameof(fourCC));

        return new FourCC(fourCC);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true"/> if equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is FourCC cC && Equals(cC);
    }

    /// <summary>
    /// Determines whether the specified <see cref="FourCC"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="FourCC"/> to compare.</param>
    /// <returns><see langword="true"/> if equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(FourCC other)
    {
        return _fcc == other._fcc;
    }

    /// <summary>
    /// Gets the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(_fcc, Value);
    }

    /// <summary>
    /// Checks if two <see cref="FourCC"/> instances are equal.
    /// </summary>
    public static bool operator ==(FourCC left, FourCC right) => left.Equals(right);

    /// <summary>
    /// Checks if two <see cref="FourCC"/> instances are not equal.
    /// </summary>
    public static bool operator !=(FourCC left, FourCC right) => !(left == right);

    /// <summary>
    /// Returns the FourCC value as a string.
    /// </summary>
    /// <returns>A four-character string representation of the FourCC value.</returns>
    public readonly override string ToString() => ValueText;

    /// <summary>
    ///   Implicitly converts <see cref="FourCC"/> to <see cref="uint"/>.
    /// </summary>
    /// <param name="fourCC"><see cref="FourCC"/></param>
    public static implicit operator uint(FourCC fourCC) => fourCC._fcc;

    /// <summary>
    ///   Implicitly converts <see cref="uint"/> to <see cref="FourCC"/>.
    /// </summary>
    /// <param name="fourCC"><see cref="uint"/></param>
    public static implicit operator FourCC(uint fourCC) => new(fourCC);
}
