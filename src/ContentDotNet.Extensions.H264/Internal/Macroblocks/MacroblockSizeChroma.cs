namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

// MbWidthC, MbHeightC
internal readonly record struct MacroblockSizeChroma(int Width, int Height)
{
    public static readonly MacroblockSizeChroma Zero = new(0, 0);
}
