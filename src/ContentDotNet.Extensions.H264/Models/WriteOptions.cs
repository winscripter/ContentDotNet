namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// HRD write options.
/// </summary>
public struct MemoryHrdWriteOptions
{
    public ReadOnlyMemory<uint> BitRateValueMinus1;
    public ReadOnlyMemory<uint> CpbSizeValueMinus1;
    public ReadOnlyMemory<bool> CbrFlag;

    public MemoryHrdWriteOptions(ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        BitRateValueMinus1 = bitRateValueMinus1;
        CpbSizeValueMinus1 = cpbSizeValueMinus1;
        CbrFlag = cbrFlag;
    }
}

public readonly struct MemoryVuiWriteOptions
{
    public readonly MemoryHrdWriteOptions NalHrdWriteOptions;
    public readonly MemoryHrdWriteOptions VclHrdWriteOptions;
    public readonly bool IsNalPresent;
    public readonly bool IsVclPresent;

    public MemoryVuiWriteOptions(MemoryHrdWriteOptions nalHrdWriteOptions, MemoryHrdWriteOptions vclHrdWriteOptions, bool isNalPresent, bool isVclPresent)
    {
        NalHrdWriteOptions = nalHrdWriteOptions;
        VclHrdWriteOptions = vclHrdWriteOptions;
        IsNalPresent = isNalPresent;
        IsVclPresent = isVclPresent;
    }
}

public readonly ref struct VuiWriteOptions
{
    public readonly HrdWriteOptions NalHrdWriteOptions;
    public readonly HrdWriteOptions VclHrdWriteOptions;
    public readonly bool IsNalPresent;
    public readonly bool IsVclPresent;

    public VuiWriteOptions(HrdWriteOptions nalHrdWriteOptions, HrdWriteOptions vclHrdWriteOptions, bool isNalPresent, bool isVclPresent)
    {
        NalHrdWriteOptions = nalHrdWriteOptions;
        VclHrdWriteOptions = vclHrdWriteOptions;
        IsNalPresent = isNalPresent;
        IsVclPresent = isVclPresent;
    }
}

/// <summary>
/// HRD write options.
/// </summary>
public ref struct HrdWriteOptions
{
    public ReadOnlySpan<uint> BitRateValueMinus1;
    public ReadOnlySpan<uint> CpbSizeValueMinus1;
    public ReadOnlySpan<bool> CbrFlag;

    public HrdWriteOptions(ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        BitRateValueMinus1 = bitRateValueMinus1;
        CpbSizeValueMinus1 = cpbSizeValueMinus1;
        CbrFlag = cbrFlag;
    }
}
