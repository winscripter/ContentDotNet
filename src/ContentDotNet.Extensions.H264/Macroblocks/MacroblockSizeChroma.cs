namespace ContentDotNet.Extensions.H264.Macroblocks;

/// <summary>
///   Represents a Macroblock size in the Chroma channel (MbHeightC/MbWidthC).
/// </summary>
/// <param name="Width">Width of the macroblock.</param>
/// <param name="Height">Height of the macroblock.</param>
public readonly record struct MacroblockSizeChroma(int Width, int Height)
{
    /// <summary>
    ///   A singleton macroblock with (0, 0) as value.
    /// </summary>
    public static readonly MacroblockSizeChroma Zero = new(0, 0);

    internal static MacroblockSizeChroma From(uint chromaFormatIdc)
    {
        return chromaFormatIdc switch
        {
            0 => Zero,
            1 => new MacroblockSizeChroma(8, 8),
            2 => new MacroblockSizeChroma(8, 16),
            3 => new MacroblockSizeChroma(16, 16),
            _ => throw new InvalidOperationException("Invalid chroma_format_idc")
        };
    }
}
