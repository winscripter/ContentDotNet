namespace ContentDotNet.Extensions.H264.Models;

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
