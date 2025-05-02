using System.Runtime.CompilerServices;

namespace ContentDotNet;

/// <summary>
///   Creates representations of data sizes in bytes.
/// </summary>
public static class DataSize
{
    /// <summary>
    /// Converts the specified number of kilobytes to bytes.
    /// </summary>
    /// <param name="numKBs">The number of kilobytes.</param>
    /// <returns>The equivalent size in bytes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Kilobytes(int numKBs) => numKBs * 1024;

    /// <summary>
    /// Converts the specified number of megabytes to bytes.
    /// </summary>
    /// <param name="numMBs">The number of megabytes.</param>
    /// <returns>The equivalent size in bytes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Megabytes(int numMBs) => numMBs * 1024 * 1024;

    /// <summary>
    /// Converts the specified number of gigabytes to bytes.
    /// </summary>
    /// <param name="numGBs">The number of gigabytes.</param>
    /// <returns>The equivalent size in bytes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Gigabytes(int numGBs) => numGBs * 1024 * 1024 * 1024;

    /// <summary>
    /// Converts the specified number of kilobytes to bytes as a long value.
    /// </summary>
    /// <param name="numKBs">The number of kilobytes.</param>
    /// <returns>The equivalent size in bytes as a long value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long LongKilobytes(long numKBs) => numKBs * 1024;

    /// <summary>
    /// Converts the specified number of megabytes to bytes as a long value.
    /// </summary>
    /// <param name="numMBs">The number of megabytes.</param>
    /// <returns>The equivalent size in bytes as a long value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long LongMegabytes(long numMBs) => numMBs * 1024 * 1024;

    /// <summary>
    /// Converts the specified number of gigabytes to bytes as a long value.
    /// </summary>
    /// <param name="numGBs">The number of gigabytes.</param>
    /// <returns>The equivalent size in bytes as a long value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long LongGigabytes(long numGBs) => numGBs * 1024 * 1024 * 1024;

    /// <summary>
    /// Converts the specified number of terabytes to bytes as a long value.
    /// </summary>
    /// <param name="numTBs">The number of terabytes.</param>
    /// <returns>The equivalent size in bytes as a long value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long LongTerabytes(long numTBs) => numTBs * 1024 * 1024 * 1024 * 1024;
}
