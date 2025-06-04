using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Containers;

/// <summary>
///   A container for 8 <see cref="PackedFlags32"/>s.
/// </summary>
public struct Container8PackedFlags32 : IEquatable<Container8PackedFlags32>
{
    private PackedFlags32 _0, _1, _2, _3, _4, _5, _6, _7;

    public Container8PackedFlags32()
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
    }

    public PackedFlags32 this[int index]
    {
        get
        {
            if ((uint)index >= 4)
                throw new IndexOutOfRangeException();

            ref PackedFlags32 firstElement = ref _0;
            return Unsafe.Add(ref firstElement, index);
        }
        set
        {
            if ((uint)index >= 4)
                throw new IndexOutOfRangeException();

            ref PackedFlags32 firstElement = ref _0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Container8PackedFlags32 @int && Equals(@int);
    }

    public readonly bool Equals(Container8PackedFlags32 other)
    {
        return _0 == other._0 &&
               _1 == other._1 &&
               _2 == other._2 &&
               _3 == other._3 &&
               _4 == other._4 &&
               _5 == other._5 &&
               _6 == other._6 &&
               _7 == other._7;
    }

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
        return hash.ToHashCode();
    }

    public static bool operator ==(Container8PackedFlags32 left, Container8PackedFlags32 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Container8PackedFlags32 left, Container8PackedFlags32 right)
    {
        return !(left == right);
    }
}
