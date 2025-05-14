using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal readonly struct FilterOffsets : IEquatable<FilterOffsets>
{
    public readonly int A;
    public readonly int B;

    public FilterOffsets(int a, int b)
    {
        A = a;
        B = b;
    }

    public static FilterOffsets Compute(SliceHeader sliceHeader) =>
        new((sliceHeader.SliceAlphaC0OffsetDiv2) << 1, (sliceHeader.SliceBetaOffsetDiv2) << 1);

    public override bool Equals(object? obj)
    {
        return obj is FilterOffsets offsets && Equals(offsets);
    }

    public bool Equals(FilterOffsets other)
    {
        return A == other.A &&
               B == other.B;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(A, B);
    }

    public static bool operator ==(FilterOffsets left, FilterOffsets right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(FilterOffsets left, FilterOffsets right)
    {
        return !(left == right);
    }
}
