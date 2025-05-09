namespace ContentDotNet.Extensions.H264.Macroblocks;

internal sealed class MacroblockUtility : IMacroblockUtility
{
    public bool AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(int address)
    {
        throw new NotImplementedException();
    }

    public void GetIntra16x16PredMode(int mbAddr, Span<int> output)
    {
        throw new NotImplementedException();
    }

    public void GetIntra4x4PredMode(int mbAddr, Span<int> output)
    {
        throw new NotImplementedException();
    }

    public void GetIntra8x8PredMode(int mbAddr, Span<int> output)
    {
        throw new NotImplementedException();
    }

    public uint GetMbType(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public int GetTotalCoefficient(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsCodedWithInter(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsCodedWithIntra(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsCodedWithIntra16x16(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsCodedWithIntra4x4(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsCodedWithIntra8x8(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsFieldMacroblock(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsFrameMacroblock(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsMacroblockOfTypeSi(int mbAddr)
    {
        throw new NotImplementedException();
    }
}
