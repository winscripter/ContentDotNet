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
