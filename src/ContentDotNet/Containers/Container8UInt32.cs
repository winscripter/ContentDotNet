using System.Runtime.CompilerServices;

namespace ContentDotNet.Containers;

/// <summary>
///   A container for 8 <see cref="uint"/>s.
/// </summary>
public struct Container8UInt32 : IEquatable<Container8UInt32>
{
    private uint _0, _1, _2, _3, _4, _5, _6, _7;

    public Container8UInt32()
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

    public uint this[int index]
    {
        get
        {
            if ((uint)index >= 4)
                throw new IndexOutOfRangeException();

            ref uint firstElement = ref _0;
            return Unsafe.Add(ref firstElement, index);
        }
        set
        {
            if ((uint)index >= 4)
                throw new IndexOutOfRangeException();

            ref uint firstElement = ref _0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Container8UInt32 @int && Equals(@int);
    }

    public readonly bool Equals(Container8UInt32 other)
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

    public static bool operator ==(Container8UInt32 left, Container8UInt32 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Container8UInt32 left, Container8UInt32 right)
    {
        return !(left == right);
    }
}
