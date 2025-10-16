using System.Numerics;

namespace ContentDotNet.Colors;

/// <summary>
///   Represents an RGB color with 8 bits per channel.
/// </summary>
public struct Rgb : IColor, IEquatable<Rgb>
{
    private byte _r, _g, _b;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Rgb"/> struct with the specified red, green, and blue values.
    /// </summary>
    /// <param name="r">The red component (0-255).</param>
    /// <param name="g">The green component (0-255).</param>
    /// <param name="b">The blue component (0-255).</param>
    public Rgb(byte r, byte g, byte b)
    {
        _r = r;
        _g = g;
        _b = b;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Rgb"/> struct with all components set to zero.
    /// </summary>
    public Rgb()
    {
        _r = 0;
        _g = 0;
        _b = 0;
    }

    /// <summary>
    ///   Gets or sets the red component.
    /// </summary>
    public byte R
    {
        readonly get => _r;
        set => _r = value;
    }

    /// <summary>
    ///   Gets or sets the green component.
    /// </summary>
    public byte G
    {
        readonly get => _g;
        set => _g = value;
    }

    /// <summary>
    ///   Gets or sets the blue component.
    /// </summary>
    public byte B
    {
        readonly get => _b;
        set => _b = value;
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is Rgb rgb && Equals(rgb);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Rgb other)
    {
        return _r == other._r &&
               _g == other._g &&
               _b == other._b;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(_r, _g, _b);
    }

    /// <summary>
    ///   Packs the RGB color into a 24-bit unsigned integer (0xRRGGBB).
    /// </summary>
    /// <returns>The packed color as a <see cref="uint"/>.</returns>
    public readonly uint Pack() => (uint)((_r << 16) | (_g << 8) | _b);

    /// <summary>
    ///   Packs the RGB color into a 24-bit unsigned integer and returns it as a <see cref="ulong"/>.
    /// </summary>
    /// <returns>The packed color as a <see cref="ulong"/>.</returns>
    public readonly ulong LongPack() => Pack();

    /// <summary>
    ///   Determines whether two <see cref="Rgb"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Rgb"/> to compare.</param>
    /// <param name="right">The second <see cref="Rgb"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Rgb left, Rgb right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="Rgb"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Rgb"/> to compare.</param>
    /// <param name="right">The second <see cref="Rgb"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Rgb left, Rgb right)
    {
        return !(left == right);
    }
}
