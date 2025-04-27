namespace ContentDotNet.Extensions.H264;

public struct DerivationContext
{
    public int MbAddrX;
    public bool IsMbaff;
    public bool IsMbaffFieldMacroblock;
    public MacroblockSizeChroma Sizes;
    public NeighboringMacroblocks NeighboringMacroblocks;
    public int CurrMbAddr;
    public int MbType;
    public int SubMbType;
    public int PictureWidthInSamplesL;
    public bool MbAddrXFrameFlag;
    public int BitDepthY;
    public int BitDepthC;
}
