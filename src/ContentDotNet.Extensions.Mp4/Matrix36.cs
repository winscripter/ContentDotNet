using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.Mp4;

#pragma warning disable

internal struct Matrix36Internal : IEquatable<Matrix36Internal>
{
    private byte f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16,
                 f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28, f29, f30, f31, f32, f33, f34, f35
                 ;

    public Matrix36Internal()
    {
    }

    public byte this[int index]
    {
        get
        {
            ref byte firstElement = ref f0;
            return Unsafe.Add(ref firstElement, index);
        }

        set
        {
            ref byte firstElement = ref f0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Matrix36Internal @internal && Equals(@internal);
    }

    public bool Equals(Matrix36Internal other)
    {
        return f0 == other.f0 &&
               f1 == other.f1 &&
               f2 == other.f2 &&
               f3 == other.f3 &&
               f4 == other.f4 &&
               f5 == other.f5 &&
               f6 == other.f6 &&
               f7 == other.f7 &&
               f8 == other.f8 &&
               f9 == other.f9 &&
               f10 == other.f10 &&
               f11 == other.f11 &&
               f12 == other.f12 &&
               f13 == other.f13 &&
               f14 == other.f14 &&
               f15 == other.f15 &&
               f16 == other.f16 &&
               f17 == other.f17 &&
               f18 == other.f18 &&
               f19 == other.f19 &&
               f20 == other.f20 &&
               f21 == other.f21 &&
               f22 == other.f22 &&
               f23 == other.f23 &&
               f24 == other.f24 &&
               f25 == other.f25 &&
               f26 == other.f26 &&
               f27 == other.f27 &&
               f28 == other.f28 &&
               f29 == other.f29 &&
               f30 == other.f30 &&
               f31 == other.f31 &&
               f32 == other.f32 &&
               f33 == other.f33 &&
               f34 == other.f34 &&
               f35 == other.f35;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(f0);
        hash.Add(f1);
        hash.Add(f2);
        hash.Add(f3);
        hash.Add(f4);
        hash.Add(f5);
        hash.Add(f6);
        hash.Add(f7);
        hash.Add(f8);
        hash.Add(f9);
        hash.Add(f10);
        hash.Add(f11);
        hash.Add(f12);
        hash.Add(f13);
        hash.Add(f14);
        hash.Add(f15);
        hash.Add(f16);
        hash.Add(f17);
        hash.Add(f18);
        hash.Add(f19);
        hash.Add(f20);
        hash.Add(f21);
        hash.Add(f22);
        hash.Add(f23);
        hash.Add(f24);
        hash.Add(f25);
        hash.Add(f26);
        hash.Add(f27);
        hash.Add(f28);
        hash.Add(f29);
        hash.Add(f30);
        hash.Add(f31);
        hash.Add(f32);
        hash.Add(f33);
        hash.Add(f34);
        hash.Add(f35);
        return hash.ToHashCode();
    }

    public static bool operator ==(Matrix36Internal left, Matrix36Internal right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Matrix36Internal left, Matrix36Internal right)
    {
        return !(left == right);
    }
}

#pragma warning restore

public struct Matrix36 : IEquatable<Matrix36>
{
    private Matrix36Internal matrix;

    public Matrix36()
    {
        matrix = new Matrix36Internal();
    }

    public byte this[int index]
    {
        get
        {
            if (index > 36 || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return this.matrix[index];
        }

        set
        {
            if (index > 36 || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            this.matrix[index] = value;
        }
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Matrix36 matrix && Equals(matrix);
    }

    public readonly bool Equals(Matrix36 other)
    {
        return matrix.Equals(other.matrix);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(matrix);
    }

    public static bool operator ==(Matrix36 left, Matrix36 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Matrix36 left, Matrix36 right)
    {
        return !(left == right);
    }
}
