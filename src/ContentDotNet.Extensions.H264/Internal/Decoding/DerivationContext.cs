using ContentDotNet.Extensions.H264.Internal.Macroblocks;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal struct DerivationContext
{
    public int MbAddrX;
    public bool IsMbaff;
    public bool IsMbaffFieldMacroblock;
    public MacroblockSizeChroma Sizes;
    public NeighboringMacroblocks NeighboringMacroblocks;
    public int CurrMbAddr;
    public int MbType;
    public int SubMbType;
    public bool MbAddrXFrameFlag;
}
