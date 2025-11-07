using System.Numerics;

namespace ContentDotNet.Api.Colors;

/// <summary>
///   Represents a YCbCr color.
/// </summary>
public struct YCbCr : IColor, IEquatable<YCbCr>
{
    private byte _y;
    private byte _cb;
    private byte _cr;

    /// <summary>
    ///   Initializes a new instance of the <see cref="YCbCr"/> struct with the specified Y, Cb, and Cr components.
    /// </summary>
    /// <param name="y">The Y (luminance) component.</param>
    /// <param name="cb">The Cb (blue-difference chroma) component.</param>
    /// <param name="cr">The Cr (red-difference chroma) component.</param>
    public YCbCr(byte y, byte cb, byte cr)
    {
        _y = y;
        _cb = cb;
        _cr = cr;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="YCbCr"/> struct with all components set to zero.
    /// </summary>
    public YCbCr()
    {
        _y = 0;
        _cb = 0;
        _cr = 0;
    }

    /// <summary>
    ///   Gets or sets the Y (luminance) component.
    /// </summary>
    public byte Y
    {
        readonly get => _y;
        set => _y = value;
    }

    /// <summary>
    ///   Gets or sets the Cb (blue-difference chroma) component.
    /// </summary>
    public byte Cb
    {
        readonly get => _cb;
        set => _cb = value;
    }

    /// <summary>
    ///   Gets or sets the Cr (red-difference chroma) component.
    /// </summary>
    public byte Cr
    {
        readonly get => _cr;
        set => _cr = value;
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is YCbCr cr && Equals(cr);
    }

    /// <summary>
    ///   Indicates whether the current object is equal to another <see cref="YCbCr"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="YCbCr"/> to compare with.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(YCbCr other)
    {
        return _y == other._y &&
               _cb == other._cb &&
               _cr == other._cr;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(_y, _cb, _cr);
    }

    /// <summary>
    ///   Packs the Y, Cb, and Cr components into a 24-bit <see cref="uint"/> value.
    /// </summary>
    /// <returns>The packed color representation.</returns>
    public readonly uint Pack() => (uint)((_y << 16) | (_cb << 8) | _cr);

    /// <summary>
    ///   Packs the color into a <see cref="ulong"/> value.
    /// </summary>
    /// <returns>The packed color representation as <see cref="ulong"/>.</returns>
    public readonly ulong LongPack() => Pack();

    /// <summary>
    ///   Determines whether two <see cref="YCbCr"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="YCbCr"/> to compare.</param>
    /// <param name="right">The second <see cref="YCbCr"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(YCbCr left, YCbCr right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="YCbCr"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="YCbCr"/> to compare.</param>
    /// <param name="right">The second <see cref="YCbCr"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(YCbCr left, YCbCr right)
    {
        return !(left == right);
    }
}
