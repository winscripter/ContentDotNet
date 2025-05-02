using System.Runtime.CompilerServices;

namespace ContentDotNet;

#pragma warning disable

/// <summary>
///   A container for 4 <see cref="bool"/>s.
/// </summary>
public struct Container4Boolean : IEquatable<Container4Boolean>
{
    private bool _0, _1, _2, _3;

    public Container4Boolean()
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

    public bool this[int index]
    {
        get
        {
            if ((uint)index >= 4)
                throw new IndexOutOfRangeException();

            ref bool firstElement = ref _0;
            return Unsafe.Add(ref firstElement, index);
        }
        set
        {
            if ((uint)index >= 4)
                throw new IndexOutOfRangeException();

            ref bool firstElement = ref _0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Container4Boolean @int && Equals(@int);
    }

    public bool Equals(Container4Boolean other)
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

    public static bool operator ==(Container4Boolean left, Container4Boolean right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Container4Boolean left, Container4Boolean right)
    {
        return !(left == right);
    }
}
