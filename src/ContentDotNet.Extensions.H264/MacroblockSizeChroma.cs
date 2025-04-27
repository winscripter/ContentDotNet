namespace ContentDotNet.Extensions.H264;

// MbWidthC, MbHeightC
public readonly record struct MacroblockSizeChroma(int Width, int Height)
{
    public static readonly MacroblockSizeChroma Zero = new(0, 0);
}
