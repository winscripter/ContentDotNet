using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Cabac.Uegk;

internal readonly record struct PossiblyPrefixSuffix
{
    private readonly int integerValue;
    private readonly UegkPrefixSuffixRecord uegkValue;
    private readonly bool isUegk;

    public PossiblyPrefixSuffix(int integerValue, UegkPrefixSuffixRecord uegkValue, bool isUegk)
    {
        this.integerValue = integerValue;
        this.uegkValue = uegkValue;
        this.isUegk = isUegk;
    }

    public int IntegerValue => integerValue;
    public UegkPrefixSuffixRecord UegkValue => uegkValue;
    public bool IsUegk => isUegk;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PossiblyPrefixSuffix FromInteger(int value)
    {
        return new PossiblyPrefixSuffix(value, default, false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PossiblyPrefixSuffix FromUegk(UegkPrefixSuffixRecord uegkValue)
    {
        return new PossiblyPrefixSuffix(0, uegkValue, true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PossiblyPrefixSuffix FromUegk(int prefix, int suffix)
    {
        return new PossiblyPrefixSuffix(0, new UegkPrefixSuffixRecord(prefix, suffix), true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator PossiblyPrefixSuffix(int value) => FromInteger(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator PossiblyPrefixSuffix(UegkPrefixSuffixRecord uegkValue) => FromUegk(uegkValue);
}
