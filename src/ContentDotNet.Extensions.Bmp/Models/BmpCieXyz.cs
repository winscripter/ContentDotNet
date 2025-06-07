namespace ContentDotNet.Extensions.Bmp.Models;

/// <summary>
///   .NET representation of the CIEXYZ Windows structure.
/// </summary>
public struct BmpCieXyz : IEquatable<BmpCieXyz>
{
    /// <summary>
    ///   The X component
    /// </summary>
    public int X;

    /// <summary>
    ///   The Y component
    /// </summary>
    public int Y;

    /// <summary>
    ///   The Z component
    /// </summary>
    public int Z;

    /// <summary>
    ///   Initializes a new instance of the <see cref="BmpCieXyz"/> struct with the specified X, Y, and Z components.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    public BmpCieXyz(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current <see cref="BmpCieXyz"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is BmpCieXyz xyz && Equals(xyz);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="BmpCieXyz"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="BmpCieXyz"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="BmpCieXyz"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(BmpCieXyz other)
    {
        return X == other.X &&
               Y == other.Y &&
               Z == other.Z;
    }

    /// <summary>
    ///   Returns a hash code for the current <see cref="BmpCieXyz"/> instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public static bool operator ==(BmpCieXyz left, BmpCieXyz right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(BmpCieXyz left, BmpCieXyz right)
    {
        return !(left == right);
    }
}
