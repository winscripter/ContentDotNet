namespace ContentDotNet.Extensions.H264.Models;

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
