using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Minimal;

/// <summary>
///   H.264 Minimal CABAC residual
/// </summary>
public readonly struct MinimalCabacResidual
{
    /// <summary>
    ///   CodedBlockFlag
    /// </summary>
    public readonly bool CodedBlockFlag;

    /// <summary>
    ///   Initializes a new instance of the <see cref="MinimalCabacResidual"/> structure.
    /// </summary>
    /// <param name="codedBlockFlag">CodedBlockFlag</param>
    public MinimalCabacResidual(bool codedBlockFlag) => CodedBlockFlag = codedBlockFlag;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static MinimalCabacResidual From(CabacResidual cabac)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        => new(cabac.CodedBlockFlag);
}
