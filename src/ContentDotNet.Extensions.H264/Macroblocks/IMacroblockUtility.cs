using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Macroblocks;

/// <summary>
/// Provides utility methods for working with macroblocks in H.264 video streams.
/// </summary>
public interface IMacroblockUtility
{
    /// <summary>
    /// Retrieves the sub-macroblock type for the macroblock at the specified address.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>The sub-macroblock type as an unsigned integer.</returns>
    uint GetSubMbType(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is coded with Intra 4x4 prediction mode.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if coded with Intra 4x4, otherwise false.</returns>
    bool IsCodedWithIntra4x4(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is coded with Intra 8x8 prediction mode.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if coded with Intra 8x8, otherwise false.</returns>
    bool IsCodedWithIntra8x8(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is coded with Intra 16x16 prediction mode.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if coded with Intra 16x16, otherwise false.</returns>
    bool IsCodedWithIntra16x16(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is coded with any Intra prediction mode.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if coded with Intra, otherwise false.</returns>
    bool IsCodedWithIntra(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is coded with Inter prediction mode.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if coded with Inter, otherwise false.</returns>
    bool IsCodedWithInter(int mbAddr);

    /// <summary>
    /// Retrieves the Intra 4x4 prediction modes for the macroblock at the specified address.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <param name="output">A span to store the prediction modes.</param>
    void GetIntra4x4PredMode(int mbAddr, Span<int> output);

    /// <summary>
    /// Retrieves the Intra 8x8 prediction modes for the macroblock at the specified address.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <param name="output">A span to store the prediction modes.</param>
    void GetIntra8x8PredMode(int mbAddr, Span<int> output);

    /// <summary>
    /// Retrieves the Intra 16x16 prediction modes for the macroblock at the specified address.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <param name="output">A span to store the prediction modes.</param>
    void GetIntra16x16PredMode(int mbAddr, Span<int> output);

    /// <summary>
    /// Determines if the macroblock at the specified address is a frame macroblock.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if it is a frame macroblock, otherwise false.</returns>
    bool IsFrameMacroblock(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is a field macroblock.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if it is a field macroblock, otherwise false.</returns>
    bool IsFieldMacroblock(int mbAddr);

    /// <summary>
    /// Determines if the macroblock at the specified address is of type SI.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>True if it is of type SI, otherwise false.</returns>
    bool IsMacroblockOfTypeSi(int mbAddr);

    /// <summary>
    /// Checks if all AC residual transforms are zero due to coded block patterns being zero for the macroblock at the specified address.
    /// </summary>
    /// <param name="address">The macroblock address.</param>
    /// <returns>True if all AC residual transforms are zero, otherwise false.</returns>
    bool AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(int address);

    /// <summary>
    /// Retrieves the macroblock type for the macroblock at the specified address.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>The macroblock type as an unsigned integer.</returns>
    uint GetMbType(int mbAddr);

    /// <summary>
    /// Retrieves the total coefficient count for the macroblock at the specified address.
    /// </summary>
    /// <param name="mbAddr">The macroblock address.</param>
    /// <returns>The total coefficient count.</returns>
    int GetTotalCoefficient(int mbAddr);

    /// <summary>
    ///   Returns the macroblock at address.
    /// </summary>
    /// <param name="address">Address of the macroblock.</param>
    /// <returns>Macroblock at given address, or.</returns>
    MacroblockLayer GetMacroblock(int address);

    /// <summary>
    ///   Returns the macroblock to the left of the address.
    /// </summary>
    /// <param name="address">Address of the macroblock, to the left.</param>
    /// <returns>
    /// Macroblock to the left of <paramref name="address"/>, or null if it
    /// can't be obtained (e.g. it's already at the very left of the video).
    /// </returns>
    MacroblockLayer? GetMacroblockToTheLeft(int address);

    /// <summary>
    ///   Returns the macroblock to the top of the address.
    /// </summary>
    /// <param name="address">Address of the macroblock, to the top.</param>
    /// <returns>
    /// Macroblock to the top of <paramref name="address"/>, or null if it
    /// can't be obtained (e.g. it's already at the very top of the video).
    /// </returns>
    MacroblockLayer? GetMacroblockToTheTop(int address);

    /// <summary>
    ///   Returns the macroblock to the right of the address.
    /// </summary>
    /// <param name="address">Address of the macroblock, to the right.</param>
    /// <returns>
    /// Macroblock to the right of <paramref name="address"/>, or null if it
    /// can't be obtained (e.g. it's already at the very right of the video).
    /// </returns>
    MacroblockLayer? GetMacroblockToTheRight(int address);

    /// <summary>
    ///   Returns the macroblock to the bottom of the address.
    /// </summary>
    /// <param name="address">Address of the macroblock, to the bottom.</param>
    /// <returns>
    /// Macroblock to the bottom of <paramref name="address"/>, or null if it
    /// can't be obtained (e.g. it's already at the very bottom of the video).
    /// </returns>
    MacroblockLayer? GetMacroblockToTheBottom(int address);

    /// <summary>
    ///   Returns the value of <c>mb_skip_flag</c> for a macroblock at given address.
    /// </summary>
    /// <param name="mbAddr">Address of the macroblock to compute value of <c>mb_skip_flag</c> for.</param>
    /// <returns><c>mb_skip_flag</c> for macroblock at address <paramref name="mbAddr"/>.</returns>
    bool IsMbSkipFlagForMacroblock(int mbAddr);
}
