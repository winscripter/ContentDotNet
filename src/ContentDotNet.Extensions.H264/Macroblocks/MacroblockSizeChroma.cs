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
}
