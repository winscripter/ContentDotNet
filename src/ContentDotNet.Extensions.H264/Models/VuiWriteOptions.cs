namespace ContentDotNet.Extensions.H264.Models;

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
