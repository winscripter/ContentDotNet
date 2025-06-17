using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Internal;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Minimal;

/// <summary>
///   Represents a minimal residual.
/// </summary>
public struct MinimalResidual
{
    /// <summary>
    ///   Is CABAC preferred?
    /// </summary>
    public bool PreferCabac;

    /// <summary>
    ///   First Luma residual
    /// </summary>
    public MinimalLumaResidual FirstLumaResidual;

    /// <summary />
    public MinimalLumaResidual? Yuv444Cb;

    /// <summary />
    public MinimalLumaResidual? Yuv444Cr;

    /// <summary>
    ///   CABAC Cb and Cr
    /// </summary>
    public (MinimalCabacResidual First, MinimalCabacResidual Second)? CabacCbCr;

    /// <summary>
    ///   Container for the DC coefficients of the Intra 16x16 luma block.
    /// </summary>
    public Container64Byte Intra16x16DCLevel;

    /// <summary>
    ///   Container for the AC coefficients of the Intra 16x16 luma block.
    /// </summary>
    public ContainerMatrix16x16Byte Intra16x16ACLevel;

    /// <summary>
    ///   Container for the luma coefficients in 8x8 transform blocks.
    /// </summary>
    public ContainerMatrix4x64Byte Level8x8;

    /// <summary>
    ///   Container for the luma coefficients in 4x4 transform blocks.
    /// </summary>
    public ContainerMatrix4x64Byte Level4x4;

    /// <summary>
    ///   Container for the DC coefficients of the chroma blue (Cb) 16x16 block.
    /// </summary>
    public Container64Byte Cb16x16DCLevel;

    /// <summary>
    ///   Container for the AC coefficients of the chroma blue (Cb) 16x16 block.
    /// </summary>
    public ContainerMatrix16x16Byte Cb16x16ACLevel;

    /// <summary>
    ///   Container for the chroma blue (Cb) coefficients in 8x8 transform blocks.
    /// </summary>
    public ContainerMatrix4x64Byte CbLevel8x8;

    /// <summary>
    ///   Container for the chroma blue (Cb) coefficients in 4x4 transform blocks.
    /// </summary>
    public ContainerMatrix4x64Byte CbLevel4x4;

    /// <summary>
    ///   Container for the DC coefficients of the chroma red (Cr) 16x16 block.
    /// </summary>
    public Container64Byte Cr16x16DCLevel;

    /// <summary>
    ///   Container for the AC coefficients of the chroma red (Cr) 16x16 block.
    /// </summary>
    public ContainerMatrix16x16Byte Cr16x16ACLevel;

    /// <summary>
    ///   Container for the chroma red (Cr) coefficients in 8x8 transform blocks.
    /// </summary>
    public ContainerMatrix4x64Byte CrLevel8x8;

    /// <summary>
    ///   Container for the chroma red (Cr) coefficients in 4x4 transform blocks.
    /// </summary>
    public ContainerMatrix4x64Byte CrLevel4x4;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public MinimalResidual(bool preferCabac, MinimalLumaResidual firstLumaResidual, MinimalLumaResidual? yuv444Cb, MinimalLumaResidual? yuv444Cr, Container64Byte intra16x16DCLevel, ContainerMatrix16x16Byte intra16x16ACLevel, ContainerMatrix4x64Byte level8x8, ContainerMatrix4x64Byte level4x4, Container64Byte cb16x16DCLevel, ContainerMatrix16x16Byte cb16x16ACLevel, ContainerMatrix4x64Byte cbLevel8x8, ContainerMatrix4x64Byte cbLevel4x4, Container64Byte cr16x16DCLevel, ContainerMatrix16x16Byte cr16x16ACLevel, ContainerMatrix4x64Byte crLevel8x8, ContainerMatrix4x64Byte crLevel4x4, (MinimalCabacResidual First, MinimalCabacResidual Second)? cabacCbCr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        PreferCabac = preferCabac;
        FirstLumaResidual = firstLumaResidual;
        Yuv444Cb = yuv444Cb;
        Yuv444Cr = yuv444Cr;
        Intra16x16DCLevel = intra16x16DCLevel;
        Intra16x16ACLevel = intra16x16ACLevel;
        Level8x8 = level8x8;
        Level4x4 = level4x4;
        Cb16x16DCLevel = cb16x16DCLevel;
        Cb16x16ACLevel = cb16x16ACLevel;
        CbLevel8x8 = cbLevel8x8;
        CbLevel4x4 = cbLevel4x4;
        Cr16x16DCLevel = cr16x16DCLevel;
        Cr16x16ACLevel = cr16x16ACLevel;
        CrLevel8x8 = crLevel8x8;
        CrLevel4x4 = crLevel4x4;
        CabacCbCr = cabacCbCr;
    }

    /// <summary>
    ///   Converts from an actual residual.
    /// </summary>
    /// <param name="residual"></param>
    /// <returns></returns>
    public static MinimalResidual From(Residual residual)
        => new(residual.PreferCabac, MinimalLumaResidual.From(residual.FirstLumaResidual),
            residual.Yuv444Cb is not null ? MinimalLumaResidual.From(residual.Yuv444Cb.Value) : null,
            residual.Yuv444Cr is not null ? MinimalLumaResidual.From(residual.Yuv444Cr.Value) : null,
            ContainerConverter.ToBytes(residual.Intra16x16DCLevel),
            ContainerConverter.ToBytes(residual.Intra16x16ACLevel),
            ContainerConverter.ToBytes(residual.Level8x8),
            ContainerConverter.ToBytes(residual.Level4x4),
            ContainerConverter.ToBytes(residual.Cb16x16DCLevel),
            ContainerConverter.ToBytes(residual.Cb16x16ACLevel),
            ContainerConverter.ToBytes(residual.CbLevel8x8),
            ContainerConverter.ToBytes(residual.CbLevel4x4),
            ContainerConverter.ToBytes(residual.Cr16x16DCLevel),
            ContainerConverter.ToBytes(residual.Cr16x16ACLevel),
            ContainerConverter.ToBytes(residual.CrLevel8x8),
            ContainerConverter.ToBytes(residual.CrLevel4x4),
            residual.CabacCbCr is not null ? (MinimalCabacResidual.From(residual.CabacCbCr.Value.First), MinimalCabacResidual.From(residual.CabacCbCr.Value.Second)) : null);
}
