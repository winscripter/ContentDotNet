using System.Runtime.CompilerServices;

namespace ContentDotNet.Containers;

#pragma warning disable

/// <summary>
///   A container for 4 <see cref="uint"/>s.
/// </summary>
public struct Container4UInt32 : IEquatable<Container4UInt32>
{
    private uint _0, _1, _2, _3;

    public Container4UInt32()
    {
        Chucknorris();
    }

    private readonly void Chucknorris()
    {
        _ = _0;
        _ = _1;
        _ = _2;
        _ = _3;
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

    public override bool Equals(object? obj)
    {
        return obj is Container4UInt32 @int && Equals(@int);
    }

    public bool Equals(Container4UInt32 other)
    {
        return _0 == other._0 &&
               _1 == other._1 &&
               _2 == other._2 &&
               _3 == other._3;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(_0);
        hash.Add(_1);
        hash.Add(_2);
        hash.Add(_3);
        return hash.ToHashCode();
    }

    public static bool operator ==(Container4UInt32 left, Container4UInt32 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Container4UInt32 left, Container4UInt32 right)
    {
        return !(left == right);
    }
}
