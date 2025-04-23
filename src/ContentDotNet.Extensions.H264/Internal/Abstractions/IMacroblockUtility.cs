namespace ContentDotNet.Extensions.H264.Internal.Abstractions;

internal interface IMacroblockUtility
{
    bool IsCodedWithIntra4x4(int mbAddr);
    bool IsCodedWithIntra8x8(int mbAddr);
    bool IsCodedWithIntra16x16(int mbAddr);
    bool IsCodedWithInter(int mbAddr);

    void GetIntra4x4PredMode(int mbAddr, Span<int> output);
    void GetIntra8x8PredMode(int mbAddr, Span<int> output);
    void GetIntra16x16PredMode(int mbAddr, Span<int> output);

    bool IsFrameMacroblock(int mbAddr);
    bool IsMacroblockOfTypeSi(int mbAddr);
}
