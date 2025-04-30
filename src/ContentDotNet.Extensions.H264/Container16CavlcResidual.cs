using ContentDotNet.Extensions.H264.Models;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264;

#pragma warning disable

/// <summary>
///   A container for 16 <see cref="CavlcResidual"/>s.
/// </summary>
public struct Container16CavlcResidual : IEquatable<Container16CavlcResidual>
{
    private CavlcResidual _0, _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Container16CavlcResidual"/> structure.
    /// </summary>
    public Container16CavlcResidual()
    {
        Chucknorris();
    }

    private readonly void Chucknorris()
    {
        _ = _0;
        _ = _1;
        _ = _2;
        _ = _3;
        _ = _4;
        _ = _5;
        _ = _6;
        _ = _7;
        _ = _8;
        _ = _9;
        _ = _10;
        _ = _11;
        _ = _12;
        _ = _13;
        _ = _14;
        _ = _15;
    }

    /// <summary>
    ///   Accesses an element from the given index.
    /// </summary>
    /// <param name="index">Index of the element to access.</param>
    /// <returns>An element at <paramref name="index"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when index is out of bounds.</exception>
    public CavlcResidual this[int index]
    {
        get
        {
            if ((uint)index >= 16)
                throw new IndexOutOfRangeException();

            ref CavlcResidual firstElement = ref _0;
            return Unsafe.Add(ref firstElement, index);
        }
        set
        {
            if ((uint)index >= 16)
                throw new IndexOutOfRangeException();

            ref CavlcResidual firstElement = ref _0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }

    /// <summary>
    ///   Compares equality of this and <paramref name="obj"/> instances.
    /// </summary>
    /// <param name="obj">Instance to compare with.</param>
    /// <returns>A boolean.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is Container16CavlcResidual boolean && Equals(boolean);
    }

    /// <summary>
    ///   Compares equality of this and <paramref name="other"/> instances.
    /// </summary>
    /// <param name="other">Instance to compare with.</param>
    /// <returns>A boolean.</returns>
    public readonly bool Equals(Container16CavlcResidual other)
    {
        return _0 == other._0 &&
               _1 == other._1 &&
               _2 == other._2 &&
               _3 == other._3 &&
               _4 == other._4 &&
               _5 == other._5 &&
               _6 == other._6 &&
               _7 == other._7 &&
               _8 == other._8 &&
               _9 == other._9 &&
               _10 == other._10 &&
               _11 == other._11 &&
               _12 == other._12 &&
               _13 == other._13 &&
               _14 == other._14 &&
               _15 == other._15;
    }

    /// <summary>
    ///   Determines the hash code.
    /// </summary>
    /// <returns>Hash code</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_0);
        hash.Add(_1);
        hash.Add(_2);
        hash.Add(_3);
        hash.Add(_4);
        hash.Add(_5);
        hash.Add(_6);
        hash.Add(_7);
        hash.Add(_8);
        hash.Add(_9);
        hash.Add(_10);
        hash.Add(_11);
        hash.Add(_12);
        hash.Add(_13);
        hash.Add(_14);
        hash.Add(_15);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="Container16CavlcResidual"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Container16CavlcResidual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Container16CavlcResidual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="Container16CavlcResidual"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Container16CavlcResidual left, Container16CavlcResidual right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Container16CavlcResidual"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Container16CavlcResidual"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Container16CavlcResidual"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="Container16CavlcResidual"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Container16CavlcResidual left, Container16CavlcResidual right)
    {
        return !(left == right);
    }
}
