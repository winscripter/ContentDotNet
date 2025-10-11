namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Shared.ItuT.Exceptions;

    /// <summary>
    ///   Represents the macroblock utility.
    /// </summary>
    public interface IMacroblockUtility
    {
        /// <summary>
        ///   The slice type.
        /// </summary>
        uint SliceType { get; }

        /// <summary>
        ///   Returns the macroblock at <paramref name="mbAddr"/>.
        /// </summary>
        /// <param name="mbAddr">The macroblock address.</param>
        /// <returns>Returned macroblock.</returns>
        /// <exception cref="MacroblockNotFoundException">Thrown when the macroblock wasn't found or is out of range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mbAddr"/> is negative.</exception>
        H264MacroblockInfo GetMacroblock(int mbAddr);

        /// <summary>
        ///   Returns the slice type for macroblock <paramref name="mb"/>
        /// </summary>
        /// <param name="mb">The macroblock.</param>
        /// <returns>Returned slice type.</returns>
        /// <exception cref="MacroblockNotFoundException">Thrown when the macroblock wasn't found or is out of range.</exception>
        H264SliceType GetSliceType(H264MacroblockInfo mb);

        /// <summary>
        ///   Applies the inference rule for macroblock at <paramref name="mbAddr"/>.
        /// </summary>
        /// <param name="mbAddr">The macroblock address.</param>
        /// <exception cref="MacroblockNotFoundException">Thrown when the macroblock wasn't found or is out of range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mbAddr"/> is negative.</exception>
        void Infer(int mbAddr);

        /// <summary>
        ///   Returns a boolean at <paramref name="mbAddr"/>, indicating if that macroblock is a frame macroblock.
        /// </summary>
        /// <param name="mbAddr">The macroblock address.</param>
        /// <returns>Returned boolean.</returns>
        /// <remarks>
        ///   This method shall return false when MBAFF mode is off.
        /// </remarks>
        /// <exception cref="MacroblockNotFoundException">Thrown when the macroblock wasn't found or is out of range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mbAddr"/> is negative.</exception>
        bool IsFrame(int mbAddr);

        /// <summary>
        ///   Returns a boolean for <paramref name="mb"/>, indicating if that macroblock is a frame macroblock.
        /// </summary>
        /// <param name="mb">The macroblock.</param>
        /// <returns>Returned boolean.</returns>
        /// <remarks>
        ///   This method shall return false when MBAFF mode is off.
        /// </remarks>
        /// <exception cref="MacroblockNotFoundException">Thrown when the macroblock wasn't found or is out of range.</exception>
        bool IsFrame(H264MacroblockInfo mb);

        /// <summary>
        ///   Returns a macroblock at <paramref name="mbAddr"/>, indicating if that macroblock is a valid macroblock.
        /// </summary>
        /// <param name="mbAddr">The macroblock address.</param>
        /// <returns>Returned boolean.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mbAddr"/> is negative.</exception>
        bool IsMacroblock(int mbAddr);
    }
}
