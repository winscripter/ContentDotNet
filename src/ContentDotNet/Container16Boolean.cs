using System.Runtime.CompilerServices;

namespace ContentDotNet;

#pragma warning disable

/// <summary>
///   A container for 16 <see cref="bool"/>s.
/// </summary>
public struct Container16Boolean : IEquatable<Container16Boolean>
{
    private bool _0, _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15;

    public Container16Boolean()
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

    public bool this[int index]
    {
        get
        {
            if ((uint)index >= 16)
                throw new IndexOutOfRangeException();

            ref bool firstElement = ref _0;
            return Unsafe.Add(ref firstElement, index);
        }
        set
        {
            if ((uint)index >= 16)
                throw new IndexOutOfRangeException();

            ref bool firstElement = ref _0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Container16Boolean boolean && Equals(boolean);
    }

    public bool Equals(Container16Boolean other)
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

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
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

    public static bool operator ==(Container16Boolean left, Container16Boolean right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Container16Boolean left, Container16Boolean right)
    {
        return !(left == right);
    }
}
