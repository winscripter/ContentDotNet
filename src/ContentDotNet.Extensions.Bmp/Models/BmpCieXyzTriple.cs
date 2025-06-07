namespace ContentDotNet.Extensions.Bmp.Models;

/// <summary>
///   Represents a triple of <see cref="BmpCieXyz"/> values for the red, green, and blue color channels.
/// </summary>
public struct BmpCieXyzTriple : IEquatable<BmpCieXyzTriple>
{
    /// <summary>
    ///   The <see cref="BmpCieXyz"/> value for the red channel.
    /// </summary>
    public BmpCieXyz Red;

    /// <summary>
    ///   The <see cref="BmpCieXyz"/> value for the green channel.
    /// </summary>
    public BmpCieXyz Green;

    /// <summary>
    ///   The <see cref="BmpCieXyz"/> value for the blue channel.
    /// </summary>
    public BmpCieXyz Blue;

    /// <summary>
    ///   Initializes a new instance of the <see cref="BmpCieXyzTriple"/> struct with the specified red, green, and blue values.
    /// </summary>
    /// <param name="red">The <see cref="BmpCieXyz"/> value for the red channel.</param>
    /// <param name="green">The <see cref="BmpCieXyz"/> value for the green channel.</param>
    /// <param name="blue">The <see cref="BmpCieXyz"/> value for the blue channel.</param>
    public BmpCieXyzTriple(BmpCieXyz red, BmpCieXyz green, BmpCieXyz blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current <see cref="BmpCieXyzTriple"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is BmpCieXyzTriple triple && Equals(triple);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="BmpCieXyzTriple"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="BmpCieXyzTriple"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="BmpCieXyzTriple"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(BmpCieXyzTriple other)
    {
        return Red.Equals(other.Red) &&
               Green.Equals(other.Green) &&
               Blue.Equals(other.Blue);
    }

    /// <summary>
    ///   Returns a hash code for the current <see cref="BmpCieXyzTriple"/> instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Red, Green, Blue);
    }

    /// <summary>
    ///   Determines whether two <see cref="BmpCieXyzTriple"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="BmpCieXyzTriple"/> to compare.</param>
    /// <param name="right">The second <see cref="BmpCieXyzTriple"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(BmpCieXyzTriple left, BmpCieXyzTriple right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="BmpCieXyzTriple"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="BmpCieXyzTriple"/> to compare.</param>
    /// <param name="right">The second <see cref="BmpCieXyzTriple"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(BmpCieXyzTriple left, BmpCieXyzTriple right)
    {
        return !(left == right);
    }
}
