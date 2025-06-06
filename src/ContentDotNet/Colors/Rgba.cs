using System.Numerics;
using System.Runtime.Intrinsics;

namespace ContentDotNet.Colors;

/// <summary>
/// Represents a color in RGBA (Red, Green, Blue, Alpha) format using 32-bit integer components.
/// </summary>
public struct Rgba : IColor
{
    private Vector128<int> _backing;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba"/> struct with the specified red, green, blue, and alpha values.
    /// </summary>
    /// <param name="r">The red component (0-255).</param>
    /// <param name="g">The green component (0-255).</param>
    /// <param name="b">The blue component (0-255).</param>
    /// <param name="a">The alpha component (0-255).</param>
    public Rgba(int r, int g, int b, int a)
    {
        ReadOnlySpan<int> values = [r, g, b, a]; // Lives on the stack
        _backing = Vector128.Create(values);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba"/> struct with the specified red, green, and blue values. Alpha is set to 255.
    /// </summary>
    /// <param name="r">The red component (0-255).</param>
    /// <param name="g">The green component (0-255).</param>
    /// <param name="b">The blue component (0-255).</param>
    public Rgba(int r, int g, int b)
        : this(r, g, b, 255)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba"/> struct from a <see cref="Vector128{Int32}"/> backing vector.
    /// </summary>
    /// <param name="backing">The vector containing RGBA components.</param>
    public Rgba(Vector128<int> backing)
    {
        _backing = backing;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba"/> struct by copying another <see cref="Rgba"/> instance.
    /// </summary>
    /// <param name="other">The <see cref="Rgba"/> instance to copy.</param>
    public Rgba(Rgba other)
    {
        _backing = other._backing;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba"/> struct with all color components set to the specified value and alpha set to 255.
    /// </summary>
    /// <param name="uniformValue">The value for the red, green, and blue components (0-255).</param>
    public Rgba(int uniformValue)
    {
        _backing = Vector128.Create(uniformValue, uniformValue, uniformValue, 255);
    }

    /// <summary>
    /// Gets or sets the red component.
    /// </summary>
    public int R
    {
        readonly get => _backing.GetElement(0);
        set => _backing = _backing.WithElement(0, value);
    }

    /// <summary>
    /// Gets or sets the green component.
    /// </summary>
    public int G
    {
        readonly get => _backing.GetElement(1);
        set => _backing = _backing.WithElement(1, value);
    }

    /// <summary>
    /// Gets or sets the blue component.
    /// </summary>
    public int B
    {
        readonly get => _backing.GetElement(2);
        set => _backing = _backing.WithElement(2, value);
    }

    /// <summary>
    /// Gets or sets the alpha component.
    /// </summary>
    public int A
    {
        readonly get => _backing.GetElement(3);
        set => _backing = _backing.WithElement(3, value);
    }

    /// <summary>
    /// Creates a new <see cref="Rgba"/> instance from a packed 32-bit unsigned integer.
    /// </summary>
    /// <param name="packed">The packed RGBA value (R in highest byte, A in lowest byte).</param>
    /// <returns>A new <see cref="Rgba"/> instance.</returns>
    public static Rgba FromPacked(uint packed)
    {
        return new Rgba(
            (int)((packed >> 24) & 0xFF),
            (int)((packed >> 16) & 0xFF),
            (int)((packed >> 8) & 0xFF),
            (int)(packed & 0xFF)
        );
    }

    /// <summary>
    ///   Converts the hex color to its RGBA representation.
    /// </summary>
    /// <param name="hex">Input hexadecimal color.</param>
    /// <returns><see cref="Rgba"/>, converted from <see cref="Hex"/>.</returns>
    public static Rgba FromHex(Hex hex) =>
        new(hex.R, hex.G, hex.B, hex.A);

    /// <summary>
    /// Gets a value indicating whether the color is fully black (R, G, B = 0, A = 255).
    /// </summary>
    public readonly bool IsFullyBlack => R == 0 && G == 0 && B == 0 && A == 255;

    /// <summary>
    /// Gets a value indicating whether the color is fully white (R, G, B, A = 255).
    /// </summary>
    public readonly bool IsFullyWhite => R == 255 && G == 255 && B == 255 && A == 255;

    /// <summary>
    /// Gets a value indicating whether the color is fully transparent (A = 0).
    /// </summary>
    public readonly bool IsFullyTransparent => A == 0;

    /// <inheritdoc cref="IColor{TSelf}.Pack" />
    public readonly uint Pack() => (uint)(((byte)R << 24) | ((byte)G << 16) | ((byte)B << 8) | (byte)A);

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is Rgba rgba && Equals(rgba);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Rgba"/> is equal to the current <see cref="Rgba"/>.
    /// </summary>
    /// <param name="other">The <see cref="Rgba"/> to compare with the current <see cref="Rgba"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Rgba"/> is equal to the current <see cref="Rgba"/>; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(Rgba other)
    {
        return _backing.Equals(other._backing);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(_backing);
    }

    /// <inheritdoc cref="IColor{TSelf}.LongPack" />
    public readonly ulong LongPack() => Pack();

    public static IColor FromVector4(Vector4 v4)
    {
        return new Rgba((byte)v4.X, (byte)v4.Y, (byte)v4.Z, (byte)v4.W);
    }

    public static IColor FromVector3(Vector3 v3)
    {
        return new Rgba((byte)v3.X, (byte)v3.Y, (byte)v3.Z, 255);
    }

    /// <summary>
    /// Determines whether two <see cref="Rgba"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Rgba"/> to compare.</param>
    /// <param name="right">The second <see cref="Rgba"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Rgba left, Rgba right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Rgba"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Rgba"/> to compare.</param>
    /// <param name="right">The second <see cref="Rgba"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Rgba left, Rgba right)
    {
        return !(left == right);
    }
}
