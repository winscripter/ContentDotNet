namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Provides utility methods for working with macroblocks in H.264 video streams.
/// </summary>
public interface IMacroblockUtility
{
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
}
