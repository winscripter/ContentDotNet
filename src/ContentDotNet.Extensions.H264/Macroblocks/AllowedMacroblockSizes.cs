namespace ContentDotNet.Extensions.H264.Macroblocks;

/// <summary>
///   Specifies which macroblock sizes are allowed.
/// </summary>
[Flags]
public enum AllowedMacroblockSizes
{
    /// <summary>
    ///   Allows 16x16 intra macroblocks.
    /// </summary>
    Intra16x16 = 1,

    /// <summary>
    ///   Allows 8x8 intra macroblocks.
    /// </summary>
    Intra8x8 = 2,

    /// <summary>
    ///   Allows 4x4 intra macroblocks.
    /// </summary>
    Intra4x4 = 4,

    /// <summary>
    ///   Allows 8x4 inter macroblocks.
    /// </summary>
    Inter8x4 = 8,

    /// <summary>
    ///   Allows 4x8 inter macroblocks.
    /// </summary>
    Inter4x8 = 16,

    /// <summary>
    ///   Allows 4x4 inter macroblocks.
    /// </summary>
    Inter4x4 = 32,

    /// <summary>
    ///   Allows 8x8 inter macroblocks.
    /// </summary>
    Inter8x8 = 64,

    /// <summary>
    ///   Allows 8x16 inter macroblocks.
    /// </summary>
    Inter8x16 = 128,

    /// <summary>
    ///   Allows 16x8 inter macroblocks.
    /// </summary>
    Inter16x8 = 256,

    /// <summary>
    ///   Allows 16x16 inter macroblocks.
    /// </summary>
    Inter16x16 = 512
}
